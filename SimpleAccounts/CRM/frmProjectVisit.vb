''Ahmad Sharif: Task# 407-June-2015 Make function for  get Project Visit Logs in Log histroy textbox
''Ahmad Sharif: Task# 407-June-2015 Save Data for Project Visit Log

''Task# D08062015 Ahmad Sharif: Embed a condition if cmbProject is nothing then activate 1st index and do nothing on btnRefresh event, 08-June-2015
''Task# E08062015 Ahmad Sharif: Add Option By Search Gaurd Mobile Number , 08-June-2015
'2015-08-03 Task#20150801 Making Employee Wise Selection according to rights 
'28-Aug-2015 Ahmad Sharif add new columns in grid history(getallrecords)





Imports System.Data.OleDb

Public Class frmProjectVisit
    Implements IGeneral
    Dim ProjectVisitId As Integer = 0I
    Dim IsEditMod As Boolean = False
    Dim IsOpenedForm As Boolean = False
    Dim ViewAll As Boolean = False 'Task#20150801 security rights according to security

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                '    Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                                Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else

                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False

                    For i As Integer = 0 To Rights.Count - 1
                        If Rights.Item(i).FormControlName = "View" Then
                        ElseIf Rights.Item(i).FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Update" Then
                            If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Delete" Then
                            Me.btnDelete.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Print" Then
                            Me.btnPrint.Enabled = True
                        End If
                    Next
                End If
            End If
            If (Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save") AndAlso Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
                Me.btnSave.Visible = True
            Else
                Me.btnDelete.Visible = True
                Me.btnPrint.Visible = True
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnSave.Visible = False
                Else
                    Me.btnSave.Visible = True
                End If
            End If
        Catch ex As Exception
            'msg_Error(ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "VisitType" Then
                FillDropDown(Me.cmbVisitType, "Select * from tblProjectVisitType")
            ElseIf Condition = "Project" Then
                FillUltraDropDown(Me.cmbProject, "Select ProjectCode,ProjectName,ProjectNo,GaurdMbNo from tblprojectportfolio WHERE ProjectName <> '' Order By ProjectName ASC")

                'If cmbProject.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '    For c As Integer = 0 To cmbProject.DisplayLayout.Bands(0).Columns.Count - 1
                '        Me.cmbProject.DisplayLayout.Bands(0).Columns(c).Hidden = True
                '    Next
                'End If
                Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Hidden = False
                Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Hidden = False
                Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectCode").Hidden = True
                Me.cmbProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Hidden = False
                Me.cmbProject.Rows(0).Activate()

            ElseIf Condition = "Stage" Then
                FillDropDown(Me.cmbStage, "Select Distinct Stage, Stage From tblProjectVisit where Stage <> ''", False)
            ElseIf Condition = "Priority" Then
                FillDropDown(Me.cmbPriority, "Select Distinct Priority, Priority From tblProjectVisit where Stage <> ''", False)
            ElseIf Condition = "VisitResult" Then
                FillDropDown(Me.cmbVisitResult, "Select Distinct VisitResult, VisitResult From tblProjectVisit where VisitResult <> ''", False)
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel


    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable

            'Marked Against Task#20150801 Making Employee Wise Selection 
            ''adp = New OleDbDataAdapter("SELECT ProjectVisitId,VisitNo,VisitDate,FollowupDate,VisitTypeId, " _
            '& " ProjectId,TblProjectPortFolio.ProjectNo,TblProjectPortFolio.ProjectName,TblProjectPortFolio.GaurdMbNo ,DirectorName,DirecotrComments,GMName,GMComments,ASMName,ASMComments,ManagerName, " _
            '& " ManagerComments,RPName,RPComments,OthersName,OthersComments,tblProjectVisit.Stage,tblProjectVisit.ProjType,tblProjectVisit.ProjSize,tblProjectVisit.Priority,tblProjectVisit.VisitResult, " _
            '& " ProjectVisitType from tblProjectVisit left outer Join tblProjectVisitType " _
            '& " ON tblProjectVisit.VisitTypeId=tblProjectVisitType.ProjectVisitTypeId LEFT OUTER JOIN TblProjectPortFolio on TblProjectPortFolio.ProjectCode = tblProjectVisit.ProjectId Order By ProjectVisitId DESC", Con)
            'Marked Against Task#20150801 Making Employee Wise Selection 

            'Altered Against Task#20150801 Making Employee Wise Selection 
            Dim cond As String = String.Empty
            cond = IIf(ViewAll = True, "", "Where tblProjectVisit.UserId = " & LoginUserId & "")

            Dim Str As String = "SELECT ProjectVisitId,VisitNo,VisitDate,FollowupDate,VisitTypeId, " _
                        & " tblProjectVisit.ProjectId,TblProjectPortFolio.ProjectNo,TblProjectPortFolio.ProjectName,TblProjectPortFolio.GaurdMbNo ,DirectorName,DirecotrComments,GMName,GMComments,ASMName,ASMComments,ManagerName, " _
                        & " ManagerComments,RPName,RPComments,OthersName,OthersComments,tblProjectVisit.Stage,tblProjectVisit.ProjType,tblProjectVisit.ProjSize,tblProjectVisit.Priority,tblProjectVisit.VisitResult, " _
                        & " ProjectVisitType,tblProjectVisit.userid,TblProjectPortFolio.SiteAddress, TblProjectPortFolio.BlockNo, TblProjectPortFolio.Phase, TblProjectPortFolio.Area, TblProjectPortFolio.City, VLog.Remarks from tblProjectVisit left outer Join tblProjectVisitType " _
                        & " ON tblProjectVisit.VisitTypeId=tblProjectVisitType.ProjectVisitTypeId " _
                        & "LEFT OUTER JOIN TblProjectPortFolio on TblProjectPortFolio.ProjectCode = tblProjectVisit.ProjectId LEFT OUTER JOIN(Select ProjectId, Remarks From tblProjectVisitLog WHERE ProjectVisitLogId In(Select Max(ProjectVisitLogId) From tblProjectVisitLog Group by ProjectId)) VLog on VLog.ProjectId  = tblProjectVisit.ProjectId "

            Str = Str + cond
            Str = Str + " order by ProjectVisitId desc"
            adp = New OleDbDataAdapter(Str, Con)
            'Altered Against Task#20150801 Making Employee Wise Selection 

          adp.Fill(dt)
            dt.AcceptChanges()
            Me.GrdStatus.DataSource = dt
            Me.GrdStatus.RetrieveStructure()
            Me.GrdStatus.RootTable.Columns("ProjectVisitId").Visible = False
            Me.GrdStatus.RootTable.Columns("ProjectId").Visible = False
            Me.GrdStatus.RootTable.Columns("VisitTypeId").Visible = False
            Me.GrdStatus.RootTable.Columns("VisitDate").FormatString = "dd/MMM/yyyy"
            Me.GrdStatus.RootTable.Columns("FollowupDate").FormatString = "dd/MMM/yyyy"
            'Task 20150801 Hidinf UserId Column
            Me.GrdStatus.RootTable.Columns("UserId").Visible = False
            'Task 20150801 Hidinf UserId Column
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GrdStatus.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EditRecord(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdStatus.DoubleClick

        Try
            If Me.GrdStatus.RowCount = 0 Then Exit Sub
            Me.ProjectVisitId = Val(GrdStatus.CurrentRow.Cells("ProjectVisitId").Value.ToString)
            Me.txtVisitNo.Text = GrdStatus.CurrentRow.Cells("VisitNo").Value.ToString
            Me.dtpVisitDate.Value = GrdStatus.CurrentRow.Cells("VisitDate").Value.ToString
            Me.dtpVisitFollowupDate.Value = GrdStatus.CurrentRow.Cells("FollowupDate").Value.ToString
            Me.cmbVisitType.SelectedValue = Val(GrdStatus.CurrentRow.Cells("VisitTypeId").Value.ToString)
            Me.cmbProject.Value = Val(GrdStatus.CurrentRow.Cells("ProjectId").Value.ToString)
            Me.txtDirector.Text = GrdStatus.CurrentRow.Cells("DirectorName").Value.ToString
            Me.txtDirectorComments.Text = GrdStatus.CurrentRow.Cells("DirecotrComments").Value.ToString
            Me.txtGM.Text = GrdStatus.CurrentRow.Cells("GMName").Value.ToString
            Me.txtGMComments.Text = GrdStatus.CurrentRow.Cells("GMComments").Value.ToString
            Me.txtASM.Text = GrdStatus.CurrentRow.Cells("ASMName").Value.ToString
            Me.txtASMComments.Text = GrdStatus.CurrentRow.Cells("ASMComments").Value.ToString
            Me.txtManager.Text = GrdStatus.CurrentRow.Cells("ManagerName").Value.ToString
            Me.txtManagerComments.Text = GrdStatus.CurrentRow.Cells("ManagerComments").Value.ToString
            Me.txtRP.Text = GrdStatus.CurrentRow.Cells("RPName").Value.ToString
            Me.txtRPComments.Text = GrdStatus.CurrentRow.Cells("RPComments").Value.ToString
            Me.txtOthers.Text = GrdStatus.CurrentRow.Cells("OthersName").Value.ToString
            Me.txtOthersComments.Text = GrdStatus.CurrentRow.Cells("OthersComments").Value.ToString
            Me.cmbStage.Text = GrdStatus.CurrentRow.Cells("Stage").Value.ToString
            Me.txtProjectType.Text = GrdStatus.CurrentRow.Cells("ProjType").Value.ToString
            Me.txtProjectSize.Text = GrdStatus.CurrentRow.Cells("ProjSize").Value.ToString
            Me.cmbPriority.Text = GrdStatus.CurrentRow.Cells("Priority").Value.ToString
            Me.cmbVisitResult.Text = GrdStatus.CurrentRow.Cells("VisitResult").Value.ToString
            Me.txtRemarks.Text = GrdStatus.CurrentRow.Cells("Remarks").Value.ToString
            getProjectVisitLogs()          ''Ahmad Sharif: Task# 407-June-2015 call the getProjectVisitLogs functioin for logs
            cmbProject.Enabled = False     ''Ahmad Sharif: Task# 407-June-2015 disable combo box when edit record

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''Ahmad Sharif: Task# 407-June-2015 Make function for  get Project Visit Logs in Log histroy textbox
    Public Sub getProjectVisitLogs()

        Dim projectId As String = Me.cmbProject.Value.ToString()

        Dim strQuery As String = String.Empty
        'Dim dt As DataTable = New DataTable()

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Me.txtLogs.Text = String.Empty    'clears the logs first time enter

        strQuery = "SELECT ProjectId,FollowupDate,IsNull(Remarks,null) as Remarks FROM tblProjectVisitLog WHERE ProjectId =" & projectId

        Dim cmd As New OleDbCommand(strQuery, Con)
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        If reader.HasRows Then
            Do While reader.Read()
                Me.txtLogs.Text = Me.txtLogs.Text.ToString & reader("FollowupDate").ToString & Environment.NewLine & reader("Remarks").ToString & Environment.NewLine & "-----------------------------" & Environment.NewLine   'Get Last Text from textbox and concatenate new text with it and aslo add new line in textbox
            Loop
        Else
            Exit Sub
        End If

    End Sub
    ''end Task: Task# 407-June-2015

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        'Ahmad Sharif : Date should be less or current date
        If dtpVisitDate.Value.Date > Now.Date Then
            ShowErrorMessage("Selected date should be current date or lesser from current date")
            dtpVisitDate.Focus()
            Return False
        End If

        ''Ahmad Sharif : Days between 2 dates should be 7days.
        'Dim daysDiff As Integer = Me.dtpVisitFollowupDate.Value.Subtract(dtpVisitDate.Value).Days

        'If daysDiff > 7 Then
        '    ShowErrorMessage("Difference between Visit date and follow up date should be 7 days.")
        '    dtpVisitFollowupDate.Focus()
        '    Return False
        'End If


        'Ahmad Sharif : Must select project form project combo box
        If cmbVisitType.SelectedIndex = 0 Then
            ShowErrorMessage("Please select the Project Visit Type")
            cmbVisitType.Focus()
            Return False
        End If
        If Me.cmbProject.ActiveRow Is Nothing Then
            ShowErrorMessage("Please select the Project")
            cmbProject.Focus()
            Return False
        End If
        If cmbProject.ActiveRow.Cells(0).Value = 0 Then
            ShowErrorMessage("Please select the Project")
            cmbProject.Focus()
            Return False
        End If

        Return True
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ProjectVisitId = 0I
            Me.btnSave.Text = "&Save"
            FillCombos("Stage")
            FillCombos("Priority")
            FillCombos("VisitResult")
            Me.txtVisitNo.Text = GetNextDocNo("VN", 5, "tblProjectVisit", "VisitNo")
            If Not Me.cmbVisitType.SelectedIndex = -1 Then Me.cmbVisitType.SelectedIndex = 0
            Me.cmbProject.Rows(0).Activate()
            Me.cmbStage.Text = String.Empty
            Me.cmbPriority.Text = String.Empty
            Me.cmbVisitResult.Text = String.Empty
            Me.dtpVisitDate.Value = Date.Now
            Me.dtpVisitFollowupDate.Value = Me.dtpVisitDate.Value.AddDays(7)
            Me.txtDirector.Text = String.Empty
            Me.txtGM.Text = String.Empty
            Me.txtASM.Text = String.Empty
            Me.txtManager.Text = String.Empty
            Me.txtRP.Text = String.Empty
            Me.txtOthers.Text = String.Empty
            Me.txtDirectorComments.Text = String.Empty
            Me.txtGMComments.Text = String.Empty
            Me.txtASMComments.Text = String.Empty
            Me.txtManagerComments.Text = String.Empty
            Me.txtRPComments.Text = String.Empty
            Me.txtOthersComments.Text = String.Empty
            Me.txtProjectType.Text = String.Empty
            Me.txtProjectSize.Text = String.Empty

            Me.txtLogs.Text = String.Empty          'Ahmad Sharif: Task# 407-June-2015 clear txtlog 
            Me.txtRemarks.Text = String.Empty       'Ahmad Sharif: Task# 407-June-2015 clear txtRemarks
            Me.cmbProject.Enabled = True            'Ahmad Sharif: Task# 407-June-2015 disbale cmbProject
            GetSecurityRights()
            GetAllRecords()

            Me.cmbVisitType.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Dim str As String = String.Empty

        Dim cmd As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cmd.Connection = Con

        Try
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'cm.CommandText = "insert into tblProjectVisit(VisitNo,VisitDate,FollowupDate,ProjectId, " _
                '& " DirectorName,DirecotrComments,GMName,GMComments,ASMName,ASMComments,ManagerName,ManagerComments,RPName,RPComments," _
                '& " OthersName,OthersComments,Stage,ProjType,ProjSize,Priority,VisitResult) values( " _
                '& " N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "',N'" & Me.dtpVisitDate.Value.ToString.Replace("'", "''") & "',N'" & Me.dtpVisitFollowupDate.Value.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbProject.SelectedValue.ToString.Replace("'", "''") & "',N'" & Me.txtDirector.Text.ToString.Replace("'", "''") & "',N'" & Me.txtDirectorComments.Text.ToString.Replace("'", "''") & "'," _
                '& "N'" & Me.txtGM.Text.ToString.Replace("'", "''") & "',N'" & Me.txtGMComments.Text.ToString.Replace("'", "''") & "',N'" & Me.txtASM.Text.ToString.Replace("'", "''") & "',N'" & Me.txtASMComments.Text.ToString.Replace("'", "''") & "', " _
                '& "N'" & Me.txtManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtManagerComments.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRP.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtRPComments.Text.ToString.Replace("'", "''") & "',N'" & Me.txtOthers.Text.ToString.Replace("'", "''") & "',N'" & Me.txtOthersComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbStage.SelectedValue.ToString.Replace("'", "''") & "',N'" & Me.txtProjectType.Text.ToString.Replace("'", "''") & "',N'" & Me.txtProjectSize.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.cmbPriority.SelectedValue.ToString.Replace("'", "''") & "',N'" & Me.cmbVisitResult.SelectedValue.ToString.Replace("'", "''") & "')"
                'Marked Agaisnt Task20150801 to Save UserId
                'cmd.CommandText = "insert into tblProjectVisit(VisitNo,VisitDate,FollowupDate,VisitTypeId,ProjectId, " _
                '& " DirectorName,DirecotrComments,GMName,GMComments,ASMName,ASMComments,ManagerName,ManagerComments,RPName,RPComments," _
                '& " OthersName,OthersComments,Stage,ProjType,ProjSize,Priority,VisitResult) values( " _
                '& " N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.dtpVisitDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & Me.dtpVisitFollowupDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & Me.cmbVisitType.SelectedValue.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtDirector.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtDirectorComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtGM.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtGMComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtASM.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtASMComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtManager.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtManagerComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtRP.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtRPComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtOthers.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtOthersComments.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbStage.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtProjectType.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtProjectSize.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbPriority.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbVisitResult.Text.ToString.Replace("'", "''") & "')"
                'Marked Agaisnt Task20150801 to Save UserId
                'Altered Agaisnt Task20150801 to Save UserId
                cmd.CommandText = "insert into tblProjectVisit(VisitNo,VisitDate,FollowupDate,VisitTypeId,ProjectId, " _
                & " DirectorName,DirecotrComments,GMName,GMComments,ASMName,ASMComments,ManagerName,ManagerComments,RPName,RPComments," _
                & " OthersName,OthersComments,Stage,ProjType,ProjSize,Priority,VisitResult,userid) values( " _
                & " N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.dtpVisitDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " N'" & Me.dtpVisitFollowupDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " N'" & Me.cmbVisitType.SelectedValue.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtDirector.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtDirectorComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtGM.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtGMComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtASM.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtASMComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtManager.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtManagerComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtRP.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtRPComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtOthers.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtOthersComments.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbStage.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtProjectType.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtProjectSize.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbPriority.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbVisitResult.Text.ToString.Replace("'", "''") & "', " & LoginUserId & ") "

                'Altered Agaisnt Task20150801 to Save UserId

            Else
                'Marked Agaisnt Task20150801 to Update UserId
                cmd.CommandText = "UPDATE tblProjectVisit SET " _
                & " VisitNo=N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                & " VisitDate=N'" & Me.dtpVisitDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " FollowupDate=N'" & Me.dtpVisitFollowupDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " VisitTypeId=N'" & Me.cmbVisitType.SelectedValue.ToString.Replace("'", "''") & "'," _
                & " ProjectId=N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                & " DirectorName=N'" & Me.txtDirector.Text.ToString.Replace("'", "''") & "'," _
                & " DirecotrComments=N'" & Me.txtDirectorComments.Text.ToString.Replace("'", "''") & "'," _
                & " GMName=N'" & Me.txtGM.Text.ToString.Replace("'", "''") & "'," _
                & " GMComments=N'" & Me.txtGMComments.Text.ToString.Replace("'", "''") & "'," _
                & " ASMName=N'" & Me.txtASM.Text.ToString.Replace("'", "''") & "'," _
                & " ASMComments=N'" & Me.txtASMComments.Text.ToString.Replace("'", "''") & "'," _
                & " ManagerName=N'" & Me.txtManager.Text.ToString.Replace("'", "''") & "'," _
                & " ManagerComments=N'" & Me.txtManagerComments.Text.ToString.Replace("'", "''") & "'," _
                & " RPName=N'" & Me.txtRP.Text.ToString.Replace("'", "''") & "'," _
                & " RPComments=N'" & Me.txtRPComments.Text.ToString.Replace("'", "''") & "'," _
                & " OthersName=N'" & Me.txtOthers.Text.ToString.Replace("'", "''") & "'," _
                & " OthersComments=N'" & Me.txtOthersComments.Text.ToString.Replace("'", "''") & "'," _
                & " Stage=N'" & Me.cmbStage.Text.ToString.Replace("'", "''") & "'," _
                & " ProjType=N'" & Me.txtProjectType.Text.ToString.Replace("'", "''") & "'," _
                & " ProjSize=N'" & Me.txtProjectSize.Text.ToString.Replace("'", "''") & "'," _
                & " Priority=N'" & Me.cmbPriority.Text.ToString.Replace("'", "''") & "'," _
                & " VisitResult=N'" & Me.cmbVisitResult.Text.ToString.Replace("'", "''") & "'  Where ProjectVisitId=" & Val(Me.GrdStatus.CurrentRow.Cells("ProjectVisitId").Value.ToString)
                'Marked Agaisnt Task20150801 to Update UserId

                'Altered Agaisnt Task20150801 to Update UserId
                cmd.CommandText = "UPDATE tblProjectVisit SET " _
                & " VisitNo=N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                & " VisitDate=N'" & Me.dtpVisitDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " FollowupDate=N'" & Me.dtpVisitFollowupDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " VisitTypeId=N'" & Me.cmbVisitType.SelectedValue.ToString.Replace("'", "''") & "'," _
                & " ProjectId=N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                & " DirectorName=N'" & Me.txtDirector.Text.ToString.Replace("'", "''") & "'," _
                & " DirecotrComments=N'" & Me.txtDirectorComments.Text.ToString.Replace("'", "''") & "'," _
                & " GMName=N'" & Me.txtGM.Text.ToString.Replace("'", "''") & "'," _
                & " GMComments=N'" & Me.txtGMComments.Text.ToString.Replace("'", "''") & "'," _
                & " ASMName=N'" & Me.txtASM.Text.ToString.Replace("'", "''") & "'," _
                & " ASMComments=N'" & Me.txtASMComments.Text.ToString.Replace("'", "''") & "'," _
                & " ManagerName=N'" & Me.txtManager.Text.ToString.Replace("'", "''") & "'," _
                & " ManagerComments=N'" & Me.txtManagerComments.Text.ToString.Replace("'", "''") & "'," _
                & " RPName=N'" & Me.txtRP.Text.ToString.Replace("'", "''") & "'," _
                & " RPComments=N'" & Me.txtRPComments.Text.ToString.Replace("'", "''") & "'," _
                & " OthersName=N'" & Me.txtOthers.Text.ToString.Replace("'", "''") & "'," _
                & " OthersComments=N'" & Me.txtOthersComments.Text.ToString.Replace("'", "''") & "'," _
                & " Stage=N'" & Me.cmbStage.Text.ToString.Replace("'", "''") & "'," _
                & " ProjType=N'" & Me.txtProjectType.Text.ToString.Replace("'", "''") & "'," _
                & " ProjSize=N'" & Me.txtProjectSize.Text.ToString.Replace("'", "''") & "'," _
                & " Priority=N'" & Me.cmbPriority.Text.ToString.Replace("'", "''") & "'," _
                & " VisitResult=N'" & Me.cmbVisitResult.Text.ToString.Replace("'", "''") & "' , userid = " & LoginUserId & " Where ProjectVisitId=" & Val(Me.GrdStatus.CurrentRow.Cells("ProjectVisitId").Value.ToString)
                'Altered Agaisnt Task20150801 to Update UserId
            End If

            Dim rs As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            'If Con.State = ConnectionState.Closed Then Con.Open()
            'cmd.Connection = Con
            'cmd.CommandText = String.Empty
            'If Me.cmbProject.ActiveRow.Cells(0).Value > 0 Then
            '    cmd.CommandText = "Update TblProjectPortFolio Set ComDirector='" & Me.txtDirector.Text.Replace("'", "''") & "', ComGM='" & Me.txtGM.Text.Replace("'", "''") & "', ComASM='" & Me.txtASM.Text.Replace("'", "''") & "', ComManager='" & Me.txtManager.Text.Replace("'", "''") & "', ComSE='" & Me.txtRP.Text.Replace("'", "''") & "', ComTS='" & Me.txtOthers.Text.Replace("'", "''") & "' WHERE ProjectCode=" & Me.cmbProject.Value & ""
            '    cmd.ExecuteNonQuery()
            'End If
            If Me.btnSave.Text = "&Update" Or Me.btnSave.Text = "Update" Then
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.CommandText = String.Empty
                cmd.CommandText = "INSERT INTO tblProjectVisitLog(ProjectVisitId,ProjectId,FollowupDate,Remarks) VALUES( " _
                                & " N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                                & " N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                                & " N'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "','Update')"

                Dim result As Integer = Convert.ToInt32(cmd.ExecuteNonQuery())
                'end Task: Task# 407-June-2015
            End If
            If Me.txtRemarks.Text.ToUpper <> Me.GrdStatus.GetRow.Cells("Remarks").Value.ToString.ToUpper Then
                If Me.txtRemarks.TextLength > 0 Then
                    'Ahmad Sharif: Task# 407-June-2015 Save Data for Project Visit Log
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "INSERT INTO tblProjectVisitLog(ProjectVisitId,ProjectId,FollowupDate,Remarks) VALUES( " _
                                    & " N'" & Me.txtVisitNo.Text.ToString.Replace("'", "''") & "'," _
                                    & " N'" & Me.cmbProject.Value.ToString.Replace("'", "''") & "'," _
                                    & " N'" & Me.dtpVisitFollowupDate.Value.ToString.Replace("'", "''") & "'," _
                                    & " N'" & Me.txtRemarks.Text.ToString.Replace("'", "''") & "')"
                    Dim result As Integer = Convert.ToInt32(cmd.ExecuteNonQuery())
                    'end Task: Task# 407-June-2015
                End If
            End If
            ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnRefresh.Visible = False
                Me.btnLoadAll.Visible = True
                Me.btnProjectFolio.Visible = False
            Else
                Me.btnRefresh.Visible = True
                Me.btnLoadAll.Visible = False
                Me.btnProjectFolio.Visible = True
            End If
            ApplySecurity(SBUtility.Utility.EnumDataMode.ReadOnly)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectVisit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                'btnPrint_Click(Nothing, Nothing)
                'Exit Sub
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    If Me.GrdStatus.RowCount > 0 Then
                        Me.CutToolStripButton_Click(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectVisit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub frmProjectVisit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.KeyPreview = True
            FillCombos("VisitType")
            FillCombos("Project")
            IsOpenedForm = True
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub cmbProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If IsOpenedForm = True Then
    '            GetFillProjectData()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub GetFillProjectData()
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select  * from tblprojectportfolio WHERE ProjectCode=" & Me.cmbProject.Value & "")
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                Me.txtDirector.Text = dt.Rows(0).Item("ComDirector").ToString
                Me.txtGM.Text = dt.Rows(0).Item("ComGM").ToString
                Me.txtASM.Text = dt.Rows(0).Item("ComASM").ToString
                Me.txtManager.Text = dt.Rows(0).Item("ComManager").ToString
                Me.txtRP.Text = dt.Rows(0).Item("ComSE").ToString
                'Me.txtOthers.Text = dt.Rows(0).Item("ComTS").ToString
                Me.txtProjectSize.Text = dt.Rows(0).Item("ProjSize").ToString
                Me.txtProjectType.Text = dt.Rows(0).Item("ProjType").ToString
            Else

                Me.txtDirector.Text = String.Empty
                Me.txtGM.Text = String.Empty
                Me.txtASM.Text = String.Empty
                Me.txtManager.Text = String.Empty
                Me.txtRP.Text = String.Empty
                'Me.txtOthers.Text = String.Empty
                Me.txtProjectSize.Text = String.Empty
                Me.txtProjectType.Text = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.cmbVisitType.SelectedIndex
            FillCombos("VisitType")
            Me.cmbVisitType.SelectedIndex = id


            ''Task# D08062015 Ahmad Sharif: Embed a condition if cmbProject is nothing then activate 1st index and do nothing on btnRefresh event
            If Me.cmbProject.Text = String.Empty Then
                Me.cmbProject.Rows(0).Activate()
                Exit Sub
            Else
                ''End Task# D08062015
                id = Me.cmbProject.ActiveRow.Cells(0).Value
                FillCombos("Project")
                Me.cmbProject.Value = id
            End If

            GetAllRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtASM.TextChanged

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                Save()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If Not GrdStatus.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        If msg_Confirm(str_ConfirmDelete) = True Then

            Try
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                'Application.DoEvents()

                Dim cm As New OleDbCommand

                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con
                cm.CommandText = "delete from tblProjectVisit where ProjectVisitId=" & Me.GrdStatus.CurrentRow.Cells("ProjectVisitId").Value.ToString
                cm.ExecuteNonQuery()
                Con.Close()

                Me.lblProgress.Visible = False

                'GetAllRecords()
                ReSetControls()

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmbProject_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProject.Leave
        Try
            If IsOpenedForm = True Then
                'Typed letters in cmbProject, search in combo box list
                If cmbProject.IsItemInList = False Then
                    Exit Sub
                Else
                    getProjectVisitLogs()   ''Ahmad Sharif: Task# 407-June-2015 calling getProjectVisitLogs function for visit logs
                End If
                GetFillProjectData()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Ahmad Sharif : Click on cmbProject combo box , combo box  will be blank
    Private Sub cmbProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProject.Click
        Try



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByName.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByName.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
                ''Task# E08062015 Ahmad Sharif : Add new condition for search by GaurdMbNo
            ElseIf rbtByCode.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            Else
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            End If
            ''End 'Task# E08062015
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtByCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtByCode.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByCode.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
                ''Task# E08062015 Ahmad Sharif : Add new condition for search by GaurdMbNo
            ElseIf rbtByName.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
            Else
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            End If
            ''End 'Task# E08062015
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdStatus.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Project Visit History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnProjectFolio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjectFolio.Click
        Try
            frmMain.LoadControl("ProjectPortfolio")
            frmProjectPortFolio.BindGrid()
            frmProjectPortFolio.Tag = Me.cmbProject.Value
            frmProjectPortFolio.EditByProjectVisit(frmProjectPortFolio.Tag)
            frmMain.LoadControl("ProjectPortfolio")
            frmProjectPortFolio.Tag = String.Empty

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpVisitDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpVisitDate.ValueChanged
        Try
            Me.dtpVisitFollowupDate.Value = Me.dtpVisitDate.Value.AddDays(7)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpVisitFollowupDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpVisitFollowupDate.ValueChanged
        Try
            If Me.dtpVisitFollowupDate.Value < Me.dtpVisitDate.Value Then
                ShowErrorMessage("Follow up date is not valid.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''Task# E08062015 Ahmad Sharif : Add Radio Btn checked event for search b GaurdMbNo
    Private Sub rbtByGaurd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByGaurd.CheckedChanged

        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If rbtByGaurd.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            ElseIf rbtByCode.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            Else
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''End Task# E08062015

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                'Altered Against Task#20150801 Making Employee Wise Selection 
                ViewAll = True
                'Altered Against Task#20150801 Making Employee Wise Selection 
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'Altered Against Task#20150801 Making Employee Wise Selection 
                    ElseIf Rights.Item(i).FormControlName = "View All" Then
                        ViewAll = True
                        'Altered Against Task#20150801 Making Employee Wise Selection 
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class