Imports SBDal
Imports SBModel
Public Class frmModelList
    Implements IGeneral
    Dim ObjectModel As ModelList
    Dim ObjectDAL As New ModelListDAL
    Dim ModelId As Integer = 0
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If ObjectDAL.Remove(ModelId) Then
                msg_Information("Record has been deleted.")
                ReSetControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ObjectModel = New ModelList
            ObjectModel.ModelId = ModelId
            ObjectModel.Name = Me.txtModel.Text
            ObjectModel.Description = Me.txtDescription.Text
            ObjectModel.Active = Me.cbActive.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = ObjectDAL.GetAll()
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("ModelId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Not Me.txtModel.Text.Length > 0 Then
                msg_Error("Model is required.")
                Me.txtModel.Focus() : IsValidate = False : Exit Function
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtModel.Text = ""
            Me.txtDescription.Text = ""
            Me.cbActive.Checked = False
            ModelId = 0
            'Me.btnSave.Enabled = True
            'Me.btnUpdate.Enabled = False
            'Me.btnDelete.Enabled = False
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() Then
                If ObjectDAL.Insert(ObjectModel) Then
                    msg_Information("Record has been saved.")
                    ReSetControls()
                End If
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
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Me.btnUpdate.Enabled = True
                Me.btnDelete.Enabled = True
                'Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmModelList)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                                Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else
                    Me.Visible = False
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False

                    For i As Integer = 0 To Rights.Count - 1
                        If Rights.Item(i).FormControlName = "View" Then
                            'Me.Visible = True
                        ElseIf Rights.Item(i).FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Update" Then
                            If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Delete" Then
                            Me.btnDelete.Enabled = True

                        End If
                        
                    Next
                    Me.Visible = False
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() Then
                If ObjectDAL.Edit(ObjectModel) Then
                    msg_Information("Record has been updated.")
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmModelList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        If e.Tab.Index = 1 Then
            Me.btnSave.Visible = False
            Me.btnUpdate.Visible = False
            Me.btnDelete.Visible = False
        ElseIf e.Tab.Index = 0 Then
            Me.btnSave.Visible = True
            Me.btnUpdate.Visible = True
            Me.btnDelete.Visible = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Save()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Update1()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_DoubleClick(sender As Object, e As EventArgs) Handles GridEX1.DoubleClick
        Try
            If Me.GridEX1.RowCount = 0 Then
                msg_Information("Grid is empty.")
                Exit Sub
            End If
            ModelId = Me.GridEX1.GetRow.Cells("ModelId").Value
            Me.txtModel.Text = Me.GridEX1.GetRow.Cells("Name").Value.ToString
            Me.txtDescription.Text = Me.GridEX1.GetRow.Cells("Description").Value.ToString
            Me.cbActive.Checked = Me.GridEX1.GetRow.Cells("Active").Value

            Me.btnSave.Enabled = False
            Me.btnUpdate.Enabled = True
            Me.btnDelete.Enabled = True
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            'Me.UltraTabControl1.ActiveTab.Index = 
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class