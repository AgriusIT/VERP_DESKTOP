'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class frmEmployeePromotion
    'EmployeePromotionBE in SBModel(Transaction Folder)
    'EmployeePromotionDAL in SBDal(Transaction Folder)
    Implements IGeneral
    Dim empPro As EmployeePromotionBE
    Dim proId As Integer = 0
    Dim old_eid As Integer = 0
    Dim old_salary As Double = 0

    Private Sub frmEmployeePromotion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

           
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmEmployeePromotion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            FillCombos("emp")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record " & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(c).Visible = False
            Next
            Me.grd.RootTable.Columns("Ref_No").Visible = True
            Me.grd.RootTable.Columns("Ref_Date").Visible = True
            Me.grd.RootTable.Columns("PromotionType").Visible = True
            Me.grd.RootTable.Columns("Employee_Code").Visible = True
            Me.grd.RootTable.Columns("Employee_Name").Visible = True


            Me.grd.RootTable.Columns("PromotionType").Caption = "Type"
            Me.grd.RootTable.Columns("Employee_Code").Caption = "Code"
            Me.grd.RootTable.Columns("Employee_Name").Caption = "Name"
            Me.grd.RootTable.Columns("Ref_Date").FormatString = "dd/MMM/yyyy"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "emp" Then
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'FillDropDown(cmbEmployee, "SELECT     dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDeptDefTable.EmployeeDeptId,dbo.EmployeeDeptDefTable.EmployeeDeptName, dbo.tblDefDivision.Division_Id,dbo.tblDefDivision.Division_Name,dbo.EmployeeDesignationDefTable.EmployeeDesignationId , dbo.EmployeeDesignationDefTable.EmployeeDesignationName, dbo.tblDefEmployee.Salary, dbo.tblDefEmployee.EmpPicture FROM         dbo.tblDefEmployee LEFT OUTER JOIN dbo.tblDefDivision ON dbo.tblDefEmployee.Dept_Division = dbo.tblDefDivision.Dept_Id LEFT OUTER JOIN dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN dbo.EmployeeDesignationDefTable ON dbo.tblDefEmployee.Desig_ID = dbo.EmployeeDesignationDefTable.EmployeeDesignationId ")
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillDropDown(cmbEmployee, "SELECT     dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDeptDefTable.EmployeeDeptId,dbo.EmployeeDeptDefTable.EmployeeDeptName, dbo.tblDefDivision.Division_Id,dbo.tblDefDivision.Division_Name,dbo.EmployeeDesignationDefTable.EmployeeDesignationId , dbo.EmployeeDesignationDefTable.EmployeeDesignationName, dbo.tblDefEmployee.Salary, dbo.tblDefEmployee.EmpPicture FROM         dbo.tblDefEmployee LEFT OUTER JOIN dbo.tblDefDivision ON dbo.tblDefEmployee.Dept_Division = dbo.tblDefDivision.Dept_Id LEFT OUTER JOIN dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN dbo.EmployeeDesignationDefTable ON dbo.tblDefEmployee.Desig_ID = dbo.EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1  ")
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
            End If

            If Condition = "dept" Then
                FillDropDown(Me.cmbDepartment, "Select  EmployeeDeptId, EmployeeDeptName from EmployeeDeptDefTable ")
            End If

            If Condition = "division" Then
                FillDropDown(Me.cmbDivision, "Select Division_Id, Division_Name from tblDefDivision")
            End If

            If Condition = "designation" Then
                FillDropDown(Me.cmbDesignation, "Select EmployeeDesignationId, EmployeeDesignationName from EmployeeDesignationDefTable")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ' Increment
            If Me.cmbPromotionType.SelectedIndex = 0 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.OldDepartmentId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptId").ToString)
                empPro.OldDepartmentName = Me.txtDepartment.Text
                empPro.OldDesignationId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationId").ToString)
                empPro.OldDesignationName = Me.txtDesignation.Text
                empPro.OldDivisionId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Id").ToString)
                empPro.OldDivisionName = Me.txtDivision.Text
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text) + Val(Me.txtIncreament.Text)
                empPro.Old_EmployeeId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Employee_ID").ToString)
                empPro.Old_BasicSalary = Val(Me.txtSalary.Text)
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

                ' Promotion
            ElseIf Me.cmbPromotionType.SelectedIndex = 1 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.OldDepartmentId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptId").ToString)
                empPro.OldDepartmentName = Me.txtDepartment.Text
                empPro.OldDesignationId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationId").ToString)
                empPro.OldDesignationName = Me.txtDesignation.Text
                empPro.OldDivisionId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Id").ToString)
                empPro.OldDivisionName = Me.txtDivision.Text
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text) + Val(Me.txtIncreament.Text)
                empPro.DepartmentId = Me.cmbDepartment.SelectedValue
                empPro.DesignationId = Me.cmbDesignation.SelectedValue
                empPro.DivisionId = Me.cmbDivision.SelectedValue
                empPro.Old_EmployeeId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Employee_ID").ToString)
                empPro.Old_BasicSalary = Val(Me.txtSalary.Text)
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

                ' Transfer
            ElseIf Me.cmbPromotionType.SelectedIndex = 2 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.OldDepartmentId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptId").ToString)
                empPro.OldDepartmentName = Me.txtDepartment.Text
                empPro.OldDesignationId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationId").ToString)
                empPro.OldDesignationName = Me.txtDesignation.Text
                empPro.OldDivisionId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Id").ToString)
                empPro.OldDivisionName = Me.txtDivision.Text
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                'empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text)
                empPro.DepartmentId = Me.cmbDepartment.SelectedValue
                empPro.DesignationId = Me.cmbDesignation.SelectedValue
                empPro.DivisionId = Me.cmbDivision.SelectedValue
                empPro.Old_EmployeeId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Employee_ID").ToString)
                empPro.Old_BasicSalary = Val(Me.txtSalary.Text)
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grd.DataSource = New EmployeePromotionDAL().GetAll()
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            ' Imcrement
            If Me.cmbPromotionType.SelectedIndex = 0 Then

                If Me.cmbEmployee.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a employee")
                    Me.cmbEmployee.Focus()
                    Return False
                End If

                If Me.txtIncreament.Text = String.Empty Then
                    ShowErrorMessage("Please enter increment amount")
                    Me.txtIncreament.Focus()
                    Return False
                End If

                ' Promotion
            ElseIf Me.cmbPromotionType.SelectedIndex = 1 Then

                If Me.cmbEmployee.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a employee")
                    Me.cmbEmployee.Focus()
                    Return False
                End If

                If Me.cmbDepartment.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a department")
                    Me.cmbDepartment.Focus()
                    Return False
                End If

                If Me.cmbDesignation.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a designation")
                    Me.cmbDesignation.Focus()
                    Return False
                End If

                'If Me.cmbDivision.SelectedIndex = 0 Then
                'ShowErrorMessage("Please select a division")
                'Me.cmbDivision.Focus()
                'Return False
                'End If

                ' Transfer
            ElseIf Me.cmbPromotionType.SelectedIndex = 2 Then

                If Me.cmbEmployee.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a employee")
                    Me.cmbEmployee.Focus()
                    Return False
                End If

                If Me.cmbDepartment.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a department")
                    Me.cmbDepartment.Focus()
                    Return False
                End If

                'If Me.cmbDesignation.SelectedIndex = 0 Then
                '    ShowErrorMessage("Please select a designation")
                '    Me.cmbDesignation.Focus()
                '    Return False
                'End If

                'If Me.cmbDivision.SelectedIndex = 0 Then
                '    ShowErrorMessage("Please select a division")
                '    Me.cmbDivision.Focus()
                '    Return False
                'End If

            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = EmployeePromotionDAL.GetSerialNo
            Me.dtpDocDate.Value = Date.Now
            Me.cmbPromotionType.SelectedIndex = 0
            Me.GroupBox2.Visible = False
            Me.Label7.Visible = False
            Me.cmbDepartment.Visible = False
            Me.Label8.Visible = False
            Me.cmbDivision.Visible = False
            Me.Label9.Visible = False
            Me.cmbDesignation.Visible = False
            Me.Label11.Visible = True
            Me.txtIncreament.Visible = True

            Me.cmbEmployee.SelectedIndex = 0
            Me.txtDepartment.Text = String.Empty
            Me.txtDivision.Text = String.Empty
            Me.txtDesignation.Text = String.Empty
            Me.txtSalary.Text = 0
            Me.txtIncreament.Text = 0
            Me.empPic.Image = Global.SimpleAccounts.My.Resources.Resources.no_image
            GetAllRecords()
            Me.btnSave.Text = "&Save"
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New EmployeePromotionDAL().Save(empPro, Me.cmbPromotionType.Text) = True Then
                Return True
            Else : Return False
            End If
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
            If New EmployeePromotionDAL().Update(empPro, Me.cmbPromotionType.Text) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbEmployee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmployee.SelectedIndexChanged
        Try
            Me.txtDepartment.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptName").ToString
            Me.txtDivision.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Name").ToString
            Me.txtDesignation.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationName").ToString
            Me.txtSalary.Text = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Salary").ToString)
            Me.empPic.ImageLocation = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmpPicture").ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPromotionType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPromotionType.SelectedIndexChanged
        Try
            If Me.cmbPromotionType.SelectedIndex = 0 Then
                Me.GroupBox2.Visible = False
                Me.Label7.Visible = False
                Me.cmbDepartment.Visible = False
                Me.Label8.Visible = False
                Me.cmbDivision.Visible = False
                Me.Label9.Visible = False
                Me.cmbDesignation.Visible = False
                Me.Label11.Visible = True
                Me.txtIncreament.Visible = True
            ElseIf Me.cmbPromotionType.SelectedIndex = 1 Then
                Me.GroupBox2.Visible = True
                Me.GroupBox2.Text = "Promotion"
                Me.Label7.Visible = True
                Me.cmbDepartment.Visible = True
                Me.Label8.Visible = True
                Me.cmbDivision.Visible = True
                Me.Label9.Visible = True
                Me.cmbDesignation.Visible = True
                Me.Label11.Visible = True
                Me.txtIncreament.Visible = True
                FillCombos("dept")
                Me.cmbDepartment.SelectedIndex = 0
                FillCombos("division")
                Me.cmbDivision.SelectedIndex = 0
                FillCombos("designation")
                Me.cmbDesignation.SelectedIndex = 0
            ElseIf Me.cmbPromotionType.SelectedIndex = 2 Then
                Me.GroupBox2.Visible = True
                Me.GroupBox2.Text = "Transfer"
                Me.Label7.Visible = True
                Me.cmbDepartment.Visible = True
                Me.Label8.Visible = False
                Me.cmbDivision.Visible = False
                Me.Label9.Visible = False
                Me.cmbDesignation.Visible = False
                Me.Label11.Visible = False
                Me.txtIncreament.Visible = False
                FillCombos("dept")
                Me.cmbDepartment.SelectedIndex = 0
                FillCombos("division")
                Me.cmbDivision.SelectedIndex = 0
                FillCombos("designation")
                Me.cmbDesignation.SelectedIndex = 0

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
                    ' If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    FillModel()
                    If Save() = True Then
                        ' msg_Information(str_informSave)
                        frmDefEmployee.ReSetControls()
                        FillCombos("emp")
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    FillModel1()
                    If Update1() = True Then
                        ' msg_Information(str_informUpdate)
                        frmDefEmployee.ReSetControls()
                        FillCombos("emp")
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage("An error occured while save the data " & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillModel1()
        Try

            If Me.cmbPromotionType.SelectedIndex = 0 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text) + Val(Me.txtIncreament.Text)
                empPro.Old_EmployeeId = old_eid
                empPro.Old_BasicSalary = old_salary
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

                ' Promotion
            ElseIf Me.cmbPromotionType.SelectedIndex = 1 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.OldDepartmentId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptId").ToString)
                empPro.OldDesignationId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationId").ToString)
                empPro.OldDivisionId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Id").ToString)
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text) + Val(Me.txtIncreament.Text)
                empPro.DepartmentId = Me.cmbDepartment.SelectedValue
                empPro.DesignationId = Me.cmbDesignation.SelectedValue
                empPro.DivisionId = Me.cmbDivision.SelectedValue
                empPro.Old_EmployeeId = old_eid
                empPro.Old_BasicSalary = old_salary
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

                ' Transfer
            ElseIf Me.cmbPromotionType.SelectedIndex = 2 Then
                empPro = New EmployeePromotionBE
                empPro.PromotionId = proId
                empPro.PromotionType = Me.cmbPromotionType.Text
                empPro.Ref_No = Me.txtDocNo.Text
                empPro.Ref_Date = Me.dtpDocDate.Value
                empPro.EmployeeId = Me.cmbEmployee.SelectedValue
                empPro.OldDepartmentId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptId").ToString)
                empPro.OldDesignationId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationId").ToString)
                empPro.OldDivisionId = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Id").ToString)
                empPro.BasicSalary = Val(Me.txtSalary.Text)
                'empPro.Increament_Salary = Val(Me.txtIncreament.Text)
                empPro.NewBasicSalary = Val(Me.txtSalary.Text)
                empPro.DepartmentId = Me.cmbDepartment.SelectedValue
                empPro.DesignationId = Me.cmbDesignation.SelectedValue
                empPro.DivisionId = Me.cmbDivision.SelectedValue
                empPro.Old_EmployeeId = old_eid
                empPro.Old_BasicSalary = old_salary
                empPro.EntryDate = Date.Now
                empPro.UserName = LoginUserName

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            proId = Me.grd.CurrentRow.Cells("PromotionId").Value
            If Me.grd.CurrentRow.Cells("PromotionType").Value.ToString = "Increment" Then
                Me.txtDocNo.Text = Me.grd.CurrentRow.Cells("Ref_No").Value.ToString
                Me.dtpDocDate.Value = Me.grd.CurrentRow.Cells("Ref_Date").Value.ToString
                Me.cmbEmployee.SelectedValue = Me.grd.CurrentRow.Cells("EmployeeId").Value
                Me.cmbPromotionType.Text = Me.grd.CurrentRow.Cells("PromotionType").Value.ToString
                'Me.txtDepartment.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDeptName").ToString
                'Me.txtDivision.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Division_Name").ToString
                'Me.txtDesignation.Text = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmployeeDesignationName").ToString
                Me.txtDepartment.Text = Me.grd.CurrentRow.Cells("OldDepartmentName").Value.ToString
                Me.txtDivision.Text = Me.grd.CurrentRow.Cells("OldDivisionName").Value.ToString
                Me.txtDesignation.Text = Me.grd.CurrentRow.Cells("OldDesignationName").Value.ToString
                'Me.txtSalary.Text = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Salary").ToString)
                Me.empPic.ImageLocation = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmpPicture").ToString
                Me.txtSalary.Text = Me.grd.CurrentRow.Cells("BasicSalary").Value
                Me.txtIncreament.Text = Me.grd.CurrentRow.Cells("Increament_Salary").Value
                old_eid = Me.grd.CurrentRow.Cells("EmployeeId").Value
                old_salary = Me.grd.CurrentRow.Cells("Basicsalary").Value

            ElseIf Me.grd.CurrentRow.Cells("PromotionType").Value.ToString = "Promotion" Then

                'Me.GroupBox2.Visible = True
                'Me.Label7.Visible = True
                'Me.cmbDepartment.Visible = True
                'Me.Label8.Visible = True
                'Me.cmbDivision.Visible = True
                'Me.Label9.Visible = True
                'Me.cmbDesignation.Visible = True
                'Me.Label11.Visible = True
                'Me.txtIncreament.Visible = True
                'FillCombos("dept")
                'FillCombos("division")
                'FillCombos("designation")

                Me.txtDocNo.Text = Me.grd.CurrentRow.Cells("Ref_No").Value.ToString
                Me.dtpDocDate.Value = Me.grd.CurrentRow.Cells("Ref_Date").Value.ToString
                Me.cmbEmployee.SelectedValue = Me.grd.CurrentRow.Cells("EmployeeId").Value
                Me.cmbPromotionType.Text = Me.grd.CurrentRow.Cells("PromotionType").Value.ToString
                'Old ID's
                Me.txtDepartment.Text = Me.grd.CurrentRow.Cells("OldDepartmentName").Value.ToString
                Me.txtDivision.Text = Me.grd.CurrentRow.Cells("OldDivisionName").Value.ToString
                Me.txtDesignation.Text = Me.grd.CurrentRow.Cells("OldDesignationName").Value.ToString
                'New ID's
                Me.cmbDepartment.SelectedValue = Me.grd.CurrentRow.Cells("DepartmentId").Value
                Me.cmbDivision.SelectedValue = Me.grd.CurrentRow.Cells("DivisionId").Value
                Me.cmbDesignation.SelectedValue = Me.grd.CurrentRow.Cells("DesignationId").Value

                Me.empPic.ImageLocation = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmpPicture").ToString
                Me.txtSalary.Text = Me.grd.CurrentRow.Cells("BasicSalary").Value
                Me.txtIncreament.Text = Me.grd.CurrentRow.Cells("Increament_Salary").Value
                old_eid = Me.grd.CurrentRow.Cells("EmployeeId").Value
                old_salary = Me.grd.CurrentRow.Cells("Basicsalary").Value

            ElseIf Me.grd.CurrentRow.Cells("PromotionType").Value.ToString = "Transfer" Then

                Me.txtDocNo.Text = Me.grd.CurrentRow.Cells("Ref_No").Value.ToString
                Me.dtpDocDate.Value = Me.grd.CurrentRow.Cells("Ref_Date").Value.ToString
                Me.cmbEmployee.SelectedValue = Me.grd.CurrentRow.Cells("EmployeeId").Value
                Me.cmbPromotionType.Text = Me.grd.CurrentRow.Cells("PromotionType").Value.ToString
                'Old ID's
                Me.txtDepartment.Text = Me.grd.CurrentRow.Cells("OldDepartmentName").Value.ToString
                Me.txtDivision.Text = Me.grd.CurrentRow.Cells("OldDivisionName").Value.ToString
                Me.txtDesignation.Text = Me.grd.CurrentRow.Cells("OldDesignationName").Value.ToString
                'New ID's
                Me.cmbDepartment.SelectedValue = Me.grd.CurrentRow.Cells("DepartmentId").Value
                Me.cmbDivision.SelectedValue = Me.grd.CurrentRow.Cells("DivisionId").Value
                Me.cmbDesignation.SelectedValue = Me.grd.CurrentRow.Cells("DesignationId").Value
                'Me.txtSalary.Text = Val(CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("Salary").ToString)
                Me.empPic.ImageLocation = CType(Me.cmbEmployee.SelectedItem, DataRowView).Item("EmpPicture").ToString
                Me.txtSalary.Text = Me.grd.CurrentRow.Cells("BasicSalary").Value
                'Me.txtIncreament.Text = Me.grd.CurrentRow.Cells("Increament_Salary").Value
                old_eid = Me.grd.CurrentRow.Cells("EmployeeId").Value
                old_salary = Me.grd.CurrentRow.Cells("Basicsalary").Value

            End If

            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading the record" & Chr(10) & ex.Message)
        End Try
    End Sub
    Public Function GetEmpData() As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_EmployeePromotionIncreament " & Val(Me.grd.GetRow.Cells("EmployeeId").Value.ToString) & ""
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)

            For Each r As DataRow In dtData.Rows
                If IO.File.Exists(r.Item("EmpPicture").ToString) Then
                    LoadPicture(r, "EmpImage", r.Item("EmpPicture").ToString)
                End If
            Next
            Return dtData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PromotionIncreamentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromotionIncreamentToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@EmpId", Me.grd.GetRow.Cells("EmployeeId").Value.ToString)
            ShowReport("rptEmployeeIncrement", , , , , , , GetEmpData)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PromotionLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromotionLetterToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@EmpId", Me.grd.GetRow.Cells("EmployeeId").Value.ToString)
            ShowReport("rptEmployeeIncrement", , , , , , , GetEmpData)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub btnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@EmpId", Me.grd.GetRow.Cells("EmployeeId").Value.ToString)
            ShowReport("rptEmpPromotion", , , , , , , GetEmpData)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub TransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransferToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@EmpId", Me.grd.GetRow.Cells("EmployeeId").Value.ToString)
            ShowReport("rptEmpPromotion", , , , , , , GetEmpData)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

  
End Class