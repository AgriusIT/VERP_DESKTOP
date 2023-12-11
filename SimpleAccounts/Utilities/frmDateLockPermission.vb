Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Data.SqlClient


Public Class frmDateLockPermission

    Implements IGeneral
    Enum enmFields
        ID
        DateFromLock
        DateToLock
        Lock
        UserID
        PermissionUserName
    End Enum
    Dim objMod As DateLockPermissionBE
    Dim intID As Integer = 0I




    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else

                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grdSaved.RowCount = 0 Then Return False
            objMod = New DateLockPermissionBE
            objMod.ID = Val(Me.grdSaved.GetRow.Cells(enmFields.ID).Value.ToString)
            Return New DateLockPermissionDAL().Delete(objMod)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try


            FillListBox(Me.UiListControl1.ListItem, "Select User_Id, FullName From tblUser")



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            objMod = New DateLockPermissionBE
            objMod.ID = intID ''TFS4675 : Bind Id with the model : Record was not updating
            objMod.DateFromLock = Me.dtpDateFromLock.Value
            objMod.DateToLock = Me.dtpDateToLock.Value
            objMod.Lock = Me.chkLock.Checked
            If Me.UiListControl1.SelectedIDs.Length > 0 Then
                objMod.UserIDs = Me.UiListControl1.SelectedIDs
            Else
                objMod.UserIDs = 0
            End If
            objMod.PermissionUserName = LoginUserName

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = New DateLockPermissionDAL().GetAll
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(enmFields.ID).Visible = False
            Me.grdSaved.RootTable.Columns(enmFields.UserID).Visible = False
            Me.grdSaved.AutoSizeColumns()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.UiListControl1.SelectedIDs.Length <= 0 Then
                ShowErrorMessage("Please select user who need to athorized.")
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
            intID = 0I
            Me.btnSave.Text = "&Save"
            Me.dtpDateFromLock.Value = Date.Now
            Me.dtpDateToLock.Value = Date.Now
            Me.chkLock.Checked = False
            FillCombos()
            Me.dtpDateFromLock.Focus()
            GetAllRecords()
            ApplySecurity(Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            Return New DateLockPermissionDAL().Add(objMod)


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

            Return New DateLockPermissionDAL().Update(objMod)


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmDateLockPermission_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then

                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            intID = Val(Me.grdSaved.GetRow.Cells(enmFields.ID).Value.ToString)
            If IsDBNull(Me.grdSaved.GetRow.Cells(enmFields.DateFromLock).Value) Then
                Me.dtpDateFromLock.Value = Date.Now
            Else
                Me.dtpDateFromLock.Value = Me.grdSaved.GetRow.Cells(enmFields.DateFromLock).Value
            End If

            If IsDBNull(Me.grdSaved.GetRow.Cells(enmFields.DateToLock).Value) Then
                Me.dtpDateToLock.Value = Date.Now ''Change From Date to To Date : TFS4675 : Ayesha Rehman  : 04-10-2018
            Else
                Me.dtpDateToLock.Value = Me.grdSaved.GetRow.Cells(enmFields.DateToLock).Value
            End If


            If IsDBNull(Me.grdSaved.GetRow.Cells(enmFields.Lock).Value) Then
                Me.chkLock.Checked = False
            Else
                Me.chkLock.Checked = Me.grdSaved.GetRow.Cells(enmFields.Lock).Value
            End If


            If Me.grdSaved.GetRow.Cells(enmFields.UserID).Value.ToString.Length > 0 Then
                Me.UiListControl1.SelectItemsByIDs(Me.grdSaved.GetRow.Cells(enmFields.UserID).Value.ToString)
            End If

            ApplySecurity(Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
