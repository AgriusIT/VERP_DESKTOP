''25-Apr-2014 TASK:2593 Mughees Ehnancement In Employee Attendance.
''21-May-2015 Task#20150512 Saving all employees attendance 
'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
''TASK TFS3568 Muhammad Amin applied cost center rights and showed history data according cost center rights. done on 19-06-2018
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmAttendanceEmployees
    Implements IGeneral
    Dim AttendanceEmp As AttendanceEmp
    Dim EmpAttendanceId As Integer = 0
    Dim IsFormLoaded As Boolean = False
    Dim EmpDepartment As String = String.Empty
    Dim EmpDesignation As String = String.Empty
    Dim IsEditMode As Boolean = False
    Dim ValidationDt As New DataTable
    Dim PrintLog As PrintLogBE
    Dim InvalidEmployee As Boolean = False
    Dim AutoAttendance As Boolean = False
    Dim blnBarcodeAttendance As Boolean = False
    Dim CostCenterRights As Boolean = False
    Private Sub frmAttendanceEmployees_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not getConfigValueByType("RightBasedCostCenters") = "Error" Then
                CostCenterRights = CBool(getConfigValueByType("RightBasedCostCenters"))
            End If
            FillCombos("AttStatus")
            FillCombos()
            FillCombos("SearchByDesignation")
            FillCombos("SearchByEmployee")
            FillCombos("SearchByDepartment")
            FillCombos("CostCenter")
            FillCombos("CostCenterSearch")
            ReSetControls()
            Me.Timer1.Start()
            IsFormLoaded = True
            'Me.grbBarcodeScan.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Return New AttendanceEmpDAL().DeleteAttendance(AttendanceEmp)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try


            ''TASK TFS3568 done on 19-06-18
            'Dim CostCenterRights As Boolean = False
           
            ''END TASK TFS3568
            'Dim str As String = "Select Employee_ID,  Employee_Name,Employee_Code, EmpPicture, Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id ORDER BY Employee_Name Asc"

            'Dim str As String = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id ORDER BY Employee_Name Asc"
            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            'Altered Against Task#2015060025 to only load Active Employees Ali Ansari

            Dim str As String = String.Empty
            If Condition = String.Empty Then
                If CostCenterRights = False Then
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                Else
                    'AND CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY Employee_Name Asc"

                End If
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbEmployees, str)
                Me.cmbEmployees.Rows(0).Activate()
                If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
                FillDropDown(Me.cmbAttendanceStatus, "Select Att_Status_ID, Att_Status_Name, Att_Status_Code, Active From tblDefAttendenceStatus WHERE Active=1", False)
            ElseIf Condition = "SearchEmployeeByDesignation" Then
                If CostCenterRights = False Then
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & "  ORDER BY Employee_Name Asc"
                Else
                    'AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & "  AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY Employee_Name Asc"

                End If
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
            ElseIf Condition = "SearchEmployeeByDepartment" Then
                If CostCenterRights = False Then
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & "  ORDER BY Employee_Name Asc"
                Else
                    'AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & " AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY Employee_Name Asc"
                End If
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
            ElseIf Condition = "SearchEmployeeByDepartmentAndDesignation" Then
                If CostCenterRights = False Then
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & " And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & " ORDER BY Employee_Name Asc"
                Else
                    'AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & " And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & "  AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") ORDER BY Employee_Name Asc"

                End If
                    'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                    FillUltraDropDown(Me.cmbSearchByEmployee, str)
                    Me.cmbSearchByEmployee.Rows(0).Activate()
                    If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                        Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                        Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                    End If
                ElseIf Condition = "SearchByDesignation" Then
                    str = "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable"
                    FillUltraDropDown(Me.cmbSearchByDesignation, str)
                    Me.cmbSearchByDesignation.Rows(0).Activate()
                    If Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationId").Hidden = True
                    End If
                ElseIf Condition = "SearchByDepartment" Then
                    str = "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable"
                    FillUltraDropDown(Me.cmbSearchByDepartment, str)
                    Me.cmbSearchByDepartment.Rows(0).Activate()
                    If Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptId").Hidden = True
                    End If
            ElseIf Condition = "SearchEmployee" Then
                If CostCenterRights = False Then
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 "
                Else
                    'AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")
                    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ")"

                End If

                ''Below three lines are added against TASK TFS3568
                'If Me.cmbCostCenterSearch.SelectedValue > 0 Then
                '    str += " AND tblDefEmployee.CostCentre = " & Me.cmbCostCenterSearch.SelectedValue & ""
                'End If
                ''end TASK TFS3568
                str += "ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            'Task No 2593 Append Some New Lines Of Code for Newly Added Fields

           
            AttendanceEmp = New AttendanceEmp
            If Not Me.cmbShift.SelectedIndex = -1 Then
                Dim objRow As DataRowView
                objRow = CType(Me.cmbShift.SelectedItem, DataRowView)
                If objRow.Row.Item("FlexInTime").ToString <> "" Then
                    AttendanceEmp.FlexiblityInTime = Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & " " & CDate(objRow.Row.Item("FlexInTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.FlexiblityInTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
                If objRow.Row.Item("FlexOutTime").ToString <> "" Then
                    AttendanceEmp.FlexiblityOutTime = Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & " " & CDate(objRow.Row.Item("FlexOutTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.FlexiblityOutTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
                If objRow.Row.Item("ShiftStartTime").ToString <> "" Then
                    AttendanceEmp.SchInTime = Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & " " & CDate(objRow.Row.Item("ShiftStartTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.SchInTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
                If objRow.Row.Item("ShiftEndTime").ToString <> "" Then
                    AttendanceEmp.SchOutTime = Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & " " & CDate(objRow.Row.Item("ShiftEndTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.SchOutTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If

            Else
                AttendanceEmp.FlexiblityInTime = String.Empty
                AttendanceEmp.FlexiblityOutTime = String.Empty
                AttendanceEmp.SchInTime = String.Empty
                AttendanceEmp.SchOutTime = String.Empty
            End If
            AttendanceEmp.AttendanceId = EmpAttendanceId
            AttendanceEmp.EmpID = Me.cmbEmployees.Value
            AttendanceEmp.AttendanceDate = Me.dtpAttendanceDate.Value.Date
            'If Not (Me.cmbAttendanceStatus.Text = "Absent" Or Me.cmbAttendanceStatus.Text = "Leave" Or Me.cmbAttendanceStatus.Text = "Casual Leave" Or Me.cmbAttendanceStatus.Text = "Anual Leave" Or Me.cmbAttendanceStatus.Text = "Sick Leave") = True Then
            If Me.dtpAttendanceTime.Enabled = True Then
                AttendanceEmp.AttendanceType = IIf(Me.RbtIn.Checked = True, "In", "Out")
                AttendanceEmp.AttendanceTime = CDate(Me.dtpAttendanceDate.Value.Date & " " & Me.dtpAttendanceTime.Value.ToLongTimeString)  'Me.dtpAttendanceTime.Value
            Else
                AttendanceEmp.AttendanceType = Nothing
                AttendanceEmp.AttendanceTime = Nothing
            End If
            AttendanceEmp.AttendanceStatus = Me.cmbAttendanceStatus.Text
            AttendanceEmp.ShiftId = IIf(Me.cmbShift.SelectedIndex = -1, 0, cmbShift.SelectedValue)
            AttendanceEmp.Auto = AutoAttendance
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grd.DataSource = New SBDal.AttendanceEmpDAL().GetAllRecords(IIf(Condition.Length > 1, "All", String.Empty), CostCenterRights, LoginUserId)
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbEmployees.Value <= 0 Then
                ShowErrorMessage("Please Select Employee Name")
                Me.cmbEmployees.Focus()
                Return False
            Else
                FillModel()
                Return True
            End If

            ''TASK TFS3568 done on 19-06-18
           
            ''END TASK TFS3568
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.Timer1.Start()
            IsEditMode = False
            If Not getConfigValueByType("RightBasedCostCenters") = "Error" Then
                CostCenterRights = CBool(getConfigValueByType("RightBasedCostCenters"))
            End If
            Me.BtnSave.Text = "&Save"
            If Not Me.cmbEmployees.ActiveRow Is Nothing Then Me.cmbEmployees.Rows(0).Activate()
            Me.txtEmpDesignation.Text = String.Empty
            Me.txtEmpDepartment.Text = String.Empty
            Me.dtpAttendanceDate.Value = Date.Now().ToString("dd/MMM/yyyy")
            Me.dtpAttendanceTime.Format = DateTimePickerFormat.Time
            Me.cmbAttendanceStatus.SelectedIndex = 0
            Me.txtBarCodeEmpId.Text = String.Empty
            Me.RbtIn.Checked = True
            Me.RbtOut.Checked = False
            If Me.Panel1.Visible = True Then
                Me.txtBarCodeEmpId.Focus()
                Me.Panel1.Visible = True
            Else
                Me.cmbEmployees.Focus()
                Me.Panel1.Visible = False
            End If
            Me.Timer2.Enabled = False
            Me.lblEmpName.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Name").ToString
            Me.lblEmpCode.Text = String.Empty  'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Code").ToString
            Me.lblDesig.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Designation").ToString
            Me.lblDept.Text = String.Empty '
            Me.lblWelcome.Visible = False
            Me.lblSeeU.Visible = False
            InvalidEmployee = False
            Me.ErrorProvider1.Clear()
            Me.lblError.Text = ""
            lblAttendanceType.Text = ""
            AutoAttendance = False

            GetAllRecords()
            GetShift(-1)
            GetSecurityRights() 'Call Security
            Me.gbSearchBy.Visible = False
            Me.dtpFrom.Value = Now
            Me.dtpTo.Value = Now
            If Not Me.cmbSearchByDepartment.ActiveRow Is Nothing Then Me.cmbSearchByDepartment.Rows(0).Activate()
            If Not Me.cmbSearchByDesignation.ActiveRow Is Nothing Then Me.cmbSearchByDesignation.Rows(0).Activate()
            If Not Me.cmbSearchByEmployee.ActiveRow Is Nothing Then Me.cmbSearchByEmployee.Rows(0).Activate()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            'Commented Against Task 3283 change the in function FuncAttendanceData to make single attendence of present and absent in same day

            'If Me.cmbAttendanceStatus.Text = "Absent" Then

            '    'AttendanceEmpDAL.DeletePresentMarkEmployee(cmbEmployees.Value, Me.dtpAttendanceDate.Value.Date)

            'End If

            'If Me.cmbAttendanceStatus.Text = "Present" Then

            '    'AttendanceEmpDAL.DeleteAbsentMarkEmployee(cmbEmployees.Value, Me.dtpAttendanceDate.Value.Date)

            'End If


            Return New AttendanceEmpDAL().AddAttendance(AttendanceEmp)


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveAll(Optional ByVal Condition As String = "") As Boolean
        Try

            'Commented Against Task 3283 change the in function FuncAttendanceData to make single attendence of present and absent in same day

            'If Me.cmbAttendanceStatus.Text = "Absent" Then

            '    AttendanceEmpDAL.DeletePresentMarkEmployee(cmbEmployees.Value, Me.dtpAttendanceDate.Value.Date)

            'End If

            'If Me.cmbAttendanceStatus.Text = "Present" Then

            '    AttendanceEmpDAL.DeleteAbsentMarkEmployee(cmbEmployees.Value, Me.dtpAttendanceDate.Value.Date)

            'End If


            Return New AttendanceEmpDAL().AddAttendanceAll(AttendanceEmp)

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
            Return New AttendanceEmpDAL().UpdateAttendance(AttendanceEmp)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpAttendanceDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If

            If Not IsValidate() = True Then Exit Sub
            If BtnSave.Text = "&Save" Or BtnSave.Text = "Save" Then
                If Now.Date < Me.dtpAttendanceDate.Value.Date Then
                    ShowErrorMessage("This date attendance not allowed.")
                    Me.dtpAttendanceDate.Focus()
                    Exit Sub
                End If
                'If GetValidationDt() = True Then
                '    ShowErrorMessage("Already Exits Attendance")
                '    Exit Sub
                'End If
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                SendSMS()
                'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'If Me.Panel1.Visible = False Then msg_Information(str_informSave) Comment against task:2593
                If Me.Panel1.Visible = True Then
                    Me.txtBarCodeEmpId.Text = String.Empty
                    GetAllRecords()
                    Timer2.Enabled = True
                    'ReSetControls()
                Else
                    ReSetControls()
                End If
            Else
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Update Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'If Me.Panel1.Visible = False Then msg_Information(str_informUpdate) Comment against task:2593
                SendSMS()
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            IsEditMode = True
            Me.BtnSave.Text = "&Update"
            EmpAttendanceId = Me.grd.GetRow.Cells("AttendanceId").Value
            Me.dtpAttendanceDate.Value = Me.grd.GetRow.Cells("AttendanceDate").Value
            Me.cmbEmployees.Value = Val(Me.grd.GetRow.Cells("EmpId").Value.ToString)

            If Me.cmbEmployees.ActiveRow Is Nothing Then
                ShowErrorMessage("Employee is inactive.")
                Exit Sub
            End If
            If Not Me.grd.GetRow.Cells("AttendanceType").Value.ToString = "" Then
                If IsDBNull(Me.grd.GetRow.Cells("AttendanceTime").Value) = False Then
                    Me.dtpAttendanceTime.Value = Me.grd.GetRow.Cells("AttendanceTime").Value
                End If
                If Me.grd.GetRow.Cells("AttendanceType").Value = "In" Then
                    Me.RbtIn.Checked = True
                ElseIf Me.grd.GetRow.Cells("AttendanceType").Value = "Out" Then
                    Me.RbtOut.Checked = True
                End If
                Me.RbtIn.Enabled = True
                Me.RbtOut.Enabled = True
                Me.dtpAttendanceTime.Enabled = True
            Else
                Me.dtpAttendanceTime.Enabled = False
                Me.dtpAttendanceTime.Value = Date.Now
                Me.RbtIn.Enabled = False
                Me.RbtOut.Enabled = False
                Me.RbtIn.Checked = True
                Me.RbtOut.Checked = False
            End If
            'If Me.grd.GetRow.Cells("AttendanceStatus").Value = "Present" Then
            '    Me.chkAttendanceStatus.Checked = True
            'ElseIf Me.grd.GetRow.Cells("AttendanceStatus").Value = "Absent" Then
            '    Me.chkAttendanceStatus.Checked = False
            'End If
            If Not IsDBNull(Me.grd.GetRow.Cells("AttendanceStatus")) Then
                Me.cmbAttendanceStatus.Text = Me.grd.GetRow.Cells("AttendanceStatus").Text
            Else
                Me.cmbAttendanceStatus.SelectedIndex = 0
            End If

            If IsDBNull(Me.grd.GetRow.Cells("Auto").Value) Then
                AutoAttendance = False
            Else
                AutoAttendance = Me.grd.GetRow.Cells("Auto").Value
            End If
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub cmbEmployees_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.cmbAttendanceStatus.SelectedIndex = -1 Then Exit Sub
    '        'Dim str As String = "SELECT  EmployeeDeptDefTable.EmployeeDeptName, EmployeeDesignationDefTable.EmployeeDesignationName FROM tblDefEmployee INNER JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId WHERE tblDefEmployee.Employee_Id=" & Me.cmbEmployees.SelectedValue & " "
    '        'Dim dt As DataTable = GetDataTable(str)
    '        'If dt.Rows.Count > 0 Then
    '        '    Me.EmpDesignation = dt.Rows(0).Item("EmployeeDesignationName").ToString
    '        '    Me.EmpDepartment = dt.Rows(0).Item("EmployeeDeptName").ToString
    '        'Else
    '        '    Me.EmpDesignation = String.Empty
    '        '    Me.EmpDepartment = String.Empty
    '        'End If
    '        Me.txtEmpDesignation.Text = cmbEmployees.ActiveRow.Cells("Designation").Value.ToString
    '        Me.txtEmpDepartment.Text = cmbEmployees.ActiveRow.Cells("Department").Value.ToString
    '        Me.dtpAttendanceTime.Format = DateTimePickerFormat.Time
    '        If Me.cmbEmployees.Value > 0 Then
    '            Timer1.Stop()
    '        Else
    '            Timer1.Start()
    '        End If
    '        GetShift(Me.cmbEmployees.Value)
    '        Me.PictureBox1.ImageLocation = cmbEmployees.ActiveRow.Cells("EmpPicture").Value.ToString
    '        Application.DoEvents()
    '        CtrlGrdBar1.Email = New SBModel.SendingEmail
    '        CtrlGrdBar1.Email.ToEmail = AdminEmail
    '        CtrlGrdBar1.Email.Subject = "Employee Attandance: " + "(" & Me.dtpAttendanceDate.Value.ToString("dd-MM-yyyy") & ")"
    '        CtrlGrdBar1.Email.DocumentNo = Me.dtpAttendanceDate.Value.ToString("dd-MM-yyyy")
    '        CtrlGrdBar1.Email.DocumentDate = Me.dtpAttendanceDate.Value.Date

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If IsDateLock(Me.dtpAttendanceDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'MsgBox("Record Deleted Successfully", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            grd_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Not IsEditMode = True Then dtpAttendanceTime.Value = Date.Now()
            Me.dtpAttendanceTime.Format = DateTimePickerFormat.Time
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetValidationDt() As Boolean
        Try
            If Now.Date = Me.dtpAttendanceDate.Value.Date Then
                ValidationDt = CType(Me.grd.DataSource, DataTable)
            Else
                ValidationDt = New SBDal.AttendanceEmpDAL().GetAllRecords("All")
            End If
            ValidationDt.TableName = "Validation"
            If GetFilterDataFromDataTable(ValidationDt, "[EmpID]=" & Me.cmbEmployees.Value & " AND [AttendanceDate]='" & Me.dtpAttendanceDate.Value.Date.ToString("M/d/yyyy") & " 12:00:00 AM' AND [AttendanceType]='" & IIf(Me.RbtIn.Checked = True, "In", "Out") & "' ").ToTable.Rows.Count > 0 Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetCrystalReportRights()
            Dim mydate As String = Me.dtpAttendanceDate.Value.Year & "," & Me.dtpAttendanceDate.Value.Month & "," & Me.dtpAttendanceDate.Value.Day & ",0,0,0"
            If Me.grd.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grd.GetRow.Cells("Voucher_No").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ' AddRptParam("@CurrentDate", Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@FromDate", dtpAttendanceDate.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", dtpAttendanceDate.Value.ToString("yyyy-MM-d 23:59:59"))
            ShowReport("rptEmployeeAttendanceNew")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not getConfigValueByType("RightBasedCostCenters") = "Error" Then
                CostCenterRights = CBool(getConfigValueByType("RightBasedCostCenters"))
            End If
            Dim id As Integer = 0
            id = Me.cmbEmployees.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbEmployees.Value = id
            id = Me.cmbSearchByEmployee.ActiveRow.Cells(0).Value
            FillCombos("SearchEmployee")
            Me.cmbSearchByEmployee.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.BtnSaveAll.Enabled = True
                Me.btnLoadAll.Enabled = True
                Me.btnOutTimeAttendance.Enabled = True
                Me.btnSMSTemplate.Enabled = True
                Me.btnImportAttendance.Enabled = True
                Me.btnBarcodeScan.Enabled = True
                Me.btnSearch.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.BtnSaveAll.Enabled = False
                Me.btnLoadAll.Enabled = False
                Me.btnOutTimeAttendance.Enabled = False
                Me.btnSMSTemplate.Enabled = False
                Me.btnImportAttendance.Enabled = False
                Me.btnBarcodeScan.Enabled = False
                Me.btnSearch.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Save All" Then
                        Me.BtnSaveAll.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Load All" Then
                        Me.btnLoadAll.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Search" Then
                        Me.btnSearch.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Out Time Attendance" Then
                        Me.btnOutTimeAttendance.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Setting SMS Template" Then
                        Me.btnSMSTemplate.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Import Attendance" Then
                        Me.btnImportAttendance.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Employee Card Barcode Scan" Then
                        Me.btnBarcodeScan.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True


                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetShift(ByVal EmpId As Integer)
        Try
            Dim str As String = String.Empty
            'str = "Select * From (SELECT DISTINCT dbo.ShiftTable.ShiftId, dbo.ShiftTable.ShiftName, Convert(datetime,FlexInTime) as FlexInTime, Convert(datetime,FlexOutTime) as FlexOutTime, IsNull(OvertimeRate,0) as OverTimeRate, Convert(DateTime, ShiftStartTime) as ShiftStartTime, Convert(DateTime,ShiftEndTime) as ShiftEndTime, tblDefEmployee.Employee_Id FROM dbo.tblDefEmployee INNER JOIN dbo.ShiftGroupTable ON dbo.tblDefEmployee.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId INNER JOIN dbo.ShiftScheduleTable ON dbo.ShiftGroupTable.ShiftGroupId = dbo.ShiftScheduleTable.ShiftGroupId INNER JOIN dbo.ShiftTable ON dbo.ShiftScheduleTable.ShiftId = dbo.ShiftTable.ShiftId) a WHERE a.Employee_Id=" & EmpId & " AND ('" & Me.dtpAttendanceTime.Value.ToLongTimeString & "' BETWEEN left(right(convert (varchar, a.ShiftStartTime, 114), 14),8) + ' ' + Right(Convert(varchar, a.ShiftStartTime, 109),2)   AND left(right(convert (varchar, a.ShiftEndTime, 114), 14),8) + ' ' + Right(Convert(varchar, a.ShiftEndTime, 109),2))"
            str = "SELECT DISTINCT dbo.ShiftTable.ShiftId, dbo.ShiftTable.ShiftName, Convert(datetime,FlexInTime) as FlexInTime, Convert(datetime,FlexOutTime) as FlexOutTime, IsNull(OvertimeRate,0) as OverTimeRate, Convert(DateTime, ShiftStartTime) as ShiftStartTime, Convert(DateTime,ShiftEndTime) as ShiftEndTime FROM ShiftTable WHERE ShiftId IN(Select ShiftId From ShiftScheduleTable WHERE ShiftGroupId=" & cmbEmployees.ActiveRow.Cells("ShiftGroupId").Value.ToString & ")"
            Me.cmbShift.DataSource = Nothing
            FillDropDown(Me.cmbShift, str, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            GetAllRecords("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAttendanceStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAttendanceStatus.SelectedIndexChanged
        Try

            'If Me.cmbAttendanceStatus.Text = "Absent" Or Me.cmbAttendanceStatus.Text = "Leave" Or Me.cmbAttendanceStatus.Text = "Casual Leave" Or Me.cmbAttendanceStatus.Text = "Anual Leave" Or Me.cmbAttendanceStatus.Text = "Sick Leave" Then
            If (Me.cmbAttendanceStatus.Text <> "Present" And Me.cmbAttendanceStatus.Text <> "Short Leave" And Me.cmbAttendanceStatus.Text <> "Half Leave" And Me.cmbAttendanceStatus.Text <> "OD") Then
                Me.dtpAttendanceTime.Enabled = False
                Me.RbtIn.Enabled = False
                Me.RbtOut.Enabled = False
            Else
                Me.dtpAttendanceTime.Enabled = True
                Me.RbtIn.Enabled = True
                Me.RbtOut.Enabled = True
            End If
            If Me.RbtIn.Checked = True Then
                Me.lblAttendanceType.Text = "In"
                Application.DoEvents()
            ElseIf Me.RbtOut.Checked = True Then
                Me.lblAttendanceType.Text = "Out"
                Application.DoEvents()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnImportAttendance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAttendance.Click
        Try
            ApplyStyleSheet(frmAttendanceImport)

            If frmAttendanceImport.ShowDialog = Windows.Forms.DialogResult.Yes Then


            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtBarCodeEmpId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarCodeEmpId.KeyDown
        Try
            Me.ErrorProvider1.Clear()
            If e.KeyCode = Keys.Enter Then


                Me.lblError.Text = ""

                'If Me.cmbEmployees.SelectedIndex = -1 Then Exit Sub
                If e.KeyCode = Keys.OemQuestion Then
                    txtBarCodeEmpId.Focus()
                    e.SuppressKeyPress = True
                End If
                If Me.txtBarCodeEmpId.Text.Length > 1 Then
                    Me.Timer2.Stop()
                    Me.Timer2.Enabled = False
                    GetId(Me.txtBarCodeEmpId.Text)
                    If InvalidEmployee = True Then
                        Me.RbtIn.Checked = True
                        Me.lblError.Text = "Invalid Code"
                        Me.ErrorProvider1.SetError(Me.lblError, "Invalid Code")
                        Me.txtBarCodeEmpId.Focus()
                    End If
                    If Me.cmbEmployees.Value <= 0 Then Exit Sub
                    Me.Timer2.Start()
                    blnBarcodeAttendance = True
                    Me.BtnSave_Click(Nothing, Nothing)
                    Me.txtBarCodeEmpId.Focus()
                    blnBarcodeAttendance = False
                Else
                    If InvalidEmployee = True Then
                        Me.RbtIn.Checked = True
                        Me.lblError.Text = "Invalid Code"
                        Me.ErrorProvider1.SetError(Me.lblError, "Invalid Code")
                        Me.txtBarCodeEmpId.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetId(ByVal Code As String)
        Try
            Dim EmpCode As String = String.Empty
            Dim intInTime As Integer = 0I
            Dim intOutTime As Integer = 0I
            InvalidEmployee = False
            If Code.Length > 1 Then
                'If Not Code.Contains("|") Then Exit Sub
                Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
                'If IsNumeric(Code.Substring(0, Code.LastIndexOf("|"))) Then
                '    id = Code.Substring(0, Code.LastIndexOf("|"))
                'Else
                '    Exit Sub
                'End If
                Dim dtEmp As DataTable = CType(Me.cmbEmployees.DataSource, DataTable)
                Dim drEmp() As DataRow = dtEmp.Select("Employee_Code='" & Me.txtBarCodeEmpId.Text & "'")
                If drEmp IsNot Nothing Then
                    If drEmp.Length > 0 Then
                        Me.cmbEmployees.Value = Val(drEmp(0).Item("Employee_Id").ToString)
                        Me.PictureBox1.ImageLocation = cmbEmployees.ActiveRow.Cells("EmpPicture").Value.ToString
                        InvalidEmployee = False
                        Me.lblEmpName.Text = cmbEmployees.ActiveRow.Cells("Employee_Name").Value.ToString
                        Me.lblEmpCode.Text = cmbEmployees.ActiveRow.Cells("Employee_Code").Value.ToString
                        Me.lblDesig.Text = cmbEmployees.ActiveRow.Cells("Designation").Value.ToString
                        Me.lblDept.Text = cmbEmployees.ActiveRow.Cells("Department").Value.ToString
                    Else
                        Me.cmbEmployees.Rows(0).Activate()
                        InvalidEmployee = True
                        Me.PictureBox1.ImageLocation = Nothing
                        Me.lblEmpName.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Name").ToString
                        Me.lblEmpCode.Text = String.Empty  'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Code").ToString
                        Me.lblDesig.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Designation").ToString
                        Me.lblDept.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Department").ToString
                        Exit Sub
                    End If
                Else
                    Me.cmbEmployees.Rows(0).Activate()
                    InvalidEmployee = True
                    Me.PictureBox1.ImageLocation = Nothing
                    Me.lblEmpName.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Name").ToString
                    Me.lblEmpCode.Text = String.Empty  'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Employee_Code").ToString
                    Me.lblDesig.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Designation").ToString
                    Me.lblDept.Text = String.Empty 'CType(Me.cmbEmployees.SelectedItem, DataRowView).Item("Department").ToString
                    Exit Sub
                End If


                If GetFilterDataFromDataTable(dt, "[EmpId]=" & Me.cmbEmployees.Value & " AND [AttendanceType]='In' AND AttendanceDate='" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "'").ToTable("Attendance").Rows.Count > 0 Then
                    intInTime = GetFilterDataFromDataTable(dt, "[EmpId]=" & Me.cmbEmployees.Value & " AND [AttendanceType]='In' AND AttendanceDate='" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "'").ToTable("Attendance").Rows.Count
                End If

                If GetFilterDataFromDataTable(dt, "[EmpId]=" & Me.cmbEmployees.Value & " AND [AttendanceType]='Out' AND AttendanceDate='" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "'").ToTable("Attendance").Rows.Count > 0 Then
                    intOutTime = GetFilterDataFromDataTable(dt, "[EmpId]=" & Me.cmbEmployees.Value & " AND [AttendanceType]='Out' AND AttendanceDate='" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "'").ToTable("Attendance").Rows.Count
                End If

                If intInTime = intOutTime Then
                    Me.RbtIn.Checked = True
                ElseIf intInTime > intOutTime Then
                    Me.RbtOut.Checked = True
                Else
                    Me.RbtIn.Checked = True
                End If

                Me.cmbAttendanceStatus.Text = "Present"
                If Me.RbtIn.Checked = True Then
                    Me.lblAttendanceType.Text = "In"
                    Me.lblSeeU.Visible = False
                    Me.lblWelcome.Visible = True
                    Me.lblWelcome.BringToFront()
                    Application.DoEvents()

                ElseIf Me.RbtOut.Checked = True Then
                    Me.lblAttendanceType.Text = "Out"
                    Me.lblSeeU.Visible = True
                    Me.lblWelcome.Visible = False
                    Me.lblSeeU.BringToFront()
                    Application.DoEvents()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnBarcodeScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcodeScan.Click
        Try
            If Me.Panel1.Visible = True Then
                'Me.grbBarcodeScan.Visible = True
                Me.Panel1.Visible = False
                Me.cmbEmployees.Focus()
            ElseIf Me.Panel1.Visible = False Then
                'Me.grbBarcodeScan.Visible = False
                Me.Panel1.BringToFront()
                Me.Panel1.Visible = True
                Me.txtBarCodeEmpId.Focus()
            End If
            HideControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Protected Sub HideControls()
        Try
            For i As Integer = 0 To Me.ToolStrip1.Items.Count - 1
                If Panel1.Visible = True Then Me.ToolStrip1.Items(i).Visible = False Else Me.ToolStrip1.Items(i).Visible = True
            Next
            Me.btnBarcodeScan.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RbtIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtIn.CheckedChanged
        Try
            lblAttendanceType.Text = "In"
            Application.DoEvents()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RbtOut_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtOut.CheckedChanged
        Try
            lblAttendanceType.Text = "Out"
            Application.DoEvents()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmAttendanceEmployees_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try

            If e.KeyCode = Keys.OemQuestion Then
                Me.txtBarCodeEmpId.Focus()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Back Then
                e.Handled = True

                'ElseIf e.KeyCode = Keys.Enter
                'Me.txtBarCodeEmpId.Focus()
                '    e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
  
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Daily Attendance Report "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmbEmployees_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployees.ValueChanged
        Try
            If Me.cmbAttendanceStatus.SelectedIndex = -1 Then Exit Sub
            'Dim str As String = "SELECT  EmployeeDeptDefTable.EmployeeDeptName, EmployeeDesignationDefTable.EmployeeDesignationName FROM tblDefEmployee INNER JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId WHERE tblDefEmployee.Employee_Id=" & Me.cmbEmployees.SelectedValue & " "
            'Dim dt As DataTable = GetDataTable(str)
            'If dt.Rows.Count > 0 Then
            '    Me.EmpDesignation = dt.Rows(0).Item("EmployeeDesignationName").ToString
            '    Me.EmpDepartment = dt.Rows(0).Item("EmployeeDeptName").ToString
            'Else
            '    Me.EmpDesignation = String.Empty
            '    Me.EmpDepartment = String.Empty
            'End If
            Me.txtEmpDesignation.Text = cmbEmployees.ActiveRow.Cells("Designation").Value.ToString
            Me.txtEmpDepartment.Text = cmbEmployees.ActiveRow.Cells("Department").Value.ToString
            Me.dtpAttendanceTime.Format = DateTimePickerFormat.Time
            If Me.cmbEmployees.Value > 0 Then
                Timer1.Stop()
            Else
                Timer1.Start()
            End If
            GetShift(Me.cmbEmployees.Value)
            Me.PictureBox1.ImageLocation = cmbEmployees.ActiveRow.Cells("EmpPicture").Value.ToString
            Application.DoEvents()
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = AdminEmail
            CtrlGrdBar1.Email.Subject = "Employee Attandance: " + "(" & Me.dtpAttendanceDate.Value.ToString("dd-MM-yyyy") & ")"
            CtrlGrdBar1.Email.DocumentNo = Me.dtpAttendanceDate.Value.ToString("dd-MM-yyyy")
            CtrlGrdBar1.Email.DocumentDate = Me.dtpAttendanceDate.Value.Date
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@Employee_Name")
            str.Add("@Father_Name")
            str.Add("@Employee_Code")
            str.Add("@AttendanceDate")
            str.Add("@AttendanceTime")
            str.Add("@AttendanceStatus")
            str.Add("@Department")
            str.Add("@Designation")
            str.Add("@Shift")
            str.Add("@CompanyName")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Attendance In Time")
            str.Add("Attendance Out Time")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region
    Public Sub SendSMS()
        Try
            '...................... Send SMS .............................
            Dim strAdminMobile As String = String.Empty
            Dim strEmpMobile As String = String.Empty

            If GetSMSConfig("Employee Attendance").Enable = True Then
                strEmpMobile = Me.cmbEmployees.ActiveRow.Cells("Mobile").Value.ToString.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
            End If

            If GetSMSConfig("Employee Attendance").EnabledAdmin = True Then
                strAdminMobile = getConfigValueByType("AdminMobileNo").ToString.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
            End If

            If blnBarcodeAttendance = False Then If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
            Dim strDetailMessage As String = String.Empty
            Dim objTemp As New SMSTemplateParameter

            Dim obj As Object = Nothing
            If Me.RbtIn.Checked = True Then
                obj = GetSMSTemplate("Attendance In Time")
            Else
                obj = GetSMSTemplate("Attendance Out Time")
            End If
            If obj IsNot Nothing Then
                objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                Dim strMessage As String = objTemp.SMSTemplate
                strMessage = strMessage.Replace("@Employee_Name", Me.cmbEmployees.ActiveRow.Cells("Employee_Name").Value.ToString).Replace("@Father_Name", Me.cmbEmployees.ActiveRow.Cells("Father_Name").Value.ToString).Replace("@Employee_Code", Me.cmbEmployees.ActiveRow.Cells("Employee_Code").Value.ToString).Replace("@AttendanceDate", Me.dtpAttendanceDate.Value).Replace("@AttendanceTime", Me.dtpAttendanceTime.Value).Replace("@AttendanceStatus", Me.cmbAttendanceStatus.Text).Replace("@Department", Me.txtEmpDepartment.Text).Replace("@Designation", Me.txtEmpDesignation.Text).Replace("@Shift", IIf(Me.cmbShift.SelectedIndex = -1, "", Me.cmbShift.Text)).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.SIRIUS.net")
                Dim strMobiles() As String = CStr(strEmpMobile & ";" & strAdminMobile).Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                Dim strPhone As String = String.Empty
                If strMobiles.Length > 0 Then
                    For Each str_Phone As String In strMobiles
                        If str_Phone.Length >= 10 Then
                            strPhone = str_Phone
                            If strPhone.Length < 12 Then
                                strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                            Else
                                strPhone = strPhone
                            End If
                            If strPhone.Length > 11 Then
                                SaveSMSLog(strMessage, strPhone, "Attendance")
                            End If
                        End If
                        strPhone = String.Empty
                    Next
                End If
            End If


            '...................... End Send SMS ................
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbEmployees_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbEmployees.InitializeLayout

    End Sub

    'Task#20150512 Attendance of all employees Ali Ansari
    Private Sub BtnSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            If IsDateLock(Me.dtpAttendanceDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If

           
            FillModel()

            If Now.Date < Me.dtpAttendanceDate.Value.Date Then
                ShowErrorMessage("This date attendance not allowed.")
                Me.dtpAttendanceDate.Focus()
                Exit Sub
            End If
            If SaveAll() Then DialogResult = Windows.Forms.DialogResult.Yes

            If Me.Panel1.Visible = True Then
                Me.txtBarCodeEmpId.Text = String.Empty
                GetAllRecords()
                Timer2.Enabled = True
            Else
                ReSetControls()
            End If
            
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Task#20150512 Attendance of all employees Ali Ansari

    Private Sub btnFullScreen_Click(sender As Object, e As EventArgs) Handles btnFullScreen.Click
        Try
            Dim frm As New Form
            frm = frmMain.Panel2.Controls(0)
            frm.Visible = False
            frmMain.Panel2.Controls.RemoveAt(0)
            frm.TopLevel = True
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            frm.WindowState = FormWindowState.Maximized
            frm.MaximizeBox = True
            frm.MinimizeBox = True
            frm.TopMost = True
            frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnOutTimeAttendance_Click(sender As Object, e As EventArgs) Handles btnOutTimeAttendance.Click
        Try
            ApplyStyleSheet(frmOutTimeAttendance)
            frmOutTimeAttendance._AttendanceDate = Me.dtpAttendanceDate.Value
            frmOutTimeAttendance.ShowDialog()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnHideSearchBy_Click(sender As Object, e As EventArgs) Handles btnHideSearchBy.Click
        Try
            If Me.gbSearchBy.Visible = True Then
                Me.gbSearchBy.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchHistory_Click(sender As Object, e As EventArgs) Handles btnSearchHistory.Click
        Try
            If Me.gbSearchBy.Visible = False Then
                Me.gbSearchBy.Visible = True
            Else
                Me.gbSearchBy.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSearchByDesignation_ValueChanged(sender As Object, e As EventArgs) Handles cmbSearchByDesignation.ValueChanged
        Try
            If Me.cmbSearchByDesignation.Value > 0 Then
                FillCombos("SearchEmployeeByDesignation")
            ElseIf Me.cmbSearchByDepartment.Value > 0 AndAlso Me.cmbSearchByDesignation.Value > 0 Then
                FillCombos("SearchEmployeeByDepartmentAndDesignation")
            Else
                FillCombos("SearchEmployee")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Me.grd.DataSource = New SBDal.AttendanceEmpDAL().GetOnCriteria(Me.dtpFrom.Value, Me.dtpTo.Value, CostCenterRights, LoginUserId, Me.cmbSearchByDesignation.Value, Me.cmbSearchByEmployee.Value, Me.cmbSearchByDepartment.Value)
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSearchByDepartment_ValueChanged(sender As Object, e As EventArgs) Handles cmbSearchByDepartment.ValueChanged
        Try
            If Me.cmbSearchByDepartment.Value > 0 Then
                FillCombos("SearchEmployeeByDepartment")
            ElseIf Me.cmbSearchByDepartment.Value > 0 AndAlso Me.cmbSearchByDesignation.Value > 0 Then
                FillCombos("SearchEmployeeByDepartmentAndDesignation")
            Else
                FillCombos("SearchEmployee")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs)

    End Sub

End Class