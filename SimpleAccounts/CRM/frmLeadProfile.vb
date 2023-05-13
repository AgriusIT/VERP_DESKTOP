Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmLeadProfile
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared LeadID As Integer
    Dim Lead As LeadProfileBE
    Public LeadProfileDAL As LeadProfileDAL = New LeadProfileDAL()
    Dim PropertyTypeId As Integer
    Dim Personlist As List(Of ConcernedPersonBE)
    Dim OfficeList As List(Of LeadOfficeBE)
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False

    Private Sub frmLeadProfile_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
                frmLeadProfileList.FillCombos()
                frmLeadProfileList.GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmOTCLeadProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            ' Tooltip
            FillCombos("Sector")
            FillCombos("Status")
            FillCombos("Source")
            FillCombos("Responsible")
            FillCombos("InsideSales")
            FillCombos("Manager")
            
            Dim dt As DataTable = New LeadProfileDAL().GetById(LeadID)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    txtTitle.Text = dt.Rows(0).Item("LeadTitle")
                    cmbSector.SelectedValue = dt.Rows(0).Item("SectorId")
                    txtProduct.Text = dt.Rows(0).Item("ProductName")
                    cmbStatus.SelectedValue = dt.Rows(0).Item("Statusid")
                    txtStatusRemarks.Text = dt.Rows(0).Item("StatusRemarks")
                    cmbSource.SelectedValue = dt.Rows(0).Item("SourceId")
                    txtSourceRemarks.Text = dt.Rows(0).Item("SourceRemarks")
                    cmbResponsible.SelectedValue = dt.Rows(0).Item("ResponsibleId")
                    cmbInsideSales.SelectedValue = dt.Rows(0).Item("InsideSalesId")
                    cmbManager.SelectedValue = dt.Rows(0).Item("ManagerId")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If
            GetConcernPerson(LeadID)
            GetLeadOffice(LeadID)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Private Function GetConcernPerson(ByVal LeadId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "select ConcernPersonId, LeadId, ConcernPerson, Designation, PhoneNo, Email from tblConcernPerson where LeadId = " & LeadId & ""
            dt = GetDataTable(str)
            grdConcernPerson.DataSource = dt
            grdConcernPerson.RetrieveStructure()
            grdConcernPerson.RootTable.Columns("ConcernPersonId").Visible = False
            grdConcernPerson.RootTable.Columns("LeadId").Visible = False
         Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetLeadOffice(ByVal LeadId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "select LeadOfficeId, LeadId, OfficeTitle, Address, Website from tblLeadOffice where LeadId = " & LeadId & ""
            dt = GetDataTable(str)
            grdOffice.DataSource = dt
            grdOffice.RetrieveStructure()
            grdOffice.RootTable.Columns("LeadOfficeId").Visible = False
            grdOffice.RootTable.Columns("LeadId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Sector" Then
                str = "Select * from tblLeadSector where Active = 1"
                FillDropDown(Me.cmbSector, str, True)
            ElseIf Condition = "Status" Then
                str = "Select * from tblLeadStatus where Active = 1"
                FillDropDown(Me.cmbStatus, str, True)
            ElseIf Condition = "Source" Then
                str = "Select * from tblLeadSource where Active = 1"
                FillDropDown(Me.cmbSource, str, True)
            ElseIf Condition = "Responsible" Then
                str = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1"
                FillDropDown(Me.cmbResponsible, str, True)
            ElseIf Condition = "InsideSales" Then
                str = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1"
                FillDropDown(Me.cmbInsideSales, str, True)
            ElseIf Condition = "Manager" Then
                str = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1"
                FillDropDown(Me.cmbManager, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Lead = New LeadProfileBE
            Lead.LeadId = LeadID
            Lead.LeadTitle = Me.txtTitle.Text
            Lead.SectorId = cmbSector.SelectedValue
            Lead.ProductName = Me.txtProduct.Text
            Lead.StatusId = cmbStatus.SelectedValue
            Lead.StatusRemarks = txtStatusRemarks.Text
            Lead.SourceId = cmbSource.SelectedValue
            Lead.SourceRemarks = txtSourceRemarks.Text
            Lead.ResponsibleId = cmbResponsible.SelectedValue
            Lead.InsideSalesId = cmbInsideSales.SelectedValue
            Lead.ManagerId = cmbManager.SelectedValue
            Lead.Active = chkActive.Checked
            Lead.ActivityLog = New ActivityLog
            Personlist = New List(Of ConcernedPersonBE)
            For i As Integer = 0 To grdConcernPerson.RowCount - 1
                Dim PDetail As New ConcernedPersonBE
                PDetail.LeadConcernedId = Val(grdConcernPerson.GetRows(i).Cells("ConcernPersonId").Value.ToString)
                PDetail.LeadId = Val(grdConcernPerson.GetRows(i).Cells("LeadId").Value.ToString)
                PDetail.ConcernPerson = grdConcernPerson.GetRows(i).Cells("ConcernPerson").Value.ToString
                PDetail.Designation = grdConcernPerson.GetRows(i).Cells("Designation").Value.ToString
                PDetail.Phoneno = grdConcernPerson.GetRows(i).Cells("PhoneNo").Value.ToString
                PDetail.Email = grdConcernPerson.GetRows(i).Cells("Email").Value.ToString
                'PDetail.PropertyPurchaseId = PPID
                Personlist.Add(PDetail)
            Next
            OfficeList = New List(Of LeadOfficeBE)
            For i As Integer = 0 To grdOffice.RowCount - 1
                Dim TDetail As New LeadOfficeBE
                TDetail.LeadOfficeId = Val(grdOffice.GetRows(i).Cells("LeadOfficeId").Value.ToString)
                TDetail.LeadId = Val(grdOffice.GetRows(i).Cells("LeadId").Value.ToString)
                TDetail.Name = grdOffice.GetRows(i).Cells("OfficeTitle").Value.ToString
                TDetail.Address = grdOffice.GetRows(i).Cells("Address").Value.ToString
                TDetail.Website = grdOffice.GetRows(i).Cells("Website").Value.ToString
                'TDetail.Status = grdTask.GetRows(i).Cells("Status").Value.ToString
                OfficeList.Add(TDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtTitle.Text = String.Empty Then
                ShowErrorMessage("Please enter Lead Title")
                Me.txtTitle.Focus()
                Return False
            End If
            If Me.cmbResponsible.SelectedIndex = 0 Then
                ShowErrorMessage("Please select any responsible person")
                cmbResponsible.Focus()
                Return False
            End If
            If Me.cmbInsideSales.SelectedIndex = 0 Then
                ShowErrorMessage("Please select any Inside person")
                cmbInsideSales.Focus()
                Return False
            End If
            If Me.cmbManager.SelectedIndex = 0 Then
                ShowErrorMessage("Please select any Manager")
                cmbInsideSales.Focus()
                Return False
            End If

            FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If LeadID = 0 Then
                    If LeadProfileDAL.Add(Lead) Then
                        If LeadProfileDAL.AddPerson(Personlist) = True Then
                            If LeadProfileDAL.AddOffice(OfficeList) = True Then
                                msg_Information("Record has been saved successfully.")
                                ReSetControls()
                                Me.Close()
                                frmLeadProfileList.FillCombos()
                            End If
                            'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                        End If
                    End If
                Else
                    If LeadProfileDAL.Update(Lead) Then
                        If LeadProfileDAL.UpdatePerson(Personlist) = True Then
                            If LeadProfileDAL.UpdateOffice(OfficeList) = True Then
                                msg_Information("Record has been updated successfully.")
                                ReSetControls()
                                Me.Close()
                                frmLeadProfileList.FillCombos()
                            End If
                        End If
                    End If
                    End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtTitle.Text = ""
            cmbSector.SelectedIndex = 0
            txtProduct.Text = ""
            cmbStatus.SelectedIndex = 0
            txtStatusRemarks.Text = ""
            cmbSource.SelectedIndex = 0
            txtSourceRemarks.Text = ""
            cmbResponsible.SelectedIndex = 0
            cmbInsideSales.SelectedIndex = 0
            cmbManager.SelectedIndex = 0
            btnSave.Enabled = DoHaveSaveRights
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
            frmLeadProfileList.FillCombos()
            frmLeadProfileList.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged

    End Sub
End Class