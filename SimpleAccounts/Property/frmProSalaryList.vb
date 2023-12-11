Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmProSalaryList
    Implements IGeneral
    Dim objDAL As PropertySalaryDAL
    Public SaveRight As Boolean = False
    Public UpdateRight As Boolean = False
    Public DeleteRight As Boolean = False
    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Property Salary"
        btnAddDock.FlatAppearance.BorderSize = 0
        btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
        GetAllRecords()
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        CtrlGrdBar1_Load(Nothing, Nothing)
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("SalaryId").Visible = False
            Me.grd.RootTable.Columns("SalaryDate").FormatString = "" & str_DisplayDateFormat

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
                Me.grd.RootTable.Columns("Delete").Width = 50
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                SaveRight = True
                UpdateRight = True
                DeleteRight = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Exit Sub
            End If

            SaveRight = False
            UpdateRight = False
            DeleteRight = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    SaveRight = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    UpdateRight = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    DeleteRight = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertySalaryDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
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
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            frmProSalarySheet.ProSalaryId = 0
            frmProSalarySheet.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New PropertySalaryDAL
            If e.Column.Key = "Delete" Then
                If DeleteRight = True Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    objDAL.Delete(Val(Me.grd.CurrentRow.Cells("SalaryId").Value.ToString), GetVoucherId("frmProSalarySheet", Me.grd.CurrentRow.Cells("SalaryNo").Value.ToString))
                    Me.grd.GetRow.Delete()
                Else
                    msg_Error("Sorry! You don't have access rights")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmProSalarySheet.ProSalaryId = Val(Me.grd.CurrentRow.Cells("SalaryId").Value.ToString)
            Else
                frmProSalarySheet.ProSalaryId = 0
            End If
            frmProSalarySheet.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Salary Sheet"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class