'' 03-03-2016 Muhammad Ameen TASK-332: Daily wages, report cost centre, employee, date range
''20-01-2018 Muhammad Ameen TASK TFS2169. 'GUI Changes' Addition of new fileds like Production stages, Labour type and Charge type.
''20-01-2018 Muhammad Ameen TASK TFS2170. Code behind changes against new fileds like Production stages, Labour type and Charge type.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmDailySalaries
    Implements IGeneral
    Dim DailySalary As dailysalarymaster
    Dim DailySalaryDetail As DailySalarydetail
    Dim MasterId As Integer = 0
    Dim IsFormLoaded As Boolean = False
    Dim PrintLog As PrintLogBE
    Enum GrdEnum
        EmployeeId
        CostCenterId
        ProductionStageId
        ProductionStage
        EmployeeName
        LabourTypeId
        LabourType
        ChargeType
        Rate
        Unit
        Amount
        Remarks
        'Remarks()
        'EmployeeID
        'CostCenterId
        'EmployeeName
        'Remarks
        'DailyWage
        'Time
        'Rate
        'Amount
    End Enum

    Private Property txtRatePerHour As Object

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdDetail.RootTable.Columns
                If col.Index <> GrdEnum.Unit AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.Remarks AndAlso col.Index <> GrdEnum.CostCenterId Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

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
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkPost.Visible = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
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
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Return New dailysalariesdal().DeleteDailySalary(DailySalary)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "CostCenter" Then
                'Dim dtCost As DataTable = New dailysalariesdal().GetCostCenter()
                'Me.cmbcostcenter.DataSource = dtCost 'From DAL
                'Me.cmbcostcenter.ValueMember = dtCost.Columns(0).ColumnName.ToString  'From DAL Column (Id)
                'Me.cmbcostcenter.DisplayMember = dtCost.Columns(1).ColumnName.ToString  'From Dal Column (Name)
                FillDropDown(Me.cmbcostcenter, New dailysalariesdal().GetCostCenter)
                '------------------------------------------------------------------
            ElseIf Condition = "Employee" Then
                'Dim dtEmp As DataTable = New dailysalariesdal().GetEmployee
                'Me.cmbemployee.DataSource = dtEmp 'From DAL
                'Me.cmbemployee.ValueMember = dtEmp.Columns(0).ColumnName.ToString  'From DAL Column (Id)
                'Me.cmbemployee.DisplayMember = dtEmp.Columns(1).ColumnName.ToString  'From DAL Column(Name)

                '    Dim str As String = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
                '& "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
                '& "              FROM tblDefEmployee INNER JOIN " _
                '& "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
                '& "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1"

                FillUltraDropDown(Me.cmbemployee, New dailysalariesdal().GetEmployee) ' TASK-332

                Me.cmbemployee.Rows(0).Activate()
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("Employee_Name").Header.Caption = "Employee Name"
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("EmployeeDesignationName").Header.Caption = "Designation"
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("EmployeeDeptName").Header.Caption = "Department" ''Father_Name
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("Father_Name").Header.Caption = "Father Name"
                Me.cmbemployee.DisplayLayout.Bands(0).Columns("Salary").Header.Caption = "Salary Rate"


                '-------------------------------------------------------------------
            ElseIf Condition = "GrdCostCenter" Then
                Dim dt As New DataTable
                dt = GetDataTable(New dailysalariesdal().GetCostCenter)
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0)
                dr(1) = "....Select any value...."
                dt.Rows.InsertAt(dr, 0)
                If Me.grdDetail.RowCount > 0 Then
                    Me.grdDetail.RootTable.Columns("CostCenterId").ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "CostCenter")
                End If

            ElseIf Condition = "Stages" Then
                FillDropDown(Me.cmbStages, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps Order By sort_Order ")
            ElseIf Condition = "LabourType" Then
                FillDropDown(Me.cmbLabourType, "Select tblLabourType.Id AS LabourTypeId, LabourType, ChargeType.Charge As ChargeType From tblLabourType LEFT OUTER JOIN ChargeType ON tblLabourType.Id = ChargeType.Id")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If

            DailySalary = New dailysalarymaster
            DailySalary.DailySalariesId = MasterId
            DailySalary.DcNo = Me.txtDcNo.Text
            DailySalary.DcDate = Me.dtpDcDate.Value.Date
            DailySalary.posted = IIf(Me.chkPost.Checked = True, 1, 0)
            DailySalary.Amount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            DailySalary.Reference = Me.txtrefrance.Text
            DailySalary.UserName = LoginUserName
            DailySalary.EntryDate = Date.Now
            DailySalary.DailySalaryDetail = New List(Of DailySalarydetail)
            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                DailySalaryDetail = New DailySalarydetail
                DailySalaryDetail.DailySalariesId = MasterId
                DailySalaryDetail.EmployId = grdRow.Cells("EmployeeId").Value
                DailySalaryDetail.CostCenterId = grdRow.Cells("CostCenterId").Value
                ''TASK TFS2170
                DailySalaryDetail.DailyWage = grdRow.Cells("ChargeType").Value
                DailySalaryDetail.Salary = grdRow.Cells("Rate").Value
                DailySalaryDetail.WorkingTime = grdRow.Cells("Unit").Value
                DailySalaryDetail.Remarks = grdRow.Cells("Remarks").Text
                DailySalaryDetail.ProductionStepId = grdRow.Cells("ProductionStageId").Value
                DailySalaryDetail.LabourTypeId = grdRow.Cells("LabourTypeId").Value
                ''END TASK TFS2170
                DailySalary.DailySalaryDetail.Add(DailySalaryDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            If Condition = "All" Then
                Me.grdSaved.DataSource = New dailysalariesdal().GetAllRecords("All")
            Else
                Me.grdSaved.DataSource = New dailysalariesdal().GetAllRecords()
            End If
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Caption = "Doc No"
            Me.grdSaved.RootTable.Columns(2).Caption = "Doc Date"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtDcNo.Text = String.Empty Then
                ShowErrorMessage("Please define document no.")
                Me.txtDcNo.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnsave.Text = "&Save"
            Me.MasterId = 0
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Me.txtDcNo.Text = GetSerialNo("DS" + "-" + Microsoft.VisualBasic.Right(Me.dtpDcDate.Value.Year, 2) + "-", "DailySalariesmastertbl", "DcNo")
            Else
                Me.txtDcNo.Text = GetNextDocNo("DS", 6, "DailySalariesmastertbl", "DcNo")
            End If
            Me.dtpDcDate.Value = Date.Now
            Me.dtpDcDate.Enabled = True
            Me.txtrefrance.Text = String.Empty
            Me.chkPost.Checked = True
            Me.cmbemployee.Rows(0).Activate()
            'Me.txtdepartment.Text = String.Empty
            'Me.txtdesignation.Text = String.Empty
            'Me.txtbasicesalary.Text = String.Empty
            'Me.txtRatePerHour.Text = String.Empty
            Me.txtChargeType.Text = String.Empty
            Me.txtworkingtime.Text = String.Empty
            Me.txtAmount.Text = String.Empty
            Me.cmbcostcenter.SelectedIndex = 0
            ''TASK TFS2170
            Me.cmbStages.SelectedIndex = 0
            Me.cmbLabourType.SelectedIndex = 0
            ''END TASK TFS2170
            Me.txtRemarks.Text = String.Empty
            DisplayDetail(-1)
            'GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Return New dailysalariesdal().Adddailysalary(DailySalary)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub
    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            Return New dailysalariesdal().UpdateDailySalary(DailySalary)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmDailySalaries_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnnew_Click(btnNew, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(btnPrint, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmDailySalaries_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos("CostCenter")
            FillCombos("Employee")
            ''TASK TFS2170
            FillCombos("Stages")
            FillCombos("LabourType")
            '' END TASK TFS2170
            'Me.GetAllRecords()
            ReSetControls()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmDailySalary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddGridToItems()
        Try

            Dim dtGrd As DataTable
            dtGrd = CType(Me.grdDetail.DataSource, DataTable)
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(GrdEnum.EmployeeID) = Me.cmbemployee.Value
            drGrd.Item(GrdEnum.CostCenterId) = Me.cmbcostcenter.SelectedValue
            drGrd.Item(GrdEnum.EmployeeName) = Me.cmbemployee.Text
            ''TASK TFS2170
            drGrd.Item(GrdEnum.ProductionStageId) = Me.cmbStages.SelectedValue
            drGrd.Item(GrdEnum.ProductionStage) = Me.cmbStages.Text

            drGrd.Item(GrdEnum.LabourTypeId) = Me.cmbStages.SelectedValue
            drGrd.Item(GrdEnum.LabourType) = Me.cmbStages.Text
            '' END TASK TFS2170
            drGrd.Item(GrdEnum.Remarks) = Me.txtRemarks.Text
            drGrd.Item(GrdEnum.ChargeType) = Me.txtChargeType.Text
            drGrd.Item(GrdEnum.Rate) = Val(Me.txtbasicesalary.Text)
            drGrd.Item(GrdEnum.Unit) = Val(Me.txtworkingtime.Text)
            drGrd.Item(GrdEnum.Amount) = (Val(Me.txtbasicesalary.Text) * Val(Me.txtworkingtime.Text))
            dtGrd.Rows.InsertAt(drGrd, 0)
            FillCombos("GrdCostCenter")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal DailySalaryId As Integer)
        Try
            Dim dt As DataTable = New dailysalariesdal().GetRecordbyId(DailySalaryId)
            dt.Columns("Amount").Expression = "Rate * Unit"
            Me.grdDetail.DataSource = Nothing
            Me.grdDetail.DataSource = dt ' From DAL
            FillCombos("GrdCostCenter")
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Not GridValidation() = True Then Exit Sub
            AddGridToItems()
            ClearItems()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnLoadAll.Visible = True
                GetAllRecords()
            Else
                Me.btnLoadAll.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            Dim id As Integer = 0

            id = Me.cmbcostcenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbcostcenter.SelectedValue = id

            id = Me.cmbemployee.Value
            FillCombos("Employee")
            Me.cmbemployee.Value = id
            ''TASK TFS2170
            id = Me.cmbStages.SelectedValue
            FillCombos("Stages")
            Me.cmbStages.SelectedValue = id

            id = Me.cmbLabourType.SelectedValue
            FillCombos("LabourType")
            Me.cmbLabourType.SelectedValue = id
            '' END TASK TFS2170

            FillCombos("GrdCostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub txtworkingtime_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtworkingtime.Leave
        Try
            If Val(Me.txtbasicesalary.Text) > 0 Then
                Me.txtAmount.Text = Val(Me.txtbasicesalary.Text) * Val(Me.txtworkingtime.Text)
            Else
                Me.txtAmount.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbemployee_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDcDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If

            If Me.dtpDcDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpDcDate.Focus()
                Exit Sub
            End If

            If Not IsValidate() = True Then Exit Sub
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                ' msg_Information(str_informSave)
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
                ' msg_Information(str_informUpdate)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try

            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDcDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpDcDate.Enabled = False
                Else
                    Me.dtpDcDate.Enabled = True
                End If
            Else
                Me.dtpDcDate.Enabled = True
            End If

            Me.btnSave.Text = "&Update"
            MasterId = Me.grdSaved.GetRow.Cells("DailySalariesId").Value
            Me.txtDcNo.Text = Me.grdSaved.GetRow.Cells("DcNo").Value
            Me.dtpDcDate.Value = Me.grdSaved.GetRow.Cells("DcDate").Value
            Me.txtrefrance.Text = Me.grdSaved.GetRow.Cells("Reference").Text
            Me.chkPost.Checked = IIf(Me.grdSaved.GetRow.Cells("Post").Value = "Posted", 1, 0)
            Me.DisplayDetail(MasterId)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            grdSaved_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetAllRecords("All")
            DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function GridValidation(Optional ByVal Condition As String = "") As Boolean
        Try
            If Me.cmbemployee.Value < 1 Then
                ShowErrorMessage("Please select employee")
                Me.cmbemployee.Focus()
                Return False
            End If
            If (Me.txtbasicesalary.Text) <= 0 Then
                ShowErrorMessage("Please enter basic salary")
                Me.txtbasicesalary.Focus()
                Return False
            End If
            If Val(Me.txtworkingtime.Text) <= 0 Then
                ShowErrorMessage("Please enter unit")
                Me.txtworkingtime.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grdDetail_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grdDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbdailywage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    If cmbdailywage.Text = "Normal" Then
        '        Me.txtRatePerHour.Text = (Val(Me.txtbasicesalary.Text) / Convert.ToInt32(getConfigValueByType("NormalTimeWage").ToString))
        '    Else
        '        Me.txtRatePerHour.Text = (Val(Me.txtbasicesalary.Text) / Convert.ToInt32(getConfigValueByType("OverTimeWage").ToString))
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub txtbasicesalary_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    cmbdailywage_SelectedIndexChanged(Nothing, Nothing)
        '    txtworkingtime_Leave(Nothing, Nothing)
        '    Me.txtRatePerHour.Focus()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDcDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If

            If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
            'msg_Information(str_informSave)
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSaved.CurrentRow.Delete()

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("DcNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@CurrentId", Me.grdSaved.GetRow.Cells("DailySalariesId").Value)
            ShowReport("rptDailySalaryVoucher")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ClearItems()
        Try

            Me.cmbcostcenter.SelectedIndex = 0
            Me.cmbStages.SelectedIndex = 0
            Me.cmbLabourType.SelectedIndex = 0
            Me.cmbemployee.Focus()
            Me.cmbemployee.Rows(0).Activate()
            Me.txtRemarks.Text = String.Empty
            Me.txtbasicesalary.Text = 0
            Me.txtworkingtime.Text = 0
            Me.txtAmount.Text = 0
            Me.txtDepartment.Text = String.Empty
            Me.txtDesignation.Text = String.Empty
            Me.txtbasicesalary.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbemployee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = New dailysalariesdal().GetEmployeeById(Me.cmbemployee.Value)
            If dt.Rows.Count > 0 Then
                Me.txtDepartment.Text = dt.Rows(0).Item("EmployeeDeptName").ToString
                Me.txtDesignation.Text = dt.Rows(0).Item("EmployeeDesignationName").ToString
                Me.txtbasicesalary.Text = dt.Rows(0).Item("Salary").ToString
            Else
                Me.txtDepartment.Text = String.Empty
                Me.txtDesignation.Text = String.Empty
                Me.txtbasicesalary.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtrefrance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrefrance.TextChanged

    End Sub

    Private Sub txtbasicesalary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub AddNewCostCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCostCenterToolStripMenuItem.Click
        Try
            Dim id As Integer = 0
            frmAddCostCenter.ShowDialog()
            id = Me.cmbcostcenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbcostcenter.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpDcDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetail.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Me.btnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Me.btnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Me.btnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Me.btnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub cmbemployee_ValueChanged(sender As Object, e As EventArgs) Handles cmbemployee.ValueChanged
        Try
            Dim dt As DataTable = New dailysalariesdal().GetEmployeeById(Me.cmbemployee.Value)
            If dt.Rows.Count > 0 Then
                Me.txtDepartment.Text = dt.Rows(0).Item("EmployeeDeptName").ToString
                Me.txtDesignation.Text = dt.Rows(0).Item("EmployeeDesignationName").ToString
                Me.txtbasicesalary.Text = dt.Rows(0).Item("Salary").ToString
            Else
                Me.txtDepartment.Text = String.Empty
                Me.txtDesignation.Text = String.Empty
                Me.txtbasicesalary.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''TASK TFS2170
    Private Sub cmbLabourType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLabourType.SelectedIndexChanged
        Try
            If Not Me.cmbLabourType.SelectedIndex = -1 Then
                Me.txtChargeType.Text = CType(Me.cmbLabourType.SelectedItem, DataRowView).Item("ChargeType").ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''END ''TASK TFS2170
    End Sub
End Class