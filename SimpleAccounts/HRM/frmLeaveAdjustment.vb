'Ali Faisal : TFS1481 : 21-Sep-2017 : Added new form to save leave adjustment
Imports SBDal
Imports SBModel
Public Class frmLeaveAdjustment
    Implements IGeneral
    Dim objDAL As LeaveAdjustmentDAL
    Dim objModel As LeaveAdjustmentBE
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Set indexing of columns for detail grid
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Enum grdDetail
        AdjustDetailId
        AdjustId
        EmployeeId
        EmployeeCode
        EmployeeName
        AdjustDays
        LeaveTypeId
        LeaveType
        ReasonId
        Reason
        Comments
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Set indexing of columns for history grid
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Enum grdHistory
        AdjustId
        AdjustNo
        AdjustDate
        Remarks
    End Enum
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("LA-" + Microsoft.VisualBasic.Right(Me.dtpAdjustmentDate.Value.Year, 2) + "-", "tblLeaveAdjustMaster", "AdjustNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("LA-" & Format(Me.dtpAdjustmentDate.Value, "yy") & Me.dtpAdjustmentDate.Value.Month.ToString("00"), 4, "tblLeaveAdjustMaster", "AdjustNo")
            Else
                Return GetNextDocNo("LA-", 6, "tblLeaveAdjustMaster", "AdjustNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Apply grid settings to show/hide columns in detail and history grid
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Detail" Then
                Me.grd.RootTable.Columns(grdDetail.AdjustDetailId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AdjustId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.EmployeeId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.ReasonId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.LeaveTypeId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AdjustDays).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.AdjustDays).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
            ElseIf Condition = "History" Then
                Me.grdSaved.RootTable.Columns(grdHistory.AdjustId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.AdjustDate).FormatString = "" & str_DisplayDateFormat
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Apply security rights for standard user to get enabled that buttons that he/she have rights
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
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
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New LeaveAdjustmentDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustId).Value.ToString)) = True Then
                'Insert Activity Log by Ali Faisal
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtAdjustmentNo.Text, True)
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
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Reason" Then
                str = "SELECT ReasonId, ReasonTitle FROM tblDefLeaveReasons WHERE Active = 1"
                FillDropDown(Me.cmbReason, str, True)
            ElseIf Condition = "Employee" Then
                str = "SELECT e.Employee_ID ,e.Employee_Name,e.Employee_Code, ed.EmployeeDeptName , p.EmployeeDesignationName from tblDefEmployee e inner join  EmployeeDeptDefTable ed on e.Dept_id=ed.EmployeeDeptId inner join EmployeeDesignationDefTable p on p.EmployeeDesignationId=e.Desig_ID"
                FillUltraDropDown(Me.cmbEmployee, str, True)
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
            ElseIf Condition = "LeaveType" Then
                str = "SELECT Id, LeaveTypeTitle FROM tblDefLeaveTypes WHERE Active = 1"
                FillDropDown(Me.cmbLeaveTypes, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Fill controls to show in edit mode
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub EditRecords()
        Try
            Me.txtAdjustmentNo.Text = Me.grdSaved.GetRow.Cells(grdHistory.AdjustNo).Value.ToString
            Me.dtpAdjustmentDate.Value = Me.grdSaved.GetRow.Cells(grdHistory.AdjustDate).Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells(grdHistory.Remarks).Value.ToString
            GetAllRecords("Detail")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Fill model properties from controls for DAL usage
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New LeaveAdjustmentBE
            objModel.Detail = New List(Of LeaveAdjustmentDetailBE)
            If Me.btnSave.Text = "&Save" Then
                objModel.AdjustId = 0
                objModel.AdjustNo = GetDocumentNo()
                objModel.AdjustDate = Me.dtpAdjustmentDate.Value
                objModel.Remarks = Me.txtRemarks.Text
            Else
                objModel.AdjustId = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustId).Value)
                objModel.AdjustNo = Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustNo).Value.ToString
                objModel.AdjustDate = Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustDate).Value
                objModel.Remarks = Me.grdSaved.CurrentRow.Cells(grdHistory.Remarks).Value.ToString
            End If
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New LeaveAdjustmentDetailBE
                Detail.AdjustDetailId = Val(Row.Cells(grdDetail.AdjustDetailId).Value)
                Detail.EmployeeId = Val(Row.Cells(grdDetail.EmployeeId).Value)
                Detail.AdjustDays = Row.Cells(grdDetail.AdjustDays).Value.ToString
                Detail.LeaveTypeId = Val(Row.Cells(grdDetail.LeaveTypeId).Value)
                Detail.ReasonId = Val(Row.Cells(grdDetail.ReasonId).Value)
                Detail.Comments = Row.Cells(grdDetail.Comments).Value.ToString
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Get all records to show history
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "Detail" Then
                If Me.btnSave.Text = "&Save" Then
                    str = "SELECT Adj.AdjustDetailId, Adj.AdjustId, Adj.EmployeeId, Emp.Employee_Code As EmployeeCode, Emp.Employee_Name AS EmployeeName, Adj.AdjustDays, 0 As LeaveTypeId, '' As LeaveType, Adj.ReasonId, Reason.ReasonTitle, Adj.Comments FROM tblLeaveAdjustDetail AS Adj INNER JOIN tblDefEmployee AS Emp ON Adj.EmployeeId = Emp.Employee_ID LEFT OUTER JOIN tblDefLeaveReasons AS Reason ON Adj.ReasonId = Reason.ReasonId  WHERE Adj.AdjustId = -1 ORDER BY Adj.AdjustDetailId ASC"
                Else
                    str = "SELECT Adj.AdjustDetailId, Adj.AdjustId, Adj.EmployeeId, Emp.Employee_Code As EmployeeCode, Emp.Employee_Name AS EmployeeName, Adj.AdjustDays, Adj.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, Adj.ReasonId, Reason.ReasonTitle, Adj.Comments FROM tblLeaveAdjustDetail AS Adj INNER JOIN tblDefEmployee AS Emp ON Adj.EmployeeId = Emp.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON Adj.LeaveTypeId = Type.Id LEFT OUTER JOIN tblDefLeaveReasons AS Reason ON Adj.ReasonId = Reason.ReasonId WHERE Adj.AdjustId = " & Val(Me.grdSaved.GetRow.Cells(grdHistory.AdjustId).Value) & " ORDER BY Adj.AdjustDetailId ASC"
                End If
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings("Detail")
            ElseIf Condition = "History" Then
                str = "SELECT AdjustId, AdjustNo, AdjustDate, Remarks FROM tblLeaveAdjustMaster ORDER BY AdjustId DESC"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("History")
            End If
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
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtAdjustmentNo.Text = "" Then
                msg_Error("Please enter the valid adjustment number")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Function IsValidToAdd() As Boolean
        Try
            If Me.cmbEmployee.Value = 0 Then
                msg_Error("Please select any employee")
                Return False
            ElseIf Me.txtAdjustDays.Text = 0 Or Me.txtAdjustDays.Text = "" Then
                msg_Error("Please enter the adjust days")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Reset controls to set default values to all controls
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Detail" Then
                FillCombos("Employee")
                FillCombos("Reason")
                FillCombos("LeaveType")
                Me.cmbEmployee.Value = 0
                Me.txtAdjustDays.Text = 0
                Me.cmbReason.SelectedIndex = 0
                Me.txtComments.Text = ""
            ElseIf Condition = "History" Then
                Me.txtAdjustmentNo.Text = GetDocumentNo()
                Me.dtpAdjustmentDate.Value = Now
                Me.txtRemarks.Text = ""
                Me.cmbLeaveTypes.SelectedIndex = 0
                Me.btnSave.Text = "&Save"
                Me.btnDelete.Visible = False
                GetAllRecords("History")
                GetAllRecords("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Save records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New LeaveAdjustmentDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtAdjustmentNo.Text, True)
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
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New LeaveAdjustmentDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtAdjustmentNo.Text, True)
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
    Private Sub frmLeaveAdjustment_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            ReSetControls("Detail")
            ReSetControls("History")
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
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
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

            id = Me.cmbEmployee.Value
            FillCombos("Employee")
            Me.cmbEmployee.Value = id

            id = Me.cmbReason.SelectedValue
            FillCombos("Reason")
            Me.cmbReason.SelectedValue = id

            id = Me.cmbLeaveTypes.SelectedValue
            FillCombos("LeaveType")
            Me.cmbLeaveTypes.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLeaveAdjustment_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Leave Adjustment"
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
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Detail record add to grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr(grdDetail.AdjustId) = 0
            dr(grdDetail.AdjustDetailId) = 0
            dr(grdDetail.EmployeeId) = Me.cmbEmployee.Value
            dr(grdDetail.EmployeeCode) = Me.cmbEmployee.ActiveRow.Cells(2).Value.ToString
            dr(grdDetail.EmployeeName) = Me.cmbEmployee.Text
            dr(grdDetail.AdjustDays) = Me.txtAdjustDays.Text
            dr(grdDetail.LeaveTypeId) = Me.cmbLeaveTypes.SelectedValue
            dr(grdDetail.LeaveType) = IIf(Me.cmbLeaveTypes.SelectedValue > 0, Me.cmbLeaveTypes.Text, "")
            dr(grdDetail.ReasonId) = Me.cmbReason.SelectedValue
            dr(grdDetail.Reason) = IIf(Me.cmbReason.SelectedValue > 0, Me.cmbReason.Text, "")
            dr(grdDetail.Comments) = Me.txtComments.Text
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsValidToAdd() = True Then
                AddToGrid()
                ReSetControls("Detail")
                Me.cmbEmployee.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Delete detail row from database and grid too
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New LeaveAdjustmentDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.GetRow.Cells(grdDetail.AdjustDetailId).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class