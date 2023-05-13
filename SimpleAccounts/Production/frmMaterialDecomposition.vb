'Public Class frmMaterialDecomposition
'' TASK TFS2006 on 28-12-2017 done by Muhammad Ameen found out the issue that in some cases account id wiht item was zero.
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports SBDal
Imports SBModel
Public Class frmMaterialDecomposition
    Implements IGeneral
    Dim bal As MaterialDecompositionBAL
    Dim master As MaterialDecompositionModel
    Dim dt As DataTable
    Dim IsEditMode As Boolean = False
    Dim ID As Integer = 0
    Dim dtCriteriaWiseCostSheet As DataTable
    Dim dtReturnMultipleCostSheets As DataTable
    Dim dtCostSheetAgainstsTicketItems As DataTable
    Dim AssociateItems As String = "True"
    Dim RowIndex As Integer = 0
    Dim ChildSerialNo As String = ""
    Dim SerialNo As String = ""
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim DecomposedQty As Double = 0
    Dim WastedQty As Double = 0
    Dim ScrappedQty As Double = 0
    Dim dtGlobe As DataTable
    Dim RowIndex1 As Integer = 0
    Dim IsAnyChildDecomposed As Boolean = False
    Dim IsParentDecomposed As Boolean = False
    Dim ChildCount As Integer = 0
    Dim Count As Integer = 0
    Dim ConsumedCount As Integer = 0
    Dim TotalAvailableQty As Double = 0
    Dim TotalConsumedQty As Double = 0

    Dim WastedStockAccount As Integer = 0
    Dim ScrappedStockAccount As Integer = 0
    Dim VoucherId As Integer = 0

    Dim TotalDValue As Double = 0
    Dim TotalWValue As Double = 0
    Dim TotalSValue As Double = 0


    Dim DValue As Double = 0
    Dim WValue As Double = 0
    Dim SValue As Double = 0
    Dim IsParentVoucher As Boolean = False
    Dim IsFirstCount As Boolean = False
    Dim IsSameType As Boolean = False

    Dim Counter As Integer = 0
    Dim PlanTicketId As Integer = 0
    Dim PlanNo As String = String.Empty
    Dim TicketNo As String = String.Empty
    Dim dtResult As DataTable

    Dim PlanItemId As Integer = 0
    Dim PlanItem As String = String.Empty
    Dim PlanItemSubSubId As Integer = 0
    Dim CheckChildIsConsumed As Integer = 0

    'At the moment, it is a business rule that stock impact of a top parent will not be made. The following constant is kept to indicate
    'the business rule. The purpose is to make it flag based in case in future the business rule changes. In such such case, we will not
    'need to chage the coding since coding will be done for both value of this flag.
    'Private Const STOCK_IMPACT_OF_TOP_PARENT_ITEM As Boolean = False


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        bal = New MaterialDecompositionBAL()
    End Sub

    Enum ConsumptionType
        DECOMPOSED = 10
        WASTED = 20
        SCRAPPED = 30
    End Enum

    Private Structure GridColumns
        Public Const PRICE As String = "Price"
        Public Const QTY As String = "Qty"
        Public Const DECOMPOSED_QTY As String = "DecomposedQty"
        Public Const SCRAPPED_QTY As String = "ScrappedQty"
        Public Const WASTED_QTY As String = "WastedQty"
        Public Const AVAILABLE_QTY As String = "DecomposableQty"
        Public Const TOTAL_CONSUMED_QTY As String = "TotalConsumedQty"
        Public Const IS_TOP_PARENT As String = "IsTopParent"
        Public Const STOCK_IMPACT As String = "StockImpact"
        Public Const CONSUMED_CHILD_COUNT As String = "ConsumedChildCount"
        Public Const DValue As String = "DValue"
        Public Const WValue As String = "WValue"
        Public Const SValue As String = "SValue"
    End Structure
    Enum grdEnum
        MaterialEstMasterID
        MasterArticleID
        ArticleID
        PlanItem
        ArticleDescription
        ArticleUnitId
        Unit
        Qty
        Price
        Types
        Tag
        SubDepartmentID
        SubDepartment
        Approve
        PlanTicketsId
        CostSheetID
        Delete
        Add
        Subtract
    End Enum
    Private Sub lblSpecialInstruction_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cbPlanId_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            bal.DeleteMaster(Me.grdSaved.GetRow.Cells("DecompositionId").Value)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        Try
            If Condition = "Customer" Then
                Str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code],  dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(tblCustomer.CridtLimt,0) as CreditLimit " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    Str += " AND (dbo.vwCOADetail.account_type = 'Customer')  "
                Else
                    Str += " AND (dbo.vwCOADetail.account_type in('Customer','Vendor'))  "
                End If
                'End Task:2504
                'If flgCompanyRights = True Then
                '    Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                'End If
                If IsEditMode = False Then
                    Str += " AND vwCOADetail.Active=1"
                Else
                    Str += " AND vwCOADetail.Active in(0,1,Null)"
                End If
                Str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
                FillUltraDropDown(cmbCustomer, Str)
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.Rows(0).Activate()
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("CreditLimit").Hidden = True 'Tas:2504 Set Hidden Column
                End If
            ElseIf Condition = "SO" Then
                Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo, SalesOrderMasterTable.SpecialInstructions FROM SalesOrderMasterTable WHERE VendorId = " & Me.cmbCustomer.Value & " Order by SalesOrderDate DESC"
                FillDropDown(cmbSalesOrder, Str)
            ElseIf Condition = "Plan" Then
                Str = "Select PlanMasterTable.PlanId, PlanMasterTable.PlanNo + ' ~ ' + Convert(Varchar(12), PlanMasterTable.PlanDate, 113) As PlanNo From PlanMasterTable INNER JOIN ProductionMasterTable ON PlanMasterTable.PlanId = ProductionMasterTable.PlanId " & IIf(Me.cmbSalesOrder.SelectedValue > 0, "Where PlanMasterTable.POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order By PlanMasterTable.PlanId DESC"
                FillDropDown(cmbPlan, Str)
            ElseIf Condition = "Plans" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSalesOrder.SelectedValue > 0, "Where POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order By PlanId DESC"
                FillDropDown(cmbPlan, Str)
                'ElseIf Condition = "Product" Then
                '    Str = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription As Product, PlanDetailTable.Qty From ArticleDefTable Inner Join PlanDetailTable On ArticleDefTable.ArtileId = PlanDetailTable.ArticleDefId Where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " "
                '    FillUltraDropDown(cmbTicket, Str)

            ElseIf Condition = "PlanNo" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSalesOrder.SelectedValue > 0, "Where POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order By PlanId DESC"
                FillDropDown(cmbPlan, Str)
            ElseIf Condition = "TicketNo" Then
                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster " & IIf(Me.cmbPlan.SelectedValue > 0, "Where PlanId = " & Me.cmbPlan.SelectedValue & "", "") & " Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillDropDown(cmbTicket, Str)

            ElseIf Condition = "Ticket" Then
                'PlanTicketsMasterID	int	Unchecked
                'TicketNo	nvarchar(50)	Checked
                'TicketDate	datetime	Checked
                'CustomerID	int	Checked
                'SalesOrderID	int	Checked
                'PlanID	int	Checked
                'SpecialInstructions	nvarchar(300)	Checked
                'Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & " And PlanTicketsMaster.PlanTicketsMasterID Not in (Select PlanTicketId From MaterialEstimation) Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster INNER JOIN ProductionMasterTable ON PlanTicketsMaster.PlanTicketsMasterID = ProductionMasterTable.PlanTicketId Where ProductionMasterTable.PlanId = " & Me.cmbPlan.SelectedValue & " Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillDropDown(cmbTicket, Str)
                'Me.cmbTicket.Rows(0).Activate()
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 400
            ElseIf Condition = "Tickets" Then
                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster Order By PlanTicketsMaster.PlanTicketsMasterID DESC" ''Where PlanTicketsMaster.PlanTicketsMasterID Not in (Select PlanTicketId From MaterialEstimation)
                FillDropDown(cmbTicket, Str)
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                ''Me.cmbTicket.DisplayLayout.Bands(0).Columns("TicketNo").
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 400

                'ElseIf Condition = "Ticket" Then
                '    Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation) Order By Ticket.PlanTicketsId DESC"
                '    FillUltraDropDown(cmbTicket, Str)
                '    Me.cmbTicket.Rows(0).Activate()
                'ElseIf Condition = "Tickets" Then
                '    Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Order By Ticket.PlanTicketsId DESC"
                '    FillUltraDropDown(cmbTicket, Str)
                'Me.cmbTicket.Rows(0).Activate()

            ElseIf Condition = "Estimation" Then
                'Str = "Select Distinct ProdStep_Id, prod_step, sort_Order from tblProSteps INNER JOIN MaterialEstimationDetailTable Detail ON tblProSteps.ProdStep_Id = Detail.SubDepartmentID Where Detail.MaterialEstMasterID =" & cmbEstimation.Value & "  ORDER BY 2 ASC"
                'Str = "Select ProdStep_Id, prod_step, sort_Order from tblProSteps INNER JOIN MaterialEstimationDetailTable Detail ON tblProSteps.ProdStep_Id = Detail.SubDepartmentID Where Detail.MaterialEstMasterID =" & cmbEstimation.Value & "  ORDER BY 2 ASC"
                Str = "SELECT Id As EstimationId, DocNo + ' ~ ' + Convert(Varchar(12), EstimationDate, 113) As [Estimation No] FROM MaterialEstimation WHERE PlanTicketId = " & Me.cmbTicket.SelectedValue & ""
                FillDropDown(cmbEstimation, Str)
            ElseIf Condition = "grdLocations" Then
                Str = "Select Location_Id, Location_Code, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                Dim dt As DataTable = GetDataTable(Str)
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "Location" Then
                'Task#16092015 Load  Locations user wise
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")  order by sort_order"
                'Else
                '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
                'End If

                Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                           & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                           & " Else " _
                           & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


                FillDropDown(cmbCategory, Str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            grd.UpdateData()
            'MarkAllTopParents()
            ConsumeChildrenIfParentsAreConsumed()
            ConsumeParentsIfChildrenAreConsumed()
            MarkStockImpact()
            GetChildrenValueIfParentsAreConsumed()
            SetChildrenVoucherValueIfParentsNotAreConsumed()
            grd.UpdateData()
            Dim dt5 As DataTable = grd.DataSource
            master = New MaterialDecompositionModel()
            master.DocumentNo = Me.txtDocNo.Text
            master.CustomerId = IIf(Me.cmbCustomer.ActiveRow Is Nothing, 0, Me.cmbCustomer.Value)
            master.SalesOrderId = IIf(Me.cmbSalesOrder.SelectedValue = Nothing, 0, Me.cmbSalesOrder.SelectedValue)
            master.PlanId = IIf(Me.cmbPlan.SelectedValue = Nothing, 0, Me.cmbPlan.SelectedValue)
            master.TicketId = IIf(Me.cmbTicket.SelectedValue = Nothing, 0, Me.cmbTicket.SelectedValue)
            master.EstimationId = IIf(Me.cmbEstimation.SelectedValue = Nothing, 0, Me.cmbEstimation.SelectedValue)
            'If Not Me.cmbTicket.ActiveRow Is Nothing Then
            '    master.PlanItemId = Val(cmbTicket.SelectedRow.Cells("ArticleId").Value.ToString)
            'End If
            master.DecompositionDate = dtpDate.Value
            master.Remarks = Me.txtSpecialInstructions.Text
            'master.PlanTicketId = IIf(Me.cmbTicket.SelectedRow Is Nothing, 0, Me.cmbTicket.Value)
            master.DecompositionId = ID
            dt = New DataTable
            dt = CType(grd.DataSource, DataTable).Clone()
            dt.AcceptChanges()
            'dt.Rows.Clear()
            master.StockMaster = New StockMaster
            master.StockMaster.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            master.StockMaster.DocNo = Me.txtDocNo.Text.ToString.Replace("'", "''")
            master.StockMaster.DocDate = Me.dtpDate.Value.Date
            master.StockMaster.DocType = 8 'Aashir:Show Material Decomposition DocType on Item Ledger 'Convert.ToInt32(GetStockDocTypeId("MD").ToString)
            master.StockMaster.Remaks = Me.txtSpecialInstructions.Text.ToString.Replace("'", "''")
            master.StockMaster.AccountId = 0
            master.StockMaster.StockDetailList = New List(Of StockDetail)
            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                If Val(grdRow.Cells("TotalConsumedQty").Value.ToString) > 0 Then
                    ''''
                    'If Not grdRow.Parent Is Nothing Then
                    '    SetParentRecord(grdRow.Parent)
                    'End If
                    Dim dr2 As DataRow
                    dr2 = dt.NewRow
                    dr2("DecompositionId") = grdRow.Cells("DecompositionId").Value
                    dr2("EstimationDetailId") = grdRow.Cells("EstimationDetailId").Value
                    dr2("PlanItemId") = grdRow.Cells("PlanItemId").Value
                    dr2("ProductId") = grdRow.Cells("ProductId").Value
                    dr2("ParentId") = grdRow.Cells("ParentId").Value
                    dr2("Price") = grdRow.Cells("Price").Value
                    dr2("DecomposedQty") = grdRow.Cells("DecomposedQty").Value
                    dr2("WastedQty") = grdRow.Cells("WastedQty").Value
                    dr2("ScrappedQty") = grdRow.Cells("ScrappedQty").Value
                    dr2("Tag") = grdRow.Cells("Tag").Value
                    dr2("ParentTag") = grdRow.Cells("ParentTag").Value
                    dr2("DepartmentId") = grdRow.Cells("DepartmentId").Value
                    dr2("LocationId") = grdRow.Cells("LocationId").Value
                    dr2("UniqueId") = grdRow.Cells("UniqueId").Value
                    dr2("UniqueParentId") = grdRow.Cells("UniqueParentId").Value
                    dr2("TotalConsumedQty") = Val(grdRow.Cells("TotalConsumedQty").Value.ToString)
                    dr2("StockImpact") = Val(grdRow.Cells("StockImpact").Value.ToString)
                    dr2("SubSubId") = Val(grdRow.Cells("SubSubId").Value.ToString)
                    dr2("PlanItemSubSubId") = Val(grdRow.Cells("PlanItemSubSubId").Value.ToString)
                    dr2("DValue") = Val(grdRow.Cells("DValue").Value.ToString)
                    dr2("WValue") = Val(grdRow.Cells("WValue").Value.ToString)
                    dr2("SValue") = Val(grdRow.Cells("SValue").Value.ToString)
                    dr2("TicketId") = Val(grdRow.Cells("TicketId").Value.ToString)
                    dt.Rows.Add(dr2)
                    'If grdRow.Children > 0 Then
                    '    GetChildRows(grdRow)
                    'End If
                    '''''
                    'If Val(grdRow.Cells("DecomposedQty").Value.ToString) > 0 AndAlso Val(grdRow.Cells(GridColumns.STOCK_IMPACT).Value.ToString) > 0 Then
                    If Val(grdRow.Cells("DecomposedQty").Value.ToString) > 0 AndAlso Val(grdRow.Cells(GridColumns.STOCK_IMPACT).Value.ToString) > 0 Then

                        StockDetail = New StockDetail
                        StockDetail.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtDocNo.Text).ToString)
                        StockDetail.LocationId = grdRow.Cells("LocationId").Value
                        StockDetail.ArticleDefId = grdRow.Cells("ProductId").Value
                        'If Val(grdRow.Cells("DecomposedQty").Value.ToString) > Val(grdRow.Cells("TempDecQty").Value.ToString) AndAlso IsEditMode = False Then
                        '    StockDetail.InQty = (Val(grdRow.Cells("DecomposedQty").Value.ToString) - Val(grdRow.Cells("TempDecQty").Value.ToString))
                        '    StockDetail.In_PackQty = (Val(grdRow.Cells("DecomposedQty").Value.ToString) - Val(grdRow.Cells("TempDecQty").Value.ToString))
                        'Else
                        '    StockDetail.InQty = (Val(grdRow.Cells("DecomposedQty").Value.ToString))
                        '    StockDetail.In_PackQty = Val(grdRow.Cells("DecomposedQty").Value.ToString)
                        'End If
                        StockDetail.InQty = Val(grdRow.Cells("DecomposedQty").Value.ToString)
                        StockDetail.In_PackQty = Val(grdRow.Cells("DecomposedQty").Value.ToString)
                        StockDetail.OutQty = 0
                        StockDetail.Rate = Val(grdRow.Cells("Price").Value.ToString)
                        StockDetail.InAmount = (StockDetail.InQty * Val(grdRow.Cells("Price").Value.ToString)) ''TASK-408
                        StockDetail.OutAmount = 0
                        StockDetail.Remarks = String.Empty
                        StockDetail.PackQty = 0

                        ''TASK TFS1931
                        StockDetail.Remarks = grdRow.Cells("Tag").Value.ToString
                        ''END TASK TFS1931
                        'StockDetail.In_PackQty = Val(grdRow.Cells("DecomposedQty").Value.ToString)
                        StockDetail.Out_PackQty = 0
                        StockDetail.BatchNo = String.Empty
                        master.StockMaster.StockDetailList.Add(StockDetail)
                    End If
                    If Val(grdRow.Cells("ScrappedQty").Value.ToString) > 0 AndAlso Val(grdRow.Cells(GridColumns.STOCK_IMPACT).Value.ToString) > 0 Then
                        'Dim dr2 As DataRow
                        'dr2 = dt.NewRow
                        'dr2("DecompositionId") = grdRow.Cells("DecompositionId").Value
                        'dr2("EstimationDetailId") = grdRow.Cells("EstimationDetailId").Value
                        'dr2("PlanItemId") = grdRow.Cells("PlanItemId").Value
                        'dr2("ProductId") = grdRow.Cells("ProductId").Value
                        'dr2("ParentId") = grdRow.Cells("ParentId").Value
                        'dr2("Price") = grdRow.Cells("Price").Value
                        'dr2("DecomposedQty") = grdRow.Cells("DecomposedQty").Value
                        'dr2("WastedQty") = grdRow.Cells("WastedQty").Value
                        'dr2("ScrappedQty") = grdRow.Cells("ScrappedQty").Value
                        'dr2("Tag") = grdRow.Cells("Tag").Value
                        'dr2("ParentTag") = grdRow.Cells("ParentTag").Value
                        'dr2("DepartmentId") = grdRow.Cells("DepartmentId").Value
                        'dr2("LocationId") = grdRow.Cells("LocationId").Value
                        'dr2("UniqueId") = grdRow.Cells("UniqueId").Value
                        'dr2("ParentUniqueId") = grdRow.Cells("ParentUniqueId").Value
                        'dr2("TotalConsumedQty") = (grdRow.Cells("DecomposedQty").Value + grdRow.Cells("WastedQty").Value + grdRow.Cells("ScrappedQty").Value)
                        'dt.Rows.Add(dr2)
                        'If grdRow.Children > 0 Then
                        '    GetChildRows(grdRow)
                        'End If

                        StockDetail = New StockDetail
                        StockDetail.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtDocNo.Text).ToString)
                        Dim DamageLocationId As Integer = bal.GetDamageLocation()
                        If DamageLocationId > 0 Then
                            StockDetail.LocationId = DamageLocationId
                        Else
                            StockDetail.LocationId = grdRow.Cells("LocationId").Value
                        End If
                        StockDetail.ArticleDefId = grdRow.Cells("ProductId").Value
                        'StockDetail.InQty = Val(grdRow.Cells("ScrappedQty").Value.ToString)
                        'If Val(grdRow.Cells("ScrappedQty").Value.ToString) > Val(grdRow.Cells("TempScrQty").Value.ToString) AndAlso IsEditMode = False Then
                        '    StockDetail.InQty = (Val(grdRow.Cells("ScrappedQty").Value.ToString) - Val(grdRow.Cells("TempScrQty").Value.ToString))
                        '    StockDetail.In_PackQty = (Val(grdRow.Cells("ScrappedQty").Value.ToString) - Val(grdRow.Cells("TempScrQty").Value.ToString))
                        'Else
                        '    StockDetail.InQty = (Val(grdRow.Cells("ScrappedQty").Value.ToString))
                        '    StockDetail.In_PackQty = Val(grdRow.Cells("ScrappedQty").Value.ToString)
                        'End If

                        StockDetail.InQty = Val(grdRow.Cells("ScrappedQty").Value.ToString)
                        StockDetail.In_PackQty = Val(grdRow.Cells("ScrappedQty").Value.ToString)
                        StockDetail.OutQty = 0
                        StockDetail.Rate = Val(grdRow.Cells("Price").Value.ToString)
                        StockDetail.InAmount = (StockDetail.InQty * Val(grdRow.Cells("Price").Value.ToString)) ''TASK-408
                        StockDetail.OutAmount = 0
                        StockDetail.Remarks = String.Empty
                        StockDetail.PackQty = 0
                        'StockDetail.In_PackQty = Val(grdRow.Cells("ScrappedQty").Value.ToString)
                        StockDetail.Out_PackQty = 0
                        ''TASK TFS1931
                        StockDetail.Remarks = grdRow.Cells("Tag").Value.ToString
                        ''END TASK TFS1931
                        StockDetail.BatchNo = String.Empty
                        master.StockMaster.StockDetailList.Add(StockDetail)

                    End If
                End If
                'GetConsumed(grdRow)
                'AreAllChildrenDecomposed(grdRow)
            Next
            'ChildCount = 0
            'Count = 0
            'ConsumedCount = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = bal.GetMaster()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("DecompositionId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("SalesOrderId").Visible = False
            Me.grdSaved.RootTable.Columns("PlanId").Visible = False
            Me.grdSaved.RootTable.Columns("TicketId").Visible = False
            Me.grdSaved.RootTable.Columns("EstimationId").Visible = False
            'Me.grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
            'Me.grdSaved.RootTable.Columns("DocNo").Caption = "Document No"
            'Me.grdSaved.RootTable.Columns("PlanItem").Caption = "Plan Item"
            Me.grdSaved.RootTable.Columns("DecompositionDate").Width = 150
            Me.grdSaved.RootTable.Columns("DecompositionDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            Dim DataExists As Boolean = False
            If txtDocNo.Text = "" Then
                msg_Error("Please enter PO No.")
                txtDocNo.Focus() : IsValidate = False : Exit Function
            End If
            'If cmbTicket.ActiveRow.Cells(0).Value <= 0 Then
            '    msg_Error("Please select Product")
            '    cmbTicket.Focus() : IsValidate = False : Exit Function
            'End If
            Me.grd.UpdateData()
            If Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbTicket.Focus() : IsValidate = False : Exit Function
            End If
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grd.Focus()
            End If
            For Each Row As Janus.Windows.GridEX.GridEXRow In grd.GetDataRows
                If Row.Cells("DecomposedQty").Value > 0 Or Row.Cells("WastedQty").Value > 0 Or Row.Cells("ScrappedQty").Value > 0 Then
                    DataExists = True
                End If
            Next
            If DataExists = False Then
                ShowErrorMessage("Please enter decomposed quantity or wasted quantity or scrapped quantity.")
                Me.grd.Focus() : IsValidate = False : Exit Function
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetSecurityRights()
            Me.txtDocNo.Text = Me.CreateEstimationNo()
            Me.txtSpecialInstructions.Text = String.Empty
            Me.dtpDate.Value = Now
            If Not Me.cmbCustomer.ActiveRow Is Nothing Then
                Me.cmbCustomer.Rows(0).Activate()
            End If

            If Not Me.cmbSalesOrder.SelectedValue Is Nothing Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPlan.SelectedValue Is Nothing Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not Me.cmbTicket.SelectedValue Is Nothing Then
                Me.cmbTicket.SelectedIndex = 0
            End If

            If Not Me.cmbEstimation.SelectedValue Is Nothing Then
                Me.cmbEstimation.SelectedIndex = 0
            End If
            'If Not Me.cmbTicket.SelectedRow Is Nothing Then
            '    Me.cmbTicket.Rows(0).Activate()
            'End If
            Me.btnSave.Text = "&Save"
            IsEditMode = False
            Me.cmbCustomer.Enabled = True
            Me.cmbSalesOrder.Enabled = True
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
            Me.cmbEstimation.Enabled = True
            'If Not Me.cmbTicket.SelectedValue Is Nothing Then
            '    Me.cmbTicket.SelectedIndex = 0
            'End If
            DisplayDetail(-1)
            GetAllRecords()
            'If Not getConfigValueByType("AssociateItems").ToString = "Error" Then
            '    AssociateItems = getConfigValueByType("AssociateItems")
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            bal.Save_Transation(dt, master, Me.Name, MyCompanyId, WastedStockAccount, ScrappedStockAccount)
            SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
            msg_Information("Record has been saved successfully.")
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

    End Function

    Private Sub frmMaterialEstimation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("Customer")
            FillCombos("Plan")
            FillCombos("Location")
            GetAllRecords()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function CreateEstimationNo() As String
        Dim MENo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                MENo = GetSerialNo("MD" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "MaterialDecompositionMaster", "DocumentNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("MD" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "MaterialDecompositionMaster", "DocumentNo")
            Else
                MENo = GetNextDocNo("MD", 6, "MaterialDecompositionMaster", "DocumentNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbPlan_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedValueChanged
        Try
            If Not cmbPlan.SelectedIndex = -1 Then
                FillCombos("TicketNo")

                'Task 3511 Saad Afzaal Call ProductionProgress show hierarchy view of production progress of selected plan 
                If cmbPlan.SelectedValue > 0 Then
                    ShowProductionProgress()
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbSalesOrder_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedValueChanged
        Try
            If Not Me.cmbSalesOrder.SelectedIndex = -1 Then
                FillCombos("PlanNo")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function Update_Record() As Boolean
        Try
            bal.Update_Transation(dt, master, Me.Name, MyCompanyId, WastedStockAccount, ScrappedStockAccount, VoucherId)
            SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
            msg_Information("Record has been updated successfully.")
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DisplayDetail(ByVal MasterID As Integer)
        Try
            Dim dt2 As DataTable = bal.DisplayDetail(MasterID, DecimalPointInQty)
            'dt2.Columns("DecomposableQty").Expression = " ((IsNull(DecomposedQty, 0)+IsNull(WastedQty, 0)+IsNull(ScrappedQty, 0)) = IsNull(DecomposableQty, 0))"
            'dt2.Columns("WastedQty").Expression = " ((IsNull(DecomposedQty, 0)+IsNull(WastedQty, 0)+IsNull(ScrappedQty, 0)) = IsNull(DecomposableQty, 0))"
            'dt2.Columns("ScrappedQty").Expression = " ((IsNull(DecomposedQty, 0)+IsNull(WastedQty, 0)+IsNull(ScrappedQty, 0)) = IsNull(DecomposableQty, 0))"
            'dt2.Columns("DecomposableQty").Expression = "(Qty)-(DecomposedQty + WastedQty + ScrappedQty)"
            dt2.AcceptChanges()
            Me.grd.DataSource = dt2
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            Me.grd.RootTable.Columns("DecomposableQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Product").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            'Me.grd.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grd.RootTable.Columns("TotalConsumedQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("StockImpact").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposableQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("WastedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ScrappedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposableQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("WastedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ScrappedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposableQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("WastedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ScrappedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("TotalConsumedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("WastedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ScrappedQty").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("DValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("WValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SValue").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("LocationId").HasValueList = True
            Me.grd.RootTable.Columns("LocationId").LimitToList = True
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ExpandColumn = Me.grdMaterialEstimation.RootTable.Columns("Article code")
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentDataMember = "ArticleID"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            FillCombos("grdLocations")
            'Me.grdMaterialEstimation.GroupByBoxVisible = True
            'planItemGroup.Collapse()
            'FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GetEstimation()
        Try
            'dt.Columns("DecomposableQty").Expression = "(Qty)-(DecomposedQty + WastedQty + ScrappedQty)"
            Me.grd.DataSource = dtGlobe
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Product").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            'Me.grd.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grd.RootTable.Columns("TotalConsumedQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("StockImpact").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposableQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("TotalConsumedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("DecomposedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("WastedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ScrappedQty").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("DValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("WValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue


            Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("LocationId").HasValueList = True
            Me.grd.RootTable.Columns("LocationId").LimitToList = True
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ExpandColumn = Me.grdMaterialEstimation.RootTable.Columns("Article code")
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentDataMember = "ArticleID"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            FillCombos("grdLocations")
            'Me.grdMaterialEstimation.GroupByBoxVisible = True
            'planItemGroup.Collapse()
            'FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            VoucherId = GetVoucherId(Me.Name, Me.grdSaved.CurrentRow.Cells("DocumentNo").Value.ToString)
            bal.DeleteMaster(Val(Me.grdSaved.GetRow.Cells("DecompositionId").Value.ToString), Me.grdSaved.GetRow.Cells("DocumentNo").Value.ToString, VoucherId)
            SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
            ' bal.
            msg_Information("Record has been deleted successfully.")
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ''grd.UpdateData()
            ''Prepare the grid
            'MarkAllTopParents()
            'ConsumeChildrenIfParentsAreConsumed()
            'ConsumeParentsIfChildrenAreConsumed()
            ''The following function should be called after MarkAllTopParents(), ConsumeChildrenIfParentsAreConsumed(), and ConsumeParentsIfChildrenAreConsumed()
            ''to taken benefit of the settings they have made in the grid 
            'MarkStockImpact()
            'GetChildrenValueIfParentsAreConsumed()
            'SetChildrenVoucherValueIfParentsNotAreConsumed()
            'grd.UpdateData()
            'Exit Sub
            ''TASK TFS1927 Muhammad Ameen on 18-12-2017. Configuration based account effect for wasted item or scrapped item. 
            If Not getConfigValueByType("WastedStockAccount").ToString = "Error" Then
                WastedStockAccount = CInt(getConfigValueByType("WastedStockAccount"))
            End If
            If Not getConfigValueByType("ScrappedStockAccount").ToString = "Error" Then
                ScrappedStockAccount = CInt(getConfigValueByType("ScrappedStockAccount"))
            End If
            ''END TASK TFS1927

            If Me.IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Save() = True Then
                        ReSetControls()
                        GetAllRecords()
                    End If
                Else
                    'If IsValidToDelete("ItemConsumptionDetail", "EstimationId", Me.grdSaved.CurrentRow.Cells("Id").Value.ToString) = True Then
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update_Record() = True Then
                        ReSetControls()
                        GetAllRecords()
                    End If
                    'Else
                    'msg_Error(str_ErrorDependentRecordFound)
                    'End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbCustomer.Value
            FillCombos("Customer")
            Me.cmbCustomer.Value = Id

            Id = Me.cmbSalesOrder.SelectedValue
            FillCombos("SO")
            Me.cmbSalesOrder.SelectedValue = Id

            Id = Me.cmbPlan.SelectedValue
            FillCombos("Plan")
            Me.cmbPlan.SelectedValue = Id

            Id = Me.cmbTicket.SelectedValue
            FillCombos("Ticket")
            Me.cmbTicket.SelectedValue = Id

            Id = Me.cmbEstimation.SelectedValue
            FillCombos("Estimation")
            Me.cmbEstimation.SelectedValue = Id
            'FillCombos("SO")
            'FillCombos("Plan")
            'GetAllRecords()
            'ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells("DocumentNo").Value.ToString
            VoucherId = GetVoucherId(Me.Name, Me.grdSaved.CurrentRow.Cells("DocumentNo").Value.ToString)
            Me.txtSpecialInstructions.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.GetRow.Cells("DecompositionDate").Value

            If Not IsDBNull(Me.grdSaved.GetRow.Cells("CustomerId").Value) Then
                Me.cmbSalesOrder.SelectedValue = Me.grdSaved.GetRow.Cells("CustomerId").Value
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("SalesOrderId").Value) Then
                Me.cmbSalesOrder.SelectedValue = Me.grdSaved.GetRow.Cells("SalesOrderId").Value
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("PlanId").Value) Then
                Me.cmbPlan.SelectedValue = Me.grdSaved.GetRow.Cells("PlanId").Value
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("TicketId").Value) Then
                Me.cmbTicket.SelectedValue = Me.grdSaved.GetRow.Cells("TicketId").Value
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("EstimationId").Value) Then
                Me.cmbEstimation.SelectedValue = Me.grdSaved.GetRow.Cells("EstimationId").Value
            End If
            ID = Val(Me.grdSaved.GetRow.Cells("DecompositionId").Value.ToString)
            DisplayDetail(ID)
            Me.cmbCustomer.Enabled = False
            Me.cmbSalesOrder.Enabled = False
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
            Me.cmbEstimation.Enabled = False
            Me.UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmMaterialEstimation)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'PrintToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString  ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                If Rights Is Nothing Then Exit Sub
                For Each RightstDt As GroupRights In Rights
                    If RightstDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightstDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightstDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Report Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Report Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightstDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Function GetPlanItems(ByVal PlanId As Integer) As DataTable
        Dim PlanItems As String = ""
        Try
            PlanItems = "Select ArticleDefId, Qty, ArticleDefTable.ArticleDescription As Item From PlanDetailTable LEFT OUTER JOIN ArticleDefTable ON PlanDetailTable.ArticleDefId = ArticleDefTable.ArticleId Where PlanId =" & PlanId & ""
            dtCriteriaWiseCostSheet = GetDataTable(PlanItems)
            dtCriteriaWiseCostSheet.AcceptChanges()
            Return dtCriteriaWiseCostSheet

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetSalesOrderItems(ByVal SalesOrderId As Integer) As DataTable
        Dim SalesOrderItems As String = ""
        Try
            SalesOrderItems = "Select ArticleDefId, Qty From SalesOrderDetailTable Where SalesOrderId =" & SalesOrderId & ""
            dtCriteriaWiseCostSheet = GetDataTable(SalesOrderItems)
            dtCriteriaWiseCostSheet.AcceptChanges()
            Return dtCriteriaWiseCostSheet
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function GetTicketDetail(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim Str As String = String.Empty
        Try
            Str = "Select PlanTicketsDetail.PlanTicketsDetailID, PlanTicketsDetail.ArticleId, ArticleDefTable.ArticleDescription, PlanTicketsDetail.Quantity From PlanTicketsDetail LEFT JOIN ArticleDefTable ON PlanTicketsDetail.ArticleId = ArticleDefTable.ArticleId  Where PlanTicketsMasterID=" & ID & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub RefreshDetailControls()
        Try
            'Me.txtRemarks.Text = ""
            'Me.txtQty.Text = ""
            'Me.dtpMaterialAllocation.Value = Now
            'Me.cmbDepartment.Rows(0).Activate()
            'FillCombos("Product")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Material Decomposition " & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            If Not cmbCustomer.ActiveRow Is Nothing Then
                FillCombos("SO")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                FillCombos("Estimation")

                If cmbTicket.SelectedValue > 0 Then
                    ShowProductionProgress()
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEstimation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEstimation.SelectedIndexChanged
        If IsEditMode = True Then Exit Sub
        Dim dtPro As New DataTable
        Try
            If Not cmbEstimation.SelectedIndex = -1 Then
                If cmbEstimation.SelectedValue > 0 Then
                    dtPro = bal.GetPlansItems(Me.cmbTicket.SelectedValue)
                    If dtPro.Rows.Count > 0 Then
                        dtGlobe = New DataTable()
                        For Each row As DataRow In dtPro.Rows
                            Dim dt1 As DataTable = bal.GetParents(Me.cmbTicket.SelectedValue, Me.cmbEstimation.SelectedValue, row.Item("ArticleDefId"), row.Item("Qty"), Me.cmbCategory.SelectedValue, DecimalPointInQty)
                            dtGlobe.Merge(dt1)
                        Next
                        GetEstimation()
                    End If

                Else
                    DisplayDetail(-1)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Delete" Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    Me.grd.GetRow.Delete()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                btnEdit.Enabled = True
                btnEdit.Visible = True
                btnSave.Enabled = False
                btnSave.Visible = False
                btnRefresh.Enabled = False
                btnRefresh.Visible = False
                btnNew.Enabled = False
                btnNew.Visible = False
                CtrlGrdBar1.Visible = False
                CtrlGrdBar2.Visible = True
            ElseIf e.Tab.Index = 0 Then
                btnEdit.Enabled = False
                btnEdit.Visible = False
                btnSave.Enabled = True
                btnSave.Visible = True
                btnRefresh.Enabled = True
                btnRefresh.Visible = True
                btnNew.Enabled = True
                btnNew.Visible = True
                CtrlGrdBar1.Visible = True
                CtrlGrdBar2.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick

    End Sub

    'Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
    '    Try
    '        If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
    '            Dim ParentQty As Double = 0
    '            Dim ChildQty As Double = 0
    '            Dim TotalQty As Double = 0
    '            Me.grd.UpdateData()

    '            'TestMethod(grd.GetRow.Parent)
    '            'Exit Sub
    '            If e.Column.Key = "DecomposedQty" Or e.Column.Key = "WastedQty" Or e.Column.Key = "ScrappedQty" Then
    '                '' Prevention of minus quantity
    '                'Dim ConsumedQty As Double = (Val(Me.grd.GetRow.Cells("DecomposedQty").Value - Me.grd.GetRow.Cells("TempDecQty").Value) + Val(Me.grd.GetRow.Cells("WastedQty").Value - Me.grd.GetRow.Cells("TempWasQty").Value) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value - Me.grd.GetRow.Cells("TempScrQty").Value))
    '                If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) < 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) < 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) < 0 Then
    '                    msg_Information("Minus quantity is not allowed.")
    '                    Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                    Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                    Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                    Exit Sub
    '                End If
    '                ''
    '                '' Finish or semi finish item should not be consumed less than 1
    '                If Me.grd.GetRow.Children > 0 Then
    '                    If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Then
    '                        msg_Information("Finish or semi finish item could not be decomposed less than one.")
    '                        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                        'Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                        'Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                        Exit Sub
    '                    ElseIf Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
    '                        msg_Information("Finish or semi finish item could not be scrapped less than one.")
    '                        'Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                        'Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                        Exit Sub
    '                    ElseIf Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Then
    '                        msg_Information("Finish or semi finish item could not be wasted less than one.")
    '                        'Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                        'Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                        Exit Sub
    '                    End If

    '                    If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
    '                        GetDecomposedChildRows(grd.GetRow)
    '                        If IsAnyChildDecomposed = True Then
    '                            msg_Information("Finish or semi finish can't be decomposed if any child item is decomposed.")
    '                            'If DecomposedQty > 0 Or ScrappedQty > 0 Or WastedQty > 0 Then
    '                            Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                            Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                            Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                            'End If
    '                            IsAnyChildDecomposed = False
    '                        End If
    '                    End If
    '                End If
    '                ''
    '                ''Checking multi parent consumed quantity
    '                If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
    '                    GetDecomposedParentRows(grd.GetRow.Parent)
    '                    If IsParentDecomposed = True Then
    '                        msg_Information("A child item can not be decomposed in case its parent is consumed.")
    '                        'If DecomposedQty > 0 Or ScrappedQty > 0 Or WastedQty > 0 Then
    '                        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                        'End If
    '                        IsParentDecomposed = False
    '                    End If
    '                End If


    '                ''Quantity comparing section
    '                '' End checking multi parent quantity

    '                'Dim ConsumedQty As Double = (Val(Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempDecQty").Value.ToString)) + Val(Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempWasQty").Value.ToString)) + Val(Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempScrQty").Value.ToString)))
    '                'Dim CurrentTotalQty As Double = Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString)
    '                'Dim RecentTotalQty As Double = DecomposedQty + WastedQty + ScrappedQty
    '                'If CurrentTotalQty > RecentTotalQty Then
    '                '    Dim SumQty As Double = CurrentTotalQty - RecentTotalQty
    '                '    'If (Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString)) > Val(Me.grd.GetRow.Cells("Qty").Value.ToString) Then
    '                '    If SumQty > Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString) Then
    '                '        msg_Information("Total quantity has exceeded available quantity")
    '                '        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                '        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                '        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                '        Exit Sub
    '                '    End If
    '                'End If
    '                If IsEditMode = True Then
    '                    Dim CurrentTotalQty As Double = Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString)
    '                    Dim OldTotalQty As Double = Val(Me.grd.GetRow.Cells("TempDecQty").Value.ToString) + Val(Me.grd.GetRow.Cells("TempWasQty").Value.ToString) + Val(Me.grd.GetRow.Cells("TempScrQty").Value.ToString)
    '                    If CurrentTotalQty > OldTotalQty Then
    '                        Dim SumQty As Double = CurrentTotalQty - OldTotalQty
    '                        If SumQty > Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString) Then
    '                            msg_Information("Total quantity has exceeded available quantity")
    '                            Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                            Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                            Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                        End If
    '                    End If
    '                Else
    '                    If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString) Then
    '                        msg_Information("Total quantity has exceeded available quantity")
    '                        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
    '                        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                    End If
    '                End If
    '                '' End quantity comparing section

    '                ''Single parent consumed quantity checking
    '                'If e.Column.Key = "DecomposedQty" Then
    '                '    If Not Me.grd.GetRow.Parent Is Nothing Then
    '                '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("DecomposedQty").Value.ToString)
    '                '    End If
    '                '    If ParentQty > 0 Then
    '                '        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
    '                '        msg_Information("Its parent item has already been decomposed.")
    '                '        Exit Sub
    '                '    End If
    '                'End If
    '                'If e.Column.Key = "WastedQty" Then
    '                '    If Not Me.grd.GetRow.Parent Is Nothing Then
    '                '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("WastedQty").Value.ToString)
    '                '    End If
    '                '    If ParentQty > 0 Then
    '                '        Me.grd.GetRow.Cells("WastedQty").Value = DecomposedQty
    '                '        msg_Information("Its parent item has already been decomposed.")
    '                '    End If
    '                'End If
    '                'If e.Column.Key = "ScrappedQty" Then
    '                '    If Not Me.grd.GetRow.Parent Is Nothing Then
    '                '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("ScrappedQty").Value.ToString)
    '                '    End If
    '                '    If ParentQty > 0 ThenE:\Working Folder\Version 4\SimpleAccounts\New\Configuration\frmRptArticleBarcode.vb
    '                '        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
    '                '        msg_Information("Its parent item has already been decomposed.")
    '                '    End If
    '                'End If

    '                ''End Single parent consumed quantity checking
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        Try
            If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "DecomposedQty" Or e.Column.Key = "WastedQty" Or e.Column.Key = "ScrappedQty" Then
                    DecomposedQty = Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString)
                    WastedQty = Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString)
                    ScrappedQty = Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSpecialInstructions_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSpecialInstructions.KeyPress


    End Sub

    Private Sub SetParentDecoposed(ByVal Parent As Janus.Windows.GridEX.GridEXRow)
        Try
            If Not Parent.Parent Is Nothing Then
                Parent.Cells("DecomposedQty").Value = 1
                SetParentDecoposed(Parent)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetChildRows(ByVal grdRow As Janus.Windows.GridEX.GridEXRow)
        Try
            Dim TotalConsumedQty As Double = 0
            Dim TotalConsumableQty As Double = 0
            Dim childRows() As Janus.Windows.GridEX.GridEXRow = grdRow.GetChildRows
            For Each Row As Janus.Windows.GridEX.GridEXRow In childRows
                Dim TotalConsumedQtyChild As Double = 0
                Dim TotalConsumableQtyChild As Double = 0
                Dim dr1 As DataRow
                dr1 = dt.NewRow
                dr1("DecompositionId") = Row.Cells("DecompositionId").Value
                dr1("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                dr1("PlanItemId") = Row.Cells("PlanItemId").Value
                dr1("ProductId") = Row.Cells("ProductId").Value
                dr1("ParentId") = Row.Cells("ParentId").Value
                dr1("Price") = Row.Cells("Price").Value
                dr1("WastedQty") = Row.Cells("WastedQty").Value
                dr1("ScrappedQty") = Row.Cells("ScrappedQty").Value
                dr1("Tag") = Row.Cells("Tag").Value
                dr1("ParentTag") = Row.Cells("ParentTag").Value
                dr1("DepartmentId") = Row.Cells("DepartmentId").Value
                dr1("LocationId") = Row.Cells("LocationId").Value
                dr1("UniqueId") = Row.Cells("UniqueId").Value
                dr1("ParentUniqueId") = Row.Cells("ParentUniqueId").Value
                dr1("TotalConsumedQty") = (Row.Cells("DecomposedQty").Value + Row.Cells("WastedQty").Value + Row.Cells("ScrappedQty").Value)
                dt.Rows.Add(dr1)
                If Row.Children > 0 Then
                    GetChildRows(Row)
                End If
                'SetParentRecord(Row.Parent)

                'If TotalConsumableQtyChild = TotalConsumedQtyChild AndAlso Row.Children > 0 AndAlso Row.Cells("DecomposedQty").Value < 1 AndAlso Row.Cells("WastedQty").Value < 1 AndAlso Row.Cells("ScrappedQty").Value < 1 Then
                '    Dim dr2 As DataRow
                '    dr2 = dt.NewRow
                '    dr2("DecompositionId") = Row.Cells("DecompositionId").Value
                '    dr2("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                '    dr2("PlanItemId") = Row.Cells("PlanItemId").Value
                '    dr2("ProductId") = Row.Cells("ProductId").Value
                '    dr2("ParentId") = Row.Cells("ParentId").Value
                '    dr2("Price") = Row.Cells("Price").Value
                '    dr2("WastedQty") = Row.Cells("WastedQty").Value
                '    dr2("ScrappedQty") = Row.Cells("ScrappedQty").Value
                '    dr2("Tag") = Row.Cells("Tag").Value
                '    dr2("ParentTag") = Row.Cells("ParentTag").Value
                '    dr2("DepartmentId") = Row.Cells("DepartmentId").Value
                '    dr2("LocationId") = Row.Cells("LocationId").Value
                '    dr2("UniqueId") = Row.Cells("UniqueId").Value
                '    dr2("ParentUniqueId") = Row.Cells("ParentUniqueId").Value
                '    dr2("TotalConsumedQty") = 1
                '    dt.Rows.Add(dr2)
                '    TotalConsumedQty = 0
                '    TotalConsumableQty = 0
                'End If
            Next
            'If TotalConsumableQty = TotalConsumedQty AndAlso childRows.Length > 0 AndAlso grdRow.Cells("DecomposedQty").Value < 1 AndAlso grdRow.Cells("WastedQty").Value < 1 AndAlso grdRow.Cells("ScrappedQty").Value < 1 Then
            '    Dim dr1 As DataRow
            '    dr1 = dt.NewRow
            '    dr1("DecompositionId") = grdRow.Cells("DecompositionId").Value
            '    dr1("EstimationDetailId") = grdRow.Cells("EstimationDetailId").Value
            '    dr1("PlanItemId") = grdRow.Cells("PlanItemId").Value
            '    dr1("ProductId") = grdRow.Cells("ProductId").Value
            '    dr1("ParentId") = grdRow.Cells("ParentId").Value
            '    dr1("Price") = grdRow.Cells("Price").Value
            '    dr1("WastedQty") = grdRow.Cells("WastedQty").Value
            '    dr1("ScrappedQty") = grdRow.Cells("ScrappedQty").Value
            '    dr1("Tag") = grdRow.Cells("Tag").Value
            '    dr1("ParentTag") = grdRow.Cells("ParentTag").Value
            '    dr1("DepartmentId") = grdRow.Cells("DepartmentId").Value
            '    dr1("LocationId") = grdRow.Cells("LocationId").Value
            '    dr1("UniqueId") = grdRow.Cells("UniqueId").Value
            '    dr1("ParentUniqueId") = grdRow.Cells("ParentUniqueId").Value
            '    dr1("TotalConsumedQty") = 1
            '    dt.Rows.Add(dr1)
            '    TotalConsumedQty = 0
            '    TotalConsumableQty = 0
            'End If
            ''''
            'SetParentRecord(grdRow.Parent)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SetParentRecord(ByVal parentRow As Janus.Windows.GridEX.GridEXRow)
        Dim TotalConsumedQty As Double = 0
        Dim TotalConsumableQty As Double = 0
        Try
            Dim childRows() As Janus.Windows.GridEX.GridEXRow = parentRow.GetChildRows
            'Dim childRows() As Janus.Windows.GridEX.GridEXRow = grdRow.P
            For Each Row As Janus.Windows.GridEX.GridEXRow In childRows
                Dim TotalConsumedQtyChild As Double = 0
                Dim TotalConsumableQtyChild As Double = 0
                'Dim dr1 As DataRow
                'dr1 = dt.NewRow
                'dr1("DecompositionId") = Row.Cells("DecompositionId").Value
                'dr1("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                'dr1("PlanItemId") = Row.Cells("PlanItemId").Value
                'dr1("ProductId") = Row.Cells("ProductId").Value
                'dr1("ParentId") = Row.Cells("ParentId").Value
                'dr1("Price") = Row.Cells("Price").Value
                'dr1("DecomposedQty") = Row.Cells("DecomposedQty").Value
                'dr1("WastedQty") = Row.Cells("WastedQty").Value
                'dr1("ScrappedQty") = Row.Cells("ScrappedQty").Value
                'dr1("Tag") = Row.Cells("Tag").Value
                'dr1("ParentTag") = Row.Cells("ParentTag").Value
                'dr1("DepartmentId") = Row.Cells("DepartmentId").Value
                'dr1("LocationId") = Row.Cells("LocationId").Value
                'dr1("UniqueId") = Row.Cells("UniqueId").Value
                'dr1("ParentUniqueId") = Row.Cells("ParentUniqueId").Value

                ''For parent
                TotalConsumedQty += (Row.Cells("DecomposedQty").Value + Row.Cells("WastedQty").Value + Row.Cells("ScrappedQty").Value)
                TotalConsumableQty += Row.Cells("DecomposableQty").Value
                ''
                ''For child
                TotalConsumedQtyChild += (Row.Cells("DecomposedQty").Value + Row.Cells("WastedQty").Value + Row.Cells("ScrappedQty").Value)
                TotalConsumableQtyChild += Row.Cells("DecomposableQty").Value
                ''
                'dt.Rows.Add(dr1)
                If Not Row.Parent Is Nothing Then
                    SetParentRecord(Row.Parent)
                End If
                If TotalConsumableQtyChild = TotalConsumedQtyChild AndAlso Row.Children > 0 AndAlso Row.Cells("DecomposedQty").Value < 1 AndAlso Row.Cells("WastedQty").Value < 1 AndAlso Row.Cells("ScrappedQty").Value < 1 Then
                    Dim dr2 As DataRow
                    dr2 = dt.NewRow
                    dr2("DecompositionId") = Row.Cells("DecompositionId").Value
                    dr2("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                    dr2("PlanItemId") = Row.Cells("PlanItemId").Value
                    dr2("ProductId") = Row.Cells("ProductId").Value
                    dr2("ParentId") = Row.Cells("ParentId").Value
                    dr2("Price") = Row.Cells("Price").Value
                    dr2("WastedQty") = Row.Cells("WastedQty").Value
                    dr2("ScrappedQty") = Row.Cells("ScrappedQty").Value
                    dr2("Tag") = Row.Cells("Tag").Value
                    dr2("ParentTag") = Row.Cells("ParentTag").Value
                    dr2("DepartmentId") = Row.Cells("DepartmentId").Value
                    dr2("LocationId") = Row.Cells("LocationId").Value
                    dr2("UniqueId") = Row.Cells("UniqueId").Value
                    dr2("ParentUniqueId") = Row.Cells("ParentUniqueId").Value
                    dr2("TotalConsumedQty") = 1
                    dt.Rows.Add(dr2)
                    TotalConsumedQtyChild = 0
                    TotalConsumableQtyChild = 0
                End If
            Next
            ''''
            If TotalConsumableQty = TotalConsumedQty AndAlso childRows.Length > 0 AndAlso parentRow.Cells("DecomposedQty").Value < 1 AndAlso parentRow.Cells("WastedQty").Value < 1 AndAlso parentRow.Cells("ScrappedQty").Value < 1 Then
                Dim dr1 As DataRow
                dr1 = dt.NewRow
                dr1("DecompositionId") = parentRow.Cells("DecompositionId").Value
                dr1("EstimationDetailId") = parentRow.Cells("EstimationDetailId").Value
                dr1("PlanItemId") = parentRow.Cells("PlanItemId").Value
                dr1("ProductId") = parentRow.Cells("ProductId").Value
                dr1("ParentId") = parentRow.Cells("ParentId").Value
                dr1("Price") = parentRow.Cells("Price").Value
                dr1("WastedQty") = parentRow.Cells("WastedQty").Value
                dr1("ScrappedQty") = parentRow.Cells("ScrappedQty").Value
                dr1("Tag") = parentRow.Cells("Tag").Value
                dr1("ParentTag") = parentRow.Cells("ParentTag").Value
                dr1("DepartmentId") = parentRow.Cells("DepartmentId").Value
                dr1("LocationId") = parentRow.Cells("LocationId").Value
                dr1("UniqueId") = parentRow.Cells("UniqueId").Value
                dr1("ParentUniqueId") = parentRow.Cells("ParentUniqueId").Value
                dr1("TotalConsumedQty") = 1
                dt.Rows.Add(dr1)
                If Not parentRow.Parent Is Nothing Then
                    SetParentRecord(parentRow.Parent)
                End If
                TotalConsumedQty = 0
                TotalConsumableQty = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetDecomposedChildRows(ByVal grdRow As Janus.Windows.GridEX.GridEXRow)
        Try
            'If Val(grdRow.Cells("ConsumedChildCount").Value.ToString) > 0 Then
            '    IsAnyChildDecomposed = True
            '    Exit Sub
            'End If
            Dim childRows() As Janus.Windows.GridEX.GridEXRow = grdRow.GetChildRows
            For Each Row As Janus.Windows.GridEX.GridEXRow In childRows

                If Val(Row.Cells("DecomposedQty").Value.ToString) > 0 Or Val(Row.Cells("WastedQty").Value.ToString) > 0 Or Val(Row.Cells("ScrappedQty").Value.ToString) > 0 Or Val(Row.Cells("Qty").Value.ToString) > Val(Row.Cells("DecomposableQty").Value.ToString) Then
                    IsAnyChildDecomposed = True
                    Exit Sub
                End If

                'Dim dr1 As DataRow
                'dr1 = dt.NewRow
                'dr1("DecompositionId") = Row.Cells("DecompositionId").Value
                'dr1("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                'dr1("PlanItemId") = Row.Cells("PlanItemId").Value
                'dr1("ProductId") = Row.Cells("ProductId").Value
                'dr1("ParentId") = Row.Cells("ParentId").Value
                'dr1("Price") = Row.Cells("Price").Value
                'dr1("DecomposedQty") = Row.Cells("DecomposedQty").Value
                'dr1("WastedQty") = Row.Cells("WastedQty").Value
                'dr1("ScrappedQty") = Row.Cells("ScrappedQty").Value
                'dr1("Tag") = Row.Cells("Tag").Value
                'dr1("ParentTag") = Row.Cells("ParentTag").Value
                'dr1("DepartmentId") = Row.Cells("DepartmentId").Value
                'dr1("LocationId") = Row.Cells("LocationId").Value
                'dr1("UniqueId") = Row.Cells("UniqueId").Value
                'dr1("ParentUniqueId") = Row.Cells("ParentUniqueId").Value
                'dt.Rows.Add(dr1)
                If Row.Children > 0 Then
                    GetDecomposedChildRows(Row)
                End If
            Next
            ''''
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetDecomposedParentRows(ByVal grdRow As Janus.Windows.GridEX.GridEXRow)
        Try
            If Not grdRow Is Nothing Then
                'Dim Parent As Janus.Windows.GridEX.GridEXRow = grdRow.Parent
                'For Each Row As Janus.Windows.GridEX.GridEXRow In childRows

                If grdRow.Cells("DecomposedQty").Value > 0 Or grdRow.Cells("WastedQty").Value > 0 Or grdRow.Cells("ScrappedQty").Value > 0 Then
                    IsParentDecomposed = True
                    Exit Sub
                End If

                'Dim dr1 As DataRow
                'dr1 = dt.NewRow
                'dr1("DecompositionId") = Row.Cells("DecompositionId").Value
                'dr1("EstimationDetailId") = Row.Cells("EstimationDetailId").Value
                'dr1("PlanItemId") = Row.Cells("PlanItemId").Value
                'dr1("ProductId") = Row.Cells("ProductId").Value
                'dr1("ParentId") = Row.Cells("ParentId").Value
                'dr1("Price") = Row.Cells("Price").Value
                'dr1("DecomposedQty") = Row.Cells("DecomposedQty").Value
                'dr1("WastedQty") = Row.Cells("WastedQty").Value
                'dr1("ScrappedQty") = Row.Cells("ScrappedQty").Value
                'dr1("Tag") = Row.Cells("Tag").Value
                'dr1("ParentTag") = Row.Cells("ParentTag").Value
                'dr1("DepartmentId") = Row.Cells("DepartmentId").Value
                'dr1("LocationId") = Row.Cells("LocationId").Value
                'dr1("UniqueId") = Row.Cells("UniqueId").Value
                'dr1("ParentUniqueId") = Row.Cells("ParentUniqueId").Value
                'dt.Rows.Add(dr1)
                If Not grdRow.Parent Is Nothing Then
                    GetDecomposedParentRows(grdRow.Parent)
                End If
                'Next
                ''''
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UltraTabPageControl1_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & " Material Decomposition " & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub MarkConsumed()
        Dim RowCount As Integer = grd.RowCount
        Dim LoopCounter As Integer = RowCount


        MsgBox("Rows = " & RowCount)

        For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetRows
            If IsConsumed(Node) = True Then

            End If

        Next



        'End While


    End Sub




    Private Function IsConsumed(ByVal Node As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim ChildCount As Integer = 0
        Dim Count As Integer = 0
        Dim ConsumedCount As Integer = 0
        Try
            'If Node.Cells("TotalConsumedQty").Value < 1 Then
            ChildCount = Node.Children
            Count = Node.Children
            'While Count > 0
            If Count > 0 Then
                For Each ChildRow As Janus.Windows.GridEX.GridEXRow In Node.GetChildRows
                    If ChildRow.Children > 0 Then
                        Dim ReturnValue As Boolean = IsConsumed(ChildRow)
                        If ReturnValue = True Then
                            ConsumedCount += 1
                        End If
                    Else
                        If Val(ChildRow.Cells("DecomposedQty").Value + ChildRow.Cells("WastedQty").Value + ChildRow.Cells("ScrappedQty").Value) >= Val(ChildRow.Cells("DecomposableQty").Value) Then
                            ConsumedCount += 1
                        Else
                            Return False
                        End If
                    End If
                Next
                'Count -= 1
            End If
            'End While
            If ConsumedCount >= ChildCount AndAlso ConsumedCount > 0 Then
                'Node.BeginEdit()
                'Node.Cells("TotatConsumedQty").Value = 1
                'Node.EndEdit()
                Dim dr1 As DataRow
                dr1 = dt.NewRow
                dr1("DecompositionId") = Node.Cells("DecompositionId").Value
                dr1("EstimationDetailId") = Node.Cells("EstimationDetailId").Value
                dr1("PlanItemId") = Node.Cells("PlanItemId").Value
                dr1("ProductId") = Node.Cells("ProductId").Value
                dr1("ParentId") = Node.Cells("ParentId").Value
                dr1("Price") = Node.Cells("Price").Value
                dr1("WastedQty") = Node.Cells("WastedQty").Value
                dr1("ScrappedQty") = Node.Cells("ScrappedQty").Value
                dr1("Tag") = Node.Cells("Tag").Value
                dr1("ParentTag") = Node.Cells("ParentTag").Value
                dr1("DepartmentId") = Node.Cells("DepartmentId").Value
                dr1("LocationId") = Node.Cells("LocationId").Value
                dr1("UniqueId") = Node.Cells("UniqueId").Value
                dr1("ParentUniqueId") = Node.Cells("ParentUniqueId").Value
                dr1("TotalConsumedQty") = 1
                dt.Rows.Add(dr1)
                '    Return True
                'Else
                '    Return False
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function


    Private Sub GetConsumed(ByVal Node As Janus.Windows.GridEX.GridEXRow)
        Try
            ChildCount = 0
            Count = 0
            ConsumedCount = 0
            If Node.Cells("TotalConsumedQty").Value < 1 Then
                ChildCount = Node.Children
                Count = Node.Children
                'While Count > 0
                If Count > 0 Then
                    For Each ChildRow As Janus.Windows.GridEX.GridEXRow In Node.GetChildRows
                        If ChildRow.Children > 0 Then
                            If Val(ChildRow.Cells("DecomposedQty").Value + ChildRow.Cells("WastedQty").Value + ChildRow.Cells("ScrappedQty").Value) >= Val(ChildRow.Cells("DecomposableQty").Value) Then
                                ConsumedCount += 1
                            End If
                            GetConsumed(ChildRow)
                        Else
                            If Val(ChildRow.Cells("DecomposedQty").Value + ChildRow.Cells("WastedQty").Value + ChildRow.Cells("ScrappedQty").Value) >= Val(ChildRow.Cells("DecomposableQty").Value) Then
                                ConsumedCount += 1
                            End If
                        End If
                    Next
                    'Else
                    '    If Val(Node.Cells("DecomposedQty").Value + Node.Cells("WastedQty").Value + Node.Cells("ScrappedQty").Value) >= Val(Node.Cells("DecomposableQty").Value) Then
                    '        ConsumedCount += 1
                    '    End If
                    'Count -= 1
                End If
                'End While
                If ConsumedCount >= ChildCount AndAlso ConsumedCount > 0 Then
                    'Node.BeginEdit()
                    'Node.Cells("TotatConsumedQty").Value = 1
                    'Node.EndEdit()
                    Dim dr1 As DataRow
                    dr1 = dt.NewRow
                    dr1("DecompositionId") = Node.Cells("DecompositionId").Value
                    dr1("EstimationDetailId") = Node.Cells("EstimationDetailId").Value
                    dr1("PlanItemId") = Node.Cells("PlanItemId").Value
                    dr1("ProductId") = Node.Cells("ProductId").Value
                    dr1("ParentId") = Node.Cells("ParentId").Value
                    dr1("Price") = Node.Cells("Price").Value
                    dr1("WastedQty") = Node.Cells("WastedQty").Value
                    dr1("ScrappedQty") = Node.Cells("ScrappedQty").Value
                    dr1("Tag") = Node.Cells("Tag").Value
                    dr1("ParentTag") = Node.Cells("ParentTag").Value
                    dr1("DepartmentId") = Node.Cells("DepartmentId").Value
                    dr1("LocationId") = Node.Cells("LocationId").Value
                    dr1("UniqueId") = Node.Cells("UniqueId").Value
                    dr1("ParentUniqueId") = Node.Cells("ParentUniqueId").Value
                    dr1("TotalConsumedQty") = 1
                    dt.Rows.Add(dr1)
                    '    Return True
                    'Else
                    '    Return False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AreAllChildrenDecomposed(ByVal grdRow As Janus.Windows.GridEX.GridEXRow)
        Try
            If grdRow.Children > 0 Then
                If Not Val(grdRow.Cells("DecomposedQty").Value.ToString + grdRow.Cells("WastedQty").Value.ToString + grdRow.Cells("ScrappedQty").Value.ToString) > 0 Then
                    TotalConsumedQty = 0
                    TotalAvailableQty = 0
                    GetConsumedParent(grdRow)
                    If TotalConsumedQty >= TotalAvailableQty Then
                        'grdRow.BeginEdit()
                        Dim dr1 As DataRow
                        dr1 = dt.NewRow
                        dr1("DecompositionId") = grdRow.Cells("DecompositionId").Value
                        dr1("EstimationDetailId") = grdRow.Cells("EstimationDetailId").Value
                        dr1("PlanItemId") = grdRow.Cells("PlanItemId").Value
                        dr1("ProductId") = grdRow.Cells("ProductId").Value
                        dr1("ParentId") = grdRow.Cells("ParentId").Value
                        dr1("Price") = grdRow.Cells("Price").Value
                        dr1("WastedQty") = grdRow.Cells("WastedQty").Value
                        dr1("ScrappedQty") = grdRow.Cells("ScrappedQty").Value
                        dr1("Tag") = grdRow.Cells("Tag").Value
                        dr1("ParentTag") = grdRow.Cells("ParentTag").Value
                        dr1("DepartmentId") = grdRow.Cells("DepartmentId").Value
                        dr1("LocationId") = grdRow.Cells("LocationId").Value
                        dr1("UniqueId") = grdRow.Cells("UniqueId").Value
                        dr1("ParentUniqueId") = grdRow.Cells("ParentUniqueId").Value
                        dr1("TotalConsumedQty") = 1
                        dt.Rows.Add(dr1)
                        'grdRow.EndEdit()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetConsumedParent(ByVal grdRow As Janus.Windows.GridEX.GridEXRow)
        Try
            If grdRow.Children > 0 Then
                For Each Row As Janus.Windows.GridEX.GridEXRow In grdRow.GetChildRows
                    'If Not Row.Parent Is Nothing Then
                    Dim ParentQty As Double = (Val(Row.Parent.Cells("DecomposedQty").Value.ToString) + Val(Row.Parent.Cells("WastedQty").Value.ToString) + Val(Row.Parent.Cells("ScrappedQty").Value.ToString))
                    Dim CQty As Double = (Val(Row.Cells("DecomposedQty").Value.ToString) + Val(Row.Cells("WastedQty").Value.ToString) + Val(Row.Cells("ScrappedQty").Value.ToString))
                    Dim AQty As Double = (Val(Row.Cells("DecomposableQty").Value.ToString) - (Val(Row.Cells("TempDecQty").Value.ToString) + Val(Row.Cells("TempWasQty").Value.ToString) + Val(Row.Cells("TempScrQty").Value.ToString)))
                    If CQty <= 0 AndAlso Row.Children > 0 Then
                        TotalConsumedQty += 0
                        TotalAvailableQty += 0
                    Else
                        TotalConsumedQty += (Val(Row.Cells("DecomposedQty").Value.ToString) + Val(Row.Cells("WastedQty").Value.ToString) + Val(Row.Cells("ScrappedQty").Value.ToString))
                        TotalAvailableQty += (Val(Row.Cells("DecomposableQty").Value.ToString) - (Val(Row.Cells("TempDecQty").Value.ToString) + Val(Row.Cells("TempWasQty").Value.ToString) + Val(Row.Cells("TempScrQty").Value.ToString)))
                    End If

                    'End If
                    If Row.Children > 0 Then
                        'TotalConsumedQty += (Val(Row.Cells("DecomposedQty").Value.ToString) + Val(Row.Cells("WastedQty").Value.ToString) + Val(Row.Cells("ScrappedQty").Value.ToString))
                        'TotalAvailableQty += (Val(Row.Cells("DecomposableQty").Value.ToString) - (Val(Row.Cells("TempDecQty").Value.ToString) + Val(Row.Cells("TempWasQty").Value.ToString) + Val(Row.Cells("TempScrQty").Value.ToString)))
                        GetConsumedParent(Row)
                        'ElseIf Row.Children = 0 AndAlso ParentQty > 0 Then
                        '    TotalConsumedQty += 0
                        '    TotalAvailableQty += 0
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsParentNode(ByVal NodeItem As Janus.Windows.GridEX.GridEXRow)

        If NodeItem.Children > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsTopParent(ByVal Node As Janus.Windows.GridEX.GridEXRow) As Boolean
        'Checks the value in grid column "IsTopParent". If the value is 1, True is returned. False otherwise

        If Val(Node.Cells(GridColumns.IS_TOP_PARENT).Value.ToString) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function IsTotallyConsumed(ByVal NodeItem As Janus.Windows.GridEX.GridEXRow)
        'Return true if value of "TotalConsumedQty" column is 1 in the passed parameter "NodeItem"

        If Val(NodeItem.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value.ToString) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function MarkAllChildrenConsumed(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow, ByVal ConsumpType As ConsumptionType)
        'Returns True if all children were marked consumed, False otherwise

        Dim ChildCount As Integer = NodeItem.Children

        Dim MarkedConsumedCount As Integer = 0

        For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows
            If IsParentNode(Node) And Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString) > 0 Then
                ' "Node" is a parent i.e. it has children
                MarkAllChildrenConsumed(Node, ConsumpType)

                Node.BeginEdit()

                Select Case ConsumpType
                    Case ConsumptionType.DECOMPOSED
                        Node.Cells(GridColumns.DECOMPOSED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case ConsumptionType.SCRAPPED
                        Node.Cells(GridColumns.SCRAPPED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case ConsumptionType.WASTED
                        Node.Cells(GridColumns.WASTED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case Else
                        'TO-DO: This is an internal error. this shold be logged
                        ShowErrorMessage("Invalid comsumption type passed as parameter")
                End Select

                Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = Node.Cells(GridColumns.AVAILABLE_QTY).Value

                Node.EndEdit()

                MarkedConsumedCount += 1  'Increment the counter

            Else
                ' "Node" is not a parent. Mark it consumed


                Node.BeginEdit()

                Select Case ConsumpType
                    Case ConsumptionType.DECOMPOSED
                        Node.Cells(GridColumns.DECOMPOSED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case ConsumptionType.SCRAPPED
                        Node.Cells(GridColumns.SCRAPPED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case ConsumptionType.WASTED
                        Node.Cells(GridColumns.WASTED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    Case Else
                        'TO-DO: This is an internal error. this shold be logged
                        ShowErrorMessage("Invalid comsumption type passed as parameter")
                End Select

                Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)

                Node.EndEdit()

                MarkedConsumedCount += 1  'Increment the counter


                'End If

            End If
        Next

        If ChildCount = MarkedConsumedCount Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub ConsumeChildrenIfParentsAreConsumed()
        Dim a As Integer = grd.GetDataRows.Length
        Dim b As Integer = grd.GetRows.Length
        For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetDataRows
            If IsParentNode(Node) And Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString) > 0 Then
                'Node.Cells("Total").Value =
                If Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString) Then
                    MarkAllChildrenConsumed(Node, ConsumptionType.DECOMPOSED)
                ElseIf Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString) Then
                    MarkAllChildrenConsumed(Node, ConsumptionType.SCRAPPED)
                ElseIf Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString) = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString) Then
                    MarkAllChildrenConsumed(Node, ConsumptionType.WASTED)
                End If
            End If
        Next
    End Sub

    Private Function MarkParentConsumedIfChildrenAreConsumed(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim DecomposedQty As Double = 0
        Dim WastedQty As Double = 0
        Dim ScrappedQty As Double = 0
        Dim ItemQty As Double = 0
        Dim AvailableQty As Double = 0

        Dim DecomposedQtySum As Double = 0
        Dim WastedQtySum As Double = 0
        Dim ScrappedQtySum As Double = 0
        Dim AvailableQtySum As Double = 0
        Dim QtyOfAllChildren As Double = 0

        Dim ReturnValue As Boolean = False


        Dim ChildParentsConsumedCount As Integer = 0
        Dim ChildRowCount As Integer = NodeItem.GetChildRows.Length


        Dim ChildConsumedCount As Integer = 0

        Dim WasTheLastNodeProcessedAChild As Boolean

        'Counts for each type of decomposition
        Dim ConsumedCount_Decomposed As Integer = 0
        Dim ConsumedCount_Scrapped As Integer = 0
        Dim ConsumedCount_Wasted As Integer = 0

        For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows



            WasTheLastNodeProcessedAChild = True 'Reset the variable in the start of the loop

            If IsParentNode(Node) Then
                WasTheLastNodeProcessedAChild = False

                If MarkParentConsumedIfChildrenAreConsumed(Node) = True Then
                    ChildParentsConsumedCount += 1

                    If Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) = 1 Then
                        ConsumedCount_Decomposed += 1
                    ElseIf Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) = 1 Then
                        ConsumedCount_Scrapped += 1
                    ElseIf Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString) = 1 Then
                        ConsumedCount_Wasted += 1
                    End If
                End If
            Else
                'Node is not a parnet node
                DecomposedQty = Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString)
                WastedQty = Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString)
                ScrappedQty = Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString)
                AvailableQty = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)


                'ItemQty = Val(Node.Cells(GridColumns.QTY).Value.ToString)
                ItemQty = Val(Node.Cells(GridColumns.QTY).Value.ToString)


                If AvailableQty = DecomposedQty + WastedQty + ScrappedQty Then
                    'The child node is totally consumed

                    ChildConsumedCount += 1

                    If AvailableQty = DecomposedQty Then
                        ConsumedCount_Decomposed += 1
                    ElseIf AvailableQty = ScrappedQty Then
                        ConsumedCount_Scrapped += 1
                    ElseIf AvailableQty = WastedQty Then
                        ConsumedCount_Wasted += 1
                    End If
                End If

                'Set the consumption value of this child 
                Node.BeginEdit()
                Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = DecomposedQty + WastedQty + ScrappedQty
                Node.EndEdit()


            End If
        Next

        NodeItem.BeginEdit()

        If WasTheLastNodeProcessedAChild = True Then
            If ChildRowCount = ConsumedCount_Decomposed + ConsumedCount_Scrapped + ConsumedCount_Wasted Then
                'Consumption may be either of one type or mixed


                'Now the parnet shall be marked Consumed. We have to decide what type of consumption will be marked
                'among Decomposed, Wasted, or Scrapped

                Select Case ChildRowCount
                    Case ConsumedCount_Decomposed
                        'Mark parent consumed as DECOMPOSED
                        NodeItem.Cells(GridColumns.DECOMPOSED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                    Case ConsumedCount_Wasted
                        'Mark parent consumed as WASTED
                        NodeItem.Cells(GridColumns.WASTED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                    Case ConsumedCount_Scrapped
                        'Mark parent consumed as SCRAPPED
                        NodeItem.Cells(GridColumns.SCRAPPED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                    Case Else

                End Select

                'Mark parent consumed. Since consumption may be of one or multiple types, we will mark another column
                NodeItem.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)

                ReturnValue = True
            End If

        Else
            'The last node processed in this function was not a child i.e. it was a parnet
            If ChildRowCount = ConsumedCount_Decomposed + ConsumedCount_Scrapped + ConsumedCount_Wasted Then
                Select Case ChildRowCount
                    Case ConsumedCount_Decomposed
                        'Mark parent consumed as DECOMPOSED
                        NodeItem.Cells(GridColumns.DECOMPOSED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                    Case ConsumedCount_Wasted
                        'Mark parent consumed as WASTED
                        NodeItem.Cells(GridColumns.WASTED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                    Case ConsumedCount_Scrapped
                        'Mark parent consumed as SCRAPPED
                        NodeItem.Cells(GridColumns.SCRAPPED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)
                End Select

                'Mark parent consumed. Since consumption may be of one or multiple types, we will mark another column
                NodeItem.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = Val(NodeItem.Cells(GridColumns.QTY).Value.ToString)

                ReturnValue = True
            End If
        End If

        NodeItem.EndEdit()

        Return ReturnValue

    End Function

    Private Sub ConsumeParentsIfChildrenAreConsumed()
        ' 
        Try
            Dim a As Integer = grd.GetDataRows.Length
            Dim b As Integer = grd.GetRows.Length
            Dim ItemName As String

            For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                ItemName = Node.Cells("Product").Value

                If IsParentNode(Node) Then
                    'We need to consider only those parents having Consumed Qty less than Qty.
                    'Those parents having Consumed Qty = Qty will already been considered in ConsumeChildrenIfParentsAreConsumed()
                    If Val(Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value.ToString) < 1 Then
                        MarkParentConsumedIfChildrenAreConsumed(Node)
                    End If
                Else
                    'Handle the non-parent item
                    Dim DecomposedQty As Double = 0
                    Dim WastedQty As Double = 0
                    Dim ScrappedQty As Double = 0
                    Dim ItemQty As Double = 0
                    Dim AvailableQty As Double = 0

                    Dim DecomposedQtySum As Double = 0
                    Dim WastedQtySum As Double = 0
                    Dim ScrappedQtySum As Double = 0
                    Dim QtyOfAllChildren As Double = 0
                    Dim AvailableQtySum As Double = 0

                    DecomposedQty = Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString)
                    WastedQty = Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString)
                    ScrappedQty = Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString)

                    ''
                    ''Commented on 08-12-2017
                    'ItemQty = Val(Node.Cells(GridColumns.QTY).Value.ToString)
                    AvailableQty = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)

                    DecomposedQtySum += DecomposedQty
                    WastedQtySum += WastedQty
                    ScrappedQtySum += ScrappedQty
                    AvailableQtySum += AvailableQty

                    QtyOfAllChildren += ItemQty

                    Node.BeginEdit()

                    Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = DecomposedQtySum + WastedQtySum + ScrappedQtySum

                    'If Val(QtyOfAllChildren) = Val(DecomposedQtySum + WastedQtySum + ScrappedQtySum) Then
                    If AvailableQtySum = DecomposedQtySum + WastedQtySum + ScrappedQtySum Then
                        'Now the non-parnet shall be marked Consumed. We have to decide what type of consumption will be marked
                        'among Decomposed, Wasted, or Scrapped

                        Select Case AvailableQtySum
                            Case DecomposedQtySum
                                'Mark non-parent consumed as DECOMPOSED
                                Node.Cells(GridColumns.DECOMPOSED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                            Case WastedQtySum
                                'Mark non-parent consumed as WASTED
                                Node.Cells(GridColumns.WASTED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                            Case ScrappedQtySum
                                'Mark non-parent consumed as SCRAPPED
                                Node.Cells(GridColumns.SCRAPPED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                            Case Else

                        End Select

                        'Mark parent consumed. Since consumption is of multiple types, we will mark another column
                        Node.Cells(GridColumns.TOTAL_CONSUMED_QTY).Value = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)

                        Node.EndEdit()

                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function MarkAllTopParents() As Integer
        'A TOP PARENT is row in the grid that has no parent

        Dim MarkedCount As Integer = 0

        For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetRows
            If IsParentNode(Node) Then

                Node.BeginEdit()
                Node.Cells(GridColumns.IS_TOP_PARENT).Value = 1
                Node.EndEdit()

                'If STOCK_IMPACT_OF_TOP_PARENT_ITEM = True Then
                'Mark that stock impact of this top parent is needed
                'Node.Cells(GridColumns.STOCK_IMPACT).Value = 1

                'Else
                'Mark that stock impact of this top parent is not needed
                'Node.Cells(GridColumns.STOCK_IMPACT).Value = 0
            End If

            MarkedCount += 1

        Next

        Return MarkedCount

    End Function

    Private Function WillStockImpactBeRequired(ByVal NodeItem As Janus.Windows.GridEX.GridEXRow) As Boolean
        ' === IMPORTANT ASSUMPTION ===
        'In case of scrapped item, stock will not be impacted

        'Evaluates the column value of the parameter "NodeItem" and returns:
        '   True if stock impact will be required
        '   False if stock impact will not be required


        Dim DecomposedQty As Double = Val(NodeItem.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString)
        Dim ScrappedQty As Double = Val(NodeItem.Cells(GridColumns.SCRAPPED_QTY).Value.ToString)
        Dim WastedQty As Double = Val(NodeItem.Cells(GridColumns.WASTED_QTY).Value.ToString)

        ''If child stock impact is witnessed then parent stock impact should not be witnessed.


        If IsParentNode(NodeItem) Then
            Dim ConsumedChildCount As Integer = Val(NodeItem.Cells(GridColumns.CONSUMED_CHILD_COUNT).Value.ToString)
            Dim StockImpact As Integer = Val(NodeItem.Cells(GridColumns.STOCK_IMPACT).Value.ToString)
            'In case the passed parameter is a parent node
            'Stock Impact Flag (SIF) of a parent item will be set only if all child items are decomposed in one type. In case
            'children are not decomposed in one type, SIF of parent is not mraked and its SIFs of children items are set individually.
            If ConsumedChildCount < 1 AndAlso StockImpact < 1 Then
                If DecomposedQty > 0 Or ScrappedQty > 0 Or WastedQty > 0 Then
                    Return True  'Indicate that stock will be impacted
                Else
                    Return False  'Indicate that stock will not be impacted
                End If
            Else
                Return False
            End If

        Else
            'The passed parameter is not a parent node
            If DecomposedQty + ScrappedQty > 0 Or WastedQty > 0 Then
                Return True  'Indicate that stock will be impacted
            Else
                Return False  'Indicate that stock will not be impacted
            End If
        End If

    End Function

    Private Sub MarkStockImpactOfChildren(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow)
        'With current logic, the passed parameter "Node" shall always be a top parent since the 
        'caller function handles nodes that are not top parents

        Dim RowsCount As Integer = NodeItem.Children
        Dim s As String

        For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows
            s = Node.Cells("Product").Value
            If IsParentNode(Node) And Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 0 Then
                If WillStockImpactBeRequired(Node) Then
                    Node.BeginEdit()
                    Node.Cells(GridColumns.STOCK_IMPACT).Value = 1
                    Node.EndEdit()
                Else
                    MarkStockImpactOfChildren(Node)  'Recursive function call
                End If
            ElseIf WillStockImpactBeRequired(Node) Then  'Check if stock will be required or not?
                Node.BeginEdit()
                Node.Cells(GridColumns.STOCK_IMPACT).Value = 1  'Mark that stock will be impacted
                Node.EndEdit()
            End If
        Next

    End Sub

    Private Sub MarkStockImpact()
        Try

            'Scan all items (rows) in the grid and mark them accordingly if their stock impact is needed
            'Currently, stock impact of top marent will not be made 

            ' === IMPORTANT ===
            'This function assumes that, if all children of parent have been consumed, then that parent has been marked as
            'consumed in ConsumeParentsIfChildrenAreConsumed() function
            Dim RowsCount As Integer = grd.GetRows.Length
            Dim s As String

            For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetDataRows        'GetRows
                s = Node.Cells("Product").Value
                If IsTopParent(Node) Then
                    'Node.BeginEdit()
                    'Node.Cells(GridColumns.STOCK_IMPACT).Value = 0  'Mark that stock impact of this top parent will not be made
                    'Node.EndEdit()
                    If Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) < 1 Then
                        MarkStockImpactOfChildren(Node)  'Function call
                    End If

                ElseIf WillStockImpactBeRequired(Node) Then  'Check if stock will be required or not?
                    Node.BeginEdit()
                    Node.Cells(GridColumns.STOCK_IMPACT).Value = 1  'Mark that stock will be impacted
                    Node.EndEdit()
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try

            If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim ParentQty As Double = 0
                Dim ChildQty As Double = 0
                Dim TotalQty As Double = 0

                Me.grd.UpdateData()

                'TestMethod(grd.GetRow.Parent)
                'Exit Sub
                If e.Column.Key = "DecomposedQty" Or e.Column.Key = "WastedQty" Or e.Column.Key = "ScrappedQty" Then
                    '' Prevention of minus quantity
                    'Dim ConsumedQty As Double = (Val(Me.grd.GetRow.Cells("DecomposedQty").Value - Me.grd.GetRow.Cells("TempDecQty").Value) + Val(Me.grd.GetRow.Cells("WastedQty").Value - Me.grd.GetRow.Cells("TempWasQty").Value) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value - Me.grd.GetRow.Cells("TempScrQty").Value))
                    If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) < 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) < 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) < 0 Then
                        msg_Information("Minus quantity is not allowed.")
                        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                        Exit Sub
                    End If
                    ''
                    '' Finish or semi finish item should not be consumed less than 1
                    If Me.grd.GetRow.Children > 0 Then
                        If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Then
                            msg_Information("Finish or semi finish item could not be decomposed less than one.")
                            Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                            'Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                            'Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                            Exit Sub
                        ElseIf Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
                            msg_Information("Finish or semi finish item could not be scrapped less than one.")
                            'Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                            'Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                            Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                            Exit Sub
                        ElseIf Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) < 1 AndAlso Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Then
                            msg_Information("Finish or semi finish item could not be wasted less than one.")
                            'Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                            Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                            'Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                            Exit Sub
                        End If

                        If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
                            GetDecomposedChildRows(grd.GetRow)
                            If IsAnyChildDecomposed = True Or Val(Me.grd.GetRow.Cells("CheckChildIsConsumed").Value.ToString) > 0 Then
                                msg_Information("Finish or semi finish can't be decomposed if any child item is decomposed.")
                                'If DecomposedQty > 0 Or ScrappedQty > 0 Or WastedQty > 0 Then
                                Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                                Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                                Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                                'End If
                                IsAnyChildDecomposed = False
                            End If
                        End If
                    End If
                    ''
                    ''Checking multi parent consumed quantity
                    If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) > 0 Or Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > 0 Then
                        GetDecomposedParentRows(grd.GetRow.Parent)
                        If IsParentDecomposed = True Then
                            msg_Information("A child item can not be decomposed in case its parent is consumed.")
                            'If DecomposedQty > 0 Or ScrappedQty > 0 Or WastedQty > 0 Then
                            Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                            Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                            Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                            'End If
                            IsParentDecomposed = False
                        End If
                    End If


                    ''Quantity comparing section
                    '' End checking multi parent quantity

                    'Dim ConsumedQty As Double = (Val(Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempDecQty").Value.ToString)) + Val(Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempWasQty").Value.ToString)) + Val(Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) - Val(Me.grd.GetRow.Cells("TempScrQty").Value.ToString)))
                    'Dim CurrentTotalQty As Double = Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString)
                    'Dim RecentTotalQty As Double = DecomposedQty + WastedQty + ScrappedQty
                    'If CurrentTotalQty > RecentTotalQty Then
                    '    Dim SumQty As Double = CurrentTotalQty - RecentTotalQty
                    '    'If (Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString)) > Val(Me.grd.GetRow.Cells("Qty").Value.ToString) Then
                    '    If SumQty > Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString) Then
                    '        msg_Information("Total quantity has exceeded available quantity")
                    '        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                    '        Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                    '        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                    '        Exit Sub
                    '    End If
                    'End If
                    If IsEditMode = True Then
                        Dim CurrentTotalQty As Double = Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString)
                        Dim OldTotalQty As Double = Val(Me.grd.GetRow.Cells("TempDecQty").Value.ToString) + Val(Me.grd.GetRow.Cells("TempWasQty").Value.ToString) + Val(Me.grd.GetRow.Cells("TempScrQty").Value.ToString)
                        If CurrentTotalQty > OldTotalQty Then
                            Dim SumQty As Double = CurrentTotalQty - OldTotalQty
                            If SumQty > Val(Me.grd.GetRow.Cells("DecomposableQty").Value.ToString) Then
                                msg_Information("Total quantity has exceeded available quantity")
                                Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                                Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                                Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                            End If
                        End If
                    Else
                        If Val(Me.grd.GetRow.Cells("DecomposedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("WastedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("ScrappedQty").Value.ToString) > Val(Me.grd.GetRow.Cells("Qty").Value.ToString) Then
                            msg_Information("Total quantity has exceeded available quantity")
                            Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                            Me.grd.GetRow.Cells("WastedQty").Value = WastedQty
                            Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                        End If
                    End If
                    '' End quantity comparing section

                    ''Single parent consumed quantity checking
                    'If e.Column.Key = "DecomposedQty" Then
                    '    If Not Me.grd.GetRow.Parent Is Nothing Then
                    '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("DecomposedQty").Value.ToString)
                    '    End If
                    '    If ParentQty > 0 Then
                    '        Me.grd.GetRow.Cells("DecomposedQty").Value = DecomposedQty
                    '        msg_Information("Its parent item has already been decomposed.")
                    '        Exit Sub
                    '    End If
                    'End If
                    'If e.Column.Key = "WastedQty" Then
                    '    If Not Me.grd.GetRow.Parent Is Nothing Then
                    '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("WastedQty").Value.ToString)
                    '    End If
                    '    If ParentQty > 0 Then
                    '        Me.grd.GetRow.Cells("WastedQty").Value = DecomposedQty
                    '        msg_Information("Its parent item has already been decomposed.")
                    '    End If
                    'End If
                    'If e.Column.Key = "ScrappedQty" Then
                    '    If Not Me.grd.GetRow.Parent Is Nothing Then
                    '        ParentQty = Val(Me.grd.GetRow.Parent.Cells("ScrappedQty").Value.ToString)
                    '    End If
                    '    If ParentQty > 0 ThenE:\Working Folder\Version 4\SimpleAccounts\New\Configuration\frmRptArticleBarcode.vb
                    '        Me.grd.GetRow.Cells("ScrappedQty").Value = ScrappedQty
                    '        msg_Information("Its parent item has already been decomposed.")
                    '    End If
                    'End If

                    ''End Single parent consumed quantity checking
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetChildrenValueIfParentsAreConsumed()
        Dim a As Integer = grd.GetDataRows.Length
        Dim b As Integer = grd.GetRows.Length
        For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetDataRows
            Dim DValue1 As Double = 0
            Dim WValue1 As Double = 0
            Dim SValue1 As Double = 0
            If Node.Parent IsNot Nothing Then
                DValue1 = Val(Node.Parent.Cells(GridColumns.DValue).Value.ToString)
                WValue1 = Val(Node.Parent.Cells(GridColumns.WValue).Value.ToString)
                SValue1 = Val(Node.Parent.Cells(GridColumns.SValue).Value.ToString)
            End If
            'AvailableQty = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
            If IsParentNode(Node) And Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 1 And (DValue1 + WValue1 + SValue1) < 1 And Val(Node.Cells(GridColumns.CONSUMED_CHILD_COUNT).Value.ToString) < 1 Then
                'Node.Cells("Total").Value =
                TotalDValue = 0
                TotalWValue = 0
                TotalSValue = 0
                GetAllChildrenValue(Node)
                Node.BeginEdit()
                'If Not Node.Parent Is Nothing And (Val(Node.Parent.Cells(GridColumns.DValue).Value.ToString) + Val(Node.Parent.Cells(GridColumns.DValue).Value.ToString) + Val(Node.Parent.Cells(GridColumns.DValue).Value.ToString)) < 1 Then
                If TotalDValue + TotalWValue + TotalSValue > 0 Then

                    If Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) > 0 Then
                        Node.Cells(GridColumns.DValue).Value = TotalDValue
                    ElseIf Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) > 0 Then
                        Node.Cells(GridColumns.SValue).Value = TotalSValue
                    ElseIf Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString) > 0 Then
                        Node.Cells(GridColumns.WValue).Value = TotalWValue
                    End If
                End If
                Node.EndEdit()
            End If
        Next
    End Sub
    Private Function GetAllChildrenValue(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow)
        'Returns True if all children were marked consumed, False otherwise
        Try
            Dim ChildCount As Integer = NodeItem.Children
            Dim MarkedConsumedCount As Integer = 0
            For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows
                If IsParentNode(Node) And Val(Node.Cells(GridColumns.CONSUMED_CHILD_COUNT).Value.ToString) < 1 Then
                    ' "Node" is a parent i.e. it has children
                    GetAllChildrenValue(Node)
                Else
                    TotalDValue += Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) * Val(Node.Cells(GridColumns.PRICE).Value.ToString)
                    TotalWValue += Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString) * Val(Node.Cells(GridColumns.PRICE).Value.ToString)
                    TotalSValue += Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) * Val(Node.Cells(GridColumns.PRICE).Value.ToString)
                End If
            Next
            '    NodeItem.BeginEdit()
            '    If Val(NodeItem.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) > 0 Then
            '        NodeItem.Cells(GridColumns.DValue).Value = TotalDValue
            '    ElseIf Val(NodeItem.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) > 0 Then
            '        NodeItem.Cells(GridColumns.SValue).Value = TotalSValue
            '    ElseIf Val(NodeItem.Cells(GridColumns.WASTED_QTY).Value.ToString) > 0 Then
            '        NodeItem.Cells(GridColumns.WValue).Value = TotalWValue
            '    End If
            '    NodeItem.EndEdit()
        Catch ex As Exception
            Throw ex
        End Try
        'If ChildCount = MarkedConsumedCount Then
        '    Return True
        'Else
        '    Return False
        'End If
        Return True
    End Function
    Private Sub SetChildrenVoucherValueIfParentsNotAreConsumed()
        ' 
        Try
            Dim a As Integer = grd.GetDataRows.Length
            Dim b As Integer = grd.GetRows.Length
            Dim ItemName As String

            For Each Node As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                ItemName = Node.Cells("Product").Value

                If IsParentNode(Node) And Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 0 Then
                    MarkChildrenVoucherValues(Node)
                ElseIf Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 1 Then
                    Dim DecomposedQty As Double = 0
                    Dim WastedQty As Double = 0
                    Dim ScrappedQty As Double = 0
                    Dim ItemQty As Double = 0
                    Dim AvailableQty As Double = 0

                    Dim DecomposedQtySum As Double = 0
                    Dim WastedQtySum As Double = 0
                    Dim ScrappedQtySum As Double = 0
                    Dim QtyOfAllChildren As Double = 0
                    Dim AvailableQtySum As Double = 0
                    Dim Price As Double = 0
                    DecomposedQty = Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString)
                    WastedQty = Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString)
                    ScrappedQty = Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString)
                    Price = Val(Node.Cells(GridColumns.PRICE).Value.ToString)
                    AvailableQty = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
                    DecomposedQtySum += DecomposedQty
                    WastedQtySum += WastedQty
                    ScrappedQtySum += ScrappedQty
                    AvailableQtySum += AvailableQty
                    QtyOfAllChildren += ItemQty
                    Node.BeginEdit()
                    'If AvailableQtySum = DecomposedQtySum + WastedQtySum + ScrappedQtySum Then
                    Node.Cells(GridColumns.DValue).Value = Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString) * Price
                    Node.Cells(GridColumns.WValue).Value = Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString) * Price
                    Node.Cells(GridColumns.SValue).Value = Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString) * Price
                    Node.EndEdit()
                    'End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function MarkChildrenVoucherValues(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim DecomposedQty As Double = 0
        Dim WastedQty As Double = 0
        Dim ScrappedQty As Double = 0
        Dim ItemQty As Double = 0
        Dim AvailableQty As Double = 0

        Dim DecomposedQtySum As Double = 0
        Dim WastedQtySum As Double = 0
        Dim ScrappedQtySum As Double = 0
        Dim AvailableQtySum As Double = 0
        Dim QtyOfAllChildren As Double = 0
        Dim Price As Double = 0

        Dim ReturnValue As Boolean = True


        Dim ChildParentsConsumedCount As Integer = 0
        Dim ChildRowCount As Integer = NodeItem.GetChildRows.Length


        Dim ChildConsumedCount As Integer = 0

        Dim WasTheLastNodeProcessedAChild As Boolean

        'Counts for each type of decomposition
        Dim ConsumedCount_Decomposed As Integer = 0
        Dim ConsumedCount_Scrapped As Integer = 0
        Dim ConsumedCount_Wasted As Integer = 0

        For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows
            WasTheLastNodeProcessedAChild = True 'Reset the variable in the start of the loop
            DecomposedQty = Val(Node.Cells(GridColumns.DECOMPOSED_QTY).Value.ToString)
            WastedQty = Val(Node.Cells(GridColumns.WASTED_QTY).Value.ToString)
            ScrappedQty = Val(Node.Cells(GridColumns.SCRAPPED_QTY).Value.ToString)
            AvailableQty = Val(Node.Cells(GridColumns.AVAILABLE_QTY).Value.ToString)
            Dim ConsumedChildCount As Integer = Val(Node.Cells(GridColumns.CONSUMED_CHILD_COUNT).Value.ToString)

            'If IsParentNode(Node) And (DecomposedQty + WastedQty + ScrappedQty) < 1 And ConsumedChildCount < 1 Then
            If IsParentNode(Node) And Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 0 Then

                WasTheLastNodeProcessedAChild = False

                MarkChildrenVoucherValues(Node)
            ElseIf IsParentNode(Node) = False And Val(Node.Cells(GridColumns.STOCK_IMPACT).Value.ToString) = 1 Then
                ItemQty = Val(Node.Cells(GridColumns.QTY).Value.ToString)
                Price = Val(Node.Cells(GridColumns.PRICE).Value.ToString)
                If AvailableQty = DecomposedQty + WastedQty + ScrappedQty Then
                    'The child node is totally consumed
                    ChildConsumedCount += 1
                    If AvailableQty = DecomposedQty Then
                        ConsumedCount_Decomposed += 1
                    ElseIf AvailableQty = ScrappedQty Then
                        ConsumedCount_Scrapped += 1
                    ElseIf AvailableQty = WastedQty Then
                        ConsumedCount_Wasted += 1
                    End If
                End If
                Node.BeginEdit()
                Node.Cells(GridColumns.DValue).Value = DecomposedQty * Price
                Node.Cells(GridColumns.WValue).Value = WastedQty * Price
                Node.Cells(GridColumns.SValue).Value = ScrappedQty * Price
                Node.EndEdit()
            End If
        Next
        Return ReturnValue

    End Function

    Private Sub btnProductSearch_Click(sender As Object, e As EventArgs) Handles btnProductSearch.Click
        Try
            Dim frm1 As New frmTicketDecompositionSearch(True, Me.cmbCategory.SelectedValue, Me.cmbCategory.Text)
            frm1.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub fillDecompositionGrid(ByVal dt As DataTable)
        Try
            Me.grd.DataSource = dt
            Me.grd.UpdateData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ShowProductionProgress()
        Try
            dtResult = New DataTable

            Me.Counter = 0

            Dim MaxTicketId As Integer = 0

            Dim MasterItemId As Integer = 0

            Dim str As String = String.Empty

            Dim Ticket_id As Integer = 0
            Dim Ticket_No As String = String.Empty
            Dim ProductId As Integer = 0
            Dim Product As String = String.Empty
            Dim Qty As Double = 0.0
            Dim Price As Double = 0.0
            Dim DepartmentId As Integer = 0
            Dim Department As String = String.Empty
            Dim ArticleUnitId As Integer = 0
            Dim Unit As String = String.Empty
            Dim DecomposableQty As Double = 0.0
            Dim SubSubId As Integer = 0

            dtResult.Columns.Add("DecompositionDetailId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("DecompositionId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("EstimationDetailId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("TicketNo", System.Type.GetType("System.String"))
            dtResult.Columns.Add("LocationId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("DepartmentId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("Department", System.Type.GetType("System.String"))
            dtResult.Columns.Add("PlanItemId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("PlanItem", System.Type.GetType("System.String"))
            dtResult.Columns.Add("ProductId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("Product", System.Type.GetType("System.String"))
            dtResult.Columns.Add("ParentId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("ArticleUnitId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("Unit", System.Type.GetType("System.String"))
            dtResult.Columns.Add("Price", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("Qty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("DecomposedQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("ScrappedQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("WastedQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("DecomposableQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("Tag", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("ParentTag", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("UniqueId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("UniqueParentId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("TotalConsumedQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("TempDecQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("TempWasQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("TempScrQty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("IsTopParent", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("StockImpact", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("ConsumedChildCount", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("SubSubId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("PlanItemSubSubId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("DValue", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("WValue", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("SValue", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("CheckChildIsConsumed", System.Type.GetType("System.Int32"))


            Dim dtMaxTicket As New DataTable
            Dim dtDetailMaxTicket As New DataTable
            Dim dtTicketDetail As New DataTable
            Dim dtTicketsMaterial As New DataTable
            Dim dtMasterTicketMaterial As New DataTable
            Dim dtDecompositionMaster As New DataTable
            Dim dtCloseBatch As New DataTable

            str = "select max (PlanTicketsMasterID) As TicketId from PlanTicketsMaster where planid  = " & cmbPlan.SelectedValue
            dtMaxTicket = GetDataTable(str)

            If dtMaxTicket.Rows.Count > 0 Then

                MaxTicketId = dtMaxTicket.Rows(0).Item(0)

                str = String.Empty

                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketsMaster.MasterArticleId , " _
                      & "ArticleDeftableMaster.ArticleDescription As Item , SalesOrderDetailTable.Qty , ArticleGroupDef.SubSubID As PlanItemSubSubId " _
                      & "from PlanTicketsMaster " _
                      & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                      & "Left Outer Join ArticleDeftableMaster On PlanTicketsMaster.MasterArticleId = ArticleDeftableMaster.ArticleId " _
                      & "Left Outer Join SalesOrderDetailTable On PlanTicketsMaster.SalesOrderID = SalesOrderDetailTable.SalesOrderId " _
                      & "Left Outer Join ArticleGroupDefTable As ArticleGroupDef On ArticleDeftableMaster.ArticleGroupId = ArticleGroupDef.ArticleGroupId " _
                      & "where PlanTicketsMaster.PlanTicketsMasterID = " & MaxTicketId

                dtDetailMaxTicket = GetDataTable(str)

                If dtDetailMaxTicket.Rows.Count > 0 Then

                    PlanTicketId = dtDetailMaxTicket.Rows(0).Item(0)
                    'PlanNo = dtDetailMaxTicket.Rows(0).Item(1)
                    TicketNo = dtDetailMaxTicket.Rows(0).Item(2)
                    PlanItemId = dtDetailMaxTicket.Rows(0).Item(3)
                    PlanItem = dtDetailMaxTicket.Rows(0).Item(4)
                    'Qty = dtDetailMaxTicket.Rows(0).Item(5)
                    PlanItemSubSubId = dtDetailMaxTicket.Rows(0).Item(6)

                    'Dim R As DataRow = dtResult.NewRow

                    'R("TicketId") = PlanTicketId
                    'R("PlanNo") = PlanNo
                    'R("TicketNo") = TicketNo
                    'R("Articleid") = ArticleId
                    'R("Item") = Item
                    'R("Qty") = Qty
                    'Me.Counter += 1
                    'R("UniqueId") = Me.Counter
                    'R("UniqueParentId") = 0

                    'dtResult.Rows.Add(R)

                    str = String.Empty

                    str = "select PlanTicketsMaster.PlanTicketsMasterID As TicketId , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId As ProductId , " _
                          & "ArticleDeftable.ArticleDescription As Product , isNull(SUM(CASE WHEN TYPE='Plus' THEN isNull(QTY,0) WHEN TYPE = 'Minus' THEN isNull(-QTY,0) ELSE isNull(QTY,0) END),0) AS Qty , PlanTicketMaterialDetail.CostPrice As Price , ArticleDeftable.MasterId As PlanItemId , " _
                          & "ArticleDefTableMaster.ArticleDescription As PlanItem , PlanTicketMaterialDetail.DepartmentId , " _
                          & "tblproSteps.prod_step As Department , ArticleDeftable.ArticleUnitId , ArticleUnitDefTable.ArticleUnitName As Unit , 0 As DecomposableQty , ArticleGroupDefTable.SubSubID As SubSubId " _
                          & "from PlanTicketMaterialDetail " _
                          & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                          & "Left Outer Join PlanTicketsMaster On PlanTicketMaterialDetail.ticketid = PlanTicketsMaster.PlanTicketsMasterID " _
                          & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                          & "Left outer join tblproSteps On PlanTicketMaterialDetail.DepartmentId = tblproSteps.ProdStep_Id " _
                          & "Left Outer Join ArticleDefTableMaster On ArticleDeftable.MasterId = ArticleDefTableMaster.ArticleId " _
                          & "Left Outer Join ArticleUnitDefTable On ArticleDeftable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                          & "Left Outer Join ArticleGroupDefTable On ArticleDeftable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId " _
                          & "where PlanTicketMaterialDetail.ticketid = " & MaxTicketId & " " _
                          & "group by PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , PlanTicketMaterialDetail.CostPrice , ArticleDeftable.MasterId , ArticleUnitDefTable.ArticleUnitName , ArticleGroupDefTable.SubSubID , ArticleDefTableMaster.ArticleDescription, PlanTicketMaterialDetail.DepartmentId , tblproSteps.prod_step , ArticleDeftable.ArticleUnitId "

                    dtTicketDetail = GetDataTable(str)

                    For Each row As DataRow In dtTicketDetail.Rows

                        ProductId = Val(row.Item("ProductId").ToString)
                        Product = row.Item("Product").ToString
                        Qty = Val(row.Item("Qty").ToString)
                        Price = Val(row.Item("Price").ToString)
                        MasterItemId = Val(row.Item("PlanItemId").ToString)
                        'PlanItem = row.Item("PlanItem").ToString
                        DepartmentId = Val(row.Item("DepartmentId").ToString)
                        Department = row.Item("Department").ToString
                        ArticleUnitId = Val(row.Item("ArticleUnitId").ToString)
                        Unit = row.Item("Unit").ToString
                        DecomposableQty = Val(row.Item("DecomposableQty").ToString)
                        SubSubId = Val(row.Item("SubSubId").ToString)

                        str = String.Empty

                        str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterItemId & " And PlanId = " & cmbPlan.SelectedValue

                        If cmbTicket.SelectedValue > 0 AndAlso cmbTicket.SelectedValue <> MaxTicketId Then

                            str = str & " And PlanTicketsMaster.PlanTicketsMasterID <> " & MaxTicketId & " And PlanTicketsMaster.PlanTicketsMasterID = " & cmbTicket.SelectedValue

                        End If

                        dtTicketsMaterial = GetDataTable(str)

                        If dtTicketsMaterial.Rows.Count > 0 Then

                            For Each row2 As DataRow In dtTicketsMaterial.Rows

                                Ticket_id = Val(row2.Item("PlanTicketsMasterID").ToString)
                                Ticket_No = row2.Item("TicketNo").ToString

                                str = String.Empty

                                str = "select * from closebatch where Planid = " & cmbPlan.SelectedValue & " And TicketId = " & Ticket_id

                                dtCloseBatch = GetDataTable(str)

                                If dtCloseBatch.Rows.Count > 0 Then

                                    str = String.Empty

                                    str = "select * from MaterialDecompositionDetail where ProductId = " & ProductId & " and TicketId = " & Ticket_id

                                    dtDecompositionMaster = GetDataTable(str)

                                    If dtDecompositionMaster.Rows.Count <= 0 Then

                                        Dim R1 As DataRow = dtResult.NewRow

                                        R1("DecompositionDetailId") = 0
                                        R1("DecompositionId") = 0
                                        R1("EstimationDetailId") = 0
                                        R1("TicketId") = Ticket_id
                                        R1("TicketNo") = Ticket_No
                                        R1("LocationId") = 1
                                        R1("DepartmentId") = DepartmentId
                                        R1("Department") = Department
                                        R1("PlanItemId") = Me.PlanItemId
                                        R1("PlanItem") = Me.PlanItem
                                        R1("ProductId") = ProductId
                                        R1("Product") = Product
                                        R1("ParentId") = 0
                                        R1("ArticleUnitId") = ArticleUnitId
                                        R1("Unit") = Unit
                                        R1("Price") = Price
                                        R1("Qty") = 1
                                        R1("DecomposedQty") = 0
                                        R1("ScrappedQty") = 0
                                        R1("WastedQty") = 0
                                        R1("DecomposableQty") = 1
                                        R1("Tag") = 0
                                        R1("ParentTag") = 0
                                        Me.Counter += 1
                                        R1("UniqueId") = Me.Counter
                                        R1("UniqueParentId") = 0
                                        R1("TotalConsumedQty") = 0
                                        R1("TempDecQty") = 0
                                        R1("TempWasQty") = 0
                                        R1("TempScrQty") = 0
                                        R1("IsTopParent") = 0
                                        R1("StockImpact") = 0
                                        R1("ConsumedChildCount") = 0
                                        R1("SubSubId") = SubSubId
                                        R1("PlanItemSubSubId") = Me.PlanItemSubSubId
                                        R1("DValue") = 0
                                        R1("WValue") = 0
                                        R1("SValue") = 0

                                        Me.CheckChildIsConsumed = 0

                                        If getFinishGoodItems(Ticket_id, Ticket_No, R1("UniqueId")) = True Then

                                            R1("CheckChildIsConsumed") = Me.CheckChildIsConsumed
                                            dtResult.Rows.Add(R1)

                                            Me.CheckChildIsConsumed = 0

                                        End If

                                        Me.CheckChildIsConsumed = 0

                                    End If

                                End If

                            Next

                        Else

                            If cmbTicket.SelectedValue > 0 AndAlso cmbTicket.SelectedValue <> MaxTicketId Then

                                str = String.Empty

                                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterItemId & " And PlanId = " & cmbPlan.SelectedValue

                                dtMasterTicketMaterial = GetDataTable(str)

                                If dtMasterTicketMaterial.Rows.Count <= 0 Then

                                    str = "select * from closebatch where Planid = " & cmbPlan.SelectedValue & " And TicketId = " & PlanTicketId

                                    dtCloseBatch = GetDataTable(str)

                                    If dtCloseBatch.Rows.Count > 0 Then

                                        str = "select * from MaterialDecompositionDetail where ProductId = " & ProductId & " and TicketId = " & PlanTicketId

                                        dtDecompositionMaster = GetDataTable(str)

                                        If dtDecompositionMaster.Rows.Count <= 0 Then

                                            Dim R2 As DataRow = dtResult.NewRow

                                            R2("DecompositionDetailId") = 0
                                            R2("DecompositionId") = 0
                                            R2("EstimationDetailId") = 0
                                            R2("TicketId") = PlanTicketId
                                            R2("TicketNo") = TicketNo
                                            R2("LocationId") = 1
                                            R2("DepartmentId") = DepartmentId
                                            R2("Department") = Department
                                            R2("PlanItemId") = PlanItemId
                                            R2("PlanItem") = PlanItem
                                            R2("ProductId") = ProductId
                                            R2("Product") = Product
                                            R2("ParentId") = 0
                                            R2("ArticleUnitId") = ArticleUnitId
                                            R2("Unit") = Unit
                                            R2("Price") = Price
                                            R2("Qty") = Qty
                                            R2("DecomposedQty") = 0
                                            R2("ScrappedQty") = 0
                                            R2("WastedQty") = 0
                                            R2("DecomposableQty") = Qty
                                            R2("Tag") = 0
                                            R2("ParentTag") = 0
                                            Me.Counter += 1
                                            R2("UniqueId") = Me.Counter
                                            R2("UniqueParentId") = 0
                                            R2("TotalConsumedQty") = 0
                                            R2("TempDecQty") = 0
                                            R2("TempWasQty") = 0
                                            R2("TempScrQty") = 0
                                            R2("IsTopParent") = 0
                                            R2("StockImpact") = 0
                                            R2("ConsumedChildCount") = 0
                                            R2("SubSubId") = SubSubId
                                            R2("PlanItemSubSubId") = Me.PlanItemSubSubId
                                            R2("DValue") = 0
                                            R2("WValue") = 0
                                            R2("SValue") = 0
                                            R2("CheckChildIsConsumed") = Me.CheckChildIsConsumed

                                            dtResult.Rows.Add(R2)

                                        End If

                                    End If

                                End If

                            Else

                                str = "select * from closebatch where Planid = " & cmbPlan.SelectedValue & " And TicketId = " & PlanTicketId

                                dtCloseBatch = GetDataTable(str)

                                If dtCloseBatch.Rows.Count > 0 Then

                                    str = "select * from MaterialDecompositionDetail where ProductId = " & ProductId & " and TicketId = " & PlanTicketId

                                    dtDecompositionMaster = GetDataTable(str)

                                    If dtDecompositionMaster.Rows.Count <= 0 Then

                                        Dim R2 As DataRow = dtResult.NewRow

                                        R2("DecompositionDetailId") = 0
                                        R2("DecompositionId") = 0
                                        R2("EstimationDetailId") = 0
                                        R2("TicketId") = PlanTicketId
                                        R2("TicketNo") = TicketNo
                                        R2("LocationId") = 1
                                        R2("DepartmentId") = DepartmentId
                                        R2("Department") = Department
                                        R2("PlanItemId") = PlanItemId
                                        R2("PlanItem") = PlanItem
                                        R2("ProductId") = ProductId
                                        R2("Product") = Product
                                        R2("ParentId") = 0
                                        R2("ArticleUnitId") = ArticleUnitId
                                        R2("Unit") = Unit
                                        R2("Price") = Price
                                        R2("Qty") = Qty
                                        R2("DecomposedQty") = 0
                                        R2("ScrappedQty") = 0
                                        R2("WastedQty") = 0
                                        R2("DecomposableQty") = Qty
                                        R2("Tag") = 0
                                        R2("ParentTag") = 0
                                        Me.Counter += 1
                                        R2("UniqueId") = Me.Counter
                                        R2("UniqueParentId") = 0
                                        R2("TotalConsumedQty") = 0
                                        R2("TempDecQty") = 0
                                        R2("TempWasQty") = 0
                                        R2("TempScrQty") = 0
                                        R2("IsTopParent") = 0
                                        R2("StockImpact") = 0
                                        R2("ConsumedChildCount") = 0
                                        R2("SubSubId") = SubSubId
                                        R2("PlanItemSubSubId") = Me.PlanItemSubSubId
                                        R2("DValue") = 0
                                        R2("WValue") = 0
                                        R2("SValue") = 0
                                        R2("CheckChildIsConsumed") = Me.CheckChildIsConsumed

                                        dtResult.Rows.Add(R2)

                                    End If

                                End If

                            End If

                        End If

                    Next

                End If

            End If

            dtResult.Columns("DecomposableQty").Expression = " (isNull(Qty,0) - ((IsNull(DecomposedQty, 0)+IsNull(WastedQty, 0)+IsNull(ScrappedQty, 0))))"

            dtResult.AcceptChanges()

            Me.grd.DataSource = dtResult
            Me.grd.UpdateData()



            Me.grd.RootTable.Columns("DecomposableQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Product").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("TotalConsumedQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("StockImpact").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DecomposableQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("WastedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ScrappedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposableQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("WastedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ScrappedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DecomposableQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("WastedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ScrappedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("WastedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ScrappedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("TotalConsumedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("DecomposableQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("WastedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ScrappedQty").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("DValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("WValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SValue").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("LocationId").HasValueList = True
            Me.grd.RootTable.Columns("LocationId").LimitToList = True

            FillCombos("grdLocations")


            'Me.grdProductionProgress.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty

            'Task 3438 Saad Afzaal Task Show Status of each Tickets Item its Produced or in Progress  

            'ProgressParentsIfChildrenAreConsumed()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Function getFinishGoodItems(ByVal Ticket_Id As Integer, ByVal Ticket_No As String, ByVal UniqueId As Integer) As Boolean

        Dim ReturnState As New List(Of Integer)

        Dim str As String = String.Empty


        Dim ProductId As Integer = 0
        Dim Product As String = String.Empty
        Dim Qty As Double = 0.0
        Dim Price As Double = 0
        'Dim PlanItemId As Integer = 0
        'Dim PlanItem As String = String.Empty
        Dim DepartmentId As Integer = 0
        Dim Department As String = String.Empty
        Dim ArticleUnitId As Integer = 0
        Dim Unit As String = String.Empty
        Dim DecomposedQty As Double = 0.0
        Dim SubSubId As Integer = 0

        Dim dt As New DataTable

        Dim dtFinishGoodDetail As New DataTable

        str = String.Empty

        str = "select PlanTicketsMaster.PlanTicketsMasterID As TicketId , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId As ProductId , " _
                           & "ArticleDeftable.ArticleDescription As Product , isNull(SUM(CASE WHEN TYPE='Plus' THEN isNull(QTY,0) WHEN TYPE = 'Minus' THEN isNull(-QTY,0) ELSE isNull(QTY,0) END),0) AS Qty , PlanTicketMaterialDetail.CostPrice As Price , ArticleDeftable.MasterId As PlanItemId , " _
                           & "ArticleDefTableMaster.ArticleDescription As PlanItem , PlanTicketMaterialDetail.DepartmentId , " _
                           & "tblproSteps.prod_step As Department , ArticleDeftable.ArticleUnitId , ArticleUnitDefTable.ArticleUnitName As Unit , ArticleGroupDefTable.SubSubID As SubSubId , " _
                           & "sum(isNull(MaterialDecompositionDetail.DecomposedQty,0) + ' ' + isNull(MaterialDecompositionDetail.ScrappedQty,0) + ' ' + isNull(MaterialDecompositionDetail.WastedQty , 0)) As DecomposedQty " _
                           & "from PlanTicketMaterialDetail " _
                           & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                           & "Left Outer Join PlanTicketsMaster On PlanTicketMaterialDetail.ticketid = PlanTicketsMaster.PlanTicketsMasterID " _
                           & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                           & "Left outer join tblproSteps On PlanTicketMaterialDetail.DepartmentId = tblproSteps.ProdStep_Id " _
                           & "Left Outer Join ArticleDefTableMaster On ArticleDeftable.MasterId = ArticleDefTableMaster.ArticleId " _
                           & "Left Outer Join ArticleUnitDefTable On ArticleDeftable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                           & "Left Outer Join MaterialDecompositionDetail On PlanTicketMaterialDetail.MaterialArticleId = MaterialDecompositionDetail.ProductId And PlanTicketsMaster.PlanTicketsMasterID = MaterialDecompositionDetail.TicketId " _
                           & "Left Outer Join ArticleGroupDefTable On ArticleDeftable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId " _
                           & "where PlanTicketMaterialDetail.ticketid = " & Ticket_Id & " " _
                           & "Group By PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , " _
                           & "PlanTicketMaterialDetail.CostPrice , ArticleDeftable.MasterId , ArticleDefTableMaster.ArticleDescription , " _
                           & "PlanTicketMaterialDetail.DepartmentId , tblproSteps.prod_step , ArticleDeftable.ArticleUnitId , ArticleUnitDefTable.ArticleUnitName , ArticleGroupDefTable.SubSubID"

        dtFinishGoodDetail = GetDataTable(str)

        For Each row As DataRow In dtFinishGoodDetail.Rows

            ProductId = Val(row.Item("ProductId").ToString)
            Product = row.Item("Product").ToString
            Qty = Val(row.Item("Qty").ToString)
            Price = Val(row.Item("Price").ToString)
            'PlanItemId = Val(row.Item("PlanItemId").ToString)
            'PlanItem = row.Item("PlanItem").ToString
            DepartmentId = Val(row.Item("DepartmentId").ToString)
            Department = row.Item("Department").ToString
            ArticleUnitId = Val(row.Item("ArticleUnitId").ToString)
            Unit = row.Item("Unit").ToString
            DecomposedQty = Val(row.Item("DecomposedQty").ToString)
            SubSubId = Val(row.Item("SubSubId").ToString)

            If DecomposedQty > 0 Then
                Me.CheckChildIsConsumed = 1
            End If

            Qty = Qty - DecomposedQty

            If Qty > 0 Then

                Dim R1 As DataRow = dtResult.NewRow

                R1("DecompositionDetailId") = 0
                R1("DecompositionId") = 0
                R1("EstimationDetailId") = 0
                R1("TicketId") = Ticket_Id
                R1("TicketNo") = Ticket_No
                R1("LocationId") = 1
                R1("DepartmentId") = DepartmentId
                R1("Department") = Department
                R1("PlanItemId") = PlanItemId
                R1("PlanItem") = PlanItem
                R1("ProductId") = ProductId
                R1("Product") = Product
                R1("ParentId") = 0
                R1("ArticleUnitId") = ArticleUnitId
                R1("Unit") = Unit
                R1("Price") = Price
                R1("Qty") = Qty
                R1("DecomposedQty") = 0
                R1("ScrappedQty") = 0
                R1("WastedQty") = 0
                R1("DecomposableQty") = Qty
                R1("Tag") = 0
                R1("ParentTag") = 0
                Me.Counter += 1
                R1("UniqueId") = Me.Counter
                R1("UniqueParentId") = UniqueId
                R1("TotalConsumedQty") = 0
                R1("TempDecQty") = 0
                R1("TempWasQty") = 0
                R1("TempScrQty") = 0
                R1("IsTopParent") = 0
                R1("StockImpact") = 0
                R1("ConsumedChildCount") = 0
                R1("SubSubId") = SubSubId
                R1("PlanItemSubSubId") = Me.PlanItemSubSubId
                R1("DValue") = 0
                R1("WValue") = 0
                R1("SValue") = 0
                R1("CheckChildIsConsumed") = 0

                dtResult.Rows.Add(R1)

                ReturnState.Add(1)

            Else

                ReturnState.Add(2)

            End If

        Next

        If ReturnState.Find(Function(bid As Integer) bid = 1) Then
            Return True
        Else
            Return False
        End If

    End Function


End Class
'End Class