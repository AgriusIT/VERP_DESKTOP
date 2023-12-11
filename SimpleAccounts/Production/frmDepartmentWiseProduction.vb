''TASK: TFS1083 New Material Estimation screen to load  old cost sheet and then estimation should be loaded to old store issuance.
Imports SBDal
Imports SBModel
Public Class frmDepartmentWiseProduction
    Implements IGeneral
    Dim Model As DepartmentWiseProductionModel
    Dim ModelDetail As DepartmentWiseProductionDetailsModel
    Dim DAL As DepartmentWiseproductionDAL
    Dim DALDetail As DepartmentWiseProductionDetailDAL
    Dim BAL As New DepartmentWiseProductionBAL
    Dim ID As Integer = 0
    Dim RemainingQty As Double = 0
    Dim IsEditMode As Boolean = False
    Dim IsDetailEditMode As Boolean = False
    Dim GridQty As Double = 0
    Dim RowIndex As Integer
    Dim SelectedRowQty As Double = 0
    Dim QtyBeforeChange As Double = 0
    Enum Master
        ID
        DocNo
        [Date]
        DepartmentID
        Department
        SpecialInstructions
        ReferenceNo
    End Enum
    Enum Detail
        ID
        DepartmentWiseProductionID
        SalesOrderID
        PlanID
        TicketID
        ArticleID
        ArticleCode
        ArticleDescription
        UnitName
        Qty
        Remarks
        DepartmentID
        LocationId
    End Enum

    Private Sub txtDocNo_TextChanged(sender As Object, e As EventArgs) Handles txtDocNo.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles lblDate.Click

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            BAL.Delete(ID)
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
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
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable Where PoId =" & Me.cmbSalesOrder.Value & " Order by PlanDate DESC"
                FillUltraDropDown(cmbPlan, Str)
                Me.cmbPlan.Rows(0).Activate()
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Plans" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable Order by PlanDate DESC"
                FillUltraDropDown(cmbPlan, Str)
                Me.cmbPlan.Rows(0).Activate()
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPlan.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Ticket" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.Value & " Order by Ticket.ProductionStartDate DESC"
                Str = "Select PlanTicketsMasterID, BatchNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster  Where BatchNo <> '' And PlanId = " & Me.cmbPlan.Value & " Order By PlanTicketsMasterID DESC" ''And PlanTicketsMasterID Not In(Select IsNull(TicketID, 0) As TicketID FROM DepartmentWiseProductionDetail)
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.Rows(0).Activate()
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbTicket.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Tickets" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Order by Ticket.ProductionStartDate DESC"
                Str = "Select PlanTicketsMasterID, BatchNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Where BatchNo <> '' Order By PlanTicketsMasterID DESC"
                FillUltraDropDown(cmbTicket, Str)
                Me.cmbTicket.Rows(0).Activate()
                'ElseIf Condition = "Estimation" Then
                '    Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation Where  MaterialEstimation.PlanTicketId = " & cmbTicket.Value & " Order by MaterialEstimation.EstimationDate DESC"
                '    FillUltraDropDown(cmbEstimation, Str)
                '    Me.cmbEstimation.Rows(0).Activate()
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
                'ElseIf Condition = "EstimationByPlan" Then
                '    Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation Where  MaterialEstimation.MasterPlanId = " & cmbPlan.Value & " Order by MaterialEstimation.EstimationDate DESC"
                '    FillUltraDropDown(cmbEstimation, Str)
                '    Me.cmbEstimation.Rows(0).Activate()
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
                'ElseIf Condition = "Estimations" Then
                '    Str = "Select MaterialEstimation.Id, MaterialEstimation.DocNo + ' ~ ' + Convert(Varchar(12), MaterialEstimation.EstimationDate, 113) As MaterialEstimationNo, MaterialEstimation.PlanItemId From MaterialEstimation ORDER BY MaterialEstimation.EstimationDate DESC"
                '    FillUltraDropDown(cmbEstimation, Str)
                '    Me.cmbEstimation.Rows(0).Activate()
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(0).Hidden = True
                '    Me.cmbEstimation.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Product" Then
                'Str = "SELECT DISTINCT ArticleDefTableMaster.ArticleId as ArticleId, ArticleDefTableMaster.ArticleCode , ArticleDefTableMaster.ArticleDescription as Product, ArticleUnitDefTable.ArticleUnitName As UnitName, ISNULL(ArticleDefTableMaster.PurchasePrice,0) as Price, Isnull(ArticleDefTableMaster.AccountID,0) as AccountId, PlanTicketsMaster.PlanTicketsMasterID As TicketID, PlanTicketsDetail.Quantity, Sum(IsNull(PlanTicketsDetail.Quantity, 0)-IsNull(DepartmentWiseProductionDetail.Qty, 0)) As RemainingQty FROM ArticleDefTableMaster Inner Join PlanTicketsDetail On ArticleDefTableMaster.ArticleId = PlanTicketsDetail.ArticleId INNER JOIN PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID LEFT JOIN DepartmentWiseProductionDetail ON PlanTicketsMaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID  " _
                '    & " LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                '   & " Where PlanTicketsMaster.PlanTicketsMasterID = " & Me.cmbTicket.Value & " And ArticleDefTableMaster.Active = 1 Group by ArticleDefTableMaster.ArticleId, ArticleDefTableMaster.ArticleCode, ArticleDefTableMaster.ArticleDescription, ArticleUnitDefTable.ArticleUnitName, ArticleDefTableMaster.PurchasePrice, ArticleDefTableMaster.AccountId, PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsDetail.Quantity"

                ''TASK TFS1083 replaced ArticleDefTableMaster with ArticleDefTable 
                Str = "SELECT DISTINCT ArticleDefTable.ArticleId as ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription as Product, ArticleUnitDefTable.ArticleUnitName As UnitName, ISNULL(ArticleDefTable.PurchasePrice,0) as Price, Isnull(ArticleDefTable.AccountID,0) as AccountId, PlanTicketsMaster.PlanTicketsMasterID As TicketID, PlanTicketsDetail.Quantity, Sum(IsNull(PlanTicketsDetail.Quantity, 0)-IsNull(DepartmentWiseProductionDetail.Qty, 0)) As RemainingQty, ISNULL(PlanTicketsDetail.LocationId, 0) AS LocationId FROM ArticleDefTable Inner Join PlanTicketsDetail On ArticleDefTable.ArticleId = PlanTicketsDetail.ArticleId INNER JOIN PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID LEFT JOIN DepartmentWiseProductionDetail ON PlanTicketsMaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID  " _
                  & " LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                 & " Where PlanTicketsMaster.PlanTicketsMasterID = " & Me.cmbTicket.Value & " And ArticleDefTable.Active = 1 Group by ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleUnitDefTable.ArticleUnitName, ArticleDefTable.PurchasePrice, ArticleDefTable.AccountId, PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsDetail.Quantity, PlanTicketsDetail.LocationId "
                ''END TFS1083
                'Str = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription As Product, MaterialEstimationDetailTable.Quantity, MaterialEstimationDetailTable.Id, MaterialEstimationDetailTable.PlanItemId, IsNull(MaterialEstimationDetailTable.Quantity, 0 ) - Sum(IsNull(AllocationDetail.Quantity, 0)) As RemainingQty  From ArticleDefTable Inner Join MaterialEstimationDetailTable On ArticleDefTable.ArticleId = MaterialEstimationDetailTable.ProductId Inner Join MaterialEstimation On MaterialEstimationDetailTable.MaterialEstMasterID = MaterialEstimation.Id Left Outer Join AllocationDetail ON MaterialEstimationDetailTable.Id = AllocationDetail.MaterialEstimationDetailId " _
                '     & " Where MaterialEstimationDetailTable.MaterialEstMasterID = " & Me.cmbEstimation.Value & "  Group by ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, MaterialEstimationDetailTable.Quantity, MaterialEstimationDetailTable.Id, MaterialEstimationDetailTable.PlanItemId "
                FillUltraDropDown(cmbProduct, Str)
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True

                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Combination").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns(1).Width = 200
            ElseIf Condition = "Department" Then
                'ElseIf Condition = "SubDepartment" Then
                '    FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY 2 ASC")
                Str = "Select Distinct ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY ProdStep_Id ASC"
                FillUltraDropDown(cmbDepartment, Str)
                Me.cmbDepartment.Rows(0).Activate()
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns(1).Width = 200
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ''// It has been completed and should be uncommented when model classes get completed.
            Model = New DepartmentWiseProductionModel
            Model.ID = ID
            Model.DocNo = Me.txtDocNo.Text
            Model.DwpDate = dtpDate.Value
            Model.DepartmentID = Me.cmbDepartment.Value
            Model.SpecialInstructions = Me.txtSI.Text
            Model.ReferenceNo = Me.txtReference.Text
            For index As Int32 = 0 To Me.grd.RowCount - 1
                ModelDetail = New DepartmentWiseProductionDetailsModel
                ModelDetail.ID = Val(Me.grd.GetRows(index).Cells("ID").Value.ToString)
                ModelDetail.DepartmentWiseProductionID = Val(Me.grd.GetRows(index).Cells("DepartmentWiseProductionID").Value.ToString)
                ModelDetail.SalesOrderID = Val(Me.grd.GetRows(index).Cells("SalesOrderID").Value.ToString)
                ModelDetail.PlanID = Val(Me.grd.GetRows(index).Cells("PlanID").Value.ToString)
                ModelDetail.TicketID = Val(Me.grd.GetRows(index).Cells("TicketID").Value.ToString)
                ModelDetail.ArticleID = Val(Me.grd.GetRows(index).Cells("ArticleID").Value.ToString)
                ModelDetail.Qty = Val(Me.grd.GetRows(index).Cells("Qty").Value.ToString)
                ModelDetail.Remarks = Me.grd.GetRows(index).Cells("Remarks").Value.ToString
                ModelDetail.DepartmentID = Val(Me.grd.GetRows(index).Cells("DepartmentID").Value.ToString)
                ModelDetail.LocationId = Val(Me.grd.GetRows(index).Cells("LocationId").Value.ToString)

                Model.Detail.Add(ModelDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Dim dt As DataTable = BAL.GetMaster()
            Me.grdSaved.DataSource = BAL.GetMaster()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ID").Visible = False
            Me.grdSaved.RootTable.Columns("DepartmentID").Visible = False
            Me.grdSaved.RootTable.Columns("DocNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Date").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Department").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("SpecialInstructions").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("ReferenceNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("DepartmentID").Visible = False ''DWP.ID, DWP.DocNo, DWP.Date, DWP.DepartmentID, tblproSteps.prod_step As Department, DWP.SpecialInstructions
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocNo.Text = "" Then
                Me.txtDocNo.Focus() : IsValidate = False : Exit Function
            ElseIf Me.grd.RowCount <= 0 Then
                Me.grd.Focus() : IsValidate = False : Exit Function
            Else
                Me.grd.UpdateData()
                FillModel()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = NewDocNo()
            Me.dtpDate.Value = Now
            Me.txtSI.Text = ""
            Me.txtReference.Text = ""
            FillCombos("SO")
            FillCombos("Plans")
            FillCombos("Department")
            FillCombos("Ticket")
            FillCombos("Product")
            'If Not Me.cmbDepartment.ActiveRow Is Nothing Then
            '    Me.cmbDepartment.Rows(0).Activate()
            'End If

            ' '' To reset detail fields
            'If Not Me.cmbSalesOrder.ActiveRow Is Nothing Then
            '    Me.cmbSalesOrder.Rows(0).Activate()
            'End If
            'If Not Me.cmbPlan.ActiveRow Is Nothing Then
            '    Me.cmbPlan.Rows(0).Activate()
            'End If
            'If Not Me.cmbTicket.ActiveRow Is Nothing Then
            '    Me.cmbTicket.Rows(0).Activate()
            'End If
            'If Not Me.cmbProduct.ActiveRow Is Nothing Then
            '    Me.cmbProduct.Rows(0).Activate()
            'End If
            ''Clearing of detail fields
            Me.txtReqQty.Text = ""
            Me.txtQty.Text = ""
            Me.txtRemarks.Text = ""
            Me.btnDelete.Visible = False
            Me.btnSave.Text = "&Save"
            If Me.btnAdd.Text = "Edit" Then
                Me.btnAdd.Text = "Add"
            End If
            GetAllRecords()
            DisplayDetail(-1)
            ResetDetailControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            BAL.Save(Model)
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
        Try
            BAL.Update(Model)
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function
    Private Function NewDocNo() As String
        Dim MENo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                MENo = GetSerialNo("DW" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "DepartmentWiseProduction", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("DW" & "-" & Microsoft.VisualBasic.Strings.Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "DepartmentWiseProduction", "DocNo")
            Else
                MENo = GetNextDocNo("DW", 6, "DepartmentWiseProduction", "DocNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbProduct_Leave(sender As Object, e As EventArgs) Handles cmbProduct.Leave
        Try
            If Me.cmbProduct.Value > 0 AndAlso IsDetailEditMode = False Then
                'QuantityCalculation()
                Dim DepartmentID As Integer = IIf(Me.cmbDepartment.ActiveRow Is Nothing, 0, Me.cmbDepartment.Value)
                Me.txtReqQty.Text = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value) - GetDepartmentWiseTotal(DepartmentID, Me.cmbTicket.Value, Me.cmbProduct.Value)
            End If
            'Me.txtReqQty.Text = Val(Me.cmbProduct.SelectedRow.Cells("TicketQuantity").Value.ToString)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDepartmentWiseProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicket.ValueChanged
        Try
            If Not Me.cmbTicket.Value <= 0 Then
                FillCombos("Product")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesOrder_ValueChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.ValueChanged
        Try
            If Not Me.cmbSalesOrder.Value <= 0 Then
                FillCombos("Plan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_ValueChanged(sender As Object, e As EventArgs) Handles cmbPlan.ValueChanged
        Try
            If Not Me.cmbPlan.Value <= 0 Then
                FillCombos("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddToGrid()
        Try
            If Val(Me.txtQty.Text) <= 0 Then
                msg_Error("Quanity should be larger than zero")
                Me.txtQty.Focus()
                Exit Sub
            End If

            If Me.btnAdd.Text = "Add" Then
                If Val(Me.txtReqQty.Text) < Val(Me.txtQty.Text) Then
                    If Not msg_Confirm("Do you want to add larger quantity than available") = True Then
                        Me.txtQty.Focus()
                        Exit Sub
                    End If

                End If

            Else
                Dim RemainingIncaseEdit As Double = Val(Me.txtReqQty.Text) + GridQty
                If GridQty - SelectedRowQty + Val(Me.txtQty.Text) > RemainingIncaseEdit Then
                    If Not msg_Confirm("Do you want to add larger quantity than available " & RemainingIncaseEdit & "") = True Then
                        Me.txtQty.Focus()
                        Exit Sub
                    End If
                    'msg_Error("Quanity must be less than remaining quantity of " & RemainingIncaseEdit & "")
                    'Me.txtQty.Focus()
                    'Exit Sub
                End If
            End If
            If Me.cmbProduct.Value <= 0 Then
                msg_Error("Product is required")
                Me.cmbProduct.Focus()
                Exit Sub
            End If
            ''TASK TFS2780
            If DepartmentWiseProductionBAL.IsStageApproved(Me.cmbTicket.Value, Me.cmbDepartment.Value) = False Then
                msg_Error("QC Verification is not Approved")
                Me.cmbDepartment.Focus()
                Exit Sub
            End If
            ''END TASK TFS2780

            Dim dt As New DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            If Me.btnAdd.Text = "Edit" Then
                dt.Rows.RemoveAt(RowIndex)
            End If
            dt.AcceptChanges()
            Dim dr As DataRow
            dr = dt.NewRow
            dr(Detail.ID) = 0
            dr(Detail.DepartmentWiseProductionID) = 0
            dr(Detail.SalesOrderID) = Val(Me.cmbSalesOrder.Value.ToString)
            dr(Detail.PlanID) = Val(Me.cmbPlan.Value.ToString)
            dr(Detail.TicketID) = Val(Me.cmbTicket.Value.ToString)
            dr(Detail.ArticleID) = Me.cmbProduct.Value
            dr(Detail.ArticleCode) = Me.cmbProduct.SelectedRow.Cells("ArticleCode").Value.ToString
            dr(Detail.ArticleDescription) = Me.cmbProduct.SelectedRow.Cells("Product").Value.ToString
            dr(Detail.UnitName) = Me.cmbProduct.SelectedRow.Cells("UnitName").Value.ToString
            dr(Detail.Qty) = Val(Me.txtQty.Text)
            dr(Detail.Remarks) = Me.txtRemarks.Text
            dr(Detail.DepartmentID) = IIf(Me.cmbDepartment.ActiveRow Is Nothing, 0, Me.cmbDepartment.Value)
            dr(Detail.LocationId) = Me.cmbProduct.SelectedRow.Cells("LocationId").Value
            If Me.btnAdd.Text = "Edit" Then
                dt.Rows.InsertAt(dr, RowIndex)
            Else
                dt.Rows.Add(dr)
            End If
            'QuantityCalculation()
            Me.txtReqQty.Text -= Val(txtQty.Text)
            Me.txtQty.Text = ""
            Me.txtRemarks.Text = ""
            If Me.btnAdd.Text = "Edit" Then
                Me.btnAdd.Text = "Add"
                IsDetailEditMode = False
                Me.cmbSalesOrder.Enabled = True
                Me.cmbPlan.Enabled = True
                Me.cmbTicket.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDepartmentWiseProduction)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
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



            Me.grd.UpdateData()
            Dim ArticleID As Integer = 0
            If Me.cmbProduct.Rows.Count > 0 Then
                ArticleID = Me.cmbProduct.SelectedRow.Cells("ArticleId").Value
            Else
                Exit Sub
            End If
            'Dim Total As Double = Me.grd.GetTotal()
            'Dim GridexFilterCondtion1 As New Janus.Windows.GridEX.GridEXFilterCondition()
            Dim DepartmentID As Integer = IIf(Me.cmbDepartment.ActiveRow Is Nothing, 0, Me.cmbDepartment.Value)

            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grd.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, ArticleID)
            Dim GridQty As Double = Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)

            RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
            Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value.ToString)
            Dim AllocatedQty As Double = Quantity - RemainingQty
            Dim TotalAllocatedQty = AllocatedQty + GridQty
            If TotalAllocatedQty > 0 Then
                Me.txtReqQty.Text = Quantity - TotalAllocatedQty
            ElseIf Not GridQty > 0 Then
                Me.txtReqQty.Text = RemainingQty
            Else
                Me.txtReqQty.Text = Quantity
                'Me.txtReqQty.Text = RemainingQty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then

                'Me.grd.UpdateData()
                Dim DetailID As Integer = Me.grd.GetRow.Cells(Detail.ID).Value
                Dim Qty As Double = Val(Me.grd.GetRow.Cells("Qty").Value.ToString)
                If DetailID > 0 Then
                    BAL.DeleteDetailRow(DetailID)
                End If
                If Not Me.cmbProduct.ActiveRow Is Nothing AndAlso Not Me.cmbTicket.ActiveRow Is Nothing AndAlso Not Me.cmbDepartment.ActiveRow Is Nothing Then
                    If Me.cmbProduct.Value = Me.grd.GetRow.Cells(Detail.ArticleID).Value AndAlso Me.cmbTicket.Value = Me.grd.GetRow.Cells(Detail.TicketID).Value AndAlso Me.cmbDepartment.Value = Me.grd.GetRow.Cells(Detail.DepartmentID).Value Then
                        Me.txtReqQty.Text += Val(Qty)
                    End If
                End If
                Me.grd.GetRow.Delete()
                'QuantityCalculation()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("SO")
            FillCombos("Plans")
            FillCombos("Department")
            ReSetControls()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecord(Optional Condition As String = "")
        '        ID	int	Unchecked
        'DocNo	nvarchar(MAX)	Checked
        'Date	datetime	Checked
        'DepartmentID	int	Checked
        'SpecialInstructions	text	Checked
        Try
            IsEditMode = True
            'Mode = "Edit"
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            ID = Me.grdSaved.CurrentRow.Cells("ID").Value
            Me.txtDocNo.Text = Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.CurrentRow.Cells("Date").Value
            Me.cmbDepartment.Value = Me.grdSaved.CurrentRow.Cells("DepartmentID").Value
            Me.txtSI.Text = Me.grdSaved.CurrentRow.Cells("SpecialInstructions").Value.ToString
            Me.txtReference.Text = Me.grdSaved.CurrentRow.Cells("ReferenceNo").Value.ToString
            FillCombos("SO")
            FillCombos("Plans")
            FillCombos("Ticket")
            FillCombos("Product")
            'Me.cmbSalesOrder.Rows(0).Activate()
            'Me.cmbPlan.Rows(0).Activate()
            'If Not Me.cmbTicket.ActiveRow Is Nothing Then
            '    Me.cmbTicket.Rows(0).Activate()
            'End If
            'If Not Me.cmbProduct.ActiveRow Is Nothing Then
            '    Me.cmbProduct.Rows(0).Activate()
            'End If
            'FillCombos("Product")
            'Me.cmbTicket.Rows(0).Activate()
            DisplayDetail(ID)
            Me.btnDelete.Visible = True
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                'If ValidateQty() Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() Then
                        ReSetControls()
                    End If
                Else
                    If Update1() Then
                        ReSetControls()
                    End If
                End If
                'Else
                '    Exit Sub
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbProduct.ValueChanged
        Try
            If Me.cmbProduct.Value > 0 AndAlso IsDetailEditMode = False Then
                'QuantityCalculation()
                Dim DepartmentID As Integer = IIf(Me.cmbDepartment.ActiveRow Is Nothing, 0, Me.cmbDepartment.Value)
                Me.txtReqQty.Text = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value) - GetDepartmentWiseTotal(DepartmentID, Me.cmbTicket.Value, Me.cmbProduct.Value)
            End If
            'Me.txtReqQty.Text = Val(Me.cmbProduct.SelectedRow.Cells("TicketQuantity").Value.ToString)
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
    Private Sub DisplayDetail(ByVal ID As Integer)
        Try
            Me.grd.DataSource = BAL.GetDetails(ID)
            Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Delete() Then
                ReSetControls()
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
    Private Sub EditDetailRecord()
        Try
            'RowIndex = Me.grd.GetRow.RowIndex
            'Dim TicketID As Integer = Me.grd.GetRow.Cells("TicketID").Value
            'Dim CurrentRowQty As Double = Me.grd.GetRow.Cells("Qty").Value
            ''Me.grd.
            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grd.RootTable.Columns("TicketID"), Janus.Windows.GridEX.ConditionOperator.Equal, TicketID)
            'GridQty = Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
            ''GridQty += CurrentRowQty
            ''Dim RemainingQty As Double = GetTicketWiseRemaining(Me.grd.GetRow.Cells("TicketID").Value)
            'Dim TotalTicketQty = GetTicketQty(TicketID)
            'Dim IssuedTicketQty As Double = GetDeptWiseTicketQty(TicketID)
            Me.cmbSalesOrder.Value = Me.grd.GetRow().Cells("SalesOrderID").Value
            Me.cmbPlan.Value = Me.grd.GetRow.Cells("PlanID").Value
            Me.cmbTicket.Value = Me.grd.GetRow.Cells("TicketID").Value
            Me.cmbProduct.Value = Me.grd.GetRow.Cells("ArticleID").Value
            'Me.txtReqQty.Text = TotalTicketQty - GridQty ''GetGridComparingRemaining(Me.grd.GetRow.Cells("TicketID").Value, Me.grd.GetRow.Cells("ArticleID").Value) - GridQty
            'SelectedRowQty = Val(Me.grd.GetRow.Cells("Qty").Value.ToString)
            'Me.txtQty.Text = SelectedRowQty
            Me.txtRemarks.Text = Me.grd.GetRow.Cells("Remarks").Value.ToString
            'Me.btnAdd.Text = "Edit"
            IsDetailEditMode = True
            Me.cmbSalesOrder.Enabled = False
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetGridComparingRemaining(ByVal TicketID As Integer, ByVal ArticleID As Integer) As Double
        Dim Str As String = ""
        Dim dt As New DataTable
        Try
            Str = "Select Sum(IsNull(Qty, 0)) FROM DepartmentWiseProductionDetail Where TicketID = " & TicketID & " And ArticleID =" & ArticleID & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Return Val(dt.Rows(0).Item(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetTicketQty(ByVal TicketID As Integer) As Double
        Dim Str As String = ""
        Dim dt As New DataTable
        Try
            Str = "Select Sum(IsNull(TicketQuantity, 0)) FROM PlanTickets Where PlanTicketsId = " & TicketID & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Return Val(dt.Rows(0).Item(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetDeptWiseTicketQty(ByVal TicketID As Integer) As Double
        Dim Str As String = ""
        Dim dt As New DataTable
        Try
            Str = "Select Sum(IsNull(Qty, 0)) FROM DepartmentWiseProductionDetail Where TicketID = " & TicketID & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Return Val(dt.Rows(0).Item(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetTicketWiseRemaining(ByVal TicketID As Integer) As Double
        Dim Str As String = ""
        Dim dt As New DataTable
        Try
            Str = "Select Sum(IsNull(PlanTickets.TicketQuantity, 0) - IsNull(DepartmentWiseProductionDetail.Qty, 0)) As RemQty FROM PlanTickets INNER JOIN DepartmentWiseProductionDetail ON PlanTickets.PlanTicketsId = DepartmentWiseProductionDetail.TicketID Where PlanTickets.PlanTicketsId = " & TicketID & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Return Val(dt.Rows(0).Item(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ID", ID)
            ShowReport("rptDepartmentWiseProduction")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            EditDetailRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.txtQty.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtReqQty.Text = ""
            Me.cmbSalesOrder.Enabled = True
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
            If Not Me.cmbProduct.SelectedRow Is Nothing Then
                Me.cmbProduct.Rows(0).Activate()
            End If
            If Not Me.cmbSalesOrder.SelectedRow Is Nothing Then
                Me.cmbSalesOrder.Rows(0).Activate()
            End If
            If Not Me.cmbPlan.SelectedRow Is Nothing Then
                Me.cmbPlan.Rows(0).Activate()
            End If
            If Not Me.cmbTicket.SelectedRow Is Nothing Then
                Me.cmbTicket.Rows(0).Activate()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited

    End Sub
    Private Sub QtyUpdated()
        Try
            If grd.GetRow.Cells("Qty").DataChanged = True Then
                'grd.UpdateData()
                Dim NewQty As Double = Val(Me.grd.GetRow.Cells("Qty").Value.ToString)
                Dim DetailID As Double = Val(Me.grd.GetRow.Cells("ID").Value.ToString)
                Dim TicketID As Integer = Me.grd.GetRow.Cells("TicketID").Value
                Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grd.RootTable.Columns("TicketID"), Janus.Windows.GridEX.ConditionOperator.Equal, Val(TicketID))
                GridQty = Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
                Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("TicketQuantity").Value.ToString)
                Dim IssuedTicketQty As Double = GetDeptWiseTicketQty(TicketID)
                Dim TicketQty As Double = GetTicketQty(TicketID)

                Dim AllocatedQty As Double = Quantity - RemainingQty
                Dim TotalAllocatedQty = AllocatedQty + GridQty
                If Me.btnSave.Text = "&Update" Then
                    If GridQty > TicketQty Then
                        If msg_Confirm("You have entered the Qty more than available. Would you proceed?") = True Then
                            UpdateQty(DetailID, TicketID, NewQty)
                            DisplayDetail(ID)
                        Else
                            Exit Sub
                        End If
                    End If
                Else
                    If GridQty + IssuedTicketQty > TicketQty Then
                        If msg_Confirm("You have entered the Qty more than available. Would you proceed?") = True Then
                            Exit Sub
                        Else
                            DisplayDetail(ID)
                        End If
                    End If

                End If
                If TotalAllocatedQty > 0 Then
                    Me.txtReqQty.Text = Quantity - TotalAllocatedQty
                ElseIf Not GridQty > 0 Then
                    Me.txtReqQty.Text = RemainingQty
                Else
                    Me.txtReqQty.Text = Quantity
                    'Me.txtReqQty.Text = RemainingQty
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UpdateQty(ByVal ID As Integer, ByVal TicketID As Integer, ByVal Qty As Double)
        Dim connection As New OleDb.OleDbConnection
        Dim cmd As New OleDb.OleDbCommand
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim trans As OleDb.OleDbTransaction = connection.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = connection
            cmd.Transaction = trans
            cmd.CommandText = "Update DepartmentWiseProductionDetail Set Qty = IsNull(Qty, 0) + " & TicketID & " Where TicketID = " & TicketID & " And ID = " & ID & " "
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Private Function GetTicketsAgainstSelectedID(ByVal Id As Integer) As DataTable
        Dim dt As New DataTable
        Dim query As String = String.Empty
        Try
            query = "Select DepartmentWiseProductionDetail.TicketId, PlanTickets.TicketNo From DepartmentWiseProductionDetail INNER JOIN PlanTickets ON DepartmentWiseProductionDetail.TicketID = PlanTickets.PlanTicketsId"
            dt = GetDataTable(query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Function ValidateQty() As Boolean
        Dim dt As New DataTable
        Try
            'GetTicketsAgainstSelectedID(ID)
            dt = CType(grd.DataSource, DataTable)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grd.RootTable.Columns("TicketID"), Janus.Windows.GridEX.ConditionOperator.Equal, Val(dr("TicketID").ToString))
                    Dim GridQty As Double = Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                    Dim IssuedQty As Double = GetTicketWiseRemaining(Val(dr("TicketID").ToString))
                    Dim TicketQty As Double = GetTicketQty(Val(dr("TicketID").ToString))
                    If IssuedQty > 0 AndAlso GridQty > TicketQty Then
                        If msg_Confirm("Entered quantity is larger than available quantity. Would you proceed?") = True Then
                            Return True
                        Else
                            Return False
                        End If
                    ElseIf GridQty > TicketQty Then
                        If msg_Confirm("Entered quantity is larger than available quantity. Would you proceed?") = True Then
                            Return True
                        Else
                            Return False

                        End If
                    Else
                        Return True

                    End If
                Next
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
    '' Generate this method to get total Qty of DepartmentWiseDetail against a Department. By Ameen
    Private Function GetDepartmentWiseTotal(ByVal DepartmentID As Integer, ByVal TicketID As Integer, ByVal ArticleID As Integer)
        Dim dt As New DataTable
        Dim Str As String = String.Empty
        Try
            Str = "Select TicketID, ArticleID, Sum(IsNull(Qty, 0)) As Qty From DepartmentWiseProductionDetail INNER JOIN DepartmentWiseProduction ON DepartmentWiseProductionDetail.DepartmentWiseProductionID = DepartmentWiseProduction.ID Where DepartmentWiseProduction.DepartmentID = " & DepartmentID & " And DepartmentWiseProductionDetail.TicketID =" & TicketID & " And DepartmentWiseProductionDetail.ArticleID = " & ArticleID & " Group by TicketID, ArticleID"
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(2).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class