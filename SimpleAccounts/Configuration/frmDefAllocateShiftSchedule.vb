''2-May-2014 TASK:2591 Imran Ali Enhancement In Define Shift
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmDefAllocateShiftSchedule
    Implements IGeneral
    Dim AllocateShift As List(Of AllocateShiftSchedule)
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If Not col.Index = 2 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim dt As DataTable = GetDataTable("Select ShiftId, ShiftName From ShiftTable ORDER BY 2")
            Me.GridEX1.RootTable.Columns("ShiftId").ValueList.PopulateValueList(dt.DefaultView, "ShiftId", "ShiftName")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            Me.GridEX1.UpdateData()
            AllocateShift = New List(Of AllocateShiftSchedule)
            Dim dt As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            'For Each row As DataRow In dt.GetChanges.Rows 'Comment Line Against task:2591
            For Each row As DataRow In dt.Rows
                Dim AllocateShiftDetail As New AllocateShiftSchedule
                AllocateShiftDetail.LoginUserId = LoginUserId
                AllocateShiftDetail.DateTimeLog = Date.Now
                AllocateShiftDetail.ShiftId = Val(row.Item("ShiftId").ToString)
                AllocateShiftDetail.ShiftGroupId = Val(row.Item("ShiftGroupId").ToString)
                AllocateShift.Add(AllocateShiftDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = New AllocateShiftScheduleDAL().GetAll
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            FillModel()
            Call New AllocateShiftScheduleDAL().Add(AllocateShift)
            Return True
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

    End Function

    Private Sub frmDefAllocateShiftSchedule_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

         
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmDefAllocateShiftSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ReSetControls()
            FillCombos()
        Catch ex As Exception
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            If Save() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informSave)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                If Not New AllocateShiftScheduleDAL().Delete(Me.GridEX1.GetRow.Cells("ShiftGroupId").Value, Me.GridEX1.GetRow.Cells("ShiftId").Value) = True Then
                    ShowErrorMessage("Could not delete this record")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
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
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
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
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub lblHeader_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class