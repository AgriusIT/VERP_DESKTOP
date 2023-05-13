Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmAssetStatus

    Implements IGeneral

    Dim status As AssetStatusBE
    Dim statusid As Integer

    Private Sub frmAssetStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
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

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New AssetStatusDAL().Delete(status) = True Then
                Return True
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
            status = New AssetStatusBE
            status.Asset_Status_Id = statusid
            status.Asset_Status_Name = Me.txtAssetName.Text
            status.Asset_Status_Description = Me.txtDescription.Text
            status.Sort_Order = 1
            status.Active = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdStatus.DataSource = New AssetStatusDAL().GetRecords()
            Me.grdStatus.RetrieveStructure()
            Me.grdStatus.RootTable.Columns(0).Visible = False
            Me.grdStatus.RootTable.Columns(1).Caption = "Asset Name"
            Me.grdStatus.RootTable.Columns(2).Caption = "Description"
            Me.grdStatus.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtAssetName.Text = String.Empty Then
                ShowErrorMessage("Please Enter Asset Name.")
                Me.txtAssetName.Focus()
                Return False
            End If

            If Me.txtDescription.Text = String.Empty Then
                ShowErrorMessage("Please Enter Description.")
                Me.txtDescription.Focus()
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
            statusid = 0
            Me.btnSave.Text = "&Save"
            Me.txtAssetName.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            GetAllRecords()
            UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0)
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New AssetStatusDAL().Save(status) = True Then
                Return True
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

    'Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    'End Sub

    Public Sub EditRecords(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Me.grdStatus.RowCount = 0 Then Exit Sub
            statusid = Me.grdStatus.CurrentRow.Cells("Asset_Status_Id").Value
            Me.txtAssetName.Text = Me.grdStatus.CurrentRow.Cells("Asset_Status_Name").Value
            Me.txtDescription.Text = Me.grdStatus.CurrentRow.Cells("Asset_Status_Description").Value
            Me.btnSave.Text = "&Update"
            UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0)
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New AssetStatusDAL().Update(status) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Not msg_Confirm("Do you want to save this information? ") = True Then
                        Exit Sub
                    End If
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information("Information Saved Successfully ")
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm("Do you want to update this change? ") = True Then
                        Exit Sub
                    End If
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information("Change update successfully")
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm("Do you want to delete this information? ") = True Then
                Exit Sub
            End If
            status = New AssetStatusBE
            status.Asset_Status_Id = Me.grdStatus.CurrentRow.Cells("Asset_Status_Id").Value
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                msg_Information("Information Deleted Successfully")
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStatus_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdStatus.DoubleClick
        Try
            EditRecords(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class