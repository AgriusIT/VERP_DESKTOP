Imports SBDal
Imports SBModel
Public Class frmProAgent

    Public AgentId As Integer = 0
    Public AccountId As Integer = 0
    Private Sub frmProAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim AgentAccountId = CInt(getConfigValueByType("AgentSubSub"))

        If AgentAccountId <= 0 Then
            ShowErrorMessage("Please Select a sub sub Account to map Against the Agent")
        End If


        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub

    Private Sub frmProAgent_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub New(ByVal AgentId As Integer)
        Try
            'Speciality
            InitializeComponent()
            FillDropDown(Me.cmbCity, "Select * from tblListCity")
            'FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title FROM vwCOADetail")
            FillDropDown(Me.cmbSpeciality, "SELECT SpecialityId, Speciality FROM Speciality")
            Dim dt As DataTable = AgentDAL.GetAgent(AgentId)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("AgentId") > 0 Then
                ShowData(dt)
            Else
                ResetFields()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub New(ByVal Obj As BEAgent, ByVal DoHaveUpdateRights As Boolean)
        Try
            'Speciality
            InitializeComponent()
            Me.btnSave.Enabled = DoHaveUpdateRights
            FillDropDown(Me.cmbCity, "Select * from tblListCity")
            'FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title FROM vwCOADetail")
            FillDropDown(Me.cmbSpeciality, "SELECT SpecialityId, Speciality FROM Speciality")
            'Dim dt As DataTable = AgentDAL.GetAgent(AgentId)
            If Obj IsNot Nothing Then
                ShowData(Obj)
            Else
                ResetFields()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub New(ByVal DoHaveSaveRights As Boolean)
        Try
            'Speciality
            InitializeComponent()
            Me.btnSave.Enabled = DoHaveSaveRights
            FillDropDown(Me.cmbCity, "Select * from tblListCity")
            'FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title FROM vwCOADetail")
            FillDropDown(Me.cmbSpeciality, "SELECT SpecialityId, Speciality FROM Speciality")
            'Dim dt As DataTable = AgentDAL.GetAgent(AgentId)
            'If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("AgentId") > 0 Then
            '    ShowData(dt)
            'Else
            '    ResetFields()
            'End If
            ResetFields()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub ShowData(ByVal dt As DataTable)
        Try
            'AgentId, Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, " _"
            '    & " SpecialityId, Email,  Active FROM Agent WHERE AgentId


            AgentId = dt.Rows(0).Item("AgentId")
            Me.txtName.Text = dt.Rows(0).Item("Name").ToString
            Me.txtFatherName.Text = dt.Rows(0).Item("FathersName").ToString
            Me.txtMobile1.Text = dt.Rows(0).Item("PrimaryMobile").ToString
            Me.txtMobile2.Text = dt.Rows(0).Item("SecondaryMobile").ToString
            Me.cmbCity.SelectedValue = dt.Rows(0).Item("CityId")
            'Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
            Me.cmbSpeciality.SelectedValue = dt.Rows(0).Item("SpecialityId")
            Me.txtAddressLine1.Text = dt.Rows(0).Item("AddressLine1").ToString
            Me.txtAddressLine2.Text = dt.Rows(0).Item("AddressLine2").ToString
            Me.txtEmail.Text = dt.Rows(0).Item("Email").ToString
            Me.txtCNIC.Text = dt.Rows(0).Item("CNIC").ToString
            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
            Me.cbActive.Checked = dt.Rows(0).Item("Active")
         
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal Obj As BEAgent)
        Try
            'AgentId, Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, " _"
            '    & " SpecialityId, Email,  Active FROM Agent WHERE AgentId


            AgentId = Obj.AgentId
            Me.txtName.Text = Obj.Name
            Me.txtFatherName.Text = Obj.FathersName
            Me.txtMobile1.Text = Obj.PrimaryMobile
            Me.txtMobile2.Text = Obj.SecondaryMobile
            Me.cmbCity.SelectedValue = Obj.CityId
            'Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
            Me.cmbSpeciality.SelectedValue = Obj.SpecialityId
            Me.txtAddressLine1.Text = Obj.AddressLine1
            Me.txtAddressLine2.Text = Obj.AddressLine2
            Me.txtEmail.Text = Obj.Email
            Me.txtCNIC.Text = Obj.CNIC
            Me.txtAccount.Text = Obj.AccountTitle
            Me.txtRemarks.Text = Obj.Remarks
            Me.cmbBlood.Text = Obj.BloodGroup
            AccountId = Obj.coa_detail_id
            Me.cbActive.Checked = Obj.Active

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ResetFields()
        Try
            AgentId = 0
            Me.txtName.Text = String.Empty
            Me.txtFatherName.Text = String.Empty
            Me.txtMobile1.Text = String.Empty
            Me.txtMobile2.Text = String.Empty
            Me.txtAddressLine1.Text = String.Empty
            Me.txtAddressLine2.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtCNIC.Text = String.Empty
            Me.cbActive.Checked = True
            Me.txtAccount.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.cmbBlood.SelectedIndex = 0
            'If Me.cmbBlood.SelectedIndex = "" Then
            '    Me.cmbBlood.SelectedItem
            'End If
            If Not cmbCity.SelectedIndex = -1 Then
                Me.cmbCity.SelectedIndex = 0
            End If
            'If Not cmbAccount.SelectedIndex = -1 Then
            '    Me.cmbAccount.SelectedIndex = 0
            'End If
            If Not cmbSpeciality.SelectedIndex = -1 Then
                Me.cmbSpeciality.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Agent name is required")
                Me.txtName.Focus()
                Exit Sub
            End If
            If Me.txtMobile1.Text = String.Empty Then
                ShowErrorMessage("Primary mobile is required")
                Me.txtMobile1.Focus()
                Exit Sub
            End If
            'If Me.txtAddressLine1.Text = String.Empty Then
            '    ShowErrorMessage("Address Line 1 is required")
            '    Me.txtAddressLine1.Focus()
            '    Exit Sub
            'End If
            Dim Obj As New BEAgent
            Obj.AgentId = AgentId
            Obj.Name = Me.txtName.Text
            Obj.FathersName = Me.txtFatherName.Text
            Obj.CNIC = Me.txtCNIC.Text
            Obj.PrimaryMobile = Me.txtMobile1.Text
            Obj.SecondaryMobile = Me.txtMobile2.Text
            Obj.Email = Me.txtEmail.Text
            Obj.AddressLine1 = Me.txtAddressLine1.Text
            Obj.AddressLine2 = Me.txtAddressLine2.Text
            Obj.Active = Me.cbActive.Checked
            Obj.CityId = Me.cmbCity.SelectedValue
            Obj.coa_detail_id = AccountId
            Obj.SpecialityId = Me.cmbSpeciality.SelectedValue
            Obj.Remarks = Me.txtRemarks.Text
            Obj.BloodGroup = Me.cmbBlood.Text
            Obj.Account.COADetailID = AccountId
            Obj.Account.DetailTitle = Me.txtName.Text
            Obj.Account.Active = True
            If Not getConfigValueByType("AgentSubSub") = "Error" Then
                'If (getConfigValueByType("AgentSubSub")) Then
                '    Obj.Account.MainSubSubID = 1
                'Else
                '    Obj.Account.MainSubSubID = 0
                'End If
                Obj.Account.MainSubSubID = CInt(getConfigValueByType("AgentSubSub"))
            End If
            Obj.Account.IsDate = Now
            If AgentId = 0 Then

                If COADetailDAL.GetAccountName(Obj.Name, Obj.Account.MainSubSubID) = True Then

                    If msg_Confirm("Account name already exist do you want create account with same name") = True Then

                        If AgentDAL.Save(Obj) Then
                            SaveActivityLog("Configuration", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
                            msg_Information("Record has been saved successfully.")
                            ResetFields()
                            Me.Close()
                        End If

                    End If

                Else

                    If AgentDAL.Save(Obj) Then
                        SaveActivityLog("Configuration", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
                        msg_Information("Record has been saved successfully.")
                        ResetFields()
                        Me.Close()
                    End If

                End If

            Else

                If COADetailDAL.GetAccountName(Obj.Name, Obj.Account.MainSubSubID) = True Then

                    If msg_Confirm("Account name already exist do you want create account with same name") = True Then

                        If AgentDAL.Update(Obj) Then
                            SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, "", True)
                            msg_Information("Record has been updated successfully.")
                            ResetFields()
                            Me.Close()
                        End If

                    End If

                Else

                    If AgentDAL.Update(Obj) Then
                        SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, "", True)
                        msg_Information("Record has been updated successfully.")
                        ResetFields()
                        Me.Close()
                    End If

                End If

                End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.CtrlGrdBar1.mGridPrint.Enabled = True
                'Me.CtrlGrdBar1.mGridExport.Enabled = True
                'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    'Me.CtrlGrdBar1.mGridExport.Enabled = False
                    'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                'Me.CtrlGrdBar1.mGridExport.Enabled = False
                'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        'ElseIf RightsDt.FormControlName = "Print" Then
                        '    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Export" Then
                        '    Me.CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        '    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Update" Then
                        '    Me.btnEdit.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        'Me.btnDelete.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbBlood_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBlood.SelectedIndexChanged

    End Sub
End Class