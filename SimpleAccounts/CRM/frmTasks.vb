'2015-06-22 Task#2015060024 Add Time in and Time Out Ali Ansari (Added Time In and optional Time Out in Designer)
'2015-08-03 Task#20150801 Making Employee Wise Selection according to rights 
' 14-12-2015 TASKTFS150 Muhammad Ameen: Added ComboBox Assigned To user and functioned it in save and update procedures.
' Muhammad Ameen TASK TFS1790 on 12-12-2017. Addition of date wise grouping to view.
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb
Imports System.Linq
Imports Infragistics.Win.UltraWinTabControl


Public Class frmTasks
    Implements IGeneral
    Dim Task As Task
    Dim TaskId As Integer = 0
    Dim ViewAll As Boolean = False 'Task#20150801 security check
    Public Ref_No As String = String.Empty
    Public ReferenceForm As String = String.Empty
    Private _dtNotification As New DataTable
    Private Updatemode As Boolean = False
    Private isOpenForm As Boolean = False
    Dim list As New List(Of TaskActivity)
    Dim obj As TaskActivity
    Enum EnmTasks
        TaskId
        TaskDate
        'WorkPeriod
        TaskNo
        Name
        Remarks
        TimeIn 'Task#2015060024 Add Time in and Time Out Column
        TimeOut 'Task#2015060024 Add Time in and Time Out Column
        Project
        CostCenter
        TaskUser
        UserName
        Customer
        CustomerName
        Type
        TaskType
        TaskStatus
        Status
        ClosingDate
        Active
        UserId 'Task#20150801 Add UserId
        AssignedTo
        AssignedToName
        Completed
        'CreatedBy
        'CreatedByID
        'LastUpdatedBy
        'RefNo
        'FormName

    End Enum
    Enum enmTaskActivity
        'Select ID, TypeID, Dated, Description, Time, " _
        '& " ISNULL(TaskId, 0) As TaskId From tblTaskActivityUpdate "
        ID
        Dated
        TypeID
        Time
        Description
        TaskId
    End Enum
    Sub TaskEditRecord()
        Try

            If Not GrdTask.RowCount <> 0 Then Exit Sub
            Updatemode = True
            Me.tsslCreatedBy.Text = ""
            Me.tsslLastUpdatedBy.Text = ""
            TaskId = GrdTask.GetRow.Cells("TaskId").Value
            Me.dtpDate.Value = Me.GrdTask.GetRow.Cells("TaskDate").Value
            Me.TxtName.Text = GrdTask.GetRow.Cells("Task Name").Value.ToString
            Me.tsslCreatedBy.Text = "Created by " & " " & GrdTask.GetRow.Cells("CreatedBy").Value.ToString
            Me.tsslLastUpdatedBy.Text = "Last updated by" & " " & GrdTask.GetRow.Cells("LastUpdatedBy").Value.ToString
            Me.TxtRemarks.Text = GrdTask.GetRow.Cells("Remarks").Value.ToString
            Me.CmbProject.SelectedValue = Val(GrdTask.GetRow.Cells("Project").Value.ToString)
            Me.CmbType.SelectedValue = Val(GrdTask.GetRow.Cells("Type").Value.ToString)
            Me.CmbStatus.SelectedValue = Val(GrdTask.GetRow.Cells("TaskStatus").Value.ToString)
            Me.CmbUser.SelectedValue = Val(GrdTask.GetRow.Cells("TaskUser").Value.ToString)
            Me.cmbAssignedTo.SelectedValue = Val(GrdTask.GetRow.Cells("AssignedTo").Value.ToString)
            If Not IsDBNull(GrdTask.GetRow.Cells("Completed").Value) AndAlso Me.GrdTask.GetRow.Cells("Completed").Value = True Then
                Me.chkActive.Checked = True
            Else
                Me.chkActive.Checked = False
            End If
            Me.cmbAccounts.Value = Val(Me.GrdTask.GetRow.Cells("CustomerId").Value.ToString)
            If IsDBNull(GrdTask.GetRow.Cells("ClosingDate").Value) Then
                Me.dtpCloseTask.Value = Date.Now
                Me.dtpCloseTask.Checked = False
            Else
                Me.dtpCloseTask.Value = GrdTask.GetRow.Cells("ClosingDate").Value
                Me.dtpCloseTask.Checked = True
            End If
            If IsDBNull(GrdTask.GetRow.Cells("CustomEndDate").Value) Then
                Me.lblCustomerEndDate.Text = ""
            Else
                Me.lblCustomerEndDate.Text = "Customer End Date:" + " " + GrdTask.GetRow.Cells("CustomEndDate").Value.ToString
            End If
            'Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            Me.Timer1.Stop()
            If IsDBNull(GrdTask.GetRow.Cells("TimeIn").Value) Then
                Me.dtpTimeIn.Value = Date.Now
                Me.dtpTimeIn.Checked = False
            Else
                Me.dtpTimeIn.Value = GrdTask.GetRow.Cells("TimeIn").Value
                Me.dtpTimeIn.Checked = True
            End If
            If IsDBNull(GrdTask.GetRow.Cells("TimeOut").Value) Then
                Me.DtpOutTime.Value = Date.Now
                Me.DtpOutTime.Checked = False
            Else
                Me.DtpOutTime.Value = GrdTask.GetRow.Cells("TimeOut").Value
                Me.DtpOutTime.Checked = True
            End If

            'Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            Me.txtTaskNo.Text = Me.GrdTask.GetRow.Cells("Task_No").Value.ToString
            Me.BtnSave.Text = "&Update"
            Me.BtnDelete.Enabled = True
            'If Not GrdTask.RowCount <> 0 Then Exit Sub
            'TaskId = GrdTask.GetRow.Cells(EnmTasks.TaskId).Value
            'Me.dtpDate.Value = Me.GrdTask.GetRow.Cells(EnmTasks.TaskDate).Value
            'Me.TxtName.Text = GrdTask.GetRow.Cells(EnmTasks.Name).Value.ToString
            'Me.TxtRemarks.Text = GrdTask.GetRow.Cells(EnmTasks.Remarks).Value.ToString
            'Me.CmbProject.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.Project).Value.ToString)
            'Me.CmbType.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.Type).Value.ToString)
            'Me.CmbStatus.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.TaskStatus).Value.ToString)
            'Me.CmbUser.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.TaskUser).Value.ToString)
            'Me.cmbAssignedTo.SelectedValue = Val(GrdTask.GetRow.Cells("AssignedTo").Value.ToString)

            'If Not IsDBNull(GrdTask.GetRow.Cells(EnmTasks.Completed).Value) AndAlso Me.GrdTask.GetRow.Cells(EnmTasks.Completed).Value = True Then
            '    Me.chkActive.Checked = True
            'Else
            '    Me.chkActive.Checked = False
            'End If
            'Me.cmbAccounts.Value = Val(Me.GrdTask.GetRow.Cells("CustomerId").Value.ToString)
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.ClosingDate).Value) Then
            '    Me.dtpCloseTask.Value = Date.Now
            '    Me.dtpCloseTask.Checked = False
            'Else
            '    Me.dtpCloseTask.Value = GrdTask.GetRow.Cells(EnmTasks.ClosingDate).Value
            '    Me.dtpCloseTask.Checked = True
            'End If
            ''Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            'Me.Timer1.Stop()
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.TimeIn).Value) Then
            '    Me.dtpTimeIn.Value = Date.Now
            '    Me.dtpTimeIn.Checked = False
            'Else
            '    Me.dtpTimeIn.Value = GrdTask.GetRow.Cells(EnmTasks.TimeIn).Value
            '    Me.dtpTimeIn.Checked = True
            'End If
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.TimeOut).Value) Then
            '    Me.DtpOutTime.Value = Date.Now
            '    Me.DtpOutTime.Checked = False
            'Else
            '    Me.DtpOutTime.Value = GrdTask.GetRow.Cells(EnmTasks.TimeOut).Value
            '    Me.DtpOutTime.Checked = True
            'End If

            ''Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            'Me.txtTaskNo.Text = Me.GrdTask.GetRow.Cells("Task_No").Value.ToString
            'Me.BtnSave.Text = "&Update"
            'Me.BtnDelete.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TaskBottonDisplayRecord()
        Try

            If Not GrdTask.RowCount <> 0 Then Exit Sub
            'Updatemode = True
            FillCombos("ContactPerson")
            Me.tsslCreatedBy.Text = ""
            Me.tsslLastUpdatedBy.Text = ""
            TaskId = GrdTask.GetRow.Cells("TaskId").Value
            Me.GetSingleActivity(TaskId)
            'Me.grdActivity.DataSource = New TaskDAL().GetTaskActivity(TaskId)
            'Me.grdActivity.RetrieveStructure()
            'ApplyActivityGridSettings()
            Me.dtpCreatedOnH.Value = Me.GrdTask.GetRow.Cells("TaskDate").Value
            'Me.TxtName.Text = GrdTask.GetRow.Cells("Task Name").Value.ToString
            Me.tsslCreatedBy.Text = "Created by: " & " " & GrdTask.GetRow.Cells("CreatedBy").Value.ToString
            Me.tsslLastUpdatedBy.Text = "Last updated by:" & " " & GrdTask.GetRow.Cells("LastUpdatedBy").Value.ToString
            Me.txtDetailH.Text = GrdTask.GetRow.Cells("Remarks").Value.ToString
            Dim C = "Customer End Date:"
            If Not IsDBNull(GrdTask.GetRow.Cells("CustomEndDate").Value) Then
                lblCustomerEndDate.Text = C & ":" & Convert.ToDateTime(GrdTask.GetRow.Cells("CustomEndDate").Value).ToString("dd/MMM/yyyy")
                lblCustomerEndDate.Visible = True
                If GrdTask.GetRow.Cells("CustomEndDate").Value < DateTime.Now Then
                    lblCustomerEndDate.BackColor = Color.Red
                ElseIf GrdTask.GetRow.Cells("CustomEndDate").Value < DateTime.Now.AddDays(7) Then
                    lblCustomerEndDate.BackColor = Color.Yellow
                Else
                    lblCustomerEndDate.BackColor = Control.DefaultBackColor
                End If
            Else
                lblCustomerEndDate.Text = String.Empty
                lblCustomerEndDate.Visible = False
                lblCustomerEndDate.BackColor = Control.DefaultBackColor
            End If
            If Val(GrdTask.GetRow.Cells("Project").Value.ToString) = 0 Then
                Me.cmbProjectH.SelectedIndex = 0
            Else
                Me.cmbProjectH.SelectedValue = Val(GrdTask.GetRow.Cells("Project").Value.ToString)
            End If

            If Val(Me.GrdTask.GetRow.Cells("ContactPersonID").Value.ToString) = 0 Then
                Me.cmbContactPersonH.Rows(0).Activate()
            Else
                Me.cmbContactPersonH.Value = Val(Me.GrdTask.GetRow.Cells("ContactPersonID").Value.ToString)
            End If
            'Me.cmbTypeH.SelectedValue = Val(GrdTask.GetRow.Cells("Type").Value.ToString)
            'Me.cmbStatusH.SelectedValue = Val(GrdTask.GetRow.Cells("TaskStatus").Value.ToString)
            'Me.cmbEmpoyeeH.SelectedValue = Val(GrdTask.GetRow.Cells("TaskUser").Value.ToString)
            'Me.cmbAssignedToH.SelectedValue = Val(GrdTask.GetRow.Cells("AssignedTo").Value.ToString)
            If Not IsDBNull(GrdTask.GetRow.Cells("Completed").Value) AndAlso Me.GrdTask.GetRow.Cells("Completed").Value = True Then
                Me.cbCompletedH.Checked = True
            Else
                Me.cbCompletedH.Checked = False
            End If
            If Val(Me.GrdTask.GetRow.Cells("CustomerId").Value.ToString) = 0 Then
                Me.cmbAccountsH.Rows(0).Activate()
            Else
                Me.cmbAccountsH.Value = Val(Me.GrdTask.GetRow.Cells("CustomerId").Value.ToString)
            End If

            If IsDBNull(GrdTask.GetRow.Cells("ClosingDate").Value) Then
                Me.dtpClosingDateH.Value = Date.Now
                Me.dtpClosingDateH.Checked = False
            Else
                Me.dtpClosingDateH.Value = GrdTask.GetRow.Cells("ClosingDate").Value
                Me.dtpClosingDateH.Checked = True
            End If
            'Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            Me.Timer1.Stop()
            If IsDBNull(GrdTask.GetRow.Cells("TimeIn").Value) Then
                Me.dtpStartdateH.Value = Date.Now
                Me.dtpStartdateH.Checked = False
            Else
                Me.dtpStartdateH.Value = GrdTask.GetRow.Cells("TimeIn").Value
                Me.dtpStartdateH.Checked = True
            End If
            If IsDBNull(GrdTask.GetRow.Cells("TimeOut").Value) Then
                Me.dtpEnddateH.Value = Date.Now
                Me.dtpEnddateH.Checked = False
            Else
                Me.dtpEnddateH.Value = GrdTask.GetRow.Cells("TimeOut").Value
                Me.dtpEnddateH.Checked = True
            End If

            'Dim dtTskActivity As New DataTable
            'dtTskActivity = CType(Me.grdActivity.DataSource, DataTable)
            'Dim dr As DataRow = dtTskActivity.NewRow
            'dtTskActivity.Rows.Add(dr)



            'Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            'Me.txtDetailH.Text = Me.GrdTask.GetRow.Cells("Task_No").Value.ToString
            'Me.BtnSave.Text = "&Update"
            'Me.BtnDelete.Enabled = True
            'If Not GrdTask.RowCount <> 0 Then Exit Sub
            'TaskId = GrdTask.GetRow.Cells(EnmTasks.TaskId).Value
            'Me.dtpDate.Value = Me.GrdTask.GetRow.Cells(EnmTasks.TaskDate).Value
            'Me.TxtName.Text = GrdTask.GetRow.Cells(EnmTasks.Name).Value.ToString
            'Me.TxtRemarks.Text = GrdTask.GetRow.Cells(EnmTasks.Remarks).Value.ToString
            'Me.CmbProject.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.Project).Value.ToString)
            'Me.CmbType.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.Type).Value.ToString)
            'Me.CmbStatus.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.TaskStatus).Value.ToString)
            'Me.CmbUser.SelectedValue = Val(GrdTask.GetRow.Cells(EnmTasks.TaskUser).Value.ToString)
            'Me.cmbAssignedTo.SelectedValue = Val(GrdTask.GetRow.Cells("AssignedTo").Value.ToString)

            'If Not IsDBNull(GrdTask.GetRow.Cells(EnmTasks.Completed).Value) AndAlso Me.GrdTask.GetRow.Cells(EnmTasks.Completed).Value = True Then
            '    Me.chkActive.Checked = True
            'Else
            '    Me.chkActive.Checked = False
            'End If
            'Me.cmbAccounts.Value = Val(Me.GrdTask.GetRow.Cells("CustomerId").Value.ToString)
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.ClosingDate).Value) Then
            '    Me.dtpCloseTask.Value = Date.Now
            '    Me.dtpCloseTask.Checked = False
            'Else
            '    Me.dtpCloseTask.Value = GrdTask.GetRow.Cells(EnmTasks.ClosingDate).Value
            '    Me.dtpCloseTask.Checked = True
            'End If
            ''Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            'Me.Timer1.Stop()
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.TimeIn).Value) Then
            '    Me.dtpTimeIn.Value = Date.Now
            '    Me.dtpTimeIn.Checked = False
            'Else
            '    Me.dtpTimeIn.Value = GrdTask.GetRow.Cells(EnmTasks.TimeIn).Value
            '    Me.dtpTimeIn.Checked = True
            'End If
            'If IsDBNull(GrdTask.GetRow.Cells(EnmTasks.TimeOut).Value) Then
            '    Me.DtpOutTime.Value = Date.Now
            '    Me.DtpOutTime.Checked = False
            'Else
            '    Me.DtpOutTime.Value = GrdTask.GetRow.Cells(EnmTasks.TimeOut).Value
            '    Me.DtpOutTime.Checked = True
            'End If

            ''Task#2015060024 Transfer Time in and Time in from Grid to Main Form Ali Ansari 
            'Me.txtTaskNo.Text = Me.GrdTask.GetRow.Cells("Task_No").Value.ToString
            'Me.BtnSave.Text = "&Update"
            'Me.BtnDelete.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmTasks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                BtnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmTasks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Project")
            FillCombos("Type")
            FillCombos("TaskStatus")
            FillCombos("Users")
            FillCombos("Customer")
            FillCombos("User")
            FillCombos("ActivityType")
            'Me.FillActivTypeCell()
            'FillCombos("ContactPerson")
            ReSetControls()
            'ApplyActivityGridSettings()
            _dtNotification = GetNotificationActivityConfig(LoginUserId, Me.Name.ToString)
            _dtNotification.AcceptChanges()
            isOpenForm = True
        Catch ex As Exception
            'Throw ex
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try


            'If Me.tcmbViews.Text = "AssignedTo" Then
            '    Me.GrdTask.AutomaticSort = False
            '    Dim gridAssignedTo As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns("AssignedTo"))
            '    gridAssignedTo.GroupPrefix = String.Empty

            '    Me.GrdTask.RootTable.Groups.Add(gridAssignedTo)
            '    'Me.GrdTask.RootTable.Columns("AssignedTo").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count


            '    'Me.GrdTask.GroupTotals.Always() ''= Janus.Windows.GridEX.GroupTotals.Always ''= Me.GrdTask.RootTable.GroupTotals.Always
            'ElseIf Me.tcmbViews.Text = "Type" Then
            '    Me.GrdTask.AutomaticSort = False
            '    Dim gridType As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.Type))
            '    gridType.GroupPrefix = String.Empty
            '    Me.GrdTask.RootTable.Groups.Add(gridType)
            'ElseIf Me.tcmbViews.Text = "My Work" Then
            '    Me.GrdTask.AutomaticSort = False

            '    Dim gridMyWork As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.TaskDate))
            '    gridMyWork.GroupPrefix = String.Empty
            '    Me.GrdTask.RootTable.Groups.Add(gridMyWork)
            'ElseIf Me.tcmbViews.Text = "Customer" Then
            '    Me.GrdTask.AutomaticSort = False
            '    Dim gridCustomer As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.CustomerName))
            '    gridCustomer.GroupPrefix = String.Empty
            '    Me.GrdTask.RootTable.Groups.Add(gridCustomer)
            'ElseIf Me.tcmbViews.Text = "Project" Then
            '    Me.GrdTask.AutomaticSort = False
            '    Dim gridProject As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.CostCenter))
            '    gridProject.GroupPrefix = String.Empty
            '    Me.GrdTask.RootTable.Groups.Add(gridProject)
            'ElseIf Me.tcmbViews.Text = "Status" Then
            '    Me.GrdTask.AutomaticSort = False
            '    Dim gridStatus As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.Status))
            '    gridStatus.GroupPrefix = String.Empty
            '    Me.GrdTask.RootTable.Groups.Add(gridStatus)

            'End If

            Me.GrdTask.RootTable.Columns(EnmTasks.TaskId).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Project).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).Visible = True
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).Visible = True
            Me.GrdTask.RootTable.Columns(EnmTasks.Status).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskType).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskUser).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.AssignedToName).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Customer).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.ClosingDate).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeOut).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.CostCenter).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskNo).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskDate).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Remarks).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.CustomerName).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Active).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.UserName).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeIn).Visible = False
            Me.GrdTask.RootTable.Columns(EnmTasks.Completed).Visible = False
            Me.GrdTask.RootTable.Columns("CreatedBy").Visible = False
            Me.GrdTask.RootTable.Columns("CreatedByID").Visible = False
            Me.GrdTask.RootTable.Columns("LastUpdatedBy").Visible = False
            Me.GrdTask.RootTable.Columns("ContactPersonID").Visible = False
            Me.GrdTask.RootTable.Columns("CustomEndDate").Visible = False
            ' Me.GrdTask.RootTable.Columns(EnmTasks.RefNo).Visible = False
            Me.GrdTask.RootTable.Columns("FormName").Visible = False
            Me.GrdTask.RootTable.Columns("Ref_No").ColumnType = Janus.Windows.GridEX.ColumnType.Link






            Me.GrdTask.RootTable.Columns(EnmTasks.ClosingDate).FormatString = "dd/MMM/yyyy"
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskDate).FormatString = str_DisplayDateFormat
            'Task#2015060024 Add Time in and Time Out in Grid Ali Ansari 
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeIn).Caption = "Time In"
            Me.GrdTask.RootTable.Columns(EnmTasks.Name).Caption = "Title"
            Me.GrdTask.RootTable.Columns(EnmTasks.Name).Width = 500
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).Caption = "Status"
            Me.GrdTask.RootTable.Columns(EnmTasks.AssignedToName).Caption = "Assigned To"
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeOut).Caption = "Time Out"
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeIn).FormatString = "hh:mm:ss"
            Me.GrdTask.RootTable.Columns(EnmTasks.TimeOut).FormatString = "hh:mm:ss"
            'Task#2015060024 Add Time in and Time Out in Grid Ali Ansari 
            'Task#20150801 Hiding User Id Column
            Me.GrdTask.RootTable.Columns(EnmTasks.UserId).Visible = False
            'Me.grdActivity.RootTable.Columns(enmTaskActivity.Description).Width = 500
            '  Me.GrdTask.RootTable.Columns(EnmTasks.AssignedTo).EditType = Janus.Windows.GridEX.EditType.Combo
            'Task#20150801 Hiding User Id Column
            'Me.GrdTask.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GridSettingsOnMyWork(Optional ByVal Condition As String = "")
        Try

            Me.GrdTask.RootTable.Columns("TaskId").Visible = False
            'Me.GrdTask.RootTable.Columns("TaskDate").Visible = False
            'Me.GrdTask.RootTable.Columns(EnmTasks.Type).Visible = True
            Me.GrdTask.RootTable.Columns("TaskDate").Visible = False
            Me.GrdTask.RootTable.Columns("Work Period").Visible = False
            Me.GrdTask.RootTable.Columns("Task_No").Visible = False
            Me.GrdTask.RootTable.Columns("Task Name").Visible = True
            Me.GrdTask.RootTable.Columns("Remarks").Visible = False
            Me.GrdTask.RootTable.Columns("TimeIn").Visible = False
            Me.GrdTask.RootTable.Columns("TimeOut").Visible = False
            Me.GrdTask.RootTable.Columns("Project").Visible = False
            Me.GrdTask.RootTable.Columns("Cost Center").Visible = False
            Me.GrdTask.RootTable.Columns("TaskUser").Visible = False
            Me.GrdTask.RootTable.Columns("Employee Name").Visible = False
            Me.GrdTask.RootTable.Columns("CustomerId").Visible = False
            Me.GrdTask.RootTable.Columns("Customer").Visible = False
            Me.GrdTask.RootTable.Columns("Type").Visible = True
            Me.GrdTask.RootTable.Columns("Task Type").Visible = False
            Me.GrdTask.RootTable.Columns("TaskStatus").Visible = True
            Me.GrdTask.RootTable.Columns("Status").Visible = False
            Me.GrdTask.RootTable.Columns("ClosingDate").Visible = False
            Me.GrdTask.RootTable.Columns("Active").Visible = False
            Me.GrdTask.RootTable.Columns("UserID").Visible = False
            Me.GrdTask.RootTable.Columns("AssignedTo").Visible = True
            Me.GrdTask.RootTable.Columns("CustomEndDate").Visible = True
            Me.GrdTask.RootTable.Columns("AssignedToName").Visible = False

            'Me.GrdTask.RootTable.Columns("AssignedToName").Visible = False
            Me.GrdTask.RootTable.Columns("FormName").Visible = False
            Me.GrdTask.RootTable.Columns("Ref_No").ColumnType = Janus.Windows.GridEX.ColumnType.Link





            Me.GrdTask.RootTable.Columns("ClosingDate").FormatString = "dd/MMM/yyyy"
            Me.GrdTask.RootTable.Columns("TaskDate").FormatString = "dd/MMM/yyyy"
            'Task#2015060024 Add Time in and Time Out in Grid Ali Ansari 
            Me.GrdTask.RootTable.Columns("TimeIn").Caption = "Time In"
            Me.GrdTask.RootTable.Columns("Task Name").Caption = "Title"
            Me.GrdTask.RootTable.Columns("Task Name").Width = 500
            Me.GrdTask.RootTable.Columns("TaskStatus").Caption = "Status"
            Me.GrdTask.RootTable.Columns("AssignedToName").Caption = "Assigned To"
            Me.GrdTask.RootTable.Columns("TimeOut").Caption = "Time Out"
            Me.GrdTask.RootTable.Columns("TimeIn").FormatString = "hh:mm:ss"
            Me.GrdTask.RootTable.Columns("TimeOut").FormatString = "hh:mm:ss"
            'Task#2015060024 Add Time in and Time Out in Grid Ali Ansari 
            'Task#20150801 Hiding User Id Column
            Me.GrdTask.RootTable.Columns("UserID").Visible = False
            If Me.GrdTask.RootTable.Columns.Contains("Completed") Then
                Me.GrdTask.RootTable.Columns("Completed").Visible = False
            End If

            '  Me.GrdTask.RootTable.Columns(EnmTasks.AssignedTo).EditType = Janus.Windows.GridEX.EditType.Combo
            'Task#20150801 Hiding User Id Column
            'Me.GrdTask.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                '    Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else

                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False

                    For i As Integer = 0 To Rights.Count - 1
                        If Rights.Item(i).FormControlName = "View" Then
                        ElseIf Rights.Item(i).FormControlName = "Save" Then
                            If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Update" Then
                            If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Delete" Then
                            Me.BtnDelete.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Print" Then
                            Me.BtnPrint.Enabled = True
                            ''''''''''''''''''''''''
                        ElseIf Rights.Item(i).FormControlName = "View All" Then
                            ViewAll = True
                        End If
                    Next
                End If
            End If
            If (Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save") AndAlso Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnSave.Visible = True
            Else
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.BtnSave.Visible = False
                Else
                    Me.BtnSave.Visible = True
                End If
            End If
        Catch ex As Exception
            'msg_Error(ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New TaskDAL().Delete(Task) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Project" Then
                str = "Select CostCenterId, Name From tblDefCostCenter"
                FillDropDown(Me.CmbProject, str, True)
                FillDropDown(Me.cmbProjectH, str, True)
            ElseIf Condition = "Type" Then
                str = "Select TypeId, Name From TblDefTypes"
                FillDropDown(Me.CmbType, str, True)
                FillDropDown(Me.cmbType2, str, True)
                'FillDropDown(Me.cmbTypeH, str, True)
            ElseIf Condition = "ContactPerson" Then


                str = "Select PK_Id, ContactName, RefCompanyId, Designation, Mobile, Phone, Fax, Email, Address, IndexNo, Type, Company FROM TblCompanyContacts Where RefCompanyId = " & Me.cmbAccounts.Value & ""
                FillUltraDropDown(Me.cmbContactPerson, str)
                FillUltraDropDown(Me.cmbContactPersonH, str)
                If Me.cmbContactPerson.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("RefCompanyId").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Fax").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("IndexNo").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Company").Hidden = True
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("ContactName").Width = 200
                    Me.cmbContactPerson.Rows(0).Activate()

                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("RefCompanyId").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("Fax").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("IndexNo").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("Company").Hidden = True
                    Me.cmbContactPersonH.DisplayLayout.Bands(0).Columns("ContactName").Width = 200
                    Me.cmbContactPersonH.Rows(0).Activate()
                End If
            ElseIf Condition = "ContactPersonH" Then

                'str = "Select PK_Id, ContactName, RefCompanyId, Designation, Mobile, Phone, Fax, Email, Address, IndexNo, Type, Company FROM TblCompanyContacts Where RefCompanyId = " & Me.cmbAccountsH.Value & ""
                'FillUltraDropDown(Me.cmbContactPerson, str)
                'FillUltraDropDown(Me.cmbContactPersonH, str)
                'If Me.cmbContactPerson.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("RefCompanyId").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Fax").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("IndexNo").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Company").Hidden = True
                '    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("ContactName").Width = 200
                '    Me.cmbContactPerson.Rows(0).Activate()
                'End If
                FillCombos("ContactPerson")
            ElseIf Condition = "TaskStatus" Then
                str = "Select StatusId, Name From tblDefStatus"
                FillDropDown(Me.CmbStatus, str, True)
                'FillDropDown(Me.cmbStatusH, str, True)
            ElseIf Condition = "Users" Then
                'str = "Select UserID, UserName From tblSecurityUser"
                str = "Select Employee_ID, Employee_Name,Mobile From tblDefEmployee WHERE IsNull(Active,0)=1"
                FillDropDown(Me.CmbUser, str, True)
                'FillDropDown(Me.cmbEmpoyeeH, str, True)
            ElseIf Condition = "Customer" Then
                str = "Select coa_detail_id, detail_title as [Customer], detail_code as [Code],Account_Type as [Type], Contact_Mobile as [Mobile], Contact_Email as [Email], EndDate From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title ASC"
                FillUltraDropDown(Me.cmbAccounts, str)
                FillUltraDropDown(Me.cmbAccountsH, str)
                Dim strQuery As String = String.Empty
                If Me.cmbAccounts.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Customer").Width = 200
                    Me.cmbAccounts.Rows(0).Activate()

                    Me.cmbAccountsH.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                    Me.cmbAccountsH.DisplayLayout.Bands(0).Columns("Customer").Width = 200
                    Me.cmbAccountsH.Rows(0).Activate()
                End If
            ElseIf Condition = "User" Then
                str = "Select User_ID, User_Name,Email FROM tblUser Where Active <> 0 "
                Dim dt As New DataTable
                Dim dr1 As DataRow
                dt = GetDataTable(str)
                'dr1 = dt.NewRow
                'dr1(0) = Convert.ToInt32(0)
                'dr1(1) = strZeroIndexItem
                'dt.Rows.InsertAt(dr1, 0)
                For Each dr As DataRow In dt.Rows
                    dr.BeginEdit()
                    dr(1) = Decrypt(dr("User_Name"))
                    dr.EndEdit()
                Next

                dr1 = dt.NewRow
                dr1(0) = Convert.ToInt32(0)
                dr1(1) = strZeroIndexItem
                dt.Rows.InsertAt(dr1, 0)
                dt.AcceptChanges()
                Me.cmbAssignedTo.DataSource = dt
                Me.cmbAssignedTo.DisplayMember = "User_Name"
                Me.cmbAssignedTo.ValueMember = "User_ID"

            ElseIf Condition = "ActivityType" Then
                Dim List As New List(Of ActivityType)
                List = New TaskDAL().AllActivityTypes()
                List.Insert(0, New ActivityType() With {.ID = 0, .Description = ".... Select Any Value ...."})
                Me.cmbActivityType.ValueMember = "ID"
                Me.cmbActivityType.DisplayMember = "Description"
                Me.cmbActivityType.DataSource = List
                Me.cmbActivityType.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Task = New Task
            If UltraTabControl1.SelectedTab.Index = 0 Then

                With Task
                    .TaskId = TaskId
                    .TaskDate = Me.dtpDate.Value
                    .TaskName = Me.TxtName.Text
                    .TaskRemarks = Me.TxtRemarks.Text
                    .TaskProject = Me.CmbProject.SelectedValue
                    .TaskType = Me.CmbType.SelectedValue
                    .TaskUser = Me.CmbUser.SelectedValue
                    .TaskStatus = Me.CmbStatus.SelectedValue
                    .Active = IIf(Me.chkActive.Checked = True, 0, 1)
                    .CustomerId = Me.cmbAccounts.Value
                    If IsDBNull(Me.cmbAccounts.SelectedRow.Cells("EndDate").Value) = False Then
                        .CustomEndDate = Me.cmbAccounts.SelectedRow.Cells("EndDate").Value
                    Else
                        .CustomEndDate = Nothing
                    End If
                    .ContactPersonID = Me.cmbContactPerson.Value
                    .AssignedTo = Me.cmbAssignedTo.SelectedValue
                    If (Me.dtpCloseTask.Checked = True) Then
                        .ClosingDate = Me.dtpCloseTask.Value
                    Else
                        .ClosingDate = Nothing
                    End If

                    'Task#2015060024 Fill Model  Time in and Time Out Ali Ansari 
                    .TimeIn = IIf(Me.dtpTimeIn.Checked = False, Nothing, CDate(Me.dtpTimeIn.Value.Date & " " & Me.dtpTimeIn.Value.ToLongTimeString))
                    .TimeOut = IIf(Me.DtpOutTime.Checked = False, Nothing, CDate(Me.DtpOutTime.Value.Date & " " & Me.DtpOutTime.Value.ToLongTimeString))
                    '  .TimeOut = CDate(Me.DtpOutTime.Value.Date & " " & Me.DtpOutTime.Value.ToLongTimeString)
                    'Task#2015060024 Fill Model  Time in and Time Out Ali Ansari 
                    'Altered Against Task#20150801 Fill Model  User Name 
                    .UserId = LoginUserId
                    .CreatedBy = LoginUserName
                    .CreatedByID = LoginUserId
                    .LastUpdatedBy = LoginUserId
                    'Altered Against Task#20150801 Fill Model  User Name 
                    .TaskNo = Me.txtTaskNo.Text
                    .Prefix = "TSK-" & .TaskDate.ToString("MMyy") & "-"
                    .Completed = IIf(Me.chkActive.Checked = True, 1, 0)
                    If Not Ref_No = String.Empty Then
                        .Ref_No = Ref_No
                    Else
                        .Ref_No = String.Empty
                    End If
                    If Not ReferenceForm = String.Empty Then
                        .FormName = ReferenceForm
                    Else
                        .FormName = String.Empty
                    End If
                    .ActivityLog = New ActivityLog
                    .ActivityLog.ApplicationName = "Task"
                    If Me.Name Is Nothing Then
                        .ActivityLog.FormName = ""
                    Else
                        .ActivityLog.FormName = Me.Name.ToString
                    End If
                    .ActivityLog.FormCaption = Me.Text
                    .ActivityLog.UserID = LoginUserId
                    .ActivityLog.LogDateTime = Date.Now


                End With
            Else

                With Task
                    .TaskId = TaskId
                    .TaskDate = Date.Now
                    .TaskName = Me.txtTitle2.Text.ToString.Replace("'", "''")
                    .TaskRemarks = String.Empty
                    .TaskProject = Nothing
                    .TaskType = Me.cmbType2.SelectedValue
                    .TaskUser = Nothing
                    .TaskStatus = 1
                    .Active = Me.chkActive.Checked
                    .CustomerId = 0
                    .AssignedTo = LoginUserId

                    .ClosingDate = Nothing


                    'Task#2015060024 Fill Model  Time in and Time Out Ali Ansari 
                    .TimeIn = Nothing ''IIf(Me.dtpTimeIn.Checked = False, Nothing, CDate(Me.dtpTimeIn.Value.Date & " " & Me.dtpTimeIn.Value.ToLongTimeString))
                    .TimeOut = Nothing ''IIf(Me.DtpOutTime.Checked = False, Nothing, CDate(Me.DtpOutTime.Value.Date & " " & Me.DtpOutTime.Value.ToLongTimeString))
                    '  .TimeOut = CDate(Me.DtpOutTime.Value.Date & " " & Me.DtpOutTime.Value.ToLongTimeString)
                    'Task#2015060024 Fill Model  Time in and Time Out Ali Ansari 
                    'Altered Against Task#20150801 Fill Model  User Name 
                    .UserId = LoginUserId
                    .CreatedBy = LoginUserName
                    .CreatedByID = LoginUserId
                    .LastUpdatedBy = LoginUserName
                    'Altered Against Task#20150801 Fill Model  User Name 
                    .TaskNo = Me.txtTaskNo.Text
                    If Not Ref_No = String.Empty Then
                        .Ref_No = Ref_No
                    Else
                        .Ref_No = String.Empty
                    End If
                    If Not ReferenceForm = String.Empty Then
                        .FormName = ReferenceForm
                    Else
                        .FormName = String.Empty
                    End If
                    .Prefix = "TSK-" & .TaskDate.ToString("MMyy") & "-"
                    .ActivityLog = New ActivityLog
                    .ActivityLog.ApplicationName = "Task"
                    .ActivityLog.FormName = Me.Name.ToString
                    .ActivityLog.FormCaption = Me.Text
                    .ActivityLog.UserID = LoginUserId
                    .ActivityLog.LogDateTime = Date.Now


                End With
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetReferenceTasks(ByVal ref_No As String)
        Try
            Me.GrdTask.DataSource = New TaskDAL().GetReferenceTasks(ref_No)
            Me.GrdTask.RetrieveStructure()

            Dim Str As String = String.Empty
            Str = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
            Dim dt1 As New DataTable
            dt1 = GetDataTable(Str)
            For Each dr As DataRow In dt1.Rows
                dr.BeginEdit()
                dr(1) = Decrypt(dr("User_Name"))
                dr.EndEdit()
            Next
            dt1.AcceptChanges()
            Me.GrdTask.RootTable.Columns("AssignedTo").HasValueList = True
            Me.GrdTask.RootTable.Columns("AssignedTo").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("AssignedTo").ValueList.PopulateValueList(dt1.DefaultView, "User_ID", "User_Name")
            Dim typeStr As String = String.Empty
            typeStr = "Select TypeId, Name From TblDefTypes"
            Dim dt2 As New DataTable
            dt2 = GetDataTable(typeStr)
            dt2.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).ValueList.PopulateValueList(dt2.DefaultView, "TypeId", "Name")
            Dim status As String = String.Empty
            status = "Select StatusId, Name From tblDefStatus"
            Dim dt3 As New DataTable
            dt3 = GetDataTable(status)
            dt3.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).ValueList.PopulateValueList(dt3.DefaultView, "StatusId", "Name")
            Me.ApplyGridSettings()











        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function CountReferenceTasks(ByVal ref_No As String) As Integer
        Try
            Return New TaskDAL().CountTasksAgainstDocument(ref_No)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            'Marked Against Task#20150801 Making Employee Wise Selection 
            'Me.GrdTask.DataSource = New TaskDAL().GetAllRecords(cond)
            'Marked Against Task#20150801 Making Employee Wise Selection 

            'Altered Against Task#20150801 Making Employee Wise Selection 
            Dim cond As String = String.Empty

            cond = IIf(ViewAll = True, "", "Where tbldeftasks.UserId = " & LoginUserId & "")

            Me.GrdTask.DataSource = New TaskDAL().GetAllRecords(cond)
            Me.GrdTask.RetrieveStructure()

            Dim Str As String = String.Empty
            Str = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
            Dim dt1 As New DataTable
            dt1 = GetDataTable(Str)
            For Each dr As DataRow In dt1.Rows
                dr.BeginEdit()
                dr(1) = Decrypt(dr("User_Name"))
                dr.EndEdit()
            Next
            dt1.AcceptChanges()
            Me.GrdTask.RootTable.Columns("AssignedTo").HasValueList = True
            Me.GrdTask.RootTable.Columns("AssignedTo").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("AssignedTo").ValueList.PopulateValueList(dt1.DefaultView, "User_ID", "User_Name")
            Dim typeStr As String = String.Empty
            typeStr = "Select TypeId, Name From TblDefTypes"
            Dim dt2 As New DataTable
            dt2 = GetDataTable(typeStr)
            dt2.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).ValueList.PopulateValueList(dt2.DefaultView, "TypeId", "Name")
            Dim status As String = String.Empty
            status = "Select StatusId, Name From tblDefStatus"
            Dim dt3 As New DataTable
            dt3 = GetDataTable(status)
            dt3.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).ValueList.PopulateValueList(dt3.DefaultView, "StatusId", "Name")
            Me.ApplyGridSettings()



            'Altered Against Task#20150801 Making Employee Wise Selection 







        Catch ex As Exception
            Throw ex
        End Try


    End Sub


    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            Dim EmptyFields As String = String.Empty
            If EmptyFields = (Me.TxtName.Text) Then
                ShowErrorMessage("Please Enter Task")
                Return False
            End If
            If Me.CmbType.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select a task type.")
                Me.CmbType.Focus()
                Return False
            End If
            If Me.CmbStatus.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select a task status")
                Me.CmbStatus.Focus()
                Return False
            End If
            FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetSecurityRights()
            If Not Ref_No = String.Empty AndAlso Not ReferenceForm = String.Empty Then
                Me.GetReferenceTasks(Ref_No)
            Else
                Me.GetAllRecords()
            End If
            'Me.GetAllActivities()

            Me.dtpDate.Value = Date.Now

            Me.txtTaskNo.Text = TaskDAL.GetNextDocNo("TSK-" & Me.dtpDate.Value.ToString("MMyy") & "-").ToString
            TxtName.Text = String.Empty
            TxtRemarks.Text = String.Empty
            txtTitle2.Text = String.Empty
            Me.cmbType2.Focus()
            Me.cmbType2.SelectedIndex = 0
            Me.cmbActivityType.Rows(0).Activate()
            CmbProject.SelectedIndex = 0
            'tcmbViews.SelectedIndex = 0
            'CmbUser.SelectedIndex = 0
            CmbType.SelectedIndex = 0
            'Me.cmbContactPerson.
            Me.tsslCreatedBy.Text = "Creater: " & " " & LoginUserName

            'Me.cmbType1.SelectedIndex = 0
            Me.lblLastUpdatedBy.Text = ""
            Me.BtnSave.Text = "&Save"
            Me.BtnSave.Enabled = True
            Me.BtnDelete.Enabled = False
            Me.dtpCloseTask.Value = Date.Now
            Me.dtpCloseTask.Checked = False
            'Task#2015060024 Reset  Time in and Time Out Ali Ansari 
            Me.dtpTimeIn.Value = Date.Now
            Me.dtpTimeIn.Checked = False
            Me.DtpOutTime.Value = Date.Now
            Me.DtpOutTime.Checked = False
            If Not Me.cmbAssignedTo.SelectedValue = -1 Then Me.cmbAssignedTo.SelectedValue = 0
            If Not Me.CmbUser.SelectedIndex = -1 Then Me.CmbUser.SelectedIndex = 0
            If Not Me.CmbProject.SelectedIndex = -1 Then Me.CmbProject.SelectedIndex = 0
            If Not Me.CmbType.SelectedIndex = -1 Then Me.CmbType.SelectedIndex = 0
            If Not Me.CmbStatus.SelectedIndex = -1 Then Me.CmbStatus.SelectedIndex = 0
            If Not Me.cmbType2.SelectedIndex = -1 Then Me.cmbType2.SelectedIndex = 0
            'If Not Me.tcmbViews.SelectedIndex = -1 Then Me.tcmbViews.SelectedIndex = 0
            'Task#2015060024 Reset  Time in and Time Out Ali Ansari 
            Me.Timer1.Start()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New TaskDAL().Add(Task) Then
                'SaveNotification()
                Return True
            End If

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
            If New TaskDAL().Update(Task) Then
                SaveNotification()
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If UltraTabControl1.SelectedTab.Index = 0 Then


                If Not IsValidate() Then Exit Sub
                'If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
                If Me.BtnSave.Text = "&Save" Then
                    If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                    'MsgBox("Record Successfully Saved", MsgBoxStyle.Information, str_MessageHeader)
                    'msg_Information(str_informSave)
                Else

                    If Not msg_Confirm(str_ConfirmUpdate) Then Exit Sub
                    If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                    'MsgBox("Record Successfully Update", MsgBoxStyle.Information, str_MessageHeader)
                    'msg_Information(str_informUpdate)
                End If
                If msg_Confirm(str_ConfirmSendSMSMessage) = True Then
                    SendSMS()
                End If

                Me.ReSetControls()
                Me.GetAllRecords()
            Else
                If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'Me.ReSetControls()

                Me.GetAllRecords()
                Me.txtTitle2.Text = String.Empty
                Me.txtTitle2.Focus()
            End If
            If Updatemode = True Then
                UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(1).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            TaskEditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub GrdTask_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdTask.CellEdited

        Dim ActivityLog As ActivityLog
        Dim taskID As Integer = 0
        Dim assignedTo As Integer = 0
        Dim title As String = String.Empty
        Dim status As Integer = 0
        Dim type As Integer = 0
        Dim TaskNo As String = String.Empty

        Dim taskDAL As TaskDAL
        Try

            'If Me.GrdTask.RootTable.Columns(EnmTasks.AssignedTo).E Then

            'End If
            'Me.Cursor = Cursors.WaitCursor
            'Me.lblProgress.Text = "Processing please wait ..."
            'Me.lblProgress.Visible = True
            'Application.DoEvents()

            If Me.GrdTask.GetRow().Cells(EnmTasks.AssignedTo).DataChanged = True Then
                Me.Cursor = Cursors.WaitCursor
                Me.lblProgress.Text = "Processing please wait ..."
                Me.lblProgress.BackColor = Color.LightYellow
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.GrdTask.UpdateData()
                taskID = Me.GrdTask.GetRow().Cells(EnmTasks.TaskId).Value
                assignedTo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.AssignedTo).Value
                TaskNo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskNo).Value.ToString()
                ActivityLog = New ActivityLog
                ActivityLog.ApplicationName = "Task"
                ActivityLog.FormCaption = Me.Text
                ActivityLog.UserID = LoginUserId
                ActivityLog.RefNo = TaskNo
                ActivityLog.FormName = Me.Name.ToString
                ActivityLog.LogDateTime = Date.Now
                taskDAL = New TaskDAL()
                taskDAL.UpdateAssignedTo(ActivityLog, taskID, assignedTo)
            ElseIf Me.GrdTask.GetRow().Cells(EnmTasks.Name).DataChanged = True Then
                Me.Cursor = Cursors.WaitCursor
                Me.lblProgress.Text = "Processing please wait ..."
                Me.lblProgress.BackColor = Color.LightYellow
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.GrdTask.UpdateData()
                taskID = Me.GrdTask.GetRow().Cells(EnmTasks.TaskId).Value
                title = Me.GrdTask.GetRow().Cells().Item(EnmTasks.Name).Value
                TaskNo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskNo).Value.ToString()
                ActivityLog = New ActivityLog
                ActivityLog.ApplicationName = "Task"
                ActivityLog.FormCaption = Me.Text
                ActivityLog.RefNo = TaskNo
                ActivityLog.FormName = Me.Name.ToString
                ActivityLog.UserID = LoginUserId
                ActivityLog.LogDateTime = Date.Now
                taskDAL = New TaskDAL()
                taskDAL.UpdateTitleCell(ActivityLog, taskID, title)
            ElseIf Me.GrdTask.GetRow().Cells(EnmTasks.Type).DataChanged = True Then
                Me.Cursor = Cursors.WaitCursor
                Me.lblProgress.Text = "Processing please wait ..."
                Me.lblProgress.BackColor = Color.LightYellow
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.GrdTask.UpdateData()
                taskID = Me.GrdTask.GetRow().Cells(EnmTasks.TaskId).Value
                type = Me.GrdTask.GetRow().Cells().Item(EnmTasks.Type).Value
                TaskNo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskNo).Value.ToString()
                ActivityLog = New ActivityLog
                ActivityLog.ApplicationName = "Task"
                ActivityLog.FormCaption = Me.Text
                ActivityLog.UserID = LoginUserId
                ActivityLog.RefNo = TaskNo
                ActivityLog.FormName = Me.Name.ToString
                ActivityLog.LogDateTime = Date.Now
                taskDAL = New TaskDAL()
                taskDAL.UpdateTypeCell(ActivityLog, taskID, type)
            ElseIf Me.GrdTask.GetRow().Cells(EnmTasks.TaskStatus).DataChanged = True Then
                Me.Cursor = Cursors.WaitCursor
                Me.lblProgress.Text = "Processing please wait ..."
                Me.lblProgress.Visible = True
                Me.lblProgress.BackColor = Color.LightYellow
                Application.DoEvents()
                Me.GrdTask.UpdateData()
                taskID = Me.GrdTask.GetRow().Cells(EnmTasks.TaskId).Value
                status = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskStatus).Value
                TaskNo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskNo).Value.ToString()
                ActivityLog = New ActivityLog
                ActivityLog.ApplicationName = "Task"
                ActivityLog.FormCaption = Me.Text
                ActivityLog.UserID = LoginUserId
                ActivityLog.RefNo = TaskNo
                ActivityLog.FormName = Me.Name.ToString
                ActivityLog.LogDateTime = Date.Now
                taskDAL = New TaskDAL()
                taskDAL.UpdateStatusCell(ActivityLog, taskID, status)
            End If

            'Me.lblProgress.Text = "Loading Please Wait ..."
            'Me.lblProgress.BackColor = Color.LightYellow
            'Me.lblProgress.Visible = True
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub GrdTask_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdTask.RowDoubleClick
        Try
            If Me.GrdTask.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                Me.TaskEditRecord()
                'Me.ApplyGridSettings()
                Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab

            End If
            ' GrdTask.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Not IsValidate() Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            If Delete() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.ReSetControls()
            Me.GetAllRecords()
            'MsgBox("Record Delete Successfully", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informDelete)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            'AddRptParam("@TaskId", Val(Me.GrdTask.GetRow.Cells("TaskId").Value.ToString))
            If GrdTask.RowCount = 0 Then Exit Sub
            ShowReport("rptTasks", "{SP_Tasks;1.TaskId}=" & Val(Me.GrdTask.GetRow.Cells(EnmTasks.TaskId).Value.ToString) & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True

                Me.BtnPrint.Enabled = True
                ViewAll = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "View All" Then
                        ViewAll = True

                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0I
            Id = Me.CmbProject.SelectedValue
            FillCombos("Project")
            Me.CmbProject.SelectedValue = Id
            Id = Me.CmbStatus.SelectedValue
            FillCombos("TaskStatus")
            Me.CmbStatus.SelectedValue = Id
            Id = Me.CmbType.SelectedValue
            FillCombos("Type")
            Me.CmbType.SelectedValue = Id
            Id = Me.CmbUser.SelectedValue
            FillCombos("Users")
            Me.CmbUser.SelectedValue = Id
            Id = Me.cmbAccounts.ActiveRow.Cells(0).Value
            FillCombos("Customer")
            GetAllRecords()
            Me.cmbAccounts.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@TaskNo")
            str.Add("@TaskTitle")
            str.Add("@TaskDate")
            str.Add("@Remarks")
            str.Add("@Project")
            str.Add("@Employee")
            str.Add("@Customer")
            str.Add("@Type")
            str.Add("@Status")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Customer")
            str.Add("Employee")
            str.Add("Closing")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region
    Public Sub SendSMS()
        Try


            If GetSMSConfig("Task").Enable = True Then
                Dim objTemp As New SMSTemplateParameter

                Dim obj As Object = Nothing

                If Me.CmbUser.SelectedIndex <= 0 Then
                    If Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Then
                        obj = GetSMSTemplate("Customer")
                    End If
                End If

                If Me.dtpCloseTask.Checked = False Then
                    If Me.CmbUser.SelectedIndex > 0 Then
                        obj = GetSMSTemplate("Employee")
                    End If
                End If

                If Me.dtpCloseTask.Checked = True Then
                    obj = GetSMSTemplate("Closing")
                End If


                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbAccounts.ActiveRow.Cells("Customer").Value.ToString).Replace("@AccountCode", Me.cmbAccounts.ActiveRow.Cells("Code").Value.ToString).Replace("@TaskTitle", Me.TxtName.Text).Replace("@TaskDate", Me.dtpDate.Value.ToShortDateString).Replace("@Remark", Me.TxtRemarks.Text).Replace("@Project", IIf(Me.CmbProject.SelectedIndex > 0, "" & Me.CmbProject.Text & "", "")).Replace("@Employee", IIf(Me.CmbUser.SelectedIndex > 0, "" & Me.CmbUser.Text & "", "")).Replace("@Customer", IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, "" & Me.cmbAccounts.ActiveRow.Cells("Customer").Value.ToString & "", "")).Replace("@Type", IIf(Me.CmbType.SelectedIndex > 0, "" & Me.CmbType.Text & "", "")).Replace("@Status", IIf(Me.CmbStatus.SelectedIndex > 0, "" & Me.CmbStatus.Text & "", "")).Replace("@SIRIUS", "Automated by www.siriussolution.com").Replace("@TaskNo", Me.txtTaskNo.Text)

                    If Me.CmbUser.SelectedIndex <= 0 Then
                        If Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Then
                            SaveSMSLog(strMessage, Me.cmbAccounts.ActiveRow.Cells("Mobile").Value, "Customer Task")
                        End If
                    End If

                    If Me.dtpCloseTask.Checked = False Then
                        If Me.CmbUser.SelectedIndex > 0 Then
                            SaveSMSLog(strMessage, CType(Me.CmbUser.SelectedItem, DataRowView).Row.Item("Mobile").ToString, "Employee Task")
                        End If
                    End If

                    If Me.dtpCloseTask.Checked = True Then
                        SaveSMSLog(strMessage, Me.cmbAccounts.ActiveRow.Cells("Mobile").Value, "Customer Task")
                    End If
                    'If (Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Or Me.dtpCloseTask.Checked = True) Then
                    '    SaveSMSLog(strMessage, Me.cmbAccounts.ActiveRow.Cells("Mobile").Value, "Customer Task")
                    'End If
                    'If Me.CmbUser.SelectedIndex > 0 Then
                    '    SaveSMSLog(strMessage, CType(Me.CmbUser.SelectedItem, DataRowView).Row.Item("Mobile").ToString, "Employee Task")
                    'End If


                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        Try
            Me.txtTaskNo.Text = TaskDAL.GetNextDocNo("TSK-" & Me.dtpDate.Value.ToString("MMyy") & "-")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmTasks_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'Me.dtpTimespent.CustomFormat = "hh:mm:ss"


            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            Me.DtpOutTime.Checked = False  'Out Time Disabled by default Ali Ansari Task#2015060024

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.lblProgress1.Text = "Loading Please Wait ..."
            Me.lblProgress1.BackColor = Color.LightYellow
            Me.lblProgress1.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
            Me.lblProgress1.Visible = False
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.dtpTimeIn.Value = Date.Now
            Me.dtpTimeIn.Checked = False
            Me.DtpOutTime.Value = Date.Now
            Me.DtpOutTime.Checked = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DtpOutTime_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtpOutTime.ValueChanged, dtpTimeIn.ValueChanged
        Try
            Me.Timer1.Stop()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub



    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        'Try
        '    If Me.CmbType.SelectedIndex <> 0 Then
        '        FillModel()
        '        BtnSave_Click(Nothing, Nothing)
        '        GetAllRecords()
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try



    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Try
        '    If Me.cmbType2.SelectedIndex <> 0 AndAlso Me.txtTitle2.Text <> String.Empty Then
        '        Me.lblProgress.Text = "Processing please wait ..."
        '        Me.lblProgress.Visible = True
        '        Application.DoEvents()
        '        FillModel()
        '        BtnSave_Click(Nothing, Nothing)


        '        'ReSetControls()
        '    Else
        '        If Me.cmbType2.SelectedIndex = 0 Then
        '            msg_Error("Type is required")
        '        Else
        '            msg_Error("Title is required")
        '        End If

        '    End If

        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    Me.Cursor = Cursors.Default
        '    Me.lblProgress.Visible = False
        'End Try


    End Sub



    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If UltraTabControl1.SelectedTab.Index = 1 Then
                Me.cmbType2.SelectedIndex = 0
                Me.cmbType2.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GrdTask_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdTask.CellValueChanged
        'Dim ActivityLog As ActivityLog
        'Dim taskID As Integer = 0
        'Dim assignedTo As Integer = 0
        'Dim title As String = String.Empty
        'Dim status As Integer = 0
        'Dim type As Integer = 0

        'Dim taskDAL As TaskDAL
        'Try
        '    Me.GrdTask.UpdateData()
        '    taskID = Me.GrdTask.GetRow().Cells(EnmTasks.TaskId).Value
        '    assignedTo = Me.GrdTask.GetRow().Cells().Item(EnmTasks.AssignedTo).Value
        '    title = Me.GrdTask.GetRow().Cells().Item(EnmTasks.Name).Value.ToString()
        '    type = Me.GrdTask.GetRow().Cells().Item(EnmTasks.Type).Value
        '    status = Me.GrdTask.GetRow().Cells().Item(EnmTasks.TaskStatus).Value
        '    Me.GrdTask.RootTable.Columns(EnmTasks.Type).Visible = False
        '    Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).Visible = False
        '    ActivityLog = New ActivityLog
        '    ActivityLog.ApplicationName = "Task"
        '    ActivityLog.FormCaption = Me.Text
        '    ActivityLog.UserID = LoginUserId
        '    ActivityLog.LogDateTime = Date.Now
        '    taskDAL = New TaskDAL()
        '    taskDAL.UpdateAssignedTo(ActivityLog, taskID, assignedTo, title, status, type)
        '    Me.GetAllRecords()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub txtTitle2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTitle2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.txtTitle2.Focus()
        End If
    End Sub

    Private Sub txtTitle2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTitle2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'btnAdd_Click(Me, Nothing)
                btnAdd2_Click(Me, Nothing)

                'If Me.cmbType1.SelectedIndex <> 0 Then
                '    FillModel()
                '    BtnSave_Click(Nothing, Nothing)
                '    GetAllRecords()

                'Else
                '    msg_Error("Type is required")
                'End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
        Try
            If Me.cmbType2.SelectedIndex <> 0 AndAlso Me.txtTitle2.Text <> String.Empty Then
                Me.lblProgress.Text = "Processing please wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                FillModel()
                BtnSave_Click(Nothing, Nothing)
                If Not Ref_No = String.Empty AndAlso Not ReferenceForm = String.Empty Then
                    Me.GetReferenceTasks(Ref_No)
                Else
                    Me.GetAllRecords()
                End If
            Else
                If Me.cmbType2.SelectedIndex = 0 Then
                    msg_Error("Type is required")
                Else
                    msg_Error("Title is required")
                End If

            End If

        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub FillGridCombos()
        Try
            Dim Str As String = String.Empty
            Str = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
            Dim dt1 As New DataTable
            dt1 = GetDataTable(Str)
            For Each dr As DataRow In dt1.Rows
                dr.BeginEdit()
                dr(1) = Decrypt(dr("User_Name"))
                dr.EndEdit()
            Next
            dt1.AcceptChanges()
            Me.GrdTask.RootTable.Columns("AssignedTo").HasValueList = True
            Me.GrdTask.RootTable.Columns("AssignedTo").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("AssignedTo").ValueList.PopulateValueList(dt1.DefaultView, "User_ID", "User_Name")
            Dim typeStr As String = String.Empty
            typeStr = "Select TypeId, Name From TblDefTypes"
            Dim dt2 As New DataTable
            dt2 = GetDataTable(typeStr)
            dt2.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.Type).ValueList.PopulateValueList(dt2.DefaultView, "TypeId", "Name")
            Dim status As String = String.Empty
            status = "Select StatusId, Name From tblDefStatus"
            Dim dt3 As New DataTable
            dt3 = GetDataTable(status)
            dt3.AcceptChanges()
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).HasValueList = True
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns(EnmTasks.TaskStatus).ValueList.PopulateValueList(dt3.DefaultView, "StatusId", "Name")
            'Me.ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillGridCombosForMyWork()
        Try
            Dim Str As String = String.Empty
            Str = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
            Dim dt1 As New DataTable
            dt1 = GetDataTable(Str)
            For Each dr As DataRow In dt1.Rows
                dr.BeginEdit()
                dr(1) = Decrypt(dr("User_Name"))
                dr.EndEdit()
            Next
            dt1.AcceptChanges()
            Me.GrdTask.RootTable.Columns("AssignedTo").HasValueList = True
            Me.GrdTask.RootTable.Columns("AssignedTo").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("AssignedTo").ValueList.PopulateValueList(dt1.DefaultView, "User_ID", "User_Name")
            Dim typeStr As String = String.Empty
            typeStr = "Select TypeId, Name From TblDefTypes"
            Dim dt2 As New DataTable
            dt2 = GetDataTable(typeStr)
            dt2.AcceptChanges()
            Me.GrdTask.RootTable.Columns("Type").HasValueList = True
            Me.GrdTask.RootTable.Columns("Type").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("Type").ValueList.PopulateValueList(dt2.DefaultView, "TypeId", "Name")
            Dim status As String = String.Empty
            status = "Select StatusId, Name From tblDefStatus"
            Dim dt3 As New DataTable
            dt3 = GetDataTable(status)
            dt3.AcceptChanges()
            Me.GrdTask.RootTable.Columns("TaskStatus").HasValueList = True
            Me.GrdTask.RootTable.Columns("TaskStatus").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.GrdTask.RootTable.Columns("TaskStatus").ValueList.PopulateValueList(dt3.DefaultView, "StatusId", "Name")
            'Me.ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Dim Id As Integer = 0I
            'Id = Me.CmbProject.SelectedValue
            'FillCombos("Project")
            'Me.CmbProject.SelectedValue = Id
            'Id = Me.CmbStatus.SelectedValue
            'FillCombos("TaskStatus")
            'Me.CmbStatus.SelectedValue = Id
            Id = Me.cmbType2.SelectedValue
            FillCombos("Type")
            Me.cmbType2.SelectedValue = Id
            'Id = Me.CmbUser.SelectedValue
            'FillCombos("Users")
            'Me.CmbUser.SelectedValue = Id
            'Id = Me.cmbAccounts.ActiveRow.Cells(0).Value
            'FillCombos("Customer")
            If Not Ref_No = String.Empty Then
                Me.GetReferenceTasks(Ref_No)
            Else
                Me.GetAllRecords()
            End If

            'Me.cmbAccounts.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub tcmbViews_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcmbViews.SelectedIndexChanged
    '    Try
    '        If Me.tcmbViews.Text = "AssignedTo" Then
    '            GrdTask.DataSource = New TaskDAL().GetAssignedToWise()
    '            Me.GrdTask.RetrieveStructure()

    '            Me.ReSetControls()
    '            'Me.GrdTask.AutomaticSort = False
    '            'Dim gridAssignedTo As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns("AssignedTo"))
    '            'gridAssignedTo.GroupPrefix = String.Empty
    '        ElseIf Me.tcmbViews.Text = "Type" Then
    '            GrdTask.DataSource = New TaskDAL().GetTypeWise()
    '            Me.GrdTask.RetrieveStructure()
    '            Me.ReSetControls()
    '        ElseIf Me.tcmbViews.Text = "My Work" Then
    '            Me.GrdTask.DataSource = New TaskDAL().GetMyWork(LoginUserId)
    '            Me.GrdTask.RetrieveStructure()
    '            Me.ReSetControls()
    '        ElseIf Me.tcmbViews.Text = "Customer" Then
    '            GrdTask.DataSource = New TaskDAL().GetCustomerWise()
    '            Me.GrdTask.RetrieveStructure()
    '            Me.ReSetControls()
    '        ElseIf Me.tcmbViews.Text = "Project" Then
    '            GrdTask.DataSource = New TaskDAL().GetProjectWise()
    '            Me.GrdTask.RetrieveStructure()
    '            Me.ReSetControls()
    '        ElseIf Me.tcmbViews.Text = "Status" Then
    '            GrdTask.DataSource = New TaskDAL().GetStatusWise()
    '            Me.GrdTask.RetrieveStructure()
    '            Me.ReSetControls()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub tsmAssignedTo_Click(sender As Object, e As EventArgs) Handles tsmAssignedTo.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            GrdTask.DataSource = New TaskDAL().GetAssignedToWise(IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()
            Me.GrdTask.AutomaticSort = False
            Dim gridAssignedTo As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns("AssignedTo"))
            gridAssignedTo.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridAssignedTo)

            'Me.GrdTask.RootTable.Colu
            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub tsmType_Click(sender As Object, e As EventArgs) Handles tsmType.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            GrdTask.DataSource = New TaskDAL().GetTypeWise(IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()

            Me.GrdTask.AutomaticSort = False
            Dim gridType As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.Type))
            gridType.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridType)

            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub tsmMywork_Click(sender As Object, e As EventArgs) Handles tsmMywork.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Me.GrdTask.DataSource = New TaskDAL().GetMyWork(LoginUserId, IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            'FillGridCombos()
            FillGridCombosForMyWork()

            Me.GrdTask.AutomaticSort = False
            Dim gridTaskDate As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns("Work Period"))
            gridTaskDate.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns("Task Name").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridTaskDate)

            Me.GridSettingsOnMyWork()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub tsmCustomer_Click(sender As Object, e As EventArgs) Handles tsmCustomer.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Me.GrdTask.DataSource = New TaskDAL().GetCustomerWise(IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()

            Me.GrdTask.AutomaticSort = False
            Dim gridCustomer As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.CustomerName))
            gridCustomer.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridCustomer)

            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False

        End Try

    End Sub

    Private Sub tsmProject_Click(sender As Object, e As EventArgs) Handles tsmProject.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Me.GrdTask.DataSource = New TaskDAL().GetProjectWise(IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()

            Me.GrdTask.AutomaticSort = False
            Dim gridProject As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.CostCenter))
            gridProject.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridProject)

            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub tsmStatus_Click(sender As Object, e As EventArgs) Handles tsmStatus.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.GrdTask.DataSource = New TaskDAL().GetStatusWise(IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()

            Me.GrdTask.AutomaticSort = False
            Dim gridStatus As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.Status))
            gridStatus.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridStatus)
            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub DefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultToolStripMenuItem.Click
        Try
            If Not Ref_No = String.Empty Then
                Me.GetReferenceTasks(Ref_No)
            Else
                Me.GetAllRecords()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GrdTask_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdTask.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.GrdTask.RowCount = 0 Then Exit Sub
            If e.Column.Key = "Ref_No" Then
                If Me.GrdTask.GetRow.Cells(e.Column.Index).Value.ToString <> "" Then
                    frmModProperty.Tags = String.Empty
                    frmModProperty.Tags = Me.GrdTask.GetRow.Cells("Ref_No").Text
                    If IsDrillDown = True Then
                        frmMain.LoadControl(Me.GrdTask.GetRow.Cells("FormName").Text.ToString)
                        System.Threading.Thread.Sleep(500)
                    Else
                        frmModProperty.Tags = String.Empty
                        frmMain.LoadControl(Me.GrdTask.GetRow.Cells("FormName").Text.ToString)
                        System.Threading.Thread.Sleep(500)
                        frmModProperty.Tags = Me.GrdTask.GetRow.Cells("Ref_No").Text
                        frmMain.LoadControl(Me.GrdTask.GetRow.Cells("FormName").Text.ToString)
                        System.Threading.Thread.Sleep(500)
                    End If
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub SaveNotification()
        Try
            Dim strArray() As String = {"", "", "", ""} '{"Create New Task", "Completed Task", "Assigned New Task", "Overdue Task"}
            If BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                strArray.SetValue("Create New Task", 0)
            End If
            If chkActive.Checked = True Then
                strArray.SetValue("Completed Task", 1)
            End If
            If Task.AssignedTo > 0 Then
                strArray.SetValue("Assigned New Task", 2)
            End If

            If dtpCloseTask.Checked = True Then
                strArray.SetValue("Due Task", 3)
            End If

            If strArray.Length > 0 Then
                For Each Str As String In strArray
                    If Str.Length > 0 Then
                        Dim dr() As DataRow = _dtNotification.Select("NotificationActivityName='" & Str & "'")
                        If dr.Length > 0 Then
                            SaveNotifications(dr(0), Task.TaskNo, Task.TaskDate, Task.TaskRemarks, IIf(strArray(2).Length > 0, Task.AssignedTo, 0))
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbContactPerson_EditorButtonClick(sender As Object, e As Win.UltraWinEditors.EditorButtonEventArgs) Handles cmbContactPerson.EditorButtonClick
        Try
            ApplyStyleSheet(frmCompanyContacts)
            frmCompanyContacts.ReferenceID = Me.cmbAccounts.Value
            frmCompanyContacts.StartPosition = FormStartPosition.CenterScreen
            frmCompanyContacts.BringToFront()
            frmCompanyContacts.ShowDialog()
            FillCombos("ContactPerson")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Private Sub cmbAccounts_ValueChanged(sender As Object, e As EventArgs) Handles cmbAccounts.ValueChanged
    '    Try
    '        'If Me.cmbAccounts.IsItemInList = False Then Exit Sub
    '        'If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
    '        'FillCombos("ContactPerson")
    '        'Dim C = "Customer End Date:"
    '        ''C = System.Drawing.Color.Navy.ToString()
    '        'If Not IsDBNull(Me.cmbAccounts.SelectedRow.Cells("EndDate").Value) Then
    '        '    lblCustomerEndDate.Text = C + " " + Me.cmbAccounts.SelectedRow.Cells("EndDate").Value.ToString
    '        'End If
    '        ''lblCustomerEndDate.ForeColor = Color.Blue
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub GrdTask_SelectionChanged(sender As Object, e As EventArgs) Handles GrdTask.SelectionChanged
        Try
            If isOpenForm = False Then Exit Sub
            If Me.GrdTask.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                TaskBottonDisplayRecord()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailToolStripMenuItem.Click
        Try
            Me.SplitContainer1.Panel2Collapsed = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OffToolStripMenuItem.Click
        Try
            Me.SplitContainer1.Panel2Collapsed = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbContactPersonH_EditorButtonClick(sender As Object, e As Win.UltraWinEditors.EditorButtonEventArgs) Handles cmbContactPersonH.EditorButtonClick
        Try
            ApplyStyleSheet(frmCompanyContacts)
            frmCompanyContacts.ReferenceID = Me.cmbAccountsH.Value
            frmCompanyContacts.StartPosition = FormStartPosition.CenterScreen
            frmCompanyContacts.BringToFront()
            frmCompanyContacts.ShowDialog()
            FillCombos("ContactPersonH")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccountsH_ValueChanged(sender As Object, e As EventArgs) Handles cmbAccountsH.ValueChanged
        Try
            If cmbAccountsH.IsItemInList = False Then Exit Sub
            If cmbAccountsH.ActiveRow Is Nothing Then Exit Sub
            FillCombos("ContactPersonH")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdActivity_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdActivity.CellEdited
        Dim ID As Integer = 0
        Dim Description As String = String.Empty
        Dim TypeID As Integer = 0
        Dim TypeDescription As String = String.Empty
        Dim Dated As New DateTime
        Dim Time As String = String.Empty
        Dim TaskDAL As TaskDAL
        Try
            If Me.grdActivity.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.grdActivity.GetRow().Cells(enmTaskActivity.Description).DataChanged = True Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.lblProgress1.Text = "Processing please wait ..."
                    Me.lblProgress1.Visible = True
                    Application.DoEvents()
                    Me.grdActivity.UpdateData()
                    ID = Val(Me.grdActivity.GetRow().Cells(enmTaskActivity.ID).Value.ToString)
                    Description = Me.grdActivity.GetRow().Cells(enmTaskActivity.Description).Value.ToString
                    TaskDAL = New TaskDAL()
                    TaskDAL.UpdateActivityDescription(ID, Description)
                ElseIf Me.grdActivity.GetRow().Cells(enmTaskActivity.TypeID).DataChanged = True Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.lblProgress1.Text = "Processing please wait ..."
                    Me.lblProgress1.Visible = True
                    Application.DoEvents()
                    Me.grdActivity.UpdateData()
                    ID = Val(Me.grdActivity.GetRow().Cells(enmTaskActivity.ID).Value.ToString)
                    TypeID = Me.grdActivity.GetRow().Cells(enmTaskActivity.TypeID).Value.ToString
                    TaskDAL = New TaskDAL()
                    TaskDAL.UpdateActivityType(TypeID, ID)
                ElseIf Me.grdActivity.GetRow().Cells(enmTaskActivity.Dated).DataChanged = True Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.lblProgress1.Text = "Processing please wait ..."
                    Me.lblProgress1.Visible = True
                    Application.DoEvents()
                    Me.grdActivity.UpdateData()
                    ID = Val(Me.grdActivity.GetRow().Cells(enmTaskActivity.ID).Value.ToString)
                    Dated = Me.grdActivity.GetRow().Cells(enmTaskActivity.Dated).Value
                    TaskDAL = New TaskDAL()
                    TaskDAL.UpdateActivityDated(ID, Dated)
                ElseIf Me.grdActivity.GetRow().Cells(enmTaskActivity.Time).DataChanged = True Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.lblProgress1.Text = "Processing please wait ..."
                    Me.lblProgress1.Visible = True
                    Application.DoEvents()
                    Me.grdActivity.UpdateData()
                    ID = Val(Me.grdActivity.GetRow().Cells(enmTaskActivity.ID).Value.ToString)
                    Time = Me.grdActivity.GetRow().Cells(enmTaskActivity.Time).Value.ToString
                    TaskDAL = New TaskDAL()
                    TaskDAL.UpdateActivityTime(ID, Time)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress1.Visible = False
        End Try
    End Sub
    Private Function IsActivityValidate() As Boolean
        If Me.cmbActivityType.Text = "" Then
            ShowErrorMessage("Type is required")
            Return False
        ElseIf Me.dtpDated.Value = Nothing Then
            ShowErrorMessage("Date is required")
            Return False
        ElseIf Me.txtSpenttime.Text = "" Then
            ShowErrorMessage("Time is required")
            Return False
        ElseIf Me.txtDescription.Text = "" Then
            ShowErrorMessage("Description is required")
            Return False
        End If
        Return True
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If Not IsActivityValidate() Then Exit Sub
            Me.lblProgress1.Text = "Processing please wait ..."
            Me.lblProgress1.Visible = True
            Application.DoEvents()
            Me.grdActivity.DataSource = New TaskDAL().GetTaskActivity(Val(Me.GrdTask.GetRow.Cells("TaskId").Value.ToString))
            Me.grdActivity.RetrieveStructure()
            Dim dtTskActivity As New DataTable
            dtTskActivity = CType(Me.grdActivity.DataSource, DataTable)
            Dim dr As DataRow = dtTskActivity.NewRow
            dr.Item(enmTaskActivity.TypeID) = Me.cmbActivityType.Value
            dr.Item(enmTaskActivity.Dated) = Me.dtpDated.Value
            dr.Item(enmTaskActivity.Time) = Me.txtSpenttime.Text.ToString
            dr.Item(enmTaskActivity.Description) = Me.txtDescription.Text
            dr.Item(enmTaskActivity.TaskId) = TaskId
            dtTskActivity.Rows.InsertAt(dr, 0)
            FillAllTypeCells()
            FillActivityModel()
            SaveActivityLog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress1.Visible = False

        End Try
    End Sub


    Private Sub btnSaveActivity_Click(sender As Object, e As EventArgs)
        Try
            FillActivityModel()
            SaveActivityLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillActivityModel()
        Try
            obj = New TaskActivity
            obj.Type = Val(Me.cmbActivityType.Value.ToString)
            obj.Description = Me.txtDescription.Text
            obj.Timespent = Me.txtSpenttime.Text.ToString
            If TaskId > 0 Then
                obj.TaskID = TaskId
            Else
                obj.TaskID = Me.GrdTask.GetRow.Cells("TaskId").Value
            End If
            obj.Dated = Me.dtpDated.Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SaveActivityLog()
        Try
            Dim dal As TaskDAL = New TaskDAL()
            dal.AddTaskActivity(obj)
            Me.txtDescription.Text = ""
            Me.txtSpenttime.Text = ""
            Me.cmbActivityType.Rows(0).Activate()
            Me.dtpDated.Focus()
            Me.txtSpenttime.Focus()
            Me.GetSingleActivity(Val(Me.grdActivity.GetRow.Cells(enmTaskActivity.TaskId).Value.ToString))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ApplyActivityGridSettings()
        Me.grdActivity.RootTable.Columns(enmTaskActivity.ID).Visible = False
        Me.grdActivity.RootTable.Columns(enmTaskActivity.TaskId).Visible = False
        Me.grdActivity.RootTable.Columns(enmTaskActivity.Description).Width = 300
        Me.grdActivity.RootTable.Columns(enmTaskActivity.TypeID).Width = 150
        Me.grdActivity.RootTable.Columns(enmTaskActivity.TypeID).Caption = "Type"
        Me.grdActivity.RootTable.Columns(enmTaskActivity.Description).Caption = "Detail"
        Me.grdActivity.RootTable.Columns(enmTaskActivity.Time).Caption = "Time Spent"
        Me.grdActivity.RootTable.Columns(enmTaskActivity.Dated).Caption = "Date"
        BtnActivityDelete()
    End Sub
    Private Sub BtnActivityDelete()
        Try
            Dim ButtonDelete As New Janus.Windows.GridEX.GridEXColumn
            ButtonDelete.Key = "btnDelete"
            ButtonDelete.ButtonText = "Delete"
            ButtonDelete.ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            ButtonDelete.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            ButtonDelete.Width = 70
            Me.grdActivity.RootTable.Columns.Add(ButtonDelete)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmbActivityType_EditorButtonClick(sender As Object, e As Win.UltraWinEditors.EditorButtonEventArgs) Handles cmbActivityType.EditorButtonClick
        Try
            ApplyStyleSheet(frmTaskActivityType)
            frmTaskActivityType.ShowDialog()
            FillCombos("ActivityType")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAllActivities()
        Try
            Me.grdActivity.DataSource = New TaskDAL().GetTaskActivities()
            Me.grdActivity.RetrieveStructure()
            Dim status As String = String.Empty
            Me.FillAllTypeCells()
            ApplyActivityGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSingleActivity(ByVal taskID As Integer)
        Try
            'Me.FillAllTypeCells()
            'Me.grdActivity.EmptyRows = True
            'Me.grdActivity.Refresh()
            Me.grdActivity.DataSource = New TaskDAL().GetTaskActivity(taskID)
            Me.grdActivity.RetrieveStructure()

            Me.FillAllTypeCells()
            ApplyActivityGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillAllTypeCells()
        Try
            Dim List As New List(Of ActivityType)
            List = New TaskDAL().AllActivityTypes()
            Me.grdActivity.RootTable.Columns(enmTaskActivity.TypeID).HasValueList = True
            Me.grdActivity.RootTable.Columns(enmTaskActivity.TypeID).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdActivity.RootTable.Columns(enmTaskActivity.TypeID).ValueList.PopulateValueList(List, "ID", "Description")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grdActivity_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdActivity.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "btnDelete" Then
                Dim taskDal As New TaskDAL
                taskDal.DeleteTaskActivity(Me.grdActivity.GetRow.Cells(enmTaskActivity.ID).Value)
                Me.grdActivity.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub DeleteActivity()
    '    Try
    '        Dim taskDal As New TaskDAL
    '        taskDal.DeleteTaskActivity()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub txtSpenttime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSpenttime.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccounts_Leave(sender As Object, e As EventArgs) Handles cmbAccounts.Leave
        Try
            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            FillCombos("ContactPerson")
            Dim C = "Customer End Date:"
            'C = System.Drawing.Color.Navy.ToString()
            If Not IsDBNull(Me.cmbAccounts.SelectedRow.Cells("EndDate").Value) Then
                lblCustomerEndDate.Text = C + ":" + Convert.ToDateTime(Me.cmbAccounts.SelectedRow.Cells("EndDate").Value.ToString).ToString("dd/MMM/yyyy")
                lblCustomerEndDate.Visible = True
                If Me.cmbAccounts.SelectedRow.Cells("EndDate").Value < DateTime.Now Then
                    lblCustomerEndDate.BackColor = Color.Red
                ElseIf Me.cmbAccounts.SelectedRow.Cells("EndDate").Value < DateTime.Now.AddDays(7) Then
                    lblCustomerEndDate.BackColor = Color.Yellow
                Else
                    lblCustomerEndDate.BackColor = Control.DefaultBackColor
                End If
            Else
                lblCustomerEndDate.Text = String.Empty
                lblCustomerEndDate.Visible = False
                lblCustomerEndDate.BackColor = Control.DefaultBackColor
            End If
            'lblCustomerEndDate.ForeColor = Color.Blue
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tddbViews_Click(sender As Object, e As EventArgs) Handles tddbViews.Click

    End Sub
    ''' <summary>
    ''' TASK 1790
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> Below function is added to show data with date wise grouping</remarks>

    Private Sub DateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing please wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim IsAdminGroup As Boolean = False
            If LoginGroup = "Administrator" Then
                IsAdminGroup = True
            Else
                IsAdminGroup = False
            End If

            Me.GrdTask.DataSource = New TaskDAL().GetDateWise(LoginUserId, IsAdminGroup, IIf(Ref_No = String.Empty, Nothing, Ref_No))
            Me.GrdTask.RetrieveStructure()
            FillGridCombos()

            Me.GrdTask.AutomaticSort = False
            Dim gridTaskDate As New Janus.Windows.GridEX.GridEXGroup(Me.GrdTask.RootTable.Columns(EnmTasks.TaskDate))
            gridTaskDate.GroupPrefix = String.Empty
            GrdTask.RootTable.Columns(EnmTasks.Name).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            GrdTask.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GrdTask.RootTable.Groups.Add(gridTaskDate)

            Me.ApplyGridSettings()
            'Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
End Class




