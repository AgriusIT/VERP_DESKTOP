Public Class frmRptTaskDetail


    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "Users" Then
                str = "Select Employee_ID, Employee_Name From tblDefEmployee"
                FillDropDown(Me.CmbUser, str, True)
            ElseIf Condition = "Customer" Then
                str = "Select coa_detail_id, detail_title as [Customer], detail_code as [Code],Account_Type as [Type], Contact_Mobile as [Mobile], Contact_Email as [Email] From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title ASC"
                FillUltraDropDown(Me.cmbAccounts, str)
                Me.cmbAccounts.Rows(0).Activate()
                If Me.cmbAccounts.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Customer").Width = 200
                End If
            ElseIf Condition = "TaskStatus" Then
                FillDropDown(Me.cmbTaskStatus, "Select * From tblDefStatus")
            ElseIf Condition = "TaskType" Then
                FillDropDown(Me.cmbTaskType, "Select * From TblDefTypes")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptTaskDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.DateTimePicker1.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.DateTimePicker2.Value = Date.Now
            FillCombos("TaskType")
            FillCombos("TaskStatus")
            FillCombos("Users")
            FillCombos("Customer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Dim strFilter As String = String.Empty
            strFilter = "{SP_Tasks;1.Task} <> ''"
            strFilter += " AND {SP_Tasks;1.TaskDate} >= DateTime (" & Me.DateTimePicker1.Value.ToString("yyyy, MM, dd, 00, 00, 00") & ")"
            strFilter += " AND {SP_Tasks;1.TaskDate} <= DateTime (" & Me.DateTimePicker2.Value.ToString("yyyy, MM, dd, 23, 59, 59") & ")"
            If Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Then
                strFilter += " AND {SP_Tasks;1.CustomerId}=" & Me.cmbAccounts.Value & ""
            End If
            If Me.CmbUser.SelectedIndex > 0 Then
                strFilter += " AND {SP_Tasks;1.TaskUser}=" & Me.CmbUser.SelectedValue & ""
            End If
            If Me.cmbTaskStatus.SelectedIndex > 0 Then
                strFilter += " AND {SP_Tasks;1.TaskStatus}=" & Me.cmbTaskStatus.SelectedValue & ""
            End If
            If Me.cmbTaskType.SelectedIndex > 0 Then
                strFilter += " AND {SP_Tasks;1.Type}=" & Me.cmbTaskType.SelectedValue & ""
            End If
            AddRptParam("DateFrom", Me.DateTimePicker1.Value)
            AddRptParam("ToDate", Me.DateTimePicker2.Value)
            ShowReport("rptTasksDetail", strFilter.ToString())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class