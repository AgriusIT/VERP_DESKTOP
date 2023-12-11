Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class frmSiteRegistration

    Implements IGeneral
    Enum enmGrdDetail
        coa_detail_id
        detail_code
        detail_title
        Amount
        Tenure_Start_Date
        Tenure_End_Date
        Payee_Name
        Payment_Term
        Remarks
    End Enum
    Dim SiteReg As SiteRegisrationBE
    Dim SiteRegistrationId As Integer = 0I
    Dim IsOpendForm As Boolean = False
    Dim GetTotalRecords As String = String.Empty

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            If Condition = "Master" Then
                For c As Integer = 0 To Me.grdSave.RootTable.Columns.Count - 1
                    Me.grdSave.RootTable.Columns(c).Visible = False
                Next

                Me.grdSave.RootTable.Columns("Registration_No").Visible = True
                Me.grdSave.RootTable.Columns("Registration_Date").Visible = True
                Me.grdSave.RootTable.Columns("Region").Visible = True
                Me.grdSave.RootTable.Columns("City").Visible = True
                Me.grdSave.RootTable.Columns("Area").Visible = True
                Me.grdSave.RootTable.Columns("Location").Visible = True
                Me.grdSave.RootTable.Columns("Size_Width").Visible = True
                Me.grdSave.RootTable.Columns("Size_Height").Visible = True
                Me.grdSave.RootTable.Columns("Longitude").Visible = True
                Me.grdSave.RootTable.Columns("Latitude").Visible = True
                Me.grdSave.RootTable.Columns("Authority").Visible = True
            ElseIf Condition = "Detail" Then

                For c As Integer = 0 To Me.grdDetail.RootTable.Columns.Count - 1
                    If Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Amount AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Tenure_Start_Date AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Tenure_End_Date AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Payee_Name AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Payment_Term AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Remarks Then
                        Me.grdDetail.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            End If


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
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerPlanning)
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

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
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
            SiteReg = New SiteRegisrationBE
            SiteReg.SiteRegistrationID = Me.grdSave.GetRow.Cells("SiteRegistrationId").Value.ToString
            SiteReg.Registration_No = Me.grdSave.GetRow.Cells("Registration_No").Value.ToString
            If New SiteRegistrationDAL().Delete(SiteReg) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Region" Then
                FillDropDown(Me.cmbRegion, "Select StateID, StateName From tblListState ORDER BY 2 ASC")
            ElseIf Condition = "City" Then
                FillDropDown(Me.cmbCity, "Select CityId, CityName From tblListCity WHERE StateId=" & IIf(Me.cmbRegion.SelectedIndex > 0, Me.cmbRegion.SelectedValue, 0) & " ORDER BY 2 ASC")
            ElseIf Condition = "Area" Then
                FillDropDown(Me.cmbArea, "Select TerritoryId, TerritoryName From tblListTerritory WHERE CityId=" & IIf(Me.cmbCity.SelectedIndex > 0, Me.cmbCity.SelectedValue, 0) & "   ORDER BY 2 ASC")
            ElseIf Condition = "Project" Then
                FillDropDown(Me.cmbProject, "SELECT CostCenterID, Name AS CostCenter, CostCenterGroup FROM dbo.tblDefCostCenter ORDER BY Name")
            ElseIf Condition = "CostAccount" Then
                FillUltraDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title as [Account Description], detail_code as [Account Code] from vwCOADetail WHERE detail_title <> '' ORDER BY 2 ASC")
                Me.cmbAccounts.Rows(0).Activate()
                If Me.cmbAccounts.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).Width = 300
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(2).Width = 150
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            SiteReg = New SiteRegisrationBE
            SiteReg.Area_Category = Me.cmbAreaCategory.Text
            SiteReg.Area_ID = Me.cmbArea.SelectedValue
            SiteReg.Authority = Me.txtAuthority.Text
            SiteReg.Availability_Date = Me.dtpVisibilityDate.Value
            SiteReg.Bank_Ac_No1 = Me.txtBankAccount1.Text
            SiteReg.Bank_Ac_No2 = Me.txtBankAccount2.Text
            SiteReg.CityID = Me.cmbCity.SelectedValue
            SiteReg.Clutter_Info = Me.cmbClutterInfo.Text
            SiteReg.EntryDate = Now
            SiteReg.Latitude = Me.txtLatitude.Text
            SiteReg.Location = Me.txtLocation.Text
            SiteReg.Longitude = Me.txtLongitude.Text
            SiteReg.Owner_Address = Me.txtAddress.Text
            SiteReg.Owner_CNIC_No = Me.txtCNICNumber.Text
            SiteReg.Owner_Name = Me.txtOwnerName.Text
            SiteReg.RA = Me.txtRA.Text
            SiteReg.Region_ID = Me.cmbRegion.SelectedValue
            SiteReg.Registration_Date = Me.dtpDocumentDate.Value
            SiteReg.Registration_No = Me.txtDocumentNo.Text
            SiteReg.Sided = Me.cmbSided.Text
            SiteReg.Singnal_Info = Me.cmbSignalInfo.Text
            SiteReg.Site_Type = Me.cmbSiteType.Text
            SiteReg.SiteRegistrationID = SiteRegistrationId
            SiteReg.Size_Height = Me.txtHeight.Text
            SiteReg.Size_Width = Me.txtWidth.Text
            SiteReg.Traffic_Coming_From = Me.txtComingFrom.Text
            SiteReg.Traffic_Going_To = Me.txtGoingTo.Text
            SiteReg.Traffic_Per_Day = Me.txtTraficCountPerDay.Text
            SiteReg.UserId = LoginUserId
            SiteReg.UserName = LoginUserName
            SiteReg.Visibility_Distance = Val(Me.txtVisibilityDistance.Text)
            SiteReg.ProjectId = Me.cmbProject.SelectedValue
            SiteReg.SiteRegistrationCostDetail = New List(Of SiteRegistrationCostDetailBE)
            Dim SiteRegCost As SiteRegistrationCostDetailBE
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                SiteRegCost = New SiteRegistrationCostDetailBE
                SiteRegCost.coa_detail_id = Val(objRow.Cells(enmGrdDetail.coa_detail_id).Value.ToString)
                SiteRegCost.Amount = Val(objRow.Cells(enmGrdDetail.Amount).Value.ToString)
                SiteRegCost.Tenure_Start = objRow.Cells(enmGrdDetail.Tenure_Start_Date).Value
                SiteRegCost.Tenure_End = objRow.Cells(enmGrdDetail.Tenure_End_Date).Value
                SiteRegCost.Payee_Name = objRow.Cells(enmGrdDetail.Payee_Name).Value.ToString
                SiteRegCost.Payment_Term = objRow.Cells(enmGrdDetail.Payment_Term).Value.ToString
                SiteRegCost.Remarks = objRow.Cells(enmGrdDetail.Remarks).Value.ToString
                SiteRegCost.SiteRegistrationId = SiteRegistrationId
                SiteReg.SiteRegistrationCostDetail.Add(SiteRegCost)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Dim objdt As New DataTable
                objdt = New SiteRegistrationDAL().GetAllRecords(GetTotalRecords)
                Me.grdSave.DataSource = objdt
                Me.grdSave.RetrieveStructure()
                Me.grdSave.AutoSizeColumns()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then
                Dim objDtDetail As New DataTable
                If SiteRegistrationId > 0 Then
                    objDtDetail = New SiteRegistrationDAL().GetDetailRecord(SiteRegistrationId)
                Else
                    objDtDetail = New SiteRegistrationDAL().GetDetailMaxRecord
                End If
                objDtDetail.AcceptChanges()
                Me.grdDetail.DataSource = objDtDetail
                Me.grdDetail.AutoSizeColumns()
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbProject.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select project.")
                Me.cmbProject.Focus()
                Return False
            End If
            If Me.cmbRegion.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select region.")
                Me.cmbRegion.Focus()
                Return False
            End If
            If Me.cmbCity.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select city.")
                Me.cmbCity.Focus()
                Return False
            End If
            If Me.cmbArea.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select area.")
                Me.cmbArea.Focus()
                Return False
            End If
            If Me.txtLocation.Text = String.Empty Then
                ShowErrorMessage("Please enter location")
                Me.txtLocation.Focus()
                Return False
            End If
            If Me.grdDetail.RowCount <= 0 Then
                ShowErrorMessage("There is no record in grid")
                Me.grdDetail.Focus()
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

            Me.btnSave.Text = "&Save"
            SiteRegistrationId = 0I
            Me.txtDocumentNo.Text = GetNextDocNo("ST-" & Me.dtpDocumentDate.Value.ToString("MMyy") & "", 3, "SiteRegistrationTable", "Registration_No")
            Me.dtpDocumentDate.Value = Now
            If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            If Not Me.cmbRegion.SelectedIndex = -1 Then Me.cmbRegion.SelectedIndex = 0
            If Not Me.cmbCity.SelectedIndex = -1 Then Me.cmbCity.SelectedIndex = 0
            If Not Me.cmbArea.SelectedIndex = -1 Then Me.cmbArea.SelectedIndex = 0
            Me.txtLocation.Text = String.Empty
            Me.cmbAreaCategory.SelectedIndex = 0
            Me.cmbSiteType.SelectedIndex = 0
            Me.cmbClutterInfo.SelectedIndex = 0
            Me.cmbSignalInfo.SelectedIndex = 0
            Me.txtVisibilityDistance.Text = String.Empty
            Me.txtComingFrom.Text = String.Empty
            Me.txtGoingTo.Text = String.Empty
            Me.txtTraficCountPerDay.Text = String.Empty
            Me.dtpVisibilityDate.Value = Now
            Me.txtWidth.Text = String.Empty
            Me.txtHeight.Text = String.Empty
            Me.txtLongitude.Text = String.Empty
            Me.txtLatitude.Text = String.Empty
            Me.cmbSided.SelectedIndex = 0
            Me.txtAuthority.Text = String.Empty
            Me.txtRA.Text = String.Empty
            Me.txtOwnerName.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtCNICNumber.Text = String.Empty
            Me.txtBankAccount1.Text = String.Empty
            Me.txtBankAccount2.Text = String.Empty
            GetTotalRecords = String.Empty
            GetAllRecords("Master")
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(Utility.EnumDataMode.[New])


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New SiteRegistrationDAL().Save(SiteReg) = True Then
                Return True
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
            If New SiteRegistrationDAL().Update(SiteReg) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmSiteRegistration_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSiteRegistration_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos("Project")
            FillCombos("Region")
            FillCombos("City")
            FillCombos("Area")
            FillCombos("CostAccount")
            IsOpendForm = True
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            If IsOpendForm = True Then FillCombos("City")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbCity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCity.SelectedIndexChanged
        Try
            If IsOpendForm = True Then FillCombos("Area")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            GetTotalRecords = "All"
            GetAllRecords("Master")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbProject.SelectedIndex
            FillCombos("Project")
            Me.cmbProject.SelectedIndex = id
            id = Me.cmbRegion.SelectedIndex
            FillCombos("Region")
            Me.cmbRegion.SelectedIndex = id
            id = Me.cmbCity.SelectedIndex
            FillCombos("City")
            Me.cmbCity.SelectedIndex = id
            id = Me.cmbArea.SelectedIndex
            FillCombos("Area")
            Me.cmbArea.SelectedIndex = id
            id = Me.cmbAccounts.ActiveRow.Cells(0).Value
            FillCombos("CostAccount")
            Me.cmbAccounts.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try

            If Me.grdSave.RowCount = 0 Then Exit Sub
            If Me.grdDetail.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            SiteRegistrationId = Val(Me.grdSave.GetRow.Cells("SiteRegistrationId").Value.ToString)
            Me.cmbAreaCategory.Text = Me.grdSave.GetRow.Cells("Area_Category").Value.ToString
            Me.txtAuthority.Text = Me.grdSave.GetRow.Cells("Authority").Value.ToString
            Me.dtpVisibilityDate.Value = Me.grdSave.GetRow.Cells("Availability_Date").Value.ToString
            Me.txtBankAccount1.Text = Me.grdSave.GetRow.Cells("BankAc1").Value.ToString
            Me.txtBankAccount2.Text = Me.grdSave.GetRow.Cells("BankAc2").Value.ToString
            Me.cmbRegion.SelectedValue = Me.grdSave.GetRow.Cells("Region_Id").Value.ToString
            Me.cmbCity.SelectedValue = Me.grdSave.GetRow.Cells("CityID").Value.ToString
            Me.cmbArea.SelectedValue = Me.grdSave.GetRow.Cells("Area_ID").Value.ToString
            Me.cmbClutterInfo.Text = Me.grdSave.GetRow.Cells("Clutter_Info").Value.ToString
            Me.txtLatitude.Text = Me.grdSave.GetRow.Cells("Latitude").Value.ToString
            Me.txtLocation.Text = Me.grdSave.GetRow.Cells("Location").Value.ToString
            Me.txtLongitude.Text = Me.grdSave.GetRow.Cells("Longitude").Value.ToString
            Me.txtAddress.Text = Me.grdSave.GetRow.Cells("Owner_Address").Value.ToString
            Me.txtCNICNumber.Text = Me.grdSave.GetRow.Cells("Owner_CNIC_No").Value.ToString
            Me.txtOwnerName.Text = Me.grdSave.GetRow.Cells("Owner_Name").Value.ToString
            Me.txtRA.Text = Me.grdSave.GetRow.Cells("RA").Value.ToString
            Me.dtpDocumentDate.Value = Me.grdSave.GetRow.Cells("Registration_Date").Value.ToString
            Me.txtDocumentNo.Text = Me.grdSave.GetRow.Cells("Registration_No").Value.ToString
            Me.cmbSided.Text = Me.grdSave.GetRow.Cells("Sided").Value.ToString
            Me.cmbSignalInfo.Text = Me.grdSave.GetRow.Cells("Singnal_Info").Value.ToString
            Me.cmbSiteType.Text = Me.grdSave.GetRow.Cells("Site_Type").Value.ToString
            Me.txtHeight.Text = Me.grdSave.GetRow.Cells("Size_Height").Value.ToString
            Me.txtWidth.Text = Me.grdSave.GetRow.Cells("Size_Width").Value.ToString
            Me.txtComingFrom.Text = Me.grdSave.GetRow.Cells("Traffic_Coming_From").Value.ToString
            Me.txtGoingTo.Text = Me.grdSave.GetRow.Cells("Traffic_Going_To").Value.ToString
            Me.txtTraficCountPerDay.Text = Me.grdSave.GetRow.Cells("Traffic_Per_Day").Value.ToString
            Me.cmbProject.SelectedValue = Val(Me.grdSave.GetRow.Cells("ProjectId").Value.ToString)
            GetAllRecords("Detail")
            ApplySecurity(Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.dtpDocumentDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try

            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            If Val(Me.txtAmount.Text) < 0 Then Exit Sub
            If Me.cmbAccounts.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select account.")
                Me.cmbAccounts.Focus()
                Exit Sub
            End If
            If Val(Me.txtAmount.Text) = 0 Then
                ShowErrorMessage("Please enter amount.")
                Me.txtAmount.Focus()
                Exit Sub
            End If
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            Dim drFound() As DataRow
            drFound = dt.Select("coa_detail_id=" & Me.cmbAccounts.Value & "")
            If drFound IsNot Nothing Then
                If drFound.Length > 0 Then
                    ShowErrorMessage("This account is already exists.")
                    Me.cmbAccounts.Focus()
                    Exit Sub
                End If
            End If
            Dim dr As DataRow
            dr = dt.NewRow
            dr(enmGrdDetail.coa_detail_id) = Me.cmbAccounts.Value
            dr(enmGrdDetail.detail_code) = Me.cmbAccounts.ActiveRow.Cells("Account Code").Text
            dr(enmGrdDetail.detail_title) = Me.cmbAccounts.ActiveRow.Cells("Account Description").Text
            dr(enmGrdDetail.Amount) = Val(Me.txtAmount.Text)
            dr(enmGrdDetail.Tenure_Start_Date) = Me.dtpTenureStartDate.Value
            dr(enmGrdDetail.Tenure_End_Date) = Me.dtpTenureEndDate.Value
            dr(enmGrdDetail.Payee_Name) = String.Empty
            dr(enmGrdDetail.Payment_Term) = String.Empty
            dr(enmGrdDetail.Remarks) = String.Empty
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Me.txtAmount.Text = String.Empty
            Me.cmbAccounts.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.cmbAccounts.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSave_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSave.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDetail_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.grdDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtVisibilityDistance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVisibilityDistance.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeight.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWidth.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtLatitude_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLatitude.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtLongitude_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLongitude.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSave.RowCount = 0 Then Exit Sub
            AddRptParam("@RegistrationId", Me.grdSave.GetRow.Cells("Registration_Id").Value.ToString)
            ShowReport("rptSiteRegistration")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

