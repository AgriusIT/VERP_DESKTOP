'Task No 2608 Mughees 6-5-2014 To Add New Form and functionalities related to this form 
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmDefLeaveEncashment
    Implements IGeneral

    Dim objLeave As New LeaveEncashmentBE
    Dim ID As Integer = 0I
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                If Save() = True Then ReSetControls()
            Else
                If Update1() = True Then ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            objLeave.LeaveEncashmentId = ID
            objLeave.LeaveEncashment = txtLeaveEncashment.Text
            objLeave.TotalWorkingDays = txtTotalWorkDay.Text
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New LeaveEncashmentDAL().Save(objLeave) = True Then

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("Step1").Caption = "Working Days"
            Me.grd.RootTable.Columns("Step2").Caption = "Leave Encashment"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New LeaveEncashmentDAL().Delete(objLeave) = True Then

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            dt = New LeaveEncashmentDAL().GetAll
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtLeaveEncashment.Text = String.Empty Then
                ShowErrorMessage("Please enter LeaveEncashment Value")
                Me.txtLeaveEncashment.Focus()
                Return False
            ElseIf Me.txtTotalWorkDay.Text = String.Empty Then
                ShowErrorMessage("Please enter Total WorkingDays Value")
                Me.txtTotalWorkDay.Focus()
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
            Me.BtnSave.Text = "&Save"
            ID = 0I
            Me.txtLeaveEncashment.Text = String.Empty
            Me.txtTotalWorkDay.Text = String.Empty
            Me.txtTotalWorkDay.Focus()
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New LeaveEncashmentDAL().Update(objLeave) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.BtnSave.Text = "&Update"
            ID = Val(Me.grd.GetRow.Cells("DetailId").Value.ToString)
            Me.txtLeaveEncashment.Text = Me.grd.GetRow.Cells("Step2").Value.ToString
            Me.txtTotalWorkDay.Text = Val(Me.grd.GetRow.Cells("Step1").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            objLeave = New LeaveEncashmentBE

            objLeave.LeaveEncashmentId = Me.grd.GetRow.Cells("DetailId").Value
            If New LeaveEncashmentDAL().Delete(objLeave) = True Then
                ReSetControls()
            Else
                Me.txtLeaveEncashment.Focus()
                Throw New Exception("Can't delete record.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            BtnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDefLeaveEncashment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDefLeaveEncashment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class