''01-Jul-2014 TASK:2709 Imran Ali Add 0 Index Reference User In User group Security (Ravi)
''27-Aug-2014 Task:2814 Imran Ali Reference User Selection Problem (ravi)
''Task 02 Muhammad Ameen 19-10-2015: In case Id string is empty then assingn 0 to Location_ID and CompanyId
'' Task 53 Muhammad Ameen 17-03-2016: User based cost centre
''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
Imports SBDal
Imports SBModel
Public Class frmUserGroup
    Implements IGeneral
    Dim IsOpenForm As Boolean = False
    Dim Group_Id As Integer = 0
    Dim User_Id As Integer = 0
    Dim Group As UserGroup
    Dim Users As Users
    Dim FormsControlsList As List(Of FormsControls)
    Dim RightsDetailList As List(Of RightsDetail)
    Dim RightsDetail As RightsDetail
    Dim FormsControls As FormsControls
    Dim RightsList As List(Of Rights)
    Dim Rights_List As Rights
    Dim NotificationList As List(Of SBDal.NotificationActivityConfig)
    Public MainMenuIds As String = String.Empty
    Public _GroupType As String = String.Empty
    Public _GroupId As Integer = 0
    Public UserCompanyRightsList As List(Of UserCompanyRightsBE)
    Public UserLocationRightsList As List(Of UserLocationRightsBE)
    Public UserAccountRightsList As List(Of UserAccountRightsBE)
    Public UserCostCentreRightsList As List(Of UserCostCentreRightsBE)
    Public UserVoucherTypesRightsList As List(Of UserVoucherTypesRightsBE)
    Public UserPOSRightList As List(Of UserPOSRightsBE)
    Public _strImagePath As String = String.Empty

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grduserrights.AutomaticSort = False
            Dim gridgroupmodule As New Janus.Windows.GridEX.GridEXGroup(Me.grduserrights.RootTable.Columns("FormModule"))
            gridgroupmodule.GroupPrefix = String.Empty
            Me.grduserrights.RootTable.Groups.Add(gridgroupmodule)
            Dim gridGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grduserrights.RootTable.Columns("FormCaption"))
            gridGroup.GroupPrefix = String.Empty
            Me.grduserrights.RootTable.Groups.Add(gridGroup)


            Me.grduserrights.RootTable.Columns("GroupId").Visible = False
            Me.grduserrights.RootTable.Columns("FormId").Visible = False
            Me.grduserrights.RootTable.Columns("FormControlId").Visible = False
            Me.grduserrights.RootTable.Columns("FormModule").Caption = "Module"
            Me.grduserrights.RootTable.Columns("FormCaption").Caption = "Form"
            Me.grduserrights.RootTable.Columns("FormControlName").Caption = "Control"
            Me.grduserrights.RootTable.Columns("Rights").EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grduserrights.RootTable.Columns("Rights").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            Me.grduserrights.RootTable.Columns("Rights").Width = 50

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grduserrights.RootTable.Columns
                If col.Index <> Me.grduserrights.RootTable.Columns("Rights").Index Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
            Next
            gridGroup.Collapse()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Group" Then
                Dim dtUSerGroup As DataTable = New UserGroupDAL().Getallrecord()
                dtUSerGroup.AcceptChanges()
                Me.lstgroup.DataSource = dtUSerGroup
                Me.lstgroup.ValueMember = dtUSerGroup.Columns(0).ColumnName.ToString
                Me.lstgroup.DisplayMember = dtUSerGroup.Columns(1).ColumnName.ToString
            ElseIf Condition = Me.cmbGroup.Name Then
                Dim dt As DataTable = New UserGroupDAL().Getallrecord()
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0)
                dr(1) = ".... Select any value ...."
                dt.Rows.InsertAt(dr, 0)
                Me.cmbGroup.DataSource = dt
                Me.cmbGroup.ValueMember = dt.Columns(0).ColumnName
                Me.cmbGroup.DisplayMember = dt.Columns(1).ColumnName
            ElseIf Condition = "Module" Then
                Dim dtModule As DataTable = New UserGroupDAL().DetModuleRecord()
                dtModule.AcceptChanges()
                Me.lstMainMenu.DataSource = dtModule
                'Me.lstMainMenu.ValueMember = dtModule.Columns(0).ColumnName.ToString
                Me.lstMainMenu.DisplayMember = dtModule.Columns(0).ColumnName.ToString
            ElseIf Condition = "Users" Then
                Dim dtUser As New DataTable
                dtUser = New UsersDAL().GetAllRecordGroup
                For Each dr As DataRow In dtUser.Rows
                    dr.BeginEdit()
                    dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                    dr.EndEdit()
                Next
                dtUser.AcceptChanges()
                Me.lstuserlist.DataSource = dtUser
                Me.lstuserlist.ValueMember = dtUser.Columns(0).ColumnName.ToString
                Me.lstuserlist.DisplayMember = dtUser.Columns(1).ColumnName.ToString
            ElseIf Condition = "UserCompanyRights" Then
                FillListBox(Me.lstCompanyList.ListItem, "Select CompanyId, CompanyName From CompanyDefTable")
                FillListBox(Me.ulstLocations.ListItem, "Select Location_Id, Location_Name, Location_Type From tblDefLocation")
            ElseIf Condition = "UserCostCentreRights" Then
                FillListBox(Me.lstCostCentre.ListItem, "Select CostCenterID, Name, Code From tblDefCostCenter Where IsNull(Active, 0) = 1")
            ElseIf Condition = "UserAccountRights" Then
                FillListBox(Me.listAccountList.ListItem, "select coa_detail_id, detail_title from vwCoaDetail where account_type in ('Cash', 'Bank') Order By account_type ")
                'Str = "select coa_detail_id,detail_title from vwCoaDetail where account_type=N'" & Me.cmbMethod.Text & "'"
                ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
                'TFS1751: Waqar Raza
                'Start TFS1751
            ElseIf Condition = "UserPOSRights" Then
                FillListBox(Me.lstPOS.ListItem, "SELECT POSId, POSTitle from tblPOSConfiguration where Active = 1")
                'End TFS1751
            ElseIf Condition = "UserVoucherTypesRights" Then
                FillListBox(Me.lstVoucherTypes.ListItem, "Select Voucher_Type_ID, Voucher_Type from tblDefVoucherType")
                ''END TASK:988
            ElseIf Condition = "RefUser" Then
                Dim strSQL As String = String.Empty
                'Task:2709 Added Union Query for zero index
                strSQL = "Select User_Id, User_Name, User_Code From tblUser"

                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        r("User_Code") = Decrypt(r.Item("User_Code").ToString)
                        r("User_Name") = Decrypt(r.Item("User_Name").ToString)
                    Next
                End If
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0)
                dr(1) = ".... Select Any Value ...."
                dr(2) = ""
                dt.Rows.InsertAt(dr, 0)
                'End Task:2709
                dt.AcceptChanges()
                Me.cmbReferenceUser.DisplayMember = "User_Name"
                Me.cmbReferenceUser.ValueMember = "User_ID"
                Me.cmbReferenceUser.DataSource = dt
                ''TASK TFS4867
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_ID AS EmployeeId, Employee_Name AS EmployeeName FROM tblDefEmployee")
            End If
            ''END TASK TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            If Condition = "Group" Then
                Group = New UserGroup
                Group.GroupId = Group_Id
                Group.GroupName = Me.txtGroupName.Text.ToString.Replace("'", "''")
                Group.SortOrder = 1
                Group.Active = True
            ElseIf Condition = "Users" Then
                Users = New Users
                Users.UserId = User_Id
                Users.UserCode = Encrypt(Me.txtusercode.Text.ToUpper)
                Users.UserName = Encrypt(Me.txtusername.Text)
                Users.Password = Encrypt(Me.txtpassward.Text)
                Users.UserPicture = _strImagePath
                Users.Email = Me.txtemail.Text.ToString.Replace("'", "''")
                Users.GroupId = Me.cmbGroup.SelectedValue
                Users.Block = Me.chkblockuser.Checked
                Users.DashBoardRights = Me.chkDashBoradRights.Checked
                Users.ShowCostPriceRights = Me.chkShowCostPriceRights.Checked
                Users.ShowMainMenuRights = Me.ChkShowMainMenuRights.Checked
                Users.RefUserId = IIf(Me.cmbReferenceUser.SelectedIndex > 0, Me.cmbReferenceUser.SelectedValue, 0)
                Users.FullName = Me.txtusername.Text.ToUpper
                ''TASK TFS4867 done by Amin
                Users.EmployeeId = Me.cmbEmployee.SelectedValue
                ''END TASK TFS4867
                Users.UserCompanyRights = New List(Of UserCompanyRightsBE)
                Users.UserLocationRights = New List(Of UserLocationRightsBE)
                Users.UserAccountRights = New List(Of UserAccountRightsBE)
                Users.UserCostCentreRights = New List(Of UserCostCentreRightsBE)
                Users.UserVoucherTypesRights = New List(Of UserVoucherTypesRightsBE)
                Users.UserPOSRights = New List(Of UserPOSRightsBE)

                Dim UserCompanyRights As UserCompanyRightsBE
                Dim UserLocationRights As UserLocationRightsBE
                Dim UserAccountRights As UserAccountRightsBE
                Dim UserCostCentreRights As UserCostCentreRightsBE
                Dim UserVoucherTypesRights As UserVoucherTypesRightsBE
                Dim UserPOSRights As UserPOSRightsBE

                Dim Ids() As String = Me.lstCompanyList.SelectedIDs.Split(",")
                If Ids.Length > 0 Then
                    For Each Id As String In Ids
                        UserCompanyRights = New UserCompanyRightsBE
                        UserCompanyRights.User_Id = User_Id
                        UserCompanyRights.CompanyId = IIf(Id IsNot String.Empty, Id, 0) ' Muhammad Ameen 19-10-2015: In case Id string is empty then assingn 0 to CompanyId
                        UserCompanyRights.Rights = True
                        Users.UserCompanyRights.Add(UserCompanyRights)
                    Next
                End If


                Dim Ids1() As String = Me.ulstLocations.SelectedIDs.Split(",")
                If Ids1.Length > 0 Then
                    For Each Id As String In Ids1
                        UserLocationRights = New UserLocationRightsBE
                        UserLocationRights.UserID = User_Id
                        UserLocationRights.Location_ID = IIf(Id IsNot String.Empty, Id, 0) ' Muhammad Ameen 19-10-2015: In case Id string is empty then assingn 0 to Location_ID
                        UserLocationRights.Rights = True
                        Users.UserLocationRights.Add(UserLocationRights)
                    Next
                End If
                ' Muhammad Ameen 02-03-2016
                Dim Ids2() As String = Me.listAccountList.SelectedIDs.Split(",")
                If Ids2.Length > 0 Then
                    For Each Id As String In Ids2
                        UserAccountRights = New UserAccountRightsBE
                        UserAccountRights.UserID = User_Id
                        UserAccountRights.AccountID = IIf(Id IsNot String.Empty, Id, 0)
                        UserAccountRights.Rights = True
                        Users.UserAccountRights.Add(UserAccountRights)
                    Next
                End If
                Dim Ids3() As String = Me.lstCostCentre.SelectedIDs.Split(",")
                If Ids3.Length > 0 Then
                    For Each Id As String In Ids3
                        UserCostCentreRights = New UserCostCentreRightsBE
                        UserCostCentreRights.UserID = User_Id
                        UserCostCentreRights.CostCentreId = IIf(Id IsNot String.Empty, Id, 0)
                        UserCostCentreRights.Rights = True
                        Users.UserCostCentreRights.Add(UserCostCentreRights)
                    Next
                End If
                ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
                Dim Ids4() As String = Me.lstVoucherTypes.SelectedIDs.Split(",")
                If Ids4.Length > 0 Then
                    For Each Id As String In Ids4
                        UserVoucherTypesRights = New UserVoucherTypesRightsBE
                        UserVoucherTypesRights.UserId = User_Id
                        UserVoucherTypesRights.VoucherTypeId = IIf(Id IsNot String.Empty, Id, 0)
                        UserVoucherTypesRights.Rights = True
                        Users.UserVoucherTypesRights.Add(UserVoucherTypesRights)
                    Next
                End If
                ''TASK:988
                Dim Ids5() As String = Me.lstPOS.SelectedIDs.Split(",")
                If Ids5.Length > 0 Then
                    For Each Id As String In Ids5
                        UserPOSRights = New UserPOSRightsBE
                        UserPOSRights.UserId = User_Id
                        UserPOSRights.POSId = IIf(Id IsNot String.Empty, Id, 0)
                        UserPOSRights.Rights = True
                        Users.UserPOSRights.Add(UserPOSRights)
                    Next
                End If
            ElseIf Condition = "Rights" Then
                RightsList = New List(Of Rights)
                RightsDetailList = New List(Of RightsDetail)
                Dim dt As DataTable = CType(Me.grduserrights.DataSource, DataTable)
                If dt Is Nothing Then Exit Sub
                If dt.GetChanges IsNot Nothing Then
                    For Each dr As DataRow In dt.GetChanges.Rows
                        Rights_List = New Rights
                        Rights_List.GroupId = Me.lstgroup.SelectedValue
                        Rights_List.FormId = dr.Item("FormId")
                        Rights_List.FormControlId = dr.Item("FormControlId")
                        Rights_List.Rights = dr.Item("Rights")
                        RightsList.Add(Rights_List)
                        'For j As Integer = 0 To Me.lstuserlist.Items.Count - 1
                        '    RightsDetail = New RightsDetail
                        '    RightsDetail.GroupId = Me.lstgroup.SelectedValue
                        '    RightsDetail.UserId = Me.lstuserlist.Items(j)(0)
                        '    RightsDetail.FormId = dr.Item("FormId")
                        '    RightsDetail.FormControlId = dr.Item("FormControlId")
                        '    RightsDetail.Rights = dr.Item("Rights")
                        '    RightsDetailList.Add(RightsDetail)
                        'Next
                    Next
                End If
            ElseIf Condition = "Notifications" Then
                NotificationList = New List(Of SBDal.NotificationActivityConfig)
                Dim dt As DataTable = CType(Me.grdNotification.DataSource, DataTable)
                For Each dr As DataRow In dt.Rows
                    NotificationList.Add(New SBDal.NotificationActivityConfig() With {.NotificationActivityDetailId = Val(dr.Item("NotificationActivityDetailId").ToString), .NotificationActivityId = Val(dr.Item("NotificationActivityId").ToString), .FormId = Val(dr.Item("FormId").ToString), .GroupId = IIf(Me.lstgroup.SelectedItems.Count > 0, Me.lstgroup.SelectedValue, 0), .SMS = dr.Item("SMS").ToString, .Email = dr.Item("Email").ToString, .Notifications = dr.Item("Notifications").ToString})
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim _datatable As DataTable
            _datatable = New UserRightsDAL().GetRights(Me.lstgroup.SelectedValue, Convert.ToString(MainMenuIds))
            _datatable.AcceptChanges()
            grduserrights.DataSource = _datatable
            grduserrights.RetrieveStructure()
            Call ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Users" Then
                Me.cmbGroup.Enabled = True
                lnkChangeGroup.Visible = False
                Me.btnUserSave.Text = "&Save"
                User_Id = 0
                If Not Me.cmbGroup.SelectedIndex - 1 Then Me.cmbGroup.SelectedIndex = 0
                Me.txtusercode.Text = String.Empty
                Me.txtusername.Text = String.Empty
                Me.txtpassward.Text = String.Empty
                Me.txtemail.Text = String.Empty
                Me.chkblockuser.Checked = False
                Me.chkShowCostPriceRights.Checked = True
                Me.ChkShowMainMenuRights.Checked = True
                Me.ulstLocations.DeSelect()
                Me.lstCompanyList.DeSelect()
                Me.lstCostCentre.DeSelect()
                Me.lstVoucherTypes.DeSelect()
                'TFS1751: Waqar Raza
                'Start TFS1751
                Me.lstPOS.DeSelect()
                'End TFS1751
                'If Me.ulstLocations.ListItem.Items.Count > 0 Then Me.ulstLocations.ListItem.SelectedIndex = 0
                'If Me.lstCompanyList.ListItem.Items.Count > 0 Then Me.lstCompanyList.ListItem.SelectedIndex = 0
                FillCombos("RefUser")
                FillCombos(Me.cmbGroup.Name)
                '' TFS4867
                FillCombos("Employee")
                Me.cmbEmployee.SelectedIndex = 0
                '' END TFS4867
            ElseIf Condition = "Group" Then
                If Me.lstgroup.Items.Count > 0 Then Me.lstgroup.SelectedIndex = 0
                Me.lstMainMenu.SelectedItems.Clear()
                Me.chkview.Checked = False
                Me.chkSave.Checked = False
                Me.chkupdate.Checked = False
                Me.chkdelete.Checked = False
                Me.chkprint.Checked = False
                Me.chkExport.Checked = False
                Me.chkprint.Checked = False
                FillCombos("Module")
                Me.GetAllRecords()
            ElseIf Condition = "Notifications" Then
                If Me.lstgroup.Items.Count > 0 Then Me.lstgroup.SelectedIndex = 0
                Me.lstNotificationManu.SelectedItems.Clear()
                Me.cbEmail.Checked = False
                Me.cbSMS.Checked = False
                Me.cbSystemNotification.Checked = False
                Me.GetAllNotificationRecord()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Condition = "Group" Then
                FillModel("Group")
                Call New UserGroupDAL().add(Group)
                Return True
            ElseIf Condition = "Users" Then
                FillModel("Users")
                Call New UsersDAL().add(Users)
                Return True
            ElseIf Condition = "Notifications" Then
                FillModel("Notifications")
                Call New UserRightsDAL().AddNotification(NotificationList)
                Return True
            ElseIf Condition = "Rights" Then
                FillModel("Rights")
                If RightsList IsNot Nothing Then
                    Call New UserRightsDAL().Add(RightsList)
                    Return True
                Else
                    Throw New Exception("Some data is not provided.")
                    Return False
                End If
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

    End Function
    Private Sub frmUserGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            UserCompanyRightsList = UserCompanyRightsDAL.UserCompanyRightsList
            UserLocationRightsList = UserLocationRightsDAL.UserLocationRightsList
            UserAccountRightsList = UserAccountRightsDAL.UserAccountRightsList
            UserCostCentreRightsList = UserCostCentreRightsDAL.UserCostCentreRightsList
            UserVoucherTypesRightsList = UserVoucherTypesRightsDAL.UserVoucherTypesRightsList
            UserPOSRightList = UserPOSRightsDAL.UserPOSRightsList

            FillCombos("Group")
            FillCombos(Me.cmbGroup.Name)
            FillCombos("Module")
            FillCombos("Users")
            FillCombos("UserCompanyRights")
            FillCombos("UserLocationRights")
            FillCombos("UserAccountRights")
            FillCombos("UserCostCentreRights")
            FillCombos("UserVoucherTypesRights")
            FillCombos("UserPOSRights")
            FillCombos("RefUser")
            'ReSetControls("Group")
            ReSetControls("Users")
            ReSetControls("Notifications")
            IsOpenForm = True
            lstgroup_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub chkview_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkview.CheckedChanged, chkSave.CheckedChanged, chkupdate.CheckedChanged, chkdelete.CheckedChanged, chkprint.CheckedChanged, chkExport.CheckedChanged, chkPost.CheckedChanged, chkPriceAllow.CheckedChanged
        Try

            Dim dt As DataTable = CType(Me.grduserrights.DataSource, DataTable)
            Me.grduserrights.RootTable.Groups(0).Expand()
            Me.grduserrights.RootTable.Groups(1).Expand()

            Dim chk As CheckBox = CType(sender, CheckBox)
            Select Case chk.Name
                Case Me.chkview.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "View" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkview.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkSave.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Save" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkSave.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkupdate.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Update" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkupdate.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkdelete.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Delete" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkdelete.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkprint.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Print" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkprint.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkExport.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Export" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkExport.Checked)
                        End If
                        r.EndEdit()
                    Next

                Case Me.chkPost.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Post" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkPost.Checked)
                        End If
                        r.EndEdit()
                    Next
                Case Me.chkPriceAllow.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("FormControlName") = "Price Allow" Then
                            r.Item("Rights") = Convert.ToByte(Me.chkPriceAllow.Checked)
                        End If
                        r.EndEdit()
                    Next
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lstgroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstgroup.SelectedIndexChanged
        Try

            If IsOpenForm = True Then
                FillCombos("Users")
                GetAllNotificationRecord()
                GetAllRecords()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnUsersNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsersNew.Click
        Try
            ReSetControls("Users")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls("Group")
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnUserSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserSave.Click
        Try
            If Me.btnUserSave.Text = "&Save" Then
                ''Commented this condition for internal use
                ''Will apply this condition once we will sell it to any customer
                ''Waqar And Murtaza
                'If lstuserlist.Items.Count < LicenseUsers Then
                If Me.cmbGroup.SelectedIndex = 0 Or Me.cmbGroup.SelectedIndex = -1 Then
                    ShowErrorMessage("Please select group rights")
                    Me.cmbGroup.Focus()
                    Exit Sub
                End If
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save("Users") = True Then DialogResult = Windows.Forms.DialogResult.Yes
                If Not _strImagePath = String.Empty Then
                    If Not pbUser Is Nothing Then
                        Try
                            Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_EmployeePicPath)
                            Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                            FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                            DirInfo.SetAccessControl(FolderSecurity)

                            If IO.File.Exists(_strImagePath) Then
                                IO.File.Delete(_strImagePath)
                                'Dim fs As FileStream = File.OpenRead(_strImagePath)
                                'Dim Buffer1(fs.Length) As Byte
                                'fs.Read(Buffer1, 0, Buffer1.Length)
                                pbUser.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                                'fs.Flush()
                                'fs.Dispose()
                                'fs.Close()
                            Else
                                pbUser.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                            End If

                        Catch ex As Exception
                            ShowErrorMessage(ex.Message)
                            ''Commented this condition for internal use
                            ''Will apply this condition once we will sell it to any customer
                            ''Waqar And Murtaza
                            'Finally
                            '    Me.lblProgress.Visible = False

                        End Try
                    End If
                End If

                UserCompanyRightsList = UserCompanyRightsDAL.UserCompanyRightsList
                msg_Information(str_informSave)
                Me.ReSetControls("Users")
                FillCombos("Users")
                FillCombos("Group")
                GetAdministrator(LoginUserId)
                'Else
                '    msg_Error("You cannot enter more then licensed users")
                '    Exit Sub
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.btnSave.Text = "&Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                Me.grduserrights.UpdateData()
                If Save("Rights") = True Then DialogResult = Windows.Forms.DialogResult.Yes
                msg_Information(str_informSave)
                'ConfigRights(LoginUserId)
                GroupRights = New SBDal.GroupRightsBL().GetRights(LoginUserId)
                Me.ReSetControls("Group")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lbluserrights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub

    'Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.UiCtrlGridBar1.txtGridTitle.Text = "Security Rights"
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnAddNewGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewGroup.Click
        Try
            AddUserGroup.ShowDialog()
            FillCombos("Group")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtGroupName_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGroupName.KeyUp
        Try
            If IsOpenForm = True Then
                Dim dt As DataTable = New UserGroupDAL().Getallrecord(Me.txtGroupName.Text.ToString)
                dt.AcceptChanges()
                Me.lstgroup.DataSource = dt
                Me.lstgroup.ValueMember = dt.Columns(0).ColumnName.ToString
                Me.lstgroup.DisplayMember = dt.Columns(1).ColumnName.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '    Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grduserrights_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grduserrights.FormattingRow

    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            CtrlGrdBar1.txtGridTitle.Text = "Group Rights"
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstMainMenu.MouseClick
        Try
            If IsOpenForm = True Then
                MainMenuIds = String.Empty
                For Each listMenu As Object In Me.lstMainMenu.SelectedItems
                    If TypeOf MainMenuIds Is System.String Then
                        MainMenuIds = MainMenuIds & IIf(MainMenuIds.Length > 0, ",", "") & "'" & listMenu.row.itemarray(0) & "'"
                    End If
                Next
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lstuserlist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstuserlist.SelectedIndexChanged
        Try
            If IsOpenForm = True Then
                If lstuserlist.SelectedIndex = -1 Then Exit Sub
                Dim dt As DataTable = New UsersDAL().DisplayUser(Me.lstuserlist.SelectedValue)
                For Each dr As DataRow In dt.Rows
                    dr.BeginEdit()
                    If Not IsDBNull(dr.Item("User_Picture")) Then
                        If IO.File.Exists(dr.Item("User_Picture")) Then
                            LoadPicture(dr, "UserImage", dr.Item("User_Picture"))
                        End If
                    End If

                    dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                    dr.Item("User_Code") = Decrypt(dr.Item("User_Code"))
                    dr.Item("Password") = Decrypt(dr.Item("Password"))
                    dr.EndEdit()
                Next


                dt.AcceptChanges()
                If dt.Rows.Count > 0 Then
                    User_Id = dt.Rows(0).Item("User_Id")
                    Me.cmbGroup.SelectedValue = dt.Rows(0).Item("GroupId")
                    Me.cmbGroup.Enabled = False
                    Me.lnkChangeGroup.Visible = True
                    Me.txtusername.Text = dt.Rows(0).Item("User_Name").ToString
                    Me.txtusercode.Text = dt.Rows(0).Item("User_Code").ToString
                    Me.txtpassward.Text = dt.Rows(0).Item("Password").ToString
                    Me.txtemail.Text = dt.Rows(0).Item("Email").ToString
                    Me.chkblockuser.Checked = Convert.ToBoolean(dt.Rows(0).Item("Block"))
                    Me.chkDashBoradRights.Checked = Convert.ToBoolean(dt.Rows(0).Item("DashBoardRights"))
                    Me.chkShowCostPriceRights.Checked = Convert.ToBoolean(dt.Rows(0).Item("ShowCostPriceRights"))
                    Me.ChkShowMainMenuRights.Checked = Convert.ToBoolean(dt.Rows(0).Item("ShowMainMenuRights"))
                    ''27-Aug-2014 Task:2814 Imran Ali Reference User Selection Problem (ravi)
                    Me.cmbReferenceUser.SelectedValue = Val(dt.Rows(0).Item("RefUserId").ToString)
                    '' TASK TFS4867
                    Me.cmbEmployee.SelectedValue = Val(dt.Rows(0).Item("EmployeeId").ToString)
                    '' END TASK TFS4867
                    If Not IsDBNull(dt.Rows(0).Item("User_Picture")) Then
                        If IO.File.Exists(dt.Rows(0).Item("User_Picture")) Then

                            Me.pbUser.ImageLocation = dt.Rows(0).Item("User_Picture")
                            Me.pbUser.Update()
                        Else
                            Me.pbUser.Image = Nothing
                        End If
                    Else
                        Me.pbUser.Image = Nothing

                    End If
                    'End Task:2814
                End If



                Dim Rights As List(Of UserCompanyRightsBE) = UserCompanyRightsList.FindAll(AddressOf getUserCompanyRightsList)
                Dim strIDs As String = String.Empty
                Me.lstCompanyList.DeSelect()
                If Rights IsNot Nothing Then
                    If Rights.Count > 0 Then
                        For Each r As UserCompanyRightsBE In Rights
                            If strIDs.Length > 0 Then
                                strIDs = strIDs & "," & r.CompanyId
                            Else
                                strIDs = r.CompanyId
                            End If
                        Next
                        Me.lstCompanyList.SelectItemsByIDs(strIDs)
                    End If
                End If

                ''''''''''''''''''''''''''''''''''''''''''' User Locations '''''''''''''''''''''''''''''''
                Dim lstRights As List(Of UserLocationRightsBE) = UserLocationRightsDAL.UserLocationRightsList.FindAll(AddressOf getUserLocationRightsList)
                Dim strIDs1 As String = String.Empty
                Me.ulstLocations.DeSelect()
                If lstRights IsNot Nothing Then
                    If lstRights.Count > 0 Then
                        For Each r As UserLocationRightsBE In lstRights
                            If strIDs1.Length > 0 Then
                                strIDs1 = strIDs1 & "," & r.Location_ID
                            Else
                                strIDs1 = r.Location_ID
                            End If
                        Next
                        Me.ulstLocations.SelectItemsByIDs(strIDs1)
                    End If
                End If

                ''''''''''''''''''''''''''''''''''''''''''' User Accounts '''''''''''''''''''''''''''''''
                Dim AccountRights As List(Of UserAccountRightsBE) = UserAccountRightsDAL.UserAccountRightsList.FindAll(AddressOf getUserAccountRightsList)
                Dim strIDs2 As String = String.Empty
                Me.listAccountList.DeSelect()
                If AccountRights IsNot Nothing Then
                    If AccountRights.Count > 0 Then
                        For Each r As UserAccountRightsBE In AccountRights
                            If strIDs2.Length > 0 Then
                                strIDs2 = strIDs2 & "," & r.AccountID
                            Else
                                strIDs2 = r.AccountID
                            End If
                        Next
                        Me.listAccountList.SelectItemsByIDs(strIDs2)
                    End If
                End If

                ''''''''''''''''''''''''''''''''''''''''''' User Cost Centre 53'''''''''''''''''''''''''''''''
                Dim CostCentreRights As List(Of UserCostCentreRightsBE) = UserCostCentreRightsDAL.UserCostCentreRightsList.FindAll(AddressOf getUserCostCentreRightsList) '' Task : 53 : Ameen : 17-03-2016
                Dim strIDs3 As String = String.Empty
                Me.lstCostCentre.DeSelect()
                If CostCentreRights IsNot Nothing Then
                    If CostCentreRights.Count > 0 Then
                        For Each r As UserCostCentreRightsBE In CostCentreRights
                            If strIDs3.Length > 0 Then
                                strIDs3 = strIDs3 & "," & r.CostCentreId
                            Else
                                strIDs3 = r.CostCentreId
                            End If
                        Next
                        Me.lstCostCentre.SelectItemsByIDs(strIDs3)
                    End If
                End If

                ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
                Dim VoucherTypesList As List(Of UserVoucherTypesRightsBE) = UserVoucherTypesRightsDAL.UserVoucherTypesRightsList.FindAll(AddressOf getUserVoucherTypesRightsList)
                Dim strIDs4 As String = String.Empty
                Me.lstVoucherTypes.DeSelect()
                If VoucherTypesList IsNot Nothing Then
                    If VoucherTypesList.Count > 0 Then
                        For Each r As UserVoucherTypesRightsBE In VoucherTypesList
                            If strIDs4.Length > 0 Then
                                strIDs4 = strIDs4 & "," & r.VoucherTypeId
                            Else
                                strIDs4 = r.VoucherTypeId
                            End If
                        Next
                        Me.lstVoucherTypes.SelectItemsByIDs(strIDs4)
                    End If
                End If
                ''END TASK:988
                'TFS1751: Waqar Raza
                'Start TFS1751
                Dim POSRights As List(Of UserPOSRightsBE) = UserPOSRightsDAL.UserPOSRightsList.FindAll(AddressOf getUserPOSRightsList)
                Dim strIDs5 As String = String.Empty
                Me.lstPOS.DeSelect()
                If POSRights IsNot Nothing Then
                    If POSRights.Count > 0 Then
                        For Each r As UserPOSRightsBE In POSRights
                            If strIDs5.Length > 0 Then
                                strIDs5 = strIDs5 & "," & r.POSId
                            Else
                                strIDs5 = r.POSId
                            End If
                        Next
                        Me.lstPOS.SelectItemsByIDs(strIDs5)
                    End If
                End If
                'End TFS11751

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            ' Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click, btnUserRefresh.Click
        Try
            FillCombos("Group")
            FillCombos(Me.cmbGroup.Name)
            FillCombos("Users")
            FillCombos("UserCompanyRights")
            FillCombos("UserLocationRights")
            FillCombos("UserAccountRights")
            FillCombos("UserPOSRights")
            FillCombos("RefUser")
            UserCompanyRightsList = UserCompanyRightsDAL.UserCompanyRightsList
            UserLocationRightsList = UserLocationRightsDAL.UserLocationRightsList
            UserAccountRightsList = UserAccountRightsDAL.UserAccountRightsList
            UserCostCentreRightsList = UserCostCentreRightsDAL.UserCostCentreRightsList
            UserPOSRightList = UserPOSRightsDAL.UserPOSRightsList
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lstgroup_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstgroup.DoubleClick
        Try
            _GroupId = Me.lstgroup.SelectedValue
            AddUserGroup.GroupDetail(_GroupId)
            AddUserGroup.ShowDialog()
            FillCombos("Group")
            FillCombos(Me.cmbGroup.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function getUserCompanyRightsList(ByVal Rights As UserCompanyRightsBE) As Boolean
        Try
            If Rights.User_Id = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function getUserLocationRightsList(ByVal Rights As UserLocationRightsBE) As Boolean
        Try
            If Rights.UserID = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getUserAccountRightsList(ByVal Rights As UserAccountRightsBE) As Boolean
        Try
            If Rights.UserID = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'TFS1751: Waqar Raza
    'Start TFS1751
    Public Function getUserPOSRightsList(ByVal Rights As UserPOSRightsBE) As Boolean
        Try
            If Rights.UserId = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End TFS1751
    Public Function getUserCostCentreRightsList(ByVal Rights As UserCostCentreRightsBE) As Boolean
        Try
            If Rights.UserID = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
    ''' <summary>
    ''' Get voucher type right list
    ''' </summary>
    ''' <param name="Rights"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function getUserVoucherTypesRightsList(ByVal Rights As UserVoucherTypesRightsBE) As Boolean
        Try
            If Rights.UserId = User_Id Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''End TASK:988


    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Key = "Accounts" AndAlso Me.cmbAccount.DataSource Is Nothing Then

                Dim flgCompanyRights As Boolean = False
                If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                    flgCompanyRights = GetConfigValue("CompanyRights")
                End If
                Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, TerritoryName as Territory, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " order by detail_title"
                FillUltraDropDown(cmbAccount, Str)
                If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                    Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                End If
                For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                    If row.Index > 0 Then
                        If row.Cells("Active").Value = False Then
                            row.Appearance.BackColor = Color.LightYellow
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub optByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByCode.CheckedChanged
        Try
            Me.cmbAccount.DisplayMember = Me.cmbAccount.Rows(0).Cells(2).Column.Key.ToString
            Me.cmbAccount.Rows(0).Activate()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub optByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByName.CheckedChanged
        Try
            Me.cmbAccount.DisplayMember = Me.cmbAccount.Rows(0).Cells(1).Column.Key.ToString
            Me.cmbAccount.Rows(0).Activate()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try
            Dim id As Integer = 0
            id = Me.cmbAccount.SelectedRow.Cells(0).Value
            Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0) order by detail_title"
            FillUltraDropDown(cmbAccount, Str)
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                If row.Index > 0 Then
                    If row.Cells("Active").Value = False Then
                        row.Appearance.BackColor = Color.LightYellow
                    End If
                End If
            Next
            Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
            Me.cmbAccount.SelectedRow.Cells(0).Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub btnUserImageBrows_Click(sender As Object, e As EventArgs) Handles btnUserImageBrows.Click
        Try
            'Task#1 13-Jun-2015 Check if Path greater then zero then if path doesn't exist then create path first
            If _UserPicPath.Length > 0 Then
                If Not System.IO.Directory.Exists(_UserPicPath) Then
                    System.IO.Directory.CreateDirectory(_UserPicPath)
                End If
            End If
            'End Task#1 13-Jun-2015

            If Not IO.Directory.Exists(_UserPicPath) Then
                ShowErrorMessage("Folder not exist")
                Me.btnUserImageBrows.Focus()
                Exit Sub
            End If

            If Me.txtusername.Text = String.Empty Then Exit Sub
            If Me.txtusercode.Text = String.Empty Then Exit Sub
            'Dim a As New OpenFileDialog
            Me.OpenFileDialog1.Filter = "Image File |*.*jpg"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If System.IO.File.Exists(Me.OpenFileDialog1.FileName) Then
                    _strImagePath = _UserPicPath & "\" + OpenFileDialog1.FileName.Replace(OpenFileDialog1.FileName, Me.txtusername.Text + "-" + Me.txtusercode.Text + ".jpg")
                    Me.pbUser.ImageLocation = OpenFileDialog1.FileName
                    '_strImagePath = str_ApplicationStartUpPath & "\EmployeesPicture\" + a.SafeFileName.Replace(a.SafeFileName, Me.txtName.Text + "-" + Me.txtCode.Text + ".jpg")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Try
                If Me.ToolStripButton7.Text = "&Save" Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    Me.grdNotification.UpdateData()
                    If Save("Notifications") = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    'ConfigRights(LoginUserId)
                    'GroupRights = New SBDal.GroupRightsBL().GetRights(LoginUserId)
                    Me.ReSetControls("Notifications")
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAllNotificationRecord(Optional ByVal Condition As String = "")
        Try
            Dim _datatable As DataTable
            _datatable = New UserRightsDAL().GetNotifications(Me.lstgroup.SelectedValue, Convert.ToString(MainMenuIds))
            _datatable.AcceptChanges()
            Me.grdNotification.DataSource = _datatable
            Me.grdNotification.RetrieveStructure()
            Call ApplyGridNotificationSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridNotificationSettings(Optional ByVal Condition As String = "")
        Try
            Me.grdNotification.AutomaticSort = False
            Dim gridGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdNotification.RootTable.Columns("Module"))
            gridGroup.GroupPrefix = String.Empty
            Me.grdNotification.RootTable.Groups.Add(gridGroup)

            Dim gridForm As New Janus.Windows.GridEX.GridEXGroup(Me.grdNotification.RootTable.Columns("Form"))
            gridGroup.GroupPrefix = String.Empty
            Me.grdNotification.RootTable.Groups.Add(gridForm)
            Me.grdNotification.RootTable.Columns("NotificationActivityDetailId").Visible = False
            Me.grdNotification.RootTable.Columns("NotificationActivityId").Visible = False
            Me.grdNotification.RootTable.Columns("FormId").Visible = False
            'Me.grdNotification.RootTable.Columns("TypeId").Visible = False
            'Me.grdNotification.RootTable.Columns("NotificationId").Visible = False
            Me.grdNotification.RootTable.Columns("Activity").Width = 200
            Me.grdNotification.RootTable.Columns("SMS").EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdNotification.RootTable.Columns("SMS").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            Me.grdNotification.RootTable.Columns("SMS").Width = 50
            Me.grdNotification.RootTable.Columns("Email").EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdNotification.RootTable.Columns("Email").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            Me.grdNotification.RootTable.Columns("Email").Width = 50
            Me.grdNotification.RootTable.Columns("Notifications").EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdNotification.RootTable.Columns("Notifications").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            Me.grdNotification.RootTable.Columns("Notifications").Width = 75

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdNotification.RootTable.Columns
                If col.Index <> Me.grdNotification.RootTable.Columns("SMS").Index AndAlso col.Index <> Me.grdNotification.RootTable.Columns("Email").Index AndAlso col.Index <> Me.grdNotification.RootTable.Columns("Notifications").Index Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub lstNotificationManu_MouseClick(sender As Object, e As MouseEventArgs) Handles lstNotificationManu.MouseClick
        Try
            If IsOpenForm = True Then
                MainMenuIds = String.Empty
                For Each listMenu As Object In Me.lstNotificationManu.SelectedItems
                    If TypeOf MainMenuIds Is System.String Then
                        MainMenuIds = MainMenuIds & IIf(MainMenuIds.Length > 0, ",", "") & "'" & listMenu & "'"
                    End If
                Next
                GetAllNotificationRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbSMS_CheckedChanged(sender As Object, e As EventArgs) Handles cbSMS.CheckedChanged, cbEmail.CheckedChanged, cbSystemNotification.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim chk As CheckBox = CType(sender, CheckBox)
            Me.grdNotification.UpdateData()
            Dim dt As DataTable = CType(Me.grdNotification.DataSource, DataTable)
            Select Case chk.Name
                Case cbSMS.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r("SMS") = Me.cbSMS.Checked
                        r.EndEdit()
                    Next
                Case cbEmail.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r("Email") = Me.cbEmail.Checked
                        r.EndEdit()
                    Next
                Case cbSystemNotification.Name
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r("Notifications") = Me.cbSystemNotification.Checked
                        r.EndEdit()
                    Next
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmUserGroup_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub tsmiNewCashAccount_Click(sender As Object, e As EventArgs) Handles tsmiNewCashAccount.Click
        'If Not LoginGroup.ToString = "Administrator" Then
        '    ControlName.Name = "Cash"
        '    Rights = GroupRights.FindAll(AddressOf ReturnRights)
        '    If Rights.Count = 0 Then Exit Sub
        'End If
        ''End Task:2830
        'Dim CustId As Integer = 0
        FrmAddCustomers.FormType = "Cash"
        FrmAddCustomers.ShowDialog()
        FillCombos("UserAccountRights")
        UserAccountRightsList = UserAccountRightsDAL.UserAccountRightsList


    End Sub

    Private Sub tsmiBankAccount_Click(sender As Object, e As EventArgs) Handles tsmiBankAccount.Click
        'If Not LoginGroup.ToString = "Administrator" Then
        '    ControlName.Name = "Cash"
        '    Rights = GroupRights.FindAll(AddressOf ReturnRights)
        '    If Rights.Count = 0 Then Exit Sub
        'End If
        ''End Task:2830
        'Dim CustId As Integer = 0
        FrmAddCustomers.FormType = "Bank"
        FrmAddCustomers.ShowDialog()
        FillCombos("UserAccountRights")
        UserAccountRightsList = UserAccountRightsDAL.UserAccountRightsList


    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptSecurityRights")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkChangeGroup_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkChangeGroup.LinkClicked
        Try
            Me.cmbGroup.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkNewGroup_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkNewGroup.LinkClicked
        Try
            AddUserGroup.ShowDialog()
            FillCombos("Group")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    'End Sub

    'Private Sub btnViewRightMenu_Click(sender As Object, e As EventArgs)
    '    Try
    '        'AddUserGroup.getUserViewRights(Users)
    '        ''FillCombos("Group")
    '        frmMainLogin.ShowDialog()
    '        'frmModProperty.ShowDialog()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub lstCompanyList_Load(sender As Object, e As EventArgs) Handles lstCompanyList.Load

    End Sub

    Private Sub UltraTabPageControl2_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl2.Paint

    End Sub
    'Changes added by Murtaza for checking user

    'Changes added by murtaza for checking user
End Class