Imports SBModel
Imports SBDal

Public Class frmreminder
    Implements IGeneral
    Public later As reminderBE
    Public str As String = String.Empty
    Public int As Single = 0S
    Public reminderId As Integer = 0I
    Public IsOpenForm As Boolean = False
    Public ShowDialForm As Boolean = False
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            later = New reminderBE
            later.reminderId = Me.grdreminder.GetRow.Cells(0).Value
            If New reminderDAL().delete(later) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Only My" Then
                strSQL = "Select User_Id, User_Name From tblUser WHERE User_Id=" & LoginUserId
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r.Item("User_Name") = Decrypt(r.Item("User_Name"))
                        r.EndEdit()
                    Next
                End If
                Me.UiListControl1.ListItem.DataSource = Nothing
                Me.UiListControl1.ListItem.ValueMember = "User_Id"
                Me.UiListControl1.ListItem.DisplayMember = "User_Name"
                Me.UiListControl1.ListItem.DataSource = dt
            ElseIf Condition = "Specified to User Group" Then
                strSQL = "Select GroupId, GroupName From tblUserGroup"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                Me.UiListControl1.ListItem.DataSource = Nothing
                Me.UiListControl1.ListItem.ValueMember = dt.Columns(0).ColumnName
                Me.UiListControl1.ListItem.DisplayMember = dt.Columns(1).ColumnName
                Me.UiListControl1.ListItem.DataSource = dt
            ElseIf Condition = "Specified to User" Then
                strSQL = "Select User_Id, User_Name From tblUser"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r.Item("User_Name") = Decrypt(r.Item("User_Name"))
                        r.EndEdit()
                    Next
                End If
                Me.UiListControl1.ListItem.DataSource = Nothing
                Me.UiListControl1.ListItem.ValueMember = "User_Id"
                Me.UiListControl1.ListItem.DisplayMember = "User_Name"
                Me.UiListControl1.ListItem.DataSource = dt
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub editrecords(Optional ByVal condition As String = "")
        Try
            If Not Me.grdreminder.RecordCount > 0 Then Exit Sub
            'If Me.grdreminder.RecordCount > 0 Then
            '    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            'End If
            Me.Timer1.Enabled = False
            Me.Timer1.Stop()
            reminderId = grdreminder.GetRow.Cells("ReminderId").Value
            dateR.Value = grdreminder.CurrentRow.Cells("Reminder_Date").Value
            If Not grdreminder.CurrentRow.Cells("Reminder_Time").Value.ToString = "" Then
                timeR.Text = grdreminder.CurrentRow.Cells("Reminder_Time").Value
            Else
                timeR.Text = Date.Now
                Me.timeR.Format = DateTimePickerFormat.Time
            End If
            txtmessage.Text = grdreminder.CurrentRow.Cells("Reminder_Description").Value & ""
            Me.txtSubject.Text = Me.grdreminder.GetRow.Cells("Subject").Value.ToString
            'Me.chkOwner.Checked = Me.grdreminder.GetRow.Cells("Owner").Value
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            If Not grdreminder.GetRow.Cells("User").Value.ToString = "" Then
                Me.ComboBox1.Text = grdreminder.GetRow.Cells("User").Value.ToString
            Else
                Me.ComboBox1.SelectedIndex = 0
            End If
            Dim str As String = "Select User_Id From tblReminderDetail WHERE ReminderId=" & reminderId
            Dim dt As New DataTable
            dt = GetDataTable(str)
            Dim strIDs As String = String.Empty
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        If strIDs.Length > 0 Then
                            strIDs = strIDs & "," & r.Item("User_ID").ToString
                        Else
                            strIDs = r.Item("User_ID").ToString
                        End If
                    Next
                End If
            End If

            Me.UiListControl1.DeSelect()
            Me.UiListControl1.SelectItemsByIDs(strIDs)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            later = New reminderBE
            later.reminderId = reminderId
            later.Reminder_Date = Me.dateR.Value.Date
            later.Reminder_Time = Me.timeR.Text.Replace("'", "''")
            later.Reminder_Description = Me.txtmessage.Text.Replace("'", "''")
            later.Subject = Me.txtSubject.Text.Replace("'", "''")
            later.User = Me.ComboBox1.Text
            later.ReminderDetail = New List(Of ReminderDetail)
            If Me.ComboBox1.SelectedIndex <> 1 Then
                Dim strIds() As String = Me.UiListControl1.SelectedIDs.Split(",")
                For Each strID As String In strIds
                    Dim Reminded As New ReminderDetail
                    Reminded.ReminderId = reminderId
                    Reminded.UserID = strID
                    Reminded.User_Reminder_Date = Me.dateR.Value.Date
                    Reminded.User_Reminder_Time = Me.timeR.Text
                    Reminded.Dismiss = False
                    later.ReminderDetail.Add(Reminded)
                Next
            Else
                Dim grpIDs() As String = Me.UiListControl1.SelectedIDs.Split(",")
                Dim str As String = "Select GroupId, User_Id, User_Name From tblUser"
                Dim dtUser As New DataTable
                dtUser = GetDataTable(str)
                Dim dr() As DataRow
                For Each grpID As String In grpIDs
                    dr = dtUser.Select("GroupId=" & grpID & "")
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each row As DataRow In dr
                                Dim Reminded As New ReminderDetail
                                Reminded.ReminderId = reminderId
                                Reminded.UserID = row.Item(1)
                                Reminded.User_Reminder_Date = Me.dateR.Value.Date
                                Reminded.User_Reminder_Time = Me.timeR.Text
                                Reminded.Dismiss = False
                                later.ReminderDetail.Add(Reminded)
                            Next
                        End If
                    End If
                 Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdreminder.DataSource = New reminderDAL().getall()
            grdreminder.RetrieveStructure()
            Me.grdreminder.RootTable.Columns("reminderId").Visible = False
            Me.grdreminder.RootTable.Columns("Reminder_Date").Caption = "Date"
            Me.grdreminder.RootTable.Columns("Reminder_Date").FormatString = "dd/MMM/yyyy"
            Me.grdreminder.RootTable.Columns("Reminder_Time").Caption = "Time"
            Me.grdreminder.RootTable.Columns("Reminder_Description").Caption = "Comments"
            grdreminder.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            reminderId = 0
            Me.dateR.Value = Date.Now
            If ShowDialForm = False Then
                Me.timeR.Text = String.Empty
                Me.txtmessage.Text = String.Empty
                GetAllRecords()
                Me.txtSubject.Focus()
                Timer1.Enabled = True
                Timer1.Start()
                Me.timeR.Value = Date.Now
                Me.timeR.Format = DateTimePickerFormat.Time
                Me.txtSubject.Text = String.Empty
                Me.ComboBox1.SelectedIndex = 0
                Me.timeR.Checked = False
                'Me.chkOwner.Checked = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New reminderDAL().save(later) = True Then
                Return True
            Else : Return False
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
            If New reminderDAL().update(later) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            editrecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
          

            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "save" Then
                'If Not msg_Confirm(str_ConfirmSave) = True Then
                'Exit Sub
                'End If
                FillModel()

                If Save() = True Then
                    'DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls()
                    If ShowDialForm = True Then DialogResult = Windows.Forms.DialogResult.Yes
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then
                    Exit Sub
                End If
                FillModel()
                If Update1() = True Then
                    'DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    ReSetControls()
                    If ShowDialForm = True Then DialogResult = Windows.Forms.DialogResult.Yes
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Delete() = True Then
                'msg_Information(str_informDelete)
                ReSetControls()
            Else
                ShowErrorMessage(str_ErrorDependentRecordFound)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            timeR.Value = Date.Now
            timeR.Format = DateTimePickerFormat.Time
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmreminder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 25 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmreminder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            'Task#127062015 Calling GetAllRecords() Sub
            GetAllRecords()
            'Task#127062015
            ReSetControls()
            IsOpenForm = True
            ComboBox1_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub timeR_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timeR.MouseHover
        Try
            Timer1.Enabled = False
            Timer1.Stop()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdreminder_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdreminder.DoubleClick
        Try
            If Me.grdreminder.Row < 0 Then
                Exit Sub
            Else
                editrecords()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If IsOpenForm = True Then FillCombos(Me.ComboBox1.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdreminder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdreminder.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class