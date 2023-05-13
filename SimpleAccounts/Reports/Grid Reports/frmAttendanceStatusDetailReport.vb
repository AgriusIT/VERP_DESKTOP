Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmAttendanceStatusDetailReport
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
        Dim strASD As String = ""
        Try
            ''Commented Against TFS3418 : Ayesha Rehman : 25-05-2018
            '  strASD = " Select tblDefEmployee.Employee_Name, EmployeeDesignationDefTable.EmployeeDesignationName As Designation, " _
            '      & " EmployeeDeptDefTable.EmployeeDeptName As Department, tblDefCostCenter.Name As CostCentre, " _
            '      & " tblAttendanceDetail.AttendanceDate, Convert(DateTime, tblAttendanceDetail.AttendanceTime, 102) As AttendanceTime, " _
            '   & " tblAttendanceDetail.AttendanceStatus FROM tblDefEmployee " _
            '  & " Inner Join tblAttendanceDetail ON tblDefEmployee.Employee_ID = tblAttendanceDetail.EmpId " _
            '  & " Left Join EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId " _
            ' & "  Left Join EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId " _
            ' & " Left Join tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID " _
            '& "  Where Convert(DateTime, tblAttendanceDetail.AttendanceDate, 102) Between Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)  And Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102) "
            ''Start TFS3418
            strASD = " Select tblDefEmployee.Employee_Name, EmployeeDesignationDefTable.EmployeeDesignationName As Designation, " _
                & " EmployeeDeptDefTable.EmployeeDeptName As Department, tblDefCostCenter.Name As CostCentre, " _
                & " tblAttendanceDetail.AttendanceDate, Convert(DateTime, tblAttendanceDetail.AttendanceTime, 102) As AttendanceTime, " _
                & " tblAttendanceDetail.AttendanceStatus FROM tblDefEmployee " _
                & " Inner Join tblAttendanceDetail ON tblDefEmployee.Employee_ID = tblAttendanceDetail.EmpId " _
                & " Left Join EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId " _
                & "  Left Join EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId " _
                & " Left Join tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID " _
                & "  LEFT OUTER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId " _
                & " LEFT OUTER JOIN  tblListCity ON tblDefEmployee.City_ID = tblListCity.CityId " _
                & "  Where Convert(DateTime, tblAttendanceDetail.AttendanceDate, 102) Between Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)  And Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102) "
            ''End TFS3418
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strASD += " AND tblDefCostCenter.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                strASD += " AND tblListCity.CityId  IN (" & Me.lstEmpCity.SelectedIDs & ")"
            End If
            If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                strASD += " AND EmployeeDeptDefTable.EmployeeDeptId  IN (" & Me.lstEmpDepartment.SelectedIDs & ")"
            End If
            If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                strASD += " AND EmployeeDesignationDefTable.EmployeeDesignationId IN (" & Me.lstEmpDesignation.SelectedIDs & ")"
            End If
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                strASD += " AND tblDefEmployee.Employee_ID  IN (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                strASD += " AND ShiftGroupTable.ShiftGroupId IN (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
            End If
        
            'Task1248
            'If Me.cmbEmployees.Value > 0 Then
            '    strASD += " And tblDefEmployee.Employee_ID =" & Me.cmbEmployees.Value & ""
            'End If
            'If Me.cmbSearchByDepartment.Value > 0 Then
            '    strASD += " And tblDefEmployee.Dept_ID =" & Me.cmbSearchByDepartment.Value & ""
            'End If
            'If Me.cmbSearchByDesignation.Value > 0 Then
            '    strASD += " And tblDefEmployee.Desig_ID =" & Me.cmbSearchByDesignation.Value & ""
            'End If
            'If Me.cmbCostCentre.SelectedValue > 0 Then
            '    strASD += " And tblDefEmployee.CostCentre =" & Me.cmbCostCentre.SelectedValue & ""
            'End If
            If Me.cmbAttendanceStatus.SelectedValue > 0 Then
                strASD += " And tblAttendanceDetail.AttendanceStatus Like '%" & Me.cmbAttendanceStatus.Text & "%'"
            End If

            Dim dt As DataTable = GetDataTable(strASD)
            '#Task1248
            ''Uncommented Against TFS3418
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("AttendanceTime").FormatString = "h:mm:ss tt"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1") ''TASKTFS75 added and set active =1
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstEmpShiftGroup.ListItem, "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1 ")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstEmpCity.ListItem, "SELECT  CityId, CityName FROM tblListCity WHERE Active=1 ")
            ElseIf Condition = "Status" Then
                FillDropDown(Me.cmbAttendanceStatus, "Select Att_Status_ID, Att_Status_Name, Att_Status_Code, Active From tblDefAttendenceStatus WHERE Active=1")

            End If
        Catch ex As Exception
            Throw ex
        End Try
        '#task1248
        'Try
        '    Dim str As String = String.Empty
        '    If Condition = String.Empty Then
        '        str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
        '        FillUltraDropDown(Me.cmbEmployees, str)
        '        Me.cmbEmployees.Rows(0).Activate()
        '        If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
        '            Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
        '            Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
        '            Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
        '        End If
        '        FillDropDown(Me.cmbAttendanceStatus, "Select Att_Status_ID, Att_Status_Name, Att_Status_Code, Active From tblDefAttendenceStatus WHERE Active=1")
        '    ElseIf Condition = "Designation" Then
        '        str = "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable"
        '        FillUltraDropDown(Me.cmbSearchByDesignation, str)
        '        Me.cmbSearchByDesignation.Rows(0).Activate()
        '        If Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns.Count > 0 Then
        '            Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationId").Hidden = True
        '        End If
        '    ElseIf Condition = "Department" Then
        '        str = "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable"
        '        FillUltraDropDown(Me.cmbSearchByDepartment, str)
        '        Me.cmbSearchByDepartment.Rows(0).Activate()
        '        If Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns.Count > 0 Then
        '            Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptId").Hidden = True
        '        End If
        '    ElseIf Condition = "CostCentre" Then
        '        FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 ORDER BY 2 ASC")
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub frmAttendanceStatusDetailReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today

            GetSecurityRights()
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Status")
            FillCombos("Designation")
            Me.lstEmpDesignation.DeSelect()
            FillCombos("Department")
            Me.lstEmpDepartment.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstEmpShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstEmpCity.DeSelect()
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            ServerDate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Refresh
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Status")
            FillCombos("Designation")
            Me.lstEmpDesignation.DeSelect()
            FillCombos("Department")
            Me.lstEmpDepartment.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstEmpShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstEmpCity.DeSelect()
            '#task1248
            'Dim ID As Integer = 0
            'ID = Me.cmbEmployees.Value
            'FillCombos("")
            'Me.cmbEmployees.Value = ID
            'ID = Me.cmbSearchByDepartment.Value
            'FillCombos("Department")
            'Me.cmbSearchByDepartment.Value = ID
            'ID = Me.cmbSearchByDesignation.Value
            'FillCombos("Designation")
            'Me.cmbSearchByDesignation.Value = ID
            'ID = Me.cmbCostCentre.SelectedValue
            'FillCombos("CostCentre")
            'Me.cmbCostCentre.SelectedValue = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnDisplay.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnDisplay.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
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

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos
    
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Employee_Name Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class