Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Public Class frmEmployeeStatusList
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'Task1248
        'Try
        '    Dim id As Integer
        '    id = Me.cmbDepartment.SelectedValue
        '    FillCombos("Department")
        '    Me.cmbDepartment.SelectedValue = id
        '    id = Me.cmbDesignation.SelectedValue
        '    FillCombos("Designation")
        '    Me.cmbDesignation.SelectedValue = id
        '    id = Me.cmbCostCenter.SelectedValue
        '    FillCombos("CostCenter")
        '    Me.cmbCostCenter.SelectedValue = id
        '    id = Me.cmbEmployee.SelectedValue
        '    FillCombos("Employee")
        '    Me.cmbEmployee.SelectedValue = id
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Refresh
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        'Task1248
        ''Uncommented Against TFS3418 : Ayesha Rehman : 29-05-2018
        Try
            Dim str As String = String.Empty
            Dim dt As DataTable
            If Me.rbtActive.Checked = True Then
                str = "SELECT     tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Code, tblDefEmployee.Employee_Name,EmployeeDeptDefTable.EmployeeDeptName AS Department, EmployeeDesignationDefTable.EmployeeDesignationName AS Designantion, tblDefCostCenter.Name AS CostCenter, tblDefEmployee.NIC AS CNIC, tblDefEmployee.Father_Name, tblDefEmployee.Gender, " _
                & " tblDefEmployee.DOB, tblDefEmployee.Mobile, tblDefEmployee.Phone, tblDefEmployee.Emergency_No, tblDefEmployee.Address," _
                & " tblDefEmployee.Email, tblDefEmployee.Joining_Date, tblDefEmployee.ConfirmationDate, tblDefEmployee.Salary, tblDefEmployee.JobType, tblDefEmployee.Qualification, " _
                & " tblDefEmployee.Blood_Group, tblDefEmployee.Religion, tblDefEmployee.Martial_Status, tblDefEmployee.NTN, tblDefEmployee.Passport_No, tblDefEmployee.BankAccount_No, tblDefEmployee.Bank_Ac_Name, tblDefEmployee.Active " _
                & " FROM         tblDefEmployee  LEFT OUTER JOIN " _
                & "          tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
                & "          EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                & "          EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId" _
                & " WHERE tblDefEmployee.Active=1 "
                ''Commented Against TFS3418
                '' & IIf(Me.cmbEmployee.SelectedIndex > 0, " AND  tblDefEmployee.Employee_ID = " & Me.cmbEmployee.SelectedValue & "", "") & IIf(Me.cmbDepartment.SelectedIndex > 0, " AND  tblDefEmployee.Dept_ID = " & Me.cmbDepartment.SelectedValue & "", "") & IIf(Me.cmbDesignation.SelectedIndex > 0, " AND  tblDefEmployee.Desig_ID = " & Me.cmbDesignation.SelectedValue & "", "") & " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblDefEmployee.CostCentre = " & Me.cmbCostCenter.SelectedValue & "", "") & " "
                If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.Dept_ID IN (" & Me.lstEmpDepartment.SelectedIDs & ")"
                End If
                If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.Desig_ID IN (" & Me.lstEmpDesignation.SelectedIDs & ")"
                End If
                If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.ShiftGroupId IN (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
                End If
                If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.City_ID IN (" & Me.lstEmpCity.SelectedIDs & ")"
                End If
                If Me.lstEmployee.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.Employee_ID IN (" & Me.lstEmployee.SelectedIDs & ")"
                End If
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.CostCentre IN (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
            Else
                str = "SELECT     tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Code, tblDefEmployee.Employee_Name,EmployeeDeptDefTable.EmployeeDeptName AS Department, EmployeeDesignationDefTable.EmployeeDesignationName AS Designantion, tblDefCostCenter.Name AS CostCenter, tblDefEmployee.NIC AS CNIC, tblDefEmployee.Father_Name, tblDefEmployee.Gender, " _
                & " tblDefEmployee.DOB, tblDefEmployee.Mobile, tblDefEmployee.Phone, tblDefEmployee.Emergency_No, tblDefEmployee.Address," _
                & " tblDefEmployee.Email, tblDefEmployee.Joining_Date, tblDefEmployee.Leaving_Date, tblDefEmployee.Salary, tblDefEmployee.JobType, tblDefEmployee.Qualification, " _
                & " tblDefEmployee.Blood_Group, tblDefEmployee.Religion, tblDefEmployee.Martial_Status, tblDefEmployee.NTN, tblDefEmployee.Passport_No, tblDefEmployee.BankAccount_No, tblDefEmployee.Bank_Ac_Name, tblDefEmployee.Active " _
                & " FROM         tblDefEmployee  LEFT OUTER JOIN " _
                & "          tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
                & "          EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                & "          EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId" _
                & " WHERE tblDefEmployee.Active=0 "
                ''Commented Against TFS3418
                '& IIf(Me.cmbEmployee.SelectedIndex > 0, " AND  tblDefEmployee.Employee_ID = " & Me.cmbEmployee.SelectedValue & "", "") & IIf(Me.cmbDepartment.SelectedIndex > 0, " AND  tblDefEmployee.Dept_ID = " & Me.cmbDepartment.SelectedValue & "", "") & IIf(Me.cmbDesignation.SelectedIndex > 0, " AND  tblDefEmployee.Desig_ID = " & Me.cmbDesignation.SelectedValue & "", "") & " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblDefEmployee.CostCentre = " & Me.cmbCostCenter.SelectedValue & "", "") & " "
                If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.Dept_ID IN (" & Me.lstEmpDepartment.SelectedIDs & ")"
                End If
                If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.Desig_ID IN (" & Me.lstEmpDesignation.SelectedIDs & ")"
                End If
                If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.ShiftGroupId IN (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
                End If
                If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.City_ID IN (" & Me.lstEmpCity.SelectedIDs & ")"
                End If
                If Me.lstEmployee.SelectedIDs.Length > 0 Then
                    str += " AND Emp.Employee_ID IN (" & Me.lstEmployee.SelectedIDs & ")"
                End If
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    str += " AND tblDefEmployee.CostCentre IN (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
            End If
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grdEmployee.DataSource = dt
            Me.grdEmployee.RetrieveStructure()
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdEmployee.RootTable.Columns("Employee_ID").Visible = False
            Me.grdEmployee.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployee.RootTable.Columns("Salary").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Me.btnNew.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnNew.Enabled = False
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

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            '#task1248
            'Dim str As String = String.Empty
            'If Condition = "Department" Then
            '    str = "Select EmployeeDeptId,EmployeeDeptName from EmployeeDeptDefTable where Active= 1"
            '    FillDropDown(cmbDepartment, str, True)
            'ElseIf Condition = "Designation" Then
            '    str = "Select EmployeeDesignationId,EmployeeDesignationName FROM EmployeeDesignationDefTable Where Active=1"
            '    FillDropDown(cmbDesignation, str, True)
            'ElseIf Condition = "CostCenter" Then
            '    str = "Select CostCenterId,[Name] from tblDefCostCenter Where Active=1"
            '    FillDropDown(cmbCostCenter, str, True)
            'ElseIf Condition = "Employee" Then
            '    str = "SELECT Employee_ID,Employee_Name FROM tblDefEmployee"
            '    FillDropDown(cmbEmployee, str, True)    End If
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1") ''TASKTFS75 added and set active =1
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                ''FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
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
        Try

        Catch ex As Exception
            Throw ex
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

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            '#task1248
            'Me.cmbDepartment.SelectedIndex = 0
            'Me.cmbDesignation.SelectedIndex = 0
            'Me.cmbCostCenter.SelectedIndex = 0
            'Me.cmbEmployee.SelectedIndex = 0
            Me.rbtActive.Checked = True
            Me.grdEmployee.ClearStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmEmployeeStatusList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployeeStatusList_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
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
     

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdEmployee.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            If Me.rbtActive.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Active Employees List"
            Else
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "In-Active Employees List"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

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
End Class