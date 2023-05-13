Public Class frmConfigHRProvidentFund


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

        Try
            Me.txtProvidentSFPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("SalaryFactorPercentage").ToString))
            Me.txtProvidentPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("ProvidentFactorPercentage").ToString))

            If Not getConfigValueByType("AfterAbsentDeduction").ToString = "Error" Then
                Me.rdoAfterAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAbsentDeduction").ToString)
            End If
            If rdoAfterAbsentDeduction.Checked = False Then
                Me.rdoAfterAbsentDeductionNo.Checked = True
            End If


            If Not getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString = "Error" Then
                Me.rdoAfterAllowancesAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString)
            End If
            If rdoAfterAllowancesAbsentDeduction.Checked = False Then
                Me.rdoAfterAllowancesAbsentDeductionNo.Checked = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub txtProvidentSFPercentage_Leave(sender As Object, e As EventArgs) Handles txtProvidentSFPercentage.Leave
        Try
            KeyType = "SalaryFactorPercentage"
            KeyValue = Me.txtProvidentSFPercentage.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmConfigHRProvidentFund_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub txtProvidentPercentage_Leave(sender As Object, e As EventArgs) Handles txtProvidentPercentage.Leave
        Try
            KeyType = "ProvidentFactorPercentage"
            KeyValue = Me.txtProvidentPercentage.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoAfterAbsentDeduction_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAfterAbsentDeduction.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoAfterAllowancesAbsentDeduction_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAfterAllowancesAbsentDeduction.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
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

    Private Sub lblOverTime_Click(sender As Object, e As EventArgs) Handles lblOverTime.Click
        Try
            If frmConfigHROvertime.isFormOpen = True Then
                frmConfigHROvertime.Dispose()
            End If

            frmConfigHROvertime.ShowDialog()
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

End Class