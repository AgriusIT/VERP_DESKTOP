Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.sql
Public Class frmProjectList
    Implements IGeneral
    Dim ProjectList As ProjectDefBE
    Dim objDAL As ProjectDefDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub frmProjectList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar1_Load(Nothing, Nothing)

            'Me.grd.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Me.grd.RootTable.Columns("ProjectId").Visible = False
        Me.grd.RootTable.Columns("RegionId").Visible = False
        Me.grd.RootTable.Columns("ProjectStatusId").Visible = False
        Me.grd.RootTable.Columns("ProjectCategoryId").Visible = False
        Me.grd.RootTable.Columns("LeadProfileId").Visible = False
        Me.grd.RootTable.Columns("ContactPersonId").Visible = False
        Me.grd.RootTable.Columns("EngineeringConsultantId").Visible = False
        Me.grd.RootTable.Columns("ContractAwardedId").Visible = False
        Me.grd.RootTable.Columns("ResponsiblePersonId").Visible = False
        Me.grd.RootTable.Columns("InsideSalesPersonId").Visible = False
        Me.grd.RootTable.Columns("ManagerId").Visible = False

        '// Adding Delete Button in the grid
        If Me.grd.RootTable.Columns.Contains("Delete") = False Then
            Me.grd.RootTable.Columns.Add("Delete")
            Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("Delete").Width = 70
            Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
            Me.grd.RootTable.Columns("Delete").Key = "Delete"
            Me.grd.RootTable.Columns("Delete").Caption = "Action"
        End If
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                btnAddDock.Enabled = True
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
                    Me.Visible = False
                    'Me.btnAddDock.Enabled = False
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                'Me.btnAddDock.Enabled = False
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        btnAddDock.Enabled = True
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
            Dim str As String
            str = "select ProjectId, ProjectTitle, ProjectCode, Plant, Scope, [Address], RegionId, Products, ProjectStatusId, ProjectCategoryId, LeadProfileId, ContactPersonId, EngineeringConsultantId, ContractAwardedId, ResponsiblePersonId, InsideSalesPersonId, ManagerId, Details  from tblDefProject"

            Dim dt As DataTable
            dt = GetDataTable(str)
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

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            ProjectList = New ProjectDefBE
            objDAL = New ProjectDefDal
            ProjectList.ProjectId = Val(Me.grd.CurrentRow.Cells("ProjectId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(ProjectList)
                    Me.grd.GetRow.Delete()
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
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
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project List"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        Try
            frmDefCRMProject.ProjectId = 0
            frmDefCRMProject.DoHaveSaveRights = DoHaveSaveRights
            frmDefCRMProject.ShowDialog()

            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmDefCRMProject.ProjectId = Val(Me.grd.CurrentRow.Cells("ProjectId").Value.ToString)
                frmDefCRMProject.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmDefCRMProject.ProjectId = 0
            End If
            frmDefCRMProject.ShowDialog()
            GetAllRecords()
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class