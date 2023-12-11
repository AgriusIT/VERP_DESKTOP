Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmProInvestorList
    Implements IGeneral
    Dim Investor As InvestorBE
    Dim objDAL As InvestorDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
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

            Me.grd.RootTable.Columns("InvestorId").Visible = False
            Me.grd.RootTable.Columns("coa_detail_id").Visible = False
            Me.grd.RootTable.Columns("CityId").Visible = False
            Me.grd.RootTable.Columns("InvestorId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("coa_detail_id").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CityId").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProfitRatio").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProfitRatio").FormatString = "N" & DecimalPointInValue
            '// Adding Delete Button in the grid
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").Width = 50
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns("Active").Width = 50
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
            objDAL = New InvestorDAL
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
            frmProInvestor.InvestorId = 0
            frmProInvestor.DoHaveSaveRights = DoHaveSaveRights
            frmProInvestor.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmProInvestor.InvestorId = Val(Me.grd.CurrentRow.Cells("InvestorId").Value.ToString)
                frmProInvestor.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmProInvestor.InvestorId = 0
            End If
            frmProInvestor.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            Investor = New InvestorBE
            Investor.InvestorId = Val(Me.grd.CurrentRow.Cells("InvestorId").Value.ToString)
            Investor.Name = Me.grd.CurrentRow.Cells("Name").Value.ToString
            Investor.ActivityLog = New ActivityLog
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    If IsValidToDelete("tblVoucherDetail", "coa_detail_id", Val(Me.grd.CurrentRow.Cells("coa_detail_id").Value.ToString)) = True Then
                        objDAL.Delete(Investor)
                        Me.grd.GetRow.Delete()
                        SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                    Else
                        msg_Error(str_ErrorDependentRecordFound)
                    End If
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Investor"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class