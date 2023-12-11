Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmSalaryConfig
    Implements IGeneral
    Dim ConfigDataTable As DataTable
    Dim ConfigList As List(Of ConfigSystem)
    Dim ConfigSalaryAccount As ConfigSystem
    Dim ConfigSalaryPayableAccount As ConfigSystem
    Dim WorkingDays As ConfigSystem
    Dim AttendanceBasedSalary As ConfigSystem
    Dim dt As DataTable
    Dim GrossSalaryCalculationByFormula As ConfigSystem
    Dim GrossSalaryFormula As ConfigSystem
    Dim AttendancePeriod As ConfigSystem
    Dim ErrorMessage As String = String.Empty
    Dim key As String
    Dim IsOpenForm As Boolean = False
    Dim IsChangedValue As Object
    Dim WorkingHours As ConfigSystem

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "SalaryAccount" Then
                strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                FillDropDown(Me.cmbSalaryAccount, strSQL, True)
            ElseIf Condition = "SalaryPayableAccount" Then
                strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                FillDropDown(Me.cmbSalaryPayableAccount, strSQL, True)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function GetComboData() As DataTable
        Try

            Dim strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
            dt = GetDataTable(strSQL)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ConfigList = New List(Of ConfigSystem)
            If Me.DtpStartDate.Checked = True Then
                AttendancePeriod = New ConfigSystem
                AttendancePeriod.Config_Type = "Attendance_Period"
                AttendancePeriod.Config_Value = Me.DtpStartDate.Value.Date.ToString("yyyy-M-d h:mm:ss tt")
                ConfigList.Add(AttendancePeriod)
            End If

            ConfigSalaryAccount = New ConfigSystem
            ConfigSalaryAccount.Config_Type = "SalariesAccountId"
            ConfigSalaryAccount.Config_Value = Me.cmbSalaryAccount.SelectedValue
            ConfigList.Add(ConfigSalaryAccount)

            ConfigSalaryPayableAccount = New ConfigSystem
            ConfigSalaryPayableAccount.Config_Type = "SalariesPayableAccountId"
            ConfigSalaryPayableAccount.Config_Value = Me.cmbSalaryPayableAccount.SelectedValue
            ConfigList.Add(ConfigSalaryPayableAccount)

            WorkingDays = New ConfigSystem
            WorkingDays.Config_Type = "Working_Days"
            WorkingDays.Config_Value = Convert.ToString(Me.txtWorkingDays.Text)
            ConfigList.Add(WorkingDays)

            GrossSalaryCalculationByFormula = New ConfigSystem
            GrossSalaryCalculationByFormula.Config_Type = "GrossSalaryCalcByFormula"
            GrossSalaryCalculationByFormula.Config_Value = Convert.ToString(Me.chkGrossSalaryCalc.Checked)
            ConfigList.Add(GrossSalaryCalculationByFormula)

            GrossSalaryFormula = New ConfigSystem
            GrossSalaryFormula.Config_Type = "GrossSalaryFormula"
            GrossSalaryFormula.Config_Value = Convert.ToString(Me.txtGrossSalaryFormula.Text)
            ConfigList.Add(GrossSalaryFormula)

            AttendanceBasedSalary = New ConfigSystem
            AttendanceBasedSalary.Config_Type = "AttendanceBasedSalary"
            AttendanceBasedSalary.Config_Value = Convert.ToString(Me.chkAttendanceBaseSalary.Checked)
            ConfigList.Add(AttendanceBasedSalary)

            AttendanceBasedSalary = New ConfigSystem
            AttendanceBasedSalary.Config_Type = "AttendanceBasedSalary"
            AttendanceBasedSalary.Config_Value = Convert.ToString(Me.chkAttendanceBaseSalary.Checked)
            ConfigList.Add(AttendanceBasedSalary)

            WorkingHours = New ConfigSystem
            WorkingHours.Config_Type = "DefaultWorkingHours"
            WorkingHours.Config_Value = Convert.ToString(Me.nudDefaultWorkingHours.Value)
            ConfigList.Add(WorkingHours)

        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.cmbSalaryAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesAccountId").ToString))
            Me.cmbSalaryPayableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesPayableAccountId").ToString))
            Me.chkAttendanceBaseSalary.Checked = Convert.ToBoolean(getConfigValueByType("AttendanceBasedSalary").ToString)
            Me.txtWorkingDays.Text = Convert.ToInt32(Val(getConfigValueByType("Working_Days").ToString))

            Me.chkGrossSalaryCalc.Checked = Convert.ToBoolean(getConfigValueByType("GrossSalaryCalcByFormula").ToString)
            Me.txtGrossSalaryFormula.Text = getConfigValueByType("GrossSalaryFormula").ToString

            If GetConfigValue("Attendance_Period").ToString = "" Then
                DtpStartDate.Value = Date.Now
                DtpStartDate.Checked = False
            Else
                Me.DtpStartDate.Value = Convert.ToDateTime(getConfigValueByType("Attendance_Period").ToString)
            End If

            Me.nudDefaultWorkingHours.Value = Val(getConfigValueByType("DefaultWorkingHours").ToString)
            Me.TxtTotalLeaves.Text = Val(getConfigValueByType("Leave_Days").ToString)
        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Sub
    Public Function GetConfigByType(ByVal strType As String) As String
        Try
            Return GetFilterDataFromDataTable(Me.ConfigDataTable, "[Config_Type]='" & strType & "'").ToTable("Config").Rows(0).Item("config_value").ToString()

        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Function
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        If IsValidate() = True Then
            'Me.lblPrograssbar1.Text = ""
            'Me.lblPrograssbar1.Text = "Please wait"
            'Do Until Me.PrograssBar1.Value >= 95
            '    Me.PrograssBar1.Value = Me.PrograssBar1.Value + 1
            '    Application.DoEvents()
            '    System.Threading.Thread.Sleep(10)
            'Loop
            If New SBDal.ConfigSystemDAL().SaveConfigSys(ConfigList) Then
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub frmSystemConfigurationNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Me.btnSave.Enabled = True
        Try

            FillCombos("SalaryAccount")
            FillCombos("SalaryPayableAccount")

            GetAllRecords()
            IsOpenForm = True
            'Me.PrograssBar1.Visible = False
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading" & ex.Message)
            Me.btnSave.Enabled = False
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            FillCombos("SalaryAccount")
            FillCombos("SalaryPayableAccount")

            IsOpenForm = True
            getConfigValueList()
            GetAllRecords()

            'GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub nudDefaultReminder_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudDefaultWorkingHours.ValueChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim nud As NumericUpDown = CType(sender, NumericUpDown)
            SaveConfiguration(nud.Tag, nud.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function SaveConfiguration(ByVal KeyType As String, ByVal KeyValue As String) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            key = KeyType
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            objConfigValueList.Remove(config)
            Dim AddConfig As New ConfigSystem
            AddConfig.Config_Type = KeyType.ToString
            AddConfig.Config_Value = KeyValue.ToString
            If config IsNot Nothing Then
                If config.Comments IsNot Nothing Then
                    AddConfig.Comments = config.Comments
                Else
                    AddConfig.Comments = Nothing
                End If
                AddConfig.IsActive = config.IsActive
            End If

            objConfigValueList.Add(AddConfig)
            key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetObj(ByVal Config As ConfigSystem) As Boolean
        Try
            If Config.Config_Type = key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub chkPreviouseRecordShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGrossSalaryCalc.CheckedChanged, chkAttendanceBaseSalary.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim chk As Windows.Forms.CheckBox
            chk = CType(sender, CheckBox)

            If chk.Tag.ToString.Length > 0 Then SaveConfiguration(chk.Tag, chk.Checked)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnVoucherFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWorkingDays.Click, btnGrossSalaryFormula.Click, BtnLeaves.Click, BtnStartPeriod.Click
        Try
            If IsOpenForm = False Then Exit Sub
            Dim btn As Windows.Forms.Button
            btn = CType(sender, Button)
            Dim KeyType As String = String.Empty
            Dim KeyValue As String = String.Empty
            Select Case btn.Name
                Case btnWorkingDays.Name
                    KeyType = "Working_Days"
                    KeyValue = Me.txtWorkingDays.Text
                Case BtnStartPeriod.Name
                    If DtpStartDate.Checked = True Then
                        KeyType = "Attendance_Period"
                        KeyValue = Me.DtpStartDate.Value
                    End If
                Case BtnLeaves.Name
                    KeyType = "Leave_Days"
                    KeyValue = Me.TxtTotalLeaves.Text
                Case btnGrossSalaryFormula.Name
                    KeyType = "GrossSalaryFormula"
                    KeyValue = Me.txtGrossSalaryFormula.Text
            End Select
            SaveConfiguration(KeyType, KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalaryAccount_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSalaryAccount.Enter, cmbSalaryPayableAccount.Enter
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

    Private Sub CmbPurchaseTaxIDeductionAccountNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalaryPayableAccount.Leave, cmbSalaryAccount.Leave
        Try
            If IsOpenForm = False Then Exit Sub
            Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
            If cmb.SelectedIndex = -1 Then Exit Sub

            If cmb.SelectedValue IsNot Nothing Then
                If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)
            Else
                If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class