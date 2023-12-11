Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class AssetType

    Implements IGeneral
    Dim type As AssetTypeBE
    Dim tid As Integer
    Dim AssetDT As DataTable


    Private Sub AssetType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lblProcess As New Label
        lblProcess.Text = "Loading please wait..."
        lblProcess.Location = New Point(200, 0)
        lblProcess.Visible = True
        Me.UltraTabPageControl1.Controls.Add(lblProcess)
        Threading.Thread.Sleep(500)
        Try
            FillDropDown(Me.cmbCategory, "Select Asset_Category_Id, Asset_Category_Name From tblAssetCategory")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lblProcess.Visible = False
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdType.RootTable.Columns("Asset_Type_Id").Visible = False
            Me.grdType.RootTable.Columns("Asset_Category_Id").Visible = False
            Me.grdType.RootTable.Columns(1).Caption = "Asset Name"
            Me.grdType.RootTable.Columns(2).Caption = "Description"
            Me.grdType.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
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
            If New AssetTypeDAL().Delete(type) = True Then
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
            type = New AssetTypeBE
            type.Asset_Type_Id = tid
            type.Asset_Type_Name = Me.txtAssetName.Text
            type.Asset_Type_Description = Me.txtDescription.Text
            type.Sort_Order = 1
            type.Active = True
            type.Asset_Category_Id = Me.cmbCategory.SelectedValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Me.grdType.DataSource = AssetDT
            'Me.grdType.RetrieveStructure()
            'ApplyGridSettings()
          
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtAssetName.Text = String.Empty Then
                ShowErrorMessage("Please Enter Asset Name")
                Me.txtAssetName.Focus()
                Return False
            End If

            If Me.txtDescription.Text = String.Empty Then
                ShowErrorMessage("Please Enter Description")
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
            tid = 0
            Me.btnSave.Text = "&Save"
            Me.txtAssetName.Text = String.Empty
            Me.txtDescription.Text = String.Empty

            'GetAllRecords()
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()

            UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0)
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New AssetTypeDAL().Save(type) = True Then
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

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New AssetTypeDAL().Update(type) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub EditRecords(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Me.grdType.RowCount = 0 Then Exit Sub
            tid = Me.grdType.CurrentRow.Cells("Asset_Type_Id").Value
            Me.txtAssetName.Text = Me.grdType.CurrentRow.Cells("Asset_Type_Name").Value
            Me.txtDescription.Text = Me.grdType.CurrentRow.Cells("Asset_Type_Description").Value
            Me.cmbCategory.SelectedValue = Me.grdType.CurrentRow.Cells("Asset_Category_Id").Value
            btnSave.Text = "Update"
            UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0)
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
                    If Not msg_Confirm("Do you want to update this Information? ") = True Then
                        Exit Sub
                    End If
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information("Information Update Successfully")
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
            If Not msg_Confirm("Do you want to delete this information?") = True Then
                Exit Sub
            End If
            type = New AssetTypeBE
            type.Asset_Type_Id = Me.grdType.CurrentRow.Cells("Asset_Type_Id").Value
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                msg_Information("Information Deleted Successfully")
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdType_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdType.DoubleClick
        Try
            EditRecords(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbCategory.SelectedValue
            FillDropDown(Me.cmbCategory, "Select Asset_Category_Id, Asset_Category_Name From tblAssetCategory")
            Me.cmbCategory.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            AssetDT = New AssetTypeDAL().GetRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Me.grdType.DataSource = AssetDT
            Me.grdType.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblHeader_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class