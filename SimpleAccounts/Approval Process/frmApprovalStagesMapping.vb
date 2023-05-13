'14-Feb-2018 TFS2375 : Ayesha Rehman: Add new form to Map Processes With Stages and  and Save it
Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Public Class frmApprovalStagesMapping
    Implements IGeneral

    Dim ApprovalStagesMapppingId As Integer = 0I
    Dim ApprovalStagesMappping As ApprovalStagesMapppingBE
    Public objDAL As ApprovalStagesMapppingDAL = New ApprovalStagesMapppingDAL()
    Dim ApprovalUsersMappingId As Integer = 0I
    Dim ApprovalUsersMapping As ApprovalUsersMappingBE
    Dim ApprovalUsersMappingList As List(Of ApprovalUsersMappingBE)
    Public ApprovalUsersMappingDAL As ApprovalUsersMappingDAL = New ApprovalUsersMappingDAL()
    Dim FLevel As Integer = 0
    Enum EnmApprovalStagesMappping
        ApprovalStagesMapppingId
        ApprovalStagesId
        ApprovalProcessId
        Level
        Stage
        Process
    End Enum
    Enum EnmApprovalUsersMapping
        ApprovalUsersMappingId
        ApprovalStagesMapppingId
        StageLevel
        Process
        GroupId
        Group
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "grdStageMapping" Then
                Me.grdStageMapping.ColumnAutoResize = False
                Me.grdStageMapping.RootTable.Columns(1).Visible = False
                Me.grdStageMapping.RootTable.Columns(0).Visible = False
                Me.grdStageMapping.RootTable.Columns(2).Visible = False
                Me.grdStageMapping.RootTable.Columns(3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdStageMapping.RootTable.Columns(3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdStageMapping.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
                Me.grdStageMapping.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                Me.grdStageMapping.RootTable.Columns(5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
                Me.grdStageMapping.RootTable.Columns(5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                If Me.grdStageMapping.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdStageMapping.RootTable.Columns.Add("Delete")
                    Me.grdStageMapping.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdStageMapping.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdStageMapping.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("Delete").Width = 50
                    Me.grdStageMapping.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdStageMapping.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdStageMapping.RootTable.Columns("Delete").Caption = "Action"
                End If
                If Me.grdStageMapping.RootTable.Columns.Contains("MoveUp") = False Then
                    Me.grdStageMapping.RootTable.Columns.Add("MoveUp")
                    Me.grdStageMapping.RootTable.Columns("MoveUp").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdStageMapping.RootTable.Columns("MoveUp").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdStageMapping.RootTable.Columns("MoveUp").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("MoveUp").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("MoveUp").Width = 100
                    Me.grdStageMapping.RootTable.Columns("MoveUp").ButtonText = "MoveUp"
                    Me.grdStageMapping.RootTable.Columns("MoveUp").Key = "MoveUp"
                    Me.grdStageMapping.RootTable.Columns("MoveUp").Caption = "Action"
                End If
                If Me.grdStageMapping.RootTable.Columns.Contains("MoveDown") = False Then
                    Me.grdStageMapping.RootTable.Columns.Add("MoveDown")
                    Me.grdStageMapping.RootTable.Columns("MoveDown").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdStageMapping.RootTable.Columns("MoveDown").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdStageMapping.RootTable.Columns("MoveDown").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("MoveDown").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdStageMapping.RootTable.Columns("MoveDown").Width = 100
                    Me.grdStageMapping.RootTable.Columns("MoveDown").ButtonText = "MoveDown"
                    Me.grdStageMapping.RootTable.Columns("MoveDown").Key = "MoveDown"
                    Me.grdStageMapping.RootTable.Columns("MoveDown").Caption = "Action"
                End If

            End If
            Me.grdStageMapping.RootTable.Columns(3).Width = 100
            Me.grdStageMapping.RootTable.Columns(4).Width = 300
            Me.grdStageMapping.RootTable.Columns(5).Width = 300
            If Condition = "grdUserMapping" Then
                Me.grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.GroupId).Visible = False
                Me.grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.ApprovalStagesMapppingId).Visible = False
                Me.grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.ApprovalUsersMappingId).Visible = False
                Me.grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.Process).Visible = False
                Me.grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.StageLevel).Visible = False
                Me.grdUserMapping.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdUserMapping.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                If Me.grdUserMapping.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdUserMapping.RootTable.Columns.Add("Delete")
                    Me.grdUserMapping.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdUserMapping.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdUserMapping.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdUserMapping.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdUserMapping.RootTable.Columns("Delete").Width = 10
                    Me.grdUserMapping.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdUserMapping.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdUserMapping.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If

            If Me.grdUserMapping.RootTable.Groups(grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.StageLevel)) Is Nothing Then

                Dim grp As New Janus.Windows.GridEX.GridEXGroup()

                grp.Column = grdUserMapping.RootTable.Columns(EnmApprovalUsersMapping.StageLevel)
                Me.grdUserMapping.RootTable.Groups.Add(grp)

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman </remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                CtrlGrdBar2.mGridPrint.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmCOAGroupsToAccountsMapping)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                CtrlGrdBar2.mGridPrint.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False
                CtrlGrdBar2.mGridChooseFielder.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    For Each RightsDt As SBModel.GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Update" Then
                            If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Delete" Then
                            Me.BtnDelete.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Print" Then
                            Me.BtnPrint.Enabled = True
                            CtrlGrdBar1.mGridPrint.Enabled = True
                            CtrlGrdBar2.mGridPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                            CtrlGrdBar2.mGridExport.Enabled = True
                        ElseIf RightsDt.FormControlName = "Field Chooser" Then
                            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ayesha Rehman: FillCombos of UsersGroups , ApprovalProcess, ApprovalStages, ApprovalUsers
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty

            If Condition = "ApprovalProcess" Then

                str = "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder  "
                FillUltraDropDown(cmbApprovalProcess, str)
                Me.cmbApprovalProcess.Rows(0).Activate()
                Me.cmbApprovalProcess.DisplayLayout.Bands(0).Columns("ApprovalProcessId").Hidden = True
                Me.cmbApprovalProcess.DisplayLayout.Bands(0).Columns("Title").Width = 500
            ElseIf Condition = "Groups" Then
                str = "Select GroupId , GroupName  from tblUserGroup where Active = 1 order by SortOrder "
                FillUltraDropDown(cmbUserGroup, str)
                Me.cmbUserGroup.Rows(0).Activate()
                Me.cmbUserGroup.DisplayLayout.Bands(0).Columns("GroupId").Hidden = True
                Me.cmbUserGroup.DisplayLayout.Bands(0).Columns("GroupName").Width = 500
            ElseIf Condition = "ApprovalStages" Then
                str = "Select ApprovalStagesId  , Title from ApprovalStages  where Active = 1 order by SortOrder  "
                FillUltraDropDown(cmbStages, str)
                Me.cmbStages.Rows(0).Activate()
                Me.cmbStages.DisplayLayout.Bands(0).Columns("ApprovalStagesId").Hidden = True
                Me.cmbStages.DisplayLayout.Bands(0).Columns("Title").Width = 250
            ElseIf Condition = "ApprovalUsers" Then

                str = "SELECT     ApprovalStagesMappping.ApprovalStagesMapppingId, Convert(nvarchar(50),ApprovalStagesMappping.[Level]) + ' ' +  ApprovalStages.Title  AS LevelStage" _
                        & " FROM         ApprovalProcess INNER JOIN " _
                        & " ApprovalStagesMappping ON ApprovalProcess.ApprovalProcessId = ApprovalStagesMappping.ApprovalProcessId INNER JOIN " _
                       & " ApprovalStages ON ApprovalStagesMappping.ApprovalStagesId = ApprovalStages.ApprovalStagesId " _
                       & " Where ApprovalProcess.ApprovalProcessId = " & cmbApprovalProcess.ActiveRow.Cells(0).Value & ""
                FillUltraDropDown(cmbMappedStages, str)
                Me.cmbMappedStages.Rows(0).Activate()
                Me.cmbMappedStages.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbMappedStages.DisplayLayout.Bands(0).Columns(1).Width = 500


            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Fillmodel to get the data  for ApprovalStagesMappping Table
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            'COAListGrpAcc = New List(Of ApprovalStagesMapppingBE)7

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdStageMapping.GetRows
            ApprovalStagesMappping = New ApprovalStagesMapppingBE
            ApprovalStagesMappping.ApprovalStagesMapppingId = ApprovalStagesMapppingId
            ApprovalStagesMappping.ApprovalProcessId = Me.cmbApprovalProcess.ActiveRow.Cells(0).Value
            ApprovalStagesMappping.ApprovalStagesId = Me.cmbStages.ActiveRow.Cells(0).Value
            ApprovalStagesMappping.Level = Me.grdStageMapping.RowCount

            'Next

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdUserMapping.GetRows
            '    COAGrpAcc = New ApprovalStagesMapppingBE
            '    COAGrpAcc.COAGroupMappingId = Id
            '    COAGrpAcc.COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
            '    COAGrpAcc.AccountId = r.Cells(1).Value
            '    COAGrpAcc.AccountLevel = 1
            '    COAListGrpAcc.Add(COAGrpAcc)
            'Next




        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModelForApprovalUser(Optional Condition As String = "")
        Try
            'ApprovalUsersMappingList = New List(Of ApprovalUsersMappingBE)

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdUserMapping.GetRows
            ApprovalUsersMapping = New ApprovalUsersMappingBE
            ApprovalUsersMapping.ApprovalUsersMappingId = ApprovalUsersMappingId
            ApprovalUsersMapping.ApprovalStagesMapppingId = Me.cmbMappedStages.ActiveRow.Cells(0).Value
            ApprovalUsersMapping.GroupId = Me.cmbUserGroup.ActiveRow.Cells(0).Value
            'ApprovalUsersMappingList.Add(ApprovalUsersMapping)
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grids.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            If Condition = "StageMapping" Then
                GetApprovalStages(-1)
            End If
            If Condition = "UserMapping" Then
                GetApprovalUsers(-1)

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grdStageMapping According to the ApprovalProcessId.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Private Sub GetApprovalStages(ByVal Id As Integer)
        Try
            Dim strsql As String = "SELECT     ApprovalStagesMappping.ApprovalStagesMapppingId, ApprovalStagesMappping.ApprovalStagesId, ApprovalStagesMappping.ApprovalProcessId, ApprovalStagesMappping.[Level], " _
                                  & " ApprovalStages.Title As Stage, ApprovalProcess.Title AS Process" _
                                  & " FROM  ApprovalProcess INNER JOIN " _
                                  & " ApprovalStagesMappping ON ApprovalProcess.ApprovalProcessId = ApprovalStagesMappping.ApprovalProcessId INNER JOIN " _
                                  & " ApprovalStages ON ApprovalStagesMappping.ApprovalStagesId = ApprovalStages.ApprovalStagesId where  ApprovalStagesMappping.ApprovalProcessId = " & Id & " order by [Level] "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdStageMapping.DataSource = dt
            Me.grdStageMapping.RetrieveStructure()

            Me.grdStageMapping.RootTable.Columns(0).Visible = False
            ApplyGridSettings("grdStageMapping")
        Catch ex As Exception

        End Try
    End Sub
    'Public Function ValidateStage() As Boolean
    '    Try

    '        'Ayesha Rehman: Added to check if type is exists already
    '        Dim str As String = "SELECT * FROM ApprovalStages WHERE Title = '" & Me.txtAcTitle.Text & "'"
    '        Dim dt As DataTable = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                ShowErrorMessage("Account exists already")
    '                Me.cmbStages.Focus()
    '                Return False
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception

    '    End Try
    'End Function
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grdUserMapping According to the ApprovalProcessId.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Private Sub GetApprovalUsers(ByVal Id As Integer)
        Try
            Dim strsql As String = "SELECT      ApprovalUsersMapping.ApprovalUsersMappingId, ApprovalUsersMapping.ApprovalStagesMapppingId, Convert(nvarchar(50),ApprovalStagesMappping.[Level]) + ' ' +  ApprovalStages.Title AS StageLevel,  " _
                                    & " ApprovalProcess.Title AS Process, ApprovalUsersMapping.GroupId, tblUserGroup.GroupName AS [Group] " _
                                    & "  FROM  ApprovalProcess INNER JOIN " _
                                    & " ApprovalStagesMappping ON ApprovalProcess.ApprovalProcessId = ApprovalStagesMappping.ApprovalProcessId INNER JOIN" _
                                    & " ApprovalUsersMapping ON ApprovalStagesMappping.ApprovalStagesMapppingId = ApprovalUsersMapping.ApprovalStagesMapppingId INNER JOIN " _
                                    & "ApprovalStages ON ApprovalStagesMappping.ApprovalStagesId = ApprovalStages.ApprovalStagesId INNER JOIN" _
                                    & " tblUserGroup ON ApprovalUsersMapping.GroupId = tblUserGroup.GroupId where  ApprovalProcess.ApprovalProcessId = " & Id & " "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdUserMapping.DataSource = dt
            Me.grdUserMapping.RetrieveStructure()

            Me.grdUserMapping.RootTable.Columns(0).Visible = False
            ApplyGridSettings("grdUserMapping")
        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' Ayesha Rehman : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            FillModelForApprovalUser()
            Return True
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Ayesha Rehman : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>14-Feb-2018 TFS2375 : Ayesha Rehman</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"

            Me.cmbApprovalProcess.Rows(0).Activate()
            Me.cmbStages.Rows(0).Activate()

            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)

            Me.GetAllRecords("StageMapping")
            'Me.GetAllRecords("Sub")

            ApplyGridSettings("grdStageMapping")
            ApplyGridSettings("grdUserMapping")

            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStageMapping.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStageMapping.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdStageMapping.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Approval Stages Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdUserMapping.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdUserMapping.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdUserMapping.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Approval Users Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If New ApprovalStagesMapppingDAL().Add(ApprovalStagesMappping) Then
                'Insert Activity Log by Ayesha Rehman 
                SaveActivityLog("Approvals", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, "Approval Stages", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function Save1(Optional Condition As String = "") As Boolean
        Try

            If New ApprovalUsersMappingDAL().Add(ApprovalUsersMapping) Then
                'Insert Activity Log by Ayesha Rehman 
                SaveActivityLog("Approvals", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, "Approval Process", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function


    Function UpdatePrevious(ByVal objModel As ApprovalStagesMapppingBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = " Update ApprovalStagesMappping Set Level = Level - 1   where Level > " & objModel.Level & " And ApprovalProcessId = " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Function UpdateForMoveUp(ByVal objModel As ApprovalStagesMapppingBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction


            str = " Update ApprovalStagesMappping Set Level = " & objModel.Level & "    where Level = " & objModel.Level & " - 1 And ApprovalProcessId = " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = " Update ApprovalStagesMappping Set Level = " & objModel.Level & " - 1   where ApprovalStagesMapppingId = " & objModel.ApprovalStagesMapppingId & "  And ApprovalProcessId = " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Function UpdateForMoveDown(ByVal objModel As ApprovalStagesMapppingBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = " Update ApprovalStagesMappping Set Level = " & objModel.Level & "  where Level = " & objModel.Level & " + 1 And ApprovalProcessId = " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = " Update ApprovalStagesMappping Set Level = " & objModel.Level & " + 1   where ApprovalStagesMapppingId = " & objModel.ApprovalStagesMapppingId & "  And ApprovalProcessId = " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
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

    Private Sub frmApprovalStagesMapping_RightToLeftLayoutChanged(sender As Object, e As EventArgs) Handles Me.RightToLeftLayoutChanged

    End Sub

    Private Sub frmApprovalStagesMapping_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            ''filling the combos
            Me.FillCombos()
            FillCombos("ApprovalProcess")
            FillCombos("ApprovalStages")
            FillCombos("Groups")


            ''getting the grid
            Me.GetAllRecords("StageMapping")
            Me.GetAllRecords("UserMapping")

            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)

            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMain_Click(sender As Object, e As EventArgs) Handles btnMain.Click
        Try

            If cmbApprovalProcess.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a Process")
                Me.cmbApprovalProcess.Focus()
                Exit Sub
            End If
            If cmbStages.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a Stage")
                Me.cmbApprovalProcess.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To grdStageMapping.GetRows.Length - 1
                If cmbStages.ActiveRow.Cells(0).Value = grdStageMapping.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Stage Already Exists")
                    Me.cmbStages.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdStageMapping.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnmApprovalStagesMappping.ApprovalStagesMapppingId) = ApprovalStagesMapppingId
            dr(EnmApprovalStagesMappping.ApprovalStagesId) = cmbStages.ActiveRow.Cells(0).Value
            dr(EnmApprovalStagesMappping.ApprovalProcessId) = cmbApprovalProcess.ActiveRow.Cells(0).Value
            dr(EnmApprovalStagesMappping.Stage) = cmbStages.ActiveRow.Cells(1).Value.ToString
            dr(EnmApprovalStagesMappping.Process) = cmbApprovalProcess.ActiveRow.Cells(1).Value.ToString
            dr(EnmApprovalStagesMappping.Level) = dtGrid.Rows.Count + 1
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()
            FillModel()
            Save()
            GetApprovalStages(cmbApprovalProcess.ActiveRow.Cells(0).Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSub_Click(sender As Object, e As EventArgs) Handles btnSub.Click
        Try

            If cmbApprovalProcess.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a Process")
                Me.cmbApprovalProcess.Focus()
                Exit Sub
            ElseIf cmbMappedStages.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a Stage to map with User")
                Me.cmbMappedStages.Focus()
                Exit Sub
            ElseIf cmbUserGroup.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a Group")
                Me.cmbUserGroup.Focus()
                Exit Sub

            End If
            Dim i As Integer = 0
            For i = 0 To grdUserMapping.GetDataRows.Length - 1
                If cmbMappedStages.ActiveRow.Cells(0).Value = grdUserMapping.GetDataRows(i).Cells(EnmApprovalUsersMapping.ApprovalStagesMapppingId).Value AndAlso cmbUserGroup.ActiveRow.Cells(0).Value = grdUserMapping.GetDataRows(i).Cells(EnmApprovalUsersMapping.GroupId).Value Then
                    ShowErrorMessage("This Group Mapping Already Exists")
                    Me.cmbMappedStages.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdUserMapping.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnmApprovalUsersMapping.ApprovalUsersMappingId) = ApprovalUsersMappingId
            dr(EnmApprovalUsersMapping.ApprovalStagesMapppingId) = cmbMappedStages.ActiveRow.Cells(0).Value
            dr(EnmApprovalUsersMapping.StageLevel) = cmbMappedStages.ActiveRow.Cells(1).Value.ToString
            dr(EnmApprovalUsersMapping.Process) = cmbApprovalProcess.ActiveRow.Cells(1).Value.ToString
            dr(EnmApprovalUsersMapping.GroupId) = cmbUserGroup.ActiveRow.Cells(0).Value
            dr(EnmApprovalUsersMapping.Group) = cmbUserGroup.ActiveRow.Cells(1).Value.ToString
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()
            FillModelForApprovalUser()
            Save1()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub





    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = True Then
                If BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()

                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStageMapping_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStageMapping.ColumnButtonClick
        Try

            ApprovalStagesMappping = New ApprovalStagesMapppingBE
            ApprovalStagesMappping.ApprovalStagesMapppingId = Val(Me.grdStageMapping.CurrentRow.Cells(EnmApprovalStagesMappping.ApprovalStagesMapppingId).Value.ToString)
            ApprovalStagesMappping.ApprovalProcessId = Val(Me.grdStageMapping.CurrentRow.Cells(EnmApprovalStagesMappping.ApprovalProcessId).Value.ToString)
            ApprovalStagesMappping.Level = Val(Me.grdStageMapping.CurrentRow.Cells(EnmApprovalStagesMappping.Level).Value.ToString)
            FLevel = ApprovalStagesMappping.Level
            ApprovalStagesMappping.ApprovalStagesId = Val(Me.grdStageMapping.CurrentRow.Cells(EnmApprovalStagesMappping.ApprovalStagesId).Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(ApprovalStagesMappping)
                Me.grdStageMapping.GetRow.Delete()
                UpdatePrevious(ApprovalStagesMappping)
                GetApprovalStages(ApprovalStagesMappping.ApprovalProcessId)
            End If
            If e.Column.Key = "MoveUp" Then
                If Me.grdStageMapping.CurrentRow.RowIndex = 0 Then
                    ShowInformationMessage("This Record can not be moved up further")
                    Exit Sub
                End If
                If Not msg_Confirm("Do You Want to Move This Record At upper Level") = True Then Exit Sub
                UpdateForMoveUp(ApprovalStagesMappping)
                'UpdateForMoveDown(ApprovalStagesMappping)
                GetApprovalStages(ApprovalStagesMappping.ApprovalProcessId)

            End If
            If e.Column.Key = "MoveDown" Then
                If Me.grdStageMapping.CurrentRow.RowIndex = Me.grdStageMapping.RowCount - 1 Then
                    ShowInformationMessage("This Record can not be moved down further")
                    Exit Sub
                End If
                If Not msg_Confirm("Do You Want to Move This Record At lower Level") = True Then Exit Sub
                UpdateForMoveDown(ApprovalStagesMappping)
                'UpdateForMoveUp(ApprovalStagesMappping)
                GetApprovalStages(ApprovalStagesMappping.ApprovalProcessId)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub grdSub_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdUserMapping.ColumnButtonClick
    '    Try
    '        COAGrpAcc = New ApprovalStagesMapppingBE
    '        COAGrpAcc.COAGroupMappingId = Val(Me.grdSub.CurrentRow.Cells("COAGroupMappingId").Value.ToString)
    '        If e.Column.Key = "Delete" Then
    '            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
    '            objDAL.Delete(COAGrpAcc)
    '            Me.grdUserMapping.GetRow.Delete()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub



    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub cmbApprovalProcess_ValueChanged(sender As Object, e As EventArgs) Handles cmbApprovalProcess.ValueChanged
        Try
            GetApprovalStages(cmbApprovalProcess.ActiveRow.Cells(0).Value)
            GetApprovalUsers(cmbApprovalProcess.ActiveRow.Cells(0).Value)
            FillCombos("ApprovalUsers")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ''filling the combos
            Me.FillCombos()
            FillCombos("ApprovalProcess")
            FillCombos("Groups")
            FillCombos("ApprovalStages")
            FillCombos("ApprovalUsers")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub TabDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabDetail.SelectedIndexChanged
        Try
            If TabDetail.SelectedIndex = 0 Then
                CtrlGrdBar1.Visible = True
                CtrlGrdBar2.Visible = False

            ElseIf TabDetail.SelectedIndex = 1 Then
                CtrlGrdBar1.Visible = False
                CtrlGrdBar2.Visible = True
                FillCombos("ApprovalUsers")
                FillCombos("Groups")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdUserMapping_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdUserMapping.ColumnButtonClick
        Try
            ApprovalUsersMapping = New ApprovalUsersMappingBE
            ApprovalUsersMapping.ApprovalUsersMappingId = Val(Me.grdUserMapping.CurrentRow.Cells(EnmApprovalUsersMapping.ApprovalUsersMappingId).Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                ApprovalUsersMappingDAL.Delete(ApprovalUsersMapping)
                Me.grdUserMapping.GetRow.Delete()
            End If
         
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


End Class