'Ali Faisal : TFS1525 : Add form to save, update, delete leave application on 03-Oct-2017
''Muhammad Amin done TASK TFS3571 to show employees and history data according to rights based cost centers in case configuration is on. Dated 20-06-2018
Imports SBDal
Imports SBModel
Public Class frmNewLeaveApplication
    Implements IGeneral
    Dim objDAL As LeaveApplicationDAL
    Dim objModel As LeaveApplicationBE
    Dim CostCenterRights As Boolean = False

    ''' <summary>
    ''' Ali Faisal : TFS1525 : Set indexes of grid columns
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        ApplicationId
        ApplicationNo
        ApplicationDate
        EmployeeId
        EmployeeName
        ForwardToId
        ForwardTo
        LeaveTypeId
        LeaveType
        Reason
        AttendanceId
        Attendance
        FromDate
        ToDate
        PeriodId
        ContactNo
        Description
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Get Document number for application
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("APL-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblLeaveApplication", "ApplicationNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("APL-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblLeaveApplication", "ApplicationNo")
            Else
                Return GetNextDocNo("APL-", 6, "tblLeaveApplication", "ApplicationNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Apply grid settings to hide columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.ApplicationId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.EmployeeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ForwardToId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.LeaveTypeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.AttendanceId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PeriodId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ApplicationDate).FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.FromDate).FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.ToDate).FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Add security rights for standard user to use only specific controls
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
    ''' Ali Faisal : TFS1525 : Delete records using DAL Insert activity in log too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New LeaveApplicationDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grd.ApplicationId).Value.ToString)) = True Then
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
    ''' Ali Faisal : TFS1525 : Fill drop downs
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Employee" Then
                If CostCenterRights = False Then
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1"
                Else
                    ''TASK TFS3571
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")"
                    ''END TASK TFS3571
                End If
                FillUltraDropDown(Me.cmbEmp, str, True)
                Me.cmbEmp.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                Me.cmbEmp.Rows(0).Activate()
            ElseIf Condition = "LeaveType" Then
                str = "SELECT Id, LeaveTypeTitle FROM tblDefLeaveTypes WHERE Active = 1"
                FillDropDown(Me.cmbLeaveType, str, True)
            ElseIf Condition = "ForwardTo" Then
                'str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmp.Value & ")"
                If CostCenterRights = False Then
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmp.Value & ")"
                Else
                    ''TASK TFS3571
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmp.Value & ") AND CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")"
                    ''END TASK TFS3571
                End If
                FillDropDown(Me.cmbForwardTo, str, True)
            ElseIf Condition = "AttendanceStatus" Then
                str = "SELECT Att_Status_ID, Att_Status_Name FROM tblDefAttendenceStatus WHERE Active = 1 AND Att_Status_Name NOT IN ('Present','Absent','Outdoor Duty','Break','Off Day','Short Absent')"
                FillDropDown(Me.cmbAttendanceStatus, str, True)
            ElseIf Condition = "Period" Then
                str = "SELECT PeriodId, PeriodTitle FROM tblDefLeavePeriod WHERE Active = 1"
                FillDropDown(Me.cmbPeriod, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Fill model of values to model class properties 
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New LeaveApplicationBE
            If Me.btnSave.Text = "&Save" Then
                objModel.ApplicationId = 0
                objModel.ApplicationNo = GetDocumentNo()
            Else
                objModel.ApplicationId = Val(Me.grdSaved.CurrentRow.Cells(grd.ApplicationId).Value)
                objModel.ApplicationNo = Me.txtDocNo.Text
            End If
            objModel.ApplicationDate = Me.dtpDocDate.Value
            objModel.EmployeeId = Me.cmbEmp.Value
            objModel.Reason = Me.txtReason.Text
            objModel.LeaveTypeId = Me.cmbLeaveType.SelectedValue
            objModel.ForwardToId = Me.cmbForwardTo.SelectedValue
            objModel.AlternateContactNo = Me.txtAlternateContact.Text
            objModel.AttendanceStatusId = Me.cmbAttendanceStatus.SelectedValue
            objModel.FromDate = Me.dtpFromDate.Value
            If Me.dtpToDate.Visible = True Then
                objModel.ToDate = Me.dtpToDate.Value
            Else
                objModel.ToDate = Nothing
            End If
            If Me.cmbPeriod.Visible = True Then
                objModel.PeriodId = Me.cmbPeriod.SelectedValue
            Else
                objModel.PeriodId = 0
            End If
            Dim DateFrom As DateTime = Me.dtpFromDate.Value.ToString("yyyy-MM-dd")
            Dim DateTo As DateTime = Me.dtpToDate.Value.ToString("yyyy-MM-dd")
            objModel.NoOfDays = DateDiff(DateInterval.Day, DateFrom, DateTo)
            objModel.Description = Me.txtDescription.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Fill all controls from history in edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EditRecords()
        Try
            Me.btnSave.Text = "&Update"
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(grd.ApplicationNo).Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grd.ApplicationDate).Value
            Me.cmbEmp.Value = Val(Me.grdSaved.GetRow.Cells(grd.EmployeeId).Value)
            Me.txtReason.Text = Me.grdSaved.GetRow.Cells(grd.Reason).Value.ToString
            Me.cmbLeaveType.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.LeaveTypeId).Value)
            Me.cmbForwardTo.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.ForwardToId).Value)
            Me.txtAlternateContact.Text = Me.grdSaved.GetRow.Cells(grd.ContactNo).Value.ToString
            Me.cmbAttendanceStatus.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.AttendanceId).Value)
            Me.dtpFromDate.Value = Me.grdSaved.GetRow.Cells(grd.FromDate).Value
            If IsDBNull(Me.grdSaved.GetRow.Cells(grd.ToDate).Value) Then
                Me.dtpToDate.Value = Date.Now
            Else
                Me.dtpToDate.Value = Me.grdSaved.GetRow.Cells(grd.ToDate).Value
            End If

            Me.cmbPeriod.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.PeriodId).Value)
            Me.txtDescription.Text = Me.grdSaved.GetRow.Cells(grd.Description).Value.ToString
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Get all records to show in history
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = String.Empty
            If CostCenterRights = False Then
                str = "SELECT App.LeaveApplicationId AS ApplicationId, App.ApplicationNo, App.ApplicationDate, App.EmployeeId, Emp.Employee_Name AS EmployeeName, App.ForwardToId, Frwd.Employee_Name AS ForwardTo, App.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, App.ApplicationReason AS Reason, App.AttendanceStatusId AS AttendanceId, Status.Att_Status_Name AS Attendance, App.FromDate, App.ToDate, App.PeriodId, App.AlternateContactNo AS ContactNo, App.ApplicationDetails AS Description " _
                                  & "FROM tblLeaveApplication AS App INNER JOIN tblDefEmployee AS Emp ON App.EmployeeId = Emp.Employee_ID INNER JOIN tblDefEmployee AS Frwd ON App.ForwardToId = Frwd.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON App.LeaveTypeId = Type.Id INNER JOIN tblDefAttendenceStatus AS Status ON App.AttendanceStatusId = Status.Att_Status_ID ORDER BY App.LeaveApplicationId DESC"
            Else
                'e.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                ''TASK TFS3571
                str = "SELECT App.LeaveApplicationId AS ApplicationId, App.ApplicationNo, App.ApplicationDate, App.EmployeeId, Emp.Employee_Name AS EmployeeName, App.ForwardToId, Frwd.Employee_Name AS ForwardTo, App.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, App.ApplicationReason AS Reason, App.AttendanceStatusId AS AttendanceId, Status.Att_Status_Name AS Attendance, App.FromDate, App.ToDate, App.PeriodId, App.AlternateContactNo AS ContactNo, App.ApplicationDetails AS Description " _
                                & "FROM tblLeaveApplication AS App INNER JOIN tblDefEmployee AS Emp ON App.EmployeeId = Emp.Employee_ID INNER JOIN tblDefEmployee AS Frwd ON App.ForwardToId = Frwd.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON App.LeaveTypeId = Type.Id INNER JOIN tblDefAttendenceStatus AS Status ON App.AttendanceStatusId = Status.Att_Status_ID WHERE Emp.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY App.LeaveApplicationId DESC"
                ''END TFS3571
            End If
            Dim dt As DataTable = GetDataTable(Str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Validation of controls
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
            ElseIf Me.cmbEmp.Value = 0 Then
                ShowErrorMessage("Please select any employee")
                Me.cmbEmp.Focus()
                Return False
            ElseIf Me.txtReason.Text = "" Then
                ShowErrorMessage("Please enter the leave reason")
                Me.txtReason.Focus()
                Return False
            ElseIf Me.cmbAttendanceStatus.SelectedValue = 0 Then
                ShowErrorMessage("Please select any attendance status")
                Me.cmbAttendanceStatus.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Reset all controls to default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Date.Now
            ''TASK TFS3571
            If Not getConfigValueByType("RightBasedCostCenters") = "Error" Then
                CostCenterRights = CBool(getConfigValueByType("RightBasedCostCenters"))
            End If
            ''END TASK TFS3571
            FillCombos("Employee")
            FillCombos("LeaveType")
            FillCombos("ForwardTo")
            FillCombos("AttendanceStatus")
            FillCombos("Period")
            Me.cmbEmp.Value = 0
            Me.txtReason.Text = ""
            Me.cmbLeaveType.SelectedValue = 0
            Me.cmbForwardTo.SelectedValue = 0
            Me.txtAlternateContact.Text = ""
            Me.cmbAttendanceStatus.SelectedValue = 0
            Me.dtpFromDate.Value = Date.Now
            Me.dtpToDate.Value = Date.Now
            Me.txtDescription.Text = ""
            Me.cmbPeriod.Visible = False
            Me.lblPeriod.Visible = False
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False





            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Save records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New LeaveApplicationDAL
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
    ''' Ali Faisal : TFS1525 : Update records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New LeaveApplicationDAL
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
    ''' Ali Faisal : TFS1525 : Short keys for user friendly
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNewLeaveApplication_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    ''' Ali Faisal : TFS1525 : Form load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNewLeaveApplication_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.cmbEmp.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : New button click event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Edit button click event
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
    ''' Ali Faisal : TFS1525 : Save and Update handling on Save button event
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
    ''' Ali Faisal : TFS1525 : Delete button event
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
    ''' Ali Faisal : TFS1525 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ApplicationId", Me.grdSaved.CurrentRow.Cells(grd.ApplicationId).Value.ToString)
            ShowReport("rptEmpLeaveApplication")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Refresh controls to get data from database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I

            id = Me.cmbEmp.Value
            FillCombos("Employee")
            Me.cmbEmp.Value = id

            id = Me.cmbLeaveType.SelectedValue
            FillCombos("LeaveType")
            Me.cmbLeaveType.SelectedValue = id

            id = Me.cmbForwardTo.SelectedValue
            FillCombos("ForwardTo")
            Me.cmbForwardTo.SelectedValue = id

            id = Me.cmbAttendanceStatus.SelectedValue
            FillCombos("AttendanceStatus")
            Me.cmbAttendanceStatus.SelectedValue = id

            id = Me.cmbPeriod.SelectedValue
            FillCombos("Period")
            Me.cmbPeriod.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Edit controls on history grid double click
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
    ''' Ali Faisal : TFS1525 : Grid control load
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Leave Application Records"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
    ''' Ali Faisal : TFS1525 : Fill drop down for forward to employee other than the employee selected in employee dropdown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbEmp_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmp.ValueChanged
        Try
            FillCombos("ForwardTo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Period drop down fill on the bases of status index change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbAttendanceStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAttendanceStatus.SelectedIndexChanged
        Try
            If Me.cmbAttendanceStatus.Text = "Half Leave" Or Me.cmbAttendanceStatus.Text = "Short Leave" Then
                Me.lblPeriod.Visible = True
                Me.cmbPeriod.Visible = True
                Me.lblToDate.Visible = False
                Me.dtpToDate.Visible = False
            Else
                Me.lblPeriod.Visible = False
                Me.cmbPeriod.Visible = False
                Me.lblToDate.Visible = True
                Me.dtpToDate.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class