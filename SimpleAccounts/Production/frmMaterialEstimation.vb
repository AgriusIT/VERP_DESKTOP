''TASK:1006 Added TagIds to estimation.
''TASK:TFS1141 Validate minus quantity to not exceed estimation quantity or pending estimation quantity.
Imports SBDal
Imports SBModel
Public Class frmMaterialEstimation
    Implements IGeneral
    Dim bal As MaterialEstimationBAL
    Dim master As MaterialEstimationModel
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

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        bal = New MaterialEstimationBAL()
    End Sub
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
            bal.DeleteMaster(Me.grdSaved.GetRow.Cells("Id").Value)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        Try
            If Condition = "SO" Then
                Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo, SalesOrderMasterTable.SpecialInstructions from SalesOrderMasterTable where SalesOrderId In(Select SalesOrderId From PlanMasterTable) Order by SalesOrderDate DESC"
                FillDropDown(cmbSalesOrder, Str)
            ElseIf Condition = "Plan" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSalesOrder.SelectedValue > 0, "Where POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order By PlanId DESC"
                FillDropDown(cmbPlan, Str)
                'ElseIf Condition = "Product" Then
                '    Str = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription As Product, PlanDetailTable.Qty From ArticleDefTable Inner Join PlanDetailTable On ArticleDefTable.ArtileId = PlanDetailTable.ArticleDefId Where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " "
                '    FillUltraDropDown(cmbTicket, Str)
            ElseIf Condition = "Ticket" Then
                'PlanTicketsMasterID	int	Unchecked
                'TicketNo	nvarchar(50)	Checked
                'TicketDate	datetime	Checked
                'CustomerID	int	Checked
                'SalesOrderID	int	Checked
                'PlanID	int	Checked
                'SpecialInstructions	nvarchar(300)	Checked

                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & " And PlanTicketsMaster.PlanTicketsMasterID Not in (Select PlanTicketId From MaterialEstimation) Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.Rows(0).Activate()
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicket.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 400
            ElseIf Condition = "Tickets" Then
                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster Order By PlanTicketsMaster.PlanTicketsMasterID DESC" ''Where PlanTicketsMaster.PlanTicketsMasterID Not in (Select PlanTicketId From MaterialEstimation)
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicket.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                'Me.cmbTicket.DisplayLayout.Bands(0).Columns("TicketNo").
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 400

                'ElseIf Condition = "Ticket" Then
                '    Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation) Order By Ticket.PlanTicketsId DESC"
                '    FillUltraDropDown(cmbTicket, Str)
                '    Me.cmbTicket.Rows(0).Activate()
                'ElseIf Condition = "Tickets" Then
                '    Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Order By Ticket.PlanTicketsId DESC"
                '    FillUltraDropDown(cmbTicket, Str)
                'Me.cmbTicket.Rows(0).Activate()
            ElseIf Condition = "Product" Then
                Str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Combination, IsNull(ArticleDefView.ArticleUnitId, 0) As ArticleUnitId, ArticleDefView.ArticleUnitName as Unit, Isnull(ArticleDefView.PurchasePrice,0) as Price , ArticleDefView.ArticleCompanyName as Category, ArticleDefView.ArticleLpoName as Model, ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(ArticleDefView.SubSubId,0) as AccountId, ArticleDefView.CGSAccountId, Isnull(ArticleDefView.ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterID, ArticleDefTableMaster.ArticleDescription As PlanItem FROM  ArticleDefView LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefView.MasterID = ArticleDefTableMaster.ArticleId LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
                Str += " where ArticleDefView.Active=1 "
                'Str = "SELECT DISTINCT ArticleDefView.ArticleId as ArticleId, ArticleCode , ArticleDescription as Product, ArticleUnitName as Unit, Sum(IsNull(MaterialEstimationDetailTable.Quantity, 0)) As Quantity, MaterialEstimationDetailTable.PlanItemId, Sum(IsNull(AllocationDetail.Quantity, 0)) As AllocationQty, Sum(IsNull(MaterialEstimationDetailTable.Quantity, 0 ) - IsNull(AllocationDetail.Quantity, 0)) As RemainingQty, MaterialEstimationDetailTable.MaterialEstMasterID FROM ArticleDefView Inner Join MaterialEstimationDetailTable On ArticleDefView.ArticleId = MaterialEstimationDetailTable.ProductId Inner Join MaterialEstimation On MaterialEstimationDetailTable.MaterialEstMasterID = MaterialEstimation.Id LEFT JOIN AllocationDetail ON MaterialEstimationDetailTable.Id = AllocationDetail.MaterialEstimationDetailId" _
                '   & " " & IIf(cmbDepartment.Value <= 0, " Where MaterialEstimationDetailTable.MaterialEstMasterID = " & Me.cmbEstimation.Value & "", "Where MaterialEstimationDetailTable.SubDepartmentID = " & Me.cmbDepartment.Value & "") & " And Active = 1  Group by ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, MaterialEstimationDetailTable.PlanItemId,  ArticleUnitName, MaterialEstimationDetailTable.MaterialEstMasterID"


                FillUltraDropDown(cmbProduct, Str)
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Combination").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(1).Width = 200
                If Me.cmbProduct.Rows.Count > 1 Then
                    Me.rbCode.Checked = True
                End If
            ElseIf Condition = "ParentItem" Then
                If Me.grdMaterialEstimation.RowCount > 0 Then
                    'Dim ParentItemList As New List(Of ParentItem)
                    Dim dt As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
                    Dim dtMerged As New DataTable
                    dtMerged.Columns.Add("ArticleId")
                    'dtMerged.Columns.Add("Article code")
                    dtMerged.Columns.Add("Article Description")
                    For Each row As DataRow In dt.Rows
                        Dim drArray() As DataRow = dtMerged.Select("ArticleId = '" & row("ArticleId").ToString & "'")
                        If drArray.Length = 0 Then
                            Dim drMerged As DataRow
                            drMerged = dtMerged.NewRow
                            drMerged(0) = row.Item("ArticleId")
                            drMerged(1) = row.Item("ArticleDescription")
                            dtMerged.Rows.Add(drMerged)
                        End If
                    Next
                    Dim dr As DataRow
                    dr = dtMerged.NewRow
                    dr(0) = Convert.ToInt32(0)
                    dr(1) = strZeroIndexItem
                    'dr(2) = strZeroIndexItem
                    dtMerged.Rows.InsertAt(dr, 0)
                    Me.cmbParentItem.DisplayMember = "Article Description"
                    Me.cmbParentItem.ValueMember = "ArticleId"
                    Me.cmbParentItem.DataSource = dtMerged
                    Me.cmbParentItem.Rows(0).Activate()
                    Me.cmbParentItem.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
                    Me.cmbParentItem.DisplayLayout.Bands(0).Columns("Article Description").Width = 250
                    'If rbName1.Checked = True Then
                    '    Me.cmbParentItem.DisplayMember = Me.cmbParentItem.Rows(0).Cells(2).Column.Key.ToString
                    'ElseIf rbCode1.Checked = True Then
                    '    Me.cmbParentItem.DisplayMember = Me.cmbParentItem.Rows(0).Cells(1).Column.Key.ToString
                    'End If
                End If
            ElseIf Condition = "Department" Then
                'Str = "Select Distinct ProdStep_Id, prod_step, sort_Order from tblProSteps INNER JOIN MaterialEstimationDetailTable Detail ON tblProSteps.ProdStep_Id = Detail.SubDepartmentID Where Detail.MaterialEstMasterID =" & cmbEstimation.Value & "  ORDER BY 2 ASC"
                'FillUltraDropDown(cmbDepartment, Str)
                'Me.cmbDepartment.Rows(0).Activate()
                'Me.cmbDepartment.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbDepartment.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Departments" Then
                'ElseIf Condition = "SubDepartment" Then
                '    FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY 2 ASC")
                Str = "Select Distinct ProdStep_Id, prod_step, sort_Order from tblProSteps ORDER BY sort_Order "
                FillUltraDropDown(cmbDepartment, Str)
                Me.cmbDepartment.Rows(0).Activate()
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "grdDepartments" Then
                Str = "Select Distinct ProdStep_Id, prod_step, sort_Order from tblProSteps ORDER BY sort_Order "
                Dim dt As DataTable = GetDataTable(Str)
                Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").ValueList.PopulateValueList(dt.DefaultView, "ProdStep_Id", "prod_step")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            master = New MaterialEstimationModel()
            master.DocNo = Me.txtDocNo.Text
            master.MasterPlanId = Me.cmbPlan.SelectedValue
            master.SaleOrderId = IIf(Me.cmbSalesOrder.SelectedValue = Nothing, 0, Me.cmbSalesOrder.SelectedValue)
            'If Not Me.cmbTicket.ActiveRow Is Nothing Then
            '    master.PlanItemId = Val(cmbTicket.SelectedRow.Cells("ArticleId").Value.ToString)
            'End If
            master.EstimationDate = dtpDate.Value
            master.Remarks = Me.txtSpecialInstructions.Text
            master.PlanTicketId = IIf(Me.cmbTicket.SelectedRow Is Nothing, 0, Me.cmbTicket.Value)
            master.Id = ID
            dt = New DataTable
            dt = CType(grdMaterialEstimation.DataSource, DataTable)
            dt.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = bal.GetMaster()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("Id").Visible = False
            Me.grdSaved.RootTable.Columns("MasterPlanId").Visible = False
            Me.grdSaved.RootTable.Columns("PlanTicketId").Visible = False
            Me.grdSaved.RootTable.Columns("SaleOrderId").Visible = False
            Me.grdSaved.RootTable.Columns("PlanItemId").Visible = False
            Me.grdSaved.RootTable.Columns("EstimationDate").Caption = "Estimation Date"
            Me.grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
            Me.grdSaved.RootTable.Columns("DocNo").Caption = "Document No"
            'Me.grdSaved.RootTable.Columns("PlanItem").Caption = "Plan Item"
            Me.grdSaved.RootTable.Columns("EstimationDate").Width = 150
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If txtDocNo.Text = "" Then
                msg_Error("Please enter PO No.")
                txtDocNo.Focus() : IsValidate = False : Exit Function
            End If
            'If cmbTicket.ActiveRow.Cells(0).Value <= 0 Then
            '    msg_Error("Please select Product")
            '    cmbTicket.Focus() : IsValidate = False : Exit Function
            'End If
            Me.grdMaterialEstimation.UpdateData()
            If Not Me.grdMaterialEstimation.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbTicket.Focus() : IsValidate = False : Exit Function
            End If
            If Me.grdMaterialEstimation.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grdMaterialEstimation.Focus()
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
            If Not Me.cmbSalesOrder.SelectedValue Is Nothing Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPlan.SelectedValue Is Nothing Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not Me.cmbTicket.SelectedRow Is Nothing Then
                Me.cmbTicket.Rows(0).Activate()
            End If
            Me.btnSave.Text = "&Save"
            IsEditMode = False
            Me.cmbSalesOrder.Enabled = True
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
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
            bal.Save_Transation(dt, master)
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
            FillCombos("SO")
            FillCombos("Plan")
            FillCombos("Product")
            FillCombos("Departments")
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
                MENo = GetSerialNo("ME" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "MaterialEstimation", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("ME" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "MaterialEstimation", "DocNo")
            Else
                MENo = GetNextDocNo("ME", 6, "MaterialEstimation", "DocNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbPlan_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedValueChanged
        Try
            If cmbPlan.SelectedValue > 0 Then
                FillCombos("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicket.ValueChanged
        Dim dt As New DataTable
        Try
            If Me.cmbTicket.Value > 0 Then
                dt = GetTicketDetail(Me.cmbTicket.Value)
                dt.AcceptChanges()
                dtCostSheetAgainstsTicketItems = New DataTable
                For Each dr As DataRow In dt.Rows
                    Dim dt1 As New DataTable
                    'dt1 = GetCostSheetItems(dr("ArticleId").ToString, Val(dr("Quantity").ToString), dr("ArticleDescription").ToString)
                    dt1 = GetCostSheet(dr("ArticleId"), Val(dr("Quantity").ToString), dr("ArticleDescription").ToString)
                    dt1.AcceptChanges()
                    dtCostSheetAgainstsTicketItems.Merge(dt1)
                Next
                GetCostSheetAgainstTicketItems()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesOrder_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedValueChanged
        Try
            If Me.cmbSalesOrder.SelectedValue > 0 Then
                Dim SpecialInstructions As String = CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("SpecialInstructions").ToString
                Me.txtSpecialInstructions.Text = SpecialInstructions
                FillCombos("Plan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function Update_Record() As Boolean
        Try
            bal.Update_Transation(dt, master)
            SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
            msg_Information("Record has been updated successfully.")
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DisplayDetail(ByVal MasterID As Integer)
        Try
            Me.grdMaterialEstimation.DataSource = bal.GetDetails(MasterID)
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grdMaterialEstimation.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grdMaterialEstimation.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").HasValueList = True
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").LimitToList = True
           
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ExpandColumn = Me.grdMaterialEstimation.RootTable.Columns("Article code")
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentDataMember = "ArticleID"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            
            FillCombos("grdDepartments")
            'Me.grdMaterialEstimation.GroupByBoxVisible = True
            'planItemGroup.Collapse()
            FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetCostSheetItems(ByVal PlanItemId As Integer, ByVal quantity As Double, Optional Item As String = "") As DataTable
        Try
            Dim dt As New DataTable
            dt = bal.GetCostSheetItems(PlanItemId, quantity, PlanItemId, True, 0, 0)
            bal.Clone = False
            bal.dtAllItems = Nothing
            dt.AcceptChanges()
            If Not dt.Rows.Count > 0 Then
                '    Me.grdMaterialEstimation.DataSource = dt
                'Else
                msg_Error("No cost sheet is found against selected item " & Item & "")
                '    Me.grdMaterialEstimation.DataSource = Nothing
                '    Exit Sub
            End If
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            'Me.grdMaterialEstimation.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaterialEstimation.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaterialEstimation.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaterialEstimation.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            'Me.grdMaterialEstimation.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'planItemGroup.Collapse()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PlanCostSheetItems()
        Try
            'dtReturnMultipleCostSheets.Columns.Add("PendingEstQty", System.Type.GetType("System.Double"))
            Me.grdMaterialEstimation.DataSource = dtReturnMultipleCostSheets
            'Me.grdMaterialEstimation.ExpandRecords()
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grdMaterialEstimation.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").HasValueList = True
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").LimitToList = True
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo
            FillCombos("grdDepartments")
            FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function ReturnCostSheetTables(ByVal PlanItemId As Integer, ByVal quantity As Double, Optional Item As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'dt = bal.GetCostSheetItems(PlanItemId, quantity, PlanItemId, True, 0, 0)
            dt = bal.GetCostSheet(PlanItemId, quantity)
            bal.Clone = False
            bal.dtAllItems = Nothing
            dt.AcceptChanges()
            If Not dt.Rows.Count > 0 Then
                msg_Error("Cost sheet is not found against following item '" & Item & "'")
            End If
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            'Me.grdMaterialEstimation.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaterialEstimation.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'planItemGroup.Collapse()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DisplayDSDetail(ByVal ArticleId As Integer, ByVal PlanId As Integer)
        Dim ds As New DataSet
        Try
            ds = bal.GetDetailDS(ArticleId, PlanId)
            grdMaterialEstimation.SetDataBinding(ds, "")
            grdMaterialEstimation.Refresh()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If IsValidToDelete("ItemConsumptionDetail", "EstimationId", Me.grdSaved.CurrentRow.Cells("Id").Value.ToString) = True Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                bal.DeleteMaster(Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString))
                SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
                msg_Information("Record has been deleted successfully.")
                ReSetControls()
                GetAllRecords()
            Else
                msg_Error(str_ErrorDependentRecordFound)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            grdMaterialEstimation.UpdateData()
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
        Try
            FillCombos("SO")
            FillCombos("Plan")
            GetAllRecords()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMaterialEstimation_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMaterialEstimation.ColumnButtonClick
        Try
            If e.Column.Key = "Add" Then
                ''TASK TFS1438
                If grdMaterialEstimation.GetRow.Cells("Tag#").Value > 0 Then
                    msg_Error("Parent item is not allowed to be plus.")
                    Exit Sub
                End If
                ''TASK TFS1438
                If grdMaterialEstimation.GetRow.Cells("Types").Value.ToString <> "" Then
                    msg_Error("Minus or Plus type row is not allowed to be added or subtracted.")
                    Exit Sub
                Else

                    Dim frm As New frmDialog()
                    frm.RawMaterialId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString
                    frm.PlanItemId = grdMaterialEstimation.GetRow.Cells("ParentId").Value.ToString ''
                    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                    frm.IsAddition = True
                    frm.IsSubtraction = False
                    frm.IsMEPrior = False
                    frm.btnSave.Text = "Add"
                    frm.ShowDialog()
                End If
            ElseIf e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grdMaterialEstimation.GetRow.Delete()
                Me.grdMaterialEstimation.UpdateData()
            ElseIf e.Column.Key = "Sub" Then
                ''TASK TFS1438 
                If grdMaterialEstimation.GetRow.Cells("Tag#").Value > 0 Then
                    msg_Error("Parent item is not allowed to be minus.")
                    Exit Sub
                End If
                ''END TFS1438
                ''TASK:TFS1141 Validate minus quantity to not exceed estimaion quantity or pending estimation quantity.
                If grdMaterialEstimation.GetRow.Cells("Types").Value.ToString <> "" Then
                    msg_Error("Minus or Plus type row is not allowed to be added or subtracted.")
                    Exit Sub
                Else
                    '' To get sum of minus quantity
                    Dim gridFilter As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value)
                    Dim gridFilter1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value)
                    Dim gridFilter2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("Types"), Janus.Windows.GridEX.ConditionOperator.Equal, "Minus")

                    gridFilter.AddCondition(gridFilter1)
                    gridFilter.AddCondition(gridFilter2)
                    Dim TotalMinusQty As Double = 0
                    TotalMinusQty = Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, gridFilter)

                    '' End minus section
                    '' To get sum of quantity
                    Dim gridFilterP As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value)
                    Dim gridFilterP1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value)
                    Dim gridFilterP2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("Types"), Janus.Windows.GridEX.ConditionOperator.NotEqual, "Minus")

                    gridFilterP.AddCondition(gridFilterP1)
                    gridFilterP.AddCondition(gridFilterP2)
                    Dim TotalQty As Double = 0
                    TotalQty = Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, gridFilterP)

                    '' End minus section
                    '' To get sumed estimation quantity
                    Dim gridFilterPE As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value)
                    Dim gridFilterPE1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("MaterialEstMasterID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value)
                    Dim gridFilterPE2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value)
                    Dim gridFilterPE3 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("Types"), Janus.Windows.GridEX.ConditionOperator.Equal, "Minus")
                    ''TASK TFS1438
                    Dim gridFilterPE4 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value)
                    gridFilterPE.AddCondition(gridFilterPE1)
                    gridFilterPE.AddCondition(gridFilterPE2)
                    gridFilterPE.AddCondition(gridFilterPE3)
                    gridFilterPE.AddCondition(gridFilterPE4)
                    ''TASK TFS1438
                    Dim TotalMinusEstQty As Double = 0
                    TotalMinusEstQty = Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, gridFilterPE)
                    '' End minus section

                    '' To get sumed estimation plus type quantity
                    Dim gridFilterPlus As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value)
                    Dim gridFilterPlus1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("MaterialEstMasterID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value)
                    Dim gridFilterPlus2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value)
                    Dim gridFilterPlus3 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("Types"), Janus.Windows.GridEX.ConditionOperator.Equal, "Plus")
                    ''TASK TFS1438
                    Dim gridFilterPlus4 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value)

                    gridFilterPlus.AddCondition(gridFilterPlus1)
                    gridFilterPlus.AddCondition(gridFilterPlus2)
                    gridFilterPlus.AddCondition(gridFilterPlus3)
                    gridFilterPlus.AddCondition(gridFilterPlus4)
                    ''END TASK TFS1438
                    Dim TotalPlusEstQty As Double = 0
                    TotalPlusEstQty = Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, gridFilterPlus)
                    '' To get sumed estimation plus type quantity

                    '' To get sumed estimation plus type quantity
                    Dim gridFilterNoType As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value)
                    Dim gridFilterNoType1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("MaterialEstMasterID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value)
                    Dim gridFilterNoType2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value)
                    Dim gridFilterNoType3 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("Types"), Janus.Windows.GridEX.ConditionOperator.Equal, "")
                    ''TASK TFS1438
                    Dim gridFilterNoType4 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value)

                    gridFilterNoType.AddCondition(gridFilterNoType1)
                    gridFilterNoType.AddCondition(gridFilterNoType2)
                    gridFilterNoType.AddCondition(gridFilterNoType3)
                    gridFilterNoType.AddCondition(gridFilterNoType4)
                    ''END TASK TFS1438
                    Dim TotalNoTypeQty As Double = 0
                    TotalNoTypeQty = Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, gridFilterNoType)
                    '' To get sumed estimation plus type quantity

                    'Dim PendingEstQty As Double = (TotalNoTypeQty - bal.GetIssuedAgainstEstimation(Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value))
                    ''TASK TFS1272 Getting consumed quantity instead of issuance quantity.
                    'Dim PendingEstQty As Double = (TotalNoTypeQty - bal.GetConsumedAgainstEstimation(Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value))
                    Dim PendingEstQty As Double = (bal.GetConsumedAgainstEstimation(Me.grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value, Me.grdMaterialEstimation.GetRow.Cells("SubDepartmentId").Value, Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value, Me.grdMaterialEstimation.GetRow.Cells("ParentTag#").Value))

                    ''TASK:TFS1141

                    Dim frm As New frmDialog()

                    frm.RawMaterialId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString
                    frm.PlanItemId = grdMaterialEstimation.GetRow.Cells("ParentId").Value.ToString ''
                    'frm.CheckQty = Val(grdMaterialEstimation.GetRow.Cells("Qty").Value.ToString) ''
                    frm.TotalPlusQty = TotalQty ''
                    frm.TotalMinusQty = TotalMinusQty ''
                    frm.TotalMinusEstQty = TotalMinusEstQty
                    frm.PendingEstQty = PendingEstQty
                    frm.TotalPlusEstQty = TotalPlusEstQty
                    frm.TotalNoTypeQty = TotalNoTypeQty
                    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                    frm.IsSubtraction = True
                    frm.IsAddition = False
                    frm.IsMEPrior = False
                    frm.btnSave.Text = "Sub"
                    frm.ShowDialog()
                End If
            ElseIf e.Column.Key = "Approve" Then
                If Me.grdMaterialEstimation.GetRow.Cells("Approve").Value = True Then
                    msg_Error("This document has already been approved")
                    Exit Sub
                End If
                If LoginGroup = "Administrator" Then
                    If msg_Confirm("Do you want to approve") = True Then
                        Approve(Val(grdMaterialEstimation.GetRow.Cells("ParentId").Value.ToString), Val(grdMaterialEstimation.GetRow.Cells("ArticleId").Value.ToString), True)
                        DisplayDetail(ID)
                    End If
                Else
                    msg_Error("You don't have rights to approve it")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub AddToGrid(ByVal Qty As Double)
        Dim dt As New DataTable
        Dim ArticleId As String = ""
        Dim IncrementalId As Integer = 0
        Dim NewArticleId As String = ""
        Dim TagId As Integer = 0
        Try
            'dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            'If grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.Contains("-") Then
            '    Dim AIndex As Integer = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.IndexOf("-")
            '    ArticleId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.Substring(0, AIndex)
            '    Dim drRows() As DataRow = dt.Select("ArticleId LIKE '" & ArticleId & "%'")
            '    Dim IncrementIds As DataTable = drRows.CopyToDataTable
            '    Dim IncrementalIds As New List(Of String)
            '    For Each IncrementId As DataRow In IncrementIds.Rows
            '        Dim AIndex1 As Integer = IncrementId("ArticleID").ToString.IndexOf("-")
            '        Dim Id As String = IncrementId("ArticleID").ToString.Substring(AIndex + 1)
            '        IncrementalIds.Add(Id)
            '    Next
            '    IncrementalId = IncrementalIds(IncrementalIds.Count - 1)
            '    IncrementalId += 1
            '    NewArticleId = ArticleId & "-" & IncrementalId.ToString
            'Else
            '    IncrementalId = 1
            '    ArticleId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString
            '    NewArticleId = ArticleId & "-" & IncrementalId.ToString
            'End If
            'Dim dr As DataRow
            'dr = dt.NewRow
            'dr.Item("MasterArticleID") = 0
            'dr.Item("ArticleID") = NewArticleId
            'dr.Item("ParentId") = grdMaterialEstimation.GetRow.Cells("ParentId").Value
            'dr.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value
            'dr.Item("ArticleDescription") = grdMaterialEstimation.GetRow.Cells("ArticleDescription").Value
            'dr.Item("TotalQty") = Val(Qty)
            'dr.Item("Qty") = Val(Qty)
            'dr.Item("Types") = "Plus"
            'dr.Item("Approve") = False
            'dr.Item("CostSheetID") = grdMaterialEstimation.GetRow.Cells("CostSheetID").Value
            'dr.Item("ArticleUnitId") = grdMaterialEstimation.GetRow.Cells("ArticleUnitId").Value
            'dr.Item("Unit") = grdMaterialEstimation.GetRow.Cells("Unit").Value.ToString ''SubDepartmentID
            'dr.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
            'dt.Rows.InsertAt(dr, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            ' ''
            'Dim ChildRows() As Janus.Windows.GridEX.GridEXRow = Me.grdMaterialEstimation.GetRow.GetChildRows
            'Dim dtCostSheet As DataTable = bal.GetCostSheetItems(ArticleId, Qty, NewArticleId.ToString, False, 0, 0)
            dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            '' TASK:1006 Added TagIds to estimation.
            Dim grdTagNo As Integer = Convert.ToInt32(Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Tag#"), Janus.Windows.GridEX.AggregateFunction.Max).ToString)
            Dim dtTagMax As Integer = GetTagMax()
            If grdTagNo > dtTagMax Then
                TagId = grdTagNo
            Else
                TagId = dtTagMax
            End If
            ''END TASK:1006 Added TagIds to estimation.
            'Dim dtCostSheet As DataTable = bal.GetCostSheetPlus(grdMaterialEstimation.GetRow.Cells("ArticleID").Value, Qty, Val(grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value.ToString), dt.Rows.Count, grdMaterialEstimation.GetRow.Cells("ParentId").Value)
            Dim dtCostSheet As DataTable = bal.GetCostSheet(grdMaterialEstimation.GetRow.Cells("ArticleID").Value, Qty, Val(grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value.ToString), dt.Rows.Count, grdMaterialEstimation.GetRow.Cells("ParentId").Value, TagId, grdMaterialEstimation.GetRow.Cells("ParentTag#").Value, grdMaterialEstimation.GetRow.Cells("Price").Value)
            If dtCostSheet.Rows.Count > 0 Then
                For Each Row As DataRow In dtCostSheet.Rows
                    Dim dr1 As DataRow
                    dr1 = dt.NewRow
                    'dr1.Item("MasterArticleID") = Row.Item("MasterArticleID") ' Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                    dr1.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)
                    dr1.Item("ArticleID") = Row.Item("ArticleId")
                    dr1.Item("ParentId") = Row.Item("ParentId").ToString
                    'dr1.Item("PlanItem") = Row.Item("PlanItem").ToString
                    dr1.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString

                    dr1.Item("ArticleDescription") = Row.Item("ArticleDescription").ToString
                    dr1.Item("TotalQty") = Val(Row.Item("TotalQty").ToString)
                    dr1.Item("Qty") = Val(Row.Item("Qty").ToString)
                    dr1.Item("Price") = Val(Row.Item("Price").ToString)
                    dr1.Item("Types") = "Plus"
                    dr1.Item("Approve") = False
                    dr1.Item("CostSheetID") = Row.Item("CostSheetID")
                    dr1.Item("ArticleUnitId") = Row.Item("ArticleUnitId")
                    dr1.Item("Unit") = Row.Item("Unit").ToString ''SubDepartmentID
                    'dr1.Item("SubDepartmentID") = Row.Item("SubDepartmentID")
                    dr1.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
                    dr1.Item("SubDepartment") = grdMaterialEstimation.GetRow.Cells("SubDepartment").Value.ToString
                    dr1.Item("UniqueId") = Row.Item("UniqueId") ''SubDepartmentID
                    dr1.Item("ParentUniqueId") = Row.Item("ParentUniqueId")
                    dr1.Item("Tag#") = Row.Item("Tag#") ''SubDepartmentID
                    dr1.Item("ParentTag#") = Row.Item("ParentTag#")
                    dr1.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                    dt.Rows.Add(dr1)
                Next
            Else
                Dim dt1 As DataTable
                dt1 = CType(grdMaterialEstimation.DataSource, DataTable)
                Dim rowTable As DataRow
                rowTable = dt.NewRow
                rowTable.Item("ArticleID") = grdMaterialEstimation.GetRow.Cells("ArticleID").Value
                'rowTable.Item("MasterArticleID") = grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value
                rowTable.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)

                rowTable.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString
                rowTable.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
                rowTable.Item("SubDepartment") = grdMaterialEstimation.GetRow.Cells("SubDepartment").Value.ToString
                rowTable.Item("Qty") = Qty
                rowTable.Item("TotalQty") = Qty
                rowTable.Item("ArticleDescription") = grdMaterialEstimation.GetRow.Cells("ArticleDescription").Value.ToString
                rowTable.Item("Unit") = grdMaterialEstimation.GetRow.Cells("Unit").Value.ToString
                rowTable.Item("ArticleUnitId") = Val(grdMaterialEstimation.GetRow.Cells("ArticleUnitId").Value.ToString) ''Val(Me.cmbProduct.SelectedRow.Cells("Id").Value.ToString)
                rowTable.Item("PlanTicketsId") = 0
                rowTable.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                'If Val(Qty) < 0 Then
                '    rowTable.Item("Types") = "Minus"
                'Else
                rowTable.Item("Types") = "Plus"
                'End If
                rowTable.Item("CostSheetID") = 0
                rowTable.Item("Approve") = 0
                rowTable.Item("Tag#") = 0
                ''TASK:1006 Added TagIds to estimation.
                rowTable.Item("ParentTag#") = grdMaterialEstimation.GetRow.Cells("ParentTag#").Value
                '' END TASK:1006 Added TagIds to estimation.
                rowTable.Item("Price") = 0
                rowTable.Item("UniqueId") = (dt.Rows.Count + 1)
                rowTable.Item("ParentUniqueId") = grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value
                rowTable.Item("ParentId") = grdMaterialEstimation.GetRow.Cells("ParentId").Value
                'If Not Me.cmbParentItem.ActiveRow Is Nothing Then
                '    If Me.cmbParentItem.Value > 0 Then
                '        rowTable.Item("ParentId") = Me.cmbParentItem.Value
                '    End If
                'End If
                'If Me.cmbParentItem.ActiveRow Is Nothing Or Me.cmbParentItem.Value = 0 Then
                '    rowTable.Item("ParentId") = 0
                'End If

                'dt1.Rows.Add(rowTable)
                dt1.Rows.InsertAt(rowTable, Me.grdMaterialEstimation.GetRow.RowIndex + 1)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SubToGrid(ByVal Qty As Double)
        Dim dt As New DataTable
        Dim ArticleId As String = ""
        Dim IncrementalId As Integer = 0
        Dim NewArticleId As String = ""
        Dim TagId As Integer = 0
        Try
            'dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            'If grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.Contains("-") Then
            '    Dim AIndex As Integer = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.IndexOf("-")
            '    ArticleId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString.Substring(0, AIndex)
            '    Dim drRows() As DataRow = dt.Select("ArticleId LIKE '" & ArticleId & "%'")
            '    Dim IncrementIds As DataTable = drRows.CopyToDataTable
            '    Dim IncrementalIds As New List(Of String)
            '    For Each IncrementId As DataRow In IncrementIds.Rows
            '        Dim AIndex1 As Integer = IncrementId("ArticleID").ToString.IndexOf("-")
            '        Dim Id As String = IncrementId("ArticleID").ToString.Substring(AIndex + 1)
            '        IncrementalIds.Add(Id)
            '    Next
            '    IncrementalId = IncrementalIds(IncrementalIds.Count - 1)
            '    IncrementalId += 1
            '    NewArticleId = ArticleId & "-" & IncrementalId.ToString
            'Else
            '    IncrementalId = 1
            '    ArticleId = grdMaterialEstimation.GetRow.Cells("ArticleID").Value.ToString
            '    NewArticleId = ArticleId & "-" & IncrementalId.ToString
            'End If
            'Dim dr As DataRow
            'dr = dt.NewRow
            'dr.Item("MasterArticleID") = 0
            'dr.Item("ArticleID") = NewArticleId
            'dr.Item("ParentId") = grdMaterialEstimation.GetRow.Cells("ParentId").Value
            'dr.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value
            'dr.Item("ArticleDescription") = grdMaterialEstimation.GetRow.Cells("ArticleDescription").Value
            'dr.Item("TotalQty") = Val(Qty)
            'dr.Item("Qty") = Val(Qty)
            'dr.Item("Types") = "Plus"
            'dr.Item("Approve") = False
            'dr.Item("CostSheetID") = grdMaterialEstimation.GetRow.Cells("CostSheetID").Value
            'dr.Item("ArticleUnitId") = grdMaterialEstimation.GetRow.Cells("ArticleUnitId").Value
            'dr.Item("Unit") = grdMaterialEstimation.GetRow.Cells("Unit").Value.ToString ''SubDepartmentID
            'dr.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
            'dt.Rows.InsertAt(dr, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            ' ''
            'Dim ChildRows() As Janus.Windows.GridEX.GridEXRow = Me.grdMaterialEstimation.GetRow.GetChildRows
            'Dim dtCostSheet As DataTable = bal.GetCostSheetItems(ArticleId, Qty, NewArticleId.ToString, False, 0, 0)
            dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            ''TASK:1006 Added TagIds to estimation.
            Dim grdTagNo As Integer = Convert.ToInt32(Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Tag#"), Janus.Windows.GridEX.AggregateFunction.Max).ToString)
            Dim dtTagMax As Integer = GetTagMax()
            If grdTagNo > dtTagMax Then
                TagId = grdTagNo
            Else
                TagId = dtTagMax
            End If
            '' END TASK:1006 Added TagIds to estimation.
            'Dim dtCostSheet As DataTable = bal.GetCostSheetPlus(grdMaterialEstimation.GetRow.Cells("ArticleID").Value, Qty, Val(grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value.ToString), dt.Rows.Count, grdMaterialEstimation.GetRow.Cells("ParentId").Value)
            Dim dtCostSheet As DataTable = bal.GetCostSheet(grdMaterialEstimation.GetRow.Cells("ArticleID").Value, Qty, Val(grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value.ToString), dt.Rows.Count, grdMaterialEstimation.GetRow.Cells("ParentId").Value, TagId, grdMaterialEstimation.GetRow.Cells("ParentTag#").Value, grdMaterialEstimation.GetRow.Cells("Price").Value)
            If dtCostSheet.Rows.Count > 0 Then
                For Each Row As DataRow In dtCostSheet.Rows
                    Dim dr1 As DataRow
                    dr1 = dt.NewRow
                    'dr1.Item("MasterArticleID") = Row.Item("MasterArticleID")
                    dr1.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)

                    dr1.Item("ArticleID") = Row.Item("ArticleId")
                    dr1.Item("ParentId") = Row.Item("ParentId").ToString
                    'dr1.Item("PlanItem") = Row.Item("PlanItem").ToString
                    dr1.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString

                    dr1.Item("ArticleDescription") = Row.Item("ArticleDescription").ToString
                    dr1.Item("TotalQty") = Val(Row.Item("TotalQty").ToString)
                    dr1.Item("Qty") = Val(Row.Item("Qty").ToString)
                    dr1.Item("Price") = Val(Row.Item("Price").ToString)
                    dr1.Item("Types") = "Minus"
                    dr1.Item("Approve") = False
                    dr1.Item("CostSheetID") = Row.Item("CostSheetID")
                    dr1.Item("ArticleUnitId") = Row.Item("ArticleUnitId")
                    dr1.Item("Unit") = Row.Item("Unit").ToString ''SubDepartmentID
                    'dr1.Item("SubDepartmentID") = Row.Item("SubDepartmentID")
                    dr1.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
                    dr1.Item("SubDepartment") = grdMaterialEstimation.GetRow.Cells("SubDepartment").Value.ToString
                    dr1.Item("UniqueId") = Row.Item("UniqueId") ''SubDepartmentID
                    dr1.Item("ParentUniqueId") = Row.Item("ParentUniqueId")
                    ''TASK:1006 Added TagIds to estimation.
                    dr1.Item("Tag#") = Row.Item("Tag#") ''SubDepartmentID
                    dr1.Item("ParentTag#") = Row.Item("ParentTag#")
                    dr1.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                    '' END TASK:1006 Added TagIds to estimation.
                    dt.Rows.Add(dr1)
                Next
            Else
                Dim dt1 As DataTable
                dt1 = CType(grdMaterialEstimation.DataSource, DataTable)
                Dim rowTable As DataRow
                rowTable = dt.NewRow
                rowTable.Item("ArticleID") = grdMaterialEstimation.GetRow.Cells("ArticleID").Value
                'rowTable.Item("MasterArticleID") = grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value
                rowTable.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)

                'rowTable.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString
                rowTable.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString

                rowTable.Item("SubDepartmentID") = grdMaterialEstimation.GetRow.Cells("SubDepartmentID").Value
                rowTable.Item("SubDepartment") = grdMaterialEstimation.GetRow.Cells("SubDepartment").Value.ToString
                rowTable.Item("Qty") = Qty
                rowTable.Item("TotalQty") = Qty
                rowTable.Item("ArticleDescription") = grdMaterialEstimation.GetRow.Cells("ArticleDescription").Value.ToString
                rowTable.Item("Unit") = grdMaterialEstimation.GetRow.Cells("Unit").Value.ToString
                rowTable.Item("ArticleUnitId") = Val(grdMaterialEstimation.GetRow.Cells("ArticleUnitId").Value.ToString) ''Val(Me.cmbProduct.SelectedRow.Cells("Id").Value.ToString)
                rowTable.Item("PlanTicketsId") = 0
                rowTable.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                'If Val(Qty) < 0 Then
                '    rowTable.Item("Types") = "Minus"
                'Else
                rowTable.Item("Types") = "Minus"
                'End If
                rowTable.Item("CostSheetID") = 0
                rowTable.Item("Approve") = 0
                ''TASK:1006 Added TagIds to estimation.
                rowTable.Item("Tag#") = 0
                rowTable.Item("ParentTag#") = grdMaterialEstimation.GetRow.Cells("ParentTag#").Value
                '' END TASK:1006 Added TagIds to estimation.
                rowTable.Item("Price") = 0
                rowTable.Item("UniqueId") = (dt.Rows.Count + 1)
                rowTable.Item("ParentUniqueId") = grdMaterialEstimation.GetRow.Cells("ParentUniqueId").Value
                rowTable.Item("ParentId") = grdMaterialEstimation.GetRow.Cells("ParentId").Value
                'If Not Me.cmbParentItem.ActiveRow Is Nothing Then
                '    If Me.cmbParentItem.Value > 0 Then
                '        rowTable.Item("ParentId") = Me.cmbParentItem.Value
                '    End If
                'End If
                'If Me.cmbParentItem.ActiveRow Is Nothing Or Me.cmbParentItem.Value = 0 Then
                '    rowTable.Item("ParentId") = 0
                'End If

                'dt1.Rows.Add(rowTable)
                dt1.Rows.InsertAt(rowTable, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SubtractFromGrid(ByVal Qty As Double, ByVal PlanItemId As Integer, ByVal ProductId As Integer)
        Try
            If Qty < Me.grdMaterialEstimation.GetRow.Cells("TotalQty").Value Then
                Me.grdMaterialEstimation.GetRow.Cells("TotalQty").Value -= Qty
                Me.grdMaterialEstimation.GetRow.Cells("Qty").Value -= Qty
                Me.grdMaterialEstimation.GetRow.Cells("Types").Value = "Minus"
            Else
                msg_Error("Subtraction quantity is larger than available quantity.")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grdMaterialEstimation.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.txtSpecialInstructions.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.GetRow.Cells("EstimationDate").Value
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("SaleOrderId").Value) Then
                Me.cmbSalesOrder.SelectedValue = Me.grdSaved.GetRow.Cells("SaleOrderId").Value
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("MasterPlanId").Value) Then
                Me.cmbPlan.SelectedValue = Me.grdSaved.GetRow.Cells("MasterPlanId").Value
            End If
            FillCombos("Tickets")
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("PlanTicketId").Value) Then
                Me.cmbTicket.Value = Me.grdSaved.GetRow.Cells("PlanTicketId").Value
            End If
            ID = Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString)
            DisplayDetail(ID)
            Me.cmbSalesOrder.Enabled = False
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
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
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmMaterialEstimation)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'PrintToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString  ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                Me.btnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = False
                If Rights Is Nothing Then Exit Sub
                For Each RightstDt As GroupRights In Rights
                    If RightstDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightstDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightstDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Approve" Then
                        Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightstDt.FormControlName = "Post" Then

                    ElseIf RightstDt.FormControlName = "Field Chooser" Then
                        'CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightstDt.FormControlName = "Attachment" Then
                        'Me.btnAttachment.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Approve(ByVal PlanItemId As Integer, ByVal ProductId As Integer, ByVal Approve As Boolean)
        Dim connection As New OleDb.OleDbConnection
        Dim command As New OleDb.OleDbCommand
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim transaction As OleDb.OleDbTransaction = connection.BeginTransaction
        Try
            command.CommandType = CommandType.Text
            command.Connection = connection
            command.Transaction = transaction

            command.CommandText = "Update MaterialEstimationDetailTable Set Approve = " & IIf(Approve = True, 1, 0) & " Where PlanItemId = " & PlanItemId & " And ProductId =" & ProductId & ""
            command.ExecuteNonQuery()
            transaction.Commit()
        Catch ex As Exception
            transaction.Rollback()
            Throw ex
        End Try
    End Sub

    Private Sub grdMaterialEstimation_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdMaterialEstimation.FormattingRow
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Types As String = e.Row.Cells("Types").Value.ToString()
                If Types = "Plus" Then
                    Dim Rowcol As New Janus.Windows.GridEX.GridEXFormatStyle
                    Rowcol.BackColor = Color.Red
                    e.Row.RowStyle = Rowcol
                ElseIf Types = "Minus" Then
                    Dim Rowcol As New Janus.Windows.GridEX.GridEXFormatStyle
                    Rowcol.BackColor = Color.LightGreen
                    e.Row.RowStyle = Rowcol
                ElseIf Types = "Addition" Then
                    Dim Rowcol As New Janus.Windows.GridEX.GridEXFormatStyle
                    Rowcol.BackColor = Color.LightPink
                    e.Row.RowStyle = Rowcol
                End If
            End If
            If IsEditMode = False Then
                Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = False
                Exit Sub
            Else
                Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
            End If
            If e.Row.Cells("Approve").Value Is Nothing Then Exit Sub

            Dim s As String = e.Row.Cells("Approve").Value.ToString()
            If s = "True" Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    'Dim Cellcol As New Janus.Windows.GridEX.
                    Dim Rowcol As New Janus.Windows.GridEX.GridEXFormatStyle

                    Rowcol.BackColor = Color.YellowGreen
                    'e.Row.RowStyle = Rowcol
                    e.Row.Cells("Approve").FormatStyle = Rowcol
                    e.Row.RowStyle = Rowcol
                End If
                e.Row.Cells("Approve").Text = "Approved"
            Else
                Dim Rowcol1 As New Janus.Windows.GridEX.GridEXFormatStyle
                'Rowcol1.BackColor = Color.Indigo
                e.Row.Cells("Approve").FormatStyle = Rowcol1
                e.Row.Cells("Approve").Text = "Approve"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    Try
    '        ShowReport("rptMaterialEstimation")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

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

    Private Sub lblLinkSOLoad_Click(sender As Object, e As EventArgs) Handles lblLinkSOLoad.Click
        Dim dt As DataTable
        Try
            If Me.cmbSalesOrder.SelectedValue > 0 Then
                GetSalesOrderItems(Me.cmbSalesOrder.SelectedValue)
                If dtCriteriaWiseCostSheet.Rows.Count > 0 Then
                    dtReturnMultipleCostSheets = New DataTable
                    For Each dr As DataRow In dtCriteriaWiseCostSheet.Rows
                        dt = New DataTable
                        dt = ReturnCostSheetTables(Val(dr.Item("ArticleDefId").ToString), Val(dr.Item("Qty").ToString))
                        dt.AcceptChanges()
                        dtReturnMultipleCostSheets.Merge(dt)
                    Next
                    PlanCostSheetItems()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblLinkPlanLoad_Click(sender As Object, e As EventArgs) Handles lblLinkPlanLoad.Click
        Dim dt As DataTable
        Try
            If cmbPlan.SelectedValue > 0 Then
                GetPlanItems(Me.cmbPlan.SelectedValue)
                If dtCriteriaWiseCostSheet.Rows.Count > 0 Then
                    dtReturnMultipleCostSheets = New DataTable
                    For Each dr As DataRow In dtCriteriaWiseCostSheet.Rows
                        dt = New DataTable
                        dt = ReturnCostSheetTables(Val(dr.Item("ArticleDefId").ToString), Val(dr.Item("Qty").ToString), dr.Item("Item").ToString)
                        dt.AcceptChanges()
                        dtReturnMultipleCostSheets.Merge(dt)
                    Next
                    PlanCostSheetItems()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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
    Private Sub GetCostSheetAgainstTicketItems()
        Try
            Dim dtAddRows As DataTable
            Dim dtIteration As DataTable
            dtAddRows = dtCostSheetAgainstsTicketItems.Clone
            dtIteration = dtCostSheetAgainstsTicketItems
            'If dtIteration.Rows.Count > 0 Then
            '    For Each Row As DataRow In dtIteration.Rows
            '        If Row("TotalQty") > 1 Then
            '            Dim TotalQty As Double = Row("TotalQty")
            '            Dim RepeatedIdsQty As Double = 0
            '            If Row("ParentQty") > 1 Then
            '                RepeatedIdsQty = Row("TotalQty") / Row("ParentQty")
            '            End If
            '            For i As Integer = 1 To TotalQty
            '                'If i > 1 Then
            '                Dim dr As DataRow
            '                dr = dtAddRows.NewRow
            '                dr = Row
            '                dr.BeginEdit()
            '                If Not RepeatedIdsQty = 0 Then
            '                    If i > RepeatedIdsQty Then
            '                        Dim ResetId As Integer = i - RepeatedIdsQty
            '                        'For j As Integer = 1 To RepeatedIdsQty
            '                        Dim AIndex As Integer = dr("ArticleId").ToString.IndexOf("-")
            '                        Dim AHyphen As String = dr("ArticleId").ToString.Substring(0, AIndex)
            '                        dr("ArticleId") = AHyphen & "-" & ResetId.ToString
            '                        Dim PIndex As Integer = dr("ParentId").ToString.IndexOf("-")
            '                        Dim PHyphen As String = dr("ParentId").ToString.Substring(0, PIndex)
            '                        dr("ParentId") = PHyphen & "-" & ResetId.ToString
            '                        'dr("ArticleId") = dr("ArticleId")
            '                        'dr("ParentId") = dr("ParentId")
            '                        dr("Qty") = 1
            '                        dr("TotalQty") = 1
            '                        'Next
            '                    Else
            '                        If dr("ArticleId").ToString.Contains("-") Then
            '                            Dim AIndex As Integer = dr("ArticleId").ToString.IndexOf("-")
            '                            Dim AHyphen As String = dr("ArticleId").ToString.Substring(0, AIndex)
            '                            dr("ArticleId") = AHyphen & "-" & i.ToString
            '                            dr("Qty") = 1
            '                            dr("TotalQty") = 1
            '                            If Val(dr("ParentId").ToString) <> 0 Then
            '                                Dim PIndex As Integer = dr("ParentId").ToString.IndexOf("-")
            '                                Dim PHyphen As String = dr("ParentId").ToString.Substring(0, PIndex)
            '                                dr("ParentId") = PHyphen & "-" & i.ToString
            '                            End If
            '                        Else
            '                            dr("ArticleId") = dr("ArticleId").ToString & "-" & i.ToString
            '                            dr("Qty") = 1
            '                            dr("TotalQty") = 1
            '                            If Val(dr("ParentId").ToString) <> 0 Then
            '                                dr("ParentId") = dr("ParentId").ToString & "-" & i.ToString
            '                            End If
            '                        End If
            '                    End If
            '                Else
            '                    If dr("ArticleId").ToString.Contains("-") Then
            '                        Dim AIndex As Integer = dr("ArticleId").ToString.IndexOf("-")
            '                        Dim AHyphen As String = dr("ArticleId").ToString.Substring(0, AIndex)
            '                        dr("ArticleId") = AHyphen & "-" & i.ToString
            '                        dr("Qty") = 1
            '                        dr("TotalQty") = 1
            '                        If Val(dr("ParentId").ToString) <> 0 Then
            '                            Dim PIndex As Integer = dr("ParentId").ToString.IndexOf("-")
            '                            Dim PHyphen As String = dr("ParentId").ToString.Substring(0, PIndex)
            '                            dr("ParentId") = PHyphen & "-" & i.ToString
            '                        End If
            '                    Else
            '                        dr("ArticleId") = dr("ArticleId").ToString & "-" & i.ToString
            '                        dr("Qty") = 1
            '                        dr("TotalQty") = 1
            '                        If Val(dr("ParentId").ToString) <> 0 Then
            '                            dr("ParentId") = dr("ParentId").ToString & "-" & i.ToString
            '                        End If
            '                    End If
            '                End If
            '                dr.EndEdit()
            '                dr.AcceptChanges()
            '                dtAddRows.ImportRow(dr)
            '                'End If
            '            Next
            '        End If
            '    Next
            'dtCostSheetAgainstsTicketItems = dtAddRows
            'dtCostSheetAgainstsTicketItems.Columns.Add("PendingEstQty", System.Type.GetType("System.Double"))
            dtCostSheetAgainstsTicketItems.AcceptChanges()
            Me.grdMaterialEstimation.DataSource = dtCostSheetAgainstsTicketItems
            'Else
            '    msg_Error("No cost sheet is found against selected item")
            '    Me.grdMaterialEstimation.DataSource = Nothing
            '    Exit Sub
            'End If
            'Me.grdMaterialEstimation.RootTable.Groups.Clear()
            'Me.grdMaterialEstimation.AutomaticSort = False
            'Dim planItemGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMaterialEstimation.RootTable.Columns("PlanItem"))
            'planItemGroup.GroupPrefix = String.Empty
            'grdMaterialEstimation.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            'Me.grdMaterialEstimation.RootTable.Groups.Add(planItemGroup)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdMaterialEstimation.RootTable.Columns("Types").EditType = Janus.Windows.GridEX.EditType.NoEdit ''"Types"
            Me.grdMaterialEstimation.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").HasValueList = True
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").LimitToList = True
            Me.grdMaterialEstimation.RootTable.Columns("SubDepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo

           
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ExpandColumn = Me.grdMaterialEstimation.RootTable.Columns("Article code")
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentDataMember = "ArticleId"
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.grdMaterialEstimation.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            FillCombos("grdDepartments")
            FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            If Validate_AddToGrid() = False Then Exit Sub
            'Dim Serial As String = Me.grdMaterialEstimation.GetRows.GetLength(0)

            'RefreshDetailControls()

            Dim TagId As Integer = 0
            Dim ParentTagId As Integer = 0
            Dim ParentId As Integer = 0
            Dim ParentUniqueId As Integer = 0
            If Not Me.cmbParentItem.ActiveRow Is Nothing Then
                If Me.cmbParentItem.Value > 0 Then
                    ParentId = grdMaterialEstimation.GetRow.Cells("ParentId").Value
                    ParentUniqueId = Val(grdMaterialEstimation.GetRow.Cells("UniqueId").Value.ToString)
                    ParentTagId = Val(grdMaterialEstimation.GetRow.Cells("Tag#").Value.ToString)
                Else
                    ParentId = 0
                    ParentUniqueId = 0
                    ParentTagId = 0
                End If
            Else
                ParentId = 0
                ParentUniqueId = 0
                ParentTagId = 0
            End If
            'If Me.cmbParentItem.ActiveRow Is Nothing Or Me.cmbParentItem.Value = 0 Then
            '    rowTable.Item("ParentId") = 0
            'End If
            ''TASK:1006 Added TagIds to estimation.
            Dim grdTagNo As Integer = Convert.ToInt32(Me.grdMaterialEstimation.GetTotal(Me.grdMaterialEstimation.RootTable.Columns("Tag#"), Janus.Windows.GridEX.AggregateFunction.Max).ToString)
            Dim dtTagMax As Integer = GetTagMax()
            If grdTagNo > dtTagMax Then
                TagId = grdTagNo
            Else
                TagId = dtTagMax
            End If
            '' END TASK:1006 Added TagIds to estimation.
            dt = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            Dim dtCostSheet As DataTable = bal.GetCostSheet(cmbProduct.Value, Val(txtQty.Text), ParentUniqueId, dt.Rows.Count, ParentId, TagId, ParentTagId)
            If dtCostSheet.Rows.Count > 0 Then
                For Each Row As DataRow In dtCostSheet.Rows
                    Dim dr1 As DataRow
                    dr1 = dt.NewRow
                    'dr1.Item("MasterArticleID") = Row.Item("MasterArticleID")
                    dr1.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)
                    dr1.Item("ArticleID") = Row.Item("ArticleId")
                    dr1.Item("ParentId") = Row.Item("ParentId").ToString
                    'dr1.Item("PlanItem") = Row.Item("PlanItem").ToString
                    dr1.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString
                    dr1.Item("ArticleDescription") = Row.Item("ArticleDescription").ToString
                    dr1.Item("TotalQty") = Val(Row.Item("TotalQty").ToString)
                    dr1.Item("Qty") = Val(Row.Item("Qty").ToString)
                    dr1.Item("Price") = Val(Row.Item("Price").ToString)
                    If Val(Row.Item("Qty").ToString) < 0 Then
                        dr1.Item("Types") = "Minus"
                    Else
                        dr1.Item("Types") = "Plus"
                    End If
                    dr1.Item("Approve") = False
                    dr1.Item("CostSheetID") = Row.Item("CostSheetID")
                    dr1.Item("ArticleUnitId") = Row.Item("ArticleUnitId")
                    dr1.Item("Unit") = Row.Item("Unit").ToString ''SubDepartmentID
                    dr1.Item("SubDepartmentID") = Row.Item("SubDepartmentID")
                    dr1.Item("UniqueId") = Row.Item("UniqueId") ''SubDepartmentID
                    dr1.Item("ParentUniqueId") = Row.Item("ParentUniqueId")
                    ''TASK:1006 Added TagIds to estimation.
                    dr1.Item("Tag#") = Row.Item("Tag#") ''SubDepartmentID
                    dr1.Item("ParentTag#") = Row.Item("ParentTag#")
                    dr1.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                    '' END TASK:1006 Added TagIds to estimation.
                    dt.Rows.Add(dr1)
                Next
            Else
                Dim dt As DataTable
                dt = CType(grdMaterialEstimation.DataSource, DataTable)
                Dim rowTable As DataRow
                rowTable = dt.NewRow
                rowTable.Item("ArticleID") = cmbProduct.Value
                'rowTable.Item("MasterArticleID") = Val(cmbProduct.SelectedRow.Cells("MasterID").Value.ToString)  
                rowTable.Item("MasterArticleID") = Val(grdMaterialEstimation.GetRow.Cells("MasterArticleID").Value.ToString)
                'rowTable.Item("PlanItem") = cmbProduct.SelectedRow.Cells("PlanItem").Value.ToString
                rowTable.Item("PlanItem") = grdMaterialEstimation.GetRow.Cells("PlanItem").Value.ToString
                rowTable.Item("SubDepartmentID") = cmbDepartment.Value
                rowTable.Item("SubDepartment") = IIf(cmbDepartment.Value > 0, cmbDepartment.Text, "")
                rowTable.Item("Qty") = Convert.ToDouble(txtQty.Text)
                rowTable.Item("TotalQty") = Convert.ToDouble(txtQty.Text)
                rowTable.Item("ArticleDescription") = cmbProduct.SelectedRow.Cells("Item").Value.ToString
                rowTable.Item("Unit") = cmbProduct.SelectedRow.Cells("Unit").Value.ToString
                rowTable.Item("ArticleUnitId") = Val(cmbProduct.SelectedRow.Cells("ArticleUnitId").Value.ToString) ''Val(Me.cmbProduct.SelectedRow.Cells("Id").Value.ToString)
                rowTable.Item("PlanTicketsId") = 0
                rowTable.Item("MaterialEstMasterID") = Val(grdMaterialEstimation.GetRow.Cells("MaterialEstMasterID").Value.ToString)
                If Val(txtQty.Text) < 0 Then
                    rowTable.Item("Types") = "Minus"
                Else
                    rowTable.Item("Types") = "Plus"
                End If
                rowTable.Item("CostSheetID") = 0
                rowTable.Item("Approve") = 0
                rowTable.Item("Tag#") = 0
                rowTable.Item("ParentTag#") = ParentTagId
                rowTable.Item("Price") = 0
                rowTable.Item("UniqueId") = (dt.Rows.Count + 1)
                rowTable.Item("ParentUniqueId") = ParentUniqueId
                rowTable.Item("ParentId") = ParentId
                'If Not Me.cmbParentItem.ActiveRow Is Nothing Then
                '    If Me.cmbParentItem.Value > 0 Then
                '        rowTable.Item("ParentId") = Me.cmbParentItem.Value
                '    End If
                'End If
                'If Me.cmbParentItem.ActiveRow Is Nothing Or Me.cmbParentItem.Value = 0 Then
                '    rowTable.Item("ParentId") = 0
                'End If

                'dt.Rows.Add(rowTable)
                dt.Rows.InsertAt(rowTable, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            End If
            FillCombos("ParentItem")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function Validate_AddToGrid() As Boolean
        'If Me.cmbVendor.IsItemInList = True Then
        '    If Me.cmbVendor.Rows(0).Activate Then
        '        msg_Error("You must select any customer")
        '        Me.cmbVendor.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If
        'End If
        If Me.cmbProduct.IsItemInList = False Then
            ShowErrorMessage("Item not found")
            Return False
        End If

        If cmbProduct.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("You must select any item")
            cmbProduct.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtQty.Text) = 0 Or Me.txtQty.Text = "" Then
            msg_Error("Quantity must be greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        'If Val(Me.txtQty.Text) > Val(Me.txtRemainingQty.Text) Then
        '    msg_Error("Quantity should be less than remaining quantity")
        '    txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        'End If
        Validate_AddToGrid = True
    End Function
    Private Sub RefreshDetailControls()
        Try
            'Me.txtRemarks.Text = ""
            Me.txtQty.Text = ""
            'Me.dtpMaterialAllocation.Value = Now
            Me.cmbDepartment.Rows(0).Activate()
            'FillCombos("Product")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lblLinkPlanLoad_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblLinkPlanLoad.LinkClicked

    End Sub

    Private Sub ComparisonReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComparisonReportToolStripMenuItem.Click
        Try
            AddRptParam("@EstId", Me.grdSaved.CurrentRow.Cells("Id").Value)
            ShowReport("rptEstimationComparison")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_ButtonClick(sender As Object, e As EventArgs) Handles btnPrint.ButtonClick
        Try
            AddRptParam("@ID", Me.grdSaved.CurrentRow.Cells("Id").Value)
            ShowReport("rptMaterialEstimation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If Me.cmbProduct.Rows.Count > 1 Then
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Code").Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If Me.cmbProduct.Rows.Count > 1 Then
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Item").Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMaterialEstimation_Click(sender As Object, e As EventArgs) Handles grdMaterialEstimation.Click
        Try
            If Not Me.grdMaterialEstimation.GetRow Is Nothing Then
                If grdMaterialEstimation.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    RowIndex = Me.grdMaterialEstimation.GetRow.RowIndex
                    Dim sNo As String = Me.grdMaterialEstimation.GetRow.Children.ToString
                    SerialNo = Me.grdMaterialEstimation.GetRow.Cells("SerialNo").Value.ToString
                    ChildSerialNo = "" & SerialNo & "-" & sNo + 1 & ""
                    If Me.cmbParentItem.Rows.Count > 0 Then
                        Me.cmbParentItem.Value = Me.grdMaterialEstimation.GetRow.Cells("ArticleId").Value
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaterialEstimation.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaterialEstimation.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdMaterialEstimation.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCostSheet(ByVal ArticleId As Integer, ByVal Qty As Double, Optional Item As String = "") As DataTable
        Try
            Dim dt As New DataTable
            dt = bal.GetCostSheet(ArticleId, Qty, , , , , , , ArticleId, Item)
            bal.Clone = False
            bal.dtAllItems = Nothing
            dt.AcceptChanges()
            If Not dt.Rows.Count > 0 Then
                '    Me.grdMaterialEstimation.DataSource = dt
                'Else
                msg_Error("No cost sheet is found against selected item " & Item & "")
                '    Me.grdMaterialEstimation.DataSource = Nothing
                '    Exit Sub
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''TASK:1006 Added TagIds to estimation.
    Public Function GetTagMax() As Integer
        Try
            Dim MasterQuery As String = "SELECT Case When Max(Tag#) IS NULL Then 0 Else Max(Tag#) End As Tag# From MaterialEstimationDetailTable"
            Dim table As DataTable = GetDataTable(MasterQuery)
            Return table.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
   
    ''TASK:1006 Added TagIds to estimation.

    Private Sub grdMaterialEstimation_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMaterialEstimation.CellEdited
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class