'Mughess 12-5-2014 Task No 2624 Adding THe Functionalities in LateTimeSlotForm
'14-MAY-2014 Task 2624 JUNAID SHEHZAD Adding THe Functionalities in LateTimeSlotForm (Modified)
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmlatetimeSlot
    Implements IGeneral
    Dim objLateTimeSlot As New LateTimeSlotBE
    Dim Id As Integer

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If Duplicate(txtSlotStart.Text, txtSlotEnd.Text) Then
                ShowErrorMessage("Duplicate Record Entering ...")
            Else
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then ReSetControls()
                Else
                    If Update1() = True Then ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdLateTimeSlot.RootTable.Columns(0).Visible = False
            Me.grdLateTimeSlot.RootTable.Columns("Slot_Start").Caption = "Start Slot"
            Me.grdLateTimeSlot.RootTable.Columns("Slot_End").Caption = "End Slot"
            Me.grdLateTimeSlot.RootTable.Columns("Active").Caption = "Active"
            Me.grdLateTimeSlot.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New LateTimeSlotDAL().Delete(objLateTimeSlot) = True Then

                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            'Task: 2624 Junaid Shehzad return integer type and save
            objLateTimeSlot.SlotStart = Val(txtSlotStart.Text)
            objLateTimeSlot.SlotEnd = Val(txtSlotEnd.Text)
            'End Task: 2624
            objLateTimeSlot.Active = Me.ChkActive.Checked

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = New LateTimeSlotDAL().GetAll
            Me.grdLateTimeSlot.DataSource = dt
            Me.grdLateTimeSlot.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtSlotStart.Text = String.Empty Then
                ShowErrorMessage("Please enter Slot Strating  Value")
                Me.txtSlotStart.Focus()
                Return False
            ElseIf Me.txtSlotEnd.Text = String.Empty Then
                ShowErrorMessage("Please enter Slot Ending  Value")
                Me.txtSlotEnd.Focus()
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
            Me.btnSave.Text = "&Save"
            txtSlotStart.Text = ""
            txtSlotEnd.Text = ""
            ChkActive.Checked = True
            Me.txtSlotStart.Focus()
            GetAllRecords()

        Catch ex As Exception
            Throw ex
        End Try
        
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New LateTimeSlotDAL().Save(objLateTimeSlot) = True Then

                Return True
            Else
                Return False
            End If
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
            objLateTimeSlot.SlotId = Me.grdLateTimeSlot.GetRow.Cells("Id").Value
            If New LateTimeSlotDAL().Update(objLateTimeSlot) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdLateTimeSlot.RowCount = 0 Then Exit Sub
            objLateTimeSlot = New LateTimeSlotBE

            objLateTimeSlot.SlotId = Me.grdLateTimeSlot.GetRow.Cells("Id").Value
            If New LateTimeSlotDAL().Delete(objLateTimeSlot) = True Then

                ReSetControls()
            Else
                Me.txtSlotStart.Focus()
                Throw New Exception("Can't delete record.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.grdLateTimeSlot.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me.txtSlotStart.Text = Me.grdLateTimeSlot.GetRow.Cells("Slot_Start").Value.ToString
            Me.txtSlotEnd.Text = Val(Me.grdLateTimeSlot.GetRow.Cells("Slot_End").Value.ToString)
            Me.ChkActive.Checked = IIf(grdLateTimeSlot.GetRow.Cells("Active").Value = True, Me.ChkActive.Checked = True, Me.ChkActive.Checked = False)
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

    Private Sub grdLateTimeSlot_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLateTimeSlot.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmlatetimeSlot_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.F5 Then
            ReSetControls()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

   

    Private Sub frmlatetimeSlot_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'End Task 2624

    Private Sub frmlatetimeSlot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    'Task: 2624 Junaid Shehzad
    Private Sub txtSlotStart_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSlotStart.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSlotEnd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSlotEnd.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End task: 2624
    Public Function Duplicate(ByVal SlotStart, ByVal SlotEnd) As Boolean

        Try
            If New LateTimeSlotDAL().CheckDuplicate(SlotStart, SlotEnd) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtSlotStart_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSlotStart.TextChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class