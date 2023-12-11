Public Class frmPropertySearch
    Implements IGeneral

    Dim dt As DataTable
    Dim dv As DataView
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPropertySearch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'this function will get all the record 
    Public Sub GetAll()
        Try
            Dim str As String
            str = "select * from PropertyItem"
            dt = GetDataTable(str)
            Me.grdPropertySearch.DataSource = dt
            Me.grdPropertySearch.RetrieveStructure()
            Me.grdPropertySearch.RootTable.Columns("PropertyItemId").Visible = False
            Me.grdPropertySearch.RootTable.Columns("TerritoryId").Visible = False
            Me.grdPropertySearch.RootTable.Columns("PropertyTypeId").Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

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

    Private Sub frmPropertySearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetAll()
        Me.txtSearch.Select()
        'Me.txtSearch.Focus()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If dt IsNot Nothing Then
                If Me.txtSearch.Text <> String.Empty Then
                    Dim str As String
                    str = "select * from PropertyItem where PlotNo LIKE '%" & Me.txtSearch.Text & "%' or  Title LIKE '%" & Me.txtSearch.Text & "%' "
                    dt = GetDataTable(str)
                    Me.grdPropertySearch.DataSource = dt
                    Me.grdPropertySearch.RetrieveStructure()
                    Me.grdPropertySearch.RootTable.Columns("PropertyItemId").Visible = False
                    Me.grdPropertySearch.RootTable.Columns("TerritoryId").Visible = False
                    Me.grdPropertySearch.RootTable.Columns("PropertyTypeId").Visible = False
                Else
                    GetAll()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class