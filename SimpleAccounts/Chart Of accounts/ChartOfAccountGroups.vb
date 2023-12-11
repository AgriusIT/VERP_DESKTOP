﻿Imports SBModel
Imports SBDal
Public Class ChartOfAccountGroups
    Implements IGeneral

    Dim Id As Integer = 0I
    Dim COAGroups As COAGroupsBE
    Public objDAL As COAGroupsDAL = New COAGroupsDAL()
    Enum enmCOAGroups
        COAGroupId
        Title
        Active
        SortOrder
        Details
    End Enum
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1

                grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit

            Next
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").Width = 50
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True

                '    Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.ChartOfAccountGroups)
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
                    'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
                Else
                    'Me.Visible = False
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False

                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            'Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Update" Then
                            If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Delete" Then
                            Me.BtnDelete.Enabled = True
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
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Chart Of Account Groups"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grd.RootTable Is Nothing Then Return False
            COAGroups = New COAGroupsBE
            COAGroups.COAGroupId = Me.grd.GetRow.Cells(enmCOAGroups.COAGroupId).Value
            If New COAGroupsDAL().Delete(COAGroups) Then
                SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAccountGroupTitle.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            COAGroups = New COAGroupsBE
            COAGroups.COAGroupId = Id
            COAGroups.Title = Me.txtAccountGroupTitle.Text
            COAGroups.Details = Me.txtAccountGroupDetails.Text
            COAGroups.Active = IIf(Me.chkActive.Checked = True, 1, 0)

            COAGroups.SortOrder = Me.txtSortOrder.Text


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grd.DataSource = New COAGroupsDAL().GetAll
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtAccountGroupTitle.Text = String.Empty Then
                ShowErrorMessage("Please enter Group Title")
                Me.txtAccountGroupTitle.Focus()
                Return False
            End If
            ''Ayesha Rehman : Added to check if type is exists already
            'Dim str As String = "SELECT * FROM tblDefAdvancesType WHERE Title = '" & Me.txtAdvanceType.Text & "'"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        ShowErrorMessage("Advance type exists already")
            '        Me.txtAdvanceType.Focus()
            '        Return False
            '    End If
            'End If
     
            FillModel() 'Call Fillmodel 
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"
            txtAccountGroupTitle.Text = ""
            txtAccountGroupDetails.Text = ""
            txtSortOrder.Text = "1"
            CtrlGrdBar1_Load(Nothing, Nothing)
            GetAllRecords()
            ApplyGridSettings()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New COAGroupsDAL().Add(COAGroups) Then

                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAccountGroupTitle.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
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
            If New COAGroupsDAL().Update(COAGroups) Then

                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAccountGroupTitle.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Function
    Public Function ValidateGroupTitle() As Boolean
        Try

            'Ayesha Rehman: Added to check if type is exists already
            Dim str As String = "SELECT * FROM COAGroups WHERE Title = '" & Me.txtAccountGroupTitle.Text & "'"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ShowErrorMessage("Group Title exists already")
                    Me.txtAccountGroupTitle.Focus()
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function
    Public Function ValidateGroupTitleInUpdate() As Boolean
        Try

            'Ayesha Rehman: Added to check if type is exists already
            Dim str As String = "SELECT * FROM COAGroups WHERE Title = '" & Me.txtAccountGroupTitle.Text & "' And COAGroupId != " & Me.grd.CurrentRow.Cells(0).Value & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ShowErrorMessage("Group Title exists already")
                    Me.txtAccountGroupTitle.Focus()
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords()
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            Id = Me.grd.GetRow.Cells(enmCOAGroups.COAGroupId).Value
            Me.txtAccountGroupTitle.Text = Me.grd.GetRow.Cells(enmCOAGroups.Title).Value.ToString
            Me.txtAccountGroupDetails.Text = Me.grd.GetRow.Cells(enmCOAGroups.Details).Value.ToString
            Me.txtSortOrder.Text = Me.grd.GetRow.Cells(enmCOAGroups.SortOrder).Value.ToString

            If IsDBNull(Me.grd.GetRow().Cells(enmCOAGroups.Active).Value) Then
                Me.chkActive.Checked = False
            Else
                Me.chkActive.Checked = Me.grd.GetRow().Cells(enmCOAGroups.Active).Value
            End If
           
            Me.btnSave.Text = "&Update"
            Me.txtAccountGroupTitle.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            COAGroups = New COAGroupsBE
            COAGroups.COAGroupId = Val(Me.grd.CurrentRow.Cells("COAGroupId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGroups)
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try

            If Me.grd.Row < 0 Then
                Exit Sub
            Else
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = True Then
                If BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                    If ValidateGroupTitle() = True Then
                        'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                        If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If ValidateGroupTitleInUpdate() = True Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Exit Sub
            End If

            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            Delete()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ChartOfAccountGroups_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
End Class