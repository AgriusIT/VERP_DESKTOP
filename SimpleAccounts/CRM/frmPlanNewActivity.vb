Imports SBDal
Imports SBModel
Public Class frmPlanNewActivity
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared ActivityId As Integer
    Dim LeadActivity As LeadActivityBE
    Public Shared FName As String
    Public Shared formname As Boolean = False
    Dim activityDate As DateTime
    Dim activityTime As DateTime
    Public LeadActivityDAL As LeadActivityDAL = New LeadActivityDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False

    Private Sub frmPlanNewActivity_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmOTCPlanNewActivity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Projects")
            FillCombos("Lead")
            FillCombos("Person")
            FillCombos("Office")
            FillCombos("ActivityType")
            FillCombos("Responsible")
            'FillCombos("InsideSales")
            FillCombos("Manager")
            Dim dt As DataTable = New LeadActivityDAL().GetById(ActivityId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    ActivityId = dt.Rows(0).Item("ActivityId")
                    cmbLeadTitle.SelectedValue = dt.Rows(0).Item("LeadId")
                    cmbContactPerson.SelectedValue = dt.Rows(0).Item("LeadContactId")
                    cmbOffice.SelectedValue = dt.Rows(0).Item("LeadOfficeId")
                    cmbActivityType.SelectedValue = dt.Rows(0).Item("LeadActivityTypeID")
                    dtpDate.Value = dt.Rows(0).Item("ActivityDate")
                    Me.dtpTime.Value = dt.Rows(0).Item("ActivityTime")
                    'dtpTime.Value = CDate(Now.Date & " " & dt.Rows(0).Item("ActivityTime").Value)
                    cbConfirmed.Checked = dt.Rows(0).Item("IsConfirmed")
                    txtAddressLine1.Text = dt.Rows(0).Item("Address")
                    cmbResponsiblePerson.SelectedValue = dt.Rows(0).Item("ResponsiblePerson_Employee_Id")
                    cmbInsideSales.Text = dt.Rows(0).Item("InsideSalesPerson_Employee_Id")
                    cmbManager.SelectedValue = dt.Rows(0).Item("Manager_Employee_Id")
                    txtObjectives.Text = dt.Rows(0).Item("Objective")
                    cmbProject.SelectedValue = dt.Rows(0).Item("Projectid")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If
            
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            ' Tooltip

            Ttip.SetToolTip(Me.cmbLeadTitle, "Select lead title")
            Ttip.SetToolTip(Me.cmbContactPerson, "Select contact person")
            Ttip.SetToolTip(Me.txtAddressLine1, "Enter address")
            'Ttip.SetToolTip(Me.txtAddressLine2, "Enter address")
            Ttip.SetToolTip(Me.cmbActivityType, "Select account type")
            Ttip.SetToolTip(Me.dtpDate, "Select date")
            Ttip.SetToolTip(Me.dtpTime, "Select time")
            Ttip.SetToolTip(Me.cbConfirmed, "Check confirmed status")
            Ttip.SetToolTip(Me.cmbResponsiblePerson, "Select responsible person")
            Ttip.SetToolTip(Me.cmbInsideSales, "Select inside sales person")
            Ttip.SetToolTip(Me.cmbManager, "Select manager")
            Ttip.SetToolTip(Me.txtObjectives, "Enter objective")
            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Person" Then
                str = "select ContactId, FirstName + ' ' + LastName as ConcernPerson from tblDefLeadProfileContacts where LeadProfileId = " & Me.cmbLeadTitle.SelectedValue & ""
                FillDropDown(Me.cmbContactPerson, str, False)
            ElseIf Condition = "Office" Then
                str = "Select LeadOfficeId, OfficeTitle from tblLeadOffice where LeadId = " & Me.cmbLeadTitle.SelectedValue & ""
                FillDropDown(Me.cmbOffice, str, False)
            ElseIf Condition = "ActivityType" Then
                str = "select * from leadactivitytype where Active = 1"
                FillDropDown(Me.cmbActivityType, str, True)
            ElseIf Condition = "Lead" Then
                str = "Select LeadProfileId, CompanyName from tbldefLeadProfile"
                FillDropDown(Me.cmbLeadTitle, str, True)
            ElseIf Condition = "Responsible" Then
                str = "select * from tblDefEmployee where Sale_Order_Person = 1 and Active = 1 "
                FillDropDown(Me.cmbResponsiblePerson, str, False)
                'ElseIf Condition = "InsideSales" Then
                '    str = "select * from tblDefEmployee "
                '    FillDropDown(Me.cmbInsideSales, str, False)
            ElseIf Condition = "Manager" Then
                str = "select * from tblDefEmployee "
                FillDropDown(Me.cmbManager, str, False)
            ElseIf Condition = "Projects" Then
                str = "select ProjectId, ProjectTitle from tblDefProject "
                FillDropDown(Me.cmbProject, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            activityDate = dtpDate.Value.ToShortDateString
            activityTime = dtpTime.Value.ToShortTimeString
            LeadActivity = New LeadActivityBE
            LeadActivity.ActivityId = ActivityId
            LeadActivity.LeadId = cmbLeadTitle.SelectedValue
            LeadActivity.LeadContactId = cmbContactPerson.SelectedValue
            LeadActivity.LeadOfficeId = cmbOffice.SelectedValue
            LeadActivity.LeadActivityTypeID = cmbActivityType.SelectedValue
            LeadActivity.ActivityDate = Me.dtpDate.Value
            'LeadActivity.ActivityTime = DateTime.ParseExact(activityDate + activityTime, "dd/MMM/yyyy hh:mm:ss tt", Nothing).ToShortTimeString()
            LeadActivity.ActivityTime = Convert.ToDateTime(activityDate) + " " & Convert.ToDateTime(activityTime)
            LeadActivity.IsConfirmed = cbConfirmed.Checked
            LeadActivity.Address = txtAddressLine1.Text
            LeadActivity.ResponsiblePerson_Employee_Id = cmbResponsiblePerson.SelectedValue
            LeadActivity.InsideSalesPerson_Employee_Id = cmbInsideSales.Text
            LeadActivity.Manager_Employee_Id = cmbManager.SelectedValue
            LeadActivity.Objective = txtObjectives.Text
            LeadActivity.ProjectId = cmbProject.SelectedValue
            LeadActivity.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbLeadTitle.SelectedIndex = 0 Then
                ShowErrorMessage("Please select any Lead Title")
                cmbLeadTitle.Focus()
                Return False
            End If
            If Me.cmbActivityType.SelectedIndex = 0 Then
                ShowErrorMessage("Please select any Activity")
                cmbActivityType.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ActivityId = 0
            If formname = True Then
                cmbLeadTitle.SelectedValue = frmActivityFeedback.cmbLeadTitle.SelectedValue
                'cmbLeadTitle_SelectedIndexChanged(Nothing, Nothing)

            Else
                cmbLeadTitle.SelectedIndex = 0
            End If
            'cmbContactPerson.SelectedIndex = 0
            'cmbOffice.SelectedIndex = 0
            cmbActivityType.SelectedIndex = 0
            dtpDate.Value = Date.Now
            dtpTime.Value = Date.Now
            Me.dtpTime.Format = DateTimePickerFormat.Time
            cbConfirmed.Checked = False
            txtAddressLine1.Text = ""
            'cmbResponsiblePerson.SelectedIndex = 0
            'cmbInsideSales.SelectedIndex = 0
            'cmbManager.SelectedIndex = 0
            txtObjectives.Text = ""
            cmbProject.SelectedIndex = 0
            btnSave.Enabled = DoHaveSaveRights
            formname = False
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
                If IsValidate() = True Then
                If ActivityId = 0 Then
                    If LeadActivityDAL.Add(LeadActivity) Then
                        msg_Information("Record has been saved successfully.")
                        If FName = "Feedback" Then
                            Dim str As String = ""
                            str = "select Max(ActivityId) as ActivityId from LeadActivity"
                            Dim dt As DataTable = GetDataTable(str)
                            frmActivityFeedback.PlanId = dt.Rows(0).Item("ActivityId")
                            frmActivityFeedback.GetNextActivity()
                        End If
                        ReSetControls()
                        Me.Close()
                    End If
                Else
                    If LeadActivityDAL.Update(LeadActivity) Then
                        msg_Information("Record has been updated successfully.")
                        ReSetControls()
                        Me.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbLeadTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLeadTitle.SelectedIndexChanged
        Try
            FillCombos("Person")
            FillCombos("Office")
            GetInfo(cmbLeadTitle.SelectedValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Function GetInfo(ByVal ID As Integer)
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT ResponsibleId, InsideSalesId, ManagerId from LeadProfile where LeadId = " & ID & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                cmbResponsiblePerson.SelectedValue = dt.Rows(0).Item("ResponsibleId")
                cmbInsideSales.SelectedValue = dt.Rows(0).Item("InsideSalesId")
                cmbManager.SelectedValue = dt.Rows(0).Item("ManagerId")
            End If
            Dim str1 As String
            Dim dt1 As DataTable
            str1 = "select ProjectId, ProjectTitle from tblDefProject where LeadProfileid = " & ID & ""
            dt1 = GetDataTable(str1)
            If dt1.Rows.Count > 0 Then
                cmbProject.SelectedValue = dt1.Rows(0).Item("ProjectId")
            Else
                cmbProject.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub passValue()
        Try
            FillCombos("Person")
            FillCombos("Office")
            GetInfo(cmbLeadTitle.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class