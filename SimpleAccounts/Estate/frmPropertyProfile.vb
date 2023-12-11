'10-Feb-2018 TFS2335 : Ayesha Rehman: Added a new form Property Profile To Creat Property Profiles
Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Public Class frmPropertyProfile

    Implements IGeneral
    Public Shared Id As Integer = 0I
    Private AgentID As Integer = 0I
    Dim PropertyProfile As PropertyProfileBE
    Public ProProfileDAL As PropertyProfileDAL = New PropertyProfileDAL()
    Dim PropertyProfileAgentDealer As PropertyProfileAgentDealerBE
    Dim PropertyProfileAgentDealerDAL As PropertyProfileAgentDealerDAL = New PropertyProfileAgentDealerDAL()
    Dim objModel As InvestmentBookingBE
    Dim objDAL As InvestmentBookingDAL = New InvestmentBookingDAL()
    Public IsEditMode As Boolean = False
    Dim CurrentDocNo As String = String.Empty
    Public CostCenterId As Integer = 0
    Public DoHaveSaveRights As Boolean = True
    Public DoHaveUpdateRights As Boolean = True
    Public IsDealCompleted As Boolean = False
    Public DoHaveDealRights As Boolean = True
    ''' <summary>
    ''' Ayesha Rehman : Load Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub frmPropertyProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("PropertyItem")
            FillCombos("Branch")
            GetById(Id)
            If IsEditMode = True Then
                GetInvestors()
                GetAgentDealers()
                GetTasks()
                GetLedger()
                GetSeller()
                GetBuyer()
                btnSaller.Visible = True
                btnBuyer.Visible = True
                ''Start TFS4496
                Me.txtCommission.Text = GetCommissionFromAccount()
                Me.txtNDC.Text = GetNDcExpenseFromAccount()
                Me.txtOtherExpense.Text = GetOtherExpenseFromAccount()
                ''End TFS4496
            Else
                btnSaller.Visible = False
                btnBuyer.Visible = False
            End If
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
            btnMore.FlatAppearance.BorderSize = 0
            btnMore.FlatAppearance.MouseOverBackColor = btnMore.BackColor
            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
            BtnPrint.FlatAppearance.BorderSize = 0
            BtnPrint.FlatAppearance.MouseOverBackColor = BtnPrint.BackColor
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            If IsEditMode = False Then
                Me.btnDealCompletion.Visible = False
                Me.btnDealcompleted.Visible = False
            Else
                If Me.IsDealCompleted = False Then
                    Me.btnDealcompleted.Visible = False
                    Me.btnDealcompleted.SendToBack()
                Else
                    Me.btnDealcompleted.Visible = True
                    Me.btnDealcompleted.BringToFront()
                    'Me.btnDealCompletion.Visible = False
                    'Me.btnDealCompleted.SendToBack()
                End If
                Me.btnDealCompletion.Visible = True
                'Me.btnDealcompleted.BringToFront()
                Me.btnDealCompletion.Enabled = DoHaveDealRights
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub GetById(ByVal Id As Integer)
        Try
            Dim dt As DataTable = New PropertyProfileDAL().GetById(Id)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("PropertyProfileId") > 0 Then
                ShowData(dt)
            Else
                ReSetControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal dt As DataTable)
        Try
            IsEditMode = True
            Id = dt.Rows(0).Item("PropertyProfileId")
            dtpDocDate.Value = dt.Rows(0).Item("DocDate")
            txtDocNo.Text = dt.Rows(0).Item("DocNo").ToString
            CurrentDocNo = txtDocNo.Text
            Me.cmbBranch.Value = Val(dt.Rows(0).Item("BranchId").ToString)
            Me.cmbPropertyItem.Value = Val(dt.Rows(0).Item("InvId").ToString)
            btnSave.Enabled = DoHaveUpdateRights
            btnDealCompletion.Enabled = DoHaveDealRights
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            '// This grid setting will be used for investor grid
            If Condition = "Investor" Then
                '// Disable grid editing
                Me.grdInvestor.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

                '// Hiding InvestmentBookingId
                Me.grdInvestor.RootTable.Columns("InvestmentBookingId").Visible = False

                '// Hiding InvestorId
                Me.grdInvestor.RootTable.Columns("InvestorId").Visible = False
                Me.grdInvestor.RootTable.Columns("InvReceived").Visible = False
                Me.grdInvestor.RootTable.Columns("InvBalance").Visible = False
                Me.grdInvestor.RootTable.Columns("VoucherNo").Visible = False


                '// Setting format string
                Me.grdInvestor.RootTable.Columns("InvAmount").FormatString = "N"
                Me.grdInvestor.RootTable.Columns("InvReceived").FormatString = "N"
                Me.grdInvestor.RootTable.Columns("InvBalance").FormatString = "N"
                Me.grdInvestor.RootTable.Columns("Profit").FormatString = "N"

                '// Aligning Headers
                Me.grdInvestor.RootTable.Columns("InvAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("InvReceived").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("InvBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("Profit").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                '// Aligning values
                Me.grdInvestor.RootTable.Columns("InvAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("InvReceived").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("InvBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdInvestor.RootTable.Columns("Profit").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                '// Display total row
                Me.grdInvestor.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdInvestor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

                '// Setting total row summary option
                Me.grdInvestor.RootTable.Columns("InvAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdInvestor.RootTable.Columns("InvReceived").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdInvestor.RootTable.Columns("InvBalance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdInvestor.RootTable.Columns("Profit").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                '// Setting total format string
                Me.grdInvestor.RootTable.Columns("InvAmount").TotalFormatString = "N"
                Me.grdInvestor.RootTable.Columns("InvReceived").TotalFormatString = "N"
                Me.grdInvestor.RootTable.Columns("InvBalance").TotalFormatString = "N"
                Me.grdInvestor.RootTable.Columns("Profit").TotalFormatString = "N"

                If Me.grdInvestor.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdInvestor.RootTable.Columns.Add("Delete")
                    Me.grdInvestor.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdInvestor.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdInvestor.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdInvestor.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdInvestor.RootTable.Columns("Delete").Width = 50
                    Me.grdInvestor.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdInvestor.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdInvestor.RootTable.Columns("Delete").Caption = "Action"
                End If

            End If

            If Condition = "Agent" Then
                Me.grdAgents.RootTable.Columns("Id").Visible = False
                Me.grdAgents.RootTable.Columns("PropertyProfileId").Visible = False
                Me.grdAgents.RootTable.Columns("AccountId").Visible = False
                Me.grdAgents.RootTable.Columns("VoucherNo").Visible = False
                Me.grdAgents.RootTable.Columns("CommissionAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdAgents.RootTable.Columns("CommissionAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdAgents.RootTable.Columns("CommissionAmount").FormatString = "N" & DecimalPointInValue
                Me.grdAgents.RootTable.Columns("CommissionAmount").TotalFormatString = "N" & DecimalPointInValue
                If Me.grdAgents.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdAgents.RootTable.Columns.Add("Delete")
                    Me.grdAgents.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdAgents.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdAgents.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdAgents.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdAgents.RootTable.Columns("Delete").Width = 20
                    Me.grdAgents.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdAgents.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdAgents.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If

            If Condition = "Ledger" Then
                '// Disable grid editing
                Me.grdLedger.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

                '// Setting format string
                Me.grdLedger.RootTable.Columns("Dr").FormatString = "N"
                Me.grdLedger.RootTable.Columns("Cr").FormatString = "N"

                '// Setting total format string
                Me.grdLedger.RootTable.Columns("Dr").TotalFormatString = "N"
                Me.grdLedger.RootTable.Columns("Cr").TotalFormatString = "N"

                '// Aligning Headers
                Me.grdLedger.RootTable.Columns("Dr").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdLedger.RootTable.Columns("Cr").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                '// Aligning values
                Me.grdLedger.RootTable.Columns("Dr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdLedger.RootTable.Columns("Cr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                '// Display total row
                Me.grdLedger.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdLedger.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

                '// Setting total row summary option
                Me.grdLedger.RootTable.Columns("Dr").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdLedger.RootTable.Columns("Cr").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdLedger.RootTable.Columns("Dr").Caption = "Debit"
                Me.grdLedger.RootTable.Columns("Cr").Caption = "Credit"
                Me.grdLedger.RootTable.Columns("Dr").Width = 100
                Me.grdLedger.RootTable.Columns("Cr").Width = 100
                Me.grdLedger.RootTable.Columns("Account").Width = 400
                Me.grdLedger.RootTable.Columns("AccountType").Visible = False
                Me.grdLedger.RootTable.Columns("OtherExpenses").Visible = False
            End If
            '// This grid setting will be used for Tasks grid
            If Condition = "Tasks" Then
                '// Disable grid editing
                Me.grdTask.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

                '// Hiding TaskId
                Me.grdTask.RootTable.Columns("TaskId").Visible = False



                '// Display total row
                Me.grdTask.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdTask.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


                '// Setting format strings
                'Me.grdTask.RootTable.Columns("TaskDate").FormatString = "dd/MMM/yyyy"
                Me.grdTask.RootTable.Columns("DueDate").FormatString = "dd/MMM/yyyy"

                'Me.grd.RootTable.SortKeys.Add("TaskId", Janus.Windows.GridEX.SortOrder.Descending)


            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                BtnPrint.Enabled = True
                DoHaveDealRights = True
                Exit Sub
            End If

            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    BtnPrint.Enabled = False
                    DoHaveDealRights = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                BtnPrint.Enabled = False
                DoHaveDealRights = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        BtnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Deal Completion" Then
                        DoHaveDealRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdAgents_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAgents.ColumnButtonClick
        Try
            If Me.IsDealCompleted = False Then
                PropertyProfileAgentDealer = New PropertyProfileAgentDealerBE
                If Me.grdAgents.GetRow.Cells("Type").Value.ToString = "Dealer" Then
                    PropertyProfileAgentDealer.PropertyProfileDealerId = Val(Me.grdAgents.CurrentRow.Cells("Id").Value.ToString)
                    PropertyProfileAgentDealer.DealerId = Val(Me.grdAgents.CurrentRow.Cells("AccountId").Value.ToString)
                Else
                    PropertyProfileAgentDealer.PropertyProfileAgentId = Val(Me.grdAgents.CurrentRow.Cells("Id").Value.ToString)
                    PropertyProfileAgentDealer.AgentId = Val(Me.grdAgents.CurrentRow.Cells("AccountId").Value.ToString)
                End If
                PropertyProfileAgentDealer.VoucherNo = Me.grdAgents.CurrentRow.Cells("VoucherNo").Value.ToString
                If e.Column.Key = "Delete" Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    PropertyProfileAgentDealerDAL.Delete(PropertyProfileAgentDealer)
                    Me.grdAgents.GetRow.Delete()
                End If
            Else
                ShowErrorMessage("Deal has been completed.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "PropertyItem" Then
                str = "SELECT PropertyItem.PropertyItemId, PropertyItem.PlotNo,  PropertyType , PropertyItem.Title, PropertyItem.Sector, PropertyItem.Block, PropertyItem.PlotSize, tblListTerritory.TerritoryName, tblListCity.CityName" _
                    & "  FROM tblListTerritory INNER JOIN " _
                    & " tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " _
                    & " PropertyItem ON tblListTerritory.TerritoryId = PropertyItem.TerritoryId LEFT OUTER JOIN " _
                    & " PropertyType ON PropertyItem.PropertyTypeId = PropertyType.PropertyTypeId "
                '_& " Where PropertyItem.Active =1"
                FillUltraDropDown(Me.cmbPropertyItem, str, True)
                Me.cmbPropertyItem.DisplayLayout.Bands(0).Columns("PropertyItemId").Hidden = True
            ElseIf Condition = "Branch" Then
                str = "Select BranchId , Name  from Branch where Active = 1"
                FillUltraDropDown(Me.cmbBranch, str, True)
                If Me.cmbBranch.Rows.Count > 0 Then
                    Me.cmbBranch.Rows(0).Activate()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

            PropertyProfile = New PropertyProfileBE
            PropertyProfile.PropertyProfileId = Id
            PropertyProfile.DocNo = Me.txtDocNo.Text
            PropertyProfile.DocDate = Me.dtpDocDate.Value.ToString("yyyy-M-d h:mm:ss tt")
            PropertyProfile.InvId = Me.cmbPropertyItem.Value
            PropertyProfile.InvName = Me.cmbPropertyItem.Text
            PropertyProfile.Branch = New BEBranch(Me.cmbBranch.Value, cmbBranch.Text)
            PropertyProfile.CostCenterId = 0
            PropertyProfile.Status = "Open"
            If Not Id = 0 Then
                PropertyProfile.ModifiedUser = LoginUser.LoginUserName
                PropertyProfile.ModifiedDate = DateTime.Now.ToString("yyyy-M-d h:mm:ss tt")
            End If

            If txtCommission.Text.Length > 0 Then
                PropertyProfile.CommissionAmount = txtCommission.Text
            Else
                PropertyProfile.CommissionAmount = 0
            End If
            If Me.txtMargin.Text.Length > 0 Then
                PropertyProfile.Margin = txtMargin.Text
            Else
                PropertyProfile.Margin = 0
            End If
            PropertyProfile.ActivityLog = New ActivityLog

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbBranch.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please Select Branch")
                Me.cmbBranch.Focus()
                Return False
            End If
            If Me.cmbPropertyItem.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please Select a Property")
                Me.cmbPropertyItem.Focus()
                Return False
            End If


            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Id = 0
            'Me.btnSave.Text = "&Save"
            IsEditMode = False
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Now
            Me.cmbPropertyItem.Rows(0).Activate()
            Me.txtBuyerName.Text = String.Empty
            Me.txtBuyerCellNo.Text = String.Empty
            Me.txtBuyerPrice.Text = String.Empty
            Me.txtSellerName.Text = String.Empty
            Me.txtSellerCellNo.Text = String.Empty
            Me.txtSellerPrice.Text = String.Empty
            Me.TxtSellerBalance.Text = String.Empty
            Me.TxtBuyerBalance.Text = String.Empty
            btnSave.Enabled = DoHaveSaveRights
            btnDealCompletion.Enabled = DoHaveDealRights


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("PP" & Me.cmbBranch.SelectedRow.Cells(0).Value & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "PropertyProfile", "DocNo")
            Return DocNo
        Catch ex As Exception
            msg_Error(ex.Message)
            Return String.Empty
        End Try
    End Function
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            PropertyProfile.CostCenterId = GetCostCenterId(Id)
            If New PropertyProfileDAL().Update(PropertyProfile) Then
                SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtDocNo.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Function GetCostCenterId(ByVal ID As Integer) As Integer
        Dim strSQL As String = String.Empty

        Try

            strSQL = " Select CostCenterId  from PropertyProfile where PropertyProfileId = " & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                CostCenterId = Val(dt.Rows(0).Item("CostCenterId").ToString())
            End If
            Return CostCenterId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Id = 0 Then
                    If ProProfileDAL.Add(PropertyProfile) Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                        Me.Close()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information("Record has been updated successfully.")
                    ReSetControls()
                    Me.Close()
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPropertyItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbPropertyItem.ValueChanged
        Try
            If cmbPropertyItem.ActiveRow IsNot Nothing Then
                Me.txtBlock.Text = Me.cmbPropertyItem.ActiveRow.Cells("Block").Value.ToString
                Me.txtSector.Text = Me.cmbPropertyItem.ActiveRow.Cells("Sector").Value.ToString
                Me.txtPlotNumber.Text = Me.cmbPropertyItem.ActiveRow.Cells("PlotNo").Value.ToString
                Me.txtLandArea.Text = Me.cmbPropertyItem.ActiveRow.Cells("PlotSize").Value.ToString
                Me.txtLocation.Text = Me.cmbPropertyItem.ActiveRow.Cells("TerritoryName").Value.ToString
                Me.txtCity.Text = Me.cmbPropertyItem.ActiveRow.Cells("CityName").Value.ToString
                Me.txtType.Text = Me.cmbPropertyItem.ActiveRow.Cells("PropertyType").Value.ToString
                Me.cmbPropertyItem.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbBranch_ValueChanged(sender As Object, e As EventArgs) Handles cmbBranch.ValueChanged
        Try
            If IsEditMode = False Then
                Me.txtDocNo.Text = GetDocumentNo()
            Else
                Me.txtDocNo.Text = CurrentDocNo
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpDocDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDocDate.ValueChanged
        Try
            If IsEditMode = False Then
                Me.txtDocNo.Text = GetDocumentNo()
            Else
                Me.txtDocNo.Text = CurrentDocNo
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddInvestor_Click(sender As Object, e As EventArgs) Handles btnAddInvestor.Click
        Try
            If IsEditMode = True Then
                frmProInvestmentBooking.PlotNo = txtPlotNumber.Text
                frmProInvestmentBooking.ProInvestmentId = 0
                frmProInvestmentBooking.PropertyProfileId = Id
                frmProInvestmentBooking.CostCenterId = CostCenterId
                frmProInvestmentBooking.InvestmentAmount = Val(Me.txtSellerPrice.Text)
                If frmProInvestmentBooking.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    GetInvestors()
                    GetLedger()
                End If
            Else
                ShowInformationMessage("Profile Not created yet")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Enum enmSales
        PropertyPurchaseId
        Title
        PlotNo
        PlotSize
        Block
        Sector
        Location
        City
        SerialNo
        SellerAccountId
        Price
        Remarks
        PropertyType
        CellNo
        detail_title
        Balance
    End Enum
    Enum enmPurchase
        PropertySalesId
        Title
        PlotNo
        PlotSize
        Block
        Sector
        Location
        City
        SerialNo
        BuyerAccountId
        Price
        Remarks
        PropertyType
        CellNo
        detail_title
        Balance
    End Enum
    Sub GetSeller()
        Try
            Dim dt As New DataTable
            Dim str As String
            'str = "SELECT TOP (1) PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblCOAMainSubSubDetail.detail_title, ISNULL(SUM(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount), 0) AS Balance FROM  PropertyPurchase INNER JOIN tblCOAMainSubSubDetail ON PropertyPurchase.SellerAccountId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN tblVoucherDetail ON PropertyPurchase.SellerAccountId = tblVoucherDetail.coa_detail_id WHERE (PropertyPurchase.SerialNo = '" & Me.txtDocNo.Text & "') GROUP BY PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblCOAMainSubSubDetail.detail_title ORDER BY PropertyPurchase.PropertyPurchaseId DESC"
            str = "SELECT PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblCOAMainSubSubDetail.detail_title, ISNULL(SUM(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount), 0) AS Balance FROM  PropertyPurchase INNER JOIN tblCOAMainSubSubDetail ON PropertyPurchase.SellerAccountId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN tblVoucherDetail ON PropertyPurchase.SellerAccountId = tblVoucherDetail.coa_detail_id WHERE (PropertyPurchase.SerialNo = '" & Me.txtDocNo.Text & "') GROUP BY PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblCOAMainSubSubDetail.detail_title ORDER BY PropertyPurchase.PropertyPurchaseId DESC"
            dt = GetDataTable(str)

            'dt = GetDataTable("SELECT  Top 1   PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.PropertyType, " _
            '   & " PropertyPurchase.CellNo, tblCOAMainSubSubDetail.detail_title FROM         PropertyPurchase INNER JOIN tblCOAMainSubSubDetail ON PropertyPurchase.SellerAccountId = tblCOAMainSubSubDetail.coa_detail_id where serialNo='" & Me.txtDocNo.Text & "' order by 1 desc")

            If dt.Rows.Count > 0 Then
                Me.txtSellerName.Text = dt.Rows(0).Item(enmSales.detail_title).ToString
                Me.txtSellerCellNo.Text = dt.Rows(0).Item(enmSales.CellNo).ToString
                Me.txtSellerPrice.Text = dt.Rows(0).Item(enmSales.Price).ToString
                Me.TxtSellerBalance.Text = dt.Rows(0).Item(enmSales.Balance).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GetBuyer()
        Try
            Dim dt As New DataTable

            Dim str As String
            'str = "SELECT TOP (1) PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.Location, PropertySales.City, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.PropertyType, PropertySales.CellNo, tblCOAMainSubSubDetail.detail_title, ISNULL(SUM(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount), 0) AS Balance FROM PropertySales INNER JOIN tblCOAMainSubSubDetail ON PropertySales.BuyerAccountId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN tblVoucherDetail ON PropertySales.BuyerAccountId = tblVoucherDetail.coa_detail_id WHERE (PropertySales.SerialNo = '" & Me.txtDocNo.Text & "') GROUP BY PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.Location, PropertySales.City, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.PropertyType, PropertySales.CellNo, tblCOAMainSubSubDetail.detail_title, tblVoucherDetail.coa_detail_id ORDER BY PropertySales.PropertySalesId DESC"
            str = "SELECT PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.Location, PropertySales.City, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.PropertyType, PropertySales.CellNo, tblCOAMainSubSubDetail.detail_title, ISNULL(SUM(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount), 0) AS Balance FROM PropertySales INNER JOIN tblCOAMainSubSubDetail ON PropertySales.BuyerAccountId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN tblVoucherDetail ON PropertySales.BuyerAccountId = tblVoucherDetail.coa_detail_id WHERE (PropertySales.SerialNo = '" & Me.txtDocNo.Text & "') GROUP BY PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.Location, PropertySales.City, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.PropertyType, PropertySales.CellNo, tblCOAMainSubSubDetail.detail_title, tblVoucherDetail.coa_detail_id ORDER BY PropertySales.PropertySalesId DESC"
            dt = GetDataTable(str)
            ' dt = GetDataTable("SELECT   Top 1  PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.Location, PropertySales.City, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.PropertyType, " & _
            '                " PropertySales.CellNo, tblCOAMainSubSubDetail.detail_title FROM         PropertySales INNER JOIN tblCOAMainSubSubDetail ON PropertySales.BuyerAccountId = tblCOAMainSubSubDetail.coa_detail_id where serialNo='" & Me.txtDocNo.Text & "' order by 1 desc")

            If dt.Rows.Count > 0 Then
                Me.txtBuyerName.Text = dt.Rows(0).Item(enmPurchase.detail_title).ToString
                Me.txtBuyerCellNo.Text = dt.Rows(0).Item(enmPurchase.CellNo).ToString
                Me.txtBuyerPrice.Text = dt.Rows(0).Item(enmPurchase.Price).ToString
                Me.TxtBuyerBalance.Text = dt.Rows(0).Item(enmPurchase.Balance).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GetLedger()
        Try
            Dim dt As DataTable = UtilityDAL.GetDataTable("sp_property_ledger " & Me.CostCenterId)
            Me.grdLedger.DataSource = dt
            Me.grdLedger.RetrieveStructure()
            ApplyGridSettings("Ledger")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GetInvestors()
        Dim strSQL As String = String.Empty
        Try

            strSQL = " SELECT     InvestmentBooking.InvestmentBookingId, InvestmentBooking.InvestorId, ISNULL(Investor.Name, '') AS Investor, InvestmentBooking.InvestmentAmount AS InvAmount,  " & _
                     " SUM(tblVoucherDetail.debit_amount) AS InvReceived, InvestmentBooking.InvestmentAmount - SUM(tblVoucherDetail.debit_amount) AS InvBalance, 0 AS Profit,  InvestmentBooking.VoucherNo " & _
                     " FROM         Investor INNER JOIN InvestmentBooking INNER JOIN tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id ON InvestmentBooking.VoucherNo = tblVoucher.voucher_code ON  Investor.coa_detail_id = InvestmentBooking.InvestorId " & _
                     " Where InvestmentBooking.PropertyProfileId=" & Id & _
                     " GROUP BY InvestmentBooking.InvestmentBookingId, InvestmentBooking.InvestorId, InvestmentBooking.InvestmentAmount, Investor.Name, InvestmentBooking.VoucherNo"

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Me.grdInvestor.DataSource = dt
            Me.grdInvestor.RetrieveStructure()
            ApplyGridSettings("Investor")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GetTasks()
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT TaskId, Name, DueDate, Completed as Status, case when FormName='frmProSales' then 'Sales' else 'Purchase' end  as  Category FROM TblDefTasks where FormName in ('frmProSales','frmProPurchase') and Ref_No='" & Me.txtDocNo.Text & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Me.grdTask.DataSource = dt
            'Me.grdTask.RetrieveStructure()
            ApplyGridSettings("Tasks")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GetAgentDealers()
        Try

            Dim objDAL As New PropertyProfileAgentDealerDAL
            Me.grdAgents.DataSource = objDAL.GetAll(Id)
            Me.grdAgents.RetrieveStructure()
            ApplyGridSettings("Agent")
            ' txtCommission.Text = grdAgents.GetTotal(grdAgents.RootTable.Columns("CommissionAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) ''TFS4496

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            PropertyProfile = New PropertyProfileBE
            PropertyProfile.PropertyProfileId = Val(Me.grd.CurrentRow.Cells("PropertyProfileId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                ProProfileDAL.Delete(PropertyProfile)
                Me.grd.GetRow.Delete()
                SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdInvestor_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInvestor.ColumnButtonClick
        Try
            If Me.IsDealCompleted = False Then
                objModel = New InvestmentBookingBE
                objModel.InvestmentBookingId = Val(Me.grdInvestor.CurrentRow.Cells("InvestmentBookingId").Value.ToString)
                objModel.InvestorId = Val(Me.grdInvestor.CurrentRow.Cells("InvestorId").Value.ToString)
                objModel.VoucherNo = Me.grdInvestor.CurrentRow.Cells("VoucherNo").Value.ToString
                'InvestmentBookingId
                If e.Column.Key = "Delete" Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    objDAL.Delete(objModel, Me.grdInvestor.RowCount)
                    Me.grdInvestor.GetRow.Delete()
                    GetLedger()
                End If
            Else
                ShowErrorMessage("Deal has been completed.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Saba shabbir: TFS2562 Make profit profile enteries editable by getting all updated investors records
    Private Sub grdInvestor_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdInvestor.RowDoubleClick
        Try
            If Me.grdInvestor.RowCount > 0 AndAlso grdInvestor.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.IsDealCompleted = False Then
                    frmProInvestmentBooking.ProInvestmentId = Val(Me.grdInvestor.CurrentRow.Cells("InvestmentBookingId").Value.ToString)
                    frmProInvestmentBooking.editInvestor = 1
                    frmProInvestmentBooking.blnEditMode = True
                    frmProInvestmentBooking.ShowDialog()
                    GetInvestors()
                    'frmProInvestmentBooking.GetAllRecords()
                Else
                    ShowErrorMessage("Deal has been completed.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Saba Shabbir TFS2562  Added new functionality in agent grid to double click row and make it editable 
    Private Sub grdAgents_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdAgents.RowDoubleClick
        Try
            If Me.IsDealCompleted = False Then
                If Me.grdAgents.RowCount > 0 AndAlso grdAgents.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    frmProInvestorDealer.AgentID = Val(Me.grdAgents.CurrentRow.Cells("Id").Value.ToString)
                    frmProInvestorDealer.PropertyProfileId = Val(Me.grdAgents.CurrentRow.Cells("PropertyProfileId").Value.ToString)
                    frmProInvestorDealer.editAble = True
                    IsEditMode = True
                    frmProInvestorDealer.editAgentDealer = 1
                    frmProInvestorDealer.PSID = 1
                    frmProInvestorDealer.PlotNo = txtPlotNumber.Text
                    frmProInvestorDealer.ShowDialog()
                    GetAgentDealers()
                    'frmProInvestorDealer.GetAllRecords()
                Else
                    ShowErrorMessage("Deal has been completed.")

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddAgent_Click(sender As Object, e As EventArgs) Handles btnAddAgent.Click
        Try
            If IsEditMode = True Then
                frmProInvestorDealer.PlotNo = txtPlotNumber.Text
                frmProInvestorDealer.AgentID = 0
                frmProInvestorDealer.PropertyProfileId = Id
                frmProInvestorDealer.editAble = False
                frmProInvestorDealer.PSID = 0
                'frmProInvestorDealer.CostCenterId = CostCenterId
                'frmProInvestorDealer.InvestmentAmount = Val(Me.txtSellerPrice.Text)
                If frmProInvestorDealer.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    GetAgentDealers()
                    GetLedger()
                End If
            Else
                ShowInformationMessage("Profile Not created yet")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnProfitSharing_Click(sender As Object, e As EventArgs) Handles btnProfitSharing.Click
        Try
            'IsEditMode = True
            If IsEditMode = True Then
                'frmProfitSharing.PropertyId = Id
                'If frmProfitSharing.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'End If
                frmProfitSharing.PlotNo = txtPlotNumber.Text
                Dim SellerBallance As String = Me.txtSellerPrice.Text.Replace(",", String.Empty)
                Dim BuyerBallance As String = Me.txtBuyerPrice.Text.Replace(",", String.Empty)
                Dim _frmProfitSharing As New frmProfitSharing(Id, CostCenterId, Val(SellerBallance), Val(BuyerBallance))
                _frmProfitSharing.ShowDialog()
                GetLedger()
            Else
                ShowInformationMessage("Profile Not Created Yet")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles btnMore.MouseHover
        ContextMenuStrip1.Show(btnMore, 0, btnMore.Height)
    End Sub


    Private Sub InvestorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvestorsToolStripMenuItem.Click
        If Me.IsDealCompleted = False Then
            btnAddInvestor_Click(Nothing, Nothing)
        Else
            ShowErrorMessage("Deal has been completed.")
        End If
    End Sub

    Private Sub AgentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgentsToolStripMenuItem.Click
        If Me.IsDealCompleted = False Then
            btnAddAgent_Click(Nothing, Nothing)
        Else
            ShowErrorMessage("Deal has been completed.")
        End If
    End Sub

    Private Sub ProfitSharingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfitSharingToolStripMenuItem.Click
        If Me.IsDealCompleted = False Then
            btnProfitSharing_Click(Nothing, Nothing)
        Else
            ShowErrorMessage("Deal has been completed.")
        End If
    End Sub


    Private Sub frmPropertyProfile_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmPropertySearch.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSellerPrice_TextChanged(sender As Object, e As EventArgs) Handles txtSellerPrice.TextChanged
        Try
            If Me.txtSellerPrice.Text = "" Then
                lblNumberConverter1.Text = ""
            Else
                lblNumberConverter1.Text = ModGlobel.NumberToWords(Me.txtSellerPrice.Text)
                txtSellerPrice.Text = FormatNumber(Me.txtSellerPrice.Text, 2)
                Dim buyerPrice As Double = 0
                Dim sellerPrice As Double = 0
                If txtBuyerPrice.Text.Length > 0 Then
                    buyerPrice = txtBuyerPrice.Text
                End If
                If txtSellerPrice.Text.Length > 0 Then
                    sellerPrice = txtSellerPrice.Text
                End If
                txtMargin.Text = buyerPrice - sellerPrice
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBuyerPrice_TextChanged(sender As Object, e As EventArgs) Handles txtBuyerPrice.TextChanged
        Try
            If Me.txtBuyerPrice.Text = "" Then
                lblNumberConverter.Text = ""
            Else
                lblNumberConverter.Text = ModGlobel.NumberToWords(Me.txtBuyerPrice.Text)
                txtBuyerPrice.Text = FormatNumber(Me.txtBuyerPrice.Text, 2)
                Dim buyerPrice As Double = 0
                Dim sellerPrice As Double = 0
                If txtBuyerPrice.Text.Length > 0 Then
                    buyerPrice = txtBuyerPrice.Text
                End If
                If txtSellerPrice.Text.Length > 0 Then
                    sellerPrice = txtSellerPrice.Text
                End If
                txtMargin.Text = buyerPrice - sellerPrice
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnBuyer.Click
        If Me.IsDealCompleted = False Then
            frmProSales.DocNo = Me.txtDocNo.Text
            frmProSales.EditAble = -1
            frmProSales.DoHaveSaveRights = True
            frmProSales.DoHaveUpdateRights = True
            If txtSellerPrice.TextLength > 0 Then
                frmProSales.SellerPrice = txtSellerPrice.Text
            End If
            frmProSales.Cost_CenterId = CostCenterId
            frmProSales.PropertyDate = Me.dtpDocDate.Value
            frmProSales.ShowDialog()
            GetBuyer()
            GetLedger()
        Else
            ShowErrorMessage("Deal has been completed.")
        End If
    End Sub

    Private Sub btnSaller_Click(sender As Object, e As EventArgs) Handles btnSaller.Click
        If Me.IsDealCompleted = False Then
            frmProPurchase.DocNo = Me.txtDocNo.Text
            frmProPurchase.EditAble = -1
            frmProPurchase.DoHaveSaveRights = True
            frmProPurchase.DoHaveUpdateRights = True
            frmProPurchase.Cost_CenterId = CostCenterId
            frmProPurchase.PropertyDate = Me.dtpDocDate.Value
            frmProPurchase.ShowDialog()
            GetSeller()
            GetLedger()
        Else
            ShowErrorMessage("Deal has been completed.")
        End If
    End Sub


    Private Sub TxtSellerBalance_TextChanged(sender As Object, e As EventArgs) Handles TxtSellerBalance.TextChanged
        'Try
        '    If Me.TxtSellerBalance.Text = "" Then
        '        'lblNumberConverter1.Text = ""
        '    Else
        '        TxtSellerBalance.Text = FormatNumber(Me.txtSellerPrice.Text, 2)
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub TxtBuyerBalance_TextChanged(sender As Object, e As EventArgs) Handles TxtBuyerBalance.TextChanged
        'Try
        '    If Me.TxtBuyerBalance.Text = "" Then
        '        'lblNumberConverter1.Text = ""
        '    Else
        '        TxtBuyerBalance.Text = FormatNumber(Me.TxtBuyerBalance.Text, 2)
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub cmbPropertyItem_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbPropertyItem.InitializeLayout

    End Sub

    Private Sub frmPropertyProfile_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ReSetControls()
        Me.grdAgents.DataSource = Nothing
        Me.grdInvestor.DataSource = Nothing
        Me.grdLedger.DataSource = Nothing
        Me.grdTask.DataSource = Nothing
        'IsEditMode = False
        'Me.txtBuyerName.Text = String.Empty
        'Me.txtBuyerCellNo.Text = String.Empty
        'Me.txtBuyerPrice.Text = String.Empty
        'Me.txtSellerName.Text = String.Empty
        'Me.txtSellerCellNo.Text = String.Empty
        'Me.txtSellerPrice.Text = String.Empty
        'Me.TxtSellerBalance.Text = String.Empty
        'Me.TxtBuyerBalance.Text = String.Empty
        Me.txtCommission.Text = String.Empty
        Me.txtMargin.Text = String.Empty
        Me.txtNDC.Text = String.Empty
        Me.txtStatus.Text = String.Empty
        Me.dtpBayanaDate.Text = String.Empty
        'Me.dtpDocDate.Text = String.Empty

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@PropertyProfileId", frmPropertyProfile.Id)
            ShowReport("PropertyProfile")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS3672
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDealCompletion_Click(sender As Object, e As EventArgs) Handles btnDealCompletion.Click
        Try
            Dim TotalCommissionAgentDealer As Double = 0
            Dim TotalProfitShare As Double = 0
            Dim TotalExpense As Double = 0
            Dim OtherExpense As Double = 0 ''TFS4496
            Dim NDCExpense As Double = 0 ''TFS4496'
            Dim Commission As Double = 0 ''TFS4496
            Dim Sales As Double = 0
            Dim Purchase As Double = 0
            If Val(Me.txtSellerPrice.Text) > 0 Then
                Sales = Me.txtSellerPrice.Text
            End If
            If Val(Me.txtBuyerPrice.Text) > 0 Then
                Purchase = Me.txtBuyerPrice.Text
            End If
            Dim dtAgentAndDealer As DataTable = Me.grdAgents.DataSource
            Dim dtLedger As DataTable = Me.grdLedger.DataSource
            Dim dtInvestor As DataTable = Me.grdInvestor.DataSource
            'TotalCommission = Me.grdAgents.GetTotal(grdAgents.RootTable.Columns("CommissionAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TotalCommissionAgentDealer = Me.grdAgents.GetTotal(grdAgents.RootTable.Columns("CommissionAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TotalProfitShare = ProfitSharingDetailDAL.GetPropertyProfileNetProfit(Id)
            Dim _filterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(grdLedger.RootTable.Columns("AccountType"), Janus.Windows.GridEX.ConditionOperator.Equal, "Expense")
            ' TotalExpense = Me.grdLedger.GetTotal(grdLedger.RootTable.Columns("OtherExpenses"), Janus.Windows.GridEX.AggregateFunction.Sum, _filterCondition) ''TFS4496

            If IsDealCompleted = True Then
                'If TotalCommission > 0 AndAlso TotalExpense >= TotalCommission Then
                '    TotalExpense = TotalExpense - TotalCommission
                'End If
            Else
                ' TotalCommission = Me.grdAgents.GetTotal(grdAgents.RootTable.Columns("CommissionAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                TotalCommissionAgentDealer = Me.grdAgents.GetTotal(grdAgents.RootTable.Columns("CommissionAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            End If
            OtherExpense = GetOtherExpenseFromAccount()
            NDCExpense = GetNDcExpenseFromAccount()
            Commission = GetCommissionFromAccount()
            'If TotalProfitShare > 0 AndAlso TotalExpense >= TotalProfitShare Then
            '    TotalExpense = TotalExpense - TotalProfitShare
            'End If
            Dim DealCompletion As New frmDealCompletion(Sales, Purchase, TotalCommissionAgentDealer, TotalProfitShare, OtherExpense, NDCExpense, Commission, Me.txtPlotNumber.Text, IsDealCompleted, dtAgentAndDealer, Id, dtInvestor)
            DealCompletion.ShowDialog()

            GetAllRecords()
            GetLedger()
            GetAgentDealers()
            GetInvestors()

            If Me.IsDealCompleted = False Then
                Me.btnDealcompleted.Visible = False
                Me.btnDealcompleted.SendToBack()
            Else
                Me.btnDealcompleted.Visible = True
                Me.btnDealcompleted.BringToFront()
                'Me.btnDealCompletion.Visible = False
                'Me.btnDealCompleted.SendToBack()
            End If
            'Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCommission_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCommission.KeyPress
        NumValidation(sender, e)
    End Sub

    Private Sub txtMargin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMargin.KeyPress
        NumValidation(sender, e)
    End Sub
    Private Function GetOtherExpenseFromAccount() As Double
        Try
            Dim StrSql As String = String.Empty
            Dim OtherExpense As Double = 0
            Dim OtherExpenseAccount As Integer = 0
            OtherExpenseAccount = Convert.ToInt32(Val(getConfigValueByType("OtherExpenseAccountForPropertyProfile").ToString))
            Dim dt As DataTable = GetDataTable("Select ISNull(Sum(debit_Amount),0) As Amount from tblvoucherDetail where coa_detail_id = " & OtherExpenseAccount & " And CostCenterID = " & GetCostCenterId(Id) & "")
            If dt.Rows.Count > 0 Then
                OtherExpense = Val(dt.Rows(0).Item("Amount").ToString)
            End If
            Return OtherExpense
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetNDcExpenseFromAccount() As Double
        Try
            Dim StrSql As String = String.Empty
            Dim NDCExpense As Double = 0
            Dim NDcExpenseAccount As Integer = 0
            NDcExpenseAccount = Convert.ToInt32(Val(getConfigValueByType("NDCAccountForPropertyProfile").ToString))
            Dim dt As DataTable = GetDataTable("Select ISNull(Sum(debit_Amount),0) As Amount from tblvoucherDetail where coa_detail_id = " & NDcExpenseAccount & " And CostCenterID = " & GetCostCenterId(Id) & "")
            If dt.Rows.Count > 0 Then
                NDCExpense = Val(dt.Rows(0).Item("Amount").ToString)
            End If
            Return NDCExpense
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetCommissionFromAccount() As Double
        Try
            Dim StrSql As String = String.Empty
            Dim Commission As Double = 0
            Dim CommissionAccount As Integer = 0
            CommissionAccount = Convert.ToInt32(Val(getConfigValueByType("CommissionAccountForPropertyProfile").ToString))
            Dim dt As DataTable = GetDataTable("Select ISNull(Sum(credit_Amount),0) As Amount from tblvoucherDetail where coa_detail_id = " & CommissionAccount & " And CostCenterID = " & GetCostCenterId(Id) & "")
            If dt.Rows.Count > 0 Then
                Commission = Val(dt.Rows(0).Item("Amount").ToString)
            End If
            Return Commission
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class