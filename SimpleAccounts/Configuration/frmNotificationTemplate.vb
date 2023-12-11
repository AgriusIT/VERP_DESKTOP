Imports SBModel
Imports SBDal

Public Class frmNotificationTemplate
    Implements IGeneral
    Public IsOpenForm As Boolean = False
    Public ShowDialForm As Boolean = False
    Dim objNTDAL As New NotificationTemplatesDAL
    Dim objNT As NotificationTemplates
    Dim NotificationTemplatesId As Integer = 0I
    Private Const EM_CHARFROMPOS As Int32 = &HD7
    Private Declare Function SendMessageLong Lib "user32" Alias "SendNotificationA" (ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Long


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If objNTDAL.Delete(NotificationTemplatesId) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Groups" Then
                Dim dtUSerGroup As DataTable = New UserGroupDAL().Getallrecord()
                dtUSerGroup.AcceptChanges()
                Me.lbGroups.DataSource = dtUSerGroup
                Me.lbGroups.ValueMember = dtUSerGroup.Columns(0).ColumnName.ToString
                Me.lbGroups.DisplayMember = dtUSerGroup.Columns(1).ColumnName.ToString
            ElseIf Condition = "Users" Then
                Dim dtUser As New DataTable
                dtUser = New UsersDAL().GetAllRecordGroup
                For Each dr As DataRow In dtUser.Rows
                    dr.BeginEdit()
                    dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                    dr.EndEdit()
                Next
                dtUser.AcceptChanges()
                Me.lbUsers.DataSource = dtUser
                Me.lbUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
                Me.lbUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
            ElseIf Condition = "Tables" Then
                Dim dtTables As DataTable = objNTDAL.GetTables()
                Dim dr As DataRow
                dtTables.AcceptChanges()
                dr = dtTables.NewRow
                dr(0) = "" '(objDataSet.Tables(0).Columns(0).ColumnName)
                dr(1) = strZeroIndexItem 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dtTables.Rows.InsertAt(dr, 0)
                Me.cmbTables.DataSource = dtTables
                Me.cmbTables.ValueMember = dtTables.Columns(0).ColumnName.ToString
                Me.cmbTables.DisplayMember = dtTables.Columns(1).ColumnName.ToString
                Me.cmbTables.DisplayLayout.Bands(0).Columns("TABLE_NAME").Hidden = True
                Me.cmbTables.DisplayLayout.Bands(0).Columns("Table Name").Width = 200
                Me.cmbTables.Rows(0).Activate()
                'FillDropDown()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Public Sub editrecords(Optional ByVal condition As String = "")
    '    Try
    '        If Not Me.grdreminder.RecordCount > 0 Then Exit Sub
    '        'If Me.grdreminder.RecordCount > 0 Then
    '        '    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
    '        'End If
    '        Me.Timer1.Enabled = False
    '        Me.Timer1.Stop()
    '        reminderId = grdreminder.GetRow.Cells("ReminderId").Value
    '        dateR.Value = grdreminder.CurrentRow.Cells("Reminder_Date").Value
    '        If Not grdreminder.CurrentRow.Cells("Reminder_Time").Value.ToString = "" Then
    '            timeR.Text = grdreminder.CurrentRow.Cells("Reminder_Time").Value
    '        Else
    '            timeR.Text = Date.Now
    '            Me.timeR.Format = DateTimePickerFormat.Time
    '        End If
    '        txtmessage.Text = grdreminder.CurrentRow.Cells("Reminder_Description").Value & ""
    '        Me.txtSubject.Text = Me.grdreminder.GetRow.Cells("Subject").Value.ToString
    '        'Me.chkOwner.Checked = Me.grdreminder.GetRow.Cells("Owner").Value
    '        Me.btnSave.Text = "&Update"
    '        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    '        If Not grdreminder.GetRow.Cells("User").Value.ToString = "" Then
    '            Me.ComboBox1.Text = grdreminder.GetRow.Cells("User").Value.ToString
    '        Else
    '            Me.ComboBox1.SelectedIndex = 0
    '        End If
    '        Dim str As String = "Select User_Id From tblReminderDetail WHERE ReminderId=" & reminderId
    '        Dim dt As New DataTable
    '        dt = GetDataTable(str)
    '        Dim strIDs As String = String.Empty
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                For Each r As DataRow In dt.Rows
    '                    If strIDs.Length > 0 Then
    '                        strIDs = strIDs & "," & r.Item("User_ID").ToString
    '                    Else
    '                        strIDs = r.Item("User_ID").ToString
    '                    End If
    '                Next
    '            End If
    '        End If

    '        Me.UiListControl1.DeSelect()
    '        Me.UiListControl1.SelectItemsByIDs(strIDs)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            objNT = New NotificationTemplates
            objNT.NotificationTemplatesId = NotificationTemplatesId
            objNT.Subject = Me.txtSubject.Text
            objNT.Template = Me.txtTemplate.Text
            If Me.cmbTables.Text = ".... Select any Value ...." Then
                objNT.TableName = ""
            Else
                objNT.TableName = Me.cmbTables.Value
            End If
            objNT.TemplateDate = Now
            For Each group As DataRowView In lbGroups.SelectedItems
                Dim NGroup As New NotificationGroups
                NGroup.GroupId = Val(group.Item(0).ToString)
                objNT.NGList.Add(NGroup)
                'objNT.
            Next
            For Each user As DataRowView In lbUsers.SelectedItems
                Dim NUser As New NotificationUsers
                NUser.UserId = Val(user.Item(0).ToString)
                objNT.NUList.Add(NUser)
                'objNT.
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = objNTDAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("NotificationTemplatesId").Visible = False
            Me.grdSaved.RootTable.Columns("TemplateDate").Caption = "Template Date"
            Me.grdSaved.RootTable.Columns("TableName").Caption = "Table Name"
            Me.grdSaved.RootTable.Columns("TemplateDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        If Me.txtSubject.Text = "" Then
            ShowErrorMessage("Subject is required.")
            Me.txtSubject.Focus() : IsValidate = False : Exit Function
        End If
        If Me.txtTemplate.Text = "" Then
            ShowErrorMessage("Template is required.")
            Me.txtTemplate.Focus() : IsValidate = False : Exit Function
        End If
        If Me.lbGroups.SelectedItems.Count = 0 AndAlso Me.lbUsers.SelectedItems.Count = 0 Then
            ShowErrorMessage("Either group or user is required.")
            Me.lbGroups.Focus() : IsValidate = False : Exit Function
        End If

        If objNTDAL.SubjectExists(Me.txtSubject.Text) AndAlso Me.btnSave.Text = "&Save" Then
            ShowErrorMessage("Subject exists duplicate is not allowed. ")
            Me.txtSubject.Focus() : IsValidate = False : Exit Function
        End If
        IsValidate = True
        FillModel()
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtSubject.Text = ""
            Me.txtTemplate.Text = ""
            Me.btnSave.Text = "&Save"
            NotificationTemplatesId = 0
            FillCombos("Tables")
            'Me.cmbTables.Rows(0).Activate()
            Me.lbUsers.SelectedItems.Clear()
            Me.lbGroups.SelectedItems.Clear()
            'Me.lstParameters.Items.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If objNTDAL.Add(objNT) Then
                Return True
            Else
                Return False
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
          
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                'If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "save" Then
                If Save() = True Then
                    msg_Information("Template gets saved successfully.")
                    ReSetControls()
                    GetAllRecords()
                    If ShowDialForm = True Then DialogResult = Windows.Forms.DialogResult.Yes
                End If
                'Else
                '    If Not msg_Confirm(str_ConfirmUpdate) = True Then
                '        Exit Sub
                '    End If
                '    If Update1() = True Then
                '        ReSetControls()
                '        If ShowDialForm = True Then DialogResult = Windows.Forms.DialogResult.Yes
                '    End If
                'End If
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
                GetAllRecords()
            Else
                ShowErrorMessage(str_ErrorDependentRecordFound)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub EditRecord()
        Try
            If Me.grdSaved.RowCount = 0 Then
                Exit Sub
            End If
            NotificationTemplatesId = Val(Me.grdSaved.GetRow.Cells("NotificationTemplatesId").Value.ToString)
            Me.txtSubject.Text = Me.grdSaved.GetRow.Cells("Subject").Value.ToString
            Me.txtTemplate.Text = Me.grdSaved.GetRow.Cells("Template").Value.ToString
            If IsDBNull(Me.grdSaved.GetRow.Cells("TableName").Value) Or Me.grdSaved.GetRow.Cells("TableName").Value.ToString = "" Then
            Else
                'FillCombos("Tables")
                Me.cmbTables.Value = Me.grdSaved.GetRow.Cells("TableName").Value.ToString
                Me.cmbTables.Text = Me.grdSaved.GetRow.Cells("Subject").Value.ToString
                'Me.cmbTables.ActiveRow.Cells("TABLE_NAME").Value = Me.grdSaved.GetRow.Cells("TableName").Value.ToString
                Dim dtColumns As DataTable = objNTDAL.GetColumns(Me.grdSaved.GetRow.Cells("TableName").Value.ToString)
                Me.lstParameters.DataSource = dtColumns
                Me.lstParameters.ValueMember = dtColumns.Columns(0).ColumnName.ToString
                Me.lstParameters.DisplayMember = dtColumns.Columns(0).ColumnName.ToString
            End If
            Dim dtGroups As DataTable = objNTDAL.GetGroups(NotificationTemplatesId)
            If dtGroups.Rows.Count > 0 Then
                For Each Row As DataRow In dtGroups.Rows
                    Me.lbGroups.SelectedValue = Row.Item("GroupId")
                Next
            End If
            Dim dtUsers As DataTable = objNTDAL.GetUsers(NotificationTemplatesId)
            If dtUsers.Rows.Count > 0 Then
                For Each Row As DataRow In dtUsers.Rows
                    Me.lbUsers.SelectedValue = Row.Item("UserId")
                Next
            End If
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmNotificationTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos("Groups")
            FillCombos("Users")
            FillCombos("Tables")
            GetAllRecords()
            ReSetControls()
            IsOpenForm = True
            Me.txtTemplate.AllowDrop = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub cmbTables_ValueChanged(sender As Object, e As EventArgs) Handles cmbTables.ValueChanged
    '    Try
    '        If Me.cmbTables.SelectedRow.Cells("Table Name").Value.ToString.Length > 0 Then
    '            Dim dtColumns As DataTable = objNTDAL.GetColumns(Me.cmbTables.SelectedRow.Cells("Table Name").Value.ToString)
    '            Me.lstParameters.DataSource = dtColumns
    '            Me.lstParameters.ValueMember = dtColumns.Columns(0).ColumnName.ToString
    '            Me.lstParameters.DisplayMember = dtColumns.Columns(0).ColumnName.ToString
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbTables_TextChanged(sender As Object, e As EventArgs) Handles cmbTables.TextChanged
        'Try
        '    If Me.cmbTables.SelectedRow.Cells("Table Name").Value.ToString.Length > 0 Then
        '        Dim dtColumns As DataTable = objNTDAL.GetColumns(Me.cmbTables.SelectedRow.Cells("Table Name").Value.ToString)
        '        Me.lstParameters.DataSource = dtColumns
        '        Me.lstParameters.ValueMember = dtColumns.Columns(0).ColumnName.ToString
        '        Me.lstParameters.DisplayMember = dtColumns.Columns(0).ColumnName.ToString
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub lstParameters_MouseDown(sender As Object, e As MouseEventArgs) Handles lstParameters.MouseDown
        Try
            'Me.lstParameters.DoDragDrop(Me.lstParameters.SelectedItem, DragDropEffects.Copy)
            ''Dim Selecteditems As String = ""
            ''For i As Integer = 0 To lstParameters.SelectedItems.Count - 1
            ''    Selecteditems = Selecteditems & "," & lstParameters.SelectedItems.Item(i)
            ''Next
            'lstParameters.DoDragDrop(Selectedit, DragDropEffects.Copy Or DragDropEffects.Move)
            lstParameters.DoDragDrop(lstParameters.SelectedItem, DragDropEffects.Copy Or DragDropEffects.Move)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTemplate_DragDrop(sender As Object, e As DragEventArgs) Handles txtTemplate.DragDrop
        Try
            'Me.txtTemplate.SelectedText = e.Data.GetData(DataFormats.Text).ToString & " "
            Dim obj As Object = Me.lstParameters.SelectedItem
            txtTemplate.SelectedText += obj(0).ToString & " " ''e.Data.GetData(DataFormats.Text).ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function TextBoxCursorPos(ByVal txt As TextBox, _
 ByVal X As Single, ByVal Y As Single) As Long
        ' Convert screen coordinates into control coordinates.
        Dim pt As Point = txtTemplate.PointToClient(New Point(X, _
            Y))

        ' Get the character number
        TextBoxCursorPos = SendMessageLong(txt.Handle, _
            EM_CHARFROMPOS, 0&, CLng(pt.X + pt.Y * &H10000)) _
            And &HFFFF&
    End Function

    Private Sub txtTemplate_DragEnter(sender As Object, e As DragEventArgs) Handles txtTemplate.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub MakeColumns(ByVal Subject As String)
        Try
            Dim ColumnList As New List(Of String)
            Select Case Subject
                Case "Sales Inquiry"
                    Dim SalesInquiry As New frmSalesInquiry
                    For Each Column As Janus.Windows.GridEX.GridEXColumn In SalesInquiry.grdSaved.RootTable.Columns
                        ColumnList.Add("@" & Column.Key.ToString)
                    Next
                    Me.lstParameters.DataSource = ColumnList
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSubject_Leave(sender As Object, e As EventArgs) Handles txtSubject.Leave
        Try
            If cmbTables.Text = ".... Select any Value ...." Then
                MakeColumns(Me.txtSubject.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTables_Leave(sender As Object, e As EventArgs) Handles cmbTables.Leave
        Try
            If Me.cmbTables.SelectedRow.Cells("TABLE_NAME").Value.ToString.Length > 0 Then
                Dim dtColumns As DataTable = objNTDAL.GetColumns(Me.cmbTables.SelectedRow.Cells("TABLE_NAME").Value.ToString)
                Me.lstParameters.DataSource = dtColumns
                Me.lstParameters.ValueMember = dtColumns.Columns(0).ColumnName.ToString
                Me.lstParameters.DisplayMember = dtColumns.Columns(0).ColumnName.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lbGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbGroups.SelectedIndexChanged
        Try
            If Me.lbGroups.SelectedItems.Count > 0 Then
                Me.lbUsers.SelectedItems.Clear()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lbUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbUsers.SelectedIndexChanged
        If Me.lbUsers.SelectedItems.Count > 0 Then
            Me.lbGroups.SelectedItems.Clear()
        End If
    End Sub

    Private Sub frmNotificationTemplate_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If lstParameters.Items.Count > 0 Then
                Me.lstParameters.DataSource = Nothing
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstParameters_Click(sender As Object, e As EventArgs) Handles lstParameters.Click
        Try


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class