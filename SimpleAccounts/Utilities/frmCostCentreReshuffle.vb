Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class frmCostCentreReshuffle
    Implements IGeneral
    Dim DAL As New CostCentreReshuffleDAL
    Dim CostCentreReshuffleId As Integer = 0
    Dim CostCentreFrom As Integer = 0
    Dim CostCentreTo As Integer = 0
    Dim objCostCentreReshuffle As CostCentreReshuffle
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If DAL.Delete(CostCentreReshuffleId, CostCentreFrom, CostCentreTo) Then
                ReSetControls()
                GetAllRecords()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = ""
        Try
            Str = " Select CostCenterId, Name From tblDefCostCenter "
            FillUltraDropDown(Me.cmbCostCentre1, Str)
            If Me.cmbCostCentre1.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCostCentre1.Rows(0).Activate()
                Me.cmbCostCentre1.DisplayLayout.Bands(0).Columns("CostCenterId").Hidden = True
                Me.cmbCostCentre1.DisplayLayout.Bands(0).Columns("Name").Width = 200
            End If
            FillUltraDropDown(Me.cmbCostCentre, Str)
            If Me.cmbCostCentre.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCostCentre.Rows(0).Activate()
                Me.cmbCostCentre.DisplayLayout.Bands(0).Columns("CostCenterId").Hidden = True
                Me.cmbCostCentre.DisplayLayout.Bands(0).Columns("Name").Width = 200
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objCostCentreReshuffle = New CostCentreReshuffle
            objCostCentreReshuffle.CostCentreReshuffleId = CostCentreReshuffleId
            objCostCentreReshuffle.CostCentreFrom = Me.cmbCostCentre.Value
            objCostCentreReshuffle.CostCentreTo = Me.cmbCostCentre1.Value
            objCostCentreReshuffle.Dated = Now
            objCostCentreReshuffle.UserName = LoginUserName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = Nothing
            Me.grdSaved.DataSource = DAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("CostCentreReshuffleId").Visible = False
            Me.grdSaved.RootTable.Columns("CostCentreFrom").Visible = False
            Me.grdSaved.RootTable.Columns("CostCentreTo").Visible = False
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("CCFrom").Caption = " Old Cost Centre"
            Me.grdSaved.RootTable.Columns("CCTo").Caption = " New Cost Centre"
            Me.grdSaved.RootTable.Columns("Dated").Caption = " Date"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCostCentre.Value <= 0 Then
                msg_Error("From Cost Centre value is required.")
                Me.cmbCostCentre.Focus() : IsValidate = False : Exit Function
            End If
            If Me.cmbCostCentre1.Value <= 0 Then
                msg_Error("To Cost Centre value is required.")
                Me.cmbCostCentre1.Focus() : IsValidate = False : Exit Function
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbCostCentre1.Rows(0).Activate()
            Me.cmbCostCentre.Rows(0).Activate()
            Me.btnSave.Text = "&Save"
            CostCentreReshuffleId = 0
            CostCentreFrom = 0
            CostCentreTo = 0
            'Me.cmbCostCentre.Enabled = True
            'Me.cmbCostCentre1.Enabled = True
            'GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() Then
                If DAL.Add(objCostCentreReshuffle) Then
                    ReSetControls()
                    GetAllRecords()
                    Return True
                Else
                    Return False
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

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim ID As Integer = 0
            Dim ID1 As Integer = 0
            FillCombos()
            Me.cmbCostCentre.Value = ID
            Me.cmbCostCentre1.Value = ID1
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            CostCentreReshuffleId = Me.grdSaved.GetRow.Cells("CostCentreReshuffleId").Value
            CostCentreFrom = Me.grdSaved.GetRow.Cells("CostCentreFrom").Value
            CostCentreTo = Me.grdSaved.GetRow.Cells("CostCentreTo").Value
            Me.cmbCostCentre.Value = CostCentreFrom
            Me.cmbCostCentre1.Value = CostCentreTo
            'Me.cmbCostCentre.Enabled = False
            'Me.cmbCostCentre1.Enabled = False
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCostCentreReshuffle_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            FillCombos()
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    'Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmCostCentreReshuffle)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then

                        'End Task:2395
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Update" Then
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            End If
            If Save() Then
                msg_Information("Saved successfully.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class