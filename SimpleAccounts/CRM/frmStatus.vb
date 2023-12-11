Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb
Public Class frmStatus
    Implements IGeneral
    Dim Status As Status

    Enum EnmStatus
        StatusId
        Name
        Remarks
        SortOrder
        Active
    End Enum
    Dim StatusID As Integer = 0
    Sub StatusEditRecord()
        If Not GrdStatus.RowCount <> 0 Then Exit Sub
        StatusID = GrdStatus.GetRow.Cells(EnmStatus.StatusId).Value
        Me.TxtName.Text = GrdStatus.GetRow.Cells(EnmStatus.Name).Value.ToString
        Me.TxtRemarks.Text = GrdStatus.GetRow.Cells(EnmStatus.Remarks).Value.ToString
        Me.txtSortOrder.Text = GrdStatus.GetRow.Cells(EnmStatus.SortOrder).Value.ToString
        Me.UichkActive.Checked = GrdStatus.GetRow.Cells(EnmStatus.Active).Value
        Me.BtnSave.Text = "&Update"
        Me.BtnDelete.Enabled = True
        GetAllRecords()
        GetSecurityRights()
        Me.TxtName.Focus()
    End Sub
    Private Sub frmStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ReSetControls()
        Me.GetAllRecords()
    End Sub

    Private Sub UltraTabPageControl1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.GrdStatus.RetrieveStructure()
            GrdStatus.RootTable.Columns(EnmStatus.StatusId).Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New StatusDAL().Delete(Status) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Status = New Status
            With Status
                .StatusId = StatusID
                .StatusName = Me.TxtName.Text.ToString.Replace("'", "''")
                .StatusRemarks = Me.TxtRemarks.Text.ToString.Replace("'", "''")
                .SortOrder = Me.txtSortOrder.Text.ToString.Replace("'", "''")
                .Active = Me.UichkActive.Checked
                .ActivitLog = New ActivityLog
                .ActivitLog.ApplicationName = "CRM"
                .ActivitLog.FormCaption = Me.Text
                .ActivitLog.UserID = LoginUserId
                .ActivitLog.LogDateTime = Date.Now
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GrdStatus.DataSource = New StatusDAL().GetAllRecords
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.TxtName.Text = "" Then
                ShowErrorMessage("Please Enter Status Name")
                Me.TxtName.Focus()
                Return False
            Else
                FillModel()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            TxtId.Text = ""
            TxtName.Text = ""
            TxtRemarks.Text = ""
            txtSortOrder.Text = "1"
            UichkActive.Checked = True
            Me.BtnSave.Text = "&Save"
            Me.BtnSave.Enabled = True
            Me.BtnDelete.Enabled = False
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New StatusDAL().Add(Status) Then Return True
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
            If New StatusDAL().Update(Status) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If Not IsValidate() Then Exit Sub
            If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
            If Me.BtnSave.Text = "&Save" Then
                If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Saved", MsgBoxStyle.Information, str_MessageHeader)
                msg_Information(str_informSave)
            Else
                If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Updated", MsgBoxStyle.Information, str_MessageHeader)
                msg_Information(str_informUpdate)
            End If
            Me.ReSetControls()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Not IsValidate() Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            If Delete() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            'MsgBox("Record Successfully Delete", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informDelete)
            Me.ReSetControls()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            Me.StatusEditRecord()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GrdStatus_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdStatus.RowDoubleClick
        Try
            Me.StatusEditRecord()
            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class