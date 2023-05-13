'02-Feb-2018 TFS2222 : Ayesha Rehman: Add new form to Map Accounts With the group and Save it
Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Public Class frmCOAGroupsToAccountsMapping
    Implements IGeneral

    Dim Id As Integer = 0I
    Dim COAGrpAcc As COAAccountMappingBE

    Dim COAListGrpAcc As List(Of COAAccountMappingBE)
    Public objDAL As COAAccountMappingDAL = New COAAccountMappingDAL()
    Enum EnumGrd
        COAId
        Id
        Code
        Title
        AccountLevel
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Main" Then
                Me.grdMain.RootTable.Columns(1).Visible = False
                Me.grdMain.RootTable.Columns(0).Visible = False
                Me.grdMain.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdMain.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                If Me.grdMain.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdMain.RootTable.Columns.Add("Delete")
                    Me.grdMain.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdMain.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdMain.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdMain.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdMain.RootTable.Columns("Delete").Width = 20
                    Me.grdMain.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdMain.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdMain.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If
            If Condition = "Sub" Then
                Me.grdSub.RootTable.Columns(1).Visible = False
                Me.grdSub.RootTable.Columns(0).Visible = False
                Me.grdSub.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSub.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                If Me.grdSub.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdSub.RootTable.Columns.Add("Delete")
                    Me.grdSub.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdSub.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdSub.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdSub.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdSub.RootTable.Columns("Delete").Width = 20
                    Me.grdSub.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdSub.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdSub.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If
            If Condition = "SubSub" Then
                Me.grdSubSub.RootTable.Columns(1).Visible = False
                Me.grdSubSub.RootTable.Columns(0).Visible = False
                Me.grdSubSub.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSubSub.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                If Me.grdSubSub.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdSubSub.RootTable.Columns.Add("Delete")
                    Me.grdSubSub.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdSubSub.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdSubSub.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdSubSub.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdSubSub.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdSubSub.RootTable.Columns("Delete").Width = 20
                    Me.grdSubSub.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdSubSub.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If
            If Condition = "Detail" Then
                Me.grdDetail.RootTable.Columns(1).Visible = False
                Me.grdDetail.RootTable.Columns(0).Visible = False
                Me.grdDetail.RootTable.Columns(4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdDetail.RootTable.Columns(4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                If Me.grdDetail.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdDetail.RootTable.Columns.Add("Delete")
                    Me.grdDetail.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdDetail.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdDetail.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdDetail.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdDetail.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdDetail.RootTable.Columns("Delete").Width = 20
                    Me.grdDetail.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdDetail.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman </remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                ' Me.btnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
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
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                      
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ayesha Rehman: FillCombos of Groups , MainAccount, DetailAccount, SubAccount, SubSubAccount
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty

            If Condition = "COAGroupsAccount" Then

                str = "Select COAGroupId , Title  from COAGroups Where Active = 1 order by SortOrder "
                FillUltraDropDown(cmbCOAGroupsAccount, str)
                Me.cmbCOAGroupsAccount.Rows(0).Activate()
                Me.cmbCOAGroupsAccount.DisplayLayout.Bands(0).Columns("COAGroupId").Hidden = True
                Me.cmbCOAGroupsAccount.DisplayLayout.Bands(0).Columns("Title").Width = 500
            ElseIf Condition = "MainAccount" Then
                str = "Select coa_main_id , main_code As Code , main_title As Title from tblCOAMain "
                FillUltraDropDown(cmbMainAccount, str)
                Me.cmbMainAccount.Rows(0).Activate()
                Me.cmbMainAccount.DisplayLayout.Bands(0).Columns("coa_main_id").Hidden = True
                Me.cmbMainAccount.DisplayLayout.Bands(0).Columns("Code").Width = 250
                Me.cmbMainAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250
            ElseIf Condition = "SubAccount" Then
                str = "Select main_sub_id , sub_code As Code , sub_title As Title  from tblCOAMainSub "
                FillUltraDropDown(cmbSubAccount, str)
                Me.cmbSubAccount.Rows(0).Activate()
                Me.cmbSubAccount.DisplayLayout.Bands(0).Columns("main_sub_id").Hidden = True
                Me.cmbSubAccount.DisplayLayout.Bands(0).Columns("Code").Width = 250
                Me.cmbSubAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250
            ElseIf Condition = "SubSubAccount" Then
                str = "Select main_sub_sub_id , sub_sub_code As Code , sub_sub_title As Title from tblCOAMainSubSub "
                FillUltraDropDown(cmbSubSubAccount, str)
                Me.cmbSubSubAccount.Rows(0).Activate()
                Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns("main_sub_sub_id").Hidden = True
                Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns("Code").Width = 250
                Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250
            ElseIf Condition = "DetailAccount" Then
                str = "Select coa_detail_id , detail_code As Code , detail_title As Title from tblCOAMainSubSubDetail "
                FillUltraDropDown(cmbDetailAccount, str)
                Me.cmbDetailAccount.Rows(0).Activate()
                Me.cmbDetailAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbDetailAccount.DisplayLayout.Bands(0).Columns("Code").Width = 250
                Me.cmbDetailAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Fillmodel to get a list of data of All Accounts
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            COAListGrpAcc = New List(Of COAAccountMappingBE)


            Dim COAGrpAcc As COAAccountMappingBE

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdMain.GetRows
                COAGrpAcc = New COAAccountMappingBE
                COAGrpAcc.COAGroupMappingId = Id
                COAGrpAcc.COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
                COAGrpAcc.AccountId = r.Cells(1).Value
                COAGrpAcc.AccountLevel = 0
                COAListGrpAcc.Add(COAGrpAcc)
            Next

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSub.GetRows
                COAGrpAcc = New COAAccountMappingBE
                COAGrpAcc.COAGroupMappingId = Id
                COAGrpAcc.COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
                COAGrpAcc.AccountId = r.Cells(1).Value
                COAGrpAcc.AccountLevel = 1
                COAListGrpAcc.Add(COAGrpAcc)
            Next

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSubSub.GetRows
                COAGrpAcc = New COAAccountMappingBE
                COAGrpAcc.COAGroupMappingId = Id
                COAGrpAcc.COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
                COAGrpAcc.AccountId = r.Cells(1).Value
                COAGrpAcc.AccountLevel = 2
                COAListGrpAcc.Add(COAGrpAcc)
            Next


            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                COAGrpAcc = New COAAccountMappingBE
                COAGrpAcc.COAGroupMappingId = Id
                COAGrpAcc.COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
                COAGrpAcc.AccountId = r.Cells(1).Value
                COAGrpAcc.AccountLevel = 3
                COAListGrpAcc.Add(COAGrpAcc)
            Next

            'COAGrpAcc.SubAccount = New List(Of COASubAccountMappingBE)
            'Dim SubAccounts As COASubAccountMappingBE

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSub.GetRows
            '    SubAccounts = New COASubAccountMappingBE
            '    SubAccounts.AccountId = r.Cells(1).Value
            '    SubAccounts.AccountLevel = r.Cells(1).Value
            '    COAGrpAcc.SubAccount.Add(SubAccounts)
            'Next

            'COAGrpAcc.SubSubAccount = New List(Of COASubSubAccountMappingBE)
            'Dim SubSubAccounts As COASubSubAccountMappingBE

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSubSub.GetRows
            '    SubSubAccounts = New COASubSubAccountMappingBE
            '    SubSubAccounts.AccountId = r.Cells(1).Value
            '    SubSubAccounts.AccountLevel = r.Cells(1).Value
            '    COAGrpAcc.SubSubAccount.Add(SubSubAccounts)
            'Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grids.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            If Condition = "Main" Then
                GetMainAccount(-1)
            End If
            If Condition = "Sub" Then
                GetSubAccount(-1)

            End If
            If Condition = "SubSub" Then
                GetSubSubAccount(-1)
            End If
            If Condition = "Detail" Then
                GetDetailAccount(-1)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the Maingrid According to the GroupId.
    ''' </summary>
    ''' <param name="COAId"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Private Sub GetMainAccount(ByVal COAId As Integer)
        Try
            Dim strsql As String = "Select COAGroupMappingId , Main.coa_main_id , Main.main_code As AccountCode , Main.main_title As AccountTitle , AccountLevel from COAAccountMapping Left Outer Join tblCOAMain Main on COAAccountMapping.AccountId = Main.coa_main_id where Main.coa_main_id > 0 And AccountLevel = 0  And COAGroupId = " & COAId & " "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdMain.DataSource = dt
            Me.grdMain.RetrieveStructure()

            Me.grdMain.RootTable.Columns(0).Visible = False
            ApplyGridSettings("Main")
        Catch ex As Exception

        End Try
    End Sub
    'Public Function ValidateMainAccount() As Boolean
    '    Try

    '        'Ayesha Rehman: Added to check if type is exists already
    '        Dim str As String = "SELECT * FROM COAAccountMapping WHERE Title = '" & Me.txtAccountGroupTitle.Text & "'"
    '        Dim dt As DataTable = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                ShowErrorMessage("Account exists already")
    '                Me.cmbMainAccount.Focus()
    '                Return False
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception

    '    End Try
    'End Function
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the Subgrid According to the GroupId.
    ''' </summary>
    ''' <param name="COAId"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Private Sub GetSubAccount(ByVal COAId As Integer)
        Try
            Dim strsql As String = "Select COAGroupMappingId, main_sub_id ,sub_code As AccountCode ,sub_title As AccountTitle , AccountLevel  from COAAccountMapping Left Outer Join tblCOAMainSub Sub on COAAccountMapping.AccountId = Sub.main_sub_id where Sub.main_sub_id  > 0 And AccountLevel = 1 And COAGroupId = " & COAId & " "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdSub.DataSource = dt
            Me.grdSub.RetrieveStructure()

            Me.grdSub.RootTable.Columns(0).Visible = False
            ApplyGridSettings("Sub")
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the SubSubgrid According to the GroupId.
    ''' </summary>
    ''' <param name="COAId"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Private Sub GetSubSubAccount(ByVal COAId As Integer)
        Try
            Dim strsql As String = "Select COAGroupMappingId, main_sub_sub_id , sub_sub_code As AccountCode ,sub_sub_title As AccountTitle , AccountLevel from COAAccountMapping Left Outer Join tblCOAMainSubSub  SubSub on COAAccountMapping.AccountId = SubSub.main_sub_sub_id where SubSub.main_sub_sub_id  > 0 And AccountLevel = 2 And COAGroupId = " & COAId & " "
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdSubSub.DataSource = dt
            Me.grdSubSub.RetrieveStructure()

            Me.grdSubSub.RootTable.Columns(0).Visible = False
            ApplyGridSettings("SubSub")
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the Detailgrid According to the GroupId.
    ''' </summary>
    ''' <param name="COAId"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Private Sub GetDetailAccount(ByVal COAId As Integer)
        Try
            Dim strsql As String = "Select COAGroupMappingId , coa_detail_id , detail_code As AccountCode , detail_title As AccountTitle , AccountLevel from COAAccountMapping Left Outer Join tblCOAMainSubSubDetail Detail on COAAccountMapping.AccountId = Detail.coa_detail_id where Detail.coa_detail_id  > 0 And AccountLevel = 3 And COAGroupId = " & COAId & ""
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grdDetail.DataSource = dt
            Me.grdDetail.RetrieveStructure()
            Me.grdDetail.RootTable.Columns(0).Visible = False
            ApplyGridSettings("Detail")
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Ayesha Rehman : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>02-Feb-2018 TFS2222 : Ayesha Rehman</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"

            Me.cmbCOAGroupsAccount.Rows(0).Activate()
            Me.cmbMainAccount.Rows(0).Activate()
            Me.cmbSubAccount.Rows(0).Activate()
            Me.cmbSubSubAccount.Rows(0).Activate()
            Me.cmbDetailAccount.Rows(0).Activate()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)
            CtrlGrdBar4_Load(Nothing, Nothing)

            Me.GetAllRecords("Main")
            Me.GetAllRecords("Sub")
            Me.GetAllRecords("SubSub")
            Me.GetAllRecords("Detail")
            ApplyGridSettings("Main")
            ApplyGridSettings("Sub")
            ApplyGridSettings("SubSub")
            ApplyGridSettings("Detail")
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMain.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMain.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdMain.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Groups To Account Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSub.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSub.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdSub.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Groups To Account Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSubSub.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSubSub.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdSubSub.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Groups To Account Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar4.txtGridTitle.Text = CompanyTitle & Chr(10) & "Groups To Account Mapping"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim COAGroupId As Integer = 0
            If Me.grdMain.RowCount = 0 AndAlso Me.grdSub.RowCount = 0 AndAlso Me.grdSubSub.RowCount = 0 AndAlso Me.grdDetail.RowCount = 0 AndAlso Me.grdMain.RowCount = 0 Then Exit Function
            COAGroupId = Me.cmbCOAGroupsAccount.ActiveRow.Cells(0).Value
            COAGrpAcc = New COAAccountMappingBE
            COAGrpAcc.COAGroupId = COAGroupId
            DeletePrevious(COAGrpAcc)
            If New COAAccountMappingDAL().Add(COAListGrpAcc) Then
                'Insert Activity Log by Ayesha Rehman 
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, "Accounts Group", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function
    Function DeletePrevious(ByVal objModel As COAAccountMappingBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            'Delete Master records
            str = "Delete from COAAccountMapping  where COAGroupId= " & objModel.COAGroupId
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

    Private Sub frmCOAGroupsToAccountsMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
       
    End Sub

    

    Private Sub frmCOAGroupsToAccountsMapping_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            ''filling the combos
            Me.FillCombos()
            FillCombos("COAGroupsAccount")
            FillCombos("MainAccount")
            FillCombos("SubAccount")
            FillCombos("SubSubAccount")
            FillCombos("DetailAccount")
            ''getting the grid
            Me.GetAllRecords("Main")
            Me.GetAllRecords("Sub")
            Me.GetAllRecords("SubSub")
            Me.GetAllRecords("Detail")
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)
            CtrlGrdBar4_Load(Nothing, Nothing)
            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMain_Click(sender As Object, e As EventArgs) Handles btnMain.Click
        Try

            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Exit Sub
            ElseIf cmbMainAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select an account")
                Me.cmbMainAccount.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To grdMain.GetRows.Length - 1
                If cmbMainAccount.ActiveRow.Cells(0).Value = grdMain.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Account Already Exists")
                    Me.cmbMainAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdMain.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.COAId) = Id
            dr(EnumGrd.Id) = cmbMainAccount.ActiveRow.Cells(0).Value
            dr(EnumGrd.Code) = cmbMainAccount.ActiveRow.Cells(1).Value.ToString
            dr(EnumGrd.Title) = cmbMainAccount.ActiveRow.Cells(2).Value.ToString
            dr(EnumGrd.AccountLevel) = 0
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSub_Click(sender As Object, e As EventArgs) Handles btnSub.Click
        Try

            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Exit Sub
            ElseIf cmbSubAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select an account")
                Me.cmbSubAccount.Focus()
                Exit Sub

            End If
            Dim i As Integer = 0
            For i = 0 To grdSub.GetRows.Length - 1
                If cmbSubAccount.ActiveRow.Cells(0).Value = grdSub.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Account Already Exists")
                    Me.cmbSubAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdSub.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.COAId) = Id
            dr(EnumGrd.Id) = cmbSubAccount.ActiveRow.Cells(0).Value
            dr(EnumGrd.Code) = cmbSubAccount.ActiveRow.Cells(1).Value.ToString
            dr(EnumGrd.Title) = cmbSubAccount.ActiveRow.Cells(2).Value.ToString
            dr(EnumGrd.AccountLevel) = 1
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSubSub_Click(sender As Object, e As EventArgs) Handles btnSubSub.Click
        Try

            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Exit Sub
            ElseIf cmbSubSubAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select an account")
                Me.cmbSubSubAccount.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To grdSubSub.GetRows.Length - 1
                If cmbSubSubAccount.ActiveRow.Cells(0).Value = grdSubSub.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Account Already Exists")
                    Me.cmbSubSubAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdSubSub.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.COAId) = Id
            dr(EnumGrd.Id) = cmbSubSubAccount.ActiveRow.Cells(0).Value
            dr(EnumGrd.Code) = cmbSubSubAccount.ActiveRow.Cells(1).Value.ToString
            dr(EnumGrd.Title) = cmbSubSubAccount.ActiveRow.Cells(2).Value.ToString
            dr(EnumGrd.AccountLevel) = 2
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        Try

            If cmbCOAGroupsAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select a group")
                Me.cmbCOAGroupsAccount.Focus()
                Exit Sub
            ElseIf cmbDetailAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select an account")
                Me.cmbDetailAccount.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To grdDetail.GetRows.Length - 1
                If cmbDetailAccount.ActiveRow.Cells(0).Value = grdDetail.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Account Already Exists")
                    Me.cmbDetailAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grdDetail.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.COAId) = Id
            dr(EnumGrd.Id) = cmbDetailAccount.ActiveRow.Cells(0).Value
            dr(EnumGrd.Code) = cmbDetailAccount.ActiveRow.Cells(1).Value.ToString
            dr(EnumGrd.Title) = cmbDetailAccount.ActiveRow.Cells(2).Value.ToString
            dr(EnumGrd.AccountLevel) = 3
            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = True Then
                If BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then

                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
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

    Private Sub grdMain_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.ColumnButtonClick
        Try
            COAGrpAcc = New COAAccountMappingBE
            COAGrpAcc.COAGroupMappingId = Val(Me.grdMain.CurrentRow.Cells("COAGroupMappingId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGrpAcc)
                Me.grdMain.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSub_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSub.ColumnButtonClick
        Try
            COAGrpAcc = New COAAccountMappingBE
            COAGrpAcc.COAGroupMappingId = Val(Me.grdSub.CurrentRow.Cells("COAGroupMappingId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGrpAcc)
                Me.grdSub.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSubSub_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSubSub.ColumnButtonClick
        Try
            COAGrpAcc = New COAAccountMappingBE
            COAGrpAcc.COAGroupMappingId = Val(Me.grdSubSub.CurrentRow.Cells("COAGroupMappingId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGrpAcc)
                Me.grdSubSub.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            COAGrpAcc = New COAAccountMappingBE
            COAGrpAcc.COAGroupMappingId = Val(Me.grdDetail.CurrentRow.Cells("COAGroupMappingId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(COAGrpAcc)
                Me.grdDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub cmbCOAGroupsAccount_ValueChanged(sender As Object, e As EventArgs) Handles cmbCOAGroupsAccount.ValueChanged
        Try
            GetMainAccount(cmbCOAGroupsAccount.ActiveRow.Cells(0).Value)
            GetSubAccount(cmbCOAGroupsAccount.ActiveRow.Cells(0).Value)
            GetSubSubAccount(cmbCOAGroupsAccount.ActiveRow.Cells(0).Value)
            GetDetailAccount(cmbCOAGroupsAccount.ActiveRow.Cells(0).Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ''filling the combos
            Me.FillCombos()
            FillCombos("COAGroupsAccount")
            FillCombos("MainAccount")
            FillCombos("SubAccount")
            FillCombos("SubSubAccount")
            FillCombos("DetailAccount")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub TabDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabDetail.SelectedIndexChanged
        Try
            If TabDetail.SelectedIndex = 0 Then
                CtrlGrdBar1.Visible = True
                CtrlGrdBar2.Visible = False
                CtrlGrdBar3.Visible = False
                CtrlGrdBar4.Visible = False

            ElseIf TabDetail.SelectedIndex = 1 Then
                CtrlGrdBar1.Visible = False
                CtrlGrdBar2.Visible = True
                CtrlGrdBar3.Visible = False
                CtrlGrdBar4.Visible = False

            ElseIf TabDetail.SelectedIndex = 2 Then
                CtrlGrdBar1.Visible = False
                CtrlGrdBar2.Visible = False
                CtrlGrdBar3.Visible = True
                CtrlGrdBar4.Visible = False

            ElseIf TabDetail.SelectedIndex = 3 Then
                CtrlGrdBar1.Visible = False
                CtrlGrdBar2.Visible = False
                CtrlGrdBar3.Visible = False
                CtrlGrdBar4.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click

    End Sub
End Class