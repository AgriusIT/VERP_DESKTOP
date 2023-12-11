Imports SBDal
Imports SBModel
Public Class frmNotificationConfiguration

    Implements IGeneral
    Dim objModel As NotificationConfigurationBE
    Property NotificationId As Integer

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "Groups" Or Condition = "All" Then

                Dim dtUSerGroup As DataTable = New UserGroupDAL().Getallrecord()
                dtUSerGroup.AcceptChanges()
                Me.lstUserGroups.ListItem.ValueMember = dtUSerGroup.Columns(0).ColumnName.ToString
                Me.lstUserGroups.ListItem.DisplayMember = dtUSerGroup.Columns(1).ColumnName.ToString
                Me.lstUserGroups.ListItem.DataSource = dtUSerGroup
                Me.lstUserGroups.DeSelect()

            End If

            If Condition = "Users" Or Condition = "All" Then

                Dim dtUser As New DataTable
                dtUser = New UsersDAL().GetAllRecordGroup
                For Each dr As DataRow In dtUser.Rows
                    dr.BeginEdit()
                    dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                    dr.EndEdit()
                Next
                dtUser.AcceptChanges()
                Me.lstUsers.ListItem.ValueMember = dtUser.Columns(0).ColumnName.ToString
                Me.lstUsers.ListItem.DisplayMember = dtUser.Columns(1).ColumnName.ToString
                Me.lstUsers.ListItem.DataSource = dtUser
                
                Me.lstUsers.DeSelect()

            End If

            If Condition = "Categories" Or Condition = "All" Then

                FillListBox(Me.lstCategories, "SELECT [CategoryId],[CategoryName],[Active],[SortOrder] FROM [tblNotificationCenterCategories] where active = 1 order by SortOrder, CategoryName")
                Me.lstCategories.ClearSelected()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New NotificationConfigurationBE

            objModel.NotificationId = NotificationId
            objModel.EventId = Me.lstEvents.SelectedValue
            objModel.CategoryId = Me.lstCategories.SelectedValue
            objModel.Description = Me.txtDescription.Text

            Dim objRolesList As New List(Of Roles)
            Dim objUserList As New List(Of Users)
            Dim objGroupList As New List(Of UserGroup)

            
            For Each item As String In Me.lstRoles.SelectedIDs.Split(",")

                If item.Trim.Length > 0 Then

                    Dim objRole As New Roles
                    objRole.RoleId = Val(item)
                    objRolesList.Add(objRole)
                End If

            Next

            For Each item As String In Me.lstUsers.SelectedIDs.Split(",")

                If item.Trim.Length > 0 Then

                    Dim objUser As New Users
                    objUser.UserId = Val(item)
                    objUserList.Add(objUser)
                End If

            Next
            For Each item As String In Me.lstUserGroups.SelectedIDs.Split(",")

                If item.Trim.Length > 0 Then

                    Dim objUserGroup As New UserGroup
                    objUserGroup.GroupId = Val(item)
                    objGroupList.Add(objUserGroup)
                End If

            Next

            objModel.RolesList = objRolesList
            objModel.UsersList = objUserList
            objModel.UserGroupList = objGroupList



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            Me.FillModel()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDescription.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If IsValidate() Then

                Dim Dal As New NotificationConfigurationDAL
                
                Return Dal.AddNotification(objModel)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            If IsValidate() Then

                Dim Dal As New NotificationConfigurationDAL

                Return Dal.UpdateNotification(objModel)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lstCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCategories.SelectedIndexChanged
        Try

            If Not Me.lstCategories Is Nothing AndAlso Me.lstCategories.SelectedValue > 0 Then

                FillListBox(Me.lstEvents, "SELECT EventId, EventTitle, CategoryId, SortOrder, Active FROM  tblNotificationCenterEvents where active = 1 and CategoryId=" & Me.lstCategories.SelectedValue & " order by SortOrder, EventTitle")
                ' Me.lstEvents.ClearSelected()
            Else
                Me.lstRoles.ListItem.DataSource = Nothing
                Me.lstEvents.DataSource = Nothing
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmNotificationConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.TabControl1.SelectedIndex = 0
            Me.FillCombos("All")
            ReSetControls()
            '// Opening in edit mode
            If Me.NotificationId > 0 Then

                Dim dt As New DataTable

                dt = GetDataTable("SELECT        tblNotificationCenterNotificationList.EventId, tblNotificationCenterEvents.CategoryId, tblNotificationCenterNotificationList.Description FROM            tblNotificationCenterNotificationList INNER JOIN tblNotificationCenterEvents ON tblNotificationCenterNotificationList.EventId = tblNotificationCenterEvents.EventId where tblNotificationCenterNotificationList.NotificationId=" & Me.NotificationId)

                If dt.Rows.Count > 0 Then

                    Me.lstCategories.SelectedValue = Val(dt.Rows(0).Item("CategoryId").ToString)
                    Me.lstEvents.SelectedValue = Val(dt.Rows(0).Item("EventId").ToString)
                    Me.txtDescription.Text = dt.Rows(0).Item("Description").ToString

                    '// Getting and Filling existing roles
                    Dim dtRoles As DataTable = GetDataTable("select NotificationRoleId from tblNotificationCenterNotificationListRoles where NotificationListId=" & Me.NotificationId)
                    If dtRoles.Rows.Count > 0 Then
                        Dim strIDs As String = String.Empty

                        For Each r As DataRow In dtRoles.Rows
                            If strIDs.Length > 0 Then
                                strIDs = strIDs & "," & Val(r.Item(0).ToString)
                            Else
                                strIDs = Val(r.Item(0).ToString)
                            End If
                        Next

                        Me.lstRoles.SelectItemsByIDs(strIDs)
                    End If


                    '// Getting and Filling existing User Groups
                    Dim dtUserGroups As DataTable = GetDataTable("select UserGroupId from tblNotificationCenterNotificationListUserGroups where NotificationListId=" & Me.NotificationId)
                    If dtUserGroups.Rows.Count > 0 Then
                        Dim strIDs As String = String.Empty

                        For Each r As DataRow In dtUserGroups.Rows
                            If strIDs.Length > 0 Then
                                strIDs = strIDs & "," & Val(r.Item(0).ToString)
                            Else
                                strIDs = Val(r.Item(0).ToString)
                            End If
                        Next

                        Me.lstUserGroups.SelectItemsByIDs(strIDs)
                    End If

                    '// Getting and Filling existing Users
                    Dim dtUsers As DataTable = GetDataTable("select UserId from tblNotificationCenterNotificationListUsers where NotificationListId=" & Me.NotificationId)
                    If dtUsers.Rows.Count > 0 Then
                        Dim strIDs As String = String.Empty

                        For Each r As DataRow In dtUsers.Rows
                            If strIDs.Length > 0 Then
                                strIDs = strIDs & "," & Val(r.Item(0).ToString)
                            Else
                                strIDs = Val(r.Item(0).ToString)
                            End If
                        Next

                        Me.lstUsers.SelectItemsByIDs(strIDs)
                    End If

                End If

                '// Opening users selection tab
                Me.TabControl1.SelectedIndex = 1
            End If
            '// End opening in Edit mode

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub lstEvents_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstEvents.SelectedIndexChanged
        Try

            If Not Me.lstEvents Is Nothing AndAlso Me.lstEvents.SelectedValue > 0 Then

                FillListBox(Me.lstRoles.ListItem, "SELECT        tblNotificationCenterRolesList.RoleId, tblNotificationCenterRolesList.RoleTitle " _
                            & " FROM            tblNotificationCenterRolesList INNER JOIN tblNotificationCenterEventRoles ON tblNotificationCenterRolesList.RoleId = tblNotificationCenterEventRoles.RoleId " _
                            & " Where tblNotificationCenterEventRoles.EventId = " & Me.lstEvents.SelectedValue & " And tblNotificationCenterRolesList.active = 1 And tblNotificationCenterEventRoles.Active = 1 " _
                            & " order by tblNotificationCenterEventRoles.SortOrder, tblNotificationCenterRolesList.SortOrder, tblNotificationCenterRolesList.RoleTitle")

            Else

                'Me.lstRoles.ListItem.Items.Clear()
                Me.lstRoles.ListItem.DataSource = Nothing
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            Me.TabControl1.SelectedIndex = 1
            Me.txtDescription.Text = lstEvents.Text
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

        Try

            If TabControl1.SelectedIndex = 1 Then
                If Not Me.lstEvents.SelectedValue > 0 Then
                    msg_Error("Please select an event")
                    Me.TabControl1.SelectedIndex = 0

                Else

                    If Me.txtDescription.Text.Trim.Length = 0 Then Me.txtDescription.Text = lstEvents.Text

                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        Try
            If Not Me.lstEvents.SelectedValue > 0 Then
                msg_Error("Please select an event")
                Me.TabControl1.SelectedIndex = 0

            End If

            If Me.lstRoles.ListItem.SelectedItems.Count = 0 AndAlso Me.lstUsers.ListItem.SelectedItems.Count = 0 AndAlso Me.lstUserGroups.ListItem.SelectedItems.Count = 0 Then
                msg_Error("No one is selected to deliver notification")
                Me.TabControl1.SelectedIndex = 1
            End If
            If NotificationId > 0 Then
                If Update1() Then
                    'Me.DialogResult = True
                    NotificationId = 0
                    Me.Close()
                End If

            Else
                If Save() Then
                    ' Me.DialogResult = True
                    Me.Close()
                End If

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Me.TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.NotificationId = 0
        Me.DialogResult = False
        Me.Close()
    End Sub
End Class