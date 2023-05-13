﻿Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility
Partial Class EmployeeAttendanceDetail
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
            'Ali Faisal : TFS1477 : Add Report for night shift
            'Ali Faisal : TFS1600 : Change report criteria
            If Me.rbtnNormalShift.Checked = True Then
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                AddRptParam("@DepartmentIds", Me.lstEmpDepartment.SelectedIDs)
                AddRptParam("@DesignationIds", Me.lstEmpDesignation.SelectedIDs)
                AddRptParam("@ShiftGroupIds", Me.lstEmpShiftGroup.SelectedIDs)
                AddRptParam("@CityIds", Me.lstEmpCity.SelectedIDs)
                AddRptParam("@EmployeeIds", Me.lstEmployee.SelectedIDs)
                AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
                'ShowReport("rptEmployeeAttendanceDetail", "{SP_EmpAttendanceDetailByIn;1.Employee_Id} <> 0 " & IIf(Me.lstEmployee.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.Employee_ID} IN " & Me.lstEmployee.SelectedIDs & "", "") & " " & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.CostCentre} IN " & Me.lstCostCenter.SelectedIDs & "", ""))
                'ShowReport("rptEmployeeAttendanceDetail", "{SP_EmpAttendanceDetailByIn;1.Employee_ID} <> 0 " & IIf(Me.lstEmployee.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.Employee_ID} IN [" & Me.lstEmployee.SelectedIDs & "]", "") & " " & IIf(Me.lstEmpDepartment.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.Dept_ID} IN [" & Me.lstEmpDepartment.SelectedIDs & "]", "") & " " & IIf(Me.lstEmpDesignation.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.Desig_ID} IN [" & Me.lstEmpDesignation.SelectedIDs & "]", "") & "" & IIf(Me.lstEmpShiftGroup.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.ShiftGroupId} IN [" & Me.lstEmpShiftGroup.SelectedIDs & "]", "") & "" & IIf(Me.lstEmpCity.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.City_ID} IN [" & Me.lstEmpCity.SelectedIDs & "]", "") & "" & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND {SP_EmpAttendanceDetailByIn;1.CostCentre} IN [" & Me.lstCostCenter.SelectedIDs & "]", ""), "")
                ShowReport("rptEmployeeAttendanceDetail")
            Else
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                AddRptParam("@DepartmentIds", Me.lstEmpDepartment.SelectedIDs)
                AddRptParam("@DesignationIds", Me.lstEmpDesignation.SelectedIDs)
                AddRptParam("@ShiftGroupIds", Me.lstEmpShiftGroup.SelectedIDs)
                AddRptParam("@CityIds", Me.lstEmpCity.SelectedIDs)
                AddRptParam("@EmployeeIds", Me.lstEmployee.SelectedIDs)
                AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
                ShowReport("rptEmployeeAttendanceDetailNight")
            End If
            'Ali Faisal : TFS1600 : End
            'Ali Faisal : TFS1477 : End
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
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
    Private Sub EmployeeAttendanceDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            FillListBox(Me.lstEmpDepartment.ListItem, "SELECT EmployeeDeptId, EmployeeDeptName FROM EmployeeDeptDefTable WHERE Active = 1")
            Me.lstEmpDepartment.DeSelect()
            FillListBox(Me.lstEmpDesignation.ListItem, "SELECT EmployeeDesignationId, EmployeeDesignationName FROM EmployeeDesignationDefTable WHERE Active = 1")
            Me.lstEmpDesignation.DeSelect()
            FillListBox(Me.lstEmpShiftGroup.ListItem, "SELECT ShiftGroupId, ShiftGroupName FROM ShiftGroupTable WHERE Active = 1")
            Me.lstEmpShiftGroup.DeSelect()
            FillListBox(Me.lstEmpCity.ListItem, "SELECT CityId, CityName FROM tblListCity WHERE Active = 1")
            Me.lstEmpCity.DeSelect()
            FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1")
            Me.lstEmployee.DeSelect()
            FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            Me.lstHeadCostCenter.DeSelect()
            '' FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1") ''TFS3320
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
            FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
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
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
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
    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                ''  FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "')") ''TFS3320
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') ")
            Else
                '' FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1") ''TFS3320
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.OK_Button.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.OK_Button.Enabled = False
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

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
End Class