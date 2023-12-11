''2-May-2014 Task:2591 Imran Ali New Enhancement In Define Shift.
'Definer Changed.
Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmDefShiftGroup
    Implements IGeneral
    Dim ShiftGroup As ShiftGroupBE
    Dim ShiftGroup_Id As Integer = 0

    Private Sub frmDefShiftGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
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
    Private Sub frmDefShiftGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                If New ShiftGroupDAL().Delete(ShiftGroup) = True Then
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

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ShiftGroup = New ShiftGroupBE
            ShiftGroup.ShiftGroupId = ShiftGroup_Id
            ShiftGroup.ShiftGroupCode = Me.txtShiftGroupCode.Text
            ShiftGroup.ShiftGroupName = Me.txtShiftGroupName.Text
            ShiftGroup.ShiftGroupComments = Me.txtShiftGroupComments.Text
            ShiftGroup.SortOrder = Val(Me.txtSortOrder.Text)
            ShiftGroup.Active = Me.chkActive.Checked

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.Grd.DataSource = New ShiftGroupDAL().GetAll
            Me.Grd.RetrieveStructure()
            Me.Grd.RootTable.Columns(0).Visible = False
            Me.Grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtShiftGroupCode.Text = String.Empty Then
                ShowErrorMessage("Please enter shift group code")
                Me.txtShiftGroupCode.Focus()
                Return False
            End If
            If Me.txtShiftGroupName.Text = String.Empty Then
                ShowErrorMessage("Please enter shift group name")
                Me.txtShiftGroupName.Focus()
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
            ShiftGroup_Id = 0
            Me.btnSave.Text = "&Save"
            Me.txtShiftGroupCode.Text = String.Empty
            Me.txtShiftGroupName.Text = String.Empty
            Me.txtShiftGroupComments.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                If New ShiftGroupDAL().Add(ShiftGroup) = True Then
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

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            If IsValidate() = True Then
                If New ShiftGroupDAL().Update(ShiftGroup) = True Then
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

    Private Sub Grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grd.DoubleClick
        Try
            Me.btnSave.Text = "&Update"
            If Me.Grd.RowCount = 0 Then Exit Sub
            ShiftGroup_Id = Me.Grd.GetRow.Cells("ShiftGroupId").Value
            Me.txtShiftGroupCode.Text = Me.Grd.GetRow.Cells("ShiftGroupCode").Value
            Me.txtShiftGroupName.Text = Me.Grd.GetRow.Cells("ShiftGroupName").Value
            Me.txtShiftGroupComments.Text = Me.Grd.GetRow.Cells("ShiftGroupComments").Value.ToString
            Me.txtSortOrder.Text = Me.Grd.GetRow.Cells("SortOrder").Value
            Me.chkActive.Checked = Me.Grd.GetRow.Cells("Active").Value
            GetSecurityRights()
            Me.txtShiftGroupName.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Grd_DoubleClick(Nothing, Nothing)
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informSave)
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informUpdate)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.ShiftGroup_Id = 0 Then ShowErrorMessage("Please select record") : Exit Sub : Me.Grd.Focus()
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
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
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
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
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
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

    Private Sub Grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
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