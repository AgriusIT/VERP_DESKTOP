''12-10-2015 TASKTFS48 Muhammad Ameen: Save layout is not working and required search criteria based on date
' 14-12-2015 TASKTFS150 Muhammad Ameen: Added ComboBox Assigned To user and functioned it in save and update procedures.
Imports System
Imports System.Net
Imports System.Data
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports SBDal.UtilityDAL
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Public Class frmLeads
    Implements IGeneral
    Dim LeadId As Integer = 0
    Dim IsOpenedForm As Boolean = False
    Dim Leads As LeadInfoBE
    Dim dt As DataTable
    Dim crpt As New ReportDocument
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim LeadNo As String = String.Empty
    Dim Email As Email
    Dim setEditMode As Boolean = False
    Dim strTopic As String = String.Empty
    Dim strDescription As String = String.Empty
    Dim CurrentFollowup As DateTime
    Dim CurrentLeadsDesc As String = String.Empty
    Dim CurrentTopic As String = String.Empty
    Dim blnLoadedAll As Boolean = False
    Dim DuplicateMobile As Boolean = False
    Enum enmLead
        LeadId
        LeadTopic
        LeadName
        JobTitle
        CompanyName
        BusinessType
        Address
        City
        State
        BusinessPhone
        MobilePhone
        OtherPhone
        Fax
        Email
        WebSite
        LeadDescription
        LeadNo
        AccountId
        FollowupHistory
        Followup
        EntryDate
        AssignedTo
        AssignedName
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("AccountId").Visible = False
            Me.grd.RootTable.Columns("AssignedTo").Visible = False
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
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
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                If New LeadsInfoDAL().Delete(Leads) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strQuery As String = String.Empty
            If Condition = "User" Then
                strQuery = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
                Dim dt1 As New DataTable
                Dim dr1 As DataRow
                dt1 = GetDataTable(strQuery)
                For Each dr As DataRow In dt1.Rows
                    dr.BeginEdit()
                    dr(1) = Decrypt(dr("User_Name"))
                    dr.EndEdit()
                Next
                dr1 = dt1.NewRow
                dr1(0) = Convert.ToInt32(0)
                dr1(1) = strZeroIndexItem
                dt1.Rows.InsertAt(dr1, 0)
                dt1.AcceptChanges()
                Me.cmbAssignedTo.DataSource = dt1
                Me.cmbAssignedTo.DisplayMember = "User_Name"
                Me.cmbAssignedTo.ValueMember = "User_ID"
            End If
            strQuery = "Select User_ID, User_Name FROM tblUser Where Active <> 0 "
            FillDropDown(Me.cmbBusinessType, "Select DISTINCT BusinessType, BusinessType From tblLeads WHERE BusinessType <> ''", False)
            FillDropDown(Me.cmbCity, "Select DISTINCT City, City From tblLeads WHERE City <> ''", False)
            FillDropDown(Me.cmbSate, "Select DISTINCT State, State From tblLeads WHERE State <> ''", False)
            FillDropDown(Me.cmbTopic, "Select DISTINCT LeadTopic, LeadTopic from tblLeads WHERE LeadTopic <> ''", False)
            FillUltraDropDown(Me.cmbOtherContact, "Select coa_detail_id, detail_title as [Name], detail_code as [Code], sub_sub_title as [Ac Head],StateName as [Region], CityName as [City], Contact_Phone as Phone, Contact_Mobile as Mobile, Contact_Address as Address, Contact_Email as Email From vwCOADetail WHERE detail_title <> '' and account_type in('Customer','Vendor') ORDER BY detail_title asc")

            Me.cmbOtherContact.Rows(0).Activate()
            If Me.cmbOtherContact.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbOtherContact.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            Leads = New LeadInfoBE
            Leads.LeadId = LeadId
            Leads.LeadTopic = Me.cmbTopic.Text
            Leads.LeadName = Me.txtLeadName.Text
            Leads.CompanyName = Me.txtCompanyName.Text
            Leads.JobTitle = Me.txtJobTitle.Text
            Leads.BusinessType = Me.cmbBusinessType.Text
            Leads.City = Me.cmbCity.Text
            Leads.State = Me.cmbSate.Text
            Leads.Address = Me.txtAddress.Text
            Leads.BusinessPhone = Me.txtBusinessPhone.Text
            Leads.MobilePhone = Me.txtMobilePhone.Text
            Leads.Fax = Me.txtFax.Text
            Leads.otherphone = Me.txtOtherPhone.Text
            Leads.Email = Me.txtEmail.Text
            Leads.WebSite = Me.txtWebSite.Text
            Leads.LeadDescription = Me.txtDescription.Text
            Leads.LeadNo = Me.txtLeadNo.Text
            Leads.AccountId = Me.cmbOtherContact.Value
            Leads.Followup = Me.dtpFollowup.Value
            Leads.EntryDate = Me.dtpEntryDate.Value
            Leads.AssignedTo = Me.cmbAssignedTo.SelectedValue

            Dim strFollowupHistory As String = String.Empty

            Dim str As String = String.Empty
            If Me.cmbTopic.Text <> CurrentTopic Then
                str = "[" & Me.cmbTopic.Text & "]"
            End If
            str += Chr(10) & "[" & LoginUserName & ":" & Date.Now & "]" & Chr(10) & Me.txtDescription.Text
            strFollowupHistory = str & Chr(10) & Me.txtFollowupHistory.Text
            Leads.FollowupHistory = strFollowupHistory 'Me.txtFollowupHistory.Text

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If blnLoadedAll = True Then
                Condition = "All"
            End If
            dt = New LeadsInfoDAL().GetAll(Condition)
            dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbTopic.Text = String.Empty Then
                ShowErrorMessage("Please enter topic")
                Me.cmbTopic.Focus()
                Return False
            End If
            If Me.txtCompanyName.Text = String.Empty Then
                ShowErrorMessage("Please enter company name")
                Me.txtCompanyName.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            LeadId = 0
            Me.btnSave.Text = "&Save"
            Me.txtLeadNo.Text = LeadsInfoDAL.GetDocumentNo
            If Not Me.cmbTopic.SelectedIndex = -1 Then Me.cmbTopic.SelectedIndex = 0
            Me.txtLeadName.Text = String.Empty
            Me.txtJobTitle.Text = String.Empty
            Me.txtCompanyName.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtBusinessPhone.Text = String.Empty
            Me.txtMobilePhone.Text = String.Empty
            Me.txtFax.Text = String.Empty
            Me.txtOtherPhone.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtWebSite.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            CurrentFollowup = Nothing
            CurrentLeadsDesc = String.Empty
            CurrentTopic = String.Empty
            Me.dtpFollowup.Value = Date.Now
            Me.txtFollowupHistory.Text = String.Empty
            'FillCombos()
            If Not Me.cmbAssignedTo.SelectedValue = -1 Then Me.cmbAssignedTo.SelectedValue = 0
            If Not Me.cmbBusinessType.SelectedIndex = -1 Then Me.cmbBusinessType.SelectedIndex = 0
            If Not Me.cmbCity.SelectedIndex = -1 Then Me.cmbCity.SelectedIndex = 0
            If Not Me.cmbSate.SelectedIndex = -1 Then Me.cmbSate.SelectedIndex = 0
            'Me.dtpEntryDate.Enabled = True
            Me.cmbOtherContact.Rows(0).Activate()
            FillGrid()
            ApplySecurity(EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
   

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If IsValidate() = True Then
                Dim int As Integer = 0
                LeadNo = Me.txtLeadNo.Text
                setEditMode = False
                strTopic = Me.cmbTopic.Text.Replace("'", "''")
                strDescription = Me.txtDescription.Text.Replace("'", "''")
                int = New LeadsInfoDAL().Add(Leads)
                If int > 0 Then
                    LeadId = int
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
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
        Try
            If IsValidate() = True Then
                LeadNo = Me.txtLeadNo.Text
                setEditMode = True
                strTopic = Me.cmbTopic.Text.Replace("'", "''")
                strDescription = Me.txtDescription.Text.Replace("'", "''")
                If New LeadsInfoDAL().Update(Leads) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub frmLeads_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos()
            FillCombos("User")
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub EditRecords()
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            LeadId = Me.grd.GetRow.Cells("LeadId").Value
            Me.cmbTopic.Text = Me.grd.GetRow.Cells("LeadTopic").Value.ToString
            Me.txtLeadName.Text = grd.GetRow.Cells("LeadName").Value.ToString
            Me.txtCompanyName.Text = grd.GetRow.Cells("CompanyName").Value.ToString
            Me.txtJobTitle.Text = grd.GetRow.Cells("JobTitle").Value.ToString
            Me.cmbBusinessType.Text = grd.GetRow.Cells("BusinessType").Value.ToString
            Me.txtAddress.Text = grd.GetRow.Cells("Address").Value.ToString
            Me.cmbCity.Text = Me.grd.GetRow.Cells("City").Value.ToString
            Me.cmbSate.Text = Me.grd.GetRow.Cells("State").Value.ToString
            Me.txtBusinessPhone.Text = grd.GetRow.Cells("BusinessPhone").Value.ToString
            Me.txtMobilePhone.Text = grd.GetRow.Cells("MobilePhone").Value.ToString
            Me.txtOtherPhone.Text = Me.grd.GetRow.Cells("OtherPhone").Value.ToString
            Me.txtFax.Text = Me.grd.GetRow.Cells("Fax").Value.ToString
            Me.txtEmail.Text = Me.grd.GetRow.Cells("Email").Value.ToString
            Me.txtWebSite.Text = Me.grd.GetRow.Cells("WebSite").Value.ToString
            Me.txtDescription.Text = Me.grd.GetRow.Cells("LeadDescription").Value.ToString
            Me.txtLeadNo.Text = Me.grd.GetRow.Cells("LeadNo").Value.ToString
            Me.cmbOtherContact.Value = Me.grd.GetRow.Cells("AccountId").Value.ToString

            If IsDBNull(Me.grd.GetRow.Cells("Followup").Value) Then
                Me.dtpFollowup.Value = Date.Now
            Else
                Me.dtpFollowup.Value = Me.grd.GetRow.Cells("Followup").Value

            End If
            If IsDBNull(Me.grd.GetRow.Cells("EntryDate").Value) Then
                Me.dtpEntryDate.Value = Date.Now
                'Me.dtpEntryDate.Enabled = True
            Else
                Me.dtpEntryDate.Value = Me.grd.GetRow.Cells("EntryDate").Value
                Me.dtpEntryDate.Enabled = False
            End If
            CurrentFollowup = Me.dtpFollowup.Value
            CurrentLeadsDesc = Me.txtDescription.Text
            CurrentTopic = Me.grd.GetRow.Cells("LeadTopic").Value.ToString
            Me.txtFollowupHistory.Text = Me.grd.GetRow.Cells("FollowupHistory").Value.ToString
            Me.cmbAssignedTo.SelectedValue = Val(Me.grd.GetRow.Cells("AssignedTo").Value.ToString())
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 0 Then
                Me.btnLoadAll.Visible = False
            Else
                Me.btnLoadAll.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            blnLoadedAll = True
            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop
            Me.grd.DataSource = Nothing
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            GetAllRecords("All")
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            dtpEntryDate.Value = Date.Now

        Catch ex As Exception
            ShowErrorMessage("Error occurred while reset controls: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while updating record: " & ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Start Task
        'TFS1859:Rai Haider:06-Dec-17:Check added to allow duplicate Mobile No
        Try
            If Not getConfigValueByType("DuplicateMobileInLeadsInfo").ToString = "Error" Then
                DuplicateMobile = getConfigValueByType("DuplicateMobileInLeadsInfo")
            End If
            If DuplicateMobile = False Then
                If Me.btnSave.Text = "&Save" Then
                    If New LeadsInfoDAL().IsMobileExist(txtMobilePhone.Text) = True Then
                        ShowErrorMessage("Mobile Number Already Exist")
                        Me.txtMobilePhone.Focus()
                    Else
                        If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                        If Save() = True Then

                            msg_Information(str_informSave)
                            ReSetControls()
                        End If
                    End If
                    If Me.BackgroundWorker3.IsBusy Then Exit Sub
                    BackgroundWorker3.RunWorkerAsync()
                    Do While BackgroundWorker3.IsBusy
                        Application.DoEvents()
                    Loop

                    If Me.BackgroundWorker4.IsBusy Then Exit Sub
                    BackgroundWorker4.RunWorkerAsync()
                    Do While BackgroundWorker4.IsBusy
                        Application.DoEvents()
                    Loop
                Else
                    If New LeadsInfoDAL().IsMobileExist(txtMobilePhone.Text) = False Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Update1() = True Then
                            msg_Information(str_informUpdate)
                            ReSetControls()
                        End If

                    Else
                        If New LeadsInfoDAL().IsMobileExist(txtMobilePhone.Text, txtLeadNo.Text) = True Then
                            ShowErrorMessage("Mobile number already exist with other lead information")
                            Me.txtMobilePhone.Focus()
                        Else
                            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                            If Update1() = True Then
                                msg_Information(str_informUpdate)
                                ReSetControls()
                            End If
                            If Me.BackgroundWorker3.IsBusy Then Exit Sub
                            BackgroundWorker3.RunWorkerAsync()
                            Do While BackgroundWorker3.IsBusy
                                Application.DoEvents()
                            Loop

                            If Me.BackgroundWorker4.IsBusy Then Exit Sub
                            BackgroundWorker4.RunWorkerAsync()
                            Do While BackgroundWorker4.IsBusy
                                Application.DoEvents()

                            Loop
                        End If
                        dtpEntryDate.Value = Date.Now
                    End If
                End If
            Else
                If Me.btnSave.Text = "&Save" Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        msg_Information(str_informSave)
                        ReSetControls()
                    End If
                    If Me.BackgroundWorker3.IsBusy Then Exit Sub
                    BackgroundWorker3.RunWorkerAsync()
                    Do While BackgroundWorker3.IsBusy
                        Application.DoEvents()
                    Loop

                    If Me.BackgroundWorker4.IsBusy Then Exit Sub
                    BackgroundWorker4.RunWorkerAsync()
                    Do While BackgroundWorker4.IsBusy
                        Application.DoEvents()
                    Loop
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                    If Me.BackgroundWorker3.IsBusy Then Exit Sub
                    BackgroundWorker3.RunWorkerAsync()
                    Do While BackgroundWorker3.IsBusy
                        Application.DoEvents()
                    Loop

                    If Me.BackgroundWorker4.IsBusy Then Exit Sub
                    BackgroundWorker4.RunWorkerAsync()
                    Do While BackgroundWorker4.IsBusy
                        Application.DoEvents()

                    Loop
                End If
                dtpEntryDate.Value = Date.Now
            End If


        Catch ex As Exception
            ShowErrorMessage("Error occurred while saving record:" & ex.Message)
        End Try

        'End TFS1859:06-Dec-17
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If LeadId = 0 Then
                ShowErrorMessage("Cannot delete record")
                Me.cmbTopic.Focus()
                Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occurred while delete record: " & ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while updating record: " & ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As String = String.Empty
            id = Me.cmbBusinessType.Text
            FillDropDown(Me.cmbBusinessType, "Select DISTINCT BusinessType, BusinessType From tblLeads", True)
            Me.cmbBusinessType.Text = id
            id = Me.cmbCity.Text
            FillDropDown(Me.cmbCity, "Select DISTINCT City, City From tblLeads", True)
            Me.cmbCity.Text = id
            id = Me.cmbSate.Text
            FillDropDown(Me.cmbSate, "Select DISTINCT State, State From tblLeads", True)
            Me.cmbSate.Text = id
            id = Me.cmbTopic.Text
            FillDropDown(Me.cmbTopic, "Select DISTINCT LeadTopic, LeadTopic from tblLeads", True)
            Me.cmbTopic.Text = id
            id = Me.cmbOtherContact.ActiveRow.Cells(0).Value
            FillUltraDropDown(Me.cmbOtherContact, "Select coa_detail_id, detail_title as [Name], detail_code as [Code], sub_sub_title as [Ac Head],StateName as [Region], CityName as [City], Phone, Mobile, Address, Email From vwCOADetail WHERE detail_title <> '' and account_type in('Customer','Vendor') ORDER BY detail_title asc")
            Me.cmbOtherContact.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            ShowReport("rptLeads", "{tblLeads.LeadId} = " & Me.grd.GetRow.Cells(0).Value & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal LeadId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\rptLeads.rpt", DBServerName)
                    If DBUserName <> "" Then
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                        crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
                    Else
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
                    End If

                    Dim ConnectionInfo As New ConnectionInfo
                    With ConnectionInfo
                        .ServerName = DBServerName
                        .DatabaseName = DBName
                        If DBUserName <> "" Then
                            .UserID = DBUserName
                            .Password = DBPassword
                            .IntegratedSecurity = False
                        Else
                            .IntegratedSecurity = True
                        End If
                    End With
                    Dim tbLogOnInfo As New TableLogOnInfo
                    For Each dt As Table In crpt.Database.Tables
                        tbLogOnInfo = dt.LogOnInfo
                        tbLogOnInfo.ConnectionInfo = ConnectionInfo
                        dt.ApplyLogOnInfo(tbLogOnInfo)
                    Next
                    crpt.RecordSelectionFormula = "{tblLeads.LeadId} = " & Me.grd.GetRow.Cells(0).Value

                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Lead Information" & "-" & LeadNo & ""
                    SourceFile = String.Empty
                    SourceFile = _FileExportPath & "\" & FileName & ".pdf"
                    crDiskOps.DiskFileName = SourceFile
                    crExportOps = crpt.ExportOptions
                    With crExportOps
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportDestinationOptions = crDiskOps
                        .ExportFormatOptions = crExportType
                    End With
                    'crpt.Refresh()
                    Try
                        crpt.SetParameterValue("@CompanyName", CompanyTitle)
                        crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
                        crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
                    Catch ex As Exception
                        'IsCompanyInfo = False
                        'CompanyTitle = String.Empty
                        'CompanyAddHeader = String.Empty
                    End Try
                    crpt.Export(crExportOps)
                    'crpt.Close()
                    'crpt.Dispose()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If IsEmailAlert = True Then
            Email = New Email
            Email.ToEmail = AdminEmail
            Email.CCEmail = String.Empty
            Email.BccEmail = String.Empty
            Email.Attachment = SourceFile
            Email.Subject = "Lead Information " & LeadNo & ""
            Email.Body = "Lead Information " _
            & " " & IIf(setEditMode = False, Chr(10) & "Subject:" & strTopic & "" & Chr(10) & strDescription & "", "Subject:" & strTopic & "" & Chr(10) & strDescription & "") & Chr(10) & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
            Email.Status = "Pending"
            Call New MailSentDAL().Add(Email)
        End If
        Return EmailSave

    End Function

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BackgroundWorker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Try
            ExportFile(LeadId)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbOtherContact_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOtherContact.Leave
        Try
            If Me.cmbOtherContact.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbOtherContact.ActiveRow.Cells(0).Value > 0 Then
                Me.txtCompanyName.Text = Me.cmbOtherContact.ActiveRow.Cells("Name").Value.ToString
                Me.txtMobilePhone.Text = Me.cmbOtherContact.ActiveRow.Cells("Mobile").Value.ToString
                Me.txtOtherPhone.Text = Me.cmbOtherContact.ActiveRow.Cells("Phone").Value.ToString
                Me.txtEmail.Text = Me.cmbOtherContact.ActiveRow.Cells("Email").Value.ToString
                Me.txtAddress.Text = Me.cmbOtherContact.ActiveRow.Cells("Address").Value.ToString
                Me.txtBusinessPhone.Text = Me.cmbOtherContact.ActiveRow.Cells("Phone").Value.ToString
                Me.cmbCity.Text = Me.cmbOtherContact.ActiveRow.Cells("City").Value.ToString
                Me.cmbSate.Text = Me.cmbOtherContact.ActiveRow.Cells("Region").Value.ToString
                Me.cmbTopic.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintEnvelopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintEnvelopToolStripMenuItem.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@ID", Me.grd.GetRow.Cells("LeadID").Value)
            ShowReport("PrintAddressEnvelopByLeads", , "Nothing", "Nothing", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub btnSearchDocument_Click(sender As Object, e As EventArgs) Handles btnSearchDocument.Click
        Try
            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLeads_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Me.SplitContainer1.Panel1Collapsed = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            ' ''TASKTFS48
            'Me.grd.DataSource = New LeadsInfoDAL().GetAll("All", dtpFrom.Value, dtpTo.Value)
            'Me.grd.RetrieveStructure()
            blnLoadedAll = True
            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop
            Me.grd.DataSource = Nothing
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

End Class