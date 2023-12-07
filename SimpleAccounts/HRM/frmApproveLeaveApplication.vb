'Ali Faisal : TFS1525 : Add form to save, update, delete leave application on 03-Oct-2017
''Muhammad Amin done TASK TFS3571 to show employees and history data according to rights based cost centers in case configuration is on. Dated 20-06-2018
Imports SBDal
Imports SBModel
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Public Class frmApproveLeaveApplication
    Implements IGeneral
    Dim objDAL As LeaveApplicationDAL
    Dim objModel As LeaveApplicationBE
    Dim CostCenterRights As Boolean = False
    Dim ApplicationId As Integer = 0
    Dim dtEmail As DataTable
    Dim EmailDAL As New EmailTemplateDAL
    Dim AfterFieldsElement As String = String.Empty
    Dim AllFields As List(Of String)
    Dim html As StringBuilder
    Dim EmailBody As String = String.Empty
    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As String = String.Empty
    Dim IsFormOpen As Boolean = False

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
        ScheduledLeave
        CompensatoryLeave
        Differnce
        JoiningDate
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Get Document number for application
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Function GetDocumentNo() As String
    '    Try
    '        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
    '            Return GetSerialNo("APL-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblLeaveApplication", "ApplicationNo")
    '        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
    '            Return GetNextDocNo("APL-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblLeaveApplication", "ApplicationNo")
    '        Else
    '            Return GetNextDocNo("APL-", 6, "tblLeaveApplication", "ApplicationNo")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
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
            Me.grdSaved.RootTable.Columns(grd.JoiningDate).FormatString = str_DisplayDateFormat
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
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE ReportingTo in (select EmployeeId from tblUser where User_ID = " & LoginUserId & ")"
                Else
                    ''TASK TFS3571
                    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE ReportingTo in (select EmployeeId from tblUser where User_ID = " & LoginUserId & " AND CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")"
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
                'If CostCenterRights = False Then
                'str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmp.Value & ")"
                str = "select Employee_ID, Employee_Name, OfficialEmail from tblDefEmployee where Employee_ID in (select ReportingTo from tblDefEmployee where Employee_ID = " & Me.cmbEmp.Value & ")"
                'Else
                '    ''TASK TFS3571
                '    str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1 AND Employee_ID NOT IN (" & Me.cmbEmp.Value & ") AND CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")"
                '    ''END TASK TFS3571
                'End If
                FillDropDown(Me.cmbForwardTo, str, False)
            ElseIf Condition = "AttendanceStatus" Then
                str = "SELECT Att_Status_ID, Att_Status_Name FROM tblDefAttendenceStatus WHERE Active = 1 AND Att_Status_Name NOT IN ('Present','Absent','Outdoor Duty','Break','Off Day','Short Absent')"
                FillDropDown(Me.cmbAttendanceStatus, str, True)
            ElseIf Condition = "Period" Then
                str = "SELECT PeriodId, PeriodTitle FROM tblDefLeavePeriod WHERE Active = 1"
                FillDropDown(Me.cmbPeriod, str, True)
            ElseIf Condition = "LeaveApplication" Then
                str = "SELECT LeaveApplicationId, ApplicationNo FROM tblLeaveApplication WHERE EmployeeId = " & Val(cmbEmp.Value) & " and Status is null ORDER BY LeaveApplicationId DESC"
                FillUltraDropDown(Me.cmbApplicationNo, str, True)
            ElseIf Condition = "AlternateContact" Then
                str = "select Employee_ID, Employee_Name, OfficialEmail from tblDefEmployee where Active = 1"
                FillDropDown(Me.cmbAlternateContact, str, True)
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
            'If Me.btnSave.Text = "&Save" Then
            '    objModel.ApplicationId = 0
            '    'objModel.ApplicationNo = GetDocumentNo()
            'Else
            objModel.ApplicationId = cmbApplicationNo.Value
            objModel.ApplicationNo = Me.txtDocNo.Text
            'End If
            objModel.ApplicationDate = Me.dtpDocDate.Value
            objModel.EmployeeId = Me.cmbEmp.Value
            objModel.Reason = Me.txtReason.Text
            objModel.LeaveTypeId = Me.cmbLeaveType.SelectedValue
            objModel.ForwardToId = Me.cmbForwardTo.SelectedValue
            objModel.AlternateContactNo = Me.cmbAlternateContact.Text
            objModel.AttendanceStatusId = Me.cmbAttendanceStatus.SelectedValue
            objModel.FromDate = Me.dtpFromDate.Value
            objModel.JoiningDate = Me.dtpJoiningDate.Value
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
            objModel.NoOfDays = txtNoofDays.Text
            objModel.Description = Me.txtDescription.Text
            objModel.Status = IIf(rdoApprove.Checked = True, 1, 0)
            objModel.ApprovedBy = LoginUserName
            'objModel.ApprovedBy = Username
            If pnlCompensatoryLeave.Visible = True Then
                objModel.ScheduledLeave = Me.dtpScheduledLeave.Value.ToString("yyyy-MM-dd")
                objModel.CompensatoryLeave = Me.dtpCompensatoryLeave.Value.ToString("yyyy-MM-dd")
                objModel.AlternateContactNo = Me.txtDifference.Text
            Else
                objModel.ScheduledLeave = Date.Now.ToString("yyyy-MM-dd")
                objModel.CompensatoryLeave = Date.Now.ToString("yyyy-MM-dd")
                objModel.AlternateContactNo = 0
            End If
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
            'Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells(grd.ApplicationDate).Value
            Me.cmbEmp.Value = Val(Me.grdSaved.GetRow.Cells(grd.EmployeeId).Value)
            Me.txtReason.Text = Me.grdSaved.GetRow.Cells(grd.Reason).Value.ToString
            Me.cmbLeaveType.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.LeaveTypeId).Value)
            Me.cmbForwardTo.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.ForwardToId).Value)
            Me.cmbAlternateContact.Text = Me.grdSaved.GetRow.Cells(grd.ContactNo).Value.ToString
            Me.cmbAttendanceStatus.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.AttendanceId).Value)
            Me.dtpFromDate.Value = Me.grdSaved.GetRow.Cells(grd.FromDate).Value
            If IsDBNull(Me.grdSaved.GetRow.Cells(grd.ToDate).Value) Then
                Me.dtpToDate.Value = Date.Now
            Else
                Me.dtpToDate.Value = Me.grdSaved.GetRow.Cells(grd.ToDate).Value
            End If

            Me.cmbPeriod.SelectedValue = Val(Me.grdSaved.GetRow.Cells(grd.PeriodId).Value)
            Me.txtDescription.Text = Me.grdSaved.GetRow.Cells(grd.Description).Value.ToString
            Me.dtpScheduledLeave.Value = Me.grdSaved.GetRow.Cells(grd.ScheduledLeave).Value
            Me.dtpCompensatoryLeave.Value = Me.grdSaved.GetRow.Cells(grd.CompensatoryLeave).Value
            Me.txtDifference.Text = Me.grdSaved.GetRow.Cells(grd.Differnce).Value
            Me.dtpJoiningDate.Value = Me.grdSaved.GetRow.Cells(grd.JoiningDate).Value
            'ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
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
                str = "SELECT App.LeaveApplicationId AS ApplicationId, App.ApplicationNo, App.ApplicationDate, App.EmployeeId, Emp.Employee_Name AS EmployeeName, App.ForwardToId, Frwd.Employee_Name AS ForwardTo, App.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, App.ApplicationReason AS Reason, App.AttendanceStatusId AS AttendanceId, Status.Att_Status_Name AS Attendance, App.FromDate, App.ToDate, App.PeriodId, App.AlternateContactNo AS ContactNo, App.ApplicationDetails AS Description, App.Status, App.ApprovedBy, App.ScheduledLeave, App.CompensatoryLeave, App.Difference, App.JoiningDate  " _
                                  & "FROM tblLeaveApplication AS App INNER JOIN tblDefEmployee AS Emp ON App.EmployeeId = Emp.Employee_ID INNER JOIN tblDefEmployee AS Frwd ON App.ForwardToId = Frwd.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON App.LeaveTypeId = Type.Id INNER JOIN tblDefAttendenceStatus AS Status ON App.AttendanceStatusId = Status.Att_Status_ID WHERE Emp.ReportingTo in (select EmployeeId from tblUser where User_ID = " & LoginUserId & ") ORDER BY App.LeaveApplicationId DESC"
            Else
                'e.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                ''TASK TFS3571
                str = "SELECT App.LeaveApplicationId AS ApplicationId, App.ApplicationNo, App.ApplicationDate, App.EmployeeId, Emp.Employee_Name AS EmployeeName, App.ForwardToId, Frwd.Employee_Name AS ForwardTo, App.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, App.ApplicationReason AS Reason, App.AttendanceStatusId AS AttendanceId, Status.Att_Status_Name AS Attendance, App.FromDate, App.ToDate, App.PeriodId, App.AlternateContactNo AS ContactNo, App.ApplicationDetails AS Description, App.Status, App.ApprovedBy, App.ScheduledLeave, App.CompensatoryLeave, App.Difference, App.JoiningDate  " _
                                & "FROM tblLeaveApplication AS App INNER JOIN tblDefEmployee AS Emp ON App.EmployeeId = Emp.Employee_ID INNER JOIN tblDefEmployee AS Frwd ON App.ForwardToId = Frwd.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON App.LeaveTypeId = Type.Id INNER JOIN tblDefAttendenceStatus AS Status ON App.AttendanceStatusId = Status.Att_Status_ID WHERE Emp.ReportingTo in (select EmployeeId from tblUser where User_ID = " & LoginUserId & ") AND Emp.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY App.LeaveApplicationId DESC"
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
            'If Me.txtDocNo.Text = "" Then
            '    ShowErrorMessage("Please enter the valid Doc No")
            '    Me.txtDocNo.Focus()
            '    Return False
            'If Me.cmbEmp.Value = 0 Then
            '    ShowErrorMessage("Please select any employee")
            '    Me.cmbEmp.Focus()
            '    Return False
            'ElseIf Me.txtReason.Text = "" Then
            '    ShowErrorMessage("Please enter the leave reason")
            '    Me.txtReason.Focus()
            '    Return False
            'ElseIf Me.cmbAttendanceStatus.SelectedValue = 0 Then
            '    ShowErrorMessage("Please select any attendance status")
            '    Me.cmbAttendanceStatus.Focus()
            '    Return False
            'End If
            If pnlCompensatoryLeave.Visible = True AndAlso Me.txtDifference.Text > 7 Then
                If msg_Confirm("This compensatory leave applied after 7 days. Do you want to Proceed?") = False Then Return False
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
            Me.txtDocNo.Text = ""
            'Me.dtpDocDate.Value = Date.Now
            ''TASK TFS3571
            If Not getConfigValueByType("RightBasedCostCenters") = "Error" Then
                CostCenterRights = CBool(getConfigValueByType("RightBasedCostCenters"))
            End If
            ''END TASK TFS3571
            FillCombos("Employee")
            FillCombos("LeaveType")
            FillCombos("ForwardTo")
            FillCombos("AlternateContact")
            FillCombos("AttendanceStatus")
            FillCombos("Period")
            FillCombos("LeaveApplication")
            ''Me.cmbEmp.Value = 0
            Me.txtReason.Text = ""
            Me.cmbLeaveType.SelectedValue = 0
            Me.cmbForwardTo.SelectedValue = 0
            Me.cmbAlternateContact.SelectedValue = 0
            Me.txtAlternateContact.Text = ""
            Me.cmbAttendanceStatus.SelectedValue = 0
            Me.dtpFromDate.Value = Date.Now
            Me.dtpToDate.Value = Date.Now
            Me.dtpJoiningDate.Value = Date.Now
            Me.dtpScheduledLeave.Value = Date.Now
            Me.dtpCompensatoryLeave.Value = Date.Now
            Me.txtDifference.Text = 0
            Me.txtDescription.Text = ""
            Me.cmbPeriod.Visible = False
            Me.lblPeriod.Visible = False
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False





            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.Tabs(0).Selected = True
            IsFormOpen = True
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
            'ApplySecurity(SBUtility.Utility.EnumDataMode.New)
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
            'If Me.btnSave.Text = "&Save" Then
            '    If Save() = True Then
            '        msg_Information(str_informSave)
            '        btnNew_Click(Nothing, Nothing)
            '    End If
            'Else
            '    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
            If Update1() = True Then
                msg_Information(IIf(rdoApprove.Checked = True, "Leave are Approved.", "Leave are Rejected."))
                SendAutoEmail()
                btnNew_Click(Nothing, Nothing)
            End If
            'End If
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

            id = Me.cmbAlternateContact.SelectedValue
            FillCombos("AlternateContact")
            Me.cmbAlternateContact.SelectedValue = id
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
    Private Sub cmbApplicationNo_ValueChanged(sender As Object, e As EventArgs) Handles cmbApplicationNo.ValueChanged
        Try
            If cmbApplicationNo.Value > 0 Then
                Dim str1 As String = ""
                str1 = "SELECT App.LeaveApplicationId AS ApplicationId, App.ApplicationNo, App.ApplicationDate, App.EmployeeId, Emp.Employee_Name AS EmployeeName, App.ForwardToId, Frwd.Employee_Name AS ForwardTo, App.LeaveTypeId, Type.LeaveTypeTitle AS LeaveType, App.ApplicationReason AS Reason, App.FromDate, App.ToDate, App.PeriodId, App.AlternateContactNo AS ContactNo, App.ApplicationDetails AS Description, App.NoOfDays, App.ScheduledLeave, App.CompensatoryLeave, App.Difference, App.JoiningDate  FROM tblLeaveApplication AS App INNER JOIN tblDefEmployee AS Emp ON App.EmployeeId = Emp.Employee_ID INNER JOIN tblDefEmployee AS Frwd ON App.ForwardToId = Frwd.Employee_ID LEFT OUTER JOIN tblDefLeaveTypes AS Type ON App.LeaveTypeId = Type.Id where app.LeaveApplicationId = " & cmbApplicationNo.Value & ""
                Dim dt1 As DataTable
                dt1 = GetDataTable(str1)
                Me.txtDocNo.Text = dt1.Rows(0).Item("ApplicationNo")
                Me.dtpDocDate.Value = dt1.Rows(0).Item("ApplicationDate")
                Me.txtReason.Text = dt1.Rows(0).Item("Reason")
                Me.cmbLeaveType.SelectedValue = dt1.Rows(0).Item("LeaveTypeId")
                Me.cmbAlternateContact.Text = dt1.Rows(0).Item("ContactNo")
                Me.dtpFromDate.Value = dt1.Rows(0).Item("FromDate")
                Me.dtpToDate.Value = dt1.Rows(0).Item("ToDate")
                Me.txtNoofDays.Text = dt1.Rows(0).Item("NoOfDays")
                Me.txtDescription.Text = dt1.Rows(0).Item("Description")
                ''Me.cmbAttendanceStatus.SelectedValue = dt1.Rows(0).Item("AttendanceId")
                Me.cmbForwardTo.SelectedValue = dt1.Rows(0).Item("ForwardToId")
                If dt1.Rows(0).Item("LeaveTypeId") = 3 Then
                    Me.dtpScheduledLeave.Value = dt1.Rows(0).Item("ScheduledLeave")
                    Me.dtpCompensatoryLeave.Value = dt1.Rows(0).Item("CompensatoryLeave")
                End If
                Me.txtDifference.Text = dt1.Rows(0).Item("Difference")
                Me.dtpJoiningDate.Value = dt1.Rows(0).Item("JoiningDate")
                'Me.txtCountry.Text = dt1.Rows(0).Item("Country")
                'Me.txtCity.Text = dt1.Rows(0).Item("City")
                'Dim str As Stringv
                'rafay 1modified query to add and show check box of batteries Included'
                'str = "select customerid, StartDate, EndDate, isnull(EndCustomer, '') as EndCustomer, isnull(Employee, '') as Employee,ChkBoxBatteriesIncluded FROM ContractMasterTable WHERE ContractId = " & cmbContractNo.SelectedValue & ""
                'Dim dt As DataTable
                'dt = GetDataTable(str)
                'txtCompanyName.Text = dt.Rows(0).Item("customerid")
                'txtCustomerName.Text = dt.Rows(0).Item("EndCustomer")
                'txtEmployee.Text = dt.Rows(0).Item("Employee")
                'dtpContractStartDate.Value = dt.Rows(0).Item("StartDate")
                'dtpContractEndDate.Value = dt.Rows(0).Item("EndDate")
                ''rafay 12-4-22
                'ChkBatteriesIncluded.Checked = IsDBNull(dt.Rows(0).Item("ChkBoxBatteriesIncluded"))
                'Me.ChkBatteriesIncluded.Checked = cmbSerialNo.ActiveRow.Cells("BatteriesIncluded").Value.ToString
            End If
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

    Private Sub cmbEmp_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmp.ValueChanged
        Try
            FillCombos("ForwardTo")
            FillCombos("LeaveApplication")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbLeaveType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbLeaveType.SelectedValueChanged
        Try
            If cmbLeaveType.SelectedValue = 1 Or cmbLeaveType.SelectedValue = 2 Then
                Dim AllowedLeaves As String
                AllowedLeaves = "select AllowedPerYear from tblDefLeaveTypes where Id = " & cmbLeaveType.SelectedValue & ""
                Dim dtAllowedLeaves As DataTable
                dtAllowedLeaves = GetDataTable(AllowedLeaves)
                Dim MonthlyStr As String = "select Month('" & dtpDocDate.Value.ToString("yyyy-M-d 00:00:00") & "') - 1 as DateDiffrence"
                Dim monthlydt As DataTable = GetDataTable(MonthlyStr)
                Dim NumberOfMonths As Double
                If monthlydt.Rows.Count > 0 Then
                    NumberOfMonths = monthlydt.Rows(0).Item(0)
                    If NumberOfMonths = 11 Then
                        NumberOfMonths = 12
                    End If
                End If
                Me.txtAllowedLeaves.Text = dtAllowedLeaves.Rows(0).Item("AllowedPerYear") * NumberOfMonths
                Dim AvailedLeaves As String
                AvailedLeaves = "select ISNUll(Sum(NoOfDays),0) as AvailedLeaves from tblLeaveApplication where EmployeeId = " & cmbEmp.Value & " and leavetypeid = " & cmbLeaveType.SelectedValue & " and Status = 1"
                Dim dtAvailedLeaves As DataTable
                dtAvailedLeaves = GetDataTable(AvailedLeaves)
                Me.txtAvailedLeaves.Text = dtAvailedLeaves.Rows(0).Item("AvailedLeaves")
                Me.txtRemainingLeaves.Text = Val(txtAllowedLeaves.Text) - Val(txtAvailedLeaves.Text)
                pnlCompensatoryLeave.Visible = False
                txtNoofDays.Enabled = True
            ElseIf cmbLeaveType.SelectedValue < 1 Then
                txtAllowedLeaves.Text = 0
                txtAvailedLeaves.Text = 0
                txtRemainingLeaves.Text = 0
                pnlCompensatoryLeave.Visible = False
            ElseIf cmbLeaveType.SelectedValue = 3 Then
                txtAllowedLeaves.Text = 0
                txtAvailedLeaves.Text = 0
                txtRemainingLeaves.Text = 0
                pnlCompensatoryLeave.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpToDate_CloseUp(sender As Object, e As EventArgs) Handles dtpToDate.ValueChanged
        Try
            If IsFormOpen = False Then Exit Sub
            Dim Query4 As String = String.Empty
            Dim dt4 As New DataTable
            Query4 = "NoOfLeaveDays'" & Me.dtpFromDate.Value.ToString("yyyy-M-d") & "','" & Me.dtpToDate.Value.ToString("yyyy-M-d") & "' "
            dt4 = GetDataTable(Query4)
            dt4.AcceptChanges()
            For Each D As DataRow In dt4.Rows
                txtNoofDays.Text = D.Item(0)
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub dtpCompensatoryLeave_CloseUp(sender As Object, e As EventArgs) Handles dtpCompensatoryLeave.CloseUp
    '    Try
    '        If IsFormOpen = False Then Exit Sub
    '        Dim Query4 As String = String.Empty
    '        Dim dt4 As New DataTable
    '        Query4 = "NoOfLeaveDays'" & Me.dtpScheduledLeave.Value.ToString("yyyy-M-d") & "','" & Me.dtpCompensatoryLeave.Value.ToString("yyyy-M-d") & "' "
    '        dt4 = GetDataTable(Query4)
    '        dt4.AcceptChanges()
    '        For Each D As DataRow In dt4.Rows
    '            txtDifference.Text = D.Item(0)
    '        Next
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Receipt")
            If EmailTemplate.Length > 0 Then
                'GetAutoEmailData()
                Dim query As String = String.Empty
                Dim dt As New DataTable
                query = "select OfficialEmail from tblDefEmployee where Employee_ID = " & cmbEmp.Value & ""
                dt = GetDataTable(query)
                dt.AcceptChanges()
                If dt.Rows.Count > 0 Then
                    'If dt.Rows(0).Item(0) > 0 Then
                    'Else
                    '    If Con.Database.Contains("Remms") Then
                    '        UsersEmail = "everyone@remmsit.com"
                    '    Else
                    '        UsersEmail = "everyone@agriusit.com"
                    '    End If
                    '    FormatStringBuilder(dtEmail)
                    '    CreateOutLookMail(UsersEmail)
                    '    'SaveEmailLog(VoucherNo, UsersEmail, "frmCustomerCollection", Activity)
                    'End If
                    UsersEmail = dt.Rows(0).Item("OfficialEmail")
                    FormatStringBuilder(dtEmail)
                    CreateOutLookMail(UsersEmail)
                    'SaveEmailLog(VoucherNo, UsersEmail, "frmCustomerCollection", Activity)
                Else
                    ShowErrorMessage("No email template is found for Receipt.")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                'For Each word As String In WOSpace.Split(",")
                '    Dim TrimSpace As String = word.Trim()
                '    If Me.grd.RootTable.Columns.Contains(TrimSpace) Then
                '        dtEmail.Columns.Add(TrimSpace)
                '        AllFields.Add(TrimSpace)
                '    End If
                'Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")

            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            'Dim str As String
            'str = "m.ahmad@agriusit.com"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt.Rows.Count > 0 Then
            'senderemail = "v-erp@agriusit.com"
            'OutAccount = oApp.Session.Accounts(senderemail)
            'mailItem.SendUsingAccount = OutAccount
            'End If
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            'OutAccount = oApp.Session.Accounts(senderemail)
            'mailItem.SendUsingAccount = OutAccount

            ''If Birthdate() = True Then
            mailItem.Subject = "Leave Request against " & txtDocNo.Text & " is " & IIf(rdoApprove.Checked = True, "Approved", "Rejected") & ""
            mailItem.To = Email
            mailItem.CC = "hr@agriusit.com"
            'Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            If cmbLeaveType.SelectedValue = 3 Then
                myStr = "Hi HR," & "<br>" & cmbLeaveType.Text & " of " & cmbEmp.Text & " is " & IIf(rdoApprove.Checked = True, "Approved", "Rejected") & "" & "<br>" & "<br>" & "<b>Scheduled Date:  </b>" & dtpScheduledLeave.Value.ToString("yyyy-MM-dd") & "<br>" & "<b>Compensatory Date:  </b>" & dtpCompensatoryLeave.Value.ToString("yyyy-MM-dd") & "<br>" & "<b>Alternate Contact:  </b>" & cmbAlternateContact.Text & "<br>" & "<br>" & "<b>Best Regards:</b>" & "<br>" & cmbForwardTo.Text
            Else
                myStr = "Hi HR," & "<br>" & cmbLeaveType.Text & " of " & cmbEmp.Text & " is " & IIf(rdoApprove.Checked = True, "Approved", "Rejected") & "" & "<br>" & "<br>" & "<b>From Date:  </b>" & dtpFromDate.Value.ToString("yyyy-MM-dd") & "<br>" & "<b>To Date:  </b>" & dtpToDate.Value.ToString("yyyy-MM-dd") & "<br>" & "<b>Joining Date:  </b>" & dtpJoiningDate.Value.ToString("yyyy-MM-dd") & "<br>" & "<b>Alternate Contact:  </b>" & cmbAlternateContact.Text & "<br>" & "<b>Number of Days:  </b>" & txtNoofDays.Text & "<br>" & "<br>" & "<b>Best Regards:</b>" & "<br>" & cmbForwardTo.Text
            End If
            mailItem.HTMLBody = myStr
            'mailItem.HTMLBody = ""
            'EmailBody = html.ToString
            mailItem.Send()
            'Dim ConStrBuilder As New OleDbConnectionStringBuilder(Con.ConnectionString)
            'Dim dbName As String = ConStrBuilder.Item("Initial Catalog")
            'ConStrBuilder.Item("Initial Catalog") = "master"
            'Dim Con1 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            'If Con1.State = ConnectionState.Closed Then Con1.Open()
            ''buLocation = getConfigValueByType("DatabaseBackup").ToString()
            'Application.DoEvents()

            'Dim ServerName As String = ConStrBuilder.DataSource
            'Dim Con2 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            'If Con2.State = ConnectionState.Closed Then Con2.Open()
            'Application.DoEvents()

            'Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
            'If objCon.State = ConnectionState.Closed Then objCon.Open()
            'Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
            'Dim cmd As New OleDb.OleDbCommand
            'cmd.Connection = objCon
            'cmd.CommandType = CommandType.Text
            'cmd.CommandTimeout = 30
            'cmd.Transaction = trans
            ''Dim SqlCommand1 As OleDbCommand = New OleDbCommand()
            ''SqlCommand1.Connection = Con2
            'cmd.CommandText = "Insert into BirthdayEmailProcess (BirthdayDate, Status, UserID) Values( Convert(datetime, '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ") "
            'cmd.ExecuteNonQuery()
            'trans.Commit()
            'Application.DoEvents()
            ''End If
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class