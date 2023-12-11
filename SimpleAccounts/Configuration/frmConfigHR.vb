Public Class frmConfigHR

    Implements IGeneral
    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty
    Dim IsChangedValue As Object
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim strSQL As String = String.Empty
        Select Case Condition
            Case "SalaryAccount"
                strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                FillDropDown(Me.cmbSalaryAccount, strSQL, True)
            Case "SalaryPayableAccount"
                strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                FillDropDown(Me.cmbSalaryPayableAccount, strSQL, True)
        End Select
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

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




    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        If Not getConfigValueByType("AttendanceBasedSalary").ToString = "Error" Then
            Me.rdoAttendanceBaseSalary.Checked = Convert.ToBoolean(getConfigValueByType("AttendanceBasedSalary").ToString)
        End If
        If rdoAttendanceBaseSalary.Checked = False Then
            Me.rdoAttendanceBaseSalaryNo.Checked = True
        End If

        If Not getConfigValueByType("GrossSalaryCalcByFormula").ToString = "Error" Then
            Me.rdoGrossSalaryCalc.Checked = Convert.ToBoolean(getConfigValueByType("GrossSalaryCalcByFormula").ToString)
        End If
        If rdoGrossSalaryCalc.Checked = False Then
            Me.rdoGrossSalaryCalcNo.Checked = True
        End If


        Me.cmbSalaryAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesAccountId").ToString))
        Me.cmbSalaryPayableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesPayableAccountId").ToString))
        Me.txtGrossSalaryFormula.Text = getConfigValueByType("GrossSalaryFormula").ToString

        If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
            Me.rdoRightBasedCostCenters.Checked = Convert.ToBoolean(getConfigValueByType("RightBasedCostCenters").ToString)
        End If
        If rdoRightBasedCostCenters.Checked = False Then
            Me.RdoRightBasedCostCentersNot.Checked = True
        End If

        If Not getConfigValueByType("NewSalarySheetPrint").ToString = "Error" Then
            Me.rdoNewSalarySheetPrint.Checked = Convert.ToBoolean(getConfigValueByType("NewSalarySheetPrint").ToString)
        End If
        If rdoNewSalarySheetPrint.Checked = False Then
            Me.rdoNewSalarySheetPrintNot.Checked = True
        End If

    End Sub


    Private Sub cmbSalaryAccount_Leave(sender As Object, e As EventArgs) Handles cmbSalaryAccount.Leave, cmbSalaryPayableAccount.Leave
        Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
        frmConfigCompany.SaveConfiguration(cmb.Tag.ToString, cmb.SelectedValue)
    End Sub

    Private Sub cmbSalaryAccount_Enter(sender As Object, e As EventArgs) Handles cmbSalaryAccount.Enter, cmbSalaryPayableAccount.Enter
        Try
            Dim cmb As ComboBox = CType(sender, ComboBox)
            If cmb.Items.Count > 0 Then
                If cmb.SelectedValue IsNot Nothing Then IsChangedValue = cmb.SelectedValue Else IsChangedValue = -1
            Else
                IsChangedValue = -1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoAttendanceBaseSalary_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAttendanceBaseSalary.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub frmConfigHR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.isFormOpen = True

        getConfigValueList()
        Try
            FillCombos("SalaryAccount")
            FillCombos("SalaryPayableAccount")
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading" & ex.Message)
        End Try
        GetAllRecords()

    End Sub

    Private Sub rdoGrossSalaryCalc_CheckedChanged(sender As Object, e As EventArgs) Handles rdoGrossSalaryCalc.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtGrossSalaryFormula_Leave(sender As Object, e As EventArgs) Handles txtGrossSalaryFormula.Leave
        Try
            KeyType = "GrossSalaryFormula"
            KeyValue = Me.txtGrossSalaryFormula.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblAttendance_Click(sender As Object, e As EventArgs) Handles lblAttendance.Click
        Try
            If frmConfigHRAttendance.isFormOpen = True Then
                frmConfigHRAttendance.Dispose()
            End If

            frmConfigHRAttendance.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblHROvertime_Click(sender As Object, e As EventArgs) Handles lblHROvertime.Click
        Try
            If frmConfigHROvertime.isFormOpen = True Then
                frmConfigHROvertime.Dispose()
            End If

            frmConfigHROvertime.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblHRProvidentFund_Click(sender As Object, e As EventArgs) Handles lblHRProvidentFund.Click
        Try
            If frmConfigHRProvidentFund.isFormOpen = True Then
                frmConfigHRProvidentFund.Dispose()
            End If

            frmConfigHRProvidentFund.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblEmployeeAccountMapping_Click(sender As Object, e As EventArgs) Handles lblEmployeeAccountMapping.Click
        Try
            If frmConfigHREmployeeAccount.isFormOpen = True Then
                frmConfigHREmployeeAccount.Dispose()
            End If

            frmConfigHREmployeeAccount.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoRightBasedCostCenters_CheckedChanged(sender As Object, e As EventArgs) Handles rdoRightBasedCostCenters.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkNewSalarySheetPrintNot_CheckedChanged(sender As Object, e As EventArgs) Handles rdoNewSalarySheetPrint.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class