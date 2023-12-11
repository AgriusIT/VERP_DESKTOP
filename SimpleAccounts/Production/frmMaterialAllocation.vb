
Imports SBDal
Imports SBModel
Imports Janus.Windows.GridEX.GridEXRow
Imports System.Collections.Specialized

Public Class frmMaterialAllocation
    Implements IGeneral
    Public IsEditMode As Boolean = False
    Public RemainingQty As Double = 0D
    Public MaterialAllocationId As Integer = 0
    Public ProductMasterID As Integer = 0
    Dim ProductsQuantity As NameValueCollection
    Dim MaterialEstimationDetailId As Integer = 0
    Dim AssociateItems As String = "True"
    Dim RowIndex As Integer = 0
    Dim ChildSerialNo As String = ""
    Dim SerialNo As String = ""
    Dim bal As New MaterialEstimationBAL
    Enum grdEnum
        DetailAllocation_ID
        Master_Allocation_ID
        ProductID
        ProductMasterID
        ArticleCode
        ArticleDescription
        ArticleUnitId
        Unit
        DepartmentID
        SubDepartment
        AllocDetailDate
        'EmployeeDeptId
        'EmployeeDeptName
        Quantity
        Remarks
        MaterialEstimationDetailId
    End Enum
    Dim allocation_DB As MaterialAllocationMaster = New MaterialAllocationMaster()
    Dim MaterialAllocObject As MaterialAllocationModel
    Dim table As DataTable
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles lblQty.Click

    End Sub

    Private Sub frmMaterialAllocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ProductsQuantity = New NameValueCollection
            FillCombos("SO")
            FillCombos("Plan")
            FillCombos("Departments")
            FillCombos("Tickets")
            FillCombos("Product")
            'FillCombos("Estimations")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub fillcombo()
        Try
            Dim saleorderQuery As String = "select SalesOrderId, SalesOrderNo from SalesOrderMasterTable where SalesOrderId In(select SaleOrderId  from MaterialEstimation)"
        Dim MasterplanQuery As String = "select PlanId, PlanNo from PlanMasterTable where PlanId In(select MasterPlanId  from MaterialEstimation)"
        Dim TicketNOQuery As String = "select PlanTicketsId, TicketNo from PlanTickets where PlanTicketsId In(select PlanTicketId  from MaterialEstimation)"
        Dim EmployeeDepttable As String = "SELECT EmployeeDeptId,EmployeeDeptName FROM EmployeeDeptDefTable"
            cmbSalesOrder.DataSource = allocation_DB.GetRecordsByQuery(saleorderQuery)
        cmbSalesOrder.ValueMember = "SalesOrderId"
        cmbSalesOrder.DisplayMember = "SalesOrderNo"
        cmbPlan.DataSource = allocation_DB.GetRecordsByQuery(MasterplanQuery)
        cmbPlan.ValueMember = "PlanId"
        cmbPlan.DisplayMember = "PlanNo"
        cmbTicket.DataSource = allocation_DB.GetRecordsByQuery(TicketNOQuery)
        cmbTicket.DisplayMember = "TicketNo"
        cmbTicket.ValueMember = "PlanTicketsId"
            cmbDepartment.DataSource = allocation_DB.GetRecordsByQuery(EmployeeDepttable)
        cmbDepartment.DisplayMember = "EmployeeDeptName"
        cmbDepartment.ValueMember = "EmployeeDeptId"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub fillEst(ByVal IDQuery As String)
        cmbEstimation.DataSource = allocation_DB.GetRecordsByQuery(IDQuery)
        cmbEstimation.DisplayMember = "DocNo"
        cmbEstimation.ValueMember = "Id"
    End Sub

    Private Sub GridEX1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles AllocationGrid.FormattingRow

    End Sub
    Private Sub RemoveGridIndex(ByVal RemovalID As Integer)
        table.Rows.RemoveAt(RemovalID)
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Validate_AddToGrid() = False Then Exit Sub

            Dim ParentId As Integer = 0
            Dim ParentUniqueId As Integer = 0
            If Not Me.cmbParentItem.ActiveRow Is Nothing Then
                If Me.cmbParentItem.Value > 0 Then
                    ParentId = AllocationGrid.GetRow.Cells("ParentId").Value
                    ParentUniqueId = Val(AllocationGrid.GetRow.Cells("UniqueId").Value.ToString)
                Else
                    ParentId = 0
                    ParentUniqueId = 0
                End If
            Else
                ParentId = 0
                ParentUniqueId = 0
            End If

            Dim dt As DataTable
            dt = CType(AllocationGrid.DataSource, DataTable)
            'Dim dtCostSheet As DataTable = bal.GetCostSheet(cmbProduct.Value, Val(txtQty.Text), ParentUniqueId, dt.Rows.Count, ParentId)
            'If dtCostSheet.Rows.Count > 0 Then
            '    For Each Row As DataRow In dtCostSheet.Rows
            '        Dim dr1 As DataRow
            '        dr1 = dt.NewRow
            '        dr1.Item("MasterArticleID") = Row.Item("MasterArticleID")
            '        dr1.Item("ArticleID") = Row.Item("ArticleId")
            '        dr1.Item("ParentId") = Row.Item("ParentId").ToString
            '        dr1.Item("PlanItem") = Row.Item("PlanItem").ToString
            '        dr1.Item("ArticleDescription") = Row.Item("ArticleDescription").ToString
            '        dr1.Item("TotalQty") = Val(Row.Item("TotalQty").ToString)
            '        dr1.Item("Qty") = Val(Row.Item("Qty").ToString)
            '        dr1.Item("Price") = Val(Row.Item("Price").ToString)
            '        'If Val(Row.Item("Qty").ToString) < 0 Then
            '        '    dr1.Item("Types") = "Minus"
            '        'Else
            '        '    dr1.Item("Types") = "Plus"
            '        'End If
            '        'dr1.Item("Approve") = False
            '        'dr1.Item("CostSheetID") = Row.Item("CostSheetID")
            '        dr1.Item("ArticleUnitId") = Row.Item("ArticleUnitId")
            '        dr1.Item("Unit") = Row.Item("Unit").ToString ''SubDepartmentID
            '        dr1.Item("SubDepartmentID") = Row.Item("SubDepartmentID")
            '        dr1.Item("UniqueId") = Row.Item("UniqueId") ''SubDepartmentID
            '        dr1.Item("ParentUniqueId") = Row.Item("ParentUniqueId")
            '        dt.Rows.Add(dr1)
            '    Next
            'Else
            'Dim Serial As String = Me.AllocationGrid.GetRows.GetLength(0)
            Dim rowTable As DataRow
            rowTable = dt.NewRow
            rowTable.Item(grdEnum.ProductID) = cmbProduct.Value
            rowTable.Item(grdEnum.ProductMasterID) = Val(cmbProduct.SelectedRow.Cells("MasterID").Value.ToString)
            rowTable.Item(grdEnum.DepartmentID) = cmbDepartment.Value
            rowTable.Item(grdEnum.SubDepartment) = IIf(cmbDepartment.Value > 0, cmbDepartment.Text, "")
            rowTable.Item(grdEnum.Quantity) = Convert.ToDouble(txtQty.Text)
            rowTable.Item(grdEnum.Remarks) = txtRemarks.Text
            rowTable.Item(grdEnum.AllocDetailDate) = dtpMaterialAllocation.Value
            rowTable.Item(grdEnum.ArticleCode) = cmbProduct.SelectedRow.Cells("Code").Value.ToString
            rowTable.Item(grdEnum.ArticleDescription) = cmbProduct.SelectedRow.Cells("Item").Value.ToString
            rowTable.Item(grdEnum.Unit) = cmbProduct.SelectedRow.Cells("Unit").Value.ToString
            rowTable.Item(grdEnum.MaterialEstimationDetailId) = MaterialEstimationDetailId ''Val(Me.cmbProduct.SelectedRow.Cells("Id").Value.ToString)
            'If Not Me.cmbParentItem.ActiveRow Is Nothing Then
            '    If Me.cmbParentItem.Value > 0 Then
            '        rowTable.Item("ParentId") = Me.cmbParentItem.Value
            '    End If
            'End If
            'If Me.cmbParentItem.ActiveRow Is Nothing Or Me.cmbParentItem.Value = 0 Then
            '    rowTable.Item("ParentId") = 0
            'End If
            dt.Rows.Add(rowTable)
            'dt.Rows.InsertAt(rowTable, Me.AllocationGrid.GetRow.RowIndex + 1)
            'End If
            RefreshDetailControls()
            FillCombos("ParentItem")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProductNo_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Save() = True Then
                        ReSetControls()
                        'GetAllRecords()
                    End If
                Else
                    If Update_Record() = True Then
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub materialEstCombo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim matEstmationProductQuery As String = "select m.Id ,m.MaterialEstMasterID ,m.PlanItemId , a.ArticleCode ,a.ArticleDescription from MaterialEstimationDetailTable m join ArticleDefTable a on m.PlanItemId=a.ArticleId"
            cmbProduct.DataSource = allocation_DB.GetRecordsByQuery(matEstmationProductQuery)
            cmbProduct.DisplayMember = "ArticleCode"
            cmbProduct.ValueMember = "PlanItemId"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub saleorderCombo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Dim SelectedID As String = "select * from MaterialEstimation where SaleOrderId= '" & cmbSalesOrder.SelectedItem.Value & "'"
            'fillEst(SelectedID)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub plannoCombo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Dim SelectedID As String = "select * from MaterialEstimation where MasterPlanId= '" & cmbPlan.SelectedItem.Value & "'"
            'fillEst(SelectedID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ticketnoCombo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Dim SelectedID As String = "select * from MaterialEstimation where PlanTicketId= '" & cmbTicket.SelectedItem.Value & "'"
            'fillEst(SelectedID)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub txtSpecialInstruction_TextChanged(sender As Object, e As EventArgs) Handles txtSpecialInstruction.TextChanged

    End Sub
    Private Sub DisplayDetail(ByVal allocationID As Integer, Optional ByVal condition As String = "")

        Try
            'AllocationGrid.DataSource = allocation_DB.GetRecordsByQuery("SELECT DetailAllocation_ID, Master_Allocation_ID, ProductID, ProductMasterID, MasterItem.PlanItem, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, DepartmentID, tblProSteps.prod_step As SubDepartment, AllocDetailDate, Quantity, AllocationDetail.Remarks, MaterialEstimationDetailId, AllocationDetail.ParentId As ParentId, AllocationDetail.SerialNo, AllocationDetail.ParentSerialNo FROM AllocationDetail join ArticleDefTable On CASE WHEN AllocationDetail.ProductID LIKE '%-%' THEN Convert(Int, LEFT(AllocationDetail.ProductID, CHARINDEX('-', AllocationDetail.ProductID) - 1)) Else AllocationDetail.ProductID End = ArticleDefTable.ArticleId Left Outer Join ArticleUnitDefTable ON  ArticleDefTable.ArticleUnitId =  ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On  CASE WHEN AllocationDetail.ParentId LIKE '%-%' THEN Convert(Int, LEFT(AllocationDetail.ParentId, CHARINDEX('-', AllocationDetail.ParentId) - 1)) Else AllocationDetail.ParentId End = MasterItem.ArticleId Left Outer Join tblProSteps ON AllocationDetail.DepartmentID =  tblProSteps.ProdStep_Id where AllocationDetail.Master_Allocation_ID= " & allocationID & "")
            AllocationGrid.DataSource = allocation_DB.GetRecordsByQuery("SELECT DetailAllocation_ID, Master_Allocation_ID, ProductID, ProductMasterID, MasterItem.PlanItem, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, DepartmentID, tblProSteps.prod_step As SubDepartment, AllocDetailDate, Quantity, AllocationDetail.Remarks, MaterialEstimationDetailId, AllocationDetail.ParentId As ParentId, AllocationDetail.SerialNo, AllocationDetail.ParentSerialNo, IsNull(AllocationDetail.UniqueId, 0) As UniqueId, IsNull(AllocationDetail.ParentUniqueId, 0) As ParentUniqueId FROM AllocationDetail join ArticleDefTable On AllocationDetail.ProductID = ArticleDefTable.ArticleId Left Outer Join ArticleUnitDefTable ON  ArticleDefTable.ArticleUnitId =  ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On AllocationDetail.ParentId = MasterItem.ArticleId Left Outer Join tblProSteps ON AllocationDetail.DepartmentID =  tblProSteps.ProdStep_Id where AllocationDetail.Master_Allocation_ID= " & allocationID & "")
            Me.AllocationGrid.RootTable.Columns("AllocDetailDate").FormatString = str_DisplayDateFormat
            Me.AllocationGrid.RootTable.Columns("Quantity").FormatString = "N" & DecimalPointInQty
            Me.AllocationGrid.RootTable.Columns("DepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.AllocationGrid.RootTable.Columns("DepartmentID").HasValueList = True
            Me.AllocationGrid.RootTable.Columns("DepartmentID").LimitToList = True
            
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ExpandColumn = Me.AllocationGrid.RootTable.Columns("Article code")
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentDataMember = "ProductID"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True

            FillCombos("grdDepartments")
            FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayEstimationDetail(ByVal EstimationId As Integer, Optional ByVal condition As String = "")
        '//Master
        '        Id	int	Unchecked
        'EstimationDate	datetime	Unchecked
        'MasterPlanId	int	Unchecked
        'PlanTicketId	int	Checked
        'Remarks	nvarchar(1000)	Checked
        'DocNo	varchar(50)	Unchecked
        'SaleOrderId	int	Checked
        'PlanItemId	int	Checked
        '        Unchecked()

        '//Detail
        '        Id	int	Unchecked
        'MaterialEstMasterID	int	Checked
        'Quantity	float	Checked
        'Types	nvarchar(50)	Checked
        'Approve	bit	Checked
        'PlanItemId	int	Checked
        'ProductId	int	Checked
        'CostSheetID	int	Checked
        'SubDepartmentID	int	Checked
        '        Unchecked()
        Try
            'CASE WHEN ProductId LIKE '%-%' THEN Convert(Int, LEFT([ProductId], CHARINDEX('-', [ProductId]) - 1)) Else ProductId End
            'AllocationGrid.DataSource = allocation_DB.GetRecordsByQuery("SELECT 0 As DetailAllocation_ID, 0 As Master_Allocation_ID, Detail.ProductId, Detail.PlanItemId As ProductMasterID, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, Detail.SubDepartmentID As DepartmentID, tblProSteps.prod_step As SubDepartment, GetDate() As AllocDetailDate, Detail.Quantity, '' As Remarks, Detail.Id As MaterialEstimationDetailId, IsNull(Detail.ParentId, 0) As ParentId, Detail.SerialNo, Detail.ParentSerialNo FROM MaterialEstimationDetailTable Detail join ArticleDefTable on Detail.ProductId = ArticleDefTable.ArticleId Left Outer Join ArticleUnitDefTable ON  ArticleDefTable.ArticleUnitId =  ArticleUnitDefTable.ArticleUnitId Left Outer Join tblProSteps ON Detail.SubDepartmentID =  tblProSteps.ProdStep_Id where Detail.MaterialEstMasterID= " & EstimationId & "")
            'AllocationGrid.DataSource = allocation_DB.GetRecordsByQuery("SELECT 0 As DetailAllocation_ID, 0 As Master_Allocation_ID, Detail.ProductId, Detail.PlanItemId As ProductMasterID, MasterItem.PlanItem, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(Detail.SubDepartmentID, 0) As DepartmentID, tblProSteps.prod_step As SubDepartment, GetDate() As AllocDetailDate, IsNull(Detail.TotalQty, 0) As Quantity, '' As Remarks, Detail.Id As MaterialEstimationDetailId, Detail.ParentId As ParentId, Detail.SerialNo, Detail.ParentSerialNo FROM MaterialEstimationDetailTable Detail join ArticleDefTable ON  CASE WHEN Detail.ProductId LIKE '%-%' THEN Convert(Int, LEFT(Detail.ProductId, CHARINDEX('-', Detail.ProductId) - 1)) Else ProductId End = ArticleDefTable.ArticleId Left Outer Join ArticleUnitDefTable ON  ArticleDefTable.ArticleUnitId =  ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On  CASE WHEN Detail.ParentId LIKE '%-%' THEN Convert(Int, LEFT(Detail.ParentId, CHARINDEX('-', Detail.ParentId) - 1)) Else Detail.ParentId End = MasterItem.ArticleId Left Outer Join tblProSteps ON Detail.SubDepartmentID =  tblProSteps.ProdStep_Id where Detail.MaterialEstMasterID= " & EstimationId & "")
            AllocationGrid.DataSource = allocation_DB.GetRecordsByQuery("SELECT 0 As DetailAllocation_ID, 0 As Master_Allocation_ID, Detail.ProductId, Detail.PlanItemId As ProductMasterID, MasterItem.PlanItem, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(Detail.SubDepartmentID, 0) As DepartmentID, tblProSteps.prod_step As SubDepartment, GetDate() As AllocDetailDate, IsNull(Detail.Quantity, 0) As Quantity, '' As Remarks, Detail.Id As MaterialEstimationDetailId, IsNull(Detail.ParentId, 0) As ParentId, Detail.SerialNo, Detail.ParentSerialNo, IsNull(Detail.UniqueId, 0) As UniqueId, IsNull(Detail.ParentUniqueId, 0) As ParentUniqueId FROM MaterialEstimationDetailTable Detail join ArticleDefTable ON  Detail.ProductId = ArticleDefTable.ArticleId Left Outer Join ArticleUnitDefTable ON  ArticleDefTable.ArticleUnitId =  ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On  Detail.ParentId = MasterItem.ArticleId Left Outer Join tblProSteps ON Detail.SubDepartmentID =  tblProSteps.ProdStep_Id where Detail.MaterialEstMasterID= " & EstimationId & "")
            Me.AllocationGrid.RootTable.Columns("AllocDetailDate").FormatString = str_DisplayDateFormat
            Me.AllocationGrid.RootTable.Columns("DepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.AllocationGrid.RootTable.Columns("DepartmentID").HasValueList = True
            Me.AllocationGrid.RootTable.Columns("DepartmentID").LimitToList = True

            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ChildDataMember = "ParentId"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ChildListMember = "ParentId"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ExpandColumn = Me.AllocationGrid.RootTable.Columns("Article code")
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentDataMember = "ProductId"
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.AllocationGrid.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            ''End 31-03-2017
            FillCombos("grdDepartments")
            FillCombos("ParentItem")
        Catch ex As Exception
            Throw ex
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
        Try
            If Condition = "SO" Then
                Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo, SalesOrderMasterTable.SpecialInstructions from SalesOrderMasterTable where SalesOrderId In(Select SalesOrderId From PlanMasterTable) Order by SalesOrderMasterTable.SalesOrderDate DESC"
                FillUltraDropDown(cmbSalesOrder, Str)
                Me.cmbSalesOrder.Rows(0).Activate()
                Me.cmbSalesOrder.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSalesOrder.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Plan" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSalesOrder.Value > 0, "Where POId = " & Me.cmbSalesOrder.Value & "", "") & " Order by PlanDate DESC"
                FillUltraDropDown(cmbPlan, Str)
                Me.cmbPlan.Rows(0).Activate()
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Ticket" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.Value & " Order by Ticket.ProductionStartDate DESC"
                Str = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster  Where PlanId = " & Me.cmbPlan.Value & " And PlanTicketsMasterID Not In(Select IsNull(TicketID, 0) AS TicketID From AllocationMaster) Order By PlanTicketsMasterID DESC"
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.Rows(0).Activate()
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Tickets" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Order by Ticket.ProductionStartDate DESC"
                Str = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Order By PlanTicketsMasterID DESC" ''Where PlanTicketsMasterID Not In(Select IsNull(TicketID, 0) AS TicketID From AllocationMaster)
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.Rows(0).Activate()
            ElseIf Condition = "Estimation" Then
                Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation Where  MaterialEstimation.PlanTicketId = " & cmbTicket.Value & " And MaterialEstimation.Id NOT IN (Select IsNull(tblMaterialEstimation_Id, 0) As tblMaterialEstimation_Id From AllocationMaster ) Order by MaterialEstimation.EstimationDate DESC"
                FillUltraDropDown(cmbEstimation, Str)
                Me.cmbEstimation.Rows(0).Activate()
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "EstimationByPlan" Then
                Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation Where  MaterialEstimation.MasterPlanId = " & cmbPlan.Value & " And MaterialEstimation.Id NOT IN (Select IsNull(tblMaterialEstimation_Id, 0) As tblMaterialEstimation_Id From AllocationMaster) Order by MaterialEstimation.EstimationDate DESC"
                FillUltraDropDown(cmbEstimation, Str)
                Me.cmbEstimation.Rows(0).Activate()
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Estimations" Then
                Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation ORDER BY MaterialEstimation.EstimationDate DESC"
                FillUltraDropDown(cmbEstimation, Str)
                Me.cmbEstimation.Rows(0).Activate()
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Product" Then
                Str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterID FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
                Str += " where Active=1 "
                'Str = "SELECT DISTINCT ArticleDefView.ArticleId as ArticleId, ArticleCode , ArticleDescription as Product, ArticleUnitName as Unit, Sum(IsNull(MaterialEstimationDetailTable.Quantity, 0)) As Quantity, MaterialEstimationDetailTable.PlanItemId, Sum(IsNull(AllocationDetail.Quantity, 0)) As AllocationQty, Sum(IsNull(MaterialEstimationDetailTable.Quantity, 0 ) - IsNull(AllocationDetail.Quantity, 0)) As RemainingQty, MaterialEstimationDetailTable.MaterialEstMasterID FROM ArticleDefView Inner Join MaterialEstimationDetailTable On ArticleDefView.ArticleId = MaterialEstimationDetailTable.ProductId Inner Join MaterialEstimation On MaterialEstimationDetailTable.MaterialEstMasterID = MaterialEstimation.Id LEFT JOIN AllocationDetail ON MaterialEstimationDetailTable.Id = AllocationDetail.MaterialEstimationDetailId" _
                '   & " " & IIf(cmbDepartment.Value <= 0, " Where MaterialEstimationDetailTable.MaterialEstMasterID = " & Me.cmbEstimation.Value & "", "Where MaterialEstimationDetailTable.SubDepartmentID = " & Me.cmbDepartment.Value & "") & " And Active = 1  Group by ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, MaterialEstimationDetailTable.PlanItemId,  ArticleUnitName, MaterialEstimationDetailTable.MaterialEstMasterID"

                FillUltraDropDown(cmbProduct, Str)
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Combination").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(1).Width = 200
                Me.rbCode.Checked = True
            ElseIf Condition = "Department" Then
                'ElseIf Condition = "SubDepartment" Then
                '    FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY 2 ASC")
                Str = "Select Distinct ProdStep_Id, prod_step, sort_Order from tblProSteps INNER JOIN MaterialEstimationDetailTable Detail ON tblProSteps.ProdStep_Id = Detail.SubDepartmentID Where Detail.MaterialEstMasterID =" & cmbEstimation.Value & "  ORDER BY 2 ASC"
                FillUltraDropDown(cmbDepartment, Str)
                Me.cmbDepartment.Rows(0).Activate()
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(1).Width = 200
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
                Me.AllocationGrid.RootTable.Columns("DepartmentID").ValueList.PopulateValueList(dt.DefaultView, "ProdStep_Id", "prod_step")
            ElseIf Condition = "ParentItem" Then
                If Me.AllocationGrid.RowCount > 0 Then
                    Dim ParentItemList As New List(Of ParentItem)
                    Dim dt As DataTable = CType(Me.AllocationGrid.DataSource, DataTable)
                    Dim dtMerged As New DataTable
                    dtMerged.Columns.Add("ProductId")
                    'dtMerged.Columns.Add("Article code")
                    dtMerged.Columns.Add("Article Description")
                    For Each row As DataRow In dt.Rows
                        Dim drArray() As DataRow = dtMerged.Select("ProductId = '" & row("ProductId") & "'")
                        If drArray.Length = 0 Then
                            Dim drMerged As DataRow
                            drMerged = dtMerged.NewRow
                            drMerged(0) = row.Item("ProductId")
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
                    Me.cmbParentItem.ValueMember = "ProductId"
                    Me.cmbParentItem.DataSource = dtMerged
                    Me.cmbParentItem.Rows(0).Activate()
                    Me.cmbParentItem.DisplayLayout.Bands(0).Columns("ProductId").Hidden = True
                    Me.cmbParentItem.DisplayLayout.Bands(0).Columns("Article Description").Width = 250
                    'If rbName1.Checked = True Then
                    '    Me.cmbParentItem.DisplayMember = Me.cmbParentItem.Rows(0).Cells(2).Column.Key.ToString
                    'ElseIf rbCode1.Checked = True Then
                    '    Me.cmbParentItem.DisplayMember = Me.cmbParentItem.Rows(0).Cells(1).Column.Key.ToString
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            MaterialAllocObject = New MaterialAllocationModel()
            MaterialAllocObject.Master_Allocation_ID = MaterialAllocationId
            MaterialAllocObject.DocNo = Me.txtDocNo.Text
            MaterialAllocObject.Remarks = Me.txtSpecialInstruction.Text
            MaterialAllocObject.tblMaterialEstimation_Id = Me.cmbEstimation.Value
            MaterialAllocObject.DateEntry = Me.dtpAllocation.Value
            MaterialAllocObject.TicketID = Me.cmbTicket.Value
            MaterialAllocObject.Status = Me.cbStatus.Checked
            MaterialAllocObject.PlanId = Me.cmbPlan.Value
            MaterialAllocObject.SalesOrderId = Me.cmbSalesOrder.Value
            table = Me.AllocationGrid.DataSource()
            table.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            '             Master_Allocation_ID	int	Unchecked
            'MasterDate	datetime	Checked
            'DocNo	nvarchar(50)	Checked
            'Remarks	varchar(100)	Checked
            'tblMaterialEstimation_Id	int	Checked
            'Status	varchar(10)	Checked
            '            Unchecked()
            'Dim Str As String = " Select AllocationMaster.Master_Allocation_ID, AllocationMaster.MasterDate, AllocationMaster.DocNo, AllocationMaster.Remarks, IsNull(AllocationMaster.tblMaterialEstimation_Id, 0) As tblMaterialEstimation_Id,  AllocationMaster.Status, IsNull(AllocationMaster.TicketID, 0) As TicketID, PlanTicketsMaster.TicketNo, IsNull(AllocationMaster.PlanId, 0) As PlanId, PlanMasterTable.PlanNo, IsNull(AllocationMaster.SalesOrderId, 0) As SalesOrderId, SalesOrderMasterTable.SalesOrderNo  From AllocationMaster LEFT JOIN PlanTicketsMaster On AllocationMaster.TicketID = PlanTicketsMaster.PlanTicketsMasterID LEFT JOIN SalesOrderMasterTable ON AllocationMaster.SalesOrderId = SalesOrderMasterTable.SalesOrderId LEFT JOIN PlanMasterTable ON AllocationMaster.PlanId = PlanMasterTable.PlanId Order by Master_Allocation_ID DESC"
            Dim Str As String = "SELECT     AllocationMaster.Master_Allocation_ID, AllocationMaster.DocNo, AllocationMaster.MasterDate, SalesOrderMasterTable.SalesOrderNo, PlanMasterTable.PlanNo, PlanTicketsMaster.TicketNo, " _
                      & " AllocationMaster.Status, AllocationMaster.Remarks, ISNULL(AllocationMaster.tblMaterialEstimation_Id, 0) AS tblMaterialEstimation_Id, ISNULL(AllocationMaster.TicketID, 0) AS TicketID, " _
                      & " ISNULL(AllocationMaster.PlanId, 0) AS PlanId, ISNULL(AllocationMaster.SalesOrderId, 0) AS SalesOrderId " _
                      & " FROM         AllocationMaster LEFT OUTER JOIN " _
                      & " PlanTicketsMaster ON AllocationMaster.TicketID = PlanTicketsMaster.PlanTicketsMasterID LEFT OUTER JOIN " _
                      & " SalesOrderMasterTable ON AllocationMaster.SalesOrderId = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN " _
                      & " PlanMasterTable ON AllocationMaster.PlanId = PlanMasterTable.PlanId " _
                      & " ORDER BY AllocationMaster.Master_Allocation_ID DESC"
            Me.grdSaved.DataSource = allocation_DB.GetRecordsByQuery(Str)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("Master_Allocation_ID").Visible = False
            Me.grdSaved.RootTable.Columns("tblMaterialEstimation_Id").Visible = False
            Me.grdSaved.RootTable.Columns("TicketID").Visible = False
            Me.grdSaved.RootTable.Columns("PlanId").Visible = False
            Me.grdSaved.RootTable.Columns("SalesOrderId").Visible = False
            Me.grdSaved.RootTable.Columns("Status").Visible = False
            Me.grdSaved.RootTable.Columns("Remarks").Width = 200
            Me.grdSaved.RootTable.Columns("MasterDate").Width = 150
            Me.grdSaved.RootTable.Columns("DocNo").Width = 150

            Me.grdSaved.RootTable.Columns("PlanNo").Caption = "Plan No"
            Me.grdSaved.RootTable.Columns("SalesOrderNo").Caption = "Sales Order No"
            Me.grdSaved.RootTable.Columns("TicketNo").Caption = "Ticket No"

            Me.grdSaved.RootTable.Columns("MasterDate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("DocNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Remarks").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Status").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("PlanNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("SalesOrderNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("TicketNo").EditType = Janus.Windows.GridEX.EditType.NoEdit


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocNo.Text = "" Then
                msg_Error("Document No is required")
                Me.txtDocNo.Focus() : IsValidate = False : Exit Function
            ElseIf Not Me.AllocationGrid.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Me.AllocationGrid.Focus() : IsValidate = False : Exit Function
            ElseIf Me.cmbEstimation.Value <= 0 Then
                msg_Error("Estimation is required")
                Me.cmbEstimation.Focus() : IsValidate = False : Exit Function
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
            GetSecurityRights()
            Me.txtDocNo.Text = NewAllocationNo()
            ''Me.txtRemarks.Text = ""
            Me.txtSpecialInstruction.Text = ""
            Me.dtpAllocation.Value = Now
            If Not cmbSalesOrder.SelectedRow Is Nothing Then
                Me.cmbSalesOrder.Rows(0).Activate()
            End If
            If Not cmbPlan.SelectedRow Is Nothing Then
                Me.cmbPlan.Rows(0).Activate()
            End If
            If Not Me.cmbTicket.SelectedRow Is Nothing Then
                Me.cmbTicket.Rows(0).Activate()
            End If
            If Not Me.cmbEstimation.SelectedRow Is Nothing Then
                Me.cmbEstimation.Rows(0).Activate()
            End If
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.cmbSalesOrder.Enabled = True
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
            Me.cmbEstimation.Enabled = True
            Me.cbStatus.Checked = True
            IsEditMode = False
            FillCombos("SO")
            FillCombos("Plan")
            'FillCombos("Department")
            FillCombos("Tickets")
            GetAllRecords()
            DisplayDetail(-1)
            ''Reset detail controls
            Me.txtRemarks.Text = ""
            Me.txtQty.Text = ""
            Me.txtRemainingQty.Text = ""
            Me.dtpMaterialAllocation.Value = Now
            If Not Me.cmbDepartment.SelectedRow Is Nothing Then
                Me.cmbDepartment.Rows(0).Activate()
            End If
            If Not Me.cmbProduct.SelectedRow Is Nothing Then
                Me.cmbProduct.Rows(0).Activate()
            End If
            If Not getConfigValueByType("AssociateItems").ToString = "Error" Then
                AssociateItems = getConfigValueByType("AssociateItems")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            allocation_DB.Save_Transation(table, MaterialAllocObject)
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

    Private Sub cmbSalesOrder_ValueChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.ValueChanged
        Try
            If cmbSalesOrder.Value > 0 Then
                Me.txtSpecialInstruction.Text = Me.cmbSalesOrder.SelectedRow.Cells("SpecialInstructions").Value.ToString
                FillCombos("Plan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_ValueChanged(sender As Object, e As EventArgs) Handles cmbPlan.ValueChanged
        Try
            If cmbPlan.Value > 0 Then
                FillCombos("Ticket")
                FillCombos("EstimationByPlan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEstimation_ValueChanged(sender As Object, e As EventArgs) Handles cmbEstimation.ValueChanged
        Try
            If Me.cmbEstimation.Value > 0 Then
                FillCombos("Product")
                DisplayEstimationDetail(Me.cmbEstimation.Value)
                'FillCombos("Department")
            End If
            'DisplayDetail(cmbEstimation.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicket.ValueChanged
        Try
            If Me.cmbTicket.Value > 0 Then
                FillCombos("Estimation")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbProduct.ValueChanged
        Try
            If Me.cmbProduct.Value > 0 Then
                'QuantityCalculation()
                'MaterialEstimationDetailId = GetMaterialEstDetailId(Me.cmbProduct.SelectedRow.Cells("MaterialEstMasterID").Value, Me.cmbProduct.Value)


                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.AllocationGrid.RootTable.Columns("ProductID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
                'Dim GridQty As Double = Me.AllocationGrid.GetTotal(Me.AllocationGrid.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                'RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
                'Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value.ToString)
                'If GridQty > 0 AndAlso GridQty < Quantity Then
                '    Me.txtRemainingQty.Text = Quantity - GridQty
                'Else
                '    Me.txtRemainingQty.Text = RemainingQty.ToString
                'End If
            End If

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

        If Val(txtQty.Text) <= 0 Or Me.txtQty.Text = "" Then
            msg_Error("Quantity must be greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        'If Val(Me.txtQty.Text) > Val(Me.txtRemainingQty.Text) Then
        '    msg_Error("Quantity should be less than remaining quantity")
        '    txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        'End If
        Validate_AddToGrid = True
    End Function
    Private Function NewAllocationNo() As String
        Dim MENo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                MENo = GetSerialNo("MA" + "-" + Microsoft.VisualBasic.Right(Me.dtpAllocation.Value.Year, 2) + "-", "AllocationMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("MA" & "-" & Microsoft.VisualBasic.Strings.Format(Me.dtpAllocation.Value, "yy") & Me.dtpAllocation.Value.Month.ToString("00"), 4, "AllocationMaster", "DocNo")
            Else
                MENo = GetNextDocNo("MA", 6, "AllocationMaster", "DocNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Update_Record() As Boolean
        Try
            allocation_DB.Update_Transation(table, MaterialAllocObject)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub EditRecord()
        Try
            IsEditMode = True
            'Mode = "Edit"
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.AllocationGrid.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.cmbSalesOrder.Rows(0).Activate()
            Me.cmbPlan.Rows(0).Activate()
            Me.cmbSalesOrder.Value = Me.grdSaved.GetRow.Cells("SalesOrderId").Value
            Me.cmbPlan.Value = Me.grdSaved.GetRow.Cells("PlanId").Value
            FillCombos("Tickets")
            Me.cmbTicket.Value = Me.grdSaved.GetRow.Cells("TicketID").Value
            FillCombos("Estimations")
            Me.cmbEstimation.Value = Me.grdSaved.GetRow.Cells("tblMaterialEstimation_Id").Value
            'FillCombos("Product")
            Me.dtpAllocation.Value = Me.grdSaved.GetRow.Cells("MasterDate").Value
            Me.txtSpecialInstruction.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString()
            If IsDBNull(Me.grdSaved.GetRow.Cells("Status").Value) = False Then
                Me.cbStatus.Checked = Me.grdSaved.GetRow.Cells("Status").Value
            End If
            MaterialAllocationId = Me.grdSaved.GetRow.Cells("Master_Allocation_ID").Value
            DisplayDetail(MaterialAllocationId)
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Visible = True
            Me.cmbSalesOrder.Enabled = False
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
            Me.cmbEstimation.Enabled = False
            'Master_Allocation_ID, MasterDate, DocNo, Remarks, tblMaterialEstimation_Id
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
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

    Private Sub grdSaved_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.AllocationGrid.GetRow.Delete()
                Me.AllocationGrid.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RefreshDetailControls()
        Try
            Me.txtRemarks.Text = ""
            Me.txtQty.Text = ""
            Me.dtpMaterialAllocation.Value = Now
            Me.cmbDepartment.Rows(0).Activate()
            'FillCombos("Product")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AllocationGrid_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles AllocationGrid.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.AllocationGrid.GetRow.Delete()
                Me.AllocationGrid.UpdateData()
                'QuantityCalculation()
                'Me.txtRemainingQty.Text = RemainingQty - AllocationGrid.GetTotal(Me.AllocationGrid.RootTable.Columns(grdEnum.Quantity), Janus.Windows.GridEX.AggregateFunction.Sum)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            allocation_DB.DeleteMaster(Me.grdSaved.GetRow.Cells("Master_Allocation_ID").Value)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
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
    'Private Sub QuantityCalculation()
    '    Try
    '        Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.AllocationGrid.RootTable.Columns("ProductID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
    '        Dim GridQty As Double = Me.AllocationGrid.GetTotal(Me.AllocationGrid.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '        'RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
    '        'Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value.ToString)
    '        Dim AllocatedQty As Double = Quantity - RemainingQty
    '        Dim TotalAllocatedQty As Double = AllocatedQty + GridQty
    '        If TotalAllocatedQty > 0 Then
    '            Me.txtRemainingQty.Text = Quantity - TotalAllocatedQty
    '        ElseIf Not GridQty > 0 Then
    '            Me.txtRemainingQty.Text = RemainingQty
    '        Else
    '            Me.txtRemainingQty.Text = Quantity
    '            'Me.txtRemainingQty.Text = RemainingQty.ToString
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cmbDepartment_ValueChanged(sender As Object, e As EventArgs) Handles cmbDepartment.ValueChanged
        'If Not cmbDepartment.Value <= 0 Then
        '    FillCombos("Product")
        'End If
    End Sub

    Private Sub cmbProduct_Leave(sender As Object, e As EventArgs) Handles cmbProduct.Leave
        Try
            If Me.cmbProduct.Value > 0 Then
                'QuantityCalculation()
                'MaterialEstimationDetailId = GetMaterialEstDetailId(Me.cmbProduct.SelectedRow.Cells("MaterialEstMasterID").Value, Me.cmbProduct.Value)


                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.AllocationGrid.RootTable.Columns("ProductID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
                'Dim GridQty As Double = Me.AllocationGrid.GetTotal(Me.AllocationGrid.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                'RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
                'Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value.ToString)
                'If GridQty > 0 AndAlso GridQty < Quantity Then
                '    Me.txtRemainingQty.Text = Quantity - GridQty
                'Else
                '    Me.txtRemainingQty.Text = RemainingQty.ToString
                'End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ID", MaterialAllocationId)
            ShowReport("rptMaterialAllocation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_TextChanged(sender As Object, e As EventArgs) Handles cmbProduct.TextChanged
        Try
            If Me.cmbProduct.Value > 0 Then
                'QuantityCalculation()
                'MaterialEstimationDetailId = GetMaterialEstDetailId(Me.cmbProduct.SelectedRow.Cells("MaterialEstMasterID").Value, Me.cmbProduct.Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetMaterialEstDetailId(ByVal MaterialEstimationID As Integer, ByVal ProductID As Integer) As Integer
        Dim dt As New DataTable
        Dim Quer As String = ""
        Try
            Quer = "Select IsNull(Id, 0) As Id From MaterialEstimationDetailTable Where MaterialEstMasterID =" & MaterialEstimationID & " And ProductId =" & ProductID & ""
            dt = GetDataTable(Quer)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows.Item(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                'CtrlGrdBar2.MyGrid = AllocationGrid
                CtrlGrdBar1.Visible = True
                CtrlGrdBar1.Enabled = True
                CtrlGrdBar2.Visible = False
                CtrlGrdBar2.Enabled = False
            Else
                CtrlGrdBar2.Visible = True
                CtrlGrdBar2.Enabled = True
                CtrlGrdBar1.Visible = False
                CtrlGrdBar1.Enabled = False
            End If
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
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmMaterialAllocation)
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

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If rbCode.Checked = True Then
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If rbName.Checked = True Then
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AllocationGrid_Click(sender As Object, e As EventArgs) Handles AllocationGrid.Click
        Try
            RowIndex = Me.AllocationGrid.GetRow.RowIndex
            Dim sNo As String = Me.AllocationGrid.GetRow.Children.ToString
            SerialNo = Me.AllocationGrid.GetRow.Cells("SerialNo").Value.ToString
            ChildSerialNo = "" & SerialNo & "-" & sNo + 1 & ""
            If Me.cmbParentItem.Rows.Count > 0 Then
                Me.cmbParentItem.Value = Me.AllocationGrid.GetRow.Cells("ProductId").Value
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class