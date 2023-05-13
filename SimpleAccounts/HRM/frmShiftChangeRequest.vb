'Ali Faisal : TFS1523 : Add new form to save, update & delete Shift group change request
Imports SBDal
Imports SBModel
Public Class frmShiftChangeRequest
    Implements IGeneral
    Dim objDAL As ShiftChangeRequestDAL
    Dim objModel As ShiftChangeRequestBE
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Set indexes for detail grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grdDetail
        RequestDeailId
        EmployeeId
        EmployeeCode
        EmployeeName
        CurrentShiftId
        CurrentShift
        NewShiftId
        NewShift
        CurrentCostCenterId
        CurrentCostCenter
        NewCostCenterId
        NewCostCenter
        Comments
    End Enum
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Set indexes for history grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grdHistory
        RequestId
        DocNo
        DocDate
        RequestTypeId
        RequestType
        Remarks
    End Enum
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Get New Doc No to Save in Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SCR-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "ShiftChangeRequestMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SCR-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "ShiftChangeRequestMaster", "DocNo")
            Else
                Return GetNextDocNo("SCR-", 6, "ShiftChangeRequestMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Apply grid settings to hide some columns and to apply fomating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "History" Then
                Me.grdSaved.RootTable.Columns(grdHistory.RequestId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.DocDate).FormatString = str_DisplayDateFormat
                Me.grdSaved.RootTable.Columns(grdHistory.RequestTypeId).Visible = False
            ElseIf Condition = "Detail" Then
                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grd.RootTable.Columns(grdDetail.RequestDeailId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.EmployeeId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.CurrentShiftId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.NewShiftId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.CurrentCostCenterId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.NewCostCenterId).Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Apply security to show specific controls to standard user
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
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
                    If Me.btnDelete.Text = "&Delete" Then btnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Export" Then
                    IsCrystalReportExport = True
                ElseIf Rights.Item(i).FormControlName = "Report Print" Then
                    IsCrystalReportPrint = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Delete function to remove records using DAL
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New ShiftChangeRequestDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.RequestId).Value.ToString)) = True Then
                'Insert Activity Log by Ali Faisal
                SaveActivityLog("HRM", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Fill dropdowns for selection
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Employee" Then
                str = "SELECT Employee_ID, Employee_Name + ' ~ ' + Employee_Code AS EmployeeInfo, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1"
                FillUltraDropDown(Me.cmbEmployee, str, True)
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                Me.cmbEmployee.Rows(0).Activate()
            ElseIf Condition = "RequestTypes" Then
                str = "SELECT RequestTypeId, RequestTypeTitle FROM ShiftRequestTypes WHERE Active = 1 ORDER BY SortOrder ASC"
                FillDropDown(Me.cmbRequestType, str, True)
            ElseIf Condition = "CurrentSG" Then
                If Me.cmbEmployee.Value > 0 Then
                    str = "SELECT ShiftGroupTable.ShiftGroupId, ShiftGroupTable.ShiftGroupName FROM ShiftGroupTable LEFT OUTER JOIN tblDefEmployee ON ShiftGroupTable.ShiftGroupId = tblDefEmployee.ShiftGroupId WHERE ShiftGroupTable.Active = 1 AND tblDefEmployee.Employee_ID = " & Me.cmbEmployee.Value & " ORDER BY ShiftGroupTable.SortOrder"
                Else
                    str = "SELECT ShiftGroupId, ShiftGroupName FROM ShiftGroupTable WHERE Active = 1 ORDER BY SortOrder ASC"
                End If
                FillDropDown(Me.cmbCurrentShift, str, True)
            ElseIf Condition = "NewSG" Then
                str = "SELECT ShiftGroupId, ShiftGroupName FROM ShiftGroupTable WHERE Active = 1 AND ShiftGroupId <> " & Me.cmbCurrentShift.SelectedValue & " ORDER BY SortOrder ASC"
                FillDropDown(Me.cmbNewShift, str, True)
            ElseIf Condition = "CurrentCC" Then
                If Me.cmbEmployee.Value > 0 Then
                    str = "SELECT tblDefCostCenter.CostCenterID, tblDefCostCenter.Name FROM tblDefCostCenter LEFT OUTER JOIN tblDefEmployee ON tblDefCostCenter.CostCenterID = tblDefEmployee.CostCentre WHERE tblDefCostCenter.Active = 1 AND tblDefEmployee.Employee_ID = " & Me.cmbEmployee.Value & " ORDER BY tblDefCostCenter.SortOrder"
                Else
                    str = "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1 ORDER BY SortOrder ASC"
                End If
                FillDropDown(Me.cmbCurrentCostCenter, str, True)
            ElseIf Condition = "NewCC" Then
                str = "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1 AND CostCenterID <> " & Me.cmbCurrentCostCenter.SelectedValue & " ORDER BY SortOrder ASC"
                FillDropDown(Me.cmbNewCostCenter, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Fill model properties with controls values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ShiftChangeRequestBE
            objModel.Detail = New List(Of ShiftChangeRequestDetailBE)
            If Me.btnSave.Text = "&Save" Then
                objModel.DocNo = GetDocumentNo()
                objModel.RequestId = 0
            Else
                objModel.DocNo = Me.txtDocNo.Text
                objModel.RequestId = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.RequestId).Value)
            End If
            objModel.DocDate = Me.dtpDocDate.Value
            objModel.RequestTypeId = Me.cmbRequestType.SelectedValue
            objModel.Remarks = Me.txtRemarks.Text
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New ShiftChangeRequestDetailBE
                Detail.RequestDetailId = Val(Row.Cells(grdDetail.RequestDeailId).Value)
                Detail.EmployeeId = Val(Row.Cells(grdDetail.EmployeeId).Value)
                Detail.CurrentShifId = Val(Row.Cells(grdDetail.CurrentShiftId).Value)
                Detail.NewShiftId = Val(Row.Cells(grdDetail.NewShiftId).Value)
                Detail.CurrentCostCenterId = Val(Row.Cells(grdDetail.CurrentCostCenterId).Value)
                Detail.NewCostCenterId = Val(Row.Cells(grdDetail.NewCostCenterId).Value)
                Detail.Comments = Row.Cells(grdDetail.Comments).Value.ToString
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Fill all controls in edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EditRecords()
        Try
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grdHistory.DocNo).Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grdHistory.DocDate).Value
            Me.cmbRequestType.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grdHistory.RequestTypeId).Value)
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells(grdHistory.Remarks).Value.ToString
            GetAllRecords("Detail")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Add detail records to grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddToGrid()
        Dim dt As New DataTable
        Try
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(grdDetail.RequestDeailId) = Val(0)
            dr.Item(grdDetail.EmployeeId) = Me.cmbEmployee.Value
            dr.Item(grdDetail.EmployeeCode) = Me.cmbEmployee.ActiveRow.Cells(2).Value
            dr.Item(grdDetail.EmployeeName) = Me.cmbEmployee.ActiveRow.Cells(1).Value
            If Me.cmbCurrentShift.SelectedValue > 0 Then
                dr.Item(grdDetail.CurrentShiftId) = Me.cmbCurrentShift.SelectedValue
                dr.Item(grdDetail.CurrentShift) = Me.cmbCurrentShift.Text
            Else
                dr.Item(grdDetail.CurrentShiftId) = 0
                dr.Item(grdDetail.CurrentShift) = ""
            End If
            If Me.cmbNewShift.SelectedValue > 0 Then
                dr.Item(grdDetail.NewShiftId) = Me.cmbNewShift.SelectedValue
                dr.Item(grdDetail.NewShift) = Me.cmbNewShift.Text
            Else
                dr.Item(grdDetail.NewShiftId) = 0
                dr.Item(grdDetail.NewShift) = ""
            End If
            If Me.cmbCurrentCostCenter.SelectedValue > 0 Then
                dr.Item(grdDetail.CurrentCostCenterId) = Me.cmbCurrentCostCenter.SelectedValue
                dr.Item(grdDetail.CurrentCostCenter) = Me.cmbCurrentCostCenter.Text
            Else
                dr.Item(grdDetail.CurrentCostCenterId) = 0
                dr.Item(grdDetail.CurrentCostCenter) = ""
            End If
            If Me.cmbNewCostCenter.SelectedValue > 0 Then
                dr.Item(grdDetail.NewCostCenterId) = Me.cmbNewCostCenter.SelectedValue
                dr.Item(grdDetail.NewCostCenter) = Me.cmbNewCostCenter.Text
            Else
                dr.Item(grdDetail.NewCostCenterId) = 0
                dr.Item(grdDetail.NewCostCenter) = ""
            End If
            dr.Item(grdDetail.Comments) = Me.txtComments.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Get all records to fill detail and history grid
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "History" Then
                str = "SELECT SCRM.RequestId, SCRM.DocNo, SCRM.DocDate, SCRM.RequestTypeId, SRT.RequestTypeTitle, SCRM.Remarks FROM ShiftRequestTypes AS SRT RIGHT OUTER JOIN ShiftChangeRequestMaster AS SCRM ON SRT.RequestTypeId = SCRM.RequestTypeId ORDER BY SCRM.RequestId DESC"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("History")
            ElseIf Condition = "Detail" Then
                str = "SELECT SCRD.RequestDetailId, SCRD.EmployeeId, EMP.Employee_Code AS EmployeeCode, EMP.Employee_Name AS EmployeeName, SCRD.CurrentShifId, CSG.ShiftGroupName AS CurrentShift, SCRD.NewShiftId, SG.ShiftGroupName AS NewShift, SCRD.CurrentCostCenterId, CCC.Name AS CurrentCostCenter, SCRD.NewCostCenterId, CC.Name AS NewCostCenter, SCRD.Comments FROM ShiftChangeRequestDetail AS SCRD INNER JOIN tblDefEmployee AS EMP ON SCRD.EmployeeId = EMP.Employee_ID LEFT OUTER JOIN tblDefCostCenter AS CC ON SCRD.NewCostCenterId = CC.CostCenterID LEFT OUTER JOIN tblDefCostCenter AS CCC ON SCRD.CurrentCostCenterId = CCC.CostCenterID LEFT OUTER JOIN ShiftGroupTable AS SG ON SCRD.NewShiftId = SG.ShiftGroupId LEFT OUTER JOIN ShiftGroupTable AS CSG ON SCRD.CurrentShifId = CSG.ShiftGroupId WHERE SCRD.RequestId = " & Val(Me.grdSaved.GetRow.Cells(grdHistory.RequestId).Value) & ""
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings("Detail")
            Else
                str = "SELECT SCRD.RequestDetailId, SCRD.EmployeeId, EMP.Employee_Code AS EmployeeCode, EMP.Employee_Name AS EmployeeName, SCRD.CurrentShifId, CSG.ShiftGroupName AS CurrentShift, SCRD.NewShiftId, SG.ShiftGroupName AS NewShift, SCRD.CurrentCostCenterId, CCC.Name AS CurrentCostCenter, SCRD.NewCostCenterId, CC.Name AS NewCostCenter, SCRD.Comments FROM ShiftChangeRequestDetail AS SCRD INNER JOIN tblDefEmployee AS EMP ON SCRD.EmployeeId = EMP.Employee_ID LEFT OUTER JOIN tblDefCostCenter AS CC ON SCRD.NewCostCenterId = CC.CostCenterID LEFT OUTER JOIN tblDefCostCenter AS CCC ON SCRD.CurrentCostCenterId = CCC.CostCenterID LEFT OUTER JOIN ShiftGroupTable AS SG ON SCRD.NewShiftId = SG.ShiftGroupId LEFT OUTER JOIN ShiftGroupTable AS CSG ON SCRD.CurrentShifId = CSG.ShiftGroupId WHERE SCRD.RequestId = -1"
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Validation before save
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocNo.Text = "" Then
                ShowErrorMessage("Please enter the valid Doc No")
                Me.txtDocNo.Focus()
                Return False
            ElseIf Me.grd.RowCount = 0 Then
                ShowErrorMessage("Detail grid is empty")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Validation before add detail records to grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidAddToGrid() As Boolean
        Try
            If Me.cmbEmployee.Value = 0 Then
                ShowErrorMessage("Please select any employee")
                Me.cmbEmployee.Focus()
                Return False
            ElseIf Me.cmbNewShift.SelectedValue = 0 Then
                If Not Me.cmbNewCostCenter.SelectedValue > 0 Then
                    ShowErrorMessage("Select new Shift group")
                    Me.cmbNewShift.Focus()
                    Return False
                End If
            ElseIf Me.cmbNewCostCenter.SelectedValue = 0 Then
                If Not Me.cmbNewShift.SelectedValue > 0 Then
                    ShowErrorMessage("Select new Cost Center")
                    Me.cmbNewCostCenter.Focus()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Reset controls to default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Detail" Then
                Me.cmbEmployee.Value = 0
                Me.cmbCurrentShift.SelectedValue = 0
                Me.cmbNewShift.SelectedValue = 0
                Me.cmbCurrentCostCenter.SelectedValue = 0
                Me.cmbNewCostCenter.SelectedValue = 0
                Me.txtComments.Text = ""
            Else
                Me.txtDocNo.Text = GetDocumentNo()
                Me.dtpDocDate.Value = Date.Now
                Me.txtRemarks.Text = ""
                FillCombos("Employee")
                FillCombos("RequestTypes")
                FillCombos("CurrentSG")
                FillCombos("NewSG")
                FillCombos("CurrentCC")
                FillCombos("NewCC")
                Me.cmbRequestType.SelectedValue = 0
                Me.cmbEmployee.Value = 0
                Me.cmbCurrentShift.SelectedValue = 0
                Me.cmbCurrentShift.Enabled = True
                Me.cmbNewShift.SelectedValue = 0
                Me.cmbCurrentCostCenter.SelectedValue = 0
                Me.cmbCurrentCostCenter.Enabled = True
                Me.cmbNewCostCenter.SelectedValue = 0
                Me.txtComments.Text = ""
                Me.btnSave.Text = "&Save"
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
                GetAllRecords("History")
                GetAllRecords()
                CtrlGrdBar1_Load(Nothing, Nothing)
                Me.UltraTabControl1.Tabs(0).Selected = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Save Records using DAL and in activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New ShiftChangeRequestDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("HRM", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
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
    ''' 'Ali Faisal : TFS1523 : Update records using DAL and insert into activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New ShiftChangeRequestDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("HRM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
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
    ''' 'Ali Faisal : TFS1523 : Add button click handling to add detail records to grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsValidAddToGrid() = True Then
                AddToGrid()
                ReSetControls("Detail")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : New button handling to reset all controls to their default values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Edit button click handling to edit records
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Save and Update on save button click
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
    ''' 'Ali Faisal : TFS1523 : Delete records on delete button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' 'Ali Faisal : TFS1523 : Refresh controls to get new values from DB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbEmployee.Value
            FillCombos("Employee")
            Me.cmbEmployee.Value = id

            id = Me.cmbRequestType.SelectedValue
            FillCombos("RequestTypes")
            Me.cmbRequestType.SelectedValue = id

            id = Me.cmbCurrentShift.SelectedValue
            FillCombos("CurrentSG")
            Me.cmbCurrentShift.SelectedValue = id

            id = Me.cmbNewShift.SelectedValue
            FillCombos("NewSG")
            Me.cmbNewShift.SelectedValue = id

            id = Me.cmbCurrentCostCenter.SelectedValue
            FillCombos("CurrentCC")
            Me.cmbCurrentCostCenter.SelectedValue = id

            id = Me.cmbNewCostCenter.SelectedValue
            FillCombos("NewCC")
            Me.cmbNewCostCenter.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@RequestId", Val(Me.grdSaved.CurrentRow.Cells(grdHistory.RequestId).Value))
            ShowReport("rptEmpShiftGroupChangeRequest")
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
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Ctrl grid bar to get saved layout
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Shift Group Change request Records"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Hot keys
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFineDeduction_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    ''' 'Ali Faisal : TFS1523 : Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmShiftChangeRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.cmbEmployee.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Buttons visibility control on tab change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Delete detail row from grid and DB too
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New ShiftChangeRequestDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdDetail.RequestDeailId).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEmployee_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployee.ValueChanged
        Try
            FillCombos("CurrentSG")
            If Me.cmbCurrentShift.Items.Count > 1 Then
                Me.cmbCurrentShift.SelectedIndex = 1
                Me.cmbCurrentShift.Enabled = False
            End If
            FillCombos("CurrentCC")
            If Me.cmbCurrentCostCenter.Items.Count > 1 Then
                Me.cmbCurrentCostCenter.SelectedIndex = 1
                Me.cmbCurrentCostCenter.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCurrentShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrentShift.SelectedIndexChanged
        Try
            FillCombos("NewSG")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCurrentCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrentCostCenter.SelectedIndexChanged
        Try
            FillCombos("NewCC")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class