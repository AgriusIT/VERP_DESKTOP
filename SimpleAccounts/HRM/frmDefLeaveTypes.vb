'Ali Faisal : TFS1481 : 20-Sep-2017 : Added new form to save leave types
Imports SBDal
Imports SBModel
Public Class frmDefLeaveTypes
    Implements IGeneral
    Dim objDAL As LeaveTypesDAL
    Dim objModel As LeaveTypesBE
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Set indexing of columns for history grid
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Enum grd
        Id
        CompanyId
        Company
        CostCenterId
        CostCenter
        EmpTypeId
        EmployeeType
        LeaveTypeTitle
        LeaveCatagoryId
        LeaveCatagory
        LeaveType
        LeaveAccrual
        LeaveApproval
        GenderRestriction
        LeavesInCashment
        AllowedPerYear
        CarriedForward
        CarriedForwardDays
        SortOrder
        Active
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Apply grid settings to show/hide columns ig history grid
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.Id).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CompanyId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenterId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.EmpTypeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.LeaveCatagoryId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CarriedForward).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SortOrder).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Active).Visible = False
            Me.grdSaved.RootTable.Columns(grd.AllowedPerYear).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.AllowedPerYear).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CarriedForwardDays).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CarriedForwardDays).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.SortOrder).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.SortOrder).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Apply security rights for standard user to get enabled that buttons that he/she have rights
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.btnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Delete records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New LeaveTypesDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grd.Id).Value.ToString)) = True Then
                'Insert Activity Log by Ali Faisal
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtTypeTitle.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Fill drop downs
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Company" Then
                str = "SELECT CompanyId, CompanyName FROM CompanyDefTable"
                FillDropDown(Me.cmbCompany, str, True)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT CostCenterID, [Name] FROM tblDefCostCenter WHERE Active = 1"
                FillDropDown(Me.cmbCostCenter, str, True)
            ElseIf Condition = "EmpType" Then
                str = "SELECT EmployeeTypeId, EmployeeTypeName FROM TblEmployeeType WHERE Active = 1"
                FillDropDown(Me.cmbEmpType, str, True)
            ElseIf Condition = "LeaveCatagory" Then
                str = "SELECT CatagoryId, CatagoryName FROM tblDefLeaveCatagory WHERE Active = 1"
                FillDropDown(Me.cmbCatagory, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Fill controls to show in edit mode
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub EditRecords()
        Try
            Me.cmbCompany.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells(grd.CompanyId).Value)
            Me.cmbCostCenter.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells(grd.CostCenterId).Value)
            Me.cmbEmpType.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells(grd.EmpTypeId).Value)
            Me.txtTypeTitle.Text = Me.grdSaved.CurrentRow.Cells(grd.LeaveTypeTitle).Value.ToString
            Me.cmbCatagory.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells(grd.LeaveCatagoryId).Value)
            Me.cmbLeaveType.Text = Me.grdSaved.CurrentRow.Cells(grd.LeaveType).Value.ToString
            Me.cmbLeaveAccrual.Text = Me.grdSaved.CurrentRow.Cells(grd.LeaveAccrual).Value.ToString
            Me.cmbLeaveApproval.Text = Me.grdSaved.CurrentRow.Cells(grd.LeaveApproval).Value.ToString
            Me.cmbGenderRes.Text = Me.grdSaved.CurrentRow.Cells(grd.GenderRestriction).Value.ToString
            Me.cmbLeaveInCashment.Text = Me.grdSaved.CurrentRow.Cells(grd.LeavesInCashment).Value.ToString
            Me.txtAllowedPerYear.Text = Me.grdSaved.CurrentRow.Cells(grd.AllowedPerYear).Value.ToString
            Me.cmbCarriedForward.Text = Me.grdSaved.CurrentRow.Cells(grd.CarriedForward).Value.ToString
            Me.txtCarriedForward.Text = Me.grdSaved.CurrentRow.Cells(grd.CarriedForwardDays).Value.ToString
            Me.txtSortOrder.Text = Me.grdSaved.CurrentRow.Cells(grd.SortOrder).Value.ToString
            Me.chkActive.Checked = Me.grdSaved.CurrentRow.Cells(grd.Active).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Fill model properties from controls for DAL usage
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New LeaveTypesBE
            If Me.btnSave.Text = "&Save" Then
                objModel.Id = 0
            Else
                objModel.Id = Val(Me.grdSaved.CurrentRow.Cells(grd.Id).Value)
            End If
            objModel.CompanyId = Me.cmbCompany.SelectedValue
            objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            objModel.EmpTypeId = Me.cmbEmpType.SelectedValue
            objModel.LeaveTypeTitle = Me.txtTypeTitle.Text
            objModel.LeaveCatagoryId = Me.cmbCatagory.SelectedValue
            If Me.cmbLeaveType.SelectedIndex = 0 Then
                objModel.LeaveType = ""
            Else
                objModel.LeaveType = Me.cmbLeaveType.Text
            End If
            If Me.cmbLeaveAccrual.SelectedIndex = 0 Then
                objModel.LeaveAccrual = ""
            Else
                objModel.LeaveAccrual = Me.cmbLeaveAccrual.Text
            End If
            If Me.cmbLeaveApproval.SelectedIndex = 0 Then
                objModel.LeaveApproval = ""
            Else
                objModel.LeaveApproval = Me.cmbLeaveApproval.Text
            End If
            objModel.GenderRestriction = Me.cmbGenderRes.Text
            objModel.LeavesInCashment = Me.cmbLeaveInCashment.Text
            objModel.AllowedPerYear = Me.txtAllowedPerYear.Text
            objModel.CarriedForward = Me.cmbCarriedForward.Text
            objModel.CarriedForwardDays = Me.txtCarriedForward.Text
            objModel.SortOrder = Me.txtSortOrder.Text
            objModel.Active = Me.chkActive.CheckState
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Get all records to show history
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT LeaveTypes.Id, LeaveTypes.CompanyId, Company.CompanyName AS Company, LeaveTypes.CostCenterId, CostCenter.Name AS CostCenter, LeaveTypes.EmpTypeId, EmpType.EmployeeTypeName AS EmpType, LeaveTypes.LeaveTypeTitle, LeaveTypes.LeaveCatagoryId, Catagory.CatagoryName, LeaveTypes.LeaveType, LeaveTypes.LeaveAccrual, LeaveTypes.LeaveApproval, LeaveTypes.GenderRestriction, LeaveTypes.LeavesInCashment, LeaveTypes.AllowedPerYear, LeaveTypes.CarriedForward, LeaveTypes.CarriedForwardDays, LeaveTypes.SortOrder, LeaveTypes.Active " _
                & "FROM tblDefLeaveTypes AS LeaveTypes LEFT OUTER JOIN tblDefLeaveCatagory AS Catagory ON LeaveTypes.LeaveCatagoryId = Catagory.CatagoryId LEFT OUTER JOIN TblEmployeeType AS EmpType ON LeaveTypes.EmpTypeId = EmpType.EmployeeTypeId LEFT OUTER JOIN CompanyDefTable AS Company ON LeaveTypes.CompanyId = Company.CompanyId LEFT OUTER JOIN tblDefCostCenter AS CostCenter ON LeaveTypes.CostCenterId = CostCenter.CostCenterID ORDER BY 1 DESC"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Validation on form to save reocrds
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtTypeTitle.Text = "" Then
                msg_Error("Please enter the valid title")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Reset controls to set default values to all controls
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbCompany.SelectedIndex = 0
            Me.cmbCostCenter.SelectedIndex = 0
            Me.cmbEmpType.SelectedIndex = 0
            Me.txtTypeTitle.Text = ""
            Me.cmbCatagory.SelectedIndex = 0
            Me.cmbLeaveType.SelectedIndex = 0
            Me.cmbLeaveAccrual.SelectedIndex = 0
            Me.cmbLeaveApproval.SelectedIndex = 0
            Me.cmbGenderRes.SelectedIndex = 0
            Me.cmbLeaveInCashment.SelectedIndex = 0
            Me.txtAllowedPerYear.Text = 0
            Me.cmbCarriedForward.SelectedIndex = 0
            Me.txtCarriedForward.Text = 0
            Me.txtSortOrder.Text = 0
            Me.chkActive.Checked = True
            FillCombos("Company")
            FillCombos("CostCenter")
            FillCombos("EmpType")
            FillCombos("LeaveCatagory")
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Save records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New LeaveTypesDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTypeTitle.Text, True)
                    Return True
                Else
                    Return False
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
    ''' Ali Faisal : TFS1481 : Update records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New LeaveTypesDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtTypeTitle.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Key down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDefLeaveTypes_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Me.btnSave.Text = "&Update"
            EditRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.UltraTabControl2.Tabs(0).Selected = True
            Me.btnDelete.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Save and Update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' Ali Faisal : TFS1481 : Delete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
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
    ''' Ali Faisal : TFS1481 : Refresh dropdowns
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer

            id = Me.cmbCompany.SelectedValue
            FillCombos("Company")
            Me.cmbCompany.SelectedValue = id

            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id

            id = Me.cmbEmpType.SelectedValue
            FillCombos("EmpType")
            Me.cmbEmpType.SelectedValue = id

            id = Me.cmbCatagory.SelectedValue
            FillCombos("LeaveCatagory")
            Me.cmbCatagory.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefLeaveTypes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Leave Types"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl2_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If Me.UltraTabControl2.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
                Me.btnDelete.Visible = False
            End If
            If Me.UltraTabControl2.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCarriedForward_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCarriedForward.SelectedIndexChanged
        Try
            If Me.cmbCarriedForward.SelectedIndex = 0 Then
                Me.txtCarriedForward.Enabled = True
            Else
                Me.txtCarriedForward.Text = 0
                Me.txtCarriedForward.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class