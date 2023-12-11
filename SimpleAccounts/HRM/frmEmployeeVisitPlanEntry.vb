'Ali Faisal : TFS1533 : Add new form to save, update & delete Visit Plans
Imports SBDal
Imports SBModel
Public Class frmEmployeeVisitPlanEntry
    Implements IGeneral
    Dim objDAL As EmployeeVisitPlanDAL
    Dim objModel As EmployeeVisitPlanBE
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Set indexes for history grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grdHistory
        PlanId
        DocNo
        DocDate
        EmployeeId
        EmployeeCode
        EmployeeName
        TimeFrom
        TimeTo
        Remarks
    End Enum
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Get New Doc No to Save in Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("EVP-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblEmployeeVisitPlan", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("EVP-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblEmployeeVisitPlan", "DocNo")
            Else
                Return GetNextDocNo("EVP-", 6, "tblEmployeeVisitPlan", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Apply grid settings to hide some columns and to apply fomating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grdHistory.PlanId).Visible = False
            Me.grdSaved.RootTable.Columns(grdHistory.DocDate).FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grdHistory.EmployeeId).Visible = False
            Me.grdSaved.RootTable.Columns(grdHistory.TimeFrom).FormatString = "hh:mm:ss tt"
            Me.grdSaved.RootTable.Columns(grdHistory.TimeTo).FormatString = "hh:mm:ss tt"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Apply security to show specific controls to standard user
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
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Delete function to remove records using DAL
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New EmployeeVisitPlanDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.PlanId).Value.ToString)) = True Then
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
    ''' 'Ali Faisal : TFS1533 : Fill dropdowns for selection
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
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Fill model properties with controls values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New EmployeeVisitPlanBE
            If Me.btnSave.Text = "&Save" Then
                objModel.DocNo = GetDocumentNo()
                objModel.PlanId = 0
            Else
                objModel.DocNo = Me.txtDocNo.Text
                objModel.PlanId = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.PlanId).Value)
            End If
            objModel.DocDate = Me.dtpDocDate.Value
            objModel.EmployeeId = Me.cmbEmployee.Value
            objModel.TimeFrom = Me.dtpTimeFrom.Value
            objModel.TimeTo = Me.dtpTimeTo.Value
            objModel.Remarks = Me.txtRemarks.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Fill all controls in edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EditRecords()
        Try
            If frmEmployeeVisitPlan.PlanId > 0 Then
                GetAllRecords("SinglePlan")
                Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grdHistory.DocNo).Value.ToString
                Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grdHistory.DocDate).Value
                Me.cmbEmployee.Value = Val(Me.grdSaved.GetRow.Cells(grdHistory.EmployeeId).Value)
                Me.dtpTimeFrom.Value = Me.grdSaved.GetRow.Cells(grdHistory.TimeFrom).Value
                Me.dtpTimeTo.Value = Me.grdSaved.GetRow.Cells(grdHistory.TimeTo).Value
                Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells(grdHistory.Remarks).Value.ToString
                GetAllRecords()
            Else
                Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grdHistory.DocNo).Value.ToString
                Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grdHistory.DocDate).Value
                Me.cmbEmployee.Value = Val(Me.grdSaved.GetRow.Cells(grdHistory.EmployeeId).Value)
                Me.dtpTimeFrom.Value = Me.grdSaved.GetRow.Cells(grdHistory.TimeFrom).Value
                Me.dtpTimeTo.Value = Me.grdSaved.GetRow.Cells(grdHistory.TimeTo).Value
                Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells(grdHistory.Remarks).Value.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Get all records to fill detail and history grid
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT EVP.PlanId, EVP.DocNo, EVP.DocDate, EVP.EmployeeId, Emp.Employee_Code AS [Emp Code], Emp.Employee_Name AS [Emp Name], EVP.TimeFrom, EVP.TimeTo, EVP.Remarks FROM tblEmployeeVisitPlan AS EVP INNER JOIN tblDefEmployee AS Emp ON EVP.EmployeeId = Emp.Employee_ID ORDER BY EVP.PlanId DESC"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
            If Condition = "SinglePlan" Then
                Dim str1 As String = ""
                Dim dt1 As DataTable
                str1 = "SELECT EVP.PlanId, EVP.DocNo, EVP.DocDate, EVP.EmployeeId, Emp.Employee_Code AS [Emp Code], Emp.Employee_Name AS [Emp Name], EVP.TimeFrom, EVP.TimeTo, EVP.Remarks FROM tblEmployeeVisitPlan AS EVP INNER JOIN tblDefEmployee AS Emp ON EVP.EmployeeId = Emp.Employee_ID WHERE EVP.PlanId = " & frmEmployeeVisitPlan.PlanId & " ORDER BY EVP.PlanId DESC"
                dt1 = GetDataTable(str1)
                Me.grdSaved.DataSource = dt1
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Validation before save
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
                Return False
            ElseIf Me.txtRemarks.Text = "" Then
                ShowErrorMessage("Please enter remarks")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Reset controls to default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Date.Now
            FillCombos("Employee")
            Me.cmbEmployee.Value = 0
            Me.txtRemarks.Text = ""
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            GetAllRecords()
            Me.dtpDocDate.Value = frmEmployeeVisitPlan.PlanDate
            Me.dtpTimeFrom.Value = frmEmployeeVisitPlan.PlanDate
            Me.dtpTimeTo.Value = frmEmployeeVisitPlan.PlanDate
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Save Records using DAL and in activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New EmployeeVisitPlanDAL
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
    ''' 'Ali Faisal : TFS1533 : Update records using DAL and insert into activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New EmployeeVisitPlanDAL
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
    ''' 'Ali Faisal : TFS1533 : New button handling to reset all controls to their default values
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
    ''' 'Ali Faisal : TFS1533 : Edit button click handling to edit records
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
    ''' 'Ali Faisal : TFS1533 : Save and Update on save button click
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
    ''' 'Ali Faisal : TFS1533 : Delete records on delete button click
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
    ''' 'Ali Faisal : TFS1533 : Refresh controls to get new values from DB
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            'GetCrystalReportRights()
            'AddRptParam("@PlanId", Val(Me.grdSaved.CurrentRow.Cells(grdHistory.PlanId).Value))
            'ShowReport("rptEmpVisitPlan")
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
    ''' 'Ali Faisal : TFS1533 : Ctrl grid bar to get saved layout
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Visit Plan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Hot keys
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
    ''' 'Ali Faisal : TFS1533 : Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEmployeeVisitPlanEntry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.cmbEmployee.Focus()
            If frmEmployeeVisitPlan.PlanId > 0 Then
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Buttons visibility control on tab change
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class