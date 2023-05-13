Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.SqlClient
Public Class frmDefCRMProject
    Implements IGeneral
    Public Shared ProjectId As Integer
    Public Shared ProjectQuotaionId As Integer
    Public Shared projectSalesOrderId As Integer
    Dim project As ProjectDefBE
    Dim projectQuotation As ProjectQuotationsBE
    Dim projectSalesOrder As ProjectSalesOrderBE
    Public projectDefDal As ProjectDefDAL = New ProjectDefDAL()
    Public projectQuottionDefDal As ProjectQuotationsDAL = New ProjectQuotationsDAL()
    Public ProjectSalesOrderDal As ProjectSalesOrderDAL = New ProjectSalesOrderDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub tbMain_Click(sender As Object, e As EventArgs) Handles tbMain.Click

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Me.grdActivities.RootTable.Columns("ActivityId").Visible = False
        Me.grdActivities.RootTable.Columns("LeadId").Visible = False
        Me.grdActivities.RootTable.Columns("LeadContactId").Visible = False
        Me.grdActivities.RootTable.Columns("LeadOfficeId").Visible = False
        Me.grdActivities.RootTable.Columns("LeadActivityTypeID").Visible = False
        Me.grdActivities.RootTable.Columns("ResponsiblePerson_Employee_Id").Visible = False
        Me.grdActivities.RootTable.Columns("InsideSalesPerson_Employee_Id").Visible = False
        Me.grdActivities.RootTable.Columns("Manager_Employee_Id").Visible = False
        Me.grdActivities.RootTable.Columns("ActivityTime").FormatString = "hh:mm:ss tt"
        Me.grdActivities.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat


        'Me.grdSalesOrder.RootTable.Columns("ProjectSalesOrderID").Visible = False
        'Me.grdQuotations.RootTable.Columns("ProjectQuotaionID").Visible = False

        '// Adding Delete Button in the grid
        If Me.grdQuotations.RootTable.Columns.Contains("Delete") = False Then
            Me.grdQuotations.RootTable.Columns.Add("Delete")
            Me.grdQuotations.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdQuotations.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdQuotations.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdQuotations.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdQuotations.RootTable.Columns("Delete").Width = 70
            Me.grdQuotations.RootTable.Columns("Delete").ButtonText = "Delete"
            Me.grdQuotations.RootTable.Columns("Delete").Key = "Delete"
            Me.grdQuotations.RootTable.Columns("Delete").Caption = "Action"
        End If

        '// Adding Delete Button in the grid
        If Me.grdSalesOrder.RootTable.Columns.Contains("Delete") = False Then
            Me.grdSalesOrder.RootTable.Columns.Add("Delete")
            Me.grdSalesOrder.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdSalesOrder.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdSalesOrder.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdSalesOrder.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdSalesOrder.RootTable.Columns("Delete").Width = 70
            Me.grdSalesOrder.RootTable.Columns("Delete").ButtonText = "Delete"
            Me.grdSalesOrder.RootTable.Columns("Delete").Key = "Delete"
            Me.grdSalesOrder.RootTable.Columns("Delete").Caption = "Action"
        End If

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                'Me.CtrlGrdBar1.mGridPrint.Enabled = True
                'Me.CtrlGrdBar1.mGridExport.Enabled = True
                'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    'Me.CtrlGrdBar1.mGridExport.Enabled = False
                    'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                'Me.CtrlGrdBar1.mGridExport.Enabled = False
                'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                        'ElseIf RightsDt.FormControlName = "Print" Then
                        '    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Export" Then
                        '    Me.CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        '    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Show" Then
                        '    btnAddDock.Enabled = True
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
        Try
            Dim str As String
            If Condition = "Region" Then
                str = "select RegionId, Regionname from tblListRegion where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbRegion, str, True)
            ElseIf Condition = "City" Then
                str = "select CityId, CityName from tblListCity where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbCity, str, True)
            ElseIf Condition = "Status" Then
                str = "select ProjectStatusId, ProjectStatus from tblDefProjectStatus where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbProjectStatus, str, True)
            ElseIf Condition = "ProjectCategory" Then
                str = "select ProjectCategoryId, ProjectCategory from tblDefProjectCategory where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbProjectCategory, str, True)
            ElseIf Condition = "Lead" Then
                str = "select LeadId, LeadTitle from LeadProfile where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbLead, str, True)
            ElseIf Condition = "Contact" Then
                str = "select Employee_ID, Employee_Name from tblDefEmployee where Active = 1 order by 1 desc"
                FillDropDown(Me.cmbMainContactPerson, str, True)

            ElseIf Condition = "Consultant" Then
                str = "select Pk_Id, ContactName from tblCompanyContacts where Type = 'Engineering Consultant ' order by 1 desc"
                FillDropDown(Me.cmbEngineeringConsultant, str, True)
            ElseIf Condition = "ContractAward" Then
                str = "select Pk_Id, ContactName from tblCompanyContacts where Type = 'Engineering Consultant ' order by 1 desc"
                FillDropDown(Me.cmbContractAward, str, True)
            ElseIf Condition = "Responsible" Then
                str = "select Employee_ID, Employee_Name from tblDefEmployee order by 1 desc"
                FillDropDown(Me.cmbResponsible, str, True)
            ElseIf Condition = "Inside" Then
                str = "select Employee_ID, Employee_Name from tblDefEmployee order by 1 desc"
                FillDropDown(Me.cmbInsideSales, str, True)
            ElseIf Condition = "Manager" Then
                str = "select Employee_ID, Employee_Name from tblDefEmployee order by 1 desc"
                FillDropDown(Me.cmbManager, str, True)
            ElseIf Condition = "Quotation" Then
                str = "select QuotationId, QuotationNo from QuotationMasterTable Order by QuotationId Desc"
                FillDropDown(Me.cmbQuotation, str, True)
            ElseIf Condition = "SalesOrder" Then
                str = "select SalesOrderId, SalesOrderNo from SalesOrderMasterTable Order by SalesOrderId Desc"
                FillDropDown(Me.cmbSalesOrder, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            project = New ProjectDefBE
            project.ProjectId = ProjectId
            project.ProjectTitle = Me.txtProject.Text
            project.ProjectCode = Me.txtCode.Text
            project.Plant = Me.txtPlant.Text
            project.Scope = Me.txtScope.Text
            project.Address = Me.txtAddress.Text
            project.RegionId = cmbRegion.SelectedValue
            project.CityId = cmbCity.SelectedValue
            project.Products = Me.txtProduct.Text
            project.ProjectStatusId = cmbProjectStatus.SelectedValue
            project.ProjectCategoryId = cmbProjectCategory.SelectedValue

            project.LeadProfileId = cmbLead.SelectedValue
            project.ContactPersonId = cmbMainContactPerson.SelectedValue
            project.EngineeringConsultantId = cmbEngineeringConsultant.SelectedValue
            project.ContractAwardedId = cmbContractAward.SelectedValue
            project.ResponsiblePersonId = cmbResponsible.SelectedValue
            project.InsideSalesPersonId = cmbInsideSales.SelectedValue
            project.ManagerId = cmbManager.SelectedValue
            project.Details = Me.richProjectDetail.Text
            project.ActivityLog = New ActivityLog

            projectQuotation = New ProjectQuotationsBE
            projectQuotation.QuotationId = Me.cmbQuotation.SelectedValue
            projectQuotation.ProjectId = ProjectId
            projectQuotation.ActivityLog = New ActivityLog

            projectSalesOrder = New ProjectSalesOrderBE()
            projectSalesOrder.SalesOrderId = Me.cmbSalesOrder.SelectedValue
            projectSalesOrder.ProjectId = ProjectId
            projectSalesOrder.ActivityLog = New ActivityLog


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

        'Try

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtProject.Text = String.Empty Then
                ShowErrorMessage("Please enter Project Title")
                Me.txtProject.Focus()
                Return False
            End If
            'If Me.txtCode.Text = String.Empty Then
            '    ShowErrorMessage("Please enter Code")
            '    Me.txtCode.Focus()
            '    Return False
            'End If
            'If Me.txtPlant.Text = String.Empty Then
            '    ShowErrorMessage("Please enter Plant")
            '    Me.txtPlant.Focus()
            '    Return False
            'End If
            'If Me.txtScope.Text = String.Empty Then
            '    ShowErrorMessage("Please enter Scope")
            '    Me.txtScope.Focus()
            '    Return False
            'End If
            'If Me.txtAddress.Text = String.Empty Then
            '    ShowErrorMessage("Please enter Address")
            '    Me.txtAddress.Focus()
            '    Return False
            'End If
            'If cmbRegion.SelectedValue = 0 Then
            '    ShowErrorMessage("Please select any Region")
            '    cmbRegion.Focus()
            '    Return False
            'End If
            'If cmbCity.SelectedValue = 0 Then
            '    ShowErrorMessage("Please select any City")
            '    cmbCity.Focus()
            '    Return False
            'End If
            'If Me.txtProduct.Text = String.Empty Then
            '    ShowErrorMessage("Please enter Product")
            '    Me.txtAddress.Focus()
            '    Return False
            'End If
            If cmbProjectStatus.SelectedValue = 0 Then
                ShowErrorMessage("Please select any Status")
                cmbProjectStatus.Focus()
                Return False
            End If
            If cmbProjectCategory.SelectedValue = 0 Then
                ShowErrorMessage("Please select any Category")
                cmbProjectCategory.Focus()
                Return False
            End If
            If cmbLead.SelectedValue = 0 Then
                ShowErrorMessage("Please select any Lead")
                cmbLead.Focus()
                Return False
            End If
            'If cmbMainContactPerson.SelectedValue = 0 Then
            '    ShowErrorMessage("Please select any Contact Person")
            '    cmbMainContactPerson.Focus()
            '    Return False
            'End If
            If cmbEngineeringConsultant.SelectedValue = 0 Then
                ShowErrorMessage("Please select any Engineering Consultant")
                cmbEngineeringConsultant.Focus()
                Return False
            End If
            If cmbContractAward.SelectedValue = 0 Then
                ShowErrorMessage("Please select any Contract Award")
                cmbContractAward.Focus()
                Return False
            End If
            'If Me.cmbResponsible.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any responsible person")
            '    cmbResponsible.Focus()
            '    Return False
            'End If
            'If Me.cmbInsideSales.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any Inside person")
            '    cmbInsideSales.Focus()
            '    Return False
            'End If
            'If Me.cmbManager.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any Manager")
            '    cmbInsideSales.Focus()
            '    Return False
            'End If
            FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtProject.Text = ""
            Me.txtCode.Text = ""
            Me.txtPlant.Text = ""
            Me.txtScope.Text = ""
            Me.txtAddress.Text = ""
            Me.richProjectDetail.Text = ""

            cmbRegion.SelectedValue = 0
            cmbCity.SelectedIndex = 0
            txtProduct.Text = ""
            cmbProjectStatus.SelectedIndex = 0
            cmbProjectCategory.SelectedIndex = 0
            cmbLead.SelectedIndex = 0
            cmbMainContactPerson.SelectedIndex = 0
            cmbEngineeringConsultant.SelectedIndex = 0
            cmbContractAward.SelectedIndex = 0
            cmbResponsible.SelectedIndex = 0
            cmbInsideSales.SelectedIndex = 0
            cmbManager.SelectedIndex = 0

            cmbQuotation.SelectedIndex = 0
            cmbSalesOrder.SelectedIndex = 0

            

        Catch ex As Exception
            Throw
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
            frmProjectList.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefCRMProject_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
                frmProjectList.GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefCRMProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
            
            If ProjectId > 0 Then
                btnAddQuotation.Enabled = True
                btnAddSalesOrder.Enabled = True
            Else
                btnAddQuotation.Enabled = False
                btnAddSalesOrder.Enabled = False
            End If


            FillCombos("Region")
            FillCombos("City")
            FillCombos("Status")
            FillCombos("ProjectCategory")
            FillCombos("Lead")
            FillCombos("Contact")
            FillCombos("Consultant")
            FillCombos("ContractAward")
            FillCombos("Responsible")
            FillCombos("Inside")
            FillCombos("Manager")
            FillCombos("Quotation")
            FillCombos("SalesOrder")
            Dim dt As DataTable = New ProjectDefDAL().GetById(ProjectId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtProject.Text = dt.Rows(0).Item("ProjectTitle")
                    Me.txtCode.Text = dt.Rows(0).Item("ProjectCode")
                    Me.txtPlant.Text = dt.Rows(0).Item("Plant")
                    Me.txtScope.Text = dt.Rows(0).Item("Scope")
                    Me.txtAddress.Text = dt.Rows(0).Item("Address")
                    Me.richProjectDetail.Text = dt.Rows(0).Item("Details")

                    cmbRegion.SelectedValue = dt.Rows(0).Item("RegionId")
                    cmbCity.SelectedValue = dt.Rows(0).Item("CityId")
                    txtProduct.Text = dt.Rows(0).Item("Products")
                    cmbProjectStatus.SelectedValue = dt.Rows(0).Item("ProjectStatusId")
                    cmbProjectCategory.SelectedValue = dt.Rows(0).Item("ProjectCategoryId")
                    cmbLead.SelectedValue = dt.Rows(0).Item("LeadProfileId")
                    cmbMainContactPerson.SelectedValue = dt.Rows(0).Item("ContactPersonId")
                    cmbEngineeringConsultant.SelectedValue = dt.Rows(0).Item("EngineeringConsultantId")
                    cmbContractAward.SelectedValue = dt.Rows(0).Item("ContractAwardedId")
                    cmbResponsible.SelectedValue = dt.Rows(0).Item("ResponsiblePersonId")
                    cmbInsideSales.SelectedValue = dt.Rows(0).Item("InsideSalesPersonId")
                    cmbManager.SelectedValue = dt.Rows(0).Item("ManagerId")

                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If

            'Project Quotation
            Dim dt1 As DataTable = New ProjectQuotationsDAL().GetById(ProjectQuotaionId)
            Dim j As Integer
            If dt1.Rows.Count > 0 Then
                For j = 0 To dt1.Rows.Count - 1
                    cmbQuotation.SelectedValue = dt.Rows(0).Item("QuotationId")
                    'btnAddQuotation.Enabled = DoHaveUpdateRights
                Next
            Else
                cmbQuotation.SelectedIndex = 0
                'btnAddQuotation.Enabled = DoHaveSaveRights
            End If

            'Project sales order
            Dim dt2 As DataTable = New ProjectSalesOrderDAL().GetById(projectSalesOrderId)
            Dim k As Integer
            If dt1.Rows.Count > 0 Then
                For k = 0 To dt1.Rows.Count - 1
                    cmbSalesOrder.SelectedValue = dt.Rows(0).Item("SalesOrderId")
                    'btnAddQuotation.Enabled = DoHaveUpdateRights
                Next
            Else
                cmbSalesOrder.SelectedIndex = 0
                'btnAddQuotation.Enabled = DoHaveSaveRights
            End If



            GetProjectActivities()

            GetProjectQuotations()
            GetProjectSalesOrder()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            Me.TabControl1.SelectedIndex = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If ProjectId = 0 Then
                    If projectDefDal.Add(project) Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                        Me.Close()
                        frmProjectList.FillCombos()
                    End If
                Else
                    If projectDefDal.Update(project) Then
                        msg_Information("Record has been updated successfully.")
                        ReSetControls()
                        Me.Close()
                        frmProjectList.FillCombos()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GetProjectActivities()
        Try
            Dim str As String
            str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId,  LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle,tblDefProject.ProjectTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as ResponsibleManager, tblDefEmployee.Employee_Name AS InsideSales, tblDefEmployee_1.Employee_Name AS ManagerInsideSales, LeadActivity.IsConfirmed FROM LeadActivity RIGHT OUTER JOIN tblDefProject ON LeadActivity.ProjectId = tblDefProject.ProjectId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId where LeadActivity.ProjectId = '" & ProjectId & "'  order by 1 Desc"

                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdActivities.DataSource = dt
                Me.grdActivities.RetrieveStructure()
                ApplyGridSettings()
            completedActivities()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    
    Private Sub btnAddQuotation_Click(sender As Object, e As EventArgs) Handles btnAddQuotation.Click
        Try

            If IsValidate() = True Then
                If ProjectId > 0 Then
                    Dim str As String
                    str = "select * from tblDefProjectQuotations where QuotationId = '" & cmbQuotation.SelectedValue & "' AND ProjectId   = '" & ProjectId & "'"
                    Dim dt2 As DataTable
                    dt2 = GetDataTable(str)
                    If dt2.Rows.Count > 0 Then
                        ShowErrorMessage("This Quotation is already added against this Project")
                    Else
                        If projectQuottionDefDal.Add(projectQuotation) Then
                            msg_Information("Record has been saved successfully.")
                            ReSetControls()
                            GetProjectQuotations()
                            Me.Close()
                        End If
                    End If
                Else
                    ShowErrorMessage("Please select project before adding Quotations")
                End If
            End If
                

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetProjectQuotations()
        Try
            If ProjectId > 0 Then
                Dim str As String
                str = " SELECT        tblDefProjectQuotations.ProjectQuotaionID, tblDefProject.ProjectTitle, QuotationMasterTable.QuotationNo, QuotationMasterTable.QuotationDate, QuotationMasterTable.PartyInvoiceNo, QuotationMasterTable.PartySlipNo,  " & _
                     " QuotationMasterTable.SalesOrderQty, QuotationMasterTable.SalesOrderAmount, QuotationMasterTable.CashPaid, QuotationMasterTable.Remarks, QuotationMasterTable.UserName, QuotationMasterTable.Status, QuotationMasterTable.Delivery_Date, QuotationMasterTable.Terms_And_Condition " & _
                      " FROM            tblDefProjectQuotations INNER JOIN QuotationMasterTable ON tblDefProjectQuotations.QuotationId = QuotationMasterTable.QuotationId INNER JOIN tblDefProject ON tblDefProjectQuotations.ProjectId = tblDefProject.ProjectId " & _
                      " where (tblDefProject.ProjectId = '" & ProjectId & "' ) order by 1 Desc"
                'Dim StatusCount As Integer
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdQuotations.DataSource = dt
                'StatusCount = dt.Rows.Count
                'tbQuotation.Text = "Quotations (" + StatusCount.ToString() + ")"
                Me.grdQuotations.RetrieveStructure()


                ApplyGridSettings()
                Me.grdQuotations.RootTable.Columns("ProjectQuotaionID").Visible = False
            Else
                Dim str As String
                str = " SELECT        tblDefProjectQuotations.ProjectQuotaionID, tblDefProject.ProjectTitle, QuotationMasterTable.QuotationNo, QuotationMasterTable.QuotationDate, QuotationMasterTable.PartyInvoiceNo, QuotationMasterTable.PartySlipNo,  " & _
                     " QuotationMasterTable.SalesOrderQty, QuotationMasterTable.SalesOrderAmount, QuotationMasterTable.CashPaid, QuotationMasterTable.Remarks, QuotationMasterTable.UserName, QuotationMasterTable.Status, QuotationMasterTable.Delivery_Date, QuotationMasterTable.Terms_And_Condition " & _
                      " FROM            tblDefProjectQuotations INNER JOIN QuotationMasterTable ON tblDefProjectQuotations.QuotationId = QuotationMasterTable.QuotationId INNER JOIN tblDefProject ON tblDefProjectQuotations.ProjectId = tblDefProject.ProjectId " & _
                      " where (tblDefProject.ProjectId = '-1' ) order by 1 Desc"
                'Dim StatusCount As Integer
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdQuotations.DataSource = dt
                'StatusCount = dt.Rows.Count
                'tbQuotation.Text = "Quotations (" + StatusCount.ToString() + ")"
                Me.grdQuotations.RetrieveStructure()


                ApplyGridSettings()
                Me.grdQuotations.RootTable.Columns("ProjectQuotaionID").Visible = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAddSalesOrder_Click(sender As Object, e As EventArgs) Handles btnAddSalesOrder.Click
        Try

            If IsValidate() = True Then
                If ProjectId > 0 Then
                    Dim str As String
                    str = "select * from tblProjectSalesOrder where SalesOrderId = '" & cmbSalesOrder.SelectedValue & "' AND ProjectId   = '" & ProjectId & "'"
                    Dim dt3 As DataTable
                    dt3 = GetDataTable(str)
                    If dt3.Rows.Count > 0 Then
                        ShowErrorMessage("This Sales order is already added against this Project")
                    Else
                        If ProjectSalesOrderDal.Add(projectSalesOrder) Then
                            msg_Information("Record has been saved successfully.")
                            ReSetControls()
                            GetProjectSalesOrder()
                            Me.Close()
                        End If
                    End If
                Else
                    ShowErrorMessage("Please Select Project before adding Sale Order")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetProjectSalesOrder()
        Try
            If ProjectId > 0 Then
                Dim str As String
                str = "SELECT        tblProjectSalesOrder.ProjectSalesOrderID, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, SalesOrderMasterTable.SalesOrderQty, SalesOrderMasterTable.SalesOrderAmount, " & _
                     "SalesOrderMasterTable.Remarks, SalesOrderMasterTable.Status, tblDefProject.ProjectTitle FROM tblProjectSalesOrder INNER JOIN" & _
                      " tblDefProject ON tblProjectSalesOrder.ProjectId = tblDefProject.ProjectId INNER JOIN SalesOrderMasterTable ON tblProjectSalesOrder.SalesOrderId = SalesOrderMasterTable.SalesOrderId " & _
                      " WHERE   (tblDefProject.ProjectId = '" & ProjectId & "') order by 1 Desc "
                'Dim StatusCount As Integer
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdSalesOrder.DataSource = dt
                'StatusCount = dt.Rows.Count
                'tbSalesOrder.Text = "Sales Order (" + StatusCount.ToString() + ")"
                Me.grdSalesOrder.RetrieveStructure()

                ApplyGridSettings()
                Me.grdSalesOrder.RootTable.Columns("ProjectSalesOrderID").Visible = False
            Else
                Dim str As String
                str = "SELECT        tblProjectSalesOrder.ProjectSalesOrderID, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, SalesOrderMasterTable.SalesOrderQty, SalesOrderMasterTable.SalesOrderAmount, " & _
                     "SalesOrderMasterTable.Remarks, SalesOrderMasterTable.Status, tblDefProject.ProjectTitle FROM tblProjectSalesOrder INNER JOIN" & _
                      " tblDefProject ON tblProjectSalesOrder.ProjectId = tblDefProject.ProjectId INNER JOIN SalesOrderMasterTable ON tblProjectSalesOrder.SalesOrderId = SalesOrderMasterTable.SalesOrderId " & _
                      " WHERE   (tblDefProject.ProjectId = '-1') order by 1 Desc "
                'Dim StatusCount As Integer
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdSalesOrder.DataSource = dt
                'StatusCount = dt.Rows.Count
                'tbSalesOrder.Text = "Sales Order (" + StatusCount.ToString() + ")"
                Me.grdSalesOrder.RetrieveStructure()

                ApplyGridSettings()
                Me.grdSalesOrder.RootTable.Columns("ProjectSalesOrderID").Visible = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub grdQuotations_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdQuotations.ColumnButtonClick
        Try
            projectQuotation = New ProjectQuotationsBE
            projectQuottionDefDal = New ProjectQuotationsDAL
            projectQuotation.ProjectQuotaionID = Val(Me.grdQuotations.CurrentRow.Cells("ProjectQuotaionID").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                'If DoHaveDeleteRights = True Then
                projectQuottionDefDal.Delete(projectQuotation)
                Me.grdQuotations.GetRow.Delete()
                SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                'Else
                '    msg_Information("You do not have delete rights.")
                'End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSalesOrder_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSalesOrder.ColumnButtonClick
        Try
            projectSalesOrder = New ProjectSalesOrderBE
            ProjectSalesOrderDal = New ProjectSalesOrderDAL
            projectSalesOrder.ProjectSalesOrderID = Val(Me.grdSalesOrder.CurrentRow.Cells("ProjectSalesOrderID").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                'If DoHaveDeleteRights = True Then
                ProjectSalesOrderDal.Delete(projectSalesOrder)
                Me.grdSalesOrder.GetRow.Delete()
                SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                'Else
                '    msg_Information("You do not have delete rights.")
                'End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub completedActivities()

        Dim str1 As String
        Dim StatusCount As Integer
        'str1 = "select Count(ProjectStatusId) As countStatus from tblDefProject where ProjectStatusId = 1 AND ProjectId = '" & ProjectId & "'"
        str1 = "SELECT LeadActivity.ProjectId, ActivityFeedbackStatus.Status FROM LeadActivity LEFT OUTER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId RIGHT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId Where ActivityFeedbackStatus.Status = 'Complete' And LeadActivity.ProjectId = '" & ProjectId & "'"
        Dim dt1 As DataTable
        dt1 = GetDataTable(str1)
        StatusCount = dt1.Rows.Count
        tbActivities.Text = "Activities (" + StatusCount.ToString() + ")"

    End Sub

    Private Sub cmbLead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLead.SelectedIndexChanged
        Try
            GetInfo(cmbLead.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function GetInfo(ByVal ID As Integer)
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT ResponsibleId, InsideSalesId, ManagerId from LeadProfile where LeadId = " & ID & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                cmbResponsible.SelectedValue = dt.Rows(0).Item("ResponsibleId")
                cmbInsideSales.SelectedValue = dt.Rows(0).Item("InsideSalesId")
                cmbManager.SelectedValue = dt.Rows(0).Item("ManagerId")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
