''New screen of Lead profile created on 26-09-2018 by Muhammad Amin
'' TASK TFS4867 : Implementation of displaying Employee image and Name mapped against a user. Done on 22-10-2018 by Muhammad Amin
Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmLeadProfile2
    Implements IGeneral
    Dim Lead As LeadProfileBE2
    Dim LeadProfileDAL As New LeadProfileDAL2()
    'Public DoHaveSaveRights As Boolean = False
    'Public DoHaveUpdateRights As Boolean = False
    'Public DoHaveConvertToAccountRights As Boolean = False
    'Public DoHaveActivityViewRights As Boolean = False
    Public LeadProfileId As Integer = 0
    Public IsEditMode As Boolean = False
    Public IsAccountCreated As Boolean = False
    Dim CompanyName1 As String = String.Empty
    Dim DoHaveDeleteRights As Boolean = False
    Dim TotalAttachments As Integer = 0
    Dim arrFile As New List(Of String)
    Dim ObjPath As String = String.Empty
    Dim LogoFiles As New List(Of String)
    Dim EmployeeId As Integer = 0
    Dim ContactlList As List(Of CompanyContactBE)
    Dim AccountId As Integer = 0


    Private Sub frmLeadProfile_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
                'frmLeadProfileList2.FillCombos()
                'frmLeadProfileList2.GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal HaveSaveRights As Boolean, ByVal HavePrintRights As Boolean, ByVal HaveExportRights As Boolean, ByVal HaveFieldChooserRights As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        EnableAndDisableButtons(HaveSaveRights, False, False, False, HavePrintRights, HaveExportRights, HaveFieldChooserRights, False)
        FillAllCombos()
        ReSetControls()
    End Sub
    Public Sub New(ByVal HaveUpdateRights As Boolean, ByVal HaveViewHistoryRights As Boolean, ByVal HaveConvertToAccountRights As Boolean, ByVal HavePrintRights As Boolean, ByVal HaveExportRights As Boolean, ByVal HaveFieldChooserRights As Boolean, ByVal HaveDeleteRights As Boolean, ByVal Obj As LeadProfileBE2)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        EnableAndDisableButtons(False, HaveUpdateRights, HaveConvertToAccountRights, HaveViewHistoryRights, HavePrintRights, HaveExportRights, HaveFieldChooserRights, True)
        FillAllCombos()
        ReSetControls()
        EditRecord(Obj)
        DoHaveDeleteRights = HaveDeleteRights
    End Sub
    Private Sub frmLeadProfile2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'btnSave.FlatAppearance.BorderSize = 0
            'btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
            ''btnCancel.FlatAppearance.BorderSize = 0
            ''btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
            '' Tooltip
            'FillCombos("Type")
            'FillCombos("Status")
            'FillCombos("Source")
            'FillCombos("Industry")
            'FillCombos("InterestedIn")
            'FillCombos("Focus")
            'FillCombos("Activity")
            'Dim dt As DataTable = New LeadProfileDAL().GetById(LeadID)
            'Dim i As Integer
            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        txtDocumentNo.Text = dt.Rows(0).Item("LeadTitle")
            '        cmbType.SelectedValue = dt.Rows(0).Item("SectorId")
            '        txtCompanyName.Text = dt.Rows(0).Item("ProductName")
            '        cmbActivity.SelectedValue = dt.Rows(0).Item("Statusid")
            '        'txtStatusRemarks.Text = dt.Rows(0).Item("StatusRemarks")
            '        cmbSource.SelectedValue = dt.Rows(0).Item("SourceId")
            '        txtActivityRemarks.Text = dt.Rows(0).Item("SourceRemarks")
            '        cmbIndustry.SelectedValue = dt.Rows(0).Item("ResponsibleId")
            '        cmbStatus.SelectedValue = dt.Rows(0).Item("InsideSalesId")
            '        cmbInterestedIn.SelectedValue = dt.Rows(0).Item("ManagerId")
            '        btnSave.Enabled = DoHaveUpdateRights
            '    Next
            'Else
            '    ReSetControls()
            'End If
            'GetConcernPerson(LeadID)
            'GetLeadOffice(LeadID)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   

  

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Type" Then
                str = "Select Id, Title from tblDefLeadType where Active = 1"
                FillUltraDropDown(Me.cmbType, str, True)
                Me.cmbType.Rows(0).Activate()
                Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Activity" Then
                str = "Select Id, Title FROM tblDefLeadActivity where Active = 1"
                FillUltraDropDown(Me.cmbActivity, str, True)
                Me.cmbActivity.Rows(0).Activate()
                Me.cmbActivity.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Source" Then
                str = "Select Id, Title from tblDefLeadSource where Active = 1"
                FillUltraDropDown(Me.cmbSource, str, True)
                Me.cmbSource.Rows(0).Activate()
                Me.cmbSource.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Industry" Then
                str = "select Id, Title From tblDefLeadIndustry Where Active = 1"
                FillUltraDropDown(Me.cmbIndustry, str, True)
                Me.cmbIndustry.Rows(0).Activate()
                Me.cmbIndustry.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Status" Then
                str = "SELECT Distinct Status, Status From tblDefLeadProfile WHERE ISNULL(Status, '') <> '' "
                FillDropDown(Me.cmbStatus, str, False)
            ElseIf Condition = "InterestedIn" Then
                str = "select Id, Title From tblDefInterestedIn Where Active = 1"
                FillUltraDropDown(Me.cmbInterestedIn, str, True)
                Me.cmbInterestedIn.Rows(0).Activate()
                Me.cmbInterestedIn.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                FillListBox(Me.lstInterestedIn.ListItem, "select Id, Title From tblDefInterestedIn Where Active = 1")
            ElseIf Condition = "Focus" Then
                str = "select Id, Title From tblDefBrandFocus Where Active = 1"
                FillUltraDropDown(Me.cmbBrandFocus, str, True)
                Me.cmbBrandFocus.Rows(0).Activate()
                Me.cmbBrandFocus.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                FillListBox(Me.lstBrandFocus.ListItem, "select Id, Title From tblDefBrandFocus Where Active = 1")
            ElseIf Condition = "NoofEmployee" Then
                str = "select Id, NoofEmployee From tblDefNoofEmployee Where Active = 1"
                FillUltraDropDown(Me.cmbNoofEmployee, str, True)
                Me.cmbNoofEmployee.Rows(0).Activate()
                Me.cmbNoofEmployee.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Department" Then
                str = "select EmployeeDeptId AS Id, EmployeeDeptName AS Department From EmployeeDeptDefTable Where Active = 1"
                FillUltraDropDown(Me.cmbDepartment, str, True)
                Me.cmbDepartment.Rows(0).Activate()
                Me.cmbDepartment.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Country" Then
                str = "select CountryId, CountryName From tblListCountry Where Active = 1 order by 2 asc"
                FillUltraDropDown(Me.cmbCountry, str, True)
                Me.cmbCountry.Rows(0).Activate()
                Me.cmbCountry.DisplayLayout.Bands(0).Columns("CountryId").Hidden = True
            ElseIf Condition = "MasterCountry" Then
                str = "select CountryId, CountryName From tblListCountry Where Active = 1 order by 2 asc"
                FillUltraDropDown(Me.cmbMasterCountry, str, True)
                Me.cmbMasterCountry.Rows(0).Activate()
                Me.cmbMasterCountry.DisplayLayout.Bands(0).Columns("CountryId").Hidden = True
            ElseIf Condition = "City" Then
                'str = "select CityId, CityName From tblListCity Where Active = 1"
                'FillUltraDropDown(Me.cmbCity, str, True)
                'Me.cmbCity.Rows(0).Activate()
                'Me.cmbCity.DisplayLayout.Bands(0).Columns("CityId").Hidden = True
            ElseIf Condition = "grdDepartment" Then
                str = "select EmployeeDeptId AS Id, EmployeeDeptName AS Department From EmployeeDeptDefTable Where Active = 1 Union All SELECT 0, 'Select Any Value' "
                Dim dtDepartment As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns("DepartmentId").ValueList.PopulateValueList(dtDepartment.DefaultView, "Id", "Department")
            ElseIf Condition = "grdCountry" Then
                str = "select CountryId, CountryName From tblListCountry Where Active = 1 Union All SELECT 0, 'Select Any Value'"
                Dim dtCountry As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns("CountryId").ValueList.PopulateValueList(dtCountry.DefaultView, "CountryId", "CountryName")
            ElseIf Condition = "grdCity" Then
                'str = "select CityId, CityName From tblListCity Where Active = 1 Union All SELECT 0, 'Select Any Value'"
                'Dim dtCity As DataTable = GetDataTable(str)
                'Me.grd.RootTable.Columns("CityId").ValueList.PopulateValueList(dtCity.DefaultView, "CityId", "CityName")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Lead = New LeadProfileBE2
            Lead.LeadProfileId = LeadProfileId
            Lead.AccountId = AccountId
            Lead.DocDate = Me.dtpDocumentDate.Value
            Lead.DocNo = Me.txtDocumentNo.Text
            Lead.TypeId = cmbType.Value
            Lead.CompanyName = Me.txtCompanyName.Text
            Lead.ActivityId = cmbActivity.Value
            'Lead.StatusRemarks = txtStatusRemarks.Text
            Lead.SourceId = cmbSource.Value
            Lead.Remarks = txtActivityRemarks.Text
            Lead.IndustryId = cmbIndustry.Value
            Lead.Status = cmbStatus.Text
            Lead.InterestedInId = lstInterestedIn.SelectedIDs
            Lead.BrandFocusId = lstBrandFocus.SelectedIDs
            Lead.NoofEmployeeId = cmbNoofEmployee.Value
            Lead.UserName = LoginUserName
            Lead.ModifiedUser = LoginUserName
            Lead.ModifiedDate = Now
            Lead.EmployeeId = EmployeeId
            Lead.CountryId = cmbMasterCountry.Value
            Lead.Website = txtWebsite.Text
            Lead.ActivityLog = New ActivityLog
            Lead.IsAccountCreated = IsAccountCreated
            Lead.Address = txtAddress.Text
            For i As Integer = 0 To grd.RowCount - 1
                Dim Detail As New LeadProfileContactsBE
                Detail.ContactId = Val(grd.GetRows(i).Cells("ContactId").Value.ToString)
                Detail.LeadProfileId = Val(grd.GetRows(i).Cells("LeadProfileId").Value.ToString)
                Detail.Salutation = grd.GetRows(i).Cells("Salutation").Value.ToString
                Detail.FirstName = grd.GetRows(i).Cells("FirstName").Value.ToString
                Detail.LastName = grd.GetRows(i).Cells("LastName").Value.ToString
                Detail.JobTitle = grd.GetRows(i).Cells("JobTitle").Value.ToString
                Detail.DepartmentId = Val(grd.GetRows(i).Cells("DepartmentId").Value.ToString)
                Detail.Department = grd.GetRows(i).Cells("DepartmentId").Text.ToString
                Detail.Email1 = grd.GetRows(i).Cells("Email1").Value.ToString
                Detail.Email2 = grd.GetRows(i).Cells("Email2").Value.ToString
                Detail.Mobile1 = grd.GetRows(i).Cells("Mobile1").Value.ToString
                Detail.Mobile2 = grd.GetRows(i).Cells("Mobile2").Value.ToString
                Detail.Phone = grd.GetRows(i).Cells("Phone").Value.ToString
                Detail.Extention = grd.GetRows(i).Cells("Extention").Value.ToString
                Detail.Website = grd.GetRows(i).Cells("Website").Value.ToString
                Detail.CountryId = Val(grd.GetRows(i).Cells("CountryId").Value.ToString)
                Detail.Country = grd.GetRows(i).Cells("CountryId").Text.ToString

                Detail.CityId = grd.GetRows(i).Cells("CityId").Value.ToString
                'Detail.City = grd.GetRows(i).Cells("CityId").Text.ToString

                Lead.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocumentNo.Text = String.Empty Then
                ShowErrorMessage("Document No is required.")
                Me.txtDocumentNo.Focus()
                Return False
            End If
            If Me.txtCompanyName.Text = String.Empty Then
                ShowErrorMessage("Company name is required.")
                Me.txtDocumentNo.Focus()
                Return False
            End If
            'If Me.txtMobile1.Text = String.Empty Then
            '    ShowErrorMessage("Mobile1 is required.")
            '    Me.txtMobile1.Focus()
            '    Return False
            'End If
            'If Me.txtEmail1.Text = String.Empty Then
            '    ShowErrorMessage("Email1 is required.")
            '    Me.txtEmail1.Focus()
            '    Return False
            'End If
            'If Me.grd.RowCount = 0 Then
            '    ShowErrorMessage("Contact grid is empty.")
            '    Me.grd.Focus()
            '    Return False
            'End If
            If IsEditMode = False Then
                If LeadProfileDAL.IsCompanyExisted(Me.txtCompanyName.Text) Then
                    ShowErrorMessage("Company name already exists.")
                    Me.txtCompanyName.Focus()
                    Return False
                End If
            Else
                If CompanyName1 <> Me.txtCompanyName.Text Then
                    If LeadProfileDAL.IsCompanyExisted(Me.txtCompanyName.Text) Then
                        ShowErrorMessage("Company name already exists.")
                        Me.txtCompanyName.Focus()
                        Return False
                    End If
                End If
            End If
            'If Me.cmbIndustry.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any responsible person")
            '    cmbIndustry.Focus()
            '    Return False
            'End If
            If Me.lstInterestedIn.SelectedIDs.Length = 0 Then
                ShowErrorMessage("Please select any Interest")
                lstInterestedIn.Focus()
                Return False
            End If
            If Me.txtWebsite.Text = "" Then
                ShowErrorMessage("Please enter a Website")
                txtWebsite.Focus()
                Return False
            End If
            'If Me.cmbInterestedIn.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any Manager")
            '    cmbStatus.Focus()
            '    Return False
            'End If

            FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidateDetail() As Boolean
        Try
            If Me.txtWebsite.Text = String.Empty Then
                ShowErrorMessage("Email is required.")
                Me.txtWebsite.Focus()
                Return False
            End If
            'If Me.txtMobile1.Text = String.Empty Then
            '    ShowErrorMessage("Mobile1 is required.")
            '    Me.txtMobile1.Focus()
            '    Return False
            'End If
            'If Me.txtEmail1.Text = String.Empty Then
            '    ShowErrorMessage("Email1 is required.")
            '    Me.txtEmail1.Focus()
            '    Return False
            'End If
            If Me.cmbMasterCountry.Value = 0 Then
                ShowErrorMessage("Please select any Country")
                cmbMasterCountry.Focus()
                Return False
            End If
            'If Me.cmbStatus.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any Inside person")
            '    cmbStatus.Focus()
            '    Return False
            'End If
            'If Me.cmbInterestedIn.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any Manager")
            '    cmbStatus.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If IsEditMode = False Then
                    If LeadProfileDAL.Add(Lead, ObjPath, arrFile, LogoFiles) Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                        Me.Close()
                    End If
                Else
                    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                    If LeadProfileDAL.Update(Lead, ObjPath, arrFile, LogoFiles, ContactlList) Then
                        msg_Information("Record has been updated successfully.")
                        ReSetControls()
                        Me.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ObjPath = getConfigValueByType("FileAttachmentPath").ToString
            IsAccountCreated = False
            IsEditMode = False
            LeadProfileId = 0
            AccountId = 0
            txtDocumentNo.Text = GetDocumentNo()
            Me.dtpDocumentDate.Value = Now
            cmbType.Rows(0).Activate()
            txtCompanyName.Text = ""
            cmbActivity.Rows(0).Activate()
            'txtStatusRemarks.Text = ""
            cmbSource.Rows(0).Activate()
            txtActivityRemarks.Text = ""
            txtAddress.Text = ""
            cmbIndustry.Rows(0).Activate()
            cmbStatus.Text = ""
            Me.cmbMasterCountry.Rows(0).Activate()
            cmbInterestedIn.Rows(0).Activate()
            Me.lstInterestedIn.DeSelect()
            Me.txtWebsite.Text = String.Empty
            Me.txtAddress.Text = ""
            'RAfay
            companyinitials = ""
            'Rafay
            Me.lstBrandFocus.DeSelect()
            Me.cmbBrandFocus.Rows(0).Activate()
            'Me.cmbIndustry.Enabled = False
            'Me.cmbNoofEmployee.Visible = False
            Me.cmbNoofEmployee.Rows(0).Activate()
            Me.txtCompanyName.Enabled = True
            Me.btnSave.Text = "Save"
            CompanyName1 = String.Empty
            DoHaveDeleteRights = True
            arrFile.Clear()
            LogoFiles.Clear()
            GetDetail(-1)
            '' TASK TFS4867
            GetUserWiseEmployee(LoginUserId)
            ''END TASK TFS4867
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.txtSalutation.SelectedIndex = 1
            Me.txtFirstName.Text = String.Empty
            Me.txtLastName.Text = String.Empty
            Me.txtJobTitle.Text = String.Empty
            Me.cmbDepartment.Rows(0).Activate()
            Me.txtEmail1.Text = String.Empty
            Me.txtEmail2.Text = String.Empty
            Me.txtMobile1.Text = String.Empty
            Me.txtMobile2.Text = String.Empty
            Me.txtPhone.Text = String.Empty
            Me.txtExtention.Text = String.Empty
            'Me.txtWebsite.Text = String.Empty
            Me.cmbCity.Text = String.Empty
            Me.cmbCountry.Rows(0).Activate()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddToGrid()
        Try
            '           [ContactId] [int] IDENTITY(1,1) NOT NULL,
            '[LeadProfileId] [int] NULL,
            '[Salutation] [nvarchar](250) NULL,
            '[FirstName] [nvarchar](250) NULL,
            '[LastName] [nvarchar](250) NULL,
            '[JobTitle] [nvarchar](250) NULL,
            '[DepartmentId]  [int] NULL,
            '[Email1] [nvarchar](50) NULL,
            '[Email2] [nvarchar](50) NULL,
            '[Mobile1] [nvarchar](50) NULL,
            '[Mobile2] [nvarchar](50) NULL,
            '[Phone] [nvarchar](50) NULL,
            '[Extention] [nvarchar](50) NULL,
            '[Website] [nvarchar](50) NULL,
            '[CountryId]  [int] NULL,
            '[CityId]  [int] NULL,
            Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim NewRow As DataRow = dtGrid.NewRow
            NewRow.Item("ContactId") = 0
            NewRow.Item("LeadProfileId") = LeadProfileId
            NewRow.Item("Salutation") = Me.txtSalutation.Text
            NewRow.Item("FirstName") = Me.txtFirstName.Text
            NewRow.Item("LastName") = Me.txtLastName.Text
            NewRow.Item("JobTitle") = Me.txtJobTitle.Text
            If Me.cmbDepartment.ActiveRow.Index > 0 Then
                NewRow.Item("DepartmentId") = Me.cmbDepartment.Value
                'NewRow.Item("Department") = Me.cmbDepartment.Text
            Else
                NewRow.Item("DepartmentId") = 0
                NewRow.Item("Department") = ""
            End If
            NewRow.Item("Email1") = Me.txtEmail1.Text
            NewRow.Item("Email2") = Me.txtEmail2.Text
            NewRow.Item("Mobile1") = Me.txtMobile1.Text
            NewRow.Item("Mobile2") = Me.txtMobile2.Text
            NewRow.Item("Phone") = Me.txtPhone.Text
            NewRow.Item("Extention") = Me.txtExtention.Text
            NewRow.Item("Website") = Me.txtWebsite.Text

            NewRow.Item("CityId") = Me.cmbCity.Text
            'NewRow.Item("City") = Me.cmbCity.Text
            If Me.cmbCountry.ActiveRow.Index > 0 Then
                NewRow.Item("CountryId") = Me.cmbCountry.Value
                'NewRow.Item("Country") = Me.cmbCountry.Text
            Else
                NewRow.Item("CountryId") = 0
                NewRow.Item("Country") = ""
            End If
            dtGrid.Rows.Add(NewRow)
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        'Try
        '    Me.Close()
        '    frmLeadProfileList.FillCombos()
        '    frmLeadProfileList.GetAllRecords()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub EditRecord(ByVal Obj As LeadProfileBE2)
        Try
            IsEditMode = True
            LeadProfileId = Obj.LeadProfileId
            AccountId = Obj.AccountId
            Me.dtpDocumentDate.Value = Obj.DocDate
            txtDocumentNo.Text = Obj.DocNo
            cmbType.Value = Obj.TypeId
            txtCompanyName.Text = Obj.CompanyName
            cmbActivity.Value = Obj.ActivityId
            txtActivityRemarks.Text = Obj.Remarks
            cmbSource.Value = Obj.SourceId
            'txtActivityRemarks.Text = String.Empty
            cmbIndustry.Value = Obj.IndustryId
            cmbStatus.Text = Obj.Status
            cmbInterestedIn.Value = Obj.InterestedInId
            Me.lstInterestedIn.SelectItemsByIDs(Obj.InterestedInId)
            Me.lstBrandFocus.SelectItemsByIDs(Obj.BrandFocusId)
            IsAccountCreated = Obj.IsAccountCreated
            Me.cmbBrandFocus.Value = Obj.BrandFocusId
            Me.cmbNoofEmployee.Value = Obj.NoofEmployeeId
            Me.cmbMasterCountry.Value = Obj.CountryId
            txtWebsite.Text = Obj.Website
            txtAddress.Text = Obj.Address
            CompanyName1 = Obj.CompanyName
            Me.btnAttachments.Text = "Attachments (" & Obj.NoOfAttachments & ") "
            If IsAccountCreated = True Then
                Me.txtCompanyName.Enabled = False
                Me.btnConvertToAccount.Visible = False
            Else
                Me.txtCompanyName.Enabled = True
                Me.btnConvertToAccount.Visible = True
            End If
            Me.btnSave.Text = "Update"
            '' TASK TFS4867
            If Obj.EmployeeId > 0 Then
                EmployeeId = Obj.EmployeeId
                GetEmployee(EmployeeId)
            End If
            '' END TASK TFS4867
            GetDetail(LeadProfileId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillAllCombos()
        Try
            FillCombos("Type")
            FillCombos("Status")
            FillCombos("Source")
            FillCombos("Industry")
            FillCombos("InterestedIn")
            FillCombos("Focus")
            FillCombos("NoofEmployee")
            FillCombos("Activity")
            FillCombos("Department")
            FillCombos("Country")
            FillCombos("MasterCountry")
            FillCombos("City")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EnableAndDisableButtons(ByVal HaveSaveRights As Boolean, ByVal HaveUpdateRights As Boolean, ByVal HaveCToAccountRights As Boolean, ByVal HaveViewHistoryRights As Boolean, ByVal HavePrintRights As Boolean, ByVal HaveExportRights As Boolean, ByVal HaveFieldChooserRights As Boolean, Optional ByVal IsEditMode As Boolean = False)
        Try
            If IsEditMode = True Then
                Me.btnConvertToAccount.Visible = True
                Me.btnActivityHistory.Visible = True
                Me.btnSave.Enabled = HaveUpdateRights
                Me.btnConvertToAccount.Enabled = HaveCToAccountRights
                Me.btnActivityHistory.Enabled = HaveViewHistoryRights
                Me.CtrlGrdBar3.mGridPrint.Enabled = HavePrintRights
                Me.CtrlGrdBar3.mGridExport.Enabled = HaveExportRights
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = HaveFieldChooserRights
                Me.btnPrint.Visible = True
            Else
                Me.btnSave.Enabled = HaveSaveRights
                Me.btnConvertToAccount.Visible = False
                Me.btnActivityHistory.Visible = False
                Me.btnConvertToAccount.Enabled = False
                Me.btnActivityHistory.Enabled = False
                Me.CtrlGrdBar3.mGridPrint.Enabled = HavePrintRights
                Me.CtrlGrdBar3.mGridExport.Enabled = HaveExportRights
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = HaveFieldChooserRights
                Me.btnPrint.Visible = False

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If ValidateDetail() = True Then
                AddToGrid()
                ResetDetailControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetDetail(ByVal LeadProfileId As Integer)
        Try
            Dim dtDetail As DataTable = LeadProfileDAL.GetDetail(LeadProfileId)
            'If dtDetail.Rows.Count > 0 Then
            '    txtWebsite.Text = dtDetail.Rows(0).Item("Website")
            'End If
            Me.grd.DataSource = dtDetail
            FillCombos("grdDepartment")
            FillCombos("grdCountry")
            FillCombos("grdCity")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnActivityHistory_Click(sender As Object, e As EventArgs) Handles btnActivityHistory.Click
        Try
            Dim frmActRemarks As New frmActivityRemarks(LeadProfileId)
            frmActRemarks.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub GetActivityRemarks(ByVal LeadProfileId As Integer)
    '    Try
    '        Dim dtActivityRemarks As DataTable = LeadProfileDAL.GetActivityRemarks(LeadProfileId)
    '        Me.grd.DataSource = dtActivityRemarks
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Function ConvertToAccount() As Boolean
        Dim MainCode As String = String.Empty
        Dim SubSubAccount As String = String.Empty
        Try
            If Not getConfigValueByType("LeadProfileHeadAccount") = "Error" Then
                SubSubAccount = getConfigValueByType("LeadProfileHeadAccount")
            End If
            If SubSubAccount.Length > 0 Then
                Dim SubSubAccountId As Integer = CInt(SubSubAccount)
                If SubSubAccountId < 1 Then
                    ShowErrorMessage("Sub Sub Account is not configured.")
                    Exit Function
                End If
                Dim dtMainCode As DataTable = GetDataTable("SELECT sub_sub_code  AS Title From tblCoaMainsubSub where main_sub_sub_id = " & SubSubAccountId & "")
                If dtMainCode.Rows.Count > 0 Then
                    MainCode = dtMainCode.Rows(0).Item(0).ToString
                    Dim Code As String = Microsoft.VisualBasic.Right(GetNextDocNo(MainCode, (MainCode.Trim.Length + 1), "tblCOAMainSubSubDetail", "detail_code", (MainCode.Trim.Length + 6)), 5)
                    Dim CompleteCode As String = MainCode & "-" & Code
                    If LeadProfileDAL.ConvertToAccount(SubSubAccountId, CompleteCode, Me.txtCompanyName.Text, LeadProfileId, ContactlList) Then
                        IsAccountCreated = True
                        Me.btnConvertToAccount.Visible = False
                        msg_Information("Account has been created successfully.")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnConvertToAccount_Click(sender As Object, e As EventArgs) Handles btnConvertToAccount.Click
        Try
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Contact grid is empty.")
                Me.grd.Focus()
                Exit Sub
            End If
            If txtSalutation.SelectedIndex = 0 AndAlso txtEmail1.Text = "" AndAlso txtFirstName.Text = "" AndAlso txtLastName.Text = "" AndAlso txtEmail1.Text = "" AndAlso txtPhone.Text = "" AndAlso cmbCountry.Value = 0 Then
                ShowErrorMessage("Please fill the Mandatory Fields.")
                Me.txtFirstName.Focus()
                Exit Sub
            End If
            If IsAccountCreated = True Then
                ShowErrorMessage("Account has already been created.")
                Exit Sub
            End If
            If LeadProfileDAL.IsAccountExisted(Me.txtCompanyName.Text) = False Then
                FillContactsModel()
                ConvertToAccount()
                frmLeadProfileList2.GetAll(LoginUserName)
                Me.Close()
            Else
                ShowErrorMessage("Account already exists with this name.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetDocumentNo() As String
        Dim StandardNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                'Rafay
                'StandardNo = GetSerialNo("LP" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "tblDefLeadProfile", "DocNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    StandardNo = GetSerialNo("LP" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "tblDefLeadProfile", "DocNo")
                Else
                    companyinitials = "PK"
                    StandardNo = GetNextDocNo("LP" & "-" & companyinitials & "-" & Format(Now, "yy"), 4, "tblDefLeadProfile", "DocNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                ' StandardNo = GetNextDocNo("LP" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "tblDefLeadProfile", "DocNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    StandardNo = GetSerialNo("LP" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "tblDefLeadProfile", "DocNo")
                Else
                    companyinitials = "PK"
                    StandardNo = GetNextDocNo("LP" & "-" & companyinitials & "-" & Format(Now, "yy"), 4, "tblDefLeadProfile", "DocNo")
                End If
                'Rafay
            Else
                StandardNo = GetNextDocNo("LP", 6, "tblDefLeadProfile", "StandardNo")
            End If
            Return StandardNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & CtrlGrdBar3.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.CtrlGrdBar3.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    If IsEditMode = True Then
                        If DoHaveDeleteRights = True Then
                            LeadProfileDAL.DeleteSingle(Me.grd.GetRow.Cells("ContactId").Value.ToString)
                            Me.grd.GetRow.Delete()
                        Else
                            msg_Information("You do not have delete rights.")
                        End If
                    Else
                        Me.grd.GetRow.Delete()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMobile1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMobile1.KeyDown
        'NumValidation(sender, e)
    End Sub

    Private Sub txtMobile2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMobile2.KeyDown
        'NumValidation(sender, e)
    End Sub

    Private Sub txtMobile1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobile1.KeyPress
        '  NumValidation(sender, e)
    End Sub

    Private Sub txtMobile2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobile2.KeyPress
        '  NumValidation(sender, e)
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        ' NumValidation(sender, e)
    End Sub
    ''' <summary>
    ''' TAKS TFS4792
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'If Me.btnSave.Text <> "&Save" Then
                '    If Me.grdSaved.RowCount > 0 Then
                '        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                '    End If
                'End If
                Me.btnAttachments.Text = "Attachments (" & arrFile.Count + TotalAttachments & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4792
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            GetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetLogo()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                LogoFiles.Clear()
                LogoFiles.Add(OpenFileDialog1.FileName)
                'For a = 0 To OpenFileDialog1.FileNames.Length - 1
                '    arrFile.Add(OpenFileDialog1.FileNames(a))
                'Next a
                'If Me.btnSave.Text <> "&Save" Then
                '    If Me.grdSaved.RowCount > 0 Then
                '        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                '    End If
                'End If
                'Me.btnAttachments.Text = "Attachments (" & arrFile.Count + TotalAttachments & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCompanyLogo_Click(sender As Object, e As EventArgs) Handles btnCompanyLogo.Click
        Try
            GetLogo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetUserWiseEmployee(ByVal UserId As Integer)
        Try
            ''TASK TFS4867
            Dim dtEmployee As DataTable = LeadProfileDAL.GetUserWiseEmployee(LoginUserId)
            If dtEmployee.Rows.Count > 0 Then
                lblEmployeeName.Text = dtEmployee.Rows(0).Item("EmployeeName").ToString
                EmployeeId = Val(dtEmployee.Rows(0).Item("EmployeeId").ToString)
                If Not IsDBNull(dtEmployee.Rows(0).Item("EmpPicture")) Then
                    If IO.File.Exists(dtEmployee.Rows(0).Item("EmpPicture")) Then
                        Me.pbUser.ImageLocation = dtEmployee.Rows(0).Item("EmpPicture")
                        Me.pbUser.Update()
                    Else
                        Me.pbUser.Image = Nothing
                    End If
                Else
                    Me.pbUser.Image = Nothing
                End If
            End If
            ''END TASK TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetEmployee(ByVal EmployeeId As Integer)
        Try
            ''TASK TFS4867
            Dim dtEmployee As DataTable = LeadProfileDAL.GetEmployee(EmployeeId)
            If dtEmployee.Rows.Count > 0 Then
                lblEmployeeName.Text = dtEmployee.Rows(0).Item("EmployeeName").ToString
                If Not IsDBNull(dtEmployee.Rows(0).Item("EmpPicture")) Then
                    If IO.File.Exists(dtEmployee.Rows(0).Item("EmpPicture")) Then
                        Me.pbUser.ImageLocation = dtEmployee.Rows(0).Item("EmpPicture")
                        Me.pbUser.Update()
                    Else
                        Me.pbUser.Image = Nothing
                    End If
                Else
                    Me.pbUser.Image = Nothing
                End If
            End If
            ''END TASK TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillContactsModel()
        Try
            ContactlList = New List(Of CompanyContactBE)
            For i As Integer = 0 To grd.RowCount - 1
                Dim Contact As New CompanyContactBE
                Contact.LeadProfileContactId = Val(grd.GetRows(i).Cells("ContactId").Value.ToString)
                Contact.RefCompanyId = 0
                Contact.ContactName = grd.GetRows(i).Cells("FirstName").Value.ToString & " " & grd.GetRows(i).Cells("LastName").Value.ToString
                Contact.Designation = ""
                Contact.Mobile = grd.GetRows(i).Cells("Mobile1").Value.ToString
                Contact.Phone = grd.GetRows(i).Cells("Phone").Value.ToString
                Contact.Fax = ""
                Contact.Email = grd.GetRows(i).Cells("Email1").Value.ToString
                Contact.Address = IIf(grd.GetRows(i).Cells("CountryId").Value > 0, grd.GetRows(i).Cells("CountryId").Text, "")
                Contact.Address += grd.GetRows(i).Cells("CityId").Text
                Contact.IndexNo = 0
                Contact.Type = "Customer"
                Contact.Company = Me.txtCompanyName.Text
                Contact.Department = grd.GetRows(i).Cells("DepartmentId").Text.ToString
                Contact.NamePrefix = grd.GetRows(i).Cells("Salutation").Value.ToString
                Contact.CompanyLocation = ""
                Contact.CompanyLocationId = 0
                ContactlList.Add(Contact)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        AddRptParam("@id", Me.LeadProfileId)
        ShowReport("rptLeadProfile")
    End Sub

    Private Sub cmbType_ValueChanged(sender As Object, e As EventArgs) Handles cmbType.ValueChanged
        Try
            If Me.cmbType.Value = 1 Then
                cmbActivity.Visible = True
                lblActivity.Visible = True
                lblIndustry.Visible = False
                cmbIndustry.Visible = False
            ElseIf Me.cmbType.Value = 3 Then
                cmbActivity.Visible = False
                lblActivity.Visible = False
                cmbIndustry.Visible = True
                lblIndustry.Visible = True
            ElseIf Me.cmbType.Value = 2 Then
                cmbActivity.Visible = False
                lblActivity.Visible = False
                cmbIndustry.Visible = False
                lblIndustry.Visible = False
            ElseIf Me.cmbType.Value = 0 Then
                cmbActivity.Visible = False
                lblActivity.Visible = False
                cmbIndustry.Visible = False
                lblIndustry.Visible = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstBrandFocus_Load(sender As Object, e As EventArgs) Handles lstBrandFocus.Load

    End Sub

    Private Sub CtrlGrdBar3_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load

    End Sub
End Class