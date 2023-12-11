'Ali Faisal : TFS1529 : Add form to save, update, delete Employee Warning info on 05-Oct-2017
Imports SBDal
Imports SBModel
Public Class frmEmployeeWarning
    Implements IGeneral
    Dim objModel As EmployeeWarningBE
    Dim objDAL As EmployeeWarningDAL
    ''' <summary>
    ''' Ali Faisal : TFS1529 : History grid indexing
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        WarningId
        DocNo
        DocDate
        EmployeeId
        EmployeeName
        WarnById
        WarnByName
        WarningTypeId
        WarningType
        Reason
        Description
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Get next documnet number
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("EW-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblEmployeeWarning", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("EW-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblEmployeeWarning", "DocNo")
            Else
                Return GetNextDocNo("EW-", 6, "tblEmployeeWarning", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Apply grid settings to hide some columns and also apply formating
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.WarningId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DocDate).FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.EmployeeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.WarnById).Visible = False
            Me.grdSaved.RootTable.Columns(grd.WarningTypeId).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Apply security to show specific controls to standard users
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
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Print" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Delete records using DAL and insert into activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New EmployeeWarningDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grd.WarningId).Value.ToString)) = True Then
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
    ''' Ali Faisal : TFS1529 : Fill all dropdowns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Employee" Then
                str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1"
                FillUltraDropDown(Me.cmbEmployee, str, True)
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                Me.cmbEmployee.Rows(0).Activate()
            ElseIf Condition = "WarningType" Then
                str = "SELECT WarningTypeId, WarningType FROM tblDefWarningTypes WHERE Active = 1 ORDER BY SortOrder ASC"
                FillDropDown(Me.cmbWarningType, str, True)
            ElseIf Condition = "WarnBy" Then
                str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmployee.Value & ")"
                FillUltraDropDown(Me.cmbWarningBy, str, True)
                Me.cmbWarningBy.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                Me.cmbWarningBy.Rows(0).Activate()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Fill model properties from controls values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New EmployeeWarningBE
            If Me.btnSave.Text = "&Save" Then
                objModel.WarningId = 0
                objModel.DocNo = GetDocumentNo()
            Else
                objModel.WarningId = Val(Me.grdSaved.CurrentRow.Cells(grd.WarningId).Value)
                objModel.DocNo = Me.txtDocNo.Text
            End If
            objModel.DocDate = Me.dtpDocDate.Value
            objModel.EmployeeId = Me.cmbEmployee.Value
            objModel.WarnById = Me.cmbWarningBy.Value
            objModel.WarningTypeId = Me.cmbWarningType.SelectedValue
            objModel.Reason = Me.txtReason.Text
            objModel.Description = Me.txtDescription.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Fill all controls from saved values in edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EditRecords()
        Try
            Me.btnSave.Text = "&Update"
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grd.DocNo).Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grd.DocDate).Value
            Me.cmbEmployee.Value = Val(Me.grdSaved.GetRow.Cells(grd.EmployeeId).Value)
            Me.cmbWarningBy.Value = Val(Me.grdSaved.GetRow.Cells(grd.WarnById).Value)
            Me.cmbWarningType.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.WarningTypeId).Value)
            Me.txtReason.Text = Me.grdSaved.GetRow.Cells(grd.Reason).Value.ToString
            Me.txtDescription.Text = Me.grdSaved.GetRow.Cells(grd.Description).Value.ToString
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Get all records to fill history
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT tblEmployeeWarning.WarningId, tblEmployeeWarning.DocNo, tblEmployeeWarning.DocDate, tblEmployeeWarning.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblEmployeeWarning.WarnById, tblDefEmployee_1.Employee_Name AS WarningBy, tblEmployeeWarning.WarningTypeId, tblDefWarningTypes.WarningType, tblEmployeeWarning.Reason, tblEmployeeWarning.Description " _
                & "FROM tblEmployeeWarning INNER JOIN tblDefEmployee ON tblEmployeeWarning.EmployeeId = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON tblEmployeeWarning.WarnById = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefWarningTypes ON tblEmployeeWarning.WarningTypeId = tblDefWarningTypes.WarningTypeId ORDER BY tblEmployeeWarning.WarningId DESC"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Validation of records having value before save and update
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
            ElseIf Me.cmbEmployee.Value = 0 Then
                ShowErrorMessage("Please select any employee")
                Me.cmbEmployee.Focus()
                Return False
            ElseIf Me.cmbWarningBy.Value = 0 Then
                ShowErrorMessage("Please select any warning by person")
                Me.cmbWarningBy.Focus()
                Return False
            ElseIf Me.txtReason.Text = "" Then
                ShowErrorMessage("Please enter the leave reason")
                Me.txtReason.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Reset controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Date.Now
            FillCombos("Employee")
            FillCombos("WarnBy")
            FillCombos("WarningType")
            Me.cmbEmployee.Value = 0
            Me.cmbWarningBy.Value = 0
            Me.cmbWarningType.SelectedValue = 0
            Me.txtReason.Text = ""
            Me.txtDescription.Text = ""
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Save records using DAL and also insert record log to activity
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New EmployeeWarningDAL
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
    ''' Ali Faisal : TFS1529 : Update records using DAL and also insert record log to activity
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New EmployeeWarningDAL
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
    ''' Ali Faisal : TFS1529 : Hot keys
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEmployeeWarning_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    ''' Ali Faisal : TFS1529 : Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEmployeeWarning_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.cmbEmployee.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Call reset controls at new button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : call edit records method on button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Save and Update records on save button click
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
    ''' Ali Faisal : TFS1529 : Delete records at delete button click
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
    ''' Ali Faisal : TFS1529 : Refresh all dropdowns to get latest data from DB
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

            id = Me.cmbWarningType.SelectedValue
            FillCombos("WarningType")
            Me.cmbWarningType.SelectedValue = id

            id = Me.cmbWarningBy.Value
            FillCombos("WarnBy")
            Me.cmbWarningBy.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@WarningId", Val(Me.grdSaved.CurrentRow.Cells(grd.WarningId).Value))
            ShowReport("rptEmployeeWarning")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : edit records
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Load layout of grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Warning Records"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Filter employee other than the employee dropdown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbEmployee_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployee.ValueChanged
        Try
            FillCombos("WarnBy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Tab control change
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
End Class