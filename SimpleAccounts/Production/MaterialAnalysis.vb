Imports SBModel
Imports SBDal
Public Class MaterialAnalysis
    Implements IGeneral
    Dim Bal As MaterialAnalysisBAL
    Dim Cruddb As CRUD_db
    Dim MaterialAnalysisModel As MaterialAnalysisMaster
    Dim MaterialAnalysisDetailModel As MaterialAnalysisDetail
    Dim cmbTable As DataTable
    Dim IsEditMode As Boolean = False
    Dim MasterItemId As Integer = 0
    Dim MaterialAnalysisMasterID As Integer = 0
    'Dim DecimalPointInQty As Integer = 0I
    Dim MaterialAnalysisId As Integer = 0
    Enum Detail

        MaterialEstMasterID
        RawMaterialID
        RawMaterialCode
        RawMaterialItem
        MatEtmQty
        AllocQty
        StockQty
        POQty
        AvailableQty
        RequiredStockQty
        MaterialAnalysisDetailID
        MaterialAnalysisMasterID
    End Enum

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim MasterQuery As String = ""
        Try
            If Condition = "Customers" Then

                MasterQuery = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,  vwCOAdetail.sub_sub_title as [Ac Head], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                    "dbo.tblListTerritory.TerritoryName as Territory " & _
                                    "FROM         dbo.tblCustomer INNER JOIN " & _
                                    "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                    "WHERE     (dbo.vwCOADetail.account_type = 'Customer') order by tblCustomer.Sortorder, vwCOADetail.detail_title "

                FillUltraDropDown(cmbCustomer, MasterQuery)
                Me.cmbCustomer.Rows(0).Activate()
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "SalesOrders" Then
                MasterQuery = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo, SalesOrderMasterTable.SpecialInstructions from SalesOrderMasterTable Order by SalesOrderDate DESC"
                FillDropDown(cmbSaleOrder, MasterQuery)
            ElseIf Condition = "SalesOrder" Then
                MasterQuery = "Select s.SalesOrderID, s.SalesOrderNo + ' ~ ' + Convert(varchar(12), s.SalesOrderDate,113) as SalesOrderNo, s.SpecialInstructions from SalesOrderMasterTable s where s.VendorId=" & cmbCustomer.Value & " Order by s.SalesOrderDate DESC"
                FillDropDown(cmbSaleOrder, MasterQuery)
            ElseIf Condition = "Plans" Then
                MasterQuery = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSaleOrder.SelectedValue > 0, "Where POId = " & Me.cmbSaleOrder.SelectedValue & "", "") & " Order by PlanDate DESC"
                FillUltraDropDown(Me.cmbPalnNo, MasterQuery)
                Me.cmbPalnNo.Rows(0).Activate()
                Me.cmbPalnNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPalnNo.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Plan" Then
                MasterQuery = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSaleOrder.SelectedValue > 0, "Where POId = " & Me.cmbSaleOrder.SelectedValue & "", "") & " Order by PlanDate DESC "
                FillUltraDropDown(Me.cmbPalnNo, MasterQuery)
                Me.cmbPalnNo.Rows(0).Activate()
                Me.cmbPalnNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPalnNo.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Tickets" Then
                'MasterQuery = "select PlanTicketsId, TicketNo, TicketQuantity, ArticleId from plantickets Order By PlanTicketsId DESC"
                MasterQuery = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Order By PlanTicketsMasterID DESC" ''Where PlanTicketsMasterID Not in(Select IsNull(TicketID, 0) As TicketID From MaterialAnalysisMaster)
                FillUltraDropDown(cmbTicketNo, MasterQuery)
                Me.cmbTicketNo.Rows(0).Activate()
                Me.cmbTicketNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicketNo.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Ticket" Then
                'MasterQuery = "select PlanTicketsId, TicketNo, TicketQuantity, ArticleId  from plantickets Where PlanId =" & Me.cmbPalnNo.Value & "  Order By PlanTicketsId DESC "
                MasterQuery = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster  Where PlanId = " & Me.cmbPalnNo.Value & " And PlanTicketsMasterID Not in(Select IsNull(TicketID, 0) As TicketID From MaterialAnalysisMaster) Order By PlanTicketsMasterID DESC"
                FillUltraDropDown(cmbTicketNo, MasterQuery)
                Me.cmbTicketNo.Rows(0).Activate()
                Me.cmbTicketNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicketNo.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Estimations" Then
                MasterQuery = "select Id, DocNo from MaterialEstimation Order by Id DESC"
                FillDropDown(Me.cmbEstimationNo, MasterQuery)
            ElseIf Condition = "Estimationss" Then
                MasterQuery = "select Id, DocNo from MaterialEstimation Order by Id DESC"
                FillDropDown(Me.cmbEstimationNo, MasterQuery)
            ElseIf Condition = "EstimationByPlan" Then
                MasterQuery = "select Id, DocNo from MaterialEstimation where MasterPlanId=" & cmbPalnNo.Value & " And Id Not In (Select EstimationMasterID FROM MaterialAnalysisMaster) Order By Id DESC"
                FillDropDown(Me.cmbEstimationNo, MasterQuery)

            ElseIf Condition = "EstimationBySalesOrder" Then
                MasterQuery = "select Id, DocNo from MaterialEstimation where saleorderId=" & Me.cmbSaleOrder.SelectedValue & " And Id Not In (Select EstimationMasterID FROM MaterialAnalysisMaster) Order By Id DESC "
                FillDropDown(Me.cmbEstimationNo, MasterQuery)
            ElseIf Condition = "EstimationByTicket" Then
                MasterQuery = "select Id, DocNo from MaterialEstimation where PlanTicketId=" & Me.cmbTicketNo.Value & " And Id Not In (Select EstimationMasterID FROM MaterialAnalysisMaster) Order By Id DESC "
                FillDropDown(Me.cmbEstimationNo, MasterQuery)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            MaterialAnalysisModel = New MaterialAnalysisMaster()
            MaterialAnalysisModel.DocNo = Me.txtDocNo.Text
            MaterialAnalysisModel.MDate = Me.dtpDate.Value
            MaterialAnalysisModel.CustomerID = Me.cmbCustomer.Value
            MaterialAnalysisModel.SaleOrderID = Me.cmbSaleOrder.SelectedValue
            MaterialAnalysisModel.PlanMasterID = Me.cmbPalnNo.Value
            MaterialAnalysisModel.TicketID = Me.cmbTicketNo.Value
            MaterialAnalysisModel.EstimationMasterID = Me.cmbEstimationNo.SelectedValue
            MaterialAnalysisModel.TicketQty = Val(0)
            MaterialAnalysisModel.Remarks = Me.txtRemarks.Text
            MaterialAnalysisModel.ProductID = MasterItemId
            MaterialAnalysisModel.MaterialAnalysisMasterID = MaterialAnalysisMasterID
            For i As Integer = 0 To Me.grdAllocationAnalysis.RowCount - 1
                MaterialAnalysisDetailModel = New MaterialAnalysisDetail()
                MaterialAnalysisDetailModel.MaterialAnalysisDetailID = Me.grdAllocationAnalysis.GetRows(i).Cells("MaterialAnalysisDetailID").Value
                MaterialAnalysisDetailModel.MaterialAnalysisMasterID = Me.grdAllocationAnalysis.GetRows(i).Cells("MaterialAnalysisMasterID").Value
                MaterialAnalysisDetailModel.MaterialEstMasterID = Me.grdAllocationAnalysis.GetRows(i).Cells("MaterialEstMasterID").Value
                MaterialAnalysisDetailModel.RawMaterialID = Me.grdAllocationAnalysis.GetRows(i).Cells("RawMaterialID").Value
                MaterialAnalysisDetailModel.RawMaterialCode = Me.grdAllocationAnalysis.GetRows(i).Cells("RawMaterialCode").Value
                MaterialAnalysisDetailModel.MatEtmQty = Me.grdAllocationAnalysis.GetRows(i).Cells("MatEtmQty").Value
                MaterialAnalysisDetailModel.POQty = Me.grdAllocationAnalysis.GetRows(i).Cells("POQty").Value
                MaterialAnalysisDetailModel.AllocQty = Me.grdAllocationAnalysis.GetRows(i).Cells("AllocQty").Value
                MaterialAnalysisDetailModel.StockQty = Me.grdAllocationAnalysis.GetRows(i).Cells("StockQty").Value
                MaterialAnalysisDetailModel.RequiredStockQty = Me.grdAllocationAnalysis.GetRows(i).Cells("RequiredStockQty").Value
                MaterialAnalysisDetailModel.AvailableQty = Me.grdAllocationAnalysis.GetRows(i).Cells("AvailableQty").Value
                MaterialAnalysisModel.MatAnalysisDetailList.Add(MaterialAnalysisDetailModel)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Cruddb = New CRUD_db()
            Me.grdSaved.DataSource = Cruddb.ReadTable("Select MaterialAnalysisMasterID, DocNo, MDate, CustomerID, vwCOADetail.Detail_Title As Customer, SaleOrderID, PlanMasterID, TicketID, EstimationMasterID, Remarks, ArticleId From MaterialAnalysisMaster LEFT JOIN vwCOADetail ON MaterialAnalysisMaster.CustomerID = vwCOADetail.COA_Detail_ID Order by MDate DESC")
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("MaterialAnalysisMasterID").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerID").Visible = False
            Me.grdSaved.RootTable.Columns("SaleOrderID").Visible = False
            Me.grdSaved.RootTable.Columns("PlanMasterID").Visible = False
            Me.grdSaved.RootTable.Columns("TicketID").Visible = False
            Me.grdSaved.RootTable.Columns("EstimationMasterID").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("DocNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("MDate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Remarks").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Customer").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocNo.Text = "" Then
                msg_Error("Document no is required")
                Me.txtDocNo.Focus() : IsValidate = False : Exit Function
            ElseIf Not Me.grdAllocationAnalysis.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Me.cmbEstimationNo.Focus() : IsValidate = False : Exit Function
            ElseIf Me.cmbEstimationNo.SelectedIndex <= 0 Or Me.cmbEstimationNo.SelectedIndex <= 0 Then
                msg_Error("One of Ticket or Estimation no is required")
                Me.cmbEstimationNo.Focus() : IsValidate = False : Exit Function
            Else
                FillModel()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = NewAnalysisNo()
            Me.txtRemarks.Text = ""
            Me.dtpDate.Value = Now
            If Not cmbCustomer.SelectedRow Is Nothing Then
                Me.cmbCustomer.Rows(0).Activate()
            End If
            If Not cmbSaleOrder.SelectedItem Is Nothing Then
                Me.cmbSaleOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPalnNo.SelectedRow Is Nothing Then
                Me.cmbPalnNo.Rows(0).Activate()
            End If
            If Not Me.cmbTicketNo.SelectedRow Is Nothing Then
                Me.cmbTicketNo.Rows(0).Activate()
            End If
            If Not Me.cmbEstimationNo.SelectedItem Is Nothing Then
                Me.cmbEstimationNo.SelectedIndex = 0
            End If
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            FillCombos("Customers")
            FillCombos("SalesOrders")
            FillCombos("Plans")
            FillCombos("Tickets")
            FillCombos("Estimations")
            GetAllRecords()
            DisplayDetail(-1)
            Me.cmbSaleOrder.Enabled = True
            Me.cmbPalnNo.Enabled = True
            Me.cmbTicketNo.Enabled = True
            Me.cmbEstimationNo.Enabled = True
            Me.cmbCustomer.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Bal.Save_Transation(MaterialAnalysisModel)
            Return True
        Catch ex As Exception
            Return False
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
    Public Function Update_Record() As Boolean
        Try
            Bal.Update_Transation(MaterialAnalysisModel)
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function
    Private Function NewAnalysisNo() As String
        Dim MENo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                MENo = GetSerialNo("MN" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "MaterialAnalysisMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("MN" & "-" & Microsoft.VisualBasic.Strings.Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "MaterialAnalysisMaster", "DocNo")
            Else
                MENo = GetNextDocNo("MN", 6, "MaterialAnalysisMaster", "DocNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub MaterialAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Bal = New MaterialAnalysisBAL()
            'DecimalPointInQty = Val(getConfigValueByType("DecimalPointInQty").ToString)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ID As Integer)
        Try
            Cruddb = New CRUD_db()
            Me.grdAllocationAnalysis.DataSource = Cruddb.ReadTable("Select 0 AS MaterialEstMasterID, IsNull(RawMaterialID, 0) As RawMaterialID, ArticleDefTable.ArticleCode As RawMaterialCode, ArticleDefTable.ArticleDescription As RawMaterialItem, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(MatEtmQty, 0) As MatEtmQty,  IsNull(AllocQty, 0) As AllocQty, IsNull(StockQty, 0) As StockQty, IsNull(POQty, 0) As POQty, IsNull(AvailableQty, 0) As AvailableQty, IsNull(RequiredStockQty, 0) As RequiredStockQty, IsNull(MaterialAnalysisDetailID, 0) As MaterialAnalysisDetailID, IsNull(MaterialAnalysisMasterID, 0) As MaterialAnalysisMasterID FROM MaterialAnalysisDetail INNER JOIN ArticleDefTable ON MaterialAnalysisDetail.RawMaterialID = ArticleDefTable.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where MaterialAnalysisMasterID = " & ID & "")
            Me.grdAllocationAnalysis.RootTable.Columns("MatEtmQty").FormatString = "N" & DecimalPointInQty
            Me.grdAllocationAnalysis.RootTable.Columns("AllocQty").FormatString = "N" & DecimalPointInQty
            Me.grdAllocationAnalysis.RootTable.Columns("StockQty").FormatString = "N" & DecimalPointInQty
            Me.grdAllocationAnalysis.RootTable.Columns("POQty").FormatString = "N" & DecimalPointInQty
            Me.grdAllocationAnalysis.RootTable.Columns("AvailableQty").FormatString = "N" & DecimalPointInQty
            Me.grdAllocationAnalysis.RootTable.Columns("RequiredStockQty").FormatString = "N" & DecimalPointInQty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbEstimationNo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbEstimationNo.SelectedValueChanged
        Try
            Me.grdAllocationAnalysis.DataSource = Bal.GetMatAnalysis_Grid(Me.cmbEstimationNo.SelectedValue)
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
            Throw ex
        End Try
    End Sub
    Private Sub cmbSaleOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSaleOrder.SelectedIndexChanged
        Try

            If cmbSaleOrder.SelectedIndex > 0 Then
                Dim SpecialInstructions As String = CType(Me.cmbSaleOrder.SelectedItem, DataRowView).Item("SpecialInstructions").ToString
                Me.txtRemarks.Text = SpecialInstructions.ToString()
                FillCombos("Plan")
                FillCombos("EstimationBySalesOrder")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPalnNo_ValueChanged(sender As Object, e As EventArgs) Handles cmbPalnNo.ValueChanged
        Try
            If Me.cmbPalnNo.Value > 0 Then
                FillCombos("Ticket")
                FillCombos("EstimationByPlan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbTicketNo_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicketNo.ValueChanged
        Try
            If Me.cmbTicketNo.Value > 0 Then
                'Me.txtTicketQty.Text = Me.cmbTicketNo.SelectedRow.Cells("TicketQuantity").Value.ToString
                'MasterItemId = Val(Me.cmbTicketNo.SelectedRow.Cells("ArticleId").Value.ToString)
                FillCombos("EstimationByTicket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub InitializeCombos()
        Try
            Bal = New MaterialAnalysisBAL()
            Bal.GetSalesOrder()
            Bal.GetPlanNo()
            Bal.GetTicketNo()
            Bal.GetEstimation()
            cmbTable = New DataTable()
            cmbTable = Bal.GetCustomers()
            Me.cmbCustomer.ValueMember = cmbTable.Columns(0).ColumnName.ToString
            Me.cmbCustomer.DisplayMember = cmbTable.Columns(1).ColumnName.ToString
            Me.cmbCustomer.DataSource = cmbTable

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grdAllocationAnalysis.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.GetRow.Cells("MDate").Value
            Me.cmbCustomer.Value = Val(Me.grdSaved.GetRow.Cells("CustomerID").Value.ToString)
            Me.cmbSaleOrder.SelectedValue = Val(Me.grdSaved.GetRow.Cells("SaleOrderID").Value.ToString)
            Me.cmbPalnNo.Value = Val(Me.grdSaved.GetRow.Cells("PlanMasterID").Value.ToString)
            FillCombos("Tickets")
            Me.cmbTicketNo.Value = Val(Me.grdSaved.GetRow.Cells("TicketID").Value.ToString)
            FillCombos("Estimations")
            'Me.txtTicketQty.Text = Val(Me.grdSaved.GetRow.Cells("TicketQty").Value.ToString)
            Me.cmbEstimationNo.SelectedValue = Val(Me.grdSaved.GetRow.Cells("EstimationMasterID").Value.ToString)
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            MasterItemId = Me.grdSaved.GetRow.Cells("ArticleId").Value
            MaterialAnalysisMasterID = Val(Me.grdSaved.GetRow.Cells("MaterialAnalysisMasterID").Value.ToString)
            DisplayDetail(MaterialAnalysisMasterID)
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Visible = True
            Me.cmbSaleOrder.Enabled = False
            Me.cmbPalnNo.Enabled = False
            Me.cmbTicketNo.Enabled = False
            Me.cmbEstimationNo.Enabled = False
            Me.cmbCustomer.Enabled = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Save() = True Then
                        ReSetControls()
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
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Bal.DeleteMaster(MaterialAnalysisMasterID)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("Customers")
            FillCombos("SalesOrders")
            FillCombos("Plans")
            FillCombos("Tickets")
            FillCombos("Estimations")
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
    Private Sub btnNewPO_Click(sender As Object, e As EventArgs) Handles btnNewPO.Click
        Try
            frmMain.LoadControl("frmPurchaseOrder")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdAllocationAnalysis_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAllocationAnalysis.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grdAllocationAnalysis.GetRow.Delete()
                Me.grdAllocationAnalysis.UpdateData()
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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@MaterialAnalysisId", MaterialAnalysisMasterID)
            ShowReport("rptMaterialAnalysis")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Try
            AddRptParam("@MaterialAnalysisId", Val(Me.grdSaved.GetRow.Cells("MaterialAnalysisMasterID").Value.ToString))
            ShowReport("rptMaterialAnalysis")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
