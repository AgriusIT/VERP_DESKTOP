''Ahmad Sharif: Missing controls for reset form in RefreshForm(), added on 05-June-2015
''Ahmad Sharif: add Drop downs on 05-June-2015
''Ahmad Sharif: Task# 507-June-2015, Check if Project Visit or Quotation exist against project then don't delete project 
''Ahmad Sharif: Task# 607-June-2015 if find the project id in grdHistory then allow to double click otherwise no
''Task# A08062015: Ahmad Sharif: Email Replace Another Email
''Task# B08062015 Ahmad Sharif: Owner Mobile NO saved in Main Consultant Mobile No
''Task# C08062015 Ahmad Sharif : Add Order by in query for displaying record in the grid in descending order
''Task# A136151 Imran Ali Delete Button Visible on Save Mode
'2015-08-03 Task#20150801 Making Employee Wise Selection according to rights 

Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Public Class frmProjectPortFolio
    Public CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Dim IsOpenedForm As Boolean = False
    Dim ViewAll As Boolean = False 'Task#20150801 security check
    Sub RefreshForm()
        Try
            Me.btnSave.Text = "&Save"
            Me.CurrentId = 0
            Me.txtProjectCode.Text = GetNextDocNo("AF", 5, "TblProjectPortFolio", "ProjectNo")   'Ahmad Sharif: Set Prefix to AF, on 06-06-2015
            Me.txtProjectName.Text = String.Empty
            Me.txtSiteAddress.Text = String.Empty
            Me.txtBlockNo.Text = String.Empty
            Me.txtPhase.Text = String.Empty
            Me.txtArea.Text = String.Empty
            Me.txtCity.Text = String.Empty
            Me.txtCustName.Text = String.Empty
            Me.txtCustOffAdd.Text = String.Empty
            Me.txtCustMob.Text = String.Empty
            Me.txtCustEmail.Text = String.Empty
            Me.txtRepName.Text = String.Empty
            Me.txtRepMbNo.Text = String.Empty
            Me.txtRepEmail.Text = String.Empty
            Me.txtGaurdName.Text = String.Empty
            Me.txtGaurdMbNo.Text = String.Empty
            Me.txtGaurdEmail.Text = String.Empty
            'Me.txtComDirector.Text = String.Empty
            'Me.txtComGM.Text = String.Empty
            'Me.txtComASM.Text = String.Empty
            'Me.txtComManager.Text = String.Empty
            'Me.txtComSE.Text = String.Empty
            'Me.txtComTS.Text = String.Empty
            'Me.txtComSS.Text = String.Empty

            'Ahmad Sharif:  add combo boxes for reset, modification on 05-June-2015
            FillCombo("Director")
            Me.cmbComDirector.Text = String.Empty
            FillCombo("GM")
            Me.cmbGManager.Text = String.Empty
            FillCombo("ASM")
            Me.cmbASM.Text = String.Empty
            FillCombo("Manager")
            Me.cmbManager.Text = String.Empty
            FillCombo("ResPerson")
            Me.cmbResPerson.Text = String.Empty
            FillCombo("TechSupervisor")
            Me.cmbTechSupervisor.Text = String.Empty
            FillCombo("SiteSupervisor")
            Me.cmbSiteSupervisor.Text = String.Empty
            'Ahmad Sharif: end

            Me.cmbProjType.Text = String.Empty
            Me.cmbProjSize.Text = String.Empty
            Me.txtBulName.Text = String.Empty
            Me.txtBulAdd.Text = String.Empty
            Me.txtBulPhNo.Text = String.Empty
            Me.txtBulOwner.Text = String.Empty
            Me.txtBulMbNo.Text = String.Empty
            Me.txtArchEmail.Text = String.Empty
            Me.txtBulEmail.Text = String.Empty
            Me.txtBulGManager.Text = String.Empty
            Me.txtBulGMMbNo.Text = String.Empty
            Me.txtBulGMEmail.Text = String.Empty
            Me.txtBulPManager.Text = String.Empty
            Me.txtBulPMMbNo.Text = String.Empty
            Me.txtBulPMEmail.Text = String.Empty
            Me.txtArcName.Text = String.Empty
            Me.txtArcAdd.Text = String.Empty
            Me.txtArcPhNo.Text = String.Empty
            Me.txtArcOwner.Text = String.Empty
            Me.txtArcMBNo.Text = String.Empty
            Me.txtArcGMEmail.Text = String.Empty
            Me.txtArcGManager.Text = String.Empty
            Me.txtArcGMMbNo.Text = String.Empty
            Me.txtArcGMEmail.Text = String.Empty
            Me.txtArcPManager.Text = String.Empty
            Me.txtArcPMMbNo.Text = String.Empty
            Me.txtArcPMEmail.Text = String.Empty

            'Ahmad Sharif: Missing controls for reset form in RefreshForm(), added on 05-June-2015
            Me.txtConName.Text = String.Empty
            Me.txtConAdd.Text = String.Empty
            Me.txtConPhNo.Text = String.Empty
            Me.txtConOwner.Text = String.Empty
            Me.txtConMBNo.Text = String.Empty
            Me.txtConEmail.Text = String.Empty
            Me.txtConMainName.Text = String.Empty
            Me.txtConMainMbNo.Text = String.Empty
            Me.txtConMAINEmail.Text = String.Empty
            Me.txtConConName.Text = String.Empty
            Me.txtConConMbNo.Text = String.Empty
            Me.txtConConEmail.Text = String.Empty
            'Ahmad Sharif: end

            Me.dtpProjDate.Value = Date.Now
            FillCombo("ProjType")
            FillCombo("ProjSize")
            Me.cmbProjType.Text = String.Empty
            Me.cmbProjSize.Text = String.Empty
            Me.txtAllQuotationValue.Text = String.Empty
            Me.txtProjectEstValue.Text = String.Empty

            Me.GetSecurityRights()
            Me.BindGrid()
            Me.MainTab.SelectedTab = Me.MainTab.TabPages(0)
            Me.DetailTab.SelectedTab = Me.DetailTab.TabPages(0)
            Me.txtProjectName.Focus()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub BindGrid()
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            'adp = New OleDbDataAdapter("select ProjectCode,ProjectNo,ProjectDate,ProjectName,SiteAddress,BlockNo," _
            '& "Phase,Area,City,CustName, " _
            '& "CustOffAdd,CustMob,CustEmail,RepName, " _
            '& "RepMbNo,RepEmail,GaurdName,GaurdMbNo, " _
            '& "GaurdEmail,ComDirector,ComGM,ComASM," _
            '& "ComManager,ComSE,ComTS,ComSS," _
            '& "ProjType,ProjSize,BulName,BulAdd," _
            '& "BulPhNo,BulOwner,BulMbNo,BulEmail, " _
            '& "BulGManager,BulGMMbNo,BulGMEmail,BulPManager, " _
            '& "BulPMMbNo,BulPMEmail,ArcName,ArcAdd," _
            '& "ArcPhNo,ArcOwner,ArcMBNo,ArchEmail," _
            '& "ArcGManager,ArcGMMbNo,ArcGMEmail,ArcPManager," _
            '& "ArcPMMbNo,ArcPMEmail,ConName,ConAdd," _
            '& "ConPhNo,ConOwner,ConMBNo,ConEmail," _
            '& "ConMainName,ConMainMbNo,ConMAINEmail,ConConName, " _
            '& "ConConMbNo,ConConEmail, IsNull(EstimatedQutationValue,0) as EstimatedQutationValue, IsNull(AllQutationValue,0) as AllQutationValue from tblprojectportfolio WHERE  ProjectCode <> 0  order by ProjectCode DESC", Con)

            'Ahmad Sharif: Update Query, get join two tables, updated on 06-06-2015
            ''Task# C08062015 Ahmad Sharif : Add Order by in query for displaying record in the grid in descending order
            'Marked Against Task#20150801 Add User Id 
            'Dim strQuery As String = String.Empty
            'strQuery = "SELECT ProjectCode,ProjectNo,ProjectDate,ProjectName,SiteAddress,BlockNo," _
            '& "Phase,Area,City,CustName,CustOffAdd,CustMob,CustEmail,RepName,RepMbNo,RepEmail,GaurdName,GaurdMbNo," _
            '& "GaurdEmail,ComDirector,ComGM,ComASM,ComManager,ComSE,ComTS,ComSS,ProjType,ProjSize,BulName,BulAdd," _
            '& "BulPhNo,BulOwner,BulMbNo,BulEmail,BulGManager,BulGMMbNo,BulGMEmail,BulPManager,BulPMMbNo,BulPMEmail," _
            '& "ArcName,ArcAdd,ArcPhNo,ArcOwner,ArcMBNo,ArchEmail,ArcGManager,ArcGMMbNo,ArcGMEmail,ArcPManager,ArcPMMbNo," _
            '& "ArcPMEmail,ConName,ConAdd,ConPhNo,ConOwner,ConMBNo,ConEmail,ConMainName,ConMainMbNo,ConMAINEmail,ConConName," _
            '& "ConConMbNo,ConConEmail,IsNull(EstimatedQutationValue,0) as EstimatedQutationValue,IsNULL(AllQuotaionValue,0) AS ALLQuotationsAmount" _
            '& " FROM tblProjectPortfolio LEFT OUTER JOIN(SELECT SUM(tblProjectQuotation.QuotationAmount) as AllQuotaionValue,ProjectId FROM tblProjectQuotation Group BY ProjectId) AS Quotations" _
            '& " ON tblProjectPortfolio.ProjectCode=Quotations.ProjectId "
            ''Order by ProjectNo desc"
            'Marked Against Task#20150801 Add User Id 
            'Altered Against Task#20150801 Add User Id 
            Dim strQuery As String = String.Empty
            strQuery = "SELECT ProjectCode,ProjectNo,ProjectDate,ProjectName,SiteAddress,BlockNo," _
            & "Phase,Area,City,CustName,CustOffAdd,CustMob,CustEmail,RepName,RepMbNo,RepEmail,GaurdName,GaurdMbNo," _
            & "GaurdEmail,ComDirector,ComGM,ComASM,ComManager,ComSE,ComTS,ComSS,ProjType,ProjSize,BulName,BulAdd," _
            & "BulPhNo,BulOwner,BulMbNo,BulEmail,BulGManager,BulGMMbNo,BulGMEmail,BulPManager,BulPMMbNo,BulPMEmail," _
            & "ArcName,ArcAdd,ArcPhNo,ArcOwner,ArcMBNo,ArchEmail,ArcGManager,ArcGMMbNo,ArcGMEmail,ArcPManager,ArcPMMbNo," _
            & "ArcPMEmail,ConName,ConAdd,ConPhNo,ConOwner,ConMBNo,ConEmail,ConMainName,ConMainMbNo,ConMAINEmail,ConConName," _
            & "ConConMbNo,ConConEmail,IsNull(EstimatedQutationValue,0) as EstimatedQutationValue,IsNULL(AllQuotaionValue,0) AS ALLQuotationsAmount,tblprojectportfolio.userid" _
            & " FROM tblProjectPortfolio LEFT OUTER JOIN(SELECT SUM(tblProjectQuotation.QuotationAmount) as AllQuotaionValue,ProjectId FROM tblProjectQuotation Group BY ProjectId) AS Quotations" _
            & " ON tblProjectPortfolio.ProjectCode=Quotations.ProjectId "
            'Altered Against Task#20150801 Add User Id 

            Dim cond As String = String.Empty
            cond = IIf(ViewAll = True, "", "Where tblprojectportfolio.UserId = " & LoginUserId & "")

            strQuery = strQuery + cond
            strQuery = strQuery + " order by tblprojectportfolio.projectcode"

            adp = New OleDbDataAdapter(strQuery, Con)

            adp.Fill(dt)
            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()
            Me.grdHistory.RootTable.Columns(0).Visible = False
            Me.grdHistory.RootTable.Columns("UserID").Visible = False
            Me.grdHistory.RootTable.Columns("ProjectDate").FormatString = "dd/MMMM/yyyy"
            CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grdHistory.AutoSizeColumns()
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
            Throw ex
        End Try
    End Sub

    Sub EditRecord()
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me.CurrentId = Me.grdHistory.CurrentRow.Cells(0).Value.ToString
            Me.txtProjectCode.Text = grdHistory.CurrentRow.Cells("ProjectNo").Value.ToString
            Me.txtProjectName.Text = grdHistory.CurrentRow.Cells("ProjectName").Value.ToString.ToString
            Me.txtSiteAddress.Text = grdHistory.CurrentRow.Cells("SiteAddress").Value.ToString
            Me.txtBlockNo.Text = grdHistory.CurrentRow.Cells("BlockNo").Value.ToString
            Me.txtPhase.Text = grdHistory.CurrentRow.Cells("Phase").Value.ToString
            Me.txtArea.Text = grdHistory.CurrentRow.Cells("Area").Value.ToString
            Me.txtCity.Text = grdHistory.CurrentRow.Cells("City").Value.ToString
            Me.txtCustName.Text = grdHistory.CurrentRow.Cells("CustName").Value.ToString
            Me.txtCustOffAdd.Text = grdHistory.CurrentRow.Cells("CustOffAdd").Value.ToString
            Me.txtCustMob.Text = grdHistory.CurrentRow.Cells("CustMob").Value.ToString
            Me.txtCustEmail.Text = grdHistory.CurrentRow.Cells("CustEmail").Value.ToString
            Me.txtRepName.Text = grdHistory.CurrentRow.Cells("RepName").Value.ToString
            Me.txtRepMbNo.Text = grdHistory.CurrentRow.Cells("RepMbNo").Value.ToString
            Me.txtRepEmail.Text = grdHistory.CurrentRow.Cells("RepEmail").Value.ToString
            Me.txtGaurdName.Text = grdHistory.CurrentRow.Cells("GaurdName").Value.ToString
            Me.txtGaurdMbNo.Text = grdHistory.CurrentRow.Cells("GaurdMbNo").Value.ToString
            Me.txtGaurdEmail.Text = grdHistory.CurrentRow.Cells("GaurdEmail").Value.ToString
            'Me.txtComDirector.Text = grdHistory.CurrentRow.Cells("ComDirector").Value.ToString
            'Me.txtComGM.Text = grdHistory.CurrentRow.Cells("ComGM").Value.ToString
            'Me.txtComASM.Text = grdHistory.CurrentRow.Cells("ComASM").Value.ToString
            'Me.txtComManager.Text = grdHistory.CurrentRow.Cells("ComManager").Value.ToString
            'Me.txtComSE.Text = grdHistory.CurrentRow.Cells("ComSE").Value.ToString
            'Me.txtComTS.Text = grdHistory.CurrentRow.Cells("ComTS").Value.ToString
            'Me.txtComSS.Text = grdHistory.CurrentRow.Cells("ComSS").Value.ToString

            Me.cmbComDirector.Text = grdHistory.CurrentRow.Cells("ComDirector").Value.ToString
            Me.cmbGManager.Text = grdHistory.CurrentRow.Cells("ComGM").Value.ToString
            Me.cmbASM.Text = grdHistory.CurrentRow.Cells("ComASM").Value.ToString
            Me.cmbManager.Text = grdHistory.CurrentRow.Cells("ComManager").Value.ToString
            Me.cmbResPerson.Text = grdHistory.CurrentRow.Cells("ComSE").Value.ToString
            Me.cmbTechSupervisor.Text = grdHistory.CurrentRow.Cells("ComTS").Value.ToString
            Me.cmbSiteSupervisor.Text = grdHistory.CurrentRow.Cells("ComSS").Value.ToString

            Me.cmbProjType.Text = grdHistory.CurrentRow.Cells("ProjType").Value.ToString
            Me.cmbProjSize.Text = grdHistory.CurrentRow.Cells("ProjSize").Value.ToString
            Me.txtBulName.Text = grdHistory.CurrentRow.Cells("BulName").Value.ToString
            Me.txtBulAdd.Text = grdHistory.CurrentRow.Cells("BulAdd").Value.ToString
            Me.txtBulPhNo.Text = grdHistory.CurrentRow.Cells("BulPhNo").Value.ToString
            Me.txtBulOwner.Text = grdHistory.CurrentRow.Cells("BulOwner").Value.ToString
            Me.txtBulMbNo.Text = grdHistory.CurrentRow.Cells("BulMbNo").Value.ToString
            Me.txtBulEmail.Text = grdHistory.CurrentRow.Cells("BulEmail").Value.ToString
            Me.txtBulGManager.Text = grdHistory.CurrentRow.Cells("BulGManager").Value.ToString
            Me.txtBulGMMbNo.Text = grdHistory.CurrentRow.Cells("BulGMMbNo").Value.ToString
            Me.txtBulGMEmail.Text = grdHistory.CurrentRow.Cells("BulGMEmail").Value.ToString
            Me.txtBulPManager.Text = grdHistory.CurrentRow.Cells("BulPManager").Value.ToString
            Me.txtBulPMMbNo.Text = grdHistory.CurrentRow.Cells("BulPMMbNo").Value.ToString
            Me.txtBulPMEmail.Text = grdHistory.CurrentRow.Cells("BulPMEmail").Value.ToString
            Me.txtArcName.Text = grdHistory.CurrentRow.Cells("ArcName").Value.ToString
            Me.txtArcAdd.Text = grdHistory.CurrentRow.Cells("ArcAdd").Value.ToString
            Me.txtArcPhNo.Text = grdHistory.CurrentRow.Cells("ArcPhNo").Value.ToString
            Me.txtArcOwner.Text = grdHistory.CurrentRow.Cells("ArcOwner").Value.ToString
            Me.txtArcMBNo.Text = grdHistory.CurrentRow.Cells("ArcMBNo").Value.ToString
            Me.txtArchEmail.Text = grdHistory.CurrentRow.Cells("ArchEmail").Value.ToString          ''Task# A08062015: Ahmad Sharif: Email missing
            Me.txtArcGManager.Text = grdHistory.CurrentRow.Cells("ArcGManager").Value.ToString
            Me.txtArcGMMbNo.Text = grdHistory.CurrentRow.Cells("ArcGMMbNo").Value.ToString
            Me.txtArcGMEmail.Text = grdHistory.CurrentRow.Cells("ArcGMEmail").Value.ToString
            Me.txtArcPManager.Text = grdHistory.CurrentRow.Cells("ArcPManager").Value.ToString
            Me.txtArcPMMbNo.Text = grdHistory.CurrentRow.Cells("ArcPMMbNo").Value.ToString
            Me.txtArcPMEmail.Text = grdHistory.CurrentRow.Cells("ArcPMEmail").Value.ToString
            Me.txtConName.Text = grdHistory.CurrentRow.Cells("ConName").Value.ToString
            Me.txtConAdd.Text = grdHistory.CurrentRow.Cells("ConAdd").Value.ToString
            Me.txtConPhNo.Text = grdHistory.CurrentRow.Cells("ConPhNo").Value.ToString
            Me.txtConOwner.Text = grdHistory.CurrentRow.Cells("ConOwner").Value.ToString
            Me.txtConMBNo.Text = grdHistory.CurrentRow.Cells("ConMBNo").Value.ToString
            Me.txtConEmail.Text = grdHistory.CurrentRow.Cells("ConEmail").Value.ToString
            Me.txtConMainName.Text = grdHistory.CurrentRow.Cells("ConMainName").Value.ToString
            Me.txtConMainMbNo.Text = grdHistory.CurrentRow.Cells("ConMainMbNo").Value.ToString
            Me.txtConMAINEmail.Text = grdHistory.CurrentRow.Cells("ConMAINEmail").Value.ToString
            Me.txtConConName.Text = grdHistory.CurrentRow.Cells("ConConName").Value.ToString
            Me.txtConConMbNo.Text = grdHistory.CurrentRow.Cells("ConConMbNo").Value.ToString
            Me.txtConConEmail.Text = grdHistory.CurrentRow.Cells("ConConEmail").Value.ToString
            Me.txtProjectEstValue.Text = Val(grdHistory.CurrentRow.Cells("EstimatedQutationValue").Value.ToString)
            Me.txtAllQuotationValue.Text = Val(grdHistory.CurrentRow.Cells("ALLQuotationsAmount").Value.ToString)
            If IsDBNull(grdHistory.CurrentRow.Cells("ProjectDate").Value) Then
                Me.dtpProjDate.Value = Date.Now
            Else
                Me.dtpProjDate.Value = grdHistory.CurrentRow.Cells("ProjectDate").Value
            End If
            'Me.btnSave.Text = "&Update"
            'Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0)
            'Me.StockTab.SelectedTab = TabPage2
            'Me.TabControl2.SelectedTab = TabPage1
            'Me.GetSecurityRights()
            GetSecurityRights()
            Me.MainTab.SelectedTab = Me.MainTab.TabPages(0)
            Me.DetailTab.SelectedTab = Me.DetailTab.TabPages(0)

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
            Throw ex
        End Try
    End Sub

    Private Sub ProjectPortFolio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
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
                If MainTab.SelectedIndex = 1 Then
                    If Me.grdHistory.RowCount > 0 Then
                        Me.BtnDelete_Click(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectPortFolio_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub ProjectPortFolio_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.RefreshForm()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
            Me.lblProgress.Visible = False
            Me.BindGrid()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                'Exit Sub
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
            If (Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save") AndAlso Me.MainTab.SelectedIndex = 0 Then
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
                Me.btnSave.Visible = True
            Else
                Me.btnDelete.Visible = True
                Me.btnPrint.Visible = True
                If Me.MainTab.SelectedIndex = 1 Then
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

    Private Sub grdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHistory.DoubleClick
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            Me.EditRecord()
        Catch ex As Exception
            'Throw ex
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tbllistzone WHERE ZoneId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        '                   Me.uitxtName.Text = dt.Rows(0).Item("BeltName").ToString
                        '                  Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString
                        '                 Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        '                Me.uichkActive.Checked = dt.Rows(0).Item("Active")
                        '               Me.CurrentId = dt.Rows(0).Item("beltId")
                        Me.btnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try

            If Condition = "ProjType" Then
                FillDropDown(Me.cmbProjType, "Select Distinct ProjType, ProjType From tblProjectPortfolio where ProjType <> ''", False)
            ElseIf Condition = "ProjSize" Then
                FillDropDown(Me.cmbProjSize, "Select Distinct ProjSize, ProjSize From tblProjectPortfolio where ProjSize <> ''", False)
                'Ahmad Sharif: added new Conditions ,adding on 05-06-2015
            ElseIf Condition = "Director" Then
                FillDropDown(Me.cmbComDirector, "Select Distinct ComDirector, ComDirector From tblProjectPortfolio where ComDirector <> ''", False)
            ElseIf Condition = "GM" Then
                FillDropDown(Me.cmbGManager, "Select Distinct ComGM, ComGM From tblProjectPortfolio where ComGM <> ''", False)
            ElseIf Condition = "ASM" Then
                FillDropDown(Me.cmbASM, "Select Distinct ComASM, ComASM From tblProjectPortfolio where ComASM <> ''", False)
            ElseIf Condition = "Manager" Then
                FillDropDown(Me.cmbManager, "Select Distinct ComManager, ComManager From tblProjectPortfolio where ComManager <> ''", False)
            ElseIf Condition = "ResPerson" Then
                FillDropDown(Me.cmbResPerson, "Select Distinct ComSE, ComSE From tblProjectPortfolio where ComSE <> ''", False)
            ElseIf Condition = "TechSupervisor" Then
                FillDropDown(Me.cmbTechSupervisor, "Select Distinct ComTS, ComTS From tblProjectPortfolio where ComTS <> ''", False)
            ElseIf Condition = "SiteSupervisor" Then
                FillDropDown(Me.cmbSiteSupervisor, "Select Distinct ComSS, ComSS From tblProjectPortfolio where ComSS <> ''", False)
                'Ahmad Sharif: end
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If chkDuplicateProj(CurrentId) = True Then
            ShowErrorMessage("Duplicate Reocrd Exists.")
            Exit Sub
        End If

        'Ahmad Sharif : Date should be less or current date
        If dtpProjDate.Value.Date > Now.Date Then
            ShowErrorMessage("Selected date should be current date or lesser from current date")
            dtpProjDate.Focus()
            Exit Sub
        End If

        Dim emailAddressMatch As Match
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        If txtCustEmail.Text <> String.Empty Then

            emailAddressMatch = Regex.Match(Me.txtCustEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = ProjectTab
                txtCustEmail.Focus()
                Exit Sub
            End If
        End If
        If Me.txtProjectName.Text = String.Empty Then
            ShowErrorMessage("Please enter project name.")
            Me.txtProjectName.Focus()
            Exit Sub
        End If

        If txtRepEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtRepEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = ProjectTab
                txtRepEmail.Focus()
                Exit Sub

            End If
        End If

        If txtGaurdEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtGaurdEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = ProjectTab
                txtGaurdEmail.Focus()
                Exit Sub
            End If
        End If

        If txtBulEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtBulEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtBulEmail.Focus()
                Exit Sub

            End If
        End If
        If txtBulEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtBulEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtBulEmail.Focus()
                Exit Sub
            End If
        End If

        If txtBulGMEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtBulGMEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtBulGMEmail.Focus()
                Exit Sub

            End If
        End If

        If txtBulPMEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtBulPMEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtBulPMEmail.Focus()
                Exit Sub

            End If
        End If

        If txtArcGMEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtArcGMEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtArcGMEmail.Focus()
                Exit Sub

            End If
        End If


        If txtArcGMEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtArcGMEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtArcGMEmail.Focus()
                Exit Sub
            End If
        End If

        If txtArcPMEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtArcPMEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtArcPMEmail.Focus()
                Exit Sub
            End If
        End If

        If txtConEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtConEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtConEmail.Focus()
                Exit Sub
            End If
        End If


        If txtConMAINEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtConMAINEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtConMAINEmail.Focus()
                Exit Sub
            End If
        End If

        If txtConConEmail.Text <> String.Empty Then
            emailAddressMatch = Regex.Match(Me.txtConConEmail.Text, pattern)
            If emailAddressMatch.Success = False Then
                ShowErrorMessage("Please enter valid email")
                Me.DetailTab.SelectedTab = OtherTab
                txtConConEmail.Focus()
                Exit Sub
            End If
        End If


        Dim cm As New OleDbCommand


        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            'Ahmad Sharif: Update Query , modification on 05-06-2015
            ''Task# B08062015 Ahmad Sharif: Owner Mobile NO saved in Main Consultant Mobile No
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "&Save" Then
                'Marked Against Task#20150801 Ali Ansari Add UserId in Save
                'cm.CommandText = "insert into tblprojectportfolio(ProjectNo,ProjectName,SiteAddress,BlockNo,Phase,Area,City, " _
                '& " CustName,CustOffAdd,CustMob,CustEmail,RepName,RepMbNo,RepEmail,GaurdName,GaurdMbNo,GaurdEmail," _
                '& " ComDirector,ComGM,ComASM,ComManager,ComSE,ComTS,ComSS,ProjType,ProjSize," _
                '& " BulName,BulAdd, BulPhNo,BulOwner,BulMbNo,BulEmail,BulGManager,BulGMMbNo,BulGMEmail,BulPManager,BulPMMbNo,BulPMEmail," _
                '& " ArcName,ArcAdd,ArcPhNo,ArcOwner,ArcMBNo,ArchEmail,ArcGManager,ArcGMMbNo,ArcGMEmail,ArcPManager,ArcPMMbNo,ArcPMEmail," _
                '& " ConName,ConAdd,ConPhNo,ConOwner,ConMBNo,ConEmail,ConMainName,ConMainMbNo,ConMAINEmail,ConConName,ConConMbNo,ConConEmail, EstimatedQutationValue,ProjectDate) values( " _
                '& " N'" & Me.txtProjectCode.Text.Replace("'", "''") & "', N'" & Me.txtProjectName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtSiteAddress.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBlockNo.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtPhase.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArea.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCity.Text.ToString.Replace("'", "''") & "'," _
                '& "N'" & Me.txtCustName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustOffAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustMob.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustEmail.Text.ToString.Replace("'", "''") & "', " _
                '& "N'" & Me.txtRepName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRepMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRepEmail.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtGaurdName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtGaurdMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtGaurdEmail.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbComDirector.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbASM.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.cmbManager.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbResPerson.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbTechSupervisor.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.cmbSiteSupervisor.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbProjType.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbProjSize.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtBulName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtBulOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtBulGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulGMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulGMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtBulPManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPMEmail.Text.ToString.Replace("'", "''") & "'," _
                '& " N'" & Me.txtArcName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtArcOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcMBNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArchEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtArcGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcGMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcGMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtArcPManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtConName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtConOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMBNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " N'" & Me.txtConMainName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMainMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMAINEmail.Text.ToString.Replace("'", "''") & "', " _
                '& "N'" & Me.txtConConName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConConMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConConEmail.Text.ToString.Replace("'", "''") & "'," & Val(Me.txtProjectEstValue.Text) & ", Convert(DateTime,'" & Me.dtpProjDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Select @@Identity"
                'Marked Against Task#20150801 Ali Ansari Add UserId in Save
                'Altered Against Task#20150801 Ali Ansari Add UserId in Save
                cm.CommandText = "insert into tblprojectportfolio(ProjectNo,ProjectName,SiteAddress,BlockNo,Phase,Area,City, " _
                & " CustName,CustOffAdd,CustMob,CustEmail,RepName,RepMbNo,RepEmail,GaurdName,GaurdMbNo,GaurdEmail," _
                & " ComDirector,ComGM,ComASM,ComManager,ComSE,ComTS,ComSS,ProjType,ProjSize," _
                & " BulName,BulAdd, BulPhNo,BulOwner,BulMbNo,BulEmail,BulGManager,BulGMMbNo,BulGMEmail,BulPManager,BulPMMbNo,BulPMEmail," _
                & " ArcName,ArcAdd,ArcPhNo,ArcOwner,ArcMBNo,ArchEmail,ArcGManager,ArcGMMbNo,ArcGMEmail,ArcPManager,ArcPMMbNo,ArcPMEmail," _
                & " ConName,ConAdd,ConPhNo,ConOwner,ConMBNo,ConEmail,ConMainName,ConMainMbNo,ConMAINEmail,ConConName,ConConMbNo,ConConEmail, EstimatedQutationValue,ProjectDate,userid) values( " _
                & " N'" & Me.txtProjectCode.Text.Replace("'", "''") & "', N'" & Me.txtProjectName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtSiteAddress.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBlockNo.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtPhase.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArea.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCity.Text.ToString.Replace("'", "''") & "'," _
                & "N'" & Me.txtCustName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustOffAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustMob.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCustEmail.Text.ToString.Replace("'", "''") & "', " _
                & "N'" & Me.txtRepName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRepMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRepEmail.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtGaurdName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtGaurdMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtGaurdEmail.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbComDirector.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbASM.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.cmbManager.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbResPerson.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbTechSupervisor.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.cmbSiteSupervisor.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbProjType.Text.ToString.Replace("'", "''") & "',N'" & Me.cmbProjSize.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtBulName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtBulOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtBulGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulGMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulGMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtBulPManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtBulPMEmail.Text.ToString.Replace("'", "''") & "'," _
                & " N'" & Me.txtArcName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtArcOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcMBNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArchEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtArcGManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcGMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcGMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtArcPManager.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPMMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtArcPMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtConName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConAdd.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtConOwner.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMBNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConEmail.Text.ToString.Replace("'", "''") & "', " _
                & " N'" & Me.txtConMainName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMainMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConMAINEmail.Text.ToString.Replace("'", "''") & "', " _
                & "N'" & Me.txtConConName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConConMbNo.Text.ToString.Replace("'", "''") & "',N'" & Me.txtConConEmail.Text.ToString.Replace("'", "''") & "'," & Val(Me.txtProjectEstValue.Text) & ", Convert(DateTime,'" & Me.dtpProjDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)," & LoginUserId & ") Select @@Identity"
                'Altered Against Task#20150801 Ali Ansari Add UserId in Save
            Else
                ''Marked Against Task#20150801 Ali Ansari Update UserId in Save
                'cm.CommandText = "update tblprojectportfolio set ProjectNo=N'" & Me.txtProjectCode.Text.Replace("'", "''") & "',ProjectName=N'" & Me.txtProjectName.Text.ToString.Replace("'", "''") & "'," _
                '& " SiteAddress=N'" & Me.txtSiteAddress.Text.ToString.Replace("'", "''") & "'," _
                '& "BlockNo=N'" & Me.txtBlockNo.Text.ToString.Replace("'", "''") & "', " _
                '& " Phase=N'" & Me.txtPhase.Text.ToString.Replace("'", "''") & "', " _
                '& " Area=N'" & Me.txtArea.Text.ToString.Replace("'", "''") & "', " _
                '& " City=N'" & Me.txtCity.Text.ToString.Replace("'", "''") & "', " _
                '& " CustName=N'" & Me.txtCustName.Text.ToString.Replace("'", "''") & "', " _
                '& " CustOffAdd=N'" & Me.txtCustOffAdd.Text.ToString.Replace("'", "''") & "', " _
                '& " CustMob=N'" & Me.txtCustMob.Text.ToString.Replace("'", "''") & "', " _
                '& " CustEmail=N'" & Me.txtCustEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " RepName=N'" & Me.txtRepName.Text.ToString.Replace("'", "''") & "', " _
                '& " RepMbNo=N'" & Me.txtRepMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " RepEmail=N'" & Me.txtRepEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " GaurdName=N'" & Me.txtGaurdName.Text.ToString.Replace("'", "''") & "', " _
                '& " GaurdMbNo=N'" & Me.txtGaurdMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " GaurdEmail=N'" & Me.txtGaurdEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ComDirector=N'" & Me.cmbComDirector.Text.ToString.Replace("'", "''") & "', " _
                '& " ComGM=N'" & Me.cmbGManager.Text.ToString.Replace("'", "''") & "', " _
                '& " ComASM=N'" & Me.cmbASM.Text.ToString.Replace("'", "''") & "', " _
                '& " ComManager=N'" & Me.cmbManager.Text.ToString.Replace("'", "''") & "', " _
                '& " ComSE=N'" & Me.cmbResPerson.Text.ToString.Replace("'", "''") & "', " _
                '& " ComTS=N'" & Me.cmbTechSupervisor.Text.ToString.Replace("'", "''") & "', " _
                '& " ComSS=N'" & Me.cmbSiteSupervisor.Text.ToString.Replace("'", "''") & "', " _
                '& " ProjType=N'" & Me.cmbProjType.Text.ToString.Replace("'", "''") & "', " _
                '& " ProjSize=N'" & Me.cmbProjSize.Text.ToString.Replace("'", "''") & "', " _
                '& " BulName=N'" & Me.txtBulName.Text.ToString.Replace("'", "''") & "', " _
                '& " BulAdd=N'" & Me.txtBulAdd.Text.ToString.Replace("'", "''") & "', " _
                '& " BulPhNo=N'" & Me.txtBulPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " BulOwner=N'" & Me.txtBulOwner.Text.ToString.Replace("'", "''") & "', " _
                '& " BulMbNo=N'" & Me.txtBulMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " BulEmail=N'" & Me.txtBulEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " BulGManager=N'" & Me.txtBulGManager.Text.ToString.Replace("'", "''") & "', " _
                '& " BulGMMbNo=N'" & Me.txtBulGMMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " BulGMEmail=N'" & Me.txtBulGMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " BulPManager=N'" & Me.txtBulPManager.Text.ToString.Replace("'", "''") & "', " _
                '& " BulPMMbNo=N'" & Me.txtBulPMMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " BulPMEmail=N'" & Me.txtBulPMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcName=N'" & Me.txtArcName.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcAdd=N'" & Me.txtArcAdd.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcPhNo=N'" & Me.txtArcPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcOwner=N'" & Me.txtArcOwner.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcMBNo=N'" & Me.txtArcMBNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ArchEmail=N'" & Me.txtArchEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcGManager=N'" & Me.txtArcGManager.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcGMMbNo=N'" & Me.txtArcGMMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcGMEmail=N'" & Me.txtArcGMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcPManager=N'" & Me.txtArcPManager.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcPMMbNo=N'" & Me.txtArcPMMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ArcPMEmail=N'" & Me.txtArcPMEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ConName=N'" & Me.txtConName.Text.ToString.Replace("'", "''") & "', " _
                '& " ConAdd=N'" & Me.txtConAdd.Text.ToString.Replace("'", "''") & "', " _
                '& " ConPhNo=N'" & Me.txtConPhNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ConOwner=N'" & Me.txtConOwner.Text.ToString.Replace("'", "''") & "', " _
                '& " ConMBNo=N'" & Me.txtConMBNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ConEmail=N'" & Me.txtConEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ConMainName=N'" & Me.txtConMainName.Text.ToString.Replace("'", "''") & "', " _
                '& " ConMainMbNo=N'" & Me.txtConMainMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ConMAINEmail=N'" & Me.txtConMAINEmail.Text.ToString.Replace("'", "''") & "', " _
                '& " ConConName=N'" & Me.txtConConName.Text.ToString.Replace("'", "''") & "', " _
                '& " ConConMbNo=N'" & Me.txtConConMbNo.Text.ToString.Replace("'", "''") & "', " _
                '& " ConConEmail=N'" & Me.txtConConEmail.Text.ToString.Replace("'", "''") & "', EstimatedQutationValue=" & Val(Me.txtProjectEstValue.Text) & ", ProjectDate=Convert(DateTime,'" & Me.dtpProjDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) where projectcode=" & Me.CurrentId
                ''Marked Against Task#20150801 Ali Ansari Update UserId in Save


                'Altered Against Task#20150801 Ali Ansari Update UserId in Save
                cm.CommandText = "update tblprojectportfolio set ProjectNo=N'" & Me.txtProjectCode.Text.Replace("'", "''") & "',ProjectName=N'" & Me.txtProjectName.Text.ToString.Replace("'", "''") & "'," _
                & " SiteAddress=N'" & Me.txtSiteAddress.Text.ToString.Replace("'", "''") & "'," _
                & "BlockNo=N'" & Me.txtBlockNo.Text.ToString.Replace("'", "''") & "', " _
                & " Phase=N'" & Me.txtPhase.Text.ToString.Replace("'", "''") & "', " _
                & " Area=N'" & Me.txtArea.Text.ToString.Replace("'", "''") & "', " _
                & " City=N'" & Me.txtCity.Text.ToString.Replace("'", "''") & "', " _
                & " CustName=N'" & Me.txtCustName.Text.ToString.Replace("'", "''") & "', " _
                & " CustOffAdd=N'" & Me.txtCustOffAdd.Text.ToString.Replace("'", "''") & "', " _
                & " CustMob=N'" & Me.txtCustMob.Text.ToString.Replace("'", "''") & "', " _
                & " CustEmail=N'" & Me.txtCustEmail.Text.ToString.Replace("'", "''") & "', " _
                & " RepName=N'" & Me.txtRepName.Text.ToString.Replace("'", "''") & "', " _
                & " RepMbNo=N'" & Me.txtRepMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " RepEmail=N'" & Me.txtRepEmail.Text.ToString.Replace("'", "''") & "', " _
                & " GaurdName=N'" & Me.txtGaurdName.Text.ToString.Replace("'", "''") & "', " _
                & " GaurdMbNo=N'" & Me.txtGaurdMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " GaurdEmail=N'" & Me.txtGaurdEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ComDirector=N'" & Me.cmbComDirector.Text.ToString.Replace("'", "''") & "', " _
                & " ComGM=N'" & Me.cmbGManager.Text.ToString.Replace("'", "''") & "', " _
                & " ComASM=N'" & Me.cmbASM.Text.ToString.Replace("'", "''") & "', " _
                & " ComManager=N'" & Me.cmbManager.Text.ToString.Replace("'", "''") & "', " _
                & " ComSE=N'" & Me.cmbResPerson.Text.ToString.Replace("'", "''") & "', " _
                & " ComTS=N'" & Me.cmbTechSupervisor.Text.ToString.Replace("'", "''") & "', " _
                & " ComSS=N'" & Me.cmbSiteSupervisor.Text.ToString.Replace("'", "''") & "', " _
                & " ProjType=N'" & Me.cmbProjType.Text.ToString.Replace("'", "''") & "', " _
                & " ProjSize=N'" & Me.cmbProjSize.Text.ToString.Replace("'", "''") & "', " _
                & " BulName=N'" & Me.txtBulName.Text.ToString.Replace("'", "''") & "', " _
                & " BulAdd=N'" & Me.txtBulAdd.Text.ToString.Replace("'", "''") & "', " _
                & " BulPhNo=N'" & Me.txtBulPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " BulOwner=N'" & Me.txtBulOwner.Text.ToString.Replace("'", "''") & "', " _
                & " BulMbNo=N'" & Me.txtBulMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " BulEmail=N'" & Me.txtBulEmail.Text.ToString.Replace("'", "''") & "', " _
                & " BulGManager=N'" & Me.txtBulGManager.Text.ToString.Replace("'", "''") & "', " _
                & " BulGMMbNo=N'" & Me.txtBulGMMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " BulGMEmail=N'" & Me.txtBulGMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " BulPManager=N'" & Me.txtBulPManager.Text.ToString.Replace("'", "''") & "', " _
                & " BulPMMbNo=N'" & Me.txtBulPMMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " BulPMEmail=N'" & Me.txtBulPMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ArcName=N'" & Me.txtArcName.Text.ToString.Replace("'", "''") & "', " _
                & " ArcAdd=N'" & Me.txtArcAdd.Text.ToString.Replace("'", "''") & "', " _
                & " ArcPhNo=N'" & Me.txtArcPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " ArcOwner=N'" & Me.txtArcOwner.Text.ToString.Replace("'", "''") & "', " _
                & " ArcMBNo=N'" & Me.txtArcMBNo.Text.ToString.Replace("'", "''") & "', " _
                & " ArchEmail=N'" & Me.txtArchEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ArcGManager=N'" & Me.txtArcGManager.Text.ToString.Replace("'", "''") & "', " _
                & " ArcGMMbNo=N'" & Me.txtArcGMMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " ArcGMEmail=N'" & Me.txtArcGMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ArcPManager=N'" & Me.txtArcPManager.Text.ToString.Replace("'", "''") & "', " _
                & " ArcPMMbNo=N'" & Me.txtArcPMMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " ArcPMEmail=N'" & Me.txtArcPMEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ConName=N'" & Me.txtConName.Text.ToString.Replace("'", "''") & "', " _
                & " ConAdd=N'" & Me.txtConAdd.Text.ToString.Replace("'", "''") & "', " _
                & " ConPhNo=N'" & Me.txtConPhNo.Text.ToString.Replace("'", "''") & "', " _
                & " ConOwner=N'" & Me.txtConOwner.Text.ToString.Replace("'", "''") & "', " _
                & " ConMBNo=N'" & Me.txtConMBNo.Text.ToString.Replace("'", "''") & "', " _
                & " ConEmail=N'" & Me.txtConEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ConMainName=N'" & Me.txtConMainName.Text.ToString.Replace("'", "''") & "', " _
                & " ConMainMbNo=N'" & Me.txtConMainMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " ConMAINEmail=N'" & Me.txtConMAINEmail.Text.ToString.Replace("'", "''") & "', " _
                & " ConConName=N'" & Me.txtConConName.Text.ToString.Replace("'", "''") & "', " _
                & " ConConMbNo=N'" & Me.txtConConMbNo.Text.ToString.Replace("'", "''") & "', " _
                & " ConConEmail=N'" & Me.txtConConEmail.Text.ToString.Replace("'", "''") & "', EstimatedQutationValue=" & Val(Me.txtProjectEstValue.Text) & ", ProjectDate=Convert(DateTime,'" & Me.dtpProjDate.Value.ToString("yyyy-M-d 00:00:00") & "',102), userid = " & LoginUserId & " where projectcode=" & Me.CurrentId
                'Altered Against Task#20150801 Ali Ansari Update UserId in Save
            End If
            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())

            Try

                SaveActivityLog("Config", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0
        Catch ex As Exception

            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If Not grdHistory.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If


        Try

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim cm As New OleDbCommand

            ''Ahmad Sharif: Task# 507-June-2015, Check if Project Visit or Quotation exist against project then don't delete project 
            Dim strQuery As String = String.Empty
            Dim projectId As Integer = Convert.ToInt32(Me.grdHistory.CurrentRow.Cells("ProjectCode").Value.ToString)

            If Con.State = ConnectionState.Closed Then Con.Open()
            cm.Connection = Con

            strQuery = "SELECT ProjectId FROM tblProjectVisit WHERE ProjectId= " & projectId & " UNION SELECT ProjectId FROM tblProjectQuotation WHERE ProjectId=" & projectId

            cm = New OleDbCommand(strQuery, Con)

            Dim rs As Integer = cm.ExecuteScalar()

            If rs > 0 Then
                ShowErrorMessage("Project Visit or Project Quotation exist against this Project")
                Exit Sub
            Else
                If msg_Confirm(str_ConfirmDelete) = True Then
                    cm.CommandText = "delete from TblProjectPortfolio where projectcode=" & Me.grdHistory.CurrentRow.Cells("ProjectCode").Value.ToString
                    cm.ExecuteNonQuery()
                    Me.CurrentId = 0
                Else
                    msg_Error(str_ErrorDependentRecordFound)
                End If
            End If
            ''end Task: Task# 507-June-2015
            Try
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdHistory.CurrentRow.Cells("ProjectCode").Value.ToString, True)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()

    End Sub

    Private Sub ValidateForm()
        Try
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = "Project Portfolio History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdHistory_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            RefreshForm()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtConConMbNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConMBNo.KeyPress, txtConMainMbNo.KeyPress, txtConConMbNo.KeyPress, txtBulPMMbNo.KeyPress, txtBulMbNo.KeyPress, txtBulGMMbNo.KeyPress, txtArcPMMbNo.KeyPress, txtArcMBNo.KeyPress, txtArcGMMbNo.KeyPress, txtRepMbNo.KeyPress, txtGaurdMbNo.KeyPress, txtCustMob.KeyPress
        Try
            If e.KeyChar <> ControlChars.Back Then
                e.Handled = ("0123456789+".IndexOf(e.KeyChar) = -1)
                'ElseIf e.KeyChar = Convert.ToChar(Keys.Control) And e.KeyChar = Convert.ToChar(Keys.C) Then
                '    e.Handled = True
            End If



            'If e.KeyChar <> ControlChars.Back Then
            '    If Char.IsNumber(e.KeyChar) = False Then
            '        e.Handled = True
            '    Else
            '        e.Handled = False
            '    End If
            'ElseIf e.KeyChar = Convert.ToChar(e.KeyChar) AndAlso e.KeyChar = Convert.ToChar(Keys.C) Then
            '    e.Handled = True
            'ElseIf e.KeyChar = Convert.ToChar(Keys.Control) AndAlso e.KeyChar = Convert.ToChar(Keys.V) Then
            '    e.Handled = True
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub MainTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainTab.SelectedIndexChanged
        Try
            If Me.MainTab.SelectedIndex = 0 Then
                Me.CtrlGrdBar2.Visible = False
            Else
                Me.CtrlGrdBar2.Visible = True
            End If
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnProjectVisit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjectVisit.Click
        Try
            frmMain.LoadControl("frmProjectVisit")
            BindGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function chkDuplicateProj(ByVal Id As Integer) As Boolean
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select Count(*) From TblProjectPortFolio WHERE ProjectCode <> " & CurrentId & " AND (ProjectName='" & Me.txtProjectName.Text.Replace("'", "''") & "')")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub EditByProjectVisit(ByVal Id As Integer)
        Try
            Dim flag As Boolean = False
            flag = Me.grdHistory.Find(Me.grdHistory.RootTable.Columns(0), Janus.Windows.GridEX.ConditionOperator.Equal, Id, 0, 1)

            ''Ahmad Sharif: Task# 607-June-2015 if find the project id in grdHistory then allow to double click otherwise no
            If flag = True Then
                Me.grdHistory_DoubleClick(Nothing, Nothing)
            Else
                Exit Sub
            End If
            ''end task: Task# 607-June-2015

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub frmProjectProtfolio_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.KeyPreview = True
            ''Ahmad Sharif: add Drop downs on 05-June-2015
            FillCombo("Director")
            Me.cmbComDirector.Text = String.Empty
            FillCombo("GM")
            Me.cmbGManager.Text = String.Empty
            FillCombo("ASM")
            Me.cmbASM.Text = String.Empty
            FillCombo("Manager")
            Me.cmbManager.Text = String.Empty
            FillCombo("ResPerson")
            Me.cmbResPerson.Text = String.Empty
            FillCombo("TechSupervisor")
            Me.cmbTechSupervisor.Text = String.Empty
            FillCombo("SiteSupervisor")
            Me.cmbSiteSupervisor.Text = String.Empty
            ''end task: on 05-June-2015

            IsOpenedForm = True
            'ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class


