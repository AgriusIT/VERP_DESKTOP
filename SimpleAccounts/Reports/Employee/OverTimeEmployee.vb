Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility
Partial Class OverTimeEmployee
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            'AddRptParam("@EmployeeId", Me.cmbEmployee.Value)
            ''Start TFS3418
            AddRptParam("@DepartmentIds", Me.lstEmpDepartment.SelectedIDs)
            AddRptParam("@DesignationsIds", Me.lstEmpDesignation.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstEmpShiftGroup.SelectedIDs)
            AddRptParam("@CityIds", Me.lstEmpCity.SelectedIDs)
            AddRptParam("@EmployeeId", Me.lstEmployee.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            ''End TFS3418
            ShowReport("rptOverTimeEmployee")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub EmployeeAttendanceDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            Me.Text = "Employees Attendance Detail"
            FillCombos(Me.lstEmpDepartment.Name)
            Me.lstEmpDepartment.DeSelect()
            FillCombos(Me.lstEmpDesignation.Name)
            Me.lstEmpDesignation.DeSelect()
            FillCombos(Me.lstEmpShiftGroup.Name)
            Me.lstEmpShiftGroup.DeSelect()
            FillCombos(Me.lstEmpCity.Name)
            Me.lstEmpCity.DeSelect()
            FillCombos(Me.lstEmployee.Name)
            Me.lstEmployee.DeSelect()
            FillCombos(Me.lstHeadCostCenter.Name)
            Me.lstHeadCostCenter.DeSelect()
            FillCombos(Me.lstCostCenter.Name)
            Me.lstCostCenter.DeSelect()
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
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.OK_Button.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnPrint.Enabled = False
                    Me.OK_Button.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnPrint.Enabled = False
                Me.OK_Button.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.OK_Button.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.OK_Button.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.OK_Button.Enabled = False
            Me.btnPrint.Enabled = False
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = Me.lstEmpDepartment.Name Then
                strSQL = "SELECT  EmployeeDeptId,EmployeeDeptName FROM EmployeeDeptDefTable "
                Me.lstEmpDepartment.ListItem.DisplayMember = "EmployeeDeptName"
                Me.lstEmpDepartment.ListItem.ValueMember = "EmployeeDeptId"
                Me.lstEmpDepartment.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstEmpDesignation.Name Then
                strSQL = "SELECT  EmployeeDesignationId,EmployeeDesignationName FROM EmployeeDesignationDefTable "
                Me.lstEmpDesignation.ListItem.DisplayMember = "EmployeeDesignationName"
                Me.lstEmpDesignation.ListItem.ValueMember = "EmployeeDesignationId"
                Me.lstEmpDesignation.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstEmpShiftGroup.Name Then
                strSQL = "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable "
                Me.lstEmpShiftGroup.ListItem.DisplayMember = "ShiftGroupName"
                Me.lstEmpShiftGroup.ListItem.ValueMember = "ShiftGroupId"
                Me.lstEmpShiftGroup.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstEmpCity.Name Then
                strSQL = "SELECT  CityId, CityName FROM tblListCity"
                Me.lstEmpCity.ListItem.DisplayMember = "CityName"
                Me.lstEmpCity.ListItem.ValueMember = "CityId"
                Me.lstEmpCity.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstEmployee.Name Then
                strSQL = "SELECT Employee_Id, Employee_Code + '  ' + Employee_Name as Employee_Name FROM tblDefEmployee"
                Me.lstEmployee.ListItem.DisplayMember = "Employee_Name"
                Me.lstEmployee.ListItem.ValueMember = "Employee_Id"
                Me.lstEmployee.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstHeadCostCenter.Name Then
                strSQL = "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter Where CostCenterGroup <> ''"
                Me.lstHeadCostCenter.ListItem.DisplayMember = "CostCenterGroup"
                Me.lstHeadCostCenter.ListItem.ValueMember = "CostCenterID"
                Me.lstHeadCostCenter.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.lstCostCenter.Name Then
                ''strSQL = "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter" ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                strSQL = " If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 "
                Me.lstCostCenter.ListItem.DisplayMember = "Name"
                Me.lstCostCenter.ListItem.ValueMember = "CostCenterID"
                Me.lstCostCenter.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
            End If
        Catch ex As Exception
            Throw ex

        End Try
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