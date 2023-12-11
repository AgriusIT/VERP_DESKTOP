Imports SBDal
Imports SBModel


Public Class frmProBranch
    Dim BranchId As Integer = 0
    Dim Str As String = String.Empty
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Agent name is required")
                Me.txtName.Focus()
                Exit Sub
            End If
            'If Me.txtMobile1.Text = String.Empty Then
            '    ShowErrorMessage("Primary mobile is required")
            '    Me.txtMobile1.Focus()
            '    Exit Sub
            'End If
            'If Me.txtAddressLine1.Text = String.Empty Then
            '    ShowErrorMessage("Address Line 1 is required")
            '    Me.txtAddressLine1.Focus()
            '    Exit Sub
            'End If
            Dim Obj As New BEBranch
            Obj.BranchId = BranchId
            Obj.Name = Me.txtName.Text
            Obj.Employee_ID = Me.cmbEmployee.SelectedValue
            Obj.CellPhone = Me.txtCellNo.Text
            Obj.LandlinePhone = Me.txtLandline.Text
            Obj.AreaId = Me.cmbArea.SelectedValue
            Obj.CityId = Me.cmbCity.SelectedValue
            Obj.AddressLine1 = Me.txtAddressLine1.Text
            Obj.AddressLine2 = Me.txtAddressLine2.Text
            Obj.Active = Me.cbActive.Checked
            Obj.CityId = Me.cmbCity.SelectedValue
            Obj.Remarks = Me.txtRemarks.Text
         

            If BranchId = 0 Then
                If BranchDAL.Save(Obj) Then
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
                    msg_Information("Record has been saved successfully.")
                    ResetFields()
                    Me.Close()
                End If
            Else
                If BranchDAL.Update(Obj) Then
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, "", True)
                    msg_Information("Record has been updated successfully.")
                    ResetFields()
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetFields()
        Try
            Me.txtName.Focus()
            BranchId = 0
            Me.txtName.Text = String.Empty
            Me.txtCellNo.Text = String.Empty
            Me.txtLandline.Text = String.Empty
            Me.txtAddressLine1.Text = String.Empty
            Me.txtAddressLine2.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.txtCellNo.Text = String.Empty
            Me.cbActive.Checked = True
            If Not cmbCity.SelectedIndex = -1 Then
                Me.cmbCity.SelectedIndex = 0
            End If
            If Not cmbEmployee.SelectedIndex = -1 Then
                Me.cmbEmployee.SelectedIndex = 0
            End If
            'If Not cmbSpeciality.SelectedIndex = -1 Then
            '    Me.cmbSpeciality.SelectedIndex = 0
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub New(ByVal AgentId As Integer)
        Try
            InitializeComponent()
            Str = "SELECT Employee_ID, Employee_Name FROM tblDefEmployee"
            FillDropDown(Me.cmbEmployee, Str)
            Str = "Select CityID, CityName from tblListCity"
            FillDropDown(Me.cmbCity, Str)
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
    Sub New(ByVal Obj As BEBranch, ByVal DoHaveUpdateRights As Boolean)
        Try
            'Speciality
            InitializeComponent()
            btnSave.Enabled = DoHaveUpdateRights
            Str = "SELECT Employee_ID, Employee_Name FROM tblDefEmployee"
            FillDropDown(Me.cmbEmployee, Str)
            Str = "Select CityID, CityName from tblListCity"
            FillDropDown(Me.cmbCity, Str)
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
            FillDropDown(Me.cmbCity, "Select * from tblListCity")
            Str = "SELECT Employee_ID, Employee_Name FROM tblDefEmployee"
            FillDropDown(Me.cmbEmployee, Str)
            Str = "Select CityID, CityName from tblListCity"
            FillDropDown(Me.cmbCity, Str)
            'Dim dt As DataTable = AgentDAL.GetAgent(AgentId)
            'If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("AgentId") > 0 Then
            '    ShowData(dt)
            'Else
            '    ResetFields()
            'End If
            btnSave.Enabled = DoHaveSaveRights
            ResetFields()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal dt As DataTable)
        Try
            'AgentId, Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, " _"
            '    & " SpecialityId, Email,  Active FROM Agent WHERE AgentId


            'BranchId = dt.Rows(0).Item("AgentId")
            'Me.txtName.Text = dt.Rows(0).Item("Name").ToString
            'Me.txtFatherName.Text = dt.Rows(0).Item("FathersName").ToString
            'Me.txtMobile1.Text = dt.Rows(0).Item("PrimaryMobile").ToString
            'Me.txtMobile2.Text = dt.Rows(0).Item("SecondaryMobile").ToString
            'Me.cmbCity.SelectedValue = dt.Rows(0).Item("CityId")
            ''Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
            'Me.cmbSpeciality.SelectedValue = dt.Rows(0).Item("SpecialityId")
            'Me.txtAddressLine1.Text = dt.Rows(0).Item("AddressLine1").ToString
            'Me.txtAddressLine2.Text = dt.Rows(0).Item("AddressLine2").ToString
            'Me.txtEmail.Text = dt.Rows(0).Item("Email").ToString
            'Me.txtCNIC.Text = dt.Rows(0).Item("CNIC").ToString
            'Me.cbActive.Checked = dt.Rows(0).Item("Active")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal Obj As BEBranch)
        Try
            'AgentId, Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, " _"
            '    & " SpecialityId, Email,  Active FROM Agent WHERE AgentId


            BranchId = Obj.BranchId
            Me.txtName.Text = Obj.Name
            Me.cmbEmployee.SelectedValue = Obj.Employee_ID
            Me.txtCellNo.Text = Obj.CellPhone
            Me.txtLandline.Text = Obj.LandlinePhone
            Me.cmbCity.SelectedValue = Obj.CityId
            Me.cmbArea.SelectedValue = Obj.AreaId
            Me.txtAddressLine1.Text = Obj.AddressLine1
            Me.txtAddressLine2.Text = Obj.AddressLine2
            Me.txtRemarks.Text = Obj.Remarks
            Me.cbActive.Checked = Obj.Active

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'ElseIf Condition = "City" Then
    '          str = "Select CityID, CityName from tblListCity"
    '          FillDropDown(Me.cmbCity, str, False)
    '      ElseIf Condition = "Area" Then
    '          str = "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue
    '          FillDropDown(Me.cmbArea, str, False)

    Private Sub cmbCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCity.SelectedIndexChanged
        Try
            If Not cmbCity.SelectedIndex = -1 Then
                FillDropDown(Me.cmbArea, "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProBranch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
       
    End Sub

    Private Sub frmProBranch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProBranch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub
End Class