Imports CRUFLIDAutomation
Imports SBModel
Imports SBDal
Imports SBUtility

Public Class frmEmployeeWorkingHours
    Implements IGeneral
    Dim _SearchDt As New DataTable

    Private Sub frmEmployeeWorkingHours_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.cmbHours.SelectedIndex = 1
            Me.cmbPeriod.SelectedIndex = 2
            FillCombos(Me.lstEmpDepartment.Name)
            FillCombos(Me.lstEmpDesignation.Name)
            FillCombos(Me.lstEmpShiftGroup.Name)
            FillCombos(Me.lstEmpCity.Name)
            FillCombos(Me.lstEmployee.Name)
            FillCombos(Me.lstHeadCostCenter.Name)
            FillCombos(Me.lstCostCenter.Name)
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

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
                strSQL = "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter"
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
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpDateTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpDateTo.Value = Date.Today
        End If
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
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

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lstCostCenter.DeSelect()
            Me.lstEmpCity.DeSelect()
            Me.lstEmpDepartment.DeSelect()
            Me.lstEmpDesignation.DeSelect()
            Me.lstEmployee.DeSelect()
            Me.lstEmpShiftGroup.DeSelect()
            Me.lstHeadCostCenter.DeSelect()
            Me.txtHours.Text = ""
            Me.txtSearch.Text = ""
            Me.cmbHours.SelectedIndex = 1
            Me.cmbPeriod.SelectedIndex = 2
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            'Dim id As Integer = 0
            'id = Me.lstEmployee.SelectedItems
            'FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + '  ' + Employee_Name as Employee_Name FROM tblDefEmployee")
            'Me.lstEmployee.SelectedItems = id
        Catch ex As Exception

        End Try
    End Sub
End Class