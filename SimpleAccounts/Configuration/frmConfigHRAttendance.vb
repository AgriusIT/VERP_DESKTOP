Public Class frmConfigHRAttendance

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
            Me.txtWorkingDays.Text = Convert.ToInt32(Val(getConfigValueByType("Working_Days").ToString))
            Me.txtAttendanceDbPath.Text = getConfigValueByType("AlternateAttendanceDBPath").ToString
            Me.TxtTotalLeaves.Text = Val(getConfigValueByType("Leave_Days").ToString)


            If Not getConfigValueByType("AutoBreakAttendance").ToString = "Error" Then
                Me.rdoAutoBreakAttendance.Checked = Convert.ToBoolean(getConfigValueByType("AutoBreakAttendance").ToString)
            End If
            If rdoAutoBreakAttendance.Checked = False Then
                Me.rdoAutoBreakAttendanceNo.Checked = True
            End If

            If Not getConfigValueByType("EnabledAttendanceEmailAlert").ToString = "Error" Then
                Me.rdoAttendaacnceEmailAlert.Checked = Convert.ToBoolean(getConfigValueByType("EnabledAttendanceEmailAlert").ToString)
            End If
            If Me.rdoAttendaacnceEmailAlert.Checked = False Then
                Me.rdoAttendaacnceEmailAlertNo.Checked = True
            End If


            If GetConfigValue("Attendance_Period").ToString = "" Then
                DtpStartDate.Value = Date.Now
                DtpStartDate.Checked = False
            Else
                Me.DtpStartDate.Value = Convert.ToDateTime(getConfigValueByType("Attendance_Period").ToString)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub txtWorkingDays_Leave(sender As Object, e As EventArgs) Handles txtWorkingDays.Leave
        Try
            KeyType = "Working_Days"
            KeyValue = Me.txtWorkingDays.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigHRAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub btnAttendanceDbPath_Click(sender As Object, e As EventArgs) Handles btnAttendanceDbPath.Click
        Try
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Microsoft Access|*.*mdb"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtAttendanceDbPath.Text = OpenFileDialog1.FileName
                frmConfigCompany.SaveConfiguration("AlternateAttendanceDBPath", Me.txtAttendanceDbPath.Text.ToString)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub TxtTotalLeaves_Leave(sender As Object, e As EventArgs) Handles TxtTotalLeaves.Leave
        Try
            KeyType = "Leave_Days"
            KeyValue = Me.TxtTotalLeaves.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoAutoBreakAttendance_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAutoBreakAttendance.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoAttendaacnceEmailAlert_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAttendaacnceEmailAlert.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cmbDayOff_SelectedIndexChanged(sender As Object, e As EventArgs)
        
    End Sub

    Private Sub DtpStartDate_Leave(sender As Object, e As EventArgs) Handles DtpStartDate.Leave
        Try
            If DtpStartDate.Checked = True Then
                KeyType = "Attendance_Period"
                KeyValue = Me.DtpStartDate.Value
                frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
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



    Private Sub lblOvertime_Click(sender As Object, e As EventArgs) Handles lblOvertime.Click
        Try
            If frmConfigHROvertime.isFormOpen = True Then
                frmConfigHROvertime.Dispose()
            End If

            frmConfigHROvertime.ShowDialog()
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