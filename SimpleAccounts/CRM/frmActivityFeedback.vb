Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmActivityFeedback
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared ActivityId As Integer
    Public Shared FeedbackId As Integer
    Public Shared PlanId As Integer = Nothing
    Dim ActivityFeedback As ActivityFeedbackBE
    Dim list As List(Of PotentialBE)
    Public FeedbackDAL As ActivityFeedbackDAL = New ActivityFeedbackDAL()

    'Public DoHaveSaveRights As Boolean = False
    'Public DoHaveUpdateRights As Boolean = False

    Private Sub frmOTCActivityFeedback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Lead")
            FillCombos("Status")
            FillCombos("Contact")
            FillCombos("ActivityType")
            FillCombos("Responsible")
            FillCombos("Reason")
            Dim dt As DataTable = New ActivityFeedbackDAL().GetById(ActivityId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    FeedbackId = Val(dt.Rows(0).Item("ActivityFeedbackId").ToString)
                    ActivityId = Val(dt.Rows(0).Item("ActivityId"))
                    cmbStatus.SelectedValue = Val(dt.Rows(0).Item("ActivityFeedbackStatusId").ToString)
                    cmbActivityType.SelectedValue = Val(dt.Rows(0).Item("LeadActivityTypeID").ToString)
                    cmbLeadTitle.SelectedValue = Val(dt.Rows(0).Item("LeadId").ToString)
                    cmbContactPerson.SelectedValue = dt.Rows(0).Item("LeadContactId")
                    cmbResponsiblePerson.SelectedValue = Val(dt.Rows(0).Item("ResponsiblePerson_Employee_Id").ToString)
                    PlanId = Val(dt.Rows(0).Item("NextActionPlan").ToString)
                    dtpDate.Value = dt.Rows(0).Item("ActivityDate").ToString
                    cmbReason.Text = dt.Rows(0).Item("Reason").ToString
                    txtDetail.Text = dt.Rows(0).Item("Details").ToString
                Next
                If PlanId > 0 Then
                    GetNextActivity()
                Else
                    lblNextActionPlan.Text = ""
                    btnNextPlan.Enabled = True
                End If
            Else
                ReSetControls()
            End If
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            ' Tooltip
            Ttip.SetToolTip(Me.cmbActivityType, "Select activity type")
            Ttip.SetToolTip(Me.dtpDate, "Select date")
            Ttip.SetToolTip(Me.cmbResponsiblePerson, "Select responsible person")
            Ttip.SetToolTip(Me.cmbLeadTitle, "Select lead title")
            Ttip.SetToolTip(Me.cmbContactPerson, "Select contact person")
            Ttip.SetToolTip(Me.cmbStatus, "Select status")
            Ttip.SetToolTip(Me.cmbReason, "Select reason")
            Ttip.SetToolTip(Me.txtDetail, "Enter detail")

            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")
            GetPontential(FeedbackId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Private Function GetPontential(ByVal FID As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            If FID > 0 Then
                str = "SELECT ActivityFeedbackPotential.Id,Principal.PrincipalId, Potential.P_Check, Principal.Principal, Potential.Title, Potential.Remarks, ActivityFeedbackPotential.PotentialId FROM ActivityFeedback Right OUTER JOIN ActivityFeedbackPotential ON ActivityFeedback.ActivityFeedbackId = ActivityFeedbackPotential.ActivityFeedbackId Right OUTER JOIN Principal INNER JOIN Potential ON Principal.PrincipalId = Potential.PrincipalId ON ActivityFeedbackPotential.PotentialId = Potential.PotentialId where ActivityFeedbackPotential.ActivityFeedbackId = " & FID & ""
                dt = GetDataTable(str)
                grdPayment.DataSource = dt
                grdPayment.RetrieveStructure()
                Me.grdPayment.RootTable.Columns("Id").Visible = False
                Me.grdPayment.RootTable.Columns("PrincipalId").Visible = False
                Me.grdPayment.RootTable.Columns("PotentialId").Visible = False
            Else
                str = "SELECT 0 as Id, Principal.PrincipalId, ISNULL(Potential.P_Check, 0) as P_Check, Principal.Principal, Potential.PotentialId, Potential.Title, Potential.Remarks FROM Principal INNER JOIN Potential ON Principal.PrincipalId = Potential.PrincipalId"
                dt = GetDataTable(str)
                grdPayment.DataSource = dt
                grdPayment.RetrieveStructure()
                Me.grdPayment.RootTable.Columns("Id").Visible = False
                Me.grdPayment.RootTable.Columns("PrincipalId").Visible = False
                Me.grdPayment.RootTable.Columns("PotentialId").Visible = False
            End If
            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdPayment.RootTable.Columns("Principal"))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grdPayment.RootTable.Groups.Add(grdGroupBy)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Contact" Then
                str = "Select ConcernPersonId, ConcernPerson from tblConcernPerson "
                FillDropDown(Me.cmbContactPerson, str, True)
            ElseIf Condition = "Status" Then
                str = "Select * from ActivityFeedbackStatus where Active = 1"
                FillDropDown(Me.cmbStatus, str, True)
            ElseIf Condition = "ActivityType" Then
                str = "select * from leadactivitytype where Active = 1"
                FillDropDown(Me.cmbActivityType, str, True)
            ElseIf Condition = "Lead" Then
                str = "Select LeadId, LeadTitle from LeadProfile where Active = 1"
                FillDropDown(Me.cmbLeadTitle, str, True)
            ElseIf Condition = "Responsible" Then
                str = "select * from tblDefEmployee"
                FillDropDown(Me.cmbResponsiblePerson, str, True)
            ElseIf Condition = "Reason" Then
                str = "select Distinct Reason, Reason from ActivityFeedback"
                FillDropDown(Me.cmbReason, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ActivityFeedback = New ActivityFeedbackBE
            ActivityFeedback.ActivityFeedbackId = FeedbackId
            ActivityFeedback.ActivityId = ActivityId
            ActivityFeedback.ActivityFeedbackStatusId = cmbStatus.SelectedValue
            ActivityFeedback.FeedbackDate = Date.Now
            ActivityFeedback.Reason = cmbReason.Text
            ActivityFeedback.NextActionPlan = PlanId
            ActivityFeedback.Details = txtDetail.Text
            ActivityFeedback.ActivityLog = New ActivityLog
            list = New List(Of PotentialBE)
            'For i As Integer = 0 To grdPayment.RowCount - 1
            '    Dim PDetail As New PotentialBE
            '    PDetail.Id = grdPayment.GetDataRows(i).Cells("Id").Value
            '    PDetail.PotentialId = grdPayment.GetDataRows(i).Cells("PotentialId").Value
            '    PDetail.P_Check = grdPayment.GetDataRows(i).Cells("P_Check").Value
            '    List.Add(PDetail)
            'Next
            For Each row As Janus.Windows.GridEX.GridEXRow In grdPayment.GetDataRows
                Dim PDetail As New PotentialBE
                PDetail.Id = row.Cells("Id").Value
                PDetail.PotentialId = row.Cells("PotentialId").Value
                PDetail.P_Check = row.Cells("P_Check").Value
                list.Add(PDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbStatus.SelectedValue = Nothing Then
                ShowErrorMessage("Please select Status before saving")
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
            FeedbackId = 0
            ActivityId = 0
            PlanId = 0
            cmbLeadTitle.SelectedIndex = 0
            cmbContactPerson.SelectedIndex = 0
            cmbActivityType.SelectedIndex = 0
            cmbResponsiblePerson.SelectedIndex = 0
            cmbStatus.SelectedIndex = 0
            cmbReason.SelectedIndex = 0
            dtpDate.Value = Date.Now
            txtDetail.Text = ""
            btnNextPlan.Enabled = True
            lblNextActionPlan.Text = ""
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
                If FeedbackId = 0 Then
                    Dim ActivityFeedBackId As Integer
                    If FeedbackDAL.Add(ActivityFeedback, ActivityFeedBackId) Then
                        If FeedbackDAL.AddPotential(list) Then
                            SaveApprovalLog(EnumReferenceType.ActivityFeedBack, ActivityFeedBackId, "CRM", Me.dtpDate.Value.Date, Me.cmbReason.Text)
                            msg_Information("Record has been saved successfully.")
                            ReSetControls()
                            Me.Close()
                        End If
                    End If
                Else
                    If FeedbackDAL.Update(ActivityFeedback) Then
                        If FeedbackDAL.UpdatePotential(list) Then
                            msg_Information("Record has been updated successfully.")
                            ReSetControls()
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNextPlan_Click(sender As Object, e As EventArgs) Handles btnNextPlan.Click
        Try
            frmPlanNewActivity.formname = True
            frmPlanNewActivity.ActivityId = 0
            frmPlanNewActivity.DoHaveSaveRights = True
            frmPlanNewActivity.FName = "Feedback"
            frmPlanNewActivity.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetNextActivity() As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId, LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as Manager, tblDefEmployee.Employee_Name AS Responsible, tblDefEmployee_1.Employee_Name AS InsideSales, LeadActivity.IsConfirmed FROM LeadActivity LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId  where ActivityId= " & PlanId & ""
            dt = GetDataTable(str)
            lblNextActionPlan.Text = dt.Rows(0).Item("ActivityType") + ":" + dt.Rows(0).Item("ActivityDate").ToShortDateString + " " + dt.Rows(0).Item("ActivityTime").ToShortTimeString + "," + dt.Rows(0).Item("Objective")
            btnNextPlan.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmActivityFeedback_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class