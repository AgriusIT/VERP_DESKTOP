Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmRetentionTransferList

    Implements IGeneral
    Dim Retention As RetentionTransferBE
    Dim objDAL As RetentionTransferDAL
        Public DoHaveSaveRights As Boolean = False
        Public DoHaveUpdateRights As Boolean = False
        Public DoHaveDeleteRights As Boolean = False
        Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        frmRetentionTransfer.RetentionMasterId = 0
        frmRetentionTransfer.EditMode = False
        frmRetentionTransfer.DoHaveSaveRights = DoHaveSaveRights
        frmRetentionTransfer.ShowDialog()
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

            Me.grdBranchList.RootTable.Columns("RetentionMasterId").Visible = False
            Me.grdBranchList.RootTable.Columns("VendorId").Visible = False
            Me.grdBranchList.RootTable.Columns("POId").Visible = False
            Me.grdBranchList.RootTable.Columns("VendorConractId").Visible = False
            Me.grdBranchList.RootTable.Columns("CostCenterId").Visible = False
            Me.grdBranchList.RootTable.Columns("ArticleId").Visible = False
            'Me.grdBranchList.RootTable.Columns("Manufactured Item").Width = 300
            Me.grdBranchList.RootTable.Columns("RetentionMasterId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdBranchList.RootTable.Columns("VendorId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdBranchList.RootTable.Columns("POId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdBranchList.RootTable.Columns("VendorConractId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdBranchList.RootTable.Columns("CostCenterId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdBranchList.RootTable.Columns("ArticleId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '// Adding Delete Button in the grid
            If Me.grdBranchList.RootTable.Columns.Contains("Delete") = False Then
                Me.grdBranchList.RootTable.Columns.Add("Delete")
                Me.grdBranchList.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdBranchList.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdBranchList.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdBranchList.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdBranchList.RootTable.Columns("Delete").Width = 50
                Me.grdBranchList.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdBranchList.RootTable.Columns("Delete").Key = "Delete"
                Me.grdBranchList.RootTable.Columns("Delete").Caption = "Action"
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
            objDAL = New RetentionTransferDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grdBranchList.DataSource = dt
            Me.grdBranchList.RetrieveStructure()
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

    Private Sub grdBranchList_DoubleClick(sender As Object, e As EventArgs) Handles grdBranchList.DoubleClick
        Try
            If Me.grdBranchList.RowCount > 0 Then
                frmRetentionTransfer.RetentionMasterId = Val(Me.grdBranchList.CurrentRow.Cells("RetentionMasterId").Value.ToString)
                frmRetentionTransfer.EditMode = True
                frmRetentionTransfer.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmRetentionTransfer.EditMode = False
                frmRetentionTransfer.RetentionMasterId = 0
            End If
            frmRetentionTransfer.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdBranchList_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBranchList.ColumnButtonClick
        Try
            Retention = New RetentionTransferBE
            Retention.RetentionMasterId = Val(Me.grdBranchList.CurrentRow.Cells("RetentionMasterId").Value.ToString)
            Retention.VoucherNo = Me.grdBranchList.CurrentRow.Cells("VoucherNo").Value.ToString
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(Retention)
                    Me.grdBranchList.GetRow.Delete()
                    SaveActivityLog("RetentionTransfer", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
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
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdBranchList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdBranchList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdBranchList.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Warranty Claim"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    End Class
