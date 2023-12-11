
Public Class frmItemPreviousConsumption
    Implements IGeneral
    Private Sub frmOrderStockItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.cmbPeriod.Text = "Current Month"
        Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
        Me.dtpTo.Value = Date.Today
        FillCombos("Department")
        FillCombos("Type")
        FillCombos("Item")

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos


        Try

            If Condition = "Department" Then
                FillListBox(Me.lstItemDepartment.ListItem, "select ArticleGroupId,ArticleGroupName from ArticleGroupDefTable WHERE Active = 1")
            ElseIf Condition = "Type" Then
                FillListBox(Me.lstItemType.ListItem, "SELECT ArticleTypeId , ArticleTypeName from ArticleTypeDefTable WHERE Active = 1")
            ElseIf Condition = "Item" Then
                FillListBox(Me.lstItem.ListItem, "Select ArticleId, ArticleCode + ' , '+ ArticleDescription AS ItemCode from ArticleDefTableMaster WHERE Active = 1")
            End If
        Catch ex As Exception
            Throw ex
        End Try

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

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged

        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged


        Try
            FillListBox(Me.lstItem.ListItem, "Select ArticleId, ArticleCode + ' , '+ ArticleDescription AS ItemCode from ArticleDefTableMaster WHERE ArticleCode Like '%" & Me.txtSearch.Text & "%' or  ArticleDescription Like '%" & Me.txtSearch.Text & "%' ")

        Catch ex As Exception
            Throw ex
        End Try


    End Sub


End Class