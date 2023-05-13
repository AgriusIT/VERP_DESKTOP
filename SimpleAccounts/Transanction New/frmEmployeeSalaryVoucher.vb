''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''15-9-2015 TASKM159151 Imran Ali Check Condition If Employee's Account Is Not Exists.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class frmEmployeeSalaryVoucher
    'Gross Salary Calculation By Formula
    ' 26-9-2013 by Imran Ali 
    '
    Implements IGeneral

    Dim SalariesExp As SalariesExpenseMaster
    Dim SalariesExpDetail As SalariesExpenseDetail
    Dim SalaryExpId As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Dim AttendanceBasedSalary As Boolean = False
    Dim GrossSalaryCalcByFormula As Boolean = False 'Declare Variable
    Dim GrossSalaryFormula As String = String.Empty 'Declare Variable
    Public urPK As New System.Globalization.CultureInfo("ur-PK")
    Public enUS As New System.Globalization.CultureInfo("en-US")
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If Not col.Caption = "Earning" AndAlso Not col.Caption = "Deduction" Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            If Condition = "grdSaved" Then
                Me.grdSaved.RootTable.Columns("SalaryExpId").Visible = False
                Me.grdSaved.RootTable.Columns("EmployeeId").Visible = False
                Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False
                Me.grdSaved.RootTable.Columns("EmpSalaryAccountId").Visible = False
                Me.grdSaved.RootTable.Columns("GrossSalary").Visible = False
                Me.grdSaved.RootTable.Columns("NetSalary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("NetSalary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("NetSalary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("NetSalary").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("NetSalary").TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("SalaryExpDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns.Add("Column1")
                Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
                Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
                Me.grdSaved.AutoSizeColumns()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnLoadAll.Enabled = True
                Me.btnAddSalaryType.Enabled = True
                Me.btnSearchHistory.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkPost.Visible = False
                Me.btnLoadAll.Enabled = False
                Me.btnAddSalaryType.Enabled = False
                Me.btnSearchHistory.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                    ElseIf RightsDt.FormControlName = "Load All" Then
                        Me.btnLoadAll.Enabled = True
                    ElseIf RightsDt.FormControlName = "Salary Type" Then
                        Me.btnAddSalaryType.Enabled = True
                    ElseIf RightsDt.FormControlName = "Search" Then
                        Me.btnSearchHistory.Enabled = True
                    End If
                Next
            Else
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            'If IsValidate() = True Then
            'Return New SalariesExpenseDAL().DeleteSalariesExp(SalariesExp)
            'Else
            'Return False
            'End If
            ' Return New SalariesExpenseDAL().DeleteSalariesExp(SalariesExp)
            'SalariesExp = New SalariesExpenseMaster
            'SalariesExp.SalaryExpId = SalaryExpId
            'Return New SalariesExpenseDAL().DeleteSalariesExp
            'New Code

            SalariesExp = New SalariesExpenseMaster
            SalariesExp.SalaryExpId = Me.grdSaved.GetRow.Cells("SalaryExpId").Value
            SalariesExp.SalaryExpNo = Me.grdSaved.GetRow.Cells("SalaryExpNo").Value.ToString
            SalariesExp.SalaryExpDate = Me.grdSaved.GetRow.Cells("SalaryExpDate").Value

            If New SalariesExpenseDAL().DeleteSalariesExp(SalariesExp) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Employees" Then
                str = "Select Employee_Id, Employee_Name,Father_Name,EmployeeDesignationName as Designation, EmployeeDeptName as Department,ShiftGroupName as Shift, IsNull(ShiftGroupId,0) as ShiftGroupId , IsNull(Salary,0) as Salary, IsNull(EmpSalaryAccountId,0) as EmpSalaryAccountId From EmployeesView WHERE Active=1"
                FillUltraDropDown(Me.cmbEmployees, str, True)
                Me.cmbEmployees.Rows(0).Activate()

                Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_Id").Hidden = True
                Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpSalaryAccountId").Hidden = True

            ElseIf Condition = "CostCenter" Then
                str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter"
                FillDropDown(Me.cmbCostCenter, str, True)
            ElseIf Condition = "SearchEmployeeByDesignation" Then
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & "  ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_Name").Width = 500
                End If
            ElseIf Condition = "SearchEmployeeByDepartment" Then
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & "  ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_Name").Width = 500
                End If
            ElseIf Condition = "SearchEmployeeByDepartmentAndDesignation" Then
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 And  tblDefEmployee.Dept_Id =" & Me.cmbSearchByDepartment.Value & " And tblDefEmployee.Desig_Id =" & Me.cmbSearchByDesignation.Value & " ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_Name").Width = 500
                End If
            ElseIf Condition = "SearchByDesignation" Then
                str = "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable"
                FillUltraDropDown(Me.cmbSearchByDesignation, str)
                Me.cmbSearchByDesignation.Rows(0).Activate()
                If Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationId").Hidden = True
                    Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationName").Width = 500
                End If
            ElseIf Condition = "SearchByDepartment" Then
                str = "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable"
                FillUltraDropDown(Me.cmbSearchByDepartment, str)
                Me.cmbSearchByDepartment.Rows(0).Activate()
                If Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptId").Hidden = True
                    Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptName").Width = 500
                End If
            ElseIf Condition = "SearchEmployee" Then
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbSearchByEmployee, str)
                Me.cmbSearchByEmployee.Rows(0).Activate()
                If Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                    Me.cmbSearchByEmployee.DisplayLayout.Bands(0).Columns("Employee_Name").Width = 500
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
            SalariesExp = New SalariesExpenseMaster
            SalariesExp.SalaryExpId = SalaryExpId
            SalariesExp.SalaryExpNo = Me.txtVoucherNo.Text
            SalariesExp.SalaryExpDate = Me.dtpVoucherDate.Value.Date
            SalariesExp.EmployeeId = Me.cmbEmployees.Value
            SalariesExp.CostCenterId = Me.cmbCostCenter.SelectedValue
            SalariesExp.Remarks = Me.txtRemarks.Text
            SalariesExp.NetSalary = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Earning"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Deduction"), Janus.Windows.GridEX.AggregateFunction.Sum)))
            SalariesExp.EmpSalaryPayableAccountId = cmbEmployees.ActiveRow.Cells("EmpSalaryAccountId").Value 'GetEmpSalaryId(Me.cmbEmployees.Value).ToString
            SalariesExp.SalaryAccountId = Convert.ToInt32(getConfigValueByType("SalariesAccountId").ToString)
            SalariesExp.Post = Me.chkPost.Checked
            SalariesExp.UserName = LoginUserName
            SalariesExp.FDate = Date.Now
            SalariesExp.SalariesExpenseDetail = New List(Of SalariesExpenseDetail)
            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If (Val(grdRow.Cells("Earning").Value.ToString) <> 0 Or Val(grdRow.Cells("Deduction").Value.ToString) <> 0) Then
                    If grdRow.Cells("Advance").Value = False Then 'TASKM159151 Imran Ali Check Condition If Employee's Account Is Not Exists.
                        If Val(grdRow.Cells("AccountId").Value.ToString) <= 0 Then
                            Throw New Exception(grdRow.Cells("SalaryExpType").Value.ToString & "'s account is not mapped")
                        End If
                    End If
                    SalariesExpDetail = New SalariesExpenseDetail
                    SalariesExpDetail.SalaryExpId = SalaryExpId
                    SalariesExpDetail.SalaryTypeId = grdRow.Cells("SalaryExpTypeId").Value
                    SalariesExpDetail.Earning = grdRow.Cells("Earning").Value
                    SalariesExpDetail.Deduction = grdRow.Cells("Deduction").Value
                    SalariesExpDetail.DeductionFlag = grdRow.Cells("SalaryDeduction").Value
                    SalariesExpDetail.Advance = grdRow.Cells("Advance").Value
                    SalariesExpDetail.AccountId = grdRow.Cells("AccountId").Value
                    SalariesExpDetail.SalaryType = grdRow.Cells("SalaryExpType").Value.ToString
                    SalariesExp.SalariesExpenseDetail.Add(SalariesExpDetail)

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = New SalariesExpenseDAL().GetAllRecord()
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings("grdSaved")
            CtrlGrdBar1_Load(Nothing, Nothing)
            'CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbEmployees.IsItemInList = False Then Return False : Exit Function
            If Me.cmbEmployees.ActiveRow Is Nothing Then
                ShowErrorMessage("Please select employee")
                Me.cmbEmployees.Focus()
                Return False
            End If

            If Me.cmbEmployees.ActiveRow.Cells(0).Value > 0 Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Dim dt As New DataTable
                    dt = GetDataTable("Select Count(*) From SalariesExpenseMasterTable  Where (Convert(Varchar,SalaryExpDate,102)=Convert(DateTime,'" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) And EmployeeId=" & Me.cmbEmployees.Value & "")
                    dt.AcceptChanges()
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                                ShowErrorMessage("Sorry you can't save salary voucher.")
                                Return False
                            End If
                        End If
                    End If
                End If
            End If


            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            IsEditMode = False
            Me.btnSave.Text = "&Save"
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Me.txtVoucherNo.Text = GetSerialNo("ES" + "-" + Microsoft.VisualBasic.Right(Me.dtpVoucherDate.Value.Year, 2) + "-", "SalariesExpenseMasterTable", "SalaryExpNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Me.txtVoucherNo.Text = GetNextDocNo("ES" & "-" & Format(Me.dtpVoucherDate.Value, "yy") & Me.dtpVoucherDate.Value.Month.ToString("00"), 4, "SalariesExpenseMasterTable", "SalaryExpNo")
            Else
                Me.txtVoucherNo.Text = GetNextDocNo("ES", 6, "SalariesExpenseMasterTable", "SalaryExpNo").ToString
            End If
            Me.dtpVoucherDate.Value = Date.Now
            Me.dtpVoucherDate.Enabled = True
            Me.cmbEmployees.Rows(0).Activate()
            Me.cmbCostCenter.SelectedIndex = 0
            Me.txtRemarks.Text = String.Empty
            Me.txtGrossSalary.Text = 0
            Me.txtNetSalary.Text = 0
            Me.chkPost.Checked = True
            'Me.GetAllRecords()
            ' Me.lnkLoadSalaryAccounts.Enabled = True
            If AttendanceBasedSalary = False Then
                Me.txtGrossSalary.Enabled = False
                Me.Button1.Enabled = False
            Else
                Me.txtGrossSalary.Enabled = True
                Me.Button1.Enabled = True
            End If
            Me.grd.DataSource = New SalariesExpenseDAL().GetRecordById(-1)
            ApplyGridSettings()
            ' Me.lnkLoadSalaryAccounts_LinkClicked(Nothing, Nothing)
            GetSecurityRights()
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnPrint.Visible = False
            Me.btnDelete.Visible = False
            Me.SplitContainer1.Panel1Collapsed = True

            If Not Me.cmbSearchByDepartment.ActiveRow Is Nothing Then Me.cmbSearchByDepartment.Rows(0).Activate()
            If Not Me.cmbSearchByDesignation.ActiveRow Is Nothing Then Me.cmbSearchByDesignation.Rows(0).Activate()
            If Not Me.cmbSearchByEmployee.ActiveRow Is Nothing Then Me.cmbSearchByEmployee.Rows(0).Activate()
            Me.SplitContainer1.Panel1Collapsed = True
            'Me.grdSaved.RootTable.Columns.Add("Column1").

            If Not Me.cmbSearchByDepartment.ActiveRow Is Nothing Then Me.cmbSearchByDepartment.Rows(0).Activate()
            If Not Me.cmbSearchByDesignation.ActiveRow Is Nothing Then Me.cmbSearchByDesignation.Rows(0).Activate()
            If Not Me.cmbSearchByEmployee.ActiveRow Is Nothing Then Me.cmbSearchByEmployee.Rows(0).Activate()
            'Me.grdSaved.RootTable.Columns.Add("Column1").

            CtrlGrdBar1_Load(Nothing, Nothing)
            'CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New SalariesExpenseDAL().AddSalariesExp(SalariesExp) = True Then
                Return True
            Else
                Return False
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
            If New SalariesExpenseDAL().UpdateSalariesExp(SalariesExp) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmEmployeeSalaryVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmEmployeeSalaryVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Not getConfigValueByType("AttendanceBasedSalary").ToString = "Error" Then
                AttendanceBasedSalary = getConfigValueByType("AttendanceBasedSalary")
            Else
                AttendanceBasedSalary = False
            End If

            If Not getConfigValueByType("GrossSalaryCalcByFormula").ToString = "Error" Then
                GrossSalaryCalcByFormula = getConfigValueByType("GrossSalaryCalcByFormula")
            Else
                GrossSalaryCalcByFormula = False
            End If

            If Not getConfigValueByType("GrossSalaryFormula").ToString = "Error" Then
                GrossSalaryFormula = getConfigValueByType("GrossSalaryFormula")
            Else
                GrossSalaryFormula = ""
            End If

            FillCombos("Employees")
            FillCombos("CostCenter")
            FillCombos("SearchByDesignation")
            FillCombos("SearchByEmployee")
            FillCombos("SearchByDepartment")
            Me.cmbLang.SelectedIndex = 0
            ReSetControls()
            Me.GetAllRecords()
            IsFormLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpVoucherDate.Enabled = False
                Else
                    Me.dtpVoucherDate.Enabled = True
                End If
            Else
                Me.dtpVoucherDate.Enabled = True
            End If

            IsEditMode = True
            SalaryExpId = Me.grdSaved.GetRow.Cells("SalaryExpId").Value
            Me.txtVoucherNo.Text = Me.grdSaved.GetRow.Cells("SalaryExpNo").Value.ToString
            Me.dtpVoucherDate.Value = Me.grdSaved.GetRow.Cells("SalaryExpDate").Value
            Me.cmbEmployees.Value = Me.grdSaved.GetRow.Cells("EmployeeId").Value.ToString
            Me.cmbCostCenter.SelectedValue = Me.grdSaved.GetRow.Cells("CostCenterId").Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Text.ToString
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("Post").Value) Then
                Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Post").Value
            Else
                Me.chkPost.Checked = False
            End If
            Me.txtGrossSalary.Text = Val(Me.grdSaved.GetRow.Cells("GrossSalary").Value.ToString)
            Me.btnSave.Text = "&Update"
            Me.grd.DataSource = New SalariesExpenseDAL().GetRecordById(SalaryExpId)

            'Code By Imran Ali
            ' 26-6-2013
            'GetEmpAttendanceSummary(Me.cmbEmployees.SelectedValue) 'Call Method of Attendance Summary
            GetEmpBalancesSummary(Me.cmbEmployees.Value)

            ApplyGridSettings()
            'Me.lnkLoadSalaryAccounts.Enabled = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.grd.UpdateData()
            Me.txtNetSalary.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Earning"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Deduction"), Janus.Windows.GridEX.AggregateFunction.Sum))
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnPrint.Visible = True
            Me.btnDelete.Visible = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            'CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If

            If Me.dtpVoucherDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpVoucherDate.Focus()
                Exit Sub
            End If

            If Not IsValidate() = True Then
                Exit Sub
            End If
            Me.grd.UpdateData()
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                'msg_Information(str_informSave)
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                'msg_Information(str_informUpdate)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If
            If Me.cmbEmployees.Value <= 0 Then
                ShowErrorMessage("Please selected record")
                Exit Sub
            End If
            'If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSaved.CurrentRow.Delete()
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnLoadAll.Visible = True
                GetAllRecords()
                Me.btnDelete.Visible = True
                Me.btnPrint.Visible = True
                Me.btnEdit.Visible = True
            Else
                Me.btnLoadAll.Visible = False
                If IsEditMode = False Then Me.btnDelete.Visible = False
                If IsEditMode = False Then Me.btnPrint.Visible = False
                Me.btnEdit.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetEmpSalaryId(ByVal empId As Integer) As Integer
        Try
            Dim str As String = String.Empty
            str = "Select EmpSalaryAccountId from tblDefEmployee WHERE Employee_Id=" & empId
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt Is Nothing Then Return False
            If Val(dt.Rows.Count) > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAddSalaryType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSalaryType.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ApplyStyleSheet(frmSalaryType)
            frmSalaryType.ShowDialog()
            If frmSalaryType.SalaryExpTypeId > 0 Then
                Dim dt As DataTable
                dt = CType(Me.grd.DataSource, DataTable)
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = frmSalaryType.SalaryExpTypeId
                dr(1) = frmSalaryType.SalaryExpType
                dr(2) = 0
                dr(3) = 0
                dr(4) = IIf(frmSalaryType.SalaryDeduction = True, 1, 0)
                dt.Rows.InsertAt(dr, 0)
                frmSalaryType.SalaryExpTypeId = 0
                dt.AcceptChanges()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@CurrentId", Me.grdSaved.GetRow.Cells("SalaryExpId").Value)
            ShowReport("rptEmployeeSalary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try

            Dim id As Integer = 0
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
            id = Me.cmbEmployees.ActiveRow.Cells(0).Value
            FillCombos("Employees")
            Me.cmbEmployees.Value = id



            If Not getConfigValueByType("AttendanceBasedSalary").ToString = "Error" Then
                AttendanceBasedSalary = getConfigValueByType("AttendanceBasedSalary")
            Else
                AttendanceBasedSalary = False
            End If


            If Not getConfigValueByType("GrossSalaryCalcByFormula").ToString = "Error" Then
                GrossSalaryCalcByFormula = getConfigValueByType("GrossSalaryCalcByFormula")
            Else
                GrossSalaryCalcByFormula = False
            End If

            If Not getConfigValueByType("GrossSalaryFormula").ToString = "Error" Then
                GrossSalaryFormula = getConfigValueByType("GrossSalaryFormula")
            Else
                GrossSalaryFormula = ""
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    'Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

    '    'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
    '    If e.KeyCode = Keys.F2 Then
    '        btnEdit_Click(Nothing, Nothing)
    '        Exit Sub
    '    End If

    '    If e.KeyCode = Keys.Delete Then
    '        btnDelete_Click(Nothing, Nothing)
    '        Exit Sub
    '    End If

    'End Sub
    'Private Sub grd_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyUp
    '    Try
    '        Me.grd.UpdateData()
    '        Me.txtNetSalary.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Earning"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Deduction"), Janus.Windows.GridEX.AggregateFunction.Sum))
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbEmployees_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmployees.ValueChanged
        Try
            If Me.cmbEmployees.ActiveRow Is Nothing Then Exit Sub
            chkEmpData(Me.cmbEmployees.Value)

            'Code By Imran Ali
            ' 26-6-2013
            'GetEmpAttendanceSummary(Me.cmbEmployees.SelectedValue) 'Call Method of Attendance Summary
            GetEmpBalancesSummary(Me.cmbEmployees.Value)

            'Dim dt As DataTable = New dailysalariesdal().GetEmployeeById(Me.cmbEmployees.SelectedValue)
            'If dt Is Nothing Then Exit Sub
            If Me.cmbEmployees.Value > 0 Then
                Me.txtBasicSalary.Text = Val(Me.cmbEmployees.ActiveRow.Cells("Salary").Value.ToString)
                Me.txtDepartment.Text = Me.cmbEmployees.ActiveRow.Cells("Department").Value.ToString 'dt.Rows(0).Item("EmployeeDeptName").ToString
                Me.txtDesignation.Text = Me.cmbEmployees.ActiveRow.Cells("Designation").Value.ToString 'dt.Rows(0).Item("EmployeeDesignationName").ToString
            Else
                Me.txtDepartment.Text = String.Empty
                Me.txtDesignation.Text = String.Empty
            End If

            If Me.cmbEmployees.Value > 0 Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Dim strSQL As String = "SELECT Isnull(dbo.SalaryExpenseType.SalaryDeduction,0) as SalaryDeduction, Isnull(dbo.SalaryExpenseType.flgAdvance,0) as flgAdvance, dbo.tblEmployeeAccounts.Account_Id, dbo.tblEmployeeAccounts.Type_Id, " _
                        & " ISNULL(dbo.tblEmployeeAccounts.Amount, 0) AS Amount " _
                        & " FROM dbo.tblEmployeeAccounts INNER JOIN " _
                        & " dbo.SalaryExpenseType ON dbo.tblEmployeeAccounts.Type_Id = dbo.SalaryExpenseType.SalaryExpTypeId WHERE tblEmployeeAccounts.Employee_Id=" & Me.cmbEmployees.Value & " AND IsNull(dbo.tblEmployeeAccounts.Amount,0) > 0 "

                    Dim dtData As New DataTable
                    dtData = GetDataTable(strSQL)
                    dtData.AcceptChanges()
                    Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
                    dtGrd.AcceptChanges()
                    If dtData.Rows.Count > 0 Then
                        Dim dr() As DataRow
                        If dtGrd IsNot Nothing Then
                            If dtGrd.Rows.Count Then
                                For Each r As DataRow In dtGrd.Rows
                                    dr = dtData.Select("Type_Id=" & r("SalaryExpTypeId") & "")
                                    If dr IsNot Nothing Then
                                        If dr.Length > 0 Then
                                            r.BeginEdit()
                                            If Convert.ToBoolean(dr(0).ItemArray(0)) = True Then
                                                r("Deduction") = dr(0).ItemArray(4)
                                            Else
                                                r("Earning") = dr(0).ItemArray(4)
                                            End If

                                            r.EndEdit()
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Else
                        dtGrd.AcceptChanges()
                        For Each r As DataRow In dtGrd.Rows
                            r.BeginEdit()
                            r("Deduction") = 0D
                            r("Earning") = 0D
                            r.EndEdit()
                        Next
                    End If
                End If
            Else
                'If grd.DataSource IsNot Nothing Then
                '    Dim grdData As DataTable = CType(Me.grd.DataSource, DataTable)
                '    grdData.Clear()
                'End If
            End If

            Dim SalaryAccountId As Integer = 0I
            If AttendanceBasedSalary = False Then
                If Me.grd.RowCount = 0 Then Exit Sub
                If Not getConfigValueByType("SalariesAccountId").ToString = "Error" Then
                    SalaryAccountId = getConfigValueByType("SalariesAccountId")
                End If
                Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
                For Each r As DataRow In dtData.Rows
                    If r("AccountId") = SalaryAccountId Then
                        If Not Val(Me.txtGrossSalaryCalc.Text) <> 0 Then
                            r.BeginEdit()
                            If r("SalaryDeduction") = True Then
                                r("Deduction") = Val(Me.txtBasicSalary.Text)
                            Else
                                r("Earning") = Val(Me.txtBasicSalary.Text)
                            End If
                            r.EndEdit()
                            Exit For
                        Else
                            r.BeginEdit()
                            If r("SalaryDeduction") = True Then
                                r("Deduction") = Val(Me.txtGrossSalaryCalc.Text)
                            Else
                                r("Earning") = Val(Me.txtGrossSalaryCalc.Text)
                            End If
                            r.EndEdit()
                            Exit For
                        End If
                    End If
                Next
            End If
            Me.grd.UpdateData()
            Me.txtNetSalary.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Earning"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Deduction"), Janus.Windows.GridEX.AggregateFunction.Sum))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FlgDeduction()
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.grd.UpdateData()
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                If r.Cells("SalaryDeduction").Value = False Then
                    r.Cells("Deduction").Value = 0
                    r.Cells("Earning").Value = r.Cells("Earning").Value
                Else
                    r.Cells("Deduction").Value = r.Cells("Deduction").Value
                    r.Cells("Earning").Value = 0
                End If
                r.EndEdit()
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            FlgDeduction()
            Me.grd.UpdateData()
            Me.txtNetSalary.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Earning"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Deduction"), Janus.Windows.GridEX.AggregateFunction.Sum))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub chkEmpData(ByVal EmployeeId As Integer)
        Try


            Dim strQry As String = String.Empty
            Dim RecId As Integer = 0I
            strQry = "Select Isnull(Salary,0) as Salary, Isnull(ReceiveableAccountId,0) as RecId " & IIf(GrossSalaryCalcByFormula = True, ", (Isnull(Salary,0) " & GrossSalaryFormula & ")", ",0") & " as GrossSalaryCalcByFormula From tblDefEmployee WHERE Employee_Id=" & EmployeeId & ""
            Dim dtEmp As New DataTable
            dtEmp = GetDataTable(strQry)
            If dtEmp IsNot Nothing Then
                If dtEmp.Rows.Count > 0 Then
                    Me.txtBasicSalary.Text = dtEmp.Rows(0).Item(0)
                    RecId = dtEmp.Rows(0).Item(1)
                    Me.txtGrossSalaryCalc.Text = dtEmp.Rows(0).Item(2)
                Else
                    Me.txtBasicSalary.Text = 0
                    RecId = 0
                    Me.txtGrossSalaryCalc.Text = 0
                End If
            Else
                Me.txtBasicSalary.Text = 0
                RecId = 0
                Me.txtGrossSalaryCalc.Text = 0
            End If


            strQry = String.Empty
            strQry = "Select Sum(IsNull(debit_amount,0)-Isnull(credit_amount,0)) as Rec From tblVoucherDetail WHERE coa_detail_id=" & RecId & ""
            Dim dtRec As New DataTable
            dtRec = GetDataTable(strQry)

            If dtRec IsNot Nothing Then
                If dtRec.Rows.Count > 0 Then
                    Me.txtRec.Text = dtRec.Rows(0).Item(0)
                Else
                    Me.txtRec.Text = 0
                End If
            Else
                Me.txtRec.Text = 0
            End If



        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetEmpBalancesSummary(ByVal EmpId As Integer)
        Try

            Dim strQuery As String = "SP_EmpBalances " & IIf(EmpId > 0, EmpId, -1) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.Columns.Add("Add", GetType(System.String))
            Me.grdEmpBalanceSummary.DataSource = dt
            Me.grdEmpBalanceSummary.RetrieveStructure()
            Me.grdEmpBalanceSummary.AutoSizeColumns()

            grdEmpBalanceSummary.RootTable.Columns("Add").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            grdEmpBalanceSummary.RootTable.Columns("Add").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            grdEmpBalanceSummary.RootTable.Columns("Add").ButtonText = "Add"

            grdEmpBalanceSummary.RootTable.Columns("coa_detail_id").Visible = False
            grdEmpBalanceSummary.RootTable.Columns("detail_code").Caption = "Code"
            grdEmpBalanceSummary.RootTable.Columns("detail_title").Caption = "Account"
            grdEmpBalanceSummary.RootTable.Columns("Balance").FormatString = "N"
            grdEmpBalanceSummary.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdEmpBalanceSummary.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdEmpBalanceSummary.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.cmbEmployees.IsItemInList = False Then Exit Sub
            If Me.cmbEmployees.ActiveRow.Cells(0).Value <= 0 Then Exit Sub
            Dim SalaryAccountId As Integer = 0I
            If Not getConfigValueByType("SalariesAccountId").ToString = "Error" Then
                SalaryAccountId = getConfigValueByType("SalariesAccountId")
            End If
            Dim frm As New frmEmpAttendanceSumm
            ApplyStyleSheet(frm)
            frm._EmpID = Me.cmbEmployees.Value
            If Not Val(Me.txtGrossSalaryCalc.Text) <> 0 Then
                frm._BasicSalary = Val(Me.txtBasicSalary.Text)
            Else
                frm._BasicSalary = Val(Me.txtGrossSalaryCalc.Text)
            End If
            frm.StartPosition = FormStartPosition.CenterScreen
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                Me.txtGrossSalary.Text = Math.Round(Val(frm.txtGrossSalary.Text), 0)
                Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)

                For Each r As DataRow In dt.Rows
                    If r("AccountId") = SalaryAccountId Then
                        r.BeginEdit()

                        If r("SalaryDeduction") = True Then
                            r("Deduction") = Val(Me.txtGrossSalary.Text)
                        Else
                            r("Earning") = Val(Me.txtGrossSalary.Text)
                        End If
                        r.EndEdit()
                        Exit For
                    End If
                Next
                'grd_KeyUp(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdEmpBalanceSummary_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdEmpBalanceSummary.ColumnButtonClick
        Try
            If e.Column.Key = "Add" Then
                Dim dt As New DataTable
                dt = GetDataTable("SELECT ISNULL(dbo.tblEmployeeAccounts.Amount, 0) AS Amount, dbo.tblEmployeeAccounts.Type_Id FROM  dbo.tblEmployeeAccounts INNER JOIN  dbo.SalaryExpenseType ON dbo.tblEmployeeAccounts.Type_Id = dbo.SalaryExpenseType.SalaryExpTypeId WHERE     (dbo.SalaryExpenseType.SalaryDeduction = 1) AND (dbo.SalaryExpenseType.flgAdvance = 1) AND tblEmployeeAccounts.flgReceivable=1 AND tblEmployeeAccounts.Employee_ID=" & Me.cmbEmployees.Value & " ")
                If dt IsNot Nothing Then
                    Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
                    For Each r As DataRow In dtData.Rows
                        If r("SalaryExpTypeId") = dt.Rows(0).Item("Type_Id") Then
                            r.BeginEdit()
                            r("Deduction") = Val(Me.grdEmpBalanceSummary.GetRow.Cells("Balance").Value.ToString)
                            r.EndEdit()
                            'grd_KeyUp(Nothing, Nothing)
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            Me.grdSaved.DataSource = New SalariesExpenseDAL().GetAllRecord("ALL")
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings("grdSaved")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SalarySlipToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalarySlipToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@CurrentId", Me.grdSaved.GetRow.Cells("SalaryExpId").Value)
            ShowReport("rptEmployeeSalarySlip", , , , , , , getData)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function getData() As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_Employee_Salary " & Me.grdSaved.GetRow.Cells("SalaryExpId").Value & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            For Each r As DataRow In dt.Rows
                If IO.File.Exists(r.Item("EmpPicture").ToString) Then
                    LoadPicture(r, "EmpImage", r.Item("EmpPicture").ToString)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbLang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLang.SelectedIndexChanged
        Try
            If Me.cmbLang.Text = "Urdu" Then
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(urPK)
                Me.txtRemarks.TextAlign = HorizontalAlignment.Right
            ElseIf Me.cmbLang.Text = "English" Then
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(enUS)
                Me.txtRemarks.TextAlign = HorizontalAlignment.Left
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdEmpBalanceSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdEmpBalanceSummary.KeyDown

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub grd_UpdatingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles grd.UpdatingCell
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnConfiguration_Click(sender As Object, e As EventArgs) Handles btnConfiguration.Click
        Try
            ApplyStyleSheet(frmSalaryConfig)
            frmSalaryConfig.ShowDialog()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnHideSearchBy_Click(sender As Object, e As EventArgs) Handles btnHideSearchBy.Click
        Try
            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
            'If Me.gbSearchBy.Visible = True Then
            '    Me.gbSearchBy.Visible = False
            '    'Me.grdSaved.Size = New System.Drawing.Size(1026, 471)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSearchHistory_Click(sender As Object, e As EventArgs) Handles btnSearchHistory.Click
        Try

            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
            'If Me.gbSearchBy.Visible = False Then
            '    Me.gbSearchBy.Visible = True
            '    'Me.grdSaved.Size = New System.Drawing.Size(1026, 321)
            'Else
            '    Me.gbSearchBy.Visible = False
            '    'Me.grdSaved.Size = New System.Drawing.Size(1026, 471)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Me.grdSaved.DataSource = New SalariesExpenseDAL().GetFilterWise(Me.dtpFrom.Value, Me.dtpTo.Value, Me.cmbSearchByEmployee.Value, Me.cmbSearchByDepartment.Value, Me.cmbSearchByDesignation.Value, Me.dtpFrom.Checked, Me.dtpTo.Checked)
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings("grdSaved")
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

    Private Sub PrintSelectedRowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSelectedRowsToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetCrystalReportRights()
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                If Me.grdSaved.RowCount = 0 Then Exit Sub
                'AddRptParam("@CurrentId", Me.grdSaved.GetRow.Cells("SalaryExpId").Value)
                AddRptParam("@CurrentId", Val(r.Cells("SalaryExpId").Value.ToString))
                ShowReport("rptEmployeeSalary", , , , True)
                'ShowReport("rptEmployeeSalary")
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Salary"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs)
    '    Try
    '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
    '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '            Me.grdSaved.LoadLayoutFile(fs)
    '            fs.Close()
    '            fs.Dispose()
    '        End If
    '        Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "EmployeeSalaryVoucher"
    '        'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub CtrlGrdBar2_Load_1(sender As Object, e As EventArgs)
    '    Try
    '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
    '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '            Me.grdSaved.LoadLayoutFile(fs)
    '            fs.Close()
    '            fs.Dispose()
    '        End If
    '        Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "EmployeeSalaryVoucher"
    '        'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
End Class