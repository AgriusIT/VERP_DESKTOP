'Ali Faisal : TFS1480 : Add report for Late In Time Summary 
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility
Partial Class LateInTimeSummary
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Public WorkingDays As Integer = 1
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Period selection to show records in that period
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1480</remarks>
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Form loading and fill dropdowns
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LateInTimeSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        '#Task1248
        'Try
        '    Me.cmbPeriod.Text = "Current Month"
        '    Me.cmbCostCenter.Visible = True
        '    Me.lblCostCenter.Visible = True
        '    If Me.pnlCost.Visible = False Then Me.pnlCost.Visible = True
        '    FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
        '    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView", True)
        '    FillDropDown(Me.cmbDepartment, "Select EmployeeDeptID, EmployeeDeptName, IsNull(DeptAccountHeadId,0) as DeptAccountHeadId from EmployeeDeptDefTable", True)
        '    Me.cmbEmployee.Rows(0).Activate()
        '    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
        '        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
        '    End If
        '    GetSecurityRights()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
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
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Apply security rights
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnShow.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnShow.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetAllRecords()
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Get all records on the basis of provided criteria
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetAllRecords()
        Try
            If Not getConfigValueByType("OverTimeWorkingDays").ToString = "True" Then
                WorkingDays = DateTime.DaysInMonth(Me.dtpFromDate.Value.Year, Me.dtpFromDate.Value.Month)
            Else
                WorkingDays = Val(getConfigValueByType("OverTimeWorkingDays").ToString)
            End If
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Emp.Employee_ID, Emp.Employee_Name AS [Emp Name], Emp.Employee_Code AS [Emp Code], Emp.CostCentre AS CostCenterId, tblDefCostCenter.Name AS CostCenter, EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, EmployeeDeptDefTable.EmployeeDeptName AS Department, Emp.Salary / " & WorkingDays & " PerDaySalary, EmpIn.Minutes AS LateInMin " _
                & "FROM tblDefEmployee AS Emp LEFT OUTER JOIN tblDefCostCenter ON Emp.CostCentre = tblDefCostCenter.CostCenterID RIGHT OUTER JOIN EmployeeDesignationDefTable ON Emp.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId RIGHT OUTER JOIN EmployeeDeptDefTable ON Emp.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
                & "(SELECT tblAttendanceDetail_1.EmpId, CASE WHEN SUM(CASE WHEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) > 0 THEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) ELSE 0 END) > 0  " _
                & "THEN SUM(CASE WHEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) > 0 THEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) ELSE 0 END) ELSE 0 END AS Minutes " _
                & "FROM tblAttendanceDetail AS tblAttendanceDetail_1 INNER JOIN (SELECT EmpId, CONVERT(dateTime, MIN(AttendanceTime), 102) AS AttendanceTime FROM tblAttendanceDetail GROUP BY CONVERT(varchar, AttendanceDate, 102), EmpId) AS MinEmp ON tblAttendanceDetail_1.EmpId = MinEmp.EmpId AND CONVERT(dateTime, tblAttendanceDetail_1.AttendanceTime, 102) = CONVERT(dateTime, MinEmp.AttendanceTime, 102) INNER JOIN " _
                & "ShiftTable ON tblAttendanceDetail_1.ShiftId = ShiftTable.ShiftId WHERE (CONVERT(DateTime, tblAttendanceDetail_1.AttendanceDate, 102) BETWEEN CONVERT(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (tblAttendanceDetail_1.AttendanceStatus NOT IN ('Short Leave', 'Half Leave', 'Short Absent')) GROUP BY tblAttendanceDetail_1.EmpId " _
                & "HAVING (SUM(CASE WHEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) > 0 THEN DATEDIFF(Minute, CONVERT(varchar(8), ShiftTable.FlexInTime, 108), CONVERT(varchar(8), tblAttendanceDetail_1.AttendanceTime, 108)) ELSE 0 END) BETWEEN " & Me.txtMinFrom.Text & " AND " & Me.txtMinTo.Text & ")) AS EmpIn ON Emp.Employee_ID = EmpIn.EmpId " _
                & "WHERE (Emp.Employee_ID <> 0) AND (EmpIn.Minutes <> '') "
            ''Task1248  If Me.cmbCostCenter.SelectedValue > 0 Then
            'str += " AND (Emp.CostCentre = " & IIf(Me.cmbCostCenter.SelectedValue > 0, Me.cmbCostCenter.SelectedValue, 0) & ")"
            'End If
            'If Me.cmbEmployee.Value > 0 Then
            '    str += " AND (Emp.Employee_ID = " & IIf(Me.cmbEmployee.Value > 0, Me.cmbEmployee.Value, 0) & ")"
            'End If
            ''Task1248  If Me.cmbDepartment.SelectedValue > 0 Then
            'str += " AND (Emp.Dept_ID = " & IIf(Me.cmbDepartment.SelectedValue > 0, Me.cmbDepartment.SelectedValue, 0) & ")"
            'End If
            If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                str += " AND Emp.Dept_ID IN (" & Me.lstEmpDepartment.SelectedIDs & ")"
            End If
            If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                str += " AND Emp.Desig_ID IN (" & Me.lstEmpDesignation.SelectedIDs & ")"
            End If
            If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                str += " AND Emp.ShiftGroupId IN (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                str += " AND Emp.City_ID IN (" & Me.lstEmpCity.SelectedIDs & ")"
            End If
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                str += " AND Emp.Employee_ID IN (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND Emp.CostCentre IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Apply grid settings to show/hide columns and formating
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns("Employee_ID").Visible = False
            Me.grd.RootTable.Columns("CostCenterId").Visible = False
            Me.grd.RootTable.Columns("PerDaySalary").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PerDaySalary").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PerDaySalary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("LateInMin").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1480 : Load control
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Late in time summary report" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMinFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMinFrom.KeyPress
        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMinTo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMinTo.KeyPress
        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub


    Public Sub ApplyGridSettings1(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                'Me.btnPrint.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            'Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
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

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1") ''TASKTFS75 added and set active =1
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                '' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstEmpShiftGroup.ListItem, "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1 ")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstEmpCity.ListItem, "SELECT  CityId, CityName FROM tblListCity WHERE Active=1 ")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords

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