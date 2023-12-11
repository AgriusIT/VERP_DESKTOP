Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmActivityPlanList
    Implements IGeneral
    Dim Feedback As ActivityFeedbackBE
    Dim FeebackDAL As ActivityFeedbackDAL
    Dim objDAL As LeadActivityDAL
    Dim LeadActivity As LeadActivityBE
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
            Me.grd.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grd.RootTable.Columns("ActivityId").Visible = False
            Me.grd.RootTable.Columns("LeadId").Visible = False
            Me.grd.RootTable.Columns("LeadContactId").Visible = False
            Me.grd.RootTable.Columns("LeadOfficeId").Visible = False
            Me.grd.RootTable.Columns("LeadActivityTypeID").Visible = False
            Me.grd.RootTable.Columns("ResponsiblePerson_Employee_Id").Visible = False
            'Me.grd.RootTable.Columns("InsideSalesPerson_Employee_Id").Visible = False
            Me.grd.RootTable.Columns("InsideSalesPerson_Employee_Id").Caption = "Priorities"
            Me.grd.RootTable.Columns("Manager_Employee_Id").Visible = False
            Me.grd.RootTable.Columns("ActivityTime").FormatString = "hh:mm:ss tt"

            '// Adding Delete Button in the grid
            If Me.grd.RootTable.Columns.Contains("FeedBack") = False Then
                Me.grd.RootTable.Columns.Add("FeedBack")
                Me.grd.RootTable.Columns("FeedBack").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("FeedBack").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("FeedBack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("FeedBack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("FeedBack").Width = 80
                Me.grd.RootTable.Columns("FeedBack").ButtonText = "Feedback"
                Me.grd.RootTable.Columns("FeedBack").Key = "Feedback"
                Me.grd.RootTable.Columns("FeedBack").Caption = "Feedback"
            End If
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
            Dim Str As String
            Str = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1"
            FillDropDown(Me.cmbResponsiblePerson, Str, True)
            FillDropDown(Me.cmbInside, "Select Employee_ID, Employee_Name From tblDefEmployee ", True)
            FillDropDown(Me.cmbManager, "Select Employee_ID, Employee_Name From tblDefEmployee ", True)
            FillDropDown(Me.cmbCompanyName, "Select LeadProfileId, CompanyName from tbldefLeadProfile", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String
            'str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId, LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as ResponsibleManager, tblDefEmployee.Employee_Name AS InsideSales, tblDefEmployee_1.Employee_Name AS ManagerInsideSales, LeadActivity.IsConfirmed FROM LeadActivity LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId where " & IIf(Me.cmbResponsiblePerson.SelectedValue > 0, " ResponsiblePerson_Employee_Id = " & Me.cmbResponsiblePerson.SelectedValue & "AND", "") & " " & IIf(Me.cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id = " & Me.cmbInside.SelectedValue & "AND", "") & " " & IIf(Me.cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id = " & Me.cmbManager.SelectedValue & "AND", "") & " ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)"
            'str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId,  LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle,tblDefProject.ProjectTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as ResponsibleManager, tblDefEmployee.Employee_Name AS InsideSales, tblDefEmployee_1.Employee_Name AS ManagerInsideSales, LeadActivity.IsConfirmed FROM LeadActivity RIGHT OUTER JOIN tblDefProject ON LeadActivity.ProjectId = tblDefProject.ProjectId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId where " & IIf(Me.cmbResponsiblePerson.SelectedValue > 0, " ResponsiblePerson_Employee_Id = " & Me.cmbResponsiblePerson.SelectedValue & "AND", "") & " " & IIf(Me.cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id = " & Me.cmbInside.SelectedValue & "AND", "") & " " & IIf(Me.cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id = " & Me.cmbManager.SelectedValue & "AND", "") & " ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)"
            str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId,  LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle,tblDefProject.ProjectTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as ResponsibleManager, tblDefEmployee.Employee_Name AS InsideSales, tblDefEmployee_1.Employee_Name AS ManagerInsideSales, LeadActivity.IsConfirmed FROM LeadActivity LEFT OUTER JOIN tblDefProject ON LeadActivity.ProjectId = tblDefProject.ProjectId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId where " & IIf(Me.cmbCompanyName.SelectedValue > 0, " LeadActivity.LeadId = " & Me.cmbCompanyName.SelectedValue & "AND", "") & " " & IIf(Me.cmbResponsiblePerson.SelectedValue > 0, " ResponsiblePerson_Employee_Id = " & Me.cmbResponsiblePerson.SelectedValue & "AND", "") & " " & IIf(Me.cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id = " & Me.cmbInside.SelectedValue & "AND", "") & " " & IIf(Me.cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id = " & Me.cmbManager.SelectedValue & "AND", "") & " ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)"
            Dim dt As DataTable
            'objDAL = New LeadActivityDAL
            'Dim dt As DataTable = objDAL.GetAll()
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("ActivityDate").FormatString = str_DisplayDateFormat
            ApplyGridSettings()
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
            FillCombos()
            frmPlanNewActivity.ActivityId = 0
            frmPlanNewActivity.DoHaveSaveRights = DoHaveSaveRights
            frmPlanNewActivity.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmPlanNewActivity.ActivityId = Val(Me.grd.CurrentRow.Cells("ActivityId").Value.ToString)
                frmPlanNewActivity.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmPlanNewActivity.ActivityId = 0
            End If
            frmPlanNewActivity.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            LeadActivity = New LeadActivityBE
            objDAL = New LeadActivityDAL
            If e.Column.Key = "Feedback" Then
                frmActivityFeedback.ActivityId = Val(Me.grd.CurrentRow.Cells("ActivityId").Value.ToString)
                frmActivityFeedback.ShowDialog()
            ElseIf e.Column.Key = "Delete" Then
                LeadActivity.ActivityId = Val(Me.grd.CurrentRow.Cells("ActivityId").Value.ToString)
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(LeadActivity)
                    Me.grd.GetRow.Delete()
                    SaveActivityLog("CRM", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                    msg_Information("Information is Deleted")
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Activity Plan"
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