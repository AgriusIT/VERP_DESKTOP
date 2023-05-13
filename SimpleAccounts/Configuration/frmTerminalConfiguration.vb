'' 05-12-15 TASK12515 Muahammad Ameen: Terminal Pattern to select desire enviroment for different users.
Imports SBDal
Imports SBModel
Public Class frmTerminalConfiguration

    Dim IsOpenForm As Boolean = False
    Dim Group_Id As Integer = 0
    Dim User_Id As Integer = 0
    Dim Master As TerminalConfigurationMaster
    Dim detailList As List(Of TerminalConfigurationDetail)
    Dim detail As TerminalConfigurationDetail
    Dim TCUsers As List(Of TerminalConfigurationUsers)
    Dim TCUser As TerminalConfigurationUsers
    Dim TCSystems As List(Of TerminalConfigurationSystems)
    Dim TCSystem As TerminalConfigurationSystems
    Dim ds As New DataSet
    Dim dt As New DataTable
    'Dim Users As Users
    Public Function Update(Optional ByVal Condition As String = "") As Boolean
        Return True
    End Function

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            'Me.grdComputers.RootTable.Columns("Column1").Visible = True
            ''Me.grdComputers.RootTable.Columns(1).Visible = False
            'Me.grdComputers.RootTable.Columns(2).Visible = True
            'Me.grdComputers.RootTable.Columns(3).Visible = False
            'Me.grdComputers.RootTable.Columns(4).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "")

    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "RefUser" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select User_ID, User_Name From tblUser"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r("User_Name") = Decrypt(r("User_Name").ToString)
                        r.EndEdit()
                    Next
                End If
                dt.AcceptChanges()
                Me.grdUsers.DataSource = dt
            ElseIf Condition = "Systems" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select Id, SystemCode, SystemName, SystemId  From tblSystemList"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()
                Me.grdComputers.DataSource = dt
            ElseIf Condition = "Applications" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select FormId, FormCaption, FormName, FormModule,AccessKey From tblForms"
                FillDropDown(Me.cmbApplicationsMultiple, strSQL)
            ElseIf Condition = "Application" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select FormId, FormCaption, FormName, FormModule,Accesskey From tblForms"
                FillDropDown(Me.cmbApplicationSingle, strSQL)
            ElseIf Condition = "ListApplications" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select TerminalConfigurationDetail.FormId, ManuTitle, tblForms.FormName, tblForms.FormModule,tblForms.AccessKey From TerminalConfigurationDetail INNER JOIN tblForms on tblForms.FormId = TerminalConfigurationDetail.FormId INNER JOIN TerminalConfigurationMaster on TerminalConfigurationMaster.TCMID = TerminalConfigurationDetail.TCMID WHERE TerminalConfigurationMaster.Title='" & IIf(lstManus.Items.Count > 0, Me.lstManus.Text, "") & "'"
                FillListBox(lstApplications, strSQL)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetFormId(Optional ByVal formCaption As String = "") As Integer
        Dim strSQL As String = String.Empty
        Dim formID As Integer = 0
        Try

            strSQL = "Select FormId, FormCaption, FormName, FormModule From tblForms Where FormCaption ='" & formCaption & "'"

            Dim dt1 As New DataTable
            dt1 = GetDataTable(strSQL)
            If Not dt1 Is Nothing Then
                formID = dt1.Rows(0).Item(0)
            Else
                formID = 0
            End If


        Catch ex As Exception
            Throw ex
        End Try
        Return formID
    End Function


    Public Sub FillModel(Optional ByVal Condition As String = "")
        Try

            If Condition = "SingleApplication" Then
                Master = New TerminalConfigurationMaster
                Master.FormId = IIf(cmbApplicationSingle.SelectedValue = Nothing, 0, cmbApplicationSingle.SelectedValue)
                Master.Layout = cmbLayout.Text.ToString()
                Master.Title = txtTitle.Text.ToString()
                Master.TCMId = 0
                'Master.
            ElseIf Condition = "Users" Then
                'TCUsers = New List(Of TerminalConfigurationUsers)

                If grdUsers.RowCount > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In grdUsers.GetCheckedRows
                        TCUser = New TerminalConfigurationUsers
                        TCUser.UserId = row.Cells("User_ID").Value
                        TCUser.UserName = row.Cells("User_Name").Value
                        TCUser.TCMId = 0
                        TCUser.TCDId = 0
                        Master.Users.Add(TCUser)
                    Next
                End If

            ElseIf Condition = "Systems" Then
                If grdComputers.RowCount > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In grdComputers.GetCheckedRows
                        TCSystem = New TerminalConfigurationSystems
                        TCSystem.Id = 0
                        TCSystem.SystemId = row.Cells("Id").Value
                        TCSystem.TCMId = 0
                        TCSystem.SystemName = row.Cells("SystemName").Value
                        Master.Systems.Add(TCSystem)

                    Next
                End If

            ElseIf Condition = "MultipleApplication" Then
                If lstApplications.Items.Count > 0 Then
                    detailList = New List(Of TerminalConfigurationDetail)
                    For Each obj As Object In lstApplications.Items
                        Dim Row As DataRowView = obj
                        detail = New TerminalConfigurationDetail
                        detail.TCDId = 0
                        detail.TCMId = 0
                        If Not lstManus.SelectedItem Is Nothing Then
                            detail.ManuTitle = lstManus.SelectedItem.ToString()
                        End If
                        detail.FormCaption = Row.Item(1).ToString  '' obj.ToString().Substring(obj.ToString().LastIndexOf("|") + 1)
                        detail.FormId = Val(Row.Item(0).ToString)  ''Num(obj.ToString)
                        Master.Detail.Add(detail)

                    Next
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate() As Boolean

        If Me.cmbLayout.Text = "Single Application" Then
            If cmbApplicationSingle.SelectedValue < 0 Then
                msg_Error("Application selection is required")
                Return False
            ElseIf txtTitle.Text = "" Then
                msg_Error("Title is required")
                Return False
            End If
        Else
            If Me.cmbApplicationsMultiple.SelectedValue < 0 Then
                msg_Error("Application selection is required")
                Return False
            ElseIf txtTitle.Text = "" Then
                msg_Error("Title is required")
                Return False
            ElseIf lstManus.SelectedItem Is Nothing Then
                msg_Error("Main manu is required")
                Return False
            ElseIf lstApplications.Items.Count = 0 Then
                msg_Error("List application is required")
                Return False
            End If
        End If
        If cmbApplicationSingle.SelectedValue > 0 Then
            FillModel("SingleApplication")
            FillModel("Users")
            FillModel("Systems")
        Else
            FillModel("SingleApplication")
            FillModel("MultipleApplication")
            FillModel("Users")
            FillModel("Systems")
        End If

        Return True
    End Function
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try

            'Call ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean
        Try
            Return New TerminalConfigurationDAL().Add(Master)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "")
        Try
            If Condition = "Users" Then
                FillCombos("RefUser")
            ElseIf Condition = "Group" Then
                If Me.lstgroup.Items.Count > 0 Then Me.lstgroup.SelectedIndex = 0
                Me.GetAllRecords()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean
        Return True
    End Function
    'Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
    '    Try
    '        If Condition = "Group" Then
    '            FillModel("Group")

    '            Return True
    '        ElseIf Condition = "Users" Then
    '            FillModel("Users")
    '            'Call New UsersDAL().add(Users)
    '            Return True

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Sub SetButtonImages()

    End Sub

    Public Sub SetConfigurationBaseSetting()

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "")

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean

    End Function

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

                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim id As Integer = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmTerminalPattern_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("RefUser")
            FillCombos("Systems")
            FillCombos("Applications")
            FillCombos("Application")
            FillCombos("ListApplications")
            cmbLayout.Text = "Single Application"
            IsOpenForm = True
            ApplyGridSettings()
            FillTitle()
            'MainManuTitle()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLayout.SelectedIndexChanged
        Dim value As Integer = cmbLayout.SelectedValue
        If cmbLayout.Text = "Single Application" Then
            cmbApplicationSingle.Enabled = True
            'txtTitle.Enabled = True
            cmbApplicationSingle.SelectedValue = 0
            ''   TableLayoutPanel2.Visible = False
            pnlMenu.Visible = False
        Else
            ''  TableLayoutPanel2.Visible = True
            pnlMenu.Visible = True
            cmbApplicationSingle.SelectedValue = -1
            'txtTitle.Text = ""
            cmbApplicationSingle.Enabled = False
            'txtTitle.Enabled = False
        End If
    End Sub

    Private Sub cmbApplicationsMultiple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbApplicationsMultiple.SelectedIndexChanged
        Try
            If cmbApplicationsMultiple.SelectedIndex > 0 Then

                If Me.lstManus.Items.Count = 0 Then
                    msg_Error("Manu selection is required")
                Else
                    'If dt.Rows.Count > 0 Then
                    '    Me.lstApplications.Items.Clear()
                    'End If
                    Dim application As String = cmbApplicationsMultiple.Text.Trim()
                    Me.txtSubTitle.Text = application
                    'Me.lstApplications.Items.Add(application)

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnAddApplication_Click(sender As Object, e As EventArgs) Handles btnAddApplication.Click

        Try
            If txtSubTitle.TextLength > 0 Then
                Dim application As String = txtSubTitle.Text.Trim()
                If lstManus.SelectedItem = Nothing Then
                    msg_Error("Manu selection is required")
                End If
                Dim dt As DataTable = CType(Me.lstApplications.DataSource, DataTable)
                Dim drs() As DataRow = dt.Select("ManuTitle='" & txtSubTitle.Text.Replace("'", "''") & "'")
                If Not drs.Length > 0 Then
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = Me.cmbApplicationsMultiple.SelectedValue
                    dr(1) = Me.txtSubTitle.Text
                    dr(2) = CType(Me.cmbApplicationsMultiple.SelectedItem, DataRowView).Row.Item("FormName").ToString
                    dr(2) = CType(Me.cmbApplicationsMultiple.SelectedItem, DataRowView).Row.Item("FormModule").ToString
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()
                Else
                    msg_Error("This caption is already entered.")
                End If


            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddManu_Click(sender As Object, e As EventArgs) Handles btnAddManu.Click
        Try
            If txtMainManu.TextLength > 0 Then
                Dim manu As String = txtMainManu.Text.Trim()
                If Not Me.lstManus.Items.Contains(manu) Then
                    lstManus.Items.Add(manu)
                Else
                    msg_Error("This manu is already entered.")
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If Save() Then
                    msg_Confirm(str_ConfirmSave)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtMainManu_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMainManu.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtMainManu.TextLength > 0 Then
                    btnAddManu_Click(Me, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub FillTitle()
        Dim sql As String = String.Empty
        Try
            sql = "Select TCMId, Title, Layout, FormId From TerminalConfigurationMaster"
            FillListBox(Me.lstgroup, sql)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub MainManuTitle()
        Dim sql As String = String.Empty
        Try
            sql = "Select Distinct ManuTitle From TerminalConfigurationDetail "
            FillListBox(lstManus, sql)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub lstManus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstManus.SelectedIndexChanged
        Dim sql As String = String.Empty
        Try
            If lstManus.Items.Count > 0 Then
                FillCombos("ListApplications")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSubTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSubTitle.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                If txtSubTitle.TextLength > 0 Then
                    btnAddApplication_Click(Me, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class