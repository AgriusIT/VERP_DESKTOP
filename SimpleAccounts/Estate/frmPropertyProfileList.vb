'10-Feb-2018 TFS2335 : Ayesha Rehman: Added a new form Property Profile List To Get All Profiles
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmPropertyProfileList
    Implements IGeneral
    Dim PropertyProfile As PropertyProfileBE
    Dim objDAL As PropertyProfileDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public DoHaveDealRights As Boolean = False

    Dim isloaded As Boolean = False
    ''' <summary>
    ''' Ayesha Rehman : Load Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnAddDock.FlatAppearance.BorderSize = 0
        btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
        isloaded = False
        GetAllRecords()
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        CtrlGrdBar1_Load(Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Apply Grid Seeting To Hide Some Columns and Other Work
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("PropertyProfileId").Visible = False



            '// Hiding property id coloumn
            Me.grd.RootTable.Columns("InvId").Visible = False
            Me.grd.RootTable.Columns("BranchId").Visible = False
            Me.grd.RootTable.Columns("CostCenterId").Visible = False
            Me.grd.RootTable.Columns("PropertyItemId").Visible = False
            Me.grd.RootTable.Columns("ProfitShare").Visible = False
            'Me.grd.RootTable.Columns("IsDealCompleted").Visible = False
            Dim formatstring As New Janus.Windows.GridEX.GridEXFormatCondition(grd.RootTable.Columns("ProfitShare"), Janus.Windows.GridEX.ConditionOperator.Equal, 1)
            Dim style As New Janus.Windows.GridEX.GridEXFormatStyle
            style.BackColor = Color.LightGray
            formatstring.FormatStyle = style
            Me.grd.RootTable.FormatConditions.Add(formatstring)

            ''
            Dim _formatDealCompletion As New Janus.Windows.GridEX.GridEXFormatCondition(grd.RootTable.Columns("IsDealCompleted"), Janus.Windows.GridEX.ConditionOperator.Equal, 1)
            Dim Style1 As New Janus.Windows.GridEX.GridEXFormatStyle
            Style1.BackColor = Color.LightGreen
            _formatDealCompletion.FormatStyle = Style1
            Me.grd.RootTable.FormatConditions.Add(_formatDealCompletion)
            ''
            '// Setting columns Width
            Me.grd.RootTable.Columns("DocNo").Width = 80
            Me.grd.RootTable.Columns("DocDate").Width = 70
            Me.grd.RootTable.Columns("Status").Width = 60
            Me.grd.RootTable.Columns("Title").Width = 200
            Me.grd.RootTable.Columns("PropertyType").Width = 80
            Me.grd.RootTable.Columns("PlotNo").Width = 50
            Me.grd.RootTable.Columns("Sector").Width = 60
            Me.grd.RootTable.Columns("Block").Width = 60
            Me.grd.RootTable.Columns("PlotSize").Width = 70
            Me.grd.RootTable.Columns("TerritoryName").Width = 100
            Me.grd.RootTable.Columns("CityName").Width = 100

            Me.grd.RootTable.Columns("UserName").Width = 100
            Me.grd.RootTable.Columns("ModifiedUserName").Width = 100
            Me.grd.RootTable.Columns("ModifiedDate").Width = 100
            '// Setting format strings
            Me.grd.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
            Me.grd.RootTable.SortKeys.Add("PropertyProfileId", Janus.Windows.GridEX.SortOrder.Descending)
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

            'Me.grd.VerticalScroll.Value(0)
            Me.grd.MoveFirst()
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
                DoHaveDealRights = True
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
                    DoHaveDealRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                DoHaveDealRights = False
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
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    ElseIf RightsDt.FormControlName = "Deal Completion" Then
                        DoHaveDealRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ayesha Rehman : To show all saved records in the grid.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertyProfileDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            'isloaded = True
            'grd_FormattingRow(Nothing, Nothing)
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
    ''' <summary>
    ''' Ayesha Rehman : To open the Property Profile Form on btn Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            frmPropertyProfile.Id = 0
            frmPropertyProfile.DoHaveSaveRights = DoHaveSaveRights
            frmPropertyProfile.DoHaveDealRights = DoHaveDealRights
            frmPropertyProfile.IsEditMode = False
            frmPropertyProfile.IsDealCompleted = False
            frmPropertyProfile.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : To Delete the record of Property Profile from the table as well as from the data base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                PropertyProfile = New PropertyProfileBE
                PropertyProfile.PropertyProfileId = Val(Me.grd.CurrentRow.Cells("PropertyProfileId").Value.ToString)
                If e.Column.Key = "Delete" Then
                    Dim IsDealCompleted As Boolean = CBool(Me.grd.CurrentRow.Cells("IsDealCompleted").Value)
                    If IsDealCompleted = True Then
                        ShowErrorMessage("Deal has been completed so that you can not delete.")
                        Exit Sub
                    End If
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    objDAL.Delete(PropertyProfile)
                    Me.grd.GetRow.Delete()
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "PropertyProfile"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
    '    Try
    '        If isloaded = True Then
    '            If e.Row.Cells("ProfitShare").Value = 1 Then
    '                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
    '                rowstyle.BackColor = Color.LightGray
    '                e.Row.RowStyle = rowstyle
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Ayesha Rehman : To Open the Property Profile Record in the Update Mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Feb-2018 TFS2335 : Ayesha Rehman</remarks>
    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                frmPropertyProfile.Id = Val(Me.grd.CurrentRow.Cells("PropertyProfileId").Value.ToString)
                frmPropertyProfile.CostCenterId = Val(Me.grd.CurrentRow.Cells("CostCenterId").Value.ToString)
                frmPropertyProfile.DoHaveUpdateRights = DoHaveUpdateRights
                frmPropertyProfile.DoHaveDealRights = DoHaveDealRights
                frmPropertyProfile.IsDealCompleted = CBool(Me.grd.CurrentRow.Cells("IsDealCompleted").Value)
                frmPropertyProfile.ShowDialog()
                GetAllRecords()
            Else
                ShowErrorMessage("No record is selected")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPropertyProfileList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmPropertySearch.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class