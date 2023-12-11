''Saba Shabbir : added form for vehicle log 4 april 2018

Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmVehicleLog
    Implements IGeneral

    Dim Vehi As Vehicle_Log
    Public Shared log_id As Integer
    Public Shared blnEditMode As Boolean = False

    Public DoHaveSaveRights As Boolean = True
    Public DoHaveUpdateRights As Boolean = True
    Private Sub frmVehicleLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnCancel.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.BorderSize = 0
            ''condition for check that form is open in edit mode or in a new mode
            If blnEditMode = True Then
                GetById(log_id)
            Else
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Public Sub GetById(LogId As Integer)
        Try

            ''get a record according to logId for edit
            Dim dt As DataTable = New Vehicle_LogDAL().GetById(LogId)

            If dt.Rows.Count > 0 Then

                ''fill data in a fields
                EditRecords(dt)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmVehicleLog_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F4 Then
            ''Saba shabbir: save record and close the form 
            btnSave_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Insert Then
            ''Saba shabbir: save record and remain form opened
            Insert(Nothing, Nothing)
        End If

        If e.KeyCode = Keys.Escape Then

            Me.Close()
        End If
        If e.KeyCode = Keys.F5 Then
            ''Saba shabbir: refresh the form
            ReSetControls()
        End If
    End Sub
    Private Sub Insert(sender As Object, e As EventArgs)
        Try
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If blnEditMode = False Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    ReSetControls()
                Else
                    ShowErrorMessage("Record not saved! due to some technical issues")
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                msg_Information(str_informUpdate)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.btnDelete.Enabled = False
                'Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        ' Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        ' Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(cmbVehicles, New Vehicle_LogDAL().strvehicle(), True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Vehi = New Vehicle_Log
            Vehi.LogDate = Me.dtpDate.Value
            Vehi.Vehicle_Id = Me.cmbVehicles.SelectedValue
            Vehi.Person = Me.txtPerson.Text.ToString.Replace("'", "''")
            Vehi.Purpose = Me.txtDescription.Text.ToString.Replace("'", "''")
            Vehi.Previous = Me.txtPrevious.Text.ToString.Replace("'", "''")
            Vehi.Current = Me.txtCurrent.Text.ToString.Replace("'", "''")
            Vehi.EntryNo = Me.txtEntryNo.Text.ToString.Replace("'", "''")
            Vehi.EntryDateTime = Me.dtpDate.Value
            Vehi.Usage = Me.txtUsage.Text.ToString.Replace("'", "''")
            Vehi.UserName = LoginUserName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    ''Saba Shabbir: validate record befor saving or updation
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVehicles.SelectedIndex = 0 Then
                ShowErrorMessage("Please select The Vehicle")
                Me.cmbVehicles.Focus()
                Return False
            End If

            If Me.txtPerson.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Person Name")
                Me.txtPerson.Focus()
                Return False
            End If

            If Me.txtCurrent.Text = String.Empty Then
                ShowErrorMessage("Enter The Current Meter Reading")
                Me.txtCurrent.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            log_id = 0
            blnEditMode = False
            FillCombos()
            Me.dtpDate.Value = Date.Now
            Me.cmbVehicles.SelectedIndex = 0
            Me.txtPerson.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            Me.txtPrevious.Text = String.Empty
            Me.txtCurrent.Text = String.Empty
            Me.txtUsage.Text = String.Empty
            Me.txtEntryNo.Text = Vehicle_LogDAL.GetDocumentNo
            ApplySecurity(Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                If New Vehicle_LogDAL().Save(Vehi) = True Then
                    Return True
                Else
                    Return False
                End If
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

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then
                If New Vehicle_LogDAL().Update(Vehi) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ''Saba Shabbir: form will close after saving ad updating the record
            If blnEditMode = False Then
                If Save() = True Then
                    ReSetControls()
                    msg_Information(str_informSave)
                    Me.Close()
                Else
                    ShowErrorMessage("Record not saved! due to some technical issues")
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                msg_Information(str_informUpdate)
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            'Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ReSetControls()
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmVehicleLog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ReSetControls()
    End Sub
    ''Saba Shabbir: will display data table in a form fro editing
    Private Sub EditRecords(dt As DataTable)

        Try
            ''got Column Names:LogId, LogDate, Vehicle_Id, Person, Purpose, Previouse, [Current], Fuel, UserName, EntryDateTime, EntryNo
            log_id = dt.Rows(0).Item("LogId").ToString
            Me.txtEntryNo.Text = dt.Rows(0).Item("EntryNo").ToString
            Me.cmbVehicles.SelectedValue = Val(dt.Rows(0).Item("Vehicle_Id").ToString)
            Me.txtPrevious.Text = dt.Rows(0).Item("Previouse").ToString
            Me.txtDescription.Text = dt.Rows(0).Item("Purpose").ToString
            Me.txtCurrent.Text = dt.Rows(0).Item("Current").ToString
            Me.txtPerson.Text = dt.Rows(0).Item("Person").ToString
            Me.dtpDate.Value = dt.Rows(0).Item("LogDate").ToString
            Me.txtUsage.Text = dt.Rows(0).Item("Current").ToString - dt.Rows(0).Item("Previouse").ToString
            LoginUserName = dt.Rows(0).Item("UserName").ToString
            btnSave.Enabled = DoHaveUpdateRights
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPrevious_TextChanged(sender As Object, e As EventArgs) Handles txtPrevious.TextChanged
        ''Saba Shabbir: text of usage will change whenever previous text changed
        Try
            txtUsage.Text = Val(txtCurrent.Text) - Val(txtPrevious.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class