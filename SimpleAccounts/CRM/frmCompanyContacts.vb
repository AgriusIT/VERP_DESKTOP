Imports SBModel
Imports SBDal
Imports System.Text.RegularExpressions
Imports System.Data.OleDb

Public Class frmCompanyContacts
    Dim CompConatact As CompanyContactBE
    Dim isLoaded As Boolean = False
    Public ReferenceID As Integer = 0
    Dim Pk_Id As Integer = 0

    Private Sub RefreshControls()
        Try

            ' Me.cmbType.Text = "Customer"

            'If ReferenceID > 0 Then
            '    Me.cmbReference.Value = ReferenceID
            'Else
            '    Me.cmbReference.Rows(0).Activate()
            'End If

            Pk_Id = 0
            Me.txtCompany.Text = String.Empty
            Me.txtContactName.Text = String.Empty
            Me.txtIndexNo.Text = GetIndexNo()
            Me.txtMobNo.Text = String.Empty
            Me.txtPhoneNo.Text = String.Empty
            Me.txtFaxNo.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtAddress.Text = String.Empty

            'FillCombo("VendorCustomer")

            FillCombo("Designation")
            FillCombo("Departments")
            FillCombo("Locations")
            'FillCombo("ContactGroup")

            Me.cmbDesignation.Text = String.Empty
            Me.cmbDepartment.Text = String.Empty
            'Me.cmbLocation.Text = String.Empty
            Me.cmbNamePrefix.SelectedIndex = 0

            Me.txtCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Dim dataCollection As New AutoCompleteStringCollection()
            AddItems(dataCollection)
            Me.txtCompany.AutoCompleteCustomSource = dataCollection
            Me.txtCompany.AutoCompleteSource = AutoCompleteSource.CustomSource

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.btnSave.Text = "&Save"
            GetAllRecords()
            GetSecurityRights()


        Catch ex As Exception

            Throw ex
        End Try

    End Sub

    Private Function GetIndexNo() As Integer
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trans = Con.BeginTransaction()
        Dim IndexNo As Integer = 0I

        Try

            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text


            Dim strSQL As String = String.Empty
            strSQL = "select Isnull(Max(IsNull(indexNo,0)),0) + 1 as IndexNo from TblCompanyContacts"
            cmd.CommandText = strSQL
            IndexNo = Convert.ToInt32(cmd.ExecuteScalar())

            trans.Commit()
            Return IndexNo
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Sub GetCompanyContacts()
        Try

            Me.grdContactList.DataSource = GetDataTable("select ContactName, Designation, Mobile from TblCompanyContacts where RefCompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value)
            Me.grdContactList.RetrieveStructure()
            Me.grdContactList.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAllRecords()
        Try
            Dim dt As New DataTable
            dt = New CompanyContactDAL().GetAllRecords()
            dt.AcceptChanges()
            Me.GrdStatus.DataSource = dt
            Me.GrdStatus.RetrieveStructure()

            Me.GrdStatus.RootTable.Columns("PK_Id").Visible = False
            Me.GrdStatus.RootTable.Columns("RefCompanyId").Visible = False
            Me.GrdStatus.RootTable.Columns("IndexNo").Visible = False
            Me.GrdStatus.RootTable.Columns("CompanyLocationId").Visible = False

            Me.GrdStatus.RootTable.Columns("detail_code").Caption = "Company Code"
            Me.GrdStatus.RootTable.Columns("detail_title").Caption = "Company"
            Me.GrdStatus.RootTable.Columns("Company").Caption = "Sub Company"
            Me.GrdStatus.RootTable.Columns("NamePrefix").Caption = "Title"

            Me.GrdStatus.RootTable.Columns("detail_code").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            Me.GrdStatus.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GrdStatus.AutoSizeColumns()

            GetCompanyContacts()
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.GrdStatus.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillModel()
        Try
            CompConatact = New CompanyContactBE
            CompConatact.RefCompanyId = Me.cmbReference.Value
            CompConatact.ContactName = Me.txtContactName.Text
            CompConatact.Designation = Me.cmbDesignation.Text
            CompConatact.Mobile = Me.txtMobNo.Text
            CompConatact.Phone = Me.txtPhoneNo.Text
            CompConatact.Fax = Me.txtFaxNo.Text
            CompConatact.Email = Me.txtEmail.Text
            CompConatact.Address = Me.txtAddress.Text
            CompConatact.IndexNo = Val(Me.txtIndexNo.Text)
            CompConatact.Type = Me.cmbType.Text
            CompConatact.Company = Me.txtCompany.Text
            CompConatact.Department = Me.cmbDepartment.Text
            CompConatact.NamePrefix = Me.cmbNamePrefix.Text
            CompConatact.CompanyLocation = Me.cmbLocation.Text
            CompConatact.CompanyLocationId = Val(Me.cmbCompanyLocations.ActiveRow.Cells(0).Value.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean
        Try
            If CompConatact Is Nothing Then Return False
            If New CompanyContactDAL().Add(CompConatact) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean
        Try
            If CompConatact Is Nothing Then Return False
            CompConatact.PK_ID = Me.GrdStatus.GetRow.Cells("PK_Id").Value

            If New CompanyContactDAL().Update(CompConatact) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If isValidate() = True Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then

                    If Save() = True Then
                        RefreshControls()
                        GetAllRecords()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        RefreshControls()
                        GetAllRecords()
                        Me.btnSave.Text = "&Save"
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function isValidate() As Boolean

        Try

            'If Me.cmbReference.Text = String.Empty Then
            '    ShowErrorMessage("Please Select the Company")
            '    Me.cmbReference.Focus()
            '    Return False
            'End If

          
            'If Not Me.cmbReference.Rows(0).Activate() = True Then
            '    ShowErrorMessage("Please select the Company")
            '    Me.cmbReference.Focus()
            '    Return False
            'End If


            If Me.txtContactName.Text = String.Empty Then
                ShowErrorMessage("Please enter the contact name")
                Me.txtContactName.Focus()
                Return False
            End If

            Dim emailAddressMatch As Match
            Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
            If Me.txtEmail.Text <> String.Empty Then

                emailAddressMatch = Regex.Match(Me.txtEmail.Text, pattern)
                If emailAddressMatch.Success = False Then
                    ShowErrorMessage("Please enter valid email")
                    Me.txtEmail.Focus()
                    Return False
                End If
            End If
            If Me.txtMobNo.Text.TrimStart.Length > 0 Then

                If GetDataTable("select * from TblCompanyContacts where Mobile like '%" & Me.txtMobNo.Text.TrimStart("0") & "%' and pk_id <> " & Pk_Id).Rows.Count > 0 Then
                    msg_Error("Contact already exist with this mobile number")
                    Me.txtContactName.Focus()
                    Return False
                End If
            End If

            FillModel()
            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.GrdStatus.Row < 0 Then
                Exit Sub
            End If
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            
            If Delete() = True Then
                RefreshControls()
                GetAllRecords()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean
        Try
            If Me.GrdStatus.RowCount = 0 Then Return False
            CompConatact = New CompanyContactBE
            CompConatact.PK_ID = Val(Me.GrdStatus.GetRow.Cells("PK_Id").Value.ToString)
            If New CompanyContactDAL().Delete(CompConatact) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'RefreshControls()
            'GetAllRecords()
            Dim id As Integer = 0I
            id = Me.cmbType.SelectedValue
            FillCombo("ContactGroup")
            Me.cmbType.SelectedValue = id

            id = Me.cmbReference.ActiveRow.Cells(0).Value
            FillCombo("VendorCustomer")
            Me.cmbReference.Value = id
            id = Me.cmbLocation.SelectedValue
            FillCombo("Locations")
            Me.cmbLocation.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCompanyContacts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            ElseIf e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCompanyContacts_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIndexNo.KeyPress
        Try
            If e.KeyChar <> ControlChars.Back Then
                e.Handled = ("0123456789".IndexOf(e.KeyChar) = -1)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    

    Private Sub frmCompanyContacts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.cmbType.Text = "Customer"
            FillCombo("ContactGroup")
            FillCombo("VendorCustomer")
            'FillCombo("Designation")
            'FillCombo("Departments")
            isLoaded = True
            RefreshControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            Dim strSQL = String.Empty
            'Customer()
            'Vendor()
            'Bank()
            If Condition = "VendorCustomer" Then
                If Me.cmbType.Text = "Customer" Or Me.cmbType.Text = "Vendor" Or Me.cmbType.Text = "Bank" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('" & Me.cmbType.Text & "') and  vwCOADetail.coa_detail_id is not  null"

                Else 'If Me.cmbType.Text = "Friends & Family" Or Me.cmbType.Text = "Others" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                    & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                    & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('" & Me.cmbType.Text & "') and  vwCOADetail.coa_detail_id is not  null" _
                    & " union all SELECT     tblCompanyContacts.RefCompanyId, tblCompanyContacts.ContactName, tblCompanyContacts.Type, tblCompanyContacts.Address," _
                    & " tblCompanyContacts.Mobile, tblCompanyContacts.Mobile,tblCompanyContacts.Email " _
                    & " FROM  tblCompanyContacts where RefCompanyId=0 and type in ('" & Me.cmbType.Text & "')"

                End If
                FillUltraDropDown(Me.cmbReference, strSQL)
                Me.cmbReference.Rows(0).Activate()
                Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "Designation" Then
                strSQL = String.Empty
                strSQL = "select Distinct Designation, Designation from tblCompanyContacts where Designation <> ''"
                FillDropDown(Me.cmbDesignation, strSQL, False)
            ElseIf Condition = "Departments" Then
                strSQL = String.Empty
                strSQL = "select Distinct Department,Department from tblCompanyContacts where Department <> ''"
                FillDropDown(Me.cmbDepartment, strSQL, False)
            ElseIf Condition = "Locations" Then
                strSQL = String.Empty
                strSQL = "Select Distinct CompanyLocation, CompanyLocation from tblCompanyContacts where CompanyLocation <> ''"
                'strSQL = "Select LocationId, LocationTitle From tblDefCompanyLocations"
                FillDropDown(Me.cmbLocation, strSQL)
                'If Me.cmbLocation.Items.Count > 1 Then
                '    Me.cmbLocation.SelectedValue = 0
                'End If
            ElseIf Condition = "ContactGroup" Then
                strSQL = String.Empty
                strSQL = "select GroupId, GroupName from tblDefContactGroups where Active='True'"
                FillDropDown(Me.cmbType, strSQL, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GrdStatus_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdStatus.DoubleClick
        Try
            If Me.GrdStatus.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecord()
        Try

            If Me.GrdStatus.RowCount = 0 Then Exit Sub

            'RemoveHandler cmbType.SelectedIndexChanged, AddressOf Me.cmbType_SelectedIndexChanged
            'If IsDBNull(Me.GrdStatus.CurrentRow.Cells("RefCompanyId").Value) Then
            '    Me.cmbReference.Rows(0).Activate()
            'Else
            'Me.cmbType.Text = Me.GrdStatus.CurrentRow.Cells("Type").Value.ToString

            Pk_Id = Val(Me.GrdStatus.GetRow.Cells("PK_Id").Value.ToString)
            Me.cmbType.Text = Me.GrdStatus.CurrentRow.Cells("Type").Value.ToString
            'RemoveHandler cmbReference.ValueChanged, AddressOf Me.cmbReference_ValueChanged
            Me.cmbReference.Value = Val(Me.GrdStatus.CurrentRow.Cells("RefCompanyId").Value.ToString)
            Me.cmbReference_ValueChanged(Nothing, Nothing)
            'End If
            Me.cmbDepartment.Text = Me.GrdStatus.CurrentRow.Cells("Department").Value.ToString
            Me.cmbLocation.Text = Me.GrdStatus.CurrentRow.Cells("CompanyLocation").Value.ToString
            Me.txtContactName.Text = Me.GrdStatus.CurrentRow.Cells("ContactName").Value.ToString
            Me.cmbDesignation.Text = Me.GrdStatus.CurrentRow.Cells("Designation").Value.ToString
            Me.txtMobNo.Text = Me.GrdStatus.CurrentRow.Cells("Mobile").Value.ToString
            Me.txtPhoneNo.Text = Me.GrdStatus.CurrentRow.Cells("Phone").Value.ToString
            Me.txtFaxNo.Text = Me.GrdStatus.CurrentRow.Cells("Fax").Value.ToString
            Me.txtIndexNo.Text = Me.GrdStatus.CurrentRow.Cells("IndexNo").Value.ToString
            Me.txtCompany.Text = Me.GrdStatus.CurrentRow.Cells("Company").Value.ToString
            Me.cmbNamePrefix.Text = Me.GrdStatus.CurrentRow.Cells("NamePrefix").Value.ToString
            Me.cmbCompanyLocations.Value = Val(Me.GrdStatus.CurrentRow.Cells("CompanyLocationId").Value.ToString)
            Me.txtAddress.Text = Me.GrdStatus.CurrentRow.Cells("Address").Value.ToString
            If Me.GrdStatus.CurrentRow.Cells("Email").Value.ToString.Length > 0 Then
                Me.txtEmail.Text = Me.GrdStatus.CurrentRow.Cells("Email").Value.ToString
            End If

            Me.btnSave.Text = "&Update"
            'AddHandler cmbReference.ValueChanged, AddressOf Me.cmbReference_ValueChanged
            'AddHandler cmbType.SelectedIndexChanged, AddressOf Me.cmbType_SelectedIndexChanged
            'GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReference.ValueChanged
        Try
            If isLoaded = False Then
                Exit Sub
            End If

            If Me.cmbReference.IsItemInList = False Then
                Exit Sub
            End If

            If Me.cmbReference.ActiveRow Is Nothing Then
                Exit Sub
            End If

            Me.txtCompany.Text = Me.cmbReference.ActiveRow.Cells("Name").Value.ToString
            Me.txtAddress.Text = Me.cmbReference.ActiveRow.Cells("Address").Value.ToString
            Me.txtMobNo.Text = Me.cmbReference.ActiveRow.Cells("MobileNo").Value.ToString
            Me.txtPhoneNo.Text = Me.cmbReference.ActiveRow.Cells("Phone").Value.ToString
            Me.txtEmail.Text = Me.cmbReference.ActiveRow.Cells("Email").Value.ToString

            Dim strSQL As String = String.Empty '"SELECT    0 as Id, Address as Address FROM  TblCompanyContacts WHERE Company= '" & Me.cmbReference.Text & "' "

            If Me.cmbType.Text = "Customer" Or Me.cmbType.Text = "Vendor" Or Me.cmbType.Text = "Bank" Then
                '    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, dbo.vwCOADetail.Contact_address as Address " _
                '            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('" & Me.cmbType.Text & "')  and  vwCOADetail.detail_title ='" & Me.cmbReference.Text & "' and  vwCOADetail.coa_detail_id is not  null"

                'Else 'If Me.cmbType.Text = "Friends & Family" Or Me.cmbType.Text = "Others" Then
                '    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, dbo.vwCOADetail.Contact_address as Address " _
                '    & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('" & Me.cmbType.Text & "') and  vwCOADetail.detail_title ='" & Me.cmbReference.Text & "' and  vwCOADetail.coa_detail_id is not  null" _
                '    & " union all SELECT     tblCompanyContacts.RefCompanyId, tblCompanyContacts.Address " _
                '    & " FROM  tblCompanyContacts where RefCompanyId=0 and type in ('" & Me.cmbType.Text & "') and tblCompanyContacts.ContactName='" & Me.cmbReference.Text & "' "


                strSQL = "SELECT        tblDefCompanyLocations.LocationId, tblDefCompanyLocations.LocationTitle, tblDefCompanyLocationType.LocationType, " & _
                        " ISNULL(tblDefCompanyLocations.AddressLine1, '') + ', ' + ISNULL(tblDefCompanyLocations.AddressLine2, '') + ', ' + ISNULL(tblListTerritory.TerritoryName, '') + ', ' + ISNULL(tblListCity.CityName, '') + ', ' + ISNULL(tblListState.StateName, '') + ', ' + ISNULL(tblListCountry.CountryName, '') AS Address, " & _
                        " tblDefCompanyLocations.EmailAddress, tblDefCompanyLocations.PhoneNo FROM  tblDefCompanyLocations LEFT JOIN " & _
                        " tblDefCompanyLocationType ON tblDefCompanyLocationType.LocationTypeId = tblDefCompanyLocations.LocationType LEFT JOIN " & _
                        " tblListTerritory INNER JOIN tblListCity ON tblListTerritory.CityId = tblListCity.CityId ON tblDefCompanyLocations.Area = tblListTerritory.TerritoryId LEFT JOIN " & _
                        " tblListState LEFT JOIN tblListCountry ON tblListState.CountryId = tblListCountry.CountryId ON tblListCity.StateId = tblListState.StateId where CompanyId=" & Val(Me.cmbReference.ActiveRow.Cells(0).Value.ToString)



            End If

            FillUltraDropDown(cmbCompanyLocations, strSQL)
            Me.cmbCompanyLocations.Rows(0).Activate()
            Me.cmbCompanyLocations.DisplayLayout.Bands(0).Columns(0).Hidden = True

            GetCompanyContacts()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim frm As New frmRptGrdCompanyContact
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            ApplyStyleSheet(frm)
            'frm.Size = New Size(308, 157)
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        Try
            If isLoaded = True Then FillCombo("VendorCustomer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False ' 'Task:2406 Added Field Chooser Rights
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True

                    ElseIf Rightsdt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtCompany_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCompany.KeyDown
        Try
            ''Me.txtCompany.AutoCompleteMode = AutoCompleteMode.Suggest
            ''Me.txtCompany.AutoCompleteSource = AutoCompleteSource.CustomSource

            ''Dim dataCollection As New AutoCompleteStringCollection()

            ''AddItems(dataCollection)
            ''Me.txtCompany.AutoCompleteCustomSource = dataCollection

        Catch ex As Exception

        End Try
    End Sub


    Public Function AddItems(ByVal coll As AutoCompleteStringCollection) As AutoCompleteStringCollection

        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()
        Dim reader As OleDbDataReader

        Try
            Dim list As New List(Of String)
            Dim strSQL As String = String.Empty

            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = "select Distinct Company from tblCompanyContacts"
            cmd.CommandText = strSQL
            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    coll.Add(reader("Company").ToString)
                End While
            End If

            trans.Commit()
            Return coll

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function


    Private Sub cmbCompanyLocations_ValueChanged(sender As Object, e As EventArgs) Handles cmbCompanyLocations.ValueChanged
        Try
            If cmbCompanyLocations.ActiveRow.Cells(0).Value > 0 Then

                If cmbCompanyLocations.ActiveRow.Cells("Address").Value.ToString.Trim.Replace(" ", "").Length > 5 Then
                    txtAddress.Text = cmbCompanyLocations.ActiveRow.Cells("Address").Value.ToString
                    'Me.txtAddress.ReadOnly = True
                End If

                If cmbCompanyLocations.ActiveRow.Cells("PhoneNo").Value.ToString.Trim.Replace(" ", "").Length > 1 Then
                    txtPhoneNo.Text = cmbCompanyLocations.ActiveRow.Cells("PhoneNo").Value.ToString
                    'txtPhoneNo.ReadOnly = True
                End If

                If cmbCompanyLocations.ActiveRow.Cells("EmailAddress").Value.ToString.Trim.Replace(" ", "").Length > 5 Then
                    txtEmail.Text = cmbCompanyLocations.ActiveRow.Cells("EmailAddress").Value.ToString
                    'txtEmail.ReadOnly = True
                End If

            Else

                txtAddress.ReadOnly = False
                txtPhoneNo.ReadOnly = False
                txtEmail.ReadOnly = False

                txtAddress.Text = String.Empty
                txtPhoneNo.Text = String.Empty
                txtEmail.Text = String.Empty

            End If


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddLocations_Click(sender As Object, e As EventArgs) Handles btnAddLocations.Click
        Try
            frmMain.LoadControl("CompanyLocations")
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class