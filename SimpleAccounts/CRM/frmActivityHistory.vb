Imports SBModel
Imports SBDal
Imports System.Data.SqlClient
Public Class frmActivityHistory
    Implements IGeneral


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSearch.Visible = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnSearch.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnSearch.Enabled = False
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
                        Me.btnSearch.Enabled = True
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

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

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

    Private Sub frmActivityHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillDropDown(cmbResponsible, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Name from tblDefEmployee", True)
            FillDropDown(cmbManger, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Name from tblDefEmployee", True)
            FillDropDown(cmbInside, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Name from tblDefEmployee", True)
            FillDropDown(cmbActivityType, "Select LeadActivityType.LeadActivityTypeID, LeadActivityType.ActivityType from LeadActivityType", True)
            FillDropDown(cmbStatus, "Select ActivityFeedbackStatus.ActivityFeedbackStatusId, ActivityFeedbackStatus.Status from ActivityFeedbackStatus", True)
            AddItemToGrid()

            Me.grdActivityHistory.RootTable.Columns("FeedbackDate").FormatString = str_DisplayDateFormat

            ApplySecurity(SBUtility.Utility.EnumDataMode.New)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbResponsible_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResponsible.SelectedIndexChanged
        'Try
        '    If Not Me.cmbResponsible.SelectedIndex = -1 Then FillDropDown(cmbResponsible, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Name from tblDefEmployee")
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub AddItemToGrid()
        Try
            Dim dt As DataTable
            Dim str As String

            str = "SELECT  ActivityFeedback.FeedbackDate, ActivityFeedbackStatus.Status, LeadActivityType.ActivityType, LeadProfile.LeadTitle, tblDefEmployee.Employee_Name " & _
            " FROM ActivityFeedback INNER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId" & _
            " INNER JOIN LeadActivity ON ActivityFeedback.ActivityId = LeadActivity.ActivityId " & _
            " INNER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID " & _
            " INNER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId INNER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID"
            If cmbResponsible.SelectedIndex > 0 Then
                str += " where (LeadActivity.ResponsiblePerson_Employee_Id = '" & cmbResponsible.SelectedValue & "')"
            End If
            If cmbInside.SelectedIndex > 0 Then
                str += " AND (LeadActivity.InsideSalesPerson_Employee_Id= '" & cmbInside.SelectedValue & "') "
            End If
            If cmbManger.SelectedIndex > 0 Then
                str += " AND (LeadActivity.Manager_Employee_Id= '" & cmbManger.SelectedValue & "') "
            End If
            If cmbActivityType.SelectedIndex > 0 Then
                str += " AND (LeadActivityType.LeadActivityTypeID = '" & cmbActivityType.SelectedValue & "') "
            End If
            If cmbStatus.SelectedIndex > 0 Then
                str += " AND (ActivityFeedback.ActivityFeedbackStatusId= '" & cmbStatus.SelectedValue & "') "
            End If
            'If dtpFrom.Value = Nothing And dtpTo.Value = Nothing Then
            str += " AND ( LeadActivity.ActivityDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND LeadActivity.ActivityDate <=Convert(Datetime, N'" & Me.dtpTo.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)) "
            'End If



            dt = GetDataTable(str)
            grdActivityHistory.DataSource = dt
            grdActivityHistory.RetrieveStructure()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdActivityHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdActivityHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdActivityHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Activity History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        AddItemToGrid()
    End Sub
End Class