'30-Jan-2018 TFS2055 : Ali Faisal : Add new form to save update and delete records through this form.
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Imports System.Math
Public Class frmDefBudget
    Implements IGeneral
    Dim objDAL As BudgetDefinitionDAL
    Dim objModel As BudgetDefinitionBE
    ''' <summary>
    ''' Ali Faisal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Enum grdDetail
        AccountBudgetDetailId
        AccountBudgetMasterID
        AccountId
        AccountName
        AccountCode
        AccountLevel
        AmountRequiredAtAccountLevel
        BudgetAmount
        Comments
        CategoryId
    End Enum
    Enum grdHistory
        AccountBudgetMasterId
        Title
        CostCenterId
        CostCenter
        FromDate
        ToDate
        Amount
        CurrencyId
        Currency
        Remarks
    End Enum
    ''' <summary>
    ''' Ali Faisal : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Detail" Then
                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grd.RootTable.Columns(grdDetail.AccountBudgetDetailId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AccountBudgetMasterID).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AccountId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AccountLevel).Visible = False
                Me.grd.RootTable.Columns(grdDetail.CategoryId).Visible = False

                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).TotalFormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.BudgetAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Else
                Me.grdSaved.RootTable.Columns(grdHistory.AccountBudgetMasterId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.CostCenterId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.CurrencyId).Visible = False

                Me.grdSaved.RootTable.Columns(grdHistory.Amount).FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(grdHistory.Amount).TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(grdHistory.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(grdHistory.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(grdHistory.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdSaved.RootTable.Columns(grdHistory.FromDate).FormatString = str_DisplayDateFormat
                Me.grdSaved.RootTable.Columns(grdHistory.ToDate).FormatString = str_DisplayDateFormat
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Ali Faisal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            'Me.btnPrint.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar3.mGridExport.Enabled = False
            Me.CtrlGrdBar3.mGridPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.btnDelete.Enabled = True
                    'ElseIf Rights.Item(i).FormControlName = "Print" Then
                    '    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar3.mGridPrint.Enabled = True
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar3.mGridExport.Enabled = True
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New BudgetDefinitionDAL
            If objDAL.Delete(Val(Me.grdSaved.GetRow.Cells(grdHistory.AccountBudgetMasterId).Value.ToString)) = True Then
                'Insert Activity Log by Ali Faisal on 30-Jan-2018
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtBudgetTitle.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : FillCombos of all dropdowns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "CostCenter" Then
                str = "SELECT CostCenterID, Name, CostCenterGroup, ISNULL(SortOrder,0) AS SortOrder FROM tblDefCostCenter WHERE (Active = 1) ORDER BY CostCenterID ASC"
                FillUltraDropDown(Me.cmbCostCenter, str, True)
                If Me.cmbCostCenter.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbCostCenter.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "Currency" Then
                str = "SELECT currency_id, currency_code FROM tblcurrency ORDER BY currency_id ASC"
                FillDropDown(Me.cmbCurrency, str, False)
            ElseIf Condition = "SubSubAccount" Then
                str = "SELECT main_sub_sub_id, sub_sub_title AS Title, sub_sub_code AS Code, account_type As AccountType, ISNULL(SortOrder,0) AS SortOrder FROM tblCOAMainSubSub ORDER BY main_sub_sub_id ASC"
                FillUltraDropDown(Me.cmbSubSubAccount, str, True)
                If Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "CategoryAccounts" Then
                str = "SELECT tblCOAMainSubSubDetail.coa_detail_id AS AccountId, tblCOAMainSubSubDetail.detail_title AS AccountTitle, tblCOAMainSubSubDetail.detail_code AS AccountCode, ReportTemplateDetail.AccountLevel FROM ReportTemplateDetail INNER JOIN tblCOAMainSubSubDetail ON ReportTemplateDetail.AccountId = tblCOAMainSubSubDetail.coa_detail_id WHERE (ReportTemplateDetail.AccountLevel = 'Detail') AND ReportTemplateDetail.CategoryId = " & Me.cmbCategory.SelectedValue & " UNION ALL SELECT tblCOAMainSubSub.main_sub_sub_id AS AccountId, tblCOAMainSubSub.sub_sub_code AS AccountCode, tblCOAMainSubSub.sub_sub_title AS AccountTitle, ReportTemplateDetail.AccountLevel FROM ReportTemplateDetail INNER JOIN tblCOAMainSubSub ON ReportTemplateDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id WHERE (ReportTemplateDetail.AccountLevel = 'Sub Sub') AND ReportTemplateDetail.CategoryId = " & Me.cmbCategory.SelectedValue & ""
                FillUltraDropDown(Me.cmbSubSubAccount, str, True)
                If Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "Category" Then
                str = "SELECT * FROM ReportTemplateNotesCategory ORDER BY 1"
                FillDropDown(Me.cmbCategory, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub EditRecords()
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtBudgetTitle.Text = Me.grdSaved.CurrentRow.Cells(grdHistory.Title).Value.ToString
            Me.dtpFromDate.Value = CType(Me.grdSaved.CurrentRow.Cells(grdHistory.FromDate).Value, Date)
            Me.dtpToDate.Value = CType(Me.grdSaved.CurrentRow.Cells(grdHistory.ToDate).Value, Date)
            Me.cmbCostCenter.Value = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.CostCenterId).Value.ToString)
            Me.txtNetAmount.Text = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.Amount).Value.ToString)
            Me.cmbCurrency.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.CurrencyId).Value.ToString)
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells(grdHistory.Remarks).Value.ToString
            DisplayDetail(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AccountBudgetMasterId).Value.ToString))
            Me.btnDelete.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New BudgetDefinitionBE
            objModel.Details = New List(Of BudgetDefinitionDetailBE)
            If Me.btnSave.Text = "&Save" Then
                objModel.AccountBudgetMasterId = 0
            Else
                objModel.AccountBudgetMasterId = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AccountBudgetMasterId).Value.ToString)
            End If
            objModel.Title = Me.txtBudgetTitle.Text
            objModel.CostCenterId = Me.cmbCostCenter.Value
            objModel.FromDate = Me.dtpFromDate.Value
            objModel.ToDate = Me.dtpToDate.Value
            objModel.Amount = Me.txtNetAmount.Text
            objModel.CurrencyId = Me.cmbCurrency.SelectedValue
            objModel.Remarks = Me.txtRemarks.Text
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New BudgetDefinitionDetailBE
                Detail.AccountBudgetDetailId = Val(Row.Cells(grdDetail.AccountBudgetDetailId).Value.ToString)
                Detail.AccountId = Val(Row.Cells(grdDetail.AccountId).Value.ToString)
                Detail.AccountLevel = Val(Row.Cells(grdDetail.AccountLevel).Value.ToString)
                Detail.AmountRequiredAtAccountLevel = CType(Row.Cells(grdDetail.AmountRequiredAtAccountLevel).Value.ToString, Boolean)
                Detail.BudgetAmount = CType(Row.Cells(grdDetail.BudgetAmount).Value.ToString, Double)
                Detail.Comments = Row.Cells(grdDetail.Comments).Value.ToString
                Detail.CategoryId = Val(Row.Cells(grdDetail.CategoryId).Value.ToString)
                objModel.Details.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr(grdDetail.AccountBudgetDetailId) = 0
            dr(grdDetail.AccountBudgetMasterID) = 0
            dr(grdDetail.AccountId) = Me.cmbSubSubAccount.Value
            dr(grdDetail.AccountName) = Me.cmbSubSubAccount.ActiveRow.Cells(1).Value.ToString
            dr(grdDetail.AccountCode) = Me.cmbSubSubAccount.ActiveRow.Cells(2).Value.ToString
            If Me.cmbSubSubAccount.ActiveRow.Cells(3).Value.ToString = "Detail" Then
                dr(grdDetail.AccountLevel) = 3
            Else
                dr(grdDetail.AccountLevel) = 2
            End If
            dr(grdDetail.AmountRequiredAtAccountLevel) = Me.chkAmountEnable.CheckState
            dr(grdDetail.BudgetAmount) = Me.txtBudgetAmount.Text
            dr(grdDetail.Comments) = Me.txtComments.Text
            If Me.cmbCategory.SelectedValue > 0 Then
                dr(grdDetail.CategoryId) = Me.cmbCategory.SelectedValue
            Else
                dr(grdDetail.CategoryId) = 0
            End If
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To show all saved records in history grid.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Budget.AccountBudgetMasterId, Budget.Title, Budget.CostCenterId, Proj.Name AS CostCenter, Budget.FromDate, Budget.ToDate, Budget.Amount, Budget.CurrencyId, Curr.currency_code AS Currency, Budget.Remarks FROM AccountBudgetMaster AS Budget LEFT OUTER JOIN tblcurrency AS Curr ON Budget.CurrencyId = Curr.currency_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Budget.CostCenterId = Proj.CostCenterID ORDER BY Budget.AccountBudgetMasterId DESC"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings("History")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To fill the detail grid.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub DisplayDetail(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            'str = "SELECT AccountBudgetDetail.AccountBudgetDetailId, AccountBudgetDetail.AccountBudgetMasterID, AccountBudgetDetail.AccountId, tblCOAMainSubSub.sub_sub_title AS AccountTitle, tblCOAMainSubSub.sub_sub_code AS AccountCode, AccountBudgetDetail.AccountLevel, AccountBudgetDetail.AmountRequiredAtAccountLevel, AccountBudgetDetail.BudgetAmount, AccountBudgetDetail.Comments, ISNULL(AccountBudgetDetail.CategoryId,0) AS CategoryId FROM AccountBudgetDetail LEFT OUTER JOIN tblCOAMainSubSub ON AccountBudgetDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id WHERE AccountBudgetDetail.AccountBudgetMasterID = " & Id & ""
            str = "SELECT AccountBudgetDetail.AccountBudgetDetailId, AccountBudgetDetail.AccountBudgetMasterID, AccountBudgetDetail.AccountId, tblCOAMainSubSub.sub_sub_title AS AccountTitle, tblCOAMainSubSub.sub_sub_code AS AccountCode, AccountBudgetDetail.AccountLevel, AccountBudgetDetail.AmountRequiredAtAccountLevel, AccountBudgetDetail.BudgetAmount, AccountBudgetDetail.Comments, ISNULL(AccountBudgetDetail.CategoryId, 0) AS CategoryId FROM AccountBudgetDetail LEFT OUTER JOIN tblCOAMainSubSub ON AccountBudgetDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id WHERE (AccountBudgetDetail.AccountLevel = 2) AND AccountBudgetDetail.AccountBudgetMasterID = " & Id & " UNION ALL SELECT AccountBudgetDetail.AccountBudgetDetailId, AccountBudgetDetail.AccountBudgetMasterID, AccountBudgetDetail.AccountId, tblCOAMainSubSubDetail.detail_title AS AccountTitle, tblCOAMainSubSubDetail.detail_code AS AccountCode, AccountBudgetDetail.AccountLevel, AccountBudgetDetail.AmountRequiredAtAccountLevel, AccountBudgetDetail.BudgetAmount, AccountBudgetDetail.Comments, ISNULL(AccountBudgetDetail.CategoryId, 0) AS CategoryId FROM AccountBudgetDetail LEFT OUTER JOIN tblCOAMainSubSubDetail ON AccountBudgetDetail.AccountId = tblCOAMainSubSubDetail.coa_detail_id WHERE (AccountBudgetDetail.AccountLevel = 3) AND AccountBudgetDetail.AccountBudgetMasterID = " & Id & ""
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings("Detail")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtBudgetTitle.Text = "" Then
                msg_Error("Please enter valid Budget Title")
                Return False
            ElseIf Me.txtNetAmount.Text = 0 Then
                msg_Error("Please enter Overall Budget Amount")
                Return False
            ElseIf Me.cmbCurrency.SelectedIndex < 0 Or Me.cmbCurrency.SelectedValue = 0 Then
                msg_Error("Please select any Currency")
                Return False
            ElseIf Not Me.grd.RowCount > 0 Then
                msg_Error("Detail grid is empty")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckDuplicateTitle() As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Title FROM AccountBudgetMaster WHERE Title = '" & Me.txtBudgetTitle.Text & "'"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Master" Then
                Me.txtBudgetTitle.Text = ""
                Me.dtpFromDate.Value = Now
                Me.dtpToDate.Value = Now
                Me.cmbCostCenter.Value = 0
                Me.txtNetAmount.Text = 0
                Me.txtRemarks.Text = ""
                Me.cmbCurrency.SelectedIndex = 0
                DisplayDetail(-1)
                GetAllRecords()
                Me.btnSave.Text = "&Save"
                Me.btnSave.Enabled = True
                Me.btnEdit.Visible = False
                Me.btnDelete.Visible = False
                Me.chkCategoryWise.Checked = False
                Me.lblCategory.Visible = False
                Me.cmbCategory.Visible = False
                CtrlGrdBar2_Load(Nothing, Nothing)
                CtrlGrdBar3_Load(Nothing, Nothing)
                ApplySecurityRights()
            Else
                Me.cmbSubSubAccount.Value = 0
                Me.chkAmountEnable.Checked = True
                Me.txtBudgetAmount.Text = 0
                Me.txtComments.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Call the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New BudgetDefinitionDAL
            If IsValidate() = True Then
                If CheckDuplicateTitle() = False Then
                    If objDAL.Save(objModel) = True Then
                        'Insert Activity Log by Ali Faisal on 30-Jan-2018
                        SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtBudgetTitle.Text, True)
                        Return True
                    Else
                        Return False
                    End If
                Else
                    msg_Error("Entered Budget Title exists already")
                    Me.txtBudgetTitle.Focus()
                End If
            End If
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
    ''' <summary>
    ''' Ali Faisal : Call the update function from DAL to modify the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New BudgetDefinitionDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal on 30-Jan-2018
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtBudgetTitle.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidAddToGrid() As Boolean
        Try
            Dim NetAmount As Double = 0
            NetAmount = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.BudgetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
            If Val(NetAmount + Val(Me.txtBudgetAmount.Text)) > Val(Me.txtNetAmount.Text) Then
                msg_Error("Accounts Budget sum should be equal or less than Net Amount")
                Me.txtBudgetAmount.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Set ShortKeys to perform actions on save and refresh
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                Me.btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub frmDefBudget_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("CostCenter")
            FillCombos("Currency")
            FillCombos("SubSubAccount")
            ReSetControls("Master")
            ReSetControls("Detail")
            ApplySecurityRights()
            Me.cmbCostCenter.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls("Master")
            ReSetControls("Detail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0

            id = Me.cmbCostCenter.Value
            FillCombos("CostCenter")
            Me.cmbCostCenter.Value = id

            id = Me.cmbCurrency.SelectedValue
            FillCombos("Currency")
            Me.cmbCurrency.SelectedValue = id

            id = Me.cmbSubSubAccount.Value
            FillCombos("SubSubAccount")
            Me.cmbSubSubAccount.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save and Update the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    btnNew_Click(Nothing, Nothing)
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    msg_Information(str_informUpdate)
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New BudgetDefinitionDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdDetail.AccountBudgetDetailId).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Me.btnSave.Text = "&Update"
            EditRecords()
            ApplySecurityRights()
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit the selected row from history on double click of grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Budget Definition History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar3.Visible = True
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar3.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Budget Definition Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkAmountEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkAmountEnable.CheckedChanged
        Try
            If Me.chkAmountEnable.Checked = True Then
                Me.txtBudgetAmount.Enabled = True
                Me.txtBudgetAmount.Text = 0
            Else
                Me.txtBudgetAmount.Enabled = False
                Me.txtBudgetAmount.Text = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not Me.cmbSubSubAccount.Value = 0 Then
            If IsValidAddToGrid() = True Then
                AddToGrid()
                ReSetControls("Detail")
            End If
        Else
            msg_Error("Please select any Sub Sub Level Account")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetAmount.KeyPress
        Try
            NonNegativeNumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBudgetAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBudgetAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub NonNegativeNumValidation(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = "."c Then
                e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
            ElseIf e.KeyChar <> ControlChars.Back Then
                e.Handled = ("0123456789".IndexOf(e.KeyChar) = -1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkCategoryWise_CheckedChanged(sender As Object, e As EventArgs) Handles chkCategoryWise.CheckedChanged
        Try
            If Me.chkCategoryWise.Checked = True Then
                Me.cmbCategory.Visible = True
                Me.lblCategory.Visible = True
                FillCombos("Category")
                FillCombos("CategoryAccounts")
            Else
                Me.lblCategory.Visible = False
                Me.cmbCategory.Visible = False
                FillCombos("SubSubAccount")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        Try
            FillCombos("CategoryAccounts")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class