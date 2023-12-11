'Ahmad Sharif : Design and code of ProjectQuotion
'Ahmad Sharif : Added Particulars field in desing and save update, Modification on 05-06-2015
'Task# F08062015 Ahmad Sharif : 08-June-2015
'2015-08-03 Task#20150801 Making Employee Wise Selection according to rights 
Imports System.Data.OleDb

Public Class frmProjQuotion
    Implements IGeneral
    Dim IsOpenedForm As Boolean = False
    Dim ViewAll As Boolean = False 'Task#20150801 security check

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
                'Altered Against Task#20150801 Making Employee Wise Selection 
                ViewAll = True
                'Altered Against Task#20150801 Making Employee Wise Selection 

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
                            'Altered Against Task#20150801 Making Employee Wise Selection 
                        ElseIf Rights.Item(i).FormControlName = "View All" Then
                            ViewAll = True
                            'Altered Against Task#20150801 Making Employee Wise Selection 

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
            If Condition = "Project" Then
                ''Task# F08062015 Ahmad Sharif : add GaurdMbNo column in select query
                FillUltraDropDown(Me.cmboxProject, "select ProjectCode,ProjectName,ProjectNo,GaurdMbNo from TblProjectPortFolio Order By ProjectName ASC")
                Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectCode").Hidden = True
                Me.cmboxProject.Rows(0).Activate()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel


    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            ''Task# F08062015 Ahmad Sharif : Add GaurdMbNo column in select query also order by desc order

            'Marked Against Task#20150801 to add User Column in query
            'dt = GetDataTable("SELECT ProjQuotID,ProjectId,QuotationNo,Particulars,QuotationDate,QuotationAmount,QuotationStatus,ProjectNo, ProjectName,GaurdMbNo FROM tblProjectQuotation INNER JOIN tblProjectPortFolio a on a.ProjectCode = tblProjectQuotation.ProjectId order by ProjQuotID desc")
            'Marked Against Task#20150801 to add User Column in query
            'Altered Against Task#20150801 to add User Column in query
            Dim strQuery As String = String.Empty
            strQuery = "SELECT ProjQuotID,ProjectId,QuotationNo,Particulars,QuotationDate,QuotationAmount,QuotationStatus,ProjectNo, ProjectName,GaurdMbNo,tblProjectQuotation.userid FROM tblProjectQuotation INNER JOIN tblProjectPortFolio a on a.ProjectCode = tblProjectQuotation.ProjectId"
            Dim cond As String = String.Empty
            cond = IIf(ViewAll = True, "", " Where tblProjectQuotation.UserId = " & LoginUserId & "")
            strQuery = strQuery + cond
            strQuery = strQuery + " order by ProjQuotID desc"
            dt = GetDataTable(strQuery)
            'Altered Against Task#20150801 to add User Column in query
            dt.AcceptChanges()
            Me.GrdStatus.DataSource = dt
            Me.GrdStatus.RetrieveStructure()
            Me.GrdStatus.RootTable.Columns("ProjQuotID").Visible = False
            Me.GrdStatus.RootTable.Columns("ProjectId").Visible = False
            Me.GrdStatus.RootTable.Columns("Particulars").Visible = True  'Ahmad Sharif : added Particulars , modification on 05-06-2015
            Me.GrdStatus.RootTable.Columns("QuotationNo").Visible = True
            Me.GrdStatus.RootTable.Columns("QuotationDate").FormatString = "dd/MMM/yyyy"
            Me.GrdStatus.RootTable.Columns("QuotationAmount").Visible = True
            Me.GrdStatus.RootTable.Columns("QuotationStatus").Visible = True
            Me.GrdStatus.RootTable.Columns("GaurdMbNo").Visible = True      ''Task# F08062015 Ahmad Sharif : add GaurdMbNo to GrdStatus
            Me.GrdStatus.RootTable.Columns("UserId").Visible = False      ''Task# 20150801 Ali Ansari : Hide User Id

        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub


    Sub GrdStatus_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdStatus.DoubleClick
        Try
            If GrdStatus.Row < 0 Then
                Exit Sub
            Else
                Me.cmboxProject.Value = GrdStatus.CurrentRow.Cells("ProjectId").Value.ToString
                Me.txtQuotationNo.Text = GrdStatus.CurrentRow.Cells("QuotationNo").Value.ToString
                Me.txtParticulars.Text = GrdStatus.CurrentRow.Cells("Particulars").Value.ToString  'Ahmad Sharif : added particular of edit, modification on 05-06-2015
                Me.dtpQuotationDate.Value = GrdStatus.CurrentRow.Cells("QuotationDate").Value.ToString
                Me.txtAmount.Text = GrdStatus.CurrentRow.Cells("QuotationAmount").Value.ToString
                Me.cmbStatus.Text = GrdStatus.CurrentRow.Cells("QuotationStatus").Value.ToString
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                btnSave.Text = "Update"
                ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        If Not Me.cmboxProject.IsItemInList Then
            ShowErrorMessage("Please select project.")
            Me.cmboxProject.Focus()
            Return False
        End If

        If Me.cmboxProject.Text = String.Empty Then
            ShowErrorMessage("Please select project.")
            Me.cmboxProject.Focus()
            Return False
        End If

        If Me.cmboxProject.ActiveRow Is Nothing Then
            ShowErrorMessage("Please select project.")
            Me.cmboxProject.Focus()
            Return False
        End If

        If txtQuotationNo.Text = String.Empty Then
            ShowErrorMessage("Please enter the Quotation no")
            txtQuotationNo.Focus()
            Return False
        End If

        If Val(txtAmount.Text) = 0 Then
            ShowErrorMessage("Please enter the Quotation amount")
            txtAmount.Focus()
            Return False
        End If

        If cmbStatus.SelectedIndex = -1 Then
            ShowErrorMessage("Please select the quotation status")
            cmbStatus.Focus()
            Return False
        End If

        Return True
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"

            rbtByCode.Checked = True
            Me.cmboxProject.Rows(0).Activate()
            Me.txtQuotationNo.Text = String.Empty
            Me.txtParticulars.Text = String.Empty   'Ahmad Sharif: added particulars for reset, modification on 05-05-2015
            Me.txtAmount.Text = String.Empty
            Me.cmbStatus.SelectedIndex = 0
            ViewAll = False
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            GetAllRecords()
            'ApplySecurity(SBUtility.Utility.EnumDataMode.[New])

            Me.cmboxProject.Focus()
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
            'If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "&Save" Then
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                ''Marked Against Task#20150801 to add User Column in Table
                ''Ahmad Sharif : Added Particulars column in save query, Modification on 05-06-2015
                'cmd.CommandText = "insert into tblProjectQuotation(ProjectId,QuotationNo,Particulars,QuotationDate,QuotationAmount,QuotationStatus) values( " _
                '& " N'" & Me.cmboxProject.Value.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtQuotationNo.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtParticulars.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.dtpQuotationDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & Me.txtAmount.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbStatus.Text.ToString.Replace("'", "''") & "')"
                ''Marked Against Task#20150801 to add User Column in Table

                'Altered Against Task#20150801 to add User Column in Table
                cmd.CommandText = "insert into tblProjectQuotation(ProjectId,QuotationNo,Particulars,QuotationDate,QuotationAmount,QuotationStatus,userid) values( " _
                & " N'" & Me.cmboxProject.Value.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtQuotationNo.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtParticulars.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.dtpQuotationDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                & " N'" & Me.txtAmount.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbStatus.Text.ToString.Replace("'", "''") & "'," & LoginUserId & ")"
                'Altered Against Task#20150801 to add User Column in Table
            Else
                ''Marked Against Task#20150801 to Update User Column in Table
                ''Ahmad Sharif : Added Particulars column in update query, Modification on 05-06-2015
                'cmd.CommandText = "UPDATE tblProjectQuotation SET " _
                '   & " ProjectId=N'" & Me.cmboxProject.Value.ToString.Replace("'", "''") & "'," _
                '   & " QuotationNo=N'" & Me.txtQuotationNo.Text.ToString.Replace("'", "''") & "'," _
                '   & " Particulars=N'" & Me.txtParticulars.Text.ToString.Replace("'", "''") & "'," _
                '   & " QuotationDate=N'" & Me.dtpQuotationDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '   & " QuotationAmount=N'" & Me.txtAmount.Text.ToString.Replace("'", "''") & "'," _
                '   & " QuotationStatus=N'" & Me.cmbStatus.Text.ToString.Replace("'", "''") & "' Where ProjQuotID=" & Val(Me.GrdStatus.CurrentRow.Cells("ProjQuotID").Value.ToString)
                ''Marked Against Task#20150801 to Update User Column in Table

                'Altered Against Task#20150801 to Update User Column in Table
                cmd.CommandText = "UPDATE tblProjectQuotation SET " _
                   & " ProjectId=N'" & Me.cmboxProject.Value.ToString.Replace("'", "''") & "'," _
                   & " QuotationNo=N'" & Me.txtQuotationNo.Text.ToString.Replace("'", "''") & "'," _
                   & " Particulars=N'" & Me.txtParticulars.Text.ToString.Replace("'", "''") & "'," _
                   & " QuotationDate=N'" & Me.dtpQuotationDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                   & " QuotationAmount=N'" & Me.txtAmount.Text.ToString.Replace("'", "''") & "'," _
                   & " QuotationStatus=N'" & Me.cmbStatus.Text.ToString.Replace("'", "''") & "' ,userid = " & LoginUserId & " Where ProjQuotID=" & Val(Me.GrdStatus.CurrentRow.Cells("ProjQuotID").Value.ToString)
                'Altered Against Task#20150801 to Update User Column in Table
            End If

            Dim rs As Integer = Convert.ToInt32(cmd.ExecuteScalar())


            'If Me.cmboxProject.ActiveRow.Cells(0).Value > 0 Then
            '    If Val(Me.txtAmount.Text) > 0 Then

            '        If Con.State = ConnectionState.Closed Then Con.Open()
            '        cmd.Connection = Con
            '        cmd.CommandText = "Update TblProjectPortFolio Set AllQutationValue=a.QuotationAmount From TblProjectPortFolio, (Select ProjectId, SUM(IsNull(QuotationAmount,0)) as QuotationAmount From tblProjectQuotation WHERE ProjectId=" & Me.cmboxProject.Value & "  AND QuotationStatus='Pending' Group By ProjectId ) a WHERE A.ProjectId = TblProjectPortFolio.Projectcode "
            '        cmd.ExecuteNonQuery()

            '    End If
            'End If

            ReSetControls()
            'GetAllRecords()

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
            '    If e.Tab.Index = 1 Then
            '        Me.btnRefresh.Visible = False
            '        Me.btnLoadAll.Visible = True
            '        Me.btnProjectFolio.Visible = False
            '    Else
            '        Me.btnRefresh.Visible = True
            '        Me.btnLoadAll.Visible = False
            '        Me.btnProjectFolio.Visible = True
            '    End If
            ApplySecurity(SBUtility.Utility.EnumDataMode.ReadOnly)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjQuotion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                        Me.btnDelete_Click(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectVisit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.KeyPreview = True
            '    FillCombos("VisitType")
            FillCombos("Project")
            IsOpenedForm = True
            ReSetControls()
            'GetAllRecords()
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
        'Try


        '    Dim dt As New DataTable
        '    dt = GetDataTable("Select  * from tblprojectportfolio WHERE ProjectCode=" & Me.cmbProject.Value & "")
        '    dt.AcceptChanges()

        '    If dt.Rows.Count > 0 Then
        '        Me.txtDirector.Text = dt.Rows(0).Item("ComDirector").ToString
        '        Me.txtGM.Text = dt.Rows(0).Item("ComGM").ToString
        '        Me.txtASM.Text = dt.Rows(0).Item("ComASM").ToString
        '        Me.txtManager.Text = dt.Rows(0).Item("ComManager").ToString
        '        Me.txtRP.Text = dt.Rows(0).Item("ComSE").ToString
        '        Me.txtOthers.Text = dt.Rows(0).Item("ComTS").ToString
        '        Me.txtProjectSize.Text = dt.Rows(0).Item("ProjSize").ToString
        '        Me.txtProjectType.Text = dt.Rows(0).Item("ProjType").ToString
        '    Else

        '        Me.txtDirector.Text = String.Empty
        '        Me.txtGM.Text = String.Empty
        '        Me.txtASM.Text = String.Empty
        '        Me.txtManager.Text = String.Empty
        '        Me.txtRP.Text = String.Empty
        '        Me.txtOthers.Text = String.Empty
        '        Me.txtProjectSize.Text = String.Empty
        '        Me.txtProjectType.Text = String.Empty
        '    End If


        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I

            ''Task# E08062015 Ahmad Sharif: Embed a condition if cmbProject is nothing then activate 1st index and do nothing on btnRefresh event
            If Me.cmboxProject.Text = String.Empty Then
                Me.cmboxProject.Rows(0).Activate()
                Exit Sub
            Else
                ''End Task# E08062015
                id = Me.cmboxProject.ActiveRow.Cells(0).Value
                FillCombos("Project")
                Me.cmboxProject.Value = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtASM.TextChanged

    'End Sub

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
    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByName.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByName.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
                ''Task# F08062015 Ahmad Sharif: Change and add new condition for Guard Mb NO
            ElseIf rbtByCode.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            Else
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            End If
            ''End Task# F08062015 
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtByCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtByCode.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByCode.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
                ''Task# F08062015 Ahmad Sharif: Change and add new condition for Guard Mb NO
            ElseIf rbtByName.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
            Else
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            End If
            ''End Task# F08062015
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
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
                cm.CommandText = "delete from tblProjectQuotation where ProjQuotID=" & Me.GrdStatus.CurrentRow.Cells("ProjQuotID").Value.ToString
                cm.ExecuteNonQuery()
                Con.Close()

                Me.lblProgress.Visible = False

                GetAllRecords()
                'ReSetControls()

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)
            End Try
        End If
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    ': Ahmad Sharif : Added KeyPress event for digit only
    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try

            'Dim allowedChars As String = "0123456789"
            'If e.KeyChar <> ControlChars.Back Then
            '    If allowedChars.IndexOf(e.KeyChar) = -1 Then
            '        ' Invalid Character
            '        e.Handled = True
            '    End If
            'End If.
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task# F08062015 Ahmad Sharif: Add  Radio btn checked event for Guard Mb NO 
    Private Sub rbtByGuard_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByGuard.CheckedChanged
        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If Me.rbtByGuard.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("GaurdMbNo").Key.ToString
            ElseIf Me.rbtByCode.Checked = True Then
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            Else
                Me.cmboxProject.DisplayMember = Me.cmboxProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
            End If

        Catch ex As Exception

        End Try
    End Sub
    ''Task# F08062015
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            GrdStatus_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class


