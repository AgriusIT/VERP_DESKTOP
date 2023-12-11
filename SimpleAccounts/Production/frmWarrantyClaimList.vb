Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmWarrantyClaimList
    Implements IGeneral
    Dim Warranty As WarrantyMasterTable
    Dim objDAL As WarrantyClaimDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        frmWarrantyClaim.WarrantyMasterId = 0
        frmWarrantyClaim.DoHaveSaveRights = DoHaveSaveRights
        frmWarrantyClaim.ShowDialog()
        GetAllRecords()
    End Sub

    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grdWarrantyClaim.RootTable.Columns("WarrantyMasterId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("SOId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("FinishGoodStandardId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("PlanId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("TicketId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("Manufactured Item").Width = 300
            Me.grdWarrantyClaim.RootTable.Columns("WarrantyMasterId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdWarrantyClaim.RootTable.Columns("SOId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdWarrantyClaim.RootTable.Columns("FinishGoodStandardId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdWarrantyClaim.RootTable.Columns("PlanId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdWarrantyClaim.RootTable.Columns("TicketId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '// Adding Delete Button in the grid
            If Me.grdWarrantyClaim.RootTable.Columns.Contains("Delete") = False Then
                Me.grdWarrantyClaim.RootTable.Columns.Add("Delete")
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdWarrantyClaim.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdWarrantyClaim.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdWarrantyClaim.RootTable.Columns("Delete").Width = 50
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdWarrantyClaim.RootTable.Columns("Delete").Key = "Delete"
                Me.grdWarrantyClaim.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
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
            objDAL = New WarrantyClaimDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grdWarrantyClaim.DataSource = dt
            Me.grdWarrantyClaim.RetrieveStructure()
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

    Private Sub grdWarrantyClaim_DoubleClick(sender As Object, e As EventArgs) Handles grdWarrantyClaim.DoubleClick
        Try
            If Me.grdWarrantyClaim.RowCount > 0 Then
                frmWarrantyClaim.WarrantyMasterId = Val(Me.grdWarrantyClaim.CurrentRow.Cells("WarrantyMasterId").Value.ToString)
                frmWarrantyClaim.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmWarrantyClaim.WarrantyMasterId = 0
            End If
            frmWarrantyClaim.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdWarrantyClaim_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdWarrantyClaim.ColumnButtonClick
        Try
            Warranty = New WarrantyMasterTable
            Warranty.WarrantyMasterId = Val(Me.grdWarrantyClaim.CurrentRow.Cells("WarrantyMasterId").Value.ToString)
            Warranty.WarrantyNo = Me.grdWarrantyClaim.CurrentRow.Cells("WarrantyNo").Value.ToString
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(Warranty)
                    Me.grdWarrantyClaim.GetRow.Delete()
                    SaveActivityLog("WarrantyClaim", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                Else
                    msg_Information("You do not have delete rights.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdWarrantyClaim.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdWarrantyClaim.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdWarrantyClaim.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Warranty Claim"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
