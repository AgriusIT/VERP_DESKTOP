Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb
Public Class frmTypes
    Implements IGeneral
    Enum EnmTypes
        TypeId
        Name
        Remarks
        Sorting
        Active
    End Enum
    Dim Type As Type
    Dim TypeId As Integer = 0
    Sub TypeEditRecord()
        If Not GrdTypes.RowCount <> 0 Then Exit Sub
        TypeId = GrdTypes.GetRow.Cells(EnmTypes.TypeId).Text
        Me.TxtName.Text = GrdTypes.GetRow.Cells(EnmTypes.Name).Text
        Me.TxtRemarks.Text = GrdTypes.GetRow.Cells(EnmTypes.Remarks).Text
        Me.TxtSortOrder.Text = GrdTypes.GetRow.Cells(EnmTypes.Sorting).Text
        Me.UichkActive.Checked = GrdTypes.GetRow.Cells(EnmTypes.Active).Value
        Me.BtnSave.Text = "&Update"
        Me.BtnDelete.Enabled = True
        GetsecurityRights()
        Me.TxtName.Focus()
    End Sub

    Private Sub GrdTypes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        TypeEditRecord()
        Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
        GrdTypes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

    End Sub



    Private Sub frmTypes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.ReSetControls()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.GrdTypes.RetrieveStructure()
            GrdTypes.RootTable.Columns(EnmTypes.TypeId).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
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
            Else

                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            'msg_Error(ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New TypeDAL().Delete(Type) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Type = New Type
            With Type
                .TypeId = TypeID
                .TypeName = Me.TxtName.Text
                .TypeRemarks = Me.TxtRemarks.Text.ToString.Replace("'", "''")
                .SortOrder = Me.TxtSortOrder.Text.ToString.Replace("'", "''")
                .Active = Me.UichkActive.Checked
                .ActivityLog = New ActivityLog
                .ActivityLog.ApplicationName = "CRM"
                .ActivityLog.FormCaption = Me.Text
                .ActivityLog.UserID = LoginUserId
                .ActivityLog.LogDateTime = Date.Now
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GrdTypes.DataSource = New TypeDAL().GetAllRecords
            Me.ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.TxtName.Text = "" Then
                ShowErrorMessage("Please Enter Type Name")
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
            TxtName.Text = ""
            TxtId.Text = ""
            TxtRemarks.Text = ""
            TxtSortOrder.Text = "1"
            UichkActive.Checked = True
            Me.BtnSave.Text = "&Save"
            Me.BtnSave.Enabled = True
            Me.BtnDelete.Enabled = False
            GetAllRecords()
            GetsecurityRights()
            Me.TxtName.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New TypeDAL().Add(Type) Then Return True
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
            If New TypeDAL().Update(Type) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            Me.TypeEditRecord()
            Me.ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
                'MsgBox("Record Successfully Update", MsgBoxStyle.Information, str_MessageHeader)
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
            'MsgBox("Record Successfully Deleted", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informDelete)
            Me.ReSetControls()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GrdTypes_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdTypes.RowDoubleClick
        Try
            Me.TypeEditRecord()
            Me.ApplyGridSettings()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetsecurityRights()
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