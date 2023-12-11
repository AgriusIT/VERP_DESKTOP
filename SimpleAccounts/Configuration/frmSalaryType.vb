''TaskM126121 12/6/2015 Imran Ali Config Based Deduction Against Salary 
'2015-08-06 Task# 201508004 Ali Ansari Add Income Tax Excemption and Income Tax Deduction
''21-9-2015 Task:219151 Imran Ali: Enhancement Apply Value And Existing Account.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data
Public Class frmSalaryType
    Implements IGeneral
    Dim MasterId As Integer
    Dim SalaryType As SalaryType
    Public SalaryExpTypeId As Integer = 0
    Public SalaryExpType As String = String.Empty
    Public SalaryDeduction As Boolean = False
    Public EmpSalaryAccountId As Integer = 0
    Public IsOpenForm As Boolean = False


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.AutoSizeColumns()
            grdSaved.RootTable.Columns(0).Visible = False
            grdSaved.RootTable.Columns("AccountId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Return New SalaryTypeDAL().Delete(SalaryType)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "Account Head" Then
                FillDropDown(Me.cmbAccountHead, "Select DISTINCT main_sub_sub_Id, sub_sub_title, sub_sub_code From vwCOADetail WHERE sub_sub_title <> '' ORDER BY sub_sub_title ASC")
            ElseIf Condition = "Account Detail" Then
                FillDropDown(Me.cmbAccountHead, "Select coa_detail_id,detail_title as [Account Title], detail_code as [Account Code], main_sub_sub_id,sub_sub_code From vwCOADetail WHERE detail_title <> '' Order by detail_title ASC")
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            SalaryType = New SalaryType
            SalaryType.SalaryExpTypeId = MasterId
            SalaryType.SalaryExpType = Me.txtSalaryType.Text
            SalaryType.Deduction = Me.chkDeduction.Checked
            SalaryType.Active = Me.chkActive.Checked
            SalaryType.SortOrder = Val(Me.txtSortOrder.Text)
            SalaryType.AccountId = IIf(Me.chkReceiveableAccount.Checked = True, 0, IIf(Me.rbtExistingAccount.Checked = False, EmpSalaryAccountId, Me.cmbAccountHead.SelectedValue))
            SalaryType.Advance = Me.chkReceiveableAccount.Checked
            SalaryType.main_sub_sub_id = Me.cmbAccountHead.SelectedValue
            SalaryType.sub_sub_code = CType(Me.cmbAccountHead.SelectedItem, DataRowView).Row.Item("sub_sub_code").ToString
            SalaryType.GrossSalaryType = optGrossSalaryType.Checked
            SalaryType.DeductionAgainstLeaves = Me.OptDeductionAgaistLeaves.Checked   'Me.chkDeductionAgaistLeaves.Checked ''Task #201507004 Set Deduction Against Leaves False/True In Property
            SalaryType.AllowanceAgainstOT = Me.OptAllowanceAgainstOverTime.Checked  'Me.chkAllowanceAgainstOverTime.Checked ''Task #201507004 Set Allowance Against OverTime False/True In Property
            SalaryType.IncomeTaxDeduction = Me.OptIncomeTaxDed.Checked 'Me.ChkIncomeTaxDed.Checked 'Task #201508004 Set Income Tax Expenses
            SalaryType.IncomeTaxExcemption = Me.ChkIncomeTaxExempted.Checked 'Me.ChkIncomeTaxExempted.Checked 'Task #201508004 Set Income Tax Exemption
            SalaryType.DeductionAgainstSalary = Me.OptDeductionAgaistSalary.Checked
            SalaryType.SiteVisitAllowance = Me.optSiteVisitAllowance.Checked
            SalaryType.ApplyValue = Me.cmbApplyValue.Text
            SalaryType.ExistingAccount = IIf(Me.rbtExistingAccount.Checked = True, 1, 0) 'TASK219151 Set Existing Account Value.
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = New SalaryTypeDAL().GetAllRecord
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("GrossSalaryType").Value.ToString = "True" Then
                    Me.optGrossSalaryType.Enabled = False
                    Exit For
                Else
                    Me.optGrossSalaryType.Enabled = True
                End If
            Next
            'TASKM126151 Disabled/Enabled Deduction Against Salary Check
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("DeductionAgainstIncomeTax").Value.ToString = "True" Then
                    'Me.chkDeductionAgaistSalary.Enabled = False
                    Me.OptDeductionAgaistSalary.Checked = False
                    Exit For
                Else
                    Me.OptDeductionAgaistSalary.Checked = True
                End If
            Next
            'TASK #201507004  Disabled/Enabled Deduction Against Leave Check
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("DeductionAgainstLeaves").Value.ToString = "True" Then
                    'Me.chkDeductionAgaistLeaves.Enabled = False
                    Me.OptDeductionAgaistLeaves.Enabled = False
                    Exit For
                Else
                    'Me.chkDeductionAgaistLeaves.Enabled = True
                    Me.OptDeductionAgaistLeaves.Enabled = True
                End If
            Next
            'End Task

            'TASK #201507004  Disabled/Enabled Allowance Against OT Check
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("AllowanceAgainstOT").Value.ToString = "True" Then
                    'Me.chkAllowanceAgainstOverTime.Enabled = False
                    Me.OptAllowanceAgainstOverTime.Enabled = False
                    Exit For
                Else
                    Me.OptAllowanceAgainstOverTime.Enabled = True
                    'Me.chkAllowanceAgainstOverTime.Enabled = True
                End If
            Next
            'End Task

            'TASK #201508004  Disabled/Enabled IncomeTaxDeduction
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("DeductionAgainstIncomeTax").Value.ToString = "True" Then
                    '                    Me.ChkIncomeTaxDed.Enabled = False
                    Me.OptIncomeTaxDed.Enabled = False
                    Exit For
                Else
                    Me.OptIncomeTaxDed.Enabled = True
                    'Me.ChkIncomeTaxDed.Enabled = True
                End If
            Next
            'TASK #201508004  Disabled/Enabled IncomeTaxExcemption
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If r.Cells("IncomeTaxExempted").Value.ToString = "True" Then
                    Me.ChkIncomeTaxExempted.Enabled = False
                    Exit For
                Else
                    Me.ChkIncomeTaxExempted.Enabled = True
                End If
            Next
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtSalaryType.Text = String.Empty Then
                ShowErrorMessage("Salary type must enter.")
                Me.txtSalaryType.Focus()
                Return False
            End If
            If Me.chkReceiveableAccount.Checked = False Then
                If Me.cmbAccountHead.SelectedIndex <= 0 Then
                    ShowErrorMessage("Please select a account head.")
                    Me.cmbAccountHead.Focus()
                    Return False
                End If
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''
            Dim a As Integer
            If Me.btnSave.Text = "Update" Or Me.btnSave.Text = "&Update" Then
                For a = 1 To grdSaved.RowCount - 1
                    If Convert.ToBoolean(grdSaved.GetRow(a).Cells("GrossSalaryType").Value.ToString) = True And Convert.ToInt32(grdSaved.GetRow(a).Cells("SalaryExpTypeId").Value.ToString) <> MasterId And OptGrossSalaryType.Checked = True Then
                        ShowErrorMessage("Gross salary type can only be define once")
                        Return False
                    End If
                Next
            Else


                For a = 1 To grdSaved.RowCount - 1
                    If Convert.ToBoolean(grdSaved.GetRow(a).Cells("GrossSalaryType").Value.ToString) = True And OptGrossSalaryType.Checked = True Then
                        ShowErrorMessage("Gross salary type can only be define once")
                        Return False
                    End If
                Next
            End If
            '''''''''''''''''''''''''''''''''''''''''''''
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            Me.txtSalaryType.Text = String.Empty
            Me.chkDeduction.Checked = False
            'FillDropDown(Me.cmbAccountHead, "Select DISTINCT main_sub_sub_Id, sub_sub_title, sub_sub_code From vwCOADetail WHERE sub_sub_title <> '' ORDER BY sub_sub_title ASC")
            GetAllRecords()
            'GetSecurityRights()
            EmpSalaryAccountId = 0
            Me.cmbApplyValue.SelectedIndex = 0
            Me.rbtnNone.Checked = True
            Me.rbtCreateAccount.Checked = True
            Me.txtAccountDescription.Text = String.Empty
            FillCombos("Account Head")
            If Not Me.cmbAccountHead.SelectedIndex = -1 Then Me.cmbAccountHead.SelectedIndex = 0
            CheckBox2_CheckedChanged(Nothing, Nothing)
            Me.chkReceiveableAccount.Enabled = True
            Me.cmbAccountHead.Enabled = True
            'Me.OptDeductionAgaistLeaves.Enabled = True
            'Me.OptDeductionAgaistSalary.Enabled = True
            'Me.OptIncomeTaxDed.Enabled = True
            'Me.OptAllowanceAgainstOverTime.Enabled = True
            'Me.OptGrossSalaryType.Enabled = True
            Me.ChkIncomeTaxExempted.Enabled = True
            CheckOneTimeOptionApplied()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            MasterId = New SalaryTypeDAL().AddSalaryType(SalaryType)
            Return True
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
            Return New SalaryTypeDAL().UpdateSalaryType(SalaryType)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmSalaryType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
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
    Private Sub frmSalaryType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            MasterId = Me.grdSaved.GetRow.Cells("SalaryExpTypeId").Value
            Me.txtSalaryType.Text = Me.grdSaved.GetRow.Cells("SalaryExpType").Text
            If Me.grdSaved.GetRow.Cells("ExistingAccount").Value = False Then
                Me.rbtExistingAccount.Checked = False
                Me.rbtCreateAccount.Checked = True
                Me.cmbAccountHead.SelectedValue = Val(Me.grdSaved.GetRow.Cells("main_sub_sub_id").Value.ToString)
            Else
                Me.rbtExistingAccount.Checked = True
                Me.rbtCreateAccount.Checked = False
                Me.cmbAccountHead.SelectedValue = Val(Me.grdSaved.GetRow.Cells("AccountId").Value.ToString)
            End If
            Me.chkDeduction.Checked = Me.grdSaved.GetRow.Cells("SalaryDeduction").Value
            'Me.chkDeductionAgaistLeaves.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Value
            'Me.OptDeductionAgaistLeaves.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Value
            'Me.OptAllowanceAgainstOverTime.Checked = Me.grdSaved.GetRow.Cells("AllowanceAgainstOT").Value
            Me.chkActive.Checked = Me.grdSaved.GetRow.Cells("Active").Value
            Me.txtSortOrder.Text = Me.grdSaved.GetRow.Cells("SortOrder").Text
            EmpSalaryAccountId = Val(Me.grdSaved.GetRow.Cells("AccountId").Text.ToString)
            Me.txtAccountDescription.Text = Me.grdSaved.GetRow.Cells("detail_title").Text.ToString
            Me.chkReceiveableAccount.Checked = Me.grdSaved.GetRow.Cells("Advance").Text.ToString
            Me.OptDeductionAgaistLeaves.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Text.ToString
            Me.OptIncomeTaxDed.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstIncomeTax").Text.ToString
            Me.OptAllowanceAgainstOverTime.Checked = Me.grdSaved.GetRow.Cells("AllowanceAgainstOT").Text.ToString
            Me.OptGrossSalaryType.Checked = Me.grdSaved.GetRow.Cells("GrossSalaryType").Text.ToString
            Me.OptDeductionAgaistSalary.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstSalary").Text.ToString
            Me.ChkIncomeTaxExempted.Checked = Me.grdSaved.GetRow.Cells("IncomeTaxExempted").Text.ToString
            Me.optSiteVisitAllowance.Checked = Me.grdSaved.GetRow.Cells("SiteVisitAllowance").Text.ToString
            Me.cmbApplyValue.Text = Me.grdSaved.GetRow.Cells("ApplyValue").Text.ToString
            'Dim flg As Boolean = Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("GrossSalaryType"), Janus.Windows.GridEX.ConditionOperator.Equal, True, 0, 1)
            'If flg = True Then
            '    Me.optGrossSalaryType.Enabled = True
            'End If

            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("GrossSalaryType").Value) Then
            '    Me.optGrossSalaryType.Checked = Me.grdSaved.GetRow.Cells("GrossSalaryType").Value
            '    If Me.grdSaved.GetRow.Cells("GrossSalaryType").Value = True Then
            '        Me.OptGrossSalaryType.Enabled = True
            '    End If
            'Else
            '    Me.OptGrossSalaryType.Checked = False
            '    'Me.OptDeductionAgaistLeaves.Enabled = False
            '    'Me.OptIncomeTaxDed.Enabled = False
            '    'Me.ChkIncomeTaxExempted.Enabled = True

            'End If

            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("DeductionAgainstSalary").Value) Then
            '    Me.OptDeductionAgaistSalary.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstSalary").Value
            '    If Me.grdSaved.GetRow.Cells("DeductionAgainstSalary").Value = True Then
            '        Me.OptDeductionAgaistSalary.Enabled = True
            '    End If
            'Else
            '    Me.OptDeductionAgaistSalary.Checked = False
            '    ' Me.OptDeductionAgaistLeaves.Enabled = True
            '    ' Me.OptIncomeTaxDed.Enabled = True
            '    ' Me.ChkIncomeTaxExempted.Enabled = False
            'End If
            ''Altered Against Task#20150704 Check box value of Deduction on Leaves on grid
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Value) Then
            '    Me.OptDeductionAgaistSalary.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Value
            '    If Me.grdSaved.GetRow.Cells("DeductionAgainstLeaves").Value = True Then
            '        Me.OptDeductionAgaistLeaves.Enabled = True
            '        '    Me.OptIncomeTaxDed.Enabled = False
            '        '    Me.OptIncomeTaxDed.Checked = False

            '    End If
            'Else
            '    Me.OptDeductionAgaistLeaves.Checked = False
            'End If
            ''Altered Against Task#20150704 Check box value of Deduction on Leaves on grid
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("AllowanceAgainstOT").Value) Then
            '    Me.OptAllowanceAgainstOverTime.Checked = Me.grdSaved.GetRow.Cells("AllowanceAgainstOT").Value
            '    If Me.grdSaved.GetRow.Cells("AllowanceAgainstOT").Value = True Then
            '        Me.OptAllowanceAgainstOverTime.Checked = True
            '    End If
            'Else
            '    Me.OptAllowanceAgainstOverTime.Checked = False
            'End If

            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("DeductionAgainstIncomeTax").Value) Then
            '    Me.OptAllowanceAgainstOverTime.Checked = Me.grdSaved.GetRow.Cells("DeductionAgainstIncomeTax").Value
            '    If Me.grdSaved.GetRow.Cells("DeductionAgainstIncomeTax").Value = True Then

            '        Me.OptIncomeTaxDed.Checked = True
            '    End If
            'Else
            '    Me.OptIncomeTaxDed.Checked = False

            'End If

            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("IncomeTaxExempted").Value) Then
            '    Me.OptAllowanceAgainstOverTime.Checked = Me.grdSaved.GetRow.Cells("IncomeTaxExempted").Value
            '    If Me.grdSaved.GetRow.Cells("IncomeTaxExempted").Value = True Then
            '        Me.OptIncomeTaxDed.Enabled = True
            '    End If
            'Else
            '    Me.ChkIncomeTaxExempted.Checked = False
            'End If

            'Altered Against Task#20150704 Check box value of Deduction on Leaves on grid

            Me.btnSave.Text = "&Update"
            'Me.cmbAccountHead.Enabled = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If Not IsValidate() = True Then Exit Sub
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                SalaryExpTypeId = MasterId
                SalaryExpType = Me.txtSalaryType.Text
                SalaryDeduction = Me.chkActive.Checked
                'msg_Information(str_informSave)
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informUpdate)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GetSecurityRights()
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
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            ApplyStyleSheet(frmAddAccount)
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    EmpSalaryAccountId = frmAddAccount.AccountId
                    Me.txtAccountDescription.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReceiveableAccount.CheckedChanged
        Try
            If Me.chkReceiveableAccount.Checked = True Then
                'Me.btnAdd.Enabled = False
                If Not Me.cmbAccountHead.SelectedIndex = -1 Then Me.cmbAccountHead.SelectedIndex = 0
                Me.cmbAccountHead.Enabled = False
            Else
                'If Not Me.cmbAccountHead.SelectedIndex = -1 Then Me.cmbAccountHead.SelectedIndex = 0
                Me.cmbAccountHead.Enabled = True
                'Me.btnAdd.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14


        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub


    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Me.grdSaved_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSortOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSortOrder.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub chkDeduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDeduction.CheckedChanged

    End Sub

    Private Sub OptIncomeTaxDed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptIncomeTaxDed.CheckedChanged, OptDeductionAgaistLeaves.CheckedChanged, OptDeductionAgaistSalary.CheckedChanged
        Try
            If OptIncomeTaxDed.Checked = True Or OptDeductionAgaistSalary.Checked = True Or OptDeductionAgaistLeaves.Checked = True Then
                ChkIncomeTaxExempted.Checked = False
                ChkIncomeTaxExempted.Enabled = False
            Else
                ChkIncomeTaxExempted.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CheckOneTimeOptionApplied()
        Try

            Me.grdSaved.UpdateData()


            Me.OptDeductionAgaistSalary.Enabled = True
            Me.OptGrossSalaryType.Enabled = True
            Me.OptAllowanceAgainstOverTime.Enabled = True
            Me.OptDeductionAgaistLeaves.Enabled = True
            Me.optSiteVisitAllowance.Enabled = True

            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                If jRow.Cells("DeductionAgainstLeaves").Value = True Then
                    Me.OptDeductionAgaistLeaves.Enabled = False
                ElseIf jRow.Cells("DeductionAgainstIncomeTax").Value = True Then
                    Me.OptIncomeTaxDed.Enabled = False
                ElseIf jRow.Cells("AllowanceAgainstOT").Value = True Then
                    Me.OptAllowanceAgainstOverTime.Enabled = False
                ElseIf jRow.Cells("GrossSalaryType").Value = True Then
                    Me.OptGrossSalaryType.Enabled = False
                ElseIf jRow.Cells("DeductionAgainstSalary").Value = True Then
                    Me.OptDeductionAgaistSalary.Enabled = False
                ElseIf jRow.Cells("SiteVisitAllowance").Value = True Then
                    Me.optSiteVisitAllowance.Enabled = False
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbtCreateAccount_CheckedChanged(sender As Object, e As EventArgs) Handles rbtCreateAccount.CheckedChanged
        Try
            If IsOpenForm = True Then FillCombos("Account Head")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtExistingAccount_CheckedChanged(sender As Object, e As EventArgs) Handles rbtExistingAccount.CheckedChanged
        Try
            If IsOpenForm = True Then FillCombos("Account Detail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class