Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frmTaskWork
    Implements IGeneral
    Dim MasterId As Integer = 0
    Dim TaskWork As TaskWorking
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.AutoSizeColumns()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Visible = False
            Me.grdSaved.RootTable.Columns(2).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Return New TaskWorkingDAL().Delete(TaskWork)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Employees" Then
                str = "Select Employee_Id, Employee_Name from tblDefEmployee Where Active=1 " ''TASKTFS75 added and set active =1
                FillDropDown(Me.cmbEmployees, str)
            ElseIf Condition = "Tasks" Then
                str = " Select TaskId, Name as [Task Name] From TblDefTasks"
                str += "" & IIf(Me.cmbEmployees.SelectedIndex > 0, "WHERE TaskUser=" & Me.cmbEmployees.SelectedValue & "", "") & ""
                FillDropDown(Me.cmbTask, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            TaskWork = New TaskWorking
            TaskWork.TaskWorkId = MasterId
            TaskWork.TaskWorkDate = Me.dtpDate.Value.Date
            TaskWork.EmployeeId = Me.cmbEmployees.SelectedValue
            TaskWork.TaskId = Me.cmbTask.SelectedValue
            TaskWork.TaskHours = Me.txtHours.Text.ToString.Replace("'", "''")
            TaskWork.TaskRate = Me.txtRate.Text.ToString.Replace("'", "''")
            TaskWork.UserName = LoginUserName
            TaskWork.FeedingDate = Date.Now
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable = New TaskWorkingDAL().GetAllRecords()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbEmployees.SelectedIndex = 0 Then
                Me.cmbEmployees.Focus()
                ShowErrorMessage("Please select employee")
                Return False
            End If
            'If Me.cmbTask.SelectedIndex = 0 Then
            '    Me.cmbTask.Focus()
            '    ShowErrorMessage("Please select task")

            '    Return False
            'End If
            If Me.txtHours.Text = String.Empty Then
                Me.txtHours.Focus()
                ShowErrorMessage("Please enter hours")
                Return False
            End If
            If Me.txtRate.Text = String.Empty Then
                Me.txtRate.Focus()
                ShowErrorMessage("Please enter rate")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            Me.dtpDate.Value = Date.Now.Date
            Me.cmbEmployees.SelectedIndex = 0
            Me.cmbTask.SelectedIndex = 0
            Me.txtHours.Text = String.Empty
            Me.txtRate.Text = String.Empty
            Me.GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Return New TaskWorkingDAL().Add(TaskWork)
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
            Return New TaskWorkingDAL().Update(TaskWork)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub EditRecord(Optional ByVal Condition As String = "")
        Try
            MasterId = Me.grdSaved.GetRow.Cells("TaskWorkId").Value
            Me.dtpDate.Value = Me.grdSaved.GetRow.Cells("TaskWorkDate").Value
            Me.cmbEmployees.SelectedValue = Me.grdSaved.GetRow.Cells("EmployeeId").Value
            Me.cmbTask.SelectedValue = Me.grdSaved.GetRow.Cells("TaskId").Value
            Me.txtHours.Text = Me.grdSaved.GetRow.Cells("Hours").Value
            Me.txtRate.Text = Me.grdSaved.GetRow.Cells("Rate").Value
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not IsValidate() = True Then
                Exit Sub
            Else
                FillModel()
            End If
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
                msg_Information(str_informSave)
                Me.ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Updated Successfully", MsgBoxStyle.Information, str_MessageHeader)
                msg_Information(str_informUpdate)
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmTaskWork_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Employees")
            FillCombos("Tasks")
            ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbEmployees_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmployees.SelectedIndexChanged
        Try
            FillCombos("Tasks")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.cmbEmployees.SelectedIndex = 0 Then
                ShowErrorMessage("Please select record")
                Exit Sub
            End If
            If Not IsValidate() = True Then
                Exit Sub
            Else
                FillModel()
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'MsgBox("Record Deleted Successfully", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
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
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0I
            Id = Me.cmbEmployees.SelectedValue
            FillCombos("Employees")
            Me.cmbEmployees.SelectedValue = Id
            Id = Me.cmbTask.SelectedValue
            FillCombos("Tasks")
            Me.cmbTask.SelectedValue = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
