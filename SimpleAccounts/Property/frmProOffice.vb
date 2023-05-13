Imports SBDal
Imports SBModel
Public Class frmProOffice
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared OfficeId As Integer
    Dim Office As OfficeBE
    Public OfficeDAL As OfficeDAL = New OfficeDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False

    Private Sub frmProOffice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Estate")
            FillCombos("City")
            FillCombos("Area")
            Dim dt As DataTable = New OfficeDAL().GetById(OfficeId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1
                    OfficeId = dt.Rows(0).Item("OfficeId")
                    txtName.Text = dt.Rows(0).Item("Name")
                    txtLanlineNo.Text = dt.Rows(0).Item("LandlinePhone")
                    txtCellNo.Text = dt.Rows(0).Item("CellPhone")
                    txtEmailID.Text = dt.Rows(0).Item("Email")
                    txtFaxNo.Text = dt.Rows(0).Item("FaxNumber")
                    txtAddressLine1.Text = dt.Rows(0).Item("AddressLine1")
                    txtAddressLine2.Text = dt.Rows(0).Item("AddressLine2")
                    cmbArea.SelectedValue = dt.Rows(0).Item("AreaId")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    cmbEstate.SelectedValue = dt.Rows(0).Item("EstateId")
                    cmbCity.SelectedValue = dt.Rows(0).Item("CityId")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            'ToolTip

            Ttip.SetToolTip(Me.txtName, "Enter office name")
            Ttip.SetToolTip(Me.txtLanlineNo, "Enter office landline number")
            Ttip.SetToolTip(Me.txtCellNo, "Enter office cell number")
            Ttip.SetToolTip(Me.txtEmailID, "Enter email id")
            Ttip.SetToolTip(Me.txtFaxNo, "Enter office fax number")
            Ttip.SetToolTip(Me.txtAddressLine1, "Enter office address")
            Ttip.SetToolTip(Me.cmbArea, "Select area")
            Ttip.SetToolTip(Me.cmbEstate, "Enter estate")
            Ttip.SetToolTip(Me.cmbCity, "Enter city")
            Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")
            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProOffice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
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
            If Condition = "Estate" Then
                str = "Select EstateId, Name from Estate"
                FillDropDown(Me.cmbEstate, str, False)
            ElseIf Condition = "City" Then
                str = "Select CityID, CityName from tblListCity"
                FillDropDown(Me.cmbCity, str, False)
            ElseIf Condition = "Area" Then
                str = "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue
                FillDropDown(Me.cmbArea, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

            Office = New OfficeBE
            Office.OfficeId = OfficeId
            Office.Name = Me.txtName.Text
            Office.LandlinePhone = txtLanlineNo.Text
            Office.CellPhone = txtCellNo.Text
            Office.Email = txtEmailID.Text
            Office.FaxNumber = txtFaxNo.Text
            Office.AddressLine1 = txtAddressLine1.Text
            Office.AddressLine2 = txtAddressLine2.Text
            Office.AreaId = cmbArea.SelectedValue
            Office.Remarks = txtRemarks.Text
            Office.EstateId = cmbEstate.SelectedValue
            Office.CityId = cmbCity.SelectedValue
            Office.ActivityLog = New ActivityLog

        Catch ex As Exception
            Throw ex
        End Try
        
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter Name")
                Me.txtName.Focus()
                Return False
            End If

            If Me.txtCellNo.Text = String.Empty Then
                ShowErrorMessage("Cell No is required")
                Me.txtCellNo.Focus()
                Return False
            End If
            If Me.txtAddressLine1.Text = String.Empty Then
                ShowErrorMessage("Address Line 1 is required")
                Me.txtAddressLine1.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtName.Focus()
            OfficeId = 0
            'Me.btnSave.Text = "&Save"
            txtName.Text = ""
            txtLanlineNo.Text = ""
            txtCellNo.Text = ""
            txtEmailID.Text = ""
            txtFaxNo.Text = ""
            txtAddressLine1.Text = ""
            txtAddressLine2.Text = ""
            cmbArea.SelectedValue = 0
            txtRemarks.Text = ""
            cmbEstate.SelectedValue = 0
            cmbCity.SelectedValue = 0
            btnSave.Enabled = DoHaveSaveRights

        Catch ex As Exception
            msg_Error(ex.Message)
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If OfficeId = 0 Then
                    If OfficeDAL.Add(Office) Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                        Me.Close()
                    End If
                Else
                    If OfficeDAL.Update(Office) Then
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
End Class