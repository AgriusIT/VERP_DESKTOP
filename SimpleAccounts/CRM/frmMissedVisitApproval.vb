Public Class frmMissedVisitApproval

    Private Sub frmMissedVisitApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            FillDropDown(cmbEmployeeName, "SELECT tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Name FROM LeadActivity INNER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId RIGHT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId RIGHT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID where ActivityFeedbackStatus.Status = 'Missed'", True)



            Dim dt As DataTable
            Dim str As String
            'str = "SELECT        tblDefEmployee.Employee_Name, ActivityFeedbackStatus.Status" & _
            '            "FROM            LeadActivity INNER JOIN " & _
            '             " ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId RIGHT OUTER JOIN " & _
            '             " ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId RIGHT OUTER JOIN " & _
            '             " tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID  " & _
            '             " where ActivityFeedbackStatus.Status = 'In Progress'"


            str = "SELECT tblDefEmployee.Employee_Name, ActivityFeedbackStatus.Status FROM LeadActivity INNER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId RIGHT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId RIGHT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID where ActivityFeedbackStatus.Status = 'Missed'"


            dt = GetDataTable(str)
            grdMissedVisitedApproval.DataSource = dt
            grdMissedVisitedApproval.RetrieveStructure()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class