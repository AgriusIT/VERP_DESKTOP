Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmLeadProfileList
    Implements IGeneral
    Dim LeadProfile As LeadProfileBE
    Dim objDAL As LeadProfileDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            FillCombos()
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar1_Load(Nothing, Nothing)

            'Me.grd.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grd.RootTable.Columns("LeadId").Visible = False
            Me.grd.RootTable.Columns("SectorId").Visible = False
            Me.grd.RootTable.Columns("StatusId").Visible = False
            Me.grd.RootTable.Columns("SourceId").Visible = False
            Me.grd.RootTable.Columns("ResponsibleId").Visible = False
            Me.grd.RootTable.Columns("InsideSalesId").Visible = False
            Me.grd.RootTable.Columns("ManagerId").Visible = False
            'Me.grd.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat
            Me.grd.RootTable.Columns("LeadTitle").Caption = "Company Name"
            

            '// Adding Delete Button in the grid
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").Width = 70
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                btnShow.Enabled = True
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnShow.Enabled = False
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnShow.Enabled = False
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        btnShow.Enabled = True
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
            FillDropDown(Me.cmbLeadTitle, "Select LeadId, LeadTitle from LeadProfile where Active = 1", True)
            FillDropDown(Me.cmbResponsible, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
            FillDropDown(Me.cmbInside, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
            FillDropDown(Me.cmbManager, "Select Employee_ID, Employee_Name  From tblDefEmployee", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim str As String
            'str = "SELECT LeadProfile.LeadId, LeadProfile.LeadTitle, LeadProfile.SectorId, LeadProfile.ProductName, LeadProfile.StatusId, LeadProfile.StatusRemarks, LeadProfile.SourceId, LeadProfile.SourceRemarks, LeadProfile.ResponsibleId, LeadProfile.InsideSalesId, LeadProfile.ManagerId, ActivityFeedbackStatus.Status, LeadActivityType.ActivityType, tblDefEmployee.Employee_Name As ResponsiblePerson, tblDefEmployee_1.Employee_Name AS InsidePerson, LeadActivity.ActivityDate, ActivityFeedback.Details FROM LeadActivityType RIGHT OUTER JOIN tblDefEmployee INNER JOIN LeadActivity ON tblDefEmployee.Employee_ID = LeadActivity.ResponsiblePerson_Employee_Id INNER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID ON  LeadActivityType.LeadActivityTypeID = LeadActivity.LeadActivityTypeID RIGHT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId LEFT OUTER JOIN ActivityFeedbackStatus RIGHT OUTER JOIN ActivityFeedback ON ActivityFeedbackStatus.ActivityFeedbackStatusId = ActivityFeedback.ActivityFeedbackStatusId ON LeadActivity.ActivityId = ActivityFeedback.ActivityId where " & IIf(cmbLeadTitle.SelectedValue > 0, "LeadProfile.LeadId =  " & cmbLeadTitle.SelectedValue & "AND", "") & " " & IIf(cmbResponsible.SelectedValue > 0, "LeadProfile.ResponsibleId. =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, "LeadProfile.InsideSalesId. =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, "LeadProfile.ManagerId. =  " & cmbManager.SelectedValue & "AND", "") & " (ActivityDate BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102))"
            str = "SELECT LeadProfile.LeadId, LeadProfile.LeadTitle, LeadProfile.SectorId, LeadProfile.ProductName, LeadProfile.StatusId, LeadProfile.StatusRemarks, LeadProfile.SourceId, LeadProfile.SourceRemarks, LeadProfile.ResponsibleId,  LeadProfile.InsideSalesId, LeadProfile.ManagerId, tblDefEmployee.Employee_Name AS ResponsiblePerson, tblDefEmployee_1.Employee_Name as InsidePerson, tblDefEmployee_2.Employee_Name AS Manager FROM tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadProfile ON tblDefEmployee_2.Employee_ID = LeadProfile.ManagerId LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadProfile.InsideSalesId = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee ON LeadProfile.ResponsibleId = tblDefEmployee.Employee_ID where " & IIf(cmbLeadTitle.SelectedValue > 0, "LeadProfile.LeadId =  " & cmbLeadTitle.SelectedValue & "AND", "") & " " & IIf(cmbResponsible.SelectedValue > 0, "LeadProfile.ResponsibleId =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, "LeadProfile.InsideSalesId =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, "LeadProfile.ManagerId =  " & cmbManager.SelectedValue & "AND", "") & " (LeadDate BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102))"


            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            Else
                ShowErrorMessage("There is no activity against this Title. Please add activity against your search")
                Me.grd.DataSource = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

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
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            frmLeadProfile.LeadID = 0
            frmLeadProfile.DoHaveSaveRights = DoHaveSaveRights
            frmLeadProfile.ShowDialog()

            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmLeadProfile.LeadID = Val(Me.grd.CurrentRow.Cells("LeadId").Value.ToString)
                frmLeadProfile.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmLeadProfile.LeadID = 0
            End If
            frmLeadProfile.ShowDialog()
            GetAllRecords()
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            LeadProfile = New LeadProfileBE
            objDAL = New LeadProfileDAL
            LeadProfile.LeadId = Val(Me.grd.CurrentRow.Cells("LeadId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(LeadProfile)
                    Me.grd.GetRow.Delete()
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                Else
                    msg_Information("You do not have delete rights.")
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Lead Profile"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try

            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class