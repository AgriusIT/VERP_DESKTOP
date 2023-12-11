Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmRptEmpSalarySheetDetail
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Sub FillCombo(Optional ByVal Condition As String = "")
        '#task1248
        'Try
        '    If Condition = "Dept" Then
        '        FillDropDown(Me.cmbDepartment, "Select * From EmployeeDeptDefTable")
        '    ElseIf Condition = "Emp" Then
        '        FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Code + '~' + Employee_Name as Employee_Name, Employee_Code From tblDefEmployee " & IIf(Me.cmbDepartment.SelectedIndex > 0, "  WHERE Dept_Id=" & Me.cmbDepartment.SelectedValue & "", "") & "")
        '    ElseIf Condition = "Bank" Then
        '        FillDropDown(Me.cmbBank, "Select Distinct Bank_Ac_Name, Bank_Ac_Name From tblDefEmployee WHERE Bank_Ac_Name <> ''")
        '    ElseIf Condition = "CostCentre" Then
        '        FillDropDown(Me.cmbCostCentre, "Select * From tblDefCostCenter Order By SortOrder, Name")
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            '#Task1248  If Not Me.cmbDepartment.SelectedIndex = -1 Then
            '  FillCombo("Emp")
            '  End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRptEmpSalarySheetDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Designation")
            Me.lstDesignation.DeSelect()
            FillCombos("Department")
            Me.lstDepartment.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstCity.DeSelect()
            FillCombos("Bank")
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()

            Dim strFilter As String = String.Empty

            strFilter = " {SP_SalarySheetDetail;1.Employee_Name} <> ''"
            '#task1248
            'If Me.cmbDepartment.SelectedIndex > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.Dept_Id} =" & Me.cmbDepartment.SelectedValue & ""
            'End If
            'If Me.cmbEmployee.SelectedIndex > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.Employee_Id}=" & Me.cmbEmployee.SelectedValue & ""

            'End If
            'TFS3813: 06/July/2018: Waqar Raza: Uncomment These Lines to show selected bank wise result
            'Start TFS3813:
            If Me.cmbBank.SelectedIndex > 0 AndAlso Me.cmbBank.Text.Length > 0 Then
                strFilter += " AND {SP_SalarySheetDetail;1.Bank_Ac_Name}='" & Me.cmbBank.Text.Replace("'", "''") & "'"
            End If
            'End TFS3813:
            'If Me.cmbCostCentre.SelectedValue > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.CostCentre}=" & Me.cmbCostCentre.SelectedValue & ""
            'End If

            ''Start TFS3418
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@DepartmentIds", Me.lstDepartment.SelectedIDs)
            AddRptParam("@DesignationsIds", Me.lstDesignation.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstShiftGroup.SelectedIDs)
            AddRptParam("@CityIds", Me.lstCity.SelectedIDs)
            AddRptParam("@EmployeeId", Me.lstEmployee.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            ''End TFS3418
            ShowReport("rptEmpSalarySheetDetail", strFilter.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
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
                FillListBox(Me.lstDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
          ElseIf Condition = "CostCentre" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstShiftGroup.ListItem, "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1 ")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstCity.ListItem, "SELECT  CityId, CityName FROM tblListCity WHERE Active=1 ")
            ElseIf Condition = "Bank" Then
                FillDropDown(Me.cmbBank, "Select Distinct Bank_Ac_Name, Bank_Ac_Name From tblDefEmployee WHERE Bank_Ac_Name <> ''")

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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

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