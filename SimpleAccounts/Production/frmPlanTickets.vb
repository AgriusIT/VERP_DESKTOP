Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmPlanTickets
    Implements IGeneral

    Dim PlanTicketsMaster As PlanTicketsMaster
    Dim PlanTicketsDetail As PlanTicketsDetail
    Dim PlanTicketsDAL As PlanTicketsDAL
    Dim PlanTicketsMasterID As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim PlanId As Integer = 0
    Dim PlanDetailId As Integer = 0
    Dim Qty As Double = 0
    Dim IssuedQty As Double = 0
    Dim RemainingQty As Double = 0

    'Private Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    PlanTicketsDAL = New PlanTicketsDAL()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            PlanTicketsDAL.Delete(Me.grdMaster.GetRow.Cells("PlanTicketsMasterID").Value)
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
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable Where PlanId In (Select PlanId from PlanDetailTable Where IsNull(Qty, 0) > IsNull(TicketIssuedQty, 0) ) Order by PlanDate DESC"
                FillDropDown(cmbPlan, Str)
            ElseIf Condition = "TicketProduct" Then
                'Str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price FROM ArticleDefView where Active=1"
                ' ''Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription Item, ArticleDefTable.ArticleCode Code, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanDetailTable.PlanDetailId, 0) As PlanDetailId, Sum(PlanDetailTable.Qty) As Qty FROM ArticleDefTable INNER JOIN PlanDetailTable ON ArticleDefTable.ArticleId = PlanDetailTable.ArticleDefId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " Group By ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleCode, PlanDetailTable.PlanDetailId, ArticleUnitDefTable.ArticleUnitName "
                'Dim ConfigValue As String = getConfigValueByType("CostSheetType")
                'If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                '    Str = "SELECT ArticleDefTableMaster.ArticleId as Id, ArticleDefTableMaster.ArticleCode Code, ArticleDefTableMaster.ArticleDescription Item, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanDetailTable.PlanDetailId, 0) As PlanDetailId, Sum(PlanDetailTable.Qty) As Qty FROM ArticleDefTableMaster INNER JOIN PlanDetailTable ON ArticleDefTableMaster.ArticleId = PlanDetailTable.ArticleDefId LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " Group By ArticleDefTableMaster.ArticleId, ArticleDefTableMaster.ArticleDescription, ArticleDefTableMaster.ArticleCode, ArticleUnitDefTable.ArticleUnitName, PlanDetailTable.PlanDetailId "
                'Else
                Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription Item, ArticleDefTable.ArticleCode Code, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanDetailTable.PlanDetailId, 0) As PlanDetailId, Sum(PlanDetailTable.Qty) As Qty FROM ArticleDefTable INNER JOIN PlanDetailTable ON ArticleDefTable.ArticleId = PlanDetailTable.ArticleDefId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " Group By ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleCode, PlanDetailTable.PlanDetailId, ArticleUnitDefTable.ArticleUnitName "
                'End If
                FillUltraDropDown(Me.cmbTicketProduct, Str)
                Me.cmbTicketProduct.Rows(0).Activate()
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
                Me.rbCode.Checked = True
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
            PlanTicketsMaster.TicketDate = Me.dtpDate.Value
            PlanTicketsMaster.CustomerID = Me.cmbCustomer.Value
            PlanTicketsMaster.SalesOrderID = Me.cmbSalesOrder.SelectedValue
            PlanTicketsMaster.PlanID = Me.cmbPlan.SelectedValue
            PlanTicketsMaster.SpecialInstructions = Me.txtSpecialInstructions.Text
            For i As Integer = 0 To Me.grdTicket.RowCount - 1
                PlanTicketsDetail = New PlanTicketsDetail()
                PlanTicketsDetail.PlanTicketsDetailID = Me.grdTicket.GetRows(i).Cells("PlanTicketsDetailID").Value
                PlanTicketsDetail.PlanTicketsMasterID = Me.grdTicket.GetRows(i).Cells("PlanTicketsMasterID").Value
                PlanTicketsDetail.ArticleId = Me.grdTicket.GetRows(i).Cells("ArticleId").Value
                PlanTicketsDetail.PlanDetailId = Me.grdTicket.GetRows(i).Cells("PlanDetailId").Value
                PlanTicketsDetail.Quantity = Me.grdTicket.GetRows(i).Cells("Quantity").Value
                PlanTicketsMaster.Detail.Add(PlanTicketsDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdMaster.DataSource = PlanTicketsDAL.GetAll()
            Me.grdMaster.RetrieveStructure()
            Me.grdMaster.RootTable.Columns("PlanTicketsMasterID").Visible = False
            Me.grdMaster.RootTable.Columns("CustomerID").Visible = False
            Me.grdMaster.RootTable.Columns("SalesOrderID").Visible = False
            Me.grdMaster.RootTable.Columns("PlanID").Visible = False
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
            ElseIf Not Me.grdTicket.RowCount > 0 Then
                msg_Error("Grid is empty. One or more rows are required")
                Me.grdTicket.Focus() : IsValidate = False : Exit Function
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
            IsEditMode = False
            If cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCustomer.Rows(0).Activate()
            End If
            FillCombos("SalesOrders")
            FillCombos("Plans")
            FillCombos("TicketProduct")
            'Me.cmbSalesOrder.Items.
            If Not Me.cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not cmbTicketProduct.SelectedRow Is Nothing Then
                Me.cmbTicketProduct.Rows(0).Activate()
            End If
            Me.BtnSave.Text = "&Save"
            Me.BtnDelete.Visible = False
            Me.txtTicketQty.Text = ""
            Me.txtAvailableQty.Text = ""
            ResetDetailControls()
            GetAllRecords()
            GetDetail(-1)
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            PlanTicketsDAL.Save(PlanTicketsMaster)
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
            PlanTicketsDAL.Update(PlanTicketsMaster)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetDetail(ByVal MasterID As Integer)
        Try
            Me.grdTicket.DataSource = PlanTicketsDAL.GetDetail(MasterID)
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

    Private Sub frmPlanTickets_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            PlanTicketsDAL = New PlanTicketsDAL()
            FillCombos("Customer")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
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
            If Me.grdTicket.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtTicketNo.Text = Me.grdMaster.CurrentRow.Cells("TicketNo").Value.ToString
            Me.dtpDate.Value = Me.grdMaster.CurrentRow.Cells("TicketDate").Value
            PlanTicketsMasterID = Me.grdMaster.CurrentRow.Cells("PlanTicketsMasterID").Value
            Me.cmbCustomer.Value = Me.grdMaster.CurrentRow.Cells("CustomerID").Value
            Me.cmbSalesOrder.SelectedValue = Me.grdMaster.CurrentRow.Cells("SalesOrderID").Value
            Me.cmbPlan.SelectedValue = Me.grdMaster.CurrentRow.Cells("PlanID").Value
            Me.txtSpecialInstructions.Text = Me.grdMaster.CurrentRow.Cells("SpecialInstructions").Value.ToString()
            Me.GetDetail(PlanTicketsMasterID)
            Me.BtnSave.Text = "&Update"
            Me.BtnDelete.Visible = True
            FillCombos("TicketProduct")
            Me.txtTicketQty.Text = ""
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
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
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If Me.cmbPlan.SelectedValue > 0 Then
                Dim PlanNo As String = CType(Me.cmbPlan.SelectedItem, DataRowView).Item("UsedForTicket").ToString
                If IsEditMode = False Then
                    Me.txtTicketNo.Text = GetNextTicket(PlanNo)
                End If
                FillCombos("TicketProduct")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicketProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicketProduct.ValueChanged
        Try
            If Me.cmbTicketProduct.Value > 0 Then
                Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdTicket.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbTicketProduct.SelectedRow.Cells("Id").Value)
                Dim GridQty As Double = Me.grdTicket.GetTotal(Me.grdTicket.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                PlanDetailId = Val(Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value.ToString)
                Qty = Val(Me.cmbTicketProduct.SelectedRow.Cells("Qty").Value.ToString)
                IssuedQty = GetTicketIssuedQty(PlanDetailId)
                Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
                If DifInIssuedAndNewGridQty > 0 Then
                    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
                Else
                    RemainingQty = Qty - IssuedQty
                End If
                Me.txtAvailableQty.Text = Val(RemainingQty)
            End If
            'Me.txtAvailableQty.Text = Val(Me.cmbTicketProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicketProduct_Leave(sender As Object, e As EventArgs) Handles cmbTicketProduct.Leave
        Try
            If Me.cmbTicketProduct.Value > 0 Then
                Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdTicket.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbTicketProduct.SelectedRow.Cells("Id").Value)
                Dim GridQty As Double = Me.grdTicket.GetTotal(Me.grdTicket.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                PlanDetailId = Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value
                Qty = Val(Me.cmbTicketProduct.SelectedRow.Cells("Qty").Value.ToString)
                IssuedQty = GetTicketIssuedQty(PlanDetailId)
                Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
                If DifInIssuedAndNewGridQty > 0 Then
                    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
                Else
                    RemainingQty = Qty - IssuedQty
                End If
                Me.txtAvailableQty.Text = Val(RemainingQty)
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
            If Me.cmbTicketProduct.Value <= 0 Then
                msg_Error("Please select a product")
                Me.cmbTicketProduct.Focus()
                Exit Sub
            ElseIf Val(Me.txtTicketQty.Text) <= 0 Then
                msg_Error("Quantity is required larger than zero")
                Me.txtTicketQty.Focus()
                Exit Sub
            ElseIf Val(Me.txtTicketQty.Text) > Val(Me.txtAvailableQty.Text) Then
                msg_Error("Quantity should be less than available quantity")
                Me.txtAvailableQty.Focus()
                Exit Sub
            End If
            dt = CType(Me.grdTicket.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("PlanTicketsDetailID") = 0
            dr("PlanTicketsMasterID") = 0
            dr("ArticleId") = Me.cmbTicketProduct.Value
            dr("ArticleCode") = Me.cmbTicketProduct.SelectedRow.Cells("Code").Value.ToString
            dr("ArticleDescription") = Me.cmbTicketProduct.SelectedRow.Cells("Item").Value.ToString
            dr("UnitName") = Me.cmbTicketProduct.SelectedRow.Cells("UnitName").Value.ToString
            dr("PlanDetailId") = Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value.ToString
            dr("Quantity") = Val(Me.txtTicketQty.Text)
            dt.Rows.Add(dr)
            Me.txtAvailableQty.Text -= Val(Me.txtTicketQty.Text)
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
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            'select substring(@str, 3, charindex('\', @str, 3) - 3)
            strSql = "select IsNull(Max(Convert(Integer, Right(TicketNo, CHARINDEX('-', REVERSE('-' + TicketNo)) - 1))), 0) from PlanTicketsMaster Where PlanId = " & Me.cmbPlan.SelectedValue & "" ' "
            'Else
            '    strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
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
            frmPlanTickets_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdTicket_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTicket.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then

                Dim PlainTicketsDetailID As Integer = Me.grdTicket.GetRow.Cells("PlanTicketsDetailID").Value
                Dim Qty As Double = Me.grdTicket.GetRow.Cells("Quantity").Value
                If PlainTicketsDetailID > 0 Then

                    Dim PlanDetailId As Integer = Me.grdTicket.GetRow.Cells("PlanDetailId").Value
                    PlanTicketsDAL.DeleteDetailRow(PlainTicketsDetailID)
                    PlanTicketsDAL.SubtractQty(PlanDetailId, Qty)
                    'Else
                    '    msg_Error("You can not delete all saved rows. You should delete whole transaction instead")
                    '    Exit Sub

                End If
                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdTicket.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbTicketProduct.SelectedRow.Cells("Id").Value)
                'Dim GridQty As Double = Me.grdTicket.GetTotal(Me.grdTicket.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                Dim grdArticleId As Integer = Me.grdTicket.GetRow.Cells("ArticleId").Value
                Dim cmbArticleId As Integer = Me.cmbTicketProduct.SelectedRow.Cells("Id").Value
                Me.grdTicket.GetRow.Delete()

                If grdArticleId = cmbArticleId Then
                    Me.txtAvailableQty.Text += Qty
                End If
                ResetDetailControls()

            End If
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



            Me.grdTicket.UpdateData()
            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdTicket.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbTicketProduct.SelectedRow.Cells("ArticleId").Value)
            Dim GridQty As Double = Me.grdTicket.GetTotal(Me.grdTicket.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
            RemainingQty = Val(Me.cmbTicketProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
            Dim Quantity As Double = Val(Me.cmbTicketProduct.SelectedRow.Cells("Quantity").Value.ToString)
            Dim AllocatedQty As Double = Quantity - RemainingQty
            Dim TotalAllocatedQty = AllocatedQty + GridQty
            If TotalAllocatedQty > 0 Then
                Me.txtAvailableQty.Text = Quantity - TotalAllocatedQty
            ElseIf Not GridQty > 0 Then
                Me.txtAvailableQty.Text = RemainingQty
            Else
                Me.txtAvailableQty.Text = Quantity
                'Me.txtReqQty.Text = RemainingQty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTicketQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTicketQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            AddRptParam("@PlanTicketsMasterID", PlanTicketsMasterID)
            ShowReport("rptPlanTicket")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If Not Me.cmbTicketProduct.ActiveRow Is Nothing AndAlso Me.cmbTicketProduct.Rows.Count > 1 Then
                If Me.rbCode.Checked = True Then
                    Me.cmbTicketProduct.DisplayMember = Me.cmbTicketProduct.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If Not Me.cmbTicketProduct.ActiveRow Is Nothing AndAlso Me.cmbTicketProduct.Rows.Count > 1 Then
                If Me.rbName.Checked = True Then
                    Me.cmbTicketProduct.DisplayMember = Me.cmbTicketProduct.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdTicket.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdTicket.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdTicket.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Plan Tickets"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub CtrlGrdBar1_Load_1(sender As Object, e As EventArgs)

    End Sub
End Class
