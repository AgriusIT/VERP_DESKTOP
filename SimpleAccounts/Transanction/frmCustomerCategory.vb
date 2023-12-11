Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Public Class frmCustomerCategory
    Implements IGeneral
    Dim Category As CustomerCategoryBE
    Dim CategoryDAL As CustomerCategoryDAL = New CustomerCategoryDAL()
    Dim CurrentID As Integer

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub EditRecord()
        Me.txtCategoryName.Text = DataGridView1.CurrentRow.Cells("CategoryName").Value
        Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
        Me.txtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value
        Me.richTxtRemarks.Text = DataGridView1.CurrentRow.Cells("Remarks").Value.ToString
        Me.CurrentID = Me.DataGridView1.CurrentRow.Cells(0).Value
    End Sub

    Private Sub frmCustomerCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
            GetAllRecords()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Try
            If Not Me.DataGridView1.GetRow Is Nothing Then
                Me.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            DataGridView1.RootTable.Columns("CategoryID").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
         
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

            Category = New CustomerCategoryBE
            Category.CategoryID = CurrentID
            Category.CategoryName = Me.txtCategoryName.Text
            Category.Active = uichkActive.Checked
            Category.SortOrder = txtSortOrder.Text
            Category.Remarks = richTxtRemarks.Text
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable = CategoryDAL.GetAll()
            Me.DataGridView1.DataSource = dt
            Me.DataGridView1.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtCategoryName.Text = String.Empty Then
                ShowErrorMessage("Please enter Category Name")
                Me.txtCategoryName.Focus()
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
            CurrentID = 0
            Me.txtCategoryName.Text = ""
            Me.richTxtRemarks.Text = ""
            Me.txtSortOrder.Text = "1"
            Me.uichkActive.Checked = True
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmCustomerCategory_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
                frmDefCustomerType.CustomerTypeRestControl()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmDefCustomerType.CustomerTypeRestControl()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If CurrentID = 0 Then
                    If CategoryDAL.Add(Category) Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                        Me.Close()
                        frmDefCustomerType.CustomerTypeRestControl()
                    End If
                Else
                    If CategoryDAL.Update(Category) Then
                        msg_Information("Record has been updated successfully.")
                        ReSetControls()
                        Me.Close()
                        frmDefCustomerType.CustomerTypeRestControl()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


End Class