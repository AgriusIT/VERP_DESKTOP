'Ali Faisal : TFS1528 : Add new form to save, update & delete Fine Deductions
Imports SBDal
Imports SBModel
Public Class frmFineDeduction
    Implements IGeneral
    Dim objDAL As EmployeeFineDeductionDAL
    Dim objModel As EmployeeFineDeductionBE
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Set indexes for detail grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grdDetail
        FineDetailId
        EmployeeId
        EmployeeCode
        EmployeeName
        DeductionId
        DeductionTitle
        Amount
        Reason
    End Enum
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Set indexes for history grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grdHistory
        FineId
        DocNo
        DocDate
    End Enum
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Get New Doc No to Save in Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("ED-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblEmployeeFineMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("ED-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblEmployeeFineMaster", "DocNo")
            Else
                Return GetNextDocNo("ED-", 6, "tblEmployeeFineMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Apply grid settings to hide some columns and to apply fomating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "History" Then
                Me.grdSaved.RootTable.Columns(grdHistory.FineId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.DocDate).FormatString = str_DisplayDateFormat
            ElseIf Condition = "Detail" Then
                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grd.RootTable.Columns(grdDetail.FineDetailId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.EmployeeId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.DeductionId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.Amount).FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns(grdDetail.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(grdDetail.Amount).TotalFormatString = "N" & DecimalPointInValue
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Apply security to show specific controls to standard user
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
    ''' 'Ali Faisal : TFS1528 : Delete function to remove records using DAL
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New EmployeeFineDeductionDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.FineId).Value.ToString)) = True Then
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
    ''' 'Ali Faisal : TFS1528 : Fill dropdowns for selection
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
            ElseIf Condition = "DeductionType" Then
                str = "SELECT DeductionId, DeductionTitle FROM tblDefDeductionTypes WHERE Active = 1"
                FillDropDown(Me.cmbDeductionType, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Fill model properties with controls values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New EmployeeFineDeductionBE
            objModel.Detail = New List(Of EmployeeFineDeductionDetailBE)
            If Me.btnSave.Text = "&Save" Then
                objModel.DocNo = GetDocumentNo()
                objModel.FineId = 0
            Else
                objModel.DocNo = Me.txtDocNo.Text
                objModel.FineId = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.FineId).Value)
            End If
            objModel.DocDate = Me.dtpDocDate.Value
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New EmployeeFineDeductionDetailBE
                Detail.FineDetailId = Val(Row.Cells(grdDetail.FineDetailId).Value)
                Detail.EmployeeId = Val(Row.Cells(grdDetail.EmployeeId).Value)
                Detail.DeductionId = Val(Row.Cells(grdDetail.DeductionId).Value)
                Detail.Amount = Val(Row.Cells(grdDetail.Amount).Value)
                Detail.Reason = Row.Cells(grdDetail.Reason).Value.ToString
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Fill all controls in edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EditRecords()
        Try
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grdHistory.DocNo).Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grdHistory.DocDate).Value
            GetAllRecords("Detail")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Add detail records to grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddToGrid()
        Dim dt As New DataTable
        Try
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(grdDetail.FineDetailId) = Val(0)
            dr.Item(grdDetail.EmployeeId) = Me.cmbEmployee.Value
            dr.Item(grdDetail.EmployeeCode) = Me.cmbEmployee.ActiveRow.Cells(2).Value
            dr.Item(grdDetail.EmployeeName) = Me.cmbEmployee.ActiveRow.Cells(1).Value
            dr.Item(grdDetail.DeductionId) = Me.cmbDeductionType.SelectedValue
            dr.Item(grdDetail.DeductionTitle) = Me.cmbDeductionType.Text
            dr.Item(grdDetail.Amount) = Me.txtAmount.Text
            dr.Item(grdDetail.Reason) = Me.txtReason.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Get all records to fill detail and history grid
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "History" Then
                str = "SELECT FineId, DocNo, DocDate FROM tblEmployeeFineMaster ORDER BY FineId DESC"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("History")
            ElseIf Condition = "Detail" Then
                str = "SELECT tblEmployeeFineDetail.FineDetailId, tblEmployeeFineDetail.EmployeeId, tblDefEmployee.Employee_Code AS EmployeeCode, tblDefEmployee.Employee_Name AS EmployeeName, tblEmployeeFineDetail.DeductionId, tblDefDeductionTypes.DeductionTitle, tblEmployeeFineDetail.Amount, tblEmployeeFineDetail.Reason FROM tblEmployeeFineDetail LEFT OUTER JOIN tblDefEmployee ON tblEmployeeFineDetail.EmployeeId = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefDeductionTypes ON tblEmployeeFineDetail.DeductionId = tblDefDeductionTypes.DeductionId WHERE tblEmployeeFineDetail.FineId = " & Val(Me.grdSaved.GetRow.Cells(grdHistory.FineId).Value) & ""
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings("Detail")
            Else
                str = "SELECT 0 AS FineDetailId, tblEmployeeFineDetail.EmployeeId, tblDefEmployee.Employee_Code AS EmployeeCode, tblDefEmployee.Employee_Name AS EmployeeName, tblEmployeeFineDetail.DeductionId, tblDefDeductionTypes.DeductionTitle, 0 AS Amount, tblEmployeeFineDetail.Reason FROM tblEmployeeFineDetail LEFT OUTER JOIN tblDefEmployee ON tblEmployeeFineDetail.EmployeeId = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefDeductionTypes ON tblEmployeeFineDetail.DeductionId = tblDefDeductionTypes.DeductionId WHERE tblEmployeeFineDetail.FineId = -1"
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
    ''' 'Ali Faisal : TFS1528 : Validation before save
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
    ''' 'Ali Faisal : TFS1528 : Validation before add detail records to grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidAddToGrid() As Boolean
        Try
            If Me.cmbEmployee.Value = 0 Then
                ShowErrorMessage("Please select any employee")
                Me.cmbEmployee.Focus()
                Return False
            ElseIf Me.txtAmount.Text = 0 Then
                ShowErrorMessage("Amount must be greater than 0")
                Me.txtAmount.Focus()
                Return False
            ElseIf Me.txtReason.Text = "" Then
                ShowErrorMessage("Please enter the valid reason")
                Me.txtReason.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Reset controls to default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Detail" Then
                Me.cmbEmployee.Value = 0
                Me.cmbDeductionType.SelectedValue = 0
                Me.txtAmount.Text = 0
                Me.txtReason.Text = ""
            Else
                Me.txtDocNo.Text = GetDocumentNo()
                Me.dtpDocDate.Value = Date.Now
                FillCombos("Employee")
                FillCombos("DeductionType")
                Me.cmbEmployee.Value = 0
                Me.cmbDeductionType.SelectedValue = 0
                Me.txtAmount.Text = 0
                Me.txtReason.Text = ""
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
    ''' 'Ali Faisal : TFS1528 : Save Records using DAL and in activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New EmployeeFineDeductionDAL
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
    ''' 'Ali Faisal : TFS1528 : Update records using DAL and insert into activity log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New EmployeeFineDeductionDAL
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
    ''' 'Ali Faisal : TFS1528 : Add button click handling to add detail records to grid
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
    ''' 'Ali Faisal : TFS1528 : New button handling to reset all controls to their default values
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
    ''' 'Ali Faisal : TFS1528 : Edit button click handling to edit records
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
    ''' 'Ali Faisal : TFS1528 : Save and Update on save button click
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
    ''' 'Ali Faisal : TFS1528 : Delete records on delete button click
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
    ''' 'Ali Faisal : TFS1528 : Refresh controls to get new values from DB
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

            id = Me.cmbDeductionType.SelectedValue
            FillCombos("DeductionType")
            Me.cmbDeductionType.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@FineId", Me.grdSaved.CurrentRow.Cells(grdHistory.FineId).Value.ToString)
            ShowReport("rptEmpFineDeduction")
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
    ''' 'Ali Faisal : TFS1528 : Ctrl grid bar to get saved layout
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Fine Deduction Details"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Hot keys
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
    ''' 'Ali Faisal : TFS1528 : Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFineDeduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.cmbEmployee.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Buttons visibility control on tab change
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
    ''' 'Ali Faisal : TFS1528 : Validation for only numeric value enter to text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
                e.Handled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Delete detail row from grid and DB too
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New EmployeeFineDeductionDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdDetail.FineDetailId).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class