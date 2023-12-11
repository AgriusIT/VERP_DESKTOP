Public Class frmConfigHROvertime

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

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

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
        Me.nudDefaultWorkingHours.Value = Val(getConfigValueByType("DefaultWorkingHours").ToString)
        Me.txtOTWorkingDays.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeWorkingDays").ToString))
        Me.txtOTSFPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeSalaryFactorPercentage").ToString))
        Me.txtOTNormalMultiplier.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString))
        Me.txtOTOffMultiplier.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString))




        If Not getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString = "Error" Then
            Me.rdoApplyDefaultWorkingHoursOnOverTime.Checked = Convert.ToBoolean(getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString)
        End If
        If rdoApplyDefaultWorkingHoursOnOverTime.Checked = False Then
            Me.rdoApplyDefaultWorkingHoursOnOverTimeNo.Checked = True
        End If


        If Not getConfigValueByType("SalaryGenerationPercentageBased").ToString = "Error" Then
            Me.rdoSalaryPercentage.Checked = Convert.ToBoolean(getConfigValueByType("SalaryGenerationPercentageBased").ToString)
        End If
        If rdoSalaryPercentage.Checked = False Then
            Me.rdoSalaryPercentageNo.Checked = True
        End If

        If Not getConfigValueByType("OverTimeBasedOnWorkingDays").ToString = "Error" Then
            Me.rdoOTBasedOnWorkingDays.Checked = Convert.ToBoolean(getConfigValueByType("OverTimeBasedOnWorkingDays").ToString)
        End If
        If rdoOTBasedOnWorkingDays.Checked = False Then
            Me.rdoOTBasedOnWorkingDaysNo.Checked = True
        End If


    End Sub

    Private Sub nudDefaultWorkingHours_ValueChanged(sender As Object, e As EventArgs) Handles nudDefaultWorkingHours.ValueChanged
        Try
            If isFormOpen = False Then Exit Sub
            Dim nud As NumericUpDown = CType(sender, NumericUpDown)
            frmConfigCompany.SaveConfiguration(nud.Tag, nud.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigHROvertime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub txtOTWorkingDays_Leave(sender As Object, e As EventArgs) Handles txtOTWorkingDays.Leave
        Try
            KeyType = "OverTimeWorkingDays"
            KeyValue = Me.txtOTWorkingDays.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtOTSFPercentage_Leave(sender As Object, e As EventArgs) Handles txtOTSFPercentage.Leave
        Try
            KeyType = "OverTimeSalaryFactorPercentage"
            KeyValue = Me.txtOTSFPercentage.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtOTNormalMultiplier_Leave(sender As Object, e As EventArgs) Handles txtOTNormalMultiplier.Leave
        Try
            KeyType = "OverTimeNormalDayMultiplier"
            KeyValue = Me.txtOTNormalMultiplier.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtOTOffMultiplier_Leave(sender As Object, e As EventArgs) Handles txtOTOffMultiplier.Leave
        Try
            KeyType = "OverTimeOffDayMultiplier"
            KeyValue = Me.txtOTOffMultiplier.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoApplyDefaultWorkingHoursOnOverTime_CheckedChanged(sender As Object, e As EventArgs) Handles rdoApplyDefaultWorkingHoursOnOverTime.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoSalaryPercentage_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSalaryPercentage.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoOTBasedOnWorkingDays_CheckedChanged(sender As Object, e As EventArgs) Handles rdoOTBasedOnWorkingDays.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Private Sub lblSalary_Click(sender As Object, e As EventArgs) Handles lblSalary.Click
        Try
            If frmConfigHR.isFormOpen = True Then
                frmConfigHR.Dispose()
            End If

            frmConfigHR.ShowDialog()
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

    Private Sub lblProvidentFund_Click(sender As Object, e As EventArgs) Handles lblProvidentFund.Click
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

End Class