'06-Feb-2018 TFS2225 : Ayesha Rehman : Add new form to Map Users With the group and Save it
Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Public Class frmCOAGroupsToUserMapping
    Implements IGeneral
    Dim Id As Integer = 0I
    Dim COAGrpUser As COAUserMappingBE

    Dim COAListGrpUser As List(Of COAUserMappingBE)
    Public objDAL As COAUserMappingDAL = New COAUserMappingDAL()
    ''' <summary>
    ''' Ayesha Rehman : set indexes of grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Enum EnumGrd
        COAGrpUserId
        UserId
        GroupId
        GroupName
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grd.RootTable.Columns(1).Visible = False
            Me.grd.ColumnAutoResize = False
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                'Me.grd.RootTable.Columns("Delete").Width = 30
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns(3).Width = 360

            'Me.grd.RootTable.Columns("Delete").Width = 50
            Me.grd.RootTable.Columns(4).Width = 130
            'Me.grd.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman </remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                ' Me.btnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmCOAGroupsToUserMapping)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ayesha Rehman: FillCombos of Users and Groups
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty

            If Condition = "COAGroupsAccount" Then

                str = "Select COAGroupId , Title from COAGroups Where Active = 1 order by SortOrder "
                FillUltraDropDown(cmbCOAGroupsAccount, str)
                Me.cmbCOAGroupsAccount.Rows(0).Activate()
                Me.cmbCOAGroupsAccount.DisplayLayout.Bands(0).Columns("COAGroupId").Hidden = True
                Me.cmbCOAGroupsAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250
            ElseIf Condition = "Users" Then
                str = "Select GroupId , GroupName  from tblUserGroup where Active = 1 order by SortOrder "
                FillUltraDropDown(cmbUsers, str)
                Me.cmbUsers.Rows(0).Activate()
                Me.cmbUsers.DisplayLayout.Bands(0).Columns("GroupId").Hidden = True
                Me.cmbUsers.DisplayLayout.Bands(0).Columns("GroupName").Width = 500
                'str = "Select User_id , FullName  from tblUser "
                'FillUltraDropDown(cmbUsers, str)
                'Me.cmbUsers.Rows(0).Activate()
                'Me.cmbUsers.DisplayLayout.Bands(0).Columns("User_id").Hidden = True
                'Me.cmbUsers.DisplayLayout.Bands(0).Columns("FullName").Width = 250
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Fillmodel to get a list of data 
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            COAListGrpUser = New List(Of COAUserMappingBE)

            Dim COAGrpUser As COAUserMappingBE

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                COAGrpUser = New COAUserMappingBE
                COAGrpUser.COAGroupUserMappingId = Id
                COAGrpUser.User_Id = Me.cmbUsers.ActiveRow.Cells(0).Value
                COAGrpUser.COAGroupId = r.Cells(2).Value

                COAListGrpUser.Add(COAGrpUser)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grid.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grd.DataSource = New COAUserMappingDAL().GetAll
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grid of a selected user.
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Private Sub GetRecords(ByVal UserId As Integer)
        Try
            Dim strsql As String = "Select COAGroupUserMappingId , [User_Id], COAUserMapping.COAGroupId , Title from COAUserMapping Left Outer Join COAGroups on COAUserMapping.COAGroupId = COAGroups.COAGroupId where [User_Id] = " & UserId & " "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(2).Visible = False
            ApplyGridSettings()
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbUsers.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a User")
                Me.cmbUsers.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Ayesha Rehman : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"

            Me.cmbUsers.Rows(0).Activate()
            Me.cmbCOAGroupsAccount.Rows(0).Activate()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GetRecords(-1)
            ApplyGridSettings()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Call the save function from DAL to save the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim COAUserId As Integer = 0
            If Me.grd.RowCount = 0 Then Exit Function
            COAUserId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
            COAGrpUser = New COAUserMappingBE
            COAGrpUser.User_Id = COAUserId
            DeletePrevious(COAGrpUser)
            If New COAUserMappingDAL().Add(COAListGrpUser) Then
                'Insert Activity Log by Ali Faisal 
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, "Accounts Group", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function
    ''' <summary>
    ''' Ayesha Rehman : Delete a single record agianst COAGroupId on btnDeleteClick
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <returns></returns>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Function DeletePrevious(ByVal objModel As COAUserMappingBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            'Delete Master records
            str = "Delete from COAUserMapping  where COAGroupId= " & objModel.COAGroupId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    ''' <summary>
    ''' Ayesha Rehman : fill Combos on the of Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Private Sub frmCOAGroupsToUserMapping_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            ''filling the combos
            Me.FillCombos()
            FillCombos("COAGroupsAccount")
            FillCombos("Users")
            CtrlGrdBar1_Load(Nothing, Nothing)
            ''getting the grid
            Me.GetRecords(-1)

            Me.ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Groups To User Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Add Data in the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2018 TFS2225 : Ayesha Rehman</remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If cmbUsers.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a User")
                Me.cmbUsers.Focus()
                Exit Sub

            End If
            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Exit Sub

            End If
            Dim i As Integer = 0
            For i = 0 To grd.GetRows.Length - 1
                If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = grd.GetRows(i).Cells(2).Value Then
                    ShowErrorMessage("This Group Already Exists")
                    Me.cmbCOAGroupsAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.COAGrpUserId) = Id
            dr(EnumGrd.UserId) = cmbUsers.ActiveRow.Cells(0).Value
            dr(EnumGrd.GroupId) = cmbCOAGroupsAccount.ActiveRow.Cells(0).Value.ToString
            dr(EnumGrd.GroupName) = cmbCOAGroupsAccount.ActiveRow.Cells(1).Value.ToString

            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Saving or updating the record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2017 : TFS2225 : Ayesha Rehman</remarks>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = True Then
                If BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then

                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()

                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Deleting a record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2017 : TFS2225 : Ayesha Rehman</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            COAGrpUser = New COAUserMappingBE
            COAGrpUser.COAGroupUserMappingId = Val(Me.grd.CurrentRow.Cells("COAGroupUserMappingId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGrpUser)
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Reseting All Contols on btn new
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2017 : TFS2225 : Ayesha Rehman</remarks>
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Geting Records of a selected user 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2017 : TFS2225 : Ayesha Rehman</remarks>
    Private Sub cmbUsers_ValueChanged(sender As Object, e As EventArgs) Handles cmbUsers.ValueChanged
        Try
            GetRecords(cmbUsers.ActiveRow.Cells(0).Value)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Reseting All Contols 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>06-Feb-2017 : TFS2225 : Ayesha Rehman</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Try
            ''filling the combos
            Me.FillCombos()
            FillCombos("COAGroupsAccount")
            FillCombos("Users")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCOAGroupsToUserMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click

    End Sub
End Class