'26-APRIL-2014 TASK:2591 BY JUNAID SHEHZAD  New Enhancement In Define Shift
''26-May-2014 TASK:M44 Imran Ali OverTime Column Hidden
Imports SBDal
Imports SBModel
Public Class frmDefShift
    Implements IGeneral
    Dim Shift As ShiftBE
    Dim Shift_id As Integer
    Dim IsLoadedForm As Boolean = False

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdShifttype.RootTable.Columns("ShiftStartTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("ShiftEndTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("OverTime_StartTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("BreakStartTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("BreakEndTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("SpecialDayBreakStartTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("SpecialDayBreakEndTime").FormatString = "h:mm:ss tt"
            Me.grdShifttype.RootTable.Columns("OverTimeRate").Visible = False 'Task:M44 Hidden Column
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                If New ShiftDAL().Delete(Shift) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Shift = New ShiftBE
            Shift.Active = Me.chkActive.Checked
            Shift.ShiftCode = Me.txtshiftcode.Text
            Shift.ShiftComments = Me.txtComment.Text
            Shift.ShiftEndDate = IIf(Me.dtpshiftendDate.Checked = True, Me.dtpshiftendDate.Value, Nothing)
            Shift.ShiftEndTime = Me.dtpShiftEndTime.Value.ToLongTimeString
            Shift.OverTime_StartTime = IIf(Me.dtpOTStart.Checked = True, Me.dtpOTStart.Value.ToLongTimeString, Nothing)

            Shift.ShiftId = Me.Shift_id
            Shift.ShiftName = Me.txtshiftname.Text
            Shift.ShiftStartDate = IIf(Me.dtpShiftStartDate.Checked = True, Me.dtpShiftStartDate.Value, Nothing)
            Shift.ShiftStartTime = Me.dtpshiftStartTime.Value.ToLongTimeString
            'Task:2591 Set Flex Time Property here
            Shift.FlexInTime = IIf(Me.dtpTxtFlexIn.Checked = True, Me.dtpTxtFlexIn.Value.ToLongTimeString, Nothing)
            Shift.FlexOutTime = IIf(Me.dtpTxtFlexOut.Checked = True, Me.dtpTxtFlexOut.Value.ToLongTimeString, Nothing)
            'End Task: 2591
            'Task:2591
            Shift.OverTimeRate = Val(Me.txtOverTimeRate.Text)
            'End Task: 2591
            Shift.SortOrder = Val(Me.txtshortorder.Text)
            Shift.BreakStartTime = Me.dtpBreakStartTime.Value.ToLongTimeString
            Shift.BreakEndTime = Me.dtpBreakEndTime.Value.ToLongTimeString

            Shift.SpecialDayBreak = Me.cmbSpecialBreakDay.Text
            Shift.SpecialDayBreakStartTime = Me.dtpSpecialDayBreakStartTime.Value.ToLongTimeString
            Shift.SpecialDayBreakEndTime = Me.dtpSpecialDayBreakEndTime.Value.ToLongTimeString
            Shift.NightShift = Me.chkShiftNight.Checked
            'Shift.MinHalfAbsentHours = Me.txtHalfAbsentHours.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable
            dt = New ShiftDAL().GetAll
            Me.grdShifttype.DataSource = dt
            Me.grdShifttype.RetrieveStructure()
            Me.grdShifttype.RootTable.Columns(0).Visible = False
            Me.grdShifttype.RootTable.Columns("NightShift").Visible = False
            ApplyGridSettings()
            Me.grdShifttype.AutoSizeColumns()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtshiftcode.Text = String.Empty Then
                ShowErrorMessage("Please Enter Shift Code")
                Me.txtshiftcode.Focus()
                Return False
            End If
            If Me.txtshiftname.Text = String.Empty Then
                ShowErrorMessage("Please Enter Shift Name")
                Me.txtshiftname.Focus()
                Return False
            End If
            'task 2591
            'If Me.txtOverTimeRate.Text = String.Empty Then
            '    ShowErrorMessage("Please Enter Over Time Rate")
            '    Me.txtOverTimeRate.Focus()
            '    Return False
            'End If
            'end task 2591
            'If Me.dtpshiftStartTime.Value > Me.dtpShiftEndTime.Value Then
            '    ShowErrorMessage("Starting Time Not Valid")
            '    Me.dtpshiftStartTime.Focus()
            '    Return False

            'End If
            'If Me.dtpShiftEndTime.Value < Me.dtpshiftStartTime.Value Then
            '    ShowErrorMessage("Ending Time Not Valid")
            '    Me.dtpShiftEndTime.Focus()
            '    Return False

            'End If
            'If Me.dtpShiftStartDate.Value > Me.dtpshiftendDate.Value Then
            '    ShowErrorMessage("Start Date Not Valid")
            '    Me.dtpShiftStartDate.Focus()
            '    Return False

            'End If
            'If Me.dtpshiftendDate.Value < Me.dtpShiftStartDate.Value Then
            '    ShowErrorMessage("End Date Not Valid")
            '    Me.dtpshiftendDate.Focus()
            '    Return False
            'End If
            Call FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.Shift_id = 0
            Me.btnSave.Text = "&Save"
            Me.txtshiftcode.Text = String.Empty
            Me.txtshiftname.Text = String.Empty
            dtpshiftStartTime.Value = Date.Now
            Me.dtpShiftEndTime.Value = Date.Now
            Me.dtpOTStart.Value = Date.Now
            Me.dtpBreakStartTime.Value = Date.Now
            Me.dtpBreakEndTime.Value = Date.Now
            Me.cmbSpecialBreakDay.SelectedIndex = 0
            Me.dtpSpecialDayBreakStartTime.Value = Date.Now
            Me.dtpSpecialDayBreakEndTime.Value = Date.Now
            Me.dtpshiftStartTime.Format = DateTimePickerFormat.Time
            Me.dtpShiftEndTime.Format = DateTimePickerFormat.Time
            'Task: 2591
            Me.dtpTxtFlexIn.Format = DateTimePickerFormat.Time
            Me.dtpTxtFlexOut.Format = DateTimePickerFormat.Time
            Me.dtpBreakStartTime.Format = DateTimePickerFormat.Time
            Me.dtpBreakEndTime.Format = DateTimePickerFormat.Time
            Me.dtpSpecialDayBreakStartTime.Format = DateTimePickerFormat.Time
            Me.dtpSpecialDayBreakEndTime.Format = DateTimePickerFormat.Time
            Me.txtOverTimeRate.Text = String.Empty
            'END TASK: 2591 
            Me.dtpShiftStartDate.Value = Date.Now
            Me.dtpshiftendDate.Value = Date.Now
            dtpShiftStartDate.Checked = False
            dtpshiftendDate.Checked = False
            dtpTxtFlexIn.Checked = False
            dtpTxtFlexOut.Checked = False
            Me.txtComment.Text = String.Empty
            Me.txtshortorder.Text = 1
            Me.chkActive.Checked = True
            Me.chkShiftNight.Checked = False
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                If New ShiftDAL().Add(Shift) = True Then
                    Return True
                Else
                    Return False
                End If
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
            If IsValidate() = True Then
                If New ShiftDAL().Update(Shift) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmDefShift_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 11 -1-14
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
    Private Sub frmDefShift_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor

        Me.dtpTxtFlexIn.Format = DateTimePickerFormat.Time
        Me.dtpTxtFlexOut.Format = DateTimePickerFormat.Time
        Application.DoEvents()
        Try
            ReSetControls()
            Me.Timer1.Start()
            Me.Timer1.Enabled = True
            IsLoadedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    Call ReSetControls()
                Else
                    Exit Sub
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    Call ReSetControls()
                Else
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.Shift_id = 0 Then ShowErrorMessage("Please select record") : Exit Sub : Me.grdShifttype.Focus()
            If Not msg_Confirm(str_ConfirmDelete) = True Then
                Exit Sub
            End If
            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informDelete)
                Call ReSetControls()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub grdShifttype_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdShifttype.DoubleClick
        Try
            If Me.grdShifttype.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Shift_id = Me.grdShifttype.GetRow.Cells("ShiftId").Value
            Me.txtshiftcode.Text = Me.grdShifttype.GetRow.Cells("ShiftCode").Value
            Me.txtshiftname.Text = Me.grdShifttype.GetRow.Cells("ShiftName").Value
            Me.txtComment.Text = Me.grdShifttype.GetRow.Cells("ShiftComments").Value.ToString
            'Me.txtHalfAbsentHours.Text = Me.grdShifttype.GetRow.Cells("MinHalfAbsentHours").Value.ToString
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("ShiftStartDate").Value) Then
                Me.dtpShiftStartDate.Value = Me.grdShifttype.GetRow.Cells("ShiftStartDate").Value
                dtpShiftStartDate.Checked = True
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("ShiftEndDate").Value) Then
                Me.dtpshiftendDate.Value = Me.grdShifttype.GetRow.Cells("ShiftEndDate").Value
                dtpshiftendDate.Checked = True
            End If
            dtpshiftStartTime.Format = DateTimePickerFormat.Time
            dtpShiftEndTime.Format = DateTimePickerFormat.Time
            dtpOTStart.Format = DateTimePickerFormat.Time
            dtpSpecialDayBreakStartTime.Format = DateTimePickerFormat.Time
            dtpSpecialDayBreakEndTime.Format = DateTimePickerFormat.Time
            dtpTxtFlexIn.Format = DateTimePickerFormat.Time
            dtpTxtFlexOut.Format = DateTimePickerFormat.Time

            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("ShiftStartTime").Value) Then
                Me.dtpshiftStartTime.Value = CDate(Now.Date & " " & Me.grdShifttype.GetRow.Cells("ShiftStartTime").Value)
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("ShiftEndTime").Value) Then
                Me.dtpShiftEndTime.Value = CDate(Now.Date & " " & Me.grdShifttype.GetRow.Cells("ShiftEndTime").Value)
            End If
            'TASK-421
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("OverTime_StartTime").Value) Then
                Me.dtpOTStart.Value = CDate(Now.Date & " " & Me.grdShifttype.GetRow.Cells("OverTime_StartTime").Value)
            End If
            'Task: 2591 
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("FlexInTime").Value) Then
                Me.dtpTxtFlexIn.Format = DateTimePickerFormat.Time
                Me.dtpTxtFlexIn.Value = CDate(Now.Date & " " & Me.grdShifttype.GetRow.Cells("FlexInTime").Value)
                Me.dtpTxtFlexIn.Checked = True
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("FlexOutTime").Value) Then
                Me.dtpTxtFlexOut.Format = DateTimePickerFormat.Time
                Me.dtpTxtFlexOut.Value = CDate(Now.Date & " " & Me.grdShifttype.GetRow.Cells("FlexOutTime").Value)
                Me.dtpTxtFlexOut.Checked = True

            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("OverTimeRate").Value) Then
                Me.txtOverTimeRate.Text = Val(Me.grdShifttype.GetRow.Cells("OverTimeRate").Value.ToString)
            End If
            'End Task: 2591
            Me.txtshortorder.Text = Me.grdShifttype.GetRow.Cells("SortOrder").Value
            Me.chkActive.Checked = Me.grdShifttype.GetRow.Cells("Active").Value

            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("BreakStartTime").Value) Then
                Me.dtpBreakStartTime.Value = Me.grdShifttype.GetRow.Cells("BreakStartTime").Value
            Else
                Me.dtpBreakStartTime.Value = Date.Now
            End If

            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("BreakEndTime").Value) Then
                Me.dtpBreakEndTime.Value = Me.grdShifttype.GetRow.Cells("BreakEndTime").Value
            Else
                Me.dtpBreakEndTime.Value = Date.Now
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("SpecialDayBreakTime").Value) Then
                Me.cmbSpecialBreakDay.Text = Me.grdShifttype.GetRow.Cells("SpecialDayBreakTime").Value.ToString
            Else
                Me.cmbSpecialBreakDay.SelectedIndex = 0
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("SpecialDayBreakStartTime").Value) Then
                Me.dtpSpecialDayBreakStartTime.Value = Me.grdShifttype.GetRow.Cells("SpecialDayBreakStartTime").Value
            Else
                Me.dtpSpecialDayBreakStartTime.Value = Date.Now
            End If

            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("SpecialDayBreakEndTime").Value) Then
                Me.dtpSpecialDayBreakEndTime.Value = Me.grdShifttype.GetRow.Cells("SpecialDayBreakEndTime").Value
            Else
                Me.dtpSpecialDayBreakEndTime.Value = Date.Now
            End If
            If Not IsDBNull(Me.grdShifttype.GetRow.Cells("NightShift").Value) Then
                Me.chkShiftNight.Checked = Me.grdShifttype.GetRow.Cells("NightShift").Value
            Else
                Me.chkShiftNight.Checked = False
            End If
            GetSecurityRights()
            txtshiftname.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdShifttype_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.dtpshiftStartTime.Format = DateTimePickerFormat.Time
            Me.dtpShiftEndTime.Format = DateTimePickerFormat.Time
            Me.dtpOTStart.Format = DateTimePickerFormat.Time
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
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
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
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
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        'Me.BtnPrint.Enabled = True
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

    Private Sub grdShifttype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdShifttype.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

End Class