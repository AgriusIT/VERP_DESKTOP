'29-Apr-2014 TSK:2592 JUNAID SHEHZAD New Enhancement Employee OverTime Schedule define 
'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
'TASK-421 Muhammad Ameen 04-05-2016: Auto Employee Over Time
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility


Public Class frmEmpOverTimeSchedule
    Implements IGeneral
    Public EmpObj As EmpOverTimeScheduleBE
    Dim OverTimeId As Integer = 0
    Dim IsOpenForm As Boolean = False
    Dim IsHours As Boolean = False
    Dim _Search As New DataTable
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            EmpObj = New EmpOverTimeScheduleBE
            If Not IsDBNull(Me.grdEmpOverTime.CurrentRow.Cells("OverTimeSchId").Value) Then
                EmpObj.OverTimeSchedule_Id = Me.grdEmpOverTime.CurrentRow.Cells("OverTimeSchId").Value
            End If
            If New frmEmpOverTimeScheduleDAL().Delete(EmpObj) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Try
            'Task: 2592 JUNAID SHEHZAD Apply Condition to fill list box
            If Condition = "ShiftGroup" Then
                FillListBox(Me.lstShiftGroup.ListItem, "SELECT * FROM ShiftGroupTable")
            ElseIf Condition = "Emp" Then
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'FillListBox(Me.lstEmployee.ListItem, "SELECT * FROM tblDefEmployee")
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_ID,Employee_Code + '~' + EmployeeDeptName + '~ ' + Employee_Name + '~' + Father_Name  as Employee_Name FROM EmployeesView WHERE IsNull(active,0) = 1 " & IIf(Me.lstShiftGroup.SelectedIDs.Length > 0, "AND ShiftGroupId In (" & IIf(Me.lstShiftGroup.SelectedIDs.Length > 0, lstShiftGroup.SelectedIDs, 0) & ")", "") & "")
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
            End If
            'End task: 2592
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            EmpObj = New EmpOverTimeScheduleBE
            Dim EmpDalObj As New frmEmpOverTimeScheduleDAL
            Dim rowCount As Integer = 0
            'For rowCount To grdEmpOverTime.RowCount-1
            For Each dr As Janus.Windows.GridEX.GridEXRow In grdEmpOverTime.GetRows
                'EmpObj.OverTimeSchedule_Id = dr.Cells("OverTimeScheduleId").Value
                'EmpObj.Emp_Name = dr.Cells("Employee_Name").Value.ToString
                'EmpObj.Emp_Address = dr.Cells("Address").Value.ToString
                'EmpObj.Emp_Mobile = dr.Cells("Mobile").Value.ToString
                If Not IsDBNull(dr.Cells("Start_Date").Value) Then
                    If Not IsDBNull(dr.Cells("OverTimeSchId").Value) Then
                        EmpObj.OverTimeSchedule_Id = dr.Cells("OverTimeSchId").Value
                    End If

                    If Not IsDBNull(dr.Cells("EmployeeId").Value) Then
                        EmpObj.Employee_OverTime_Id = dr.Cells("EmployeeId").Value
                    End If

                    If Not IsDBNull(dr.Cells("Start_Date").Value) Then
                        EmpObj.Start_Date = dr.Cells("Start_Date").Value
                    End If

                    If Not IsDBNull(dr.Cells("End_Date").Value) Then
                        EmpObj.End_Date = dr.Cells("End_Date").Value
                    End If


                    If Not IsDBNull(dr.Cells("Start_Time").Value.ToString) Then
                        EmpObj.Start_Time = dr.Cells("Start_Time").Value.ToString
                    End If

                    If Not IsDBNull(dr.Cells("End_Time").Value) Then
                        EmpObj.End_Time = dr.Cells("End_Time").Value
                    End If

                    If Not IsDBNull(dr.Cells("Active").Value) Then
                        EmpObj.Active = dr.Cells("Active").Value
                    End If
                    If Not IsDBNull(dr.Cells("OverTime_Rate_HR").Value) Then
                        EmpObj.Emp_OverTimeRate = Val(dr.Cells("OverTime_Rate_HR").Value.ToString)
                    End If

                    If Not IsDBNull(dr.Cells("Employee_ID").Value) Then
                        EmpObj.Employee_Id = Val(dr.Cells("Employee_ID").Value.ToString)
                    End If
                    'If Not IsDBNull(dr.Cells("OverTime_StartTime").Value) Then
                    '    EmpObj.OverTime_StartTime = dr.Cells("OverTime_StartTime").Value
                    'End If

                    If Me.btnSave.Text = "&Save" Then
                        'Task 2592  Update Employee Over Time Records
                        EmpDalObj.Save(EmpObj)
                        'End Task: 2592
                    End If
                    If Me.btnSave.Text = "&Update" Then
                        EmpDalObj.Update(EmpObj)
                    End If
                End If
            Next

            ReSetControls()



            'empobj.Employee_Id= 
            'empobj.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnSave.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnSave.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            'Task: 2592 Show Records in Employee Grid when Doulbe Click on Hisotry Grid
            If Condition = "Detail Record" Then

                Dim dtdetail As DataTable
                Dim empDalObj As New frmEmpOverTimeScheduleDAL
                
                'Me.grdEmpOverTime.RootTable.Columns("OverTimeScheduleId").Visible = False
                'Dim dtRw As DataRow
                'Dim EmpId As Integer

                'If Not IsDBNull(Me.grdEmpHistory.CurrentRow.Cells("OverTimeSchId").Value) = True Then
                '    OverTimeId = Me.grdEmpHistory.CurrentRow.Cells("OverTimeSchId").Value
                'Else
                '    ShowErrorMessage("Record could not be found")
                '    Me.grdEmpOverTime.DataSource = Nothing
                '    Me.btnSave.Text = "&Save"
                '    Exit Sub
                'End If

                'EmpId = Me.grdEmpHistory.CurrentRow.Cells("Employee_ID").Value

                dtdetail = empDalObj.GetDetailRecord(OverTimeId)

                'If New DateSheetBL().CheckExisting(Me.cmbclass.SelectedIndex, Me.cmbExam.SelectedIndex) = True Then
                ' Me.grdDateSheet.DataSource = dtdetail
                ' Me.btnSave.Text = "&Update"
                'Else
                '    Me.grdDateSheet.DataSource = dtdetail
                'End If

                Me.grdEmpOverTime.DataSource = dtdetail
                Me.grdEmpOverTime.RetrieveStructure()
                Me.grdEmpOverTime.RootTable.Columns("Employee_Id").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("OverTimeSchId").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("EmployeeId").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("Start_Date").FormatString = "dd/MMM/yyyy"
                Me.grdEmpOverTime.RootTable.Columns("End_Date").FormatString = "dd/MMM/yyyy"
                Me.grdEmpOverTime.RootTable.Columns("Start_Time").FormatString = "hh:mm:ss tt"
                Me.grdEmpOverTime.RootTable.Columns("End_Time").FormatString = "hh:mm:ss tt"
                'Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").Caption = "Rate Per Hour"
                Me.grdEmpOverTime.RootTable.Columns("EmployeeId").Visible = False
                'Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                'Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                'Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").FormatString = "N" & DecimalPointInValue

                'Me.grdEmpHistory.RootTable.Columns("Start_Time").CellStyle
                'Me.ls = Me.grdEmpOverTime.RootTable.Columns("Employee_Id").ValueList

                Me.grdEmpOverTime.AutoSizeColumns()
                'Me.btnSave.Text = "&Update"
                ApplyGridSettings()
            End If
            'End Task: 2592



            'Task: 2592 Load Employee History Grid Records When Shown event is called
            If Condition = "Master Record" Then
                Me.grdEmpHistory.DataSource = New frmEmpOverTimeScheduleDAL().GetAllRecord()
                grdEmpHistory.RetrieveStructure()
                'Me.grdEmpHistory.RootTable.Columns("OverTimeScheduleId").Visible = False
                Me.grdEmpHistory.RootTable.Columns("Employee_ID").Visible = False
                'Me.grdBranchDetail.RootTable.Columns("BranchCode").Visible = False
                Me.grdEmpHistory.RootTable.Columns("Father_Name").Visible = False
                'Me.grdEmpHistory.RootTable.Columns("Address").Visible = False
                'Me.grdBranchDetail.RootTable.Columns("Address").Visible = False

                Me.grdEmpHistory.RootTable.Columns("OverTimeSchId").Visible = False
                Me.grdEmpHistory.RootTable.Columns("EmployeeId").Visible = False
                Me.grdEmpHistory.RootTable.Columns("Start_Date").Visible = True
                Me.grdEmpHistory.RootTable.Columns("End_Date").Visible = True
                Me.grdEmpHistory.RootTable.Columns("Start_Time").Visible = True
                Me.grdEmpHistory.RootTable.Columns("End_Time").Visible = True
                'Me.grdEmpOverTime.RootTable.Columns("OverTimeScheduleId").Visible = False
                Me.grdEmpHistory.RootTable.Columns("Active").Visible = False
                'Me.grdBranchDetail.RootTable.Columns("State").Visible = False

                grdEmpHistory.AutoSizeColumns()
                'End Task: 2592

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function GetDetailRecord(ByVal dr As DataRow) As DataTable   ' That Record Get from tblDefSubject and tblExamDetail For DateSheet Tab
        'Try
        '    Dim obj As New frmEmpOverTimeScheduleDAL()
        '    Return obj.GetDetailRecord(dr)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            'If Me.lstShiftGroup.SelectedItems = 0 Then
            '    ShowErrorMessage("Please select Group")
            '    Me.lstShiftGroup.Focus()
            '    Return False
            'End If

            'task: 2592 Call FillModel Method
            FillModel()
            'END TASK: 2592
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'Dim dt As New DataTable
            'Me.grdEmpOverTime.DataSource = Nothing
            'Me.lstShiftGroup.Select()
            'Me.lstEmployee.SelectedItems = -1
            'Task: 2592 Retrieve Detail related to Employee and Over Time Info
            'GetAllRecords()
            'END TASK: 2592
            Me.btnSave.Text = "&Save"
            Me.lstEmployee.ListItem.SelectedItems.Clear()
            'If Me.grdEmpOverTime.RowCount = 0 Then Exit Sub
            OverTimeId = 0
            Me.DateTimePicker1.Value = Now.Date
            Me.DateTimePicker2.Value = Now.Date
            Me.dtpEndTime.Value = Now
            Me.dtpStartTime.Value = Now
            Me.dtpEndTime.Format = DateTimePickerFormat.Time
            Me.dtpStartTime.Format = DateTimePickerFormat.Time
            Me.dtpStartTime.Checked = False
            Me.dtpEndTime.Checked = False

            GetAllRecords("Master Record")
            GetAllRecords("Detail Record")

            'Dim dt As DataTable = CType(grdEmpOverTime.DataSource, DataTable)
            'dt.Clear()
            ApplySecurity(EnumDataMode.[New])

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try
            EmpObj = New EmpOverTimeScheduleBE
            If New frmEmpOverTimeScheduleDAL().Save(EmpObj) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
    Private Sub frmEmpOverTimeSchedule_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'Task: 2592 Fil4l list box when form is shown
            FillCombos("ShiftGroup")
            FillCombos("Emp")
            GetSecurityRights()
            'GetAllRecords("Master Record")
            'ReSetControls()
            'End Task: 2592
            Me.dtpEndTime.Format = DateTimePickerFormat.Time
            Me.dtpStartTime.Format = DateTimePickerFormat.Time
            ReSetControls()
            _Search = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            'Task: 2592 Show Data in Grip based on Selected IDs of Employee List Box
            Dim empOverTimeObj As New frmEmpOverTimeScheduleDAL
            Dim lstEmpIds As String = lstEmployee.SelectedIDs


            If lstEmpIds.Length <= 0 Then
                ShowErrorMessage("Please select employee.")
                Exit Sub
            End If
            If Me.dtpStartTime.Checked = False AndAlso Me.dtpEndTime.Checked = False Then
                If Val(Me.txtHours.Text) = 0 Then
                    ShowErrorMessage("Please enter overtime hours.")
                    Me.txtHours.Focus()
                    Exit Sub
                End If
            End If

            Dim dt As New DataTable
            Me.btnSave.Text = "&Save"
            dt = empOverTimeObj.DisplayEmpOverTimeDetail(IIf(lstEmpIds.Length > 0, lstEmpIds, 0))
            'TASK: 2592 Check whether data table is empty or not
            If dt.Rows.Count > 0 Then
                For Each objrow As DataRow In dt.Rows
                    objrow.BeginEdit()

                    Dim shiftstartDate As DateTime = CDate(Me.DateTimePicker1.Value.Date & " " & objrow.Item("ShiftStartTime"))
                    Dim shiftendDate As DateTime = CDate(Me.DateTimePicker1.Value.Date & " " & objrow.Item("ShiftEndTime"))
                    Dim totalHours As Double = 0D

                    If getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString.Replace("Error", "False").Replace("''", "False") = "False" Then
                        totalHours = Convert.ToDouble(DateDiff(DateInterval.Minute, shiftstartDate, shiftendDate) / 60)
                    Else
                        totalHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
                    End If
                    ServerDate()
                    Dim intDayinMonth As Integer
                    'If Not getConfigValueByType("KeepConfigurationMonthDays").ToString.ToUpper = "TRUE" Then
                    '    intDayinMonth = DateTime.DaysInMonth(Me.DateTimePicker1.Value.Year, Me.DateTimePicker1.Value.Month) 'Val(getConfigValueByType("Working_Days").ToString)
                    'Else
                    '    intDayinMonth = Val(getConfigValueByType("Working_Days").ToString)
                    'End If
                    If Not getConfigValueByType("OverTimeBasedOnWorkingDays").ToString = "True" Then
                        intDayinMonth = DateTime.DaysInMonth(Me.DateTimePicker1.Value.Year, Me.DateTimePicker1.Value.Month)
                    Else
                        intDayinMonth = Val(getConfigValueByType("OverTimeWorkingDays").ToString)
                    End If
                    Dim NormalMul As String = String.Empty
                    If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                        NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
                    Else
                        NormalMul = 1
                    End If
                    'Dim overtimehoursamount = ((Val(objrow.Item("Salary").ToString) / Date.DaysInMonth(GetServerDate.Year, GetServerDate.Month)) / IIf(totalHours = 0, 1, totalHours))
                    Dim overtimehoursamount = (((Val(objrow.Item("Salary").ToString) / intDayinMonth) / IIf(totalHours = 0, 1, totalHours)) * NormalMul)
                    objrow.Item("Start_Date") = Me.DateTimePicker1.Value
                    objrow.Item("End_Date") = Me.DateTimePicker2.Value
                    If Not Me.dtpStartTime.Checked = True Then
                        objrow.Item("Start_Time") = shiftendDate.ToLongTimeString
                        objrow.Item("End_Time") = shiftendDate.AddHours(Val(Me.txtHours.Text)).ToLongTimeString
                    Else
                        objrow.Item("Start_Time") = Me.dtpStartTime.Value.ToLongTimeString
                        objrow.Item("End_Time") = Me.dtpEndTime.Value.ToLongTimeString
                    End If
                    'If Me.dtpOTStart.Checked = True Then
                    '    objrow.Item("OverTime_StartTime") = Me.dtpOTStart.Value.ToLongTimeString
                    'End If
                    If Val(objrow.Item("Working_Hours").ToString) = 0 Then

                        objrow.Item("Working_Hours") = totalHours
                    End If
                    objrow.Item("OverTime_Rate_HR") = overtimehoursamount
                    objrow.Item("Active") = 1
                    objrow.EndEdit()
                Next

                dt.AcceptChanges()
                grdEmpOverTime.DataSource = dt
                grdEmpOverTime.RetrieveStructure()

                Me.grdEmpOverTime.RootTable.Columns("Employee_Id").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("OverTimeSchId").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("EmployeeId").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("Salary").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("Working_Hours").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("ShiftStartTime").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("ShiftEndTime").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("Start_Date").FormatString = "dd/MMM/yyyy"
                Me.grdEmpOverTime.RootTable.Columns("End_Date").FormatString = "dd/MMM/yyyy"
                Me.grdEmpOverTime.RootTable.Columns("Start_Time").FormatString = "hh:mm:ss tt"
                Me.grdEmpOverTime.RootTable.Columns("End_Time").FormatString = "hh:mm:ss tt"
                Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").Caption = "Rate Per Hour"
                Me.grdEmpOverTime.RootTable.Columns("EmployeeId").Visible = False
                Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmpOverTime.RootTable.Columns("OverTime_Rate_HR").FormatString = "N" & DecimalPointInValue

                For Each col As Janus.Windows.GridEX.GridEXColumn In grdEmpOverTime.RootTable.Columns
                    If col.Index = 0 Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    ElseIf col.Index = 1 Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    ElseIf col.Index = 2 Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    ElseIf col.Index = 3 Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                        'col.EditType = Janus.Windows.GridEX.EditType.Custom
                    End If
                Next
            End If
            'end task: 2592
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstShiftGroup_SelectedIndexChaned(ByVal sender As System.Object, ByVal e As SimpleAccounts.IndexEventArgs) Handles lstShiftGroup.SelectedIndexChaned
        Try
            'task: 2592 BY JUNAID SHEHZAD Fill Employee List box as Group List Item changes
            'FillListBox(Me.lstEmployee.ListItem, "SELECT dbo.tblDefEmployee.Employee_Name FROM dbo.tblDefEmployee INNER JOIN dbo.ShiftGroupTable ON dbo.tblDefEmployee.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId WHERE ShiftGroupId In (" & IIf(Me.lstShiftGroup.SelectedIDs.Length > 0, lstShiftGroup.SelectedIDs, 0) & ")")
            If IsOpenForm = False Then Exit Sub
            If Me.lstShiftGroup.SelectedIDs.Length > 0 Then
                FillCombos("Emp")
                _Search = CType(lstEmployee.ListItem.DataSource, DataTable)
            End If


            'end task: 2592
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                'If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'If Not msg_Confirm(str_ConfirmSave) = True Then
                'Exit Sub
                'End If
                'If Save() = True Then
                'DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informSave)
                'ReSetControls()
                'End If
                'Else
                'If Not msg_Confirm(str_ConfirmUpdate) = True Then
                'Exit Sub
                'End If
                'If Update1() = True Then
                'DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informUpdate)
                'ReSetControls()
                'End If
                'End If
                'Else
                'Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grdEmpHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdEmpHistory.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub EditRecords()
        Try
            If Me.grdEmpHistory.RowCount = 0 Then Exit Sub

            ' TabControl1.SelectedTab = TabControl1.TabPages(0)
            'Me.tabHistory.SelectedTab = Me.tabHistory.Tabs(0).TabPage.Tab
            If Not IsDBNull(Me.grdEmpHistory.CurrentRow.Cells("OverTimeSchId").Value) = True Then
                OverTimeId = Me.grdEmpHistory.CurrentRow.Cells("OverTimeSchId").Value
                Me.btnSave.Text = "&Update"
            Else
                ShowErrorMessage("Record could not be found")
                Me.grdEmpOverTime.DataSource = Nothing
                Me.btnSave.Text = "&Save"
                Exit Sub
            End If
            GetAllRecords("Detail Record")

            For Each col As Janus.Windows.GridEX.GridEXColumn In grdEmpOverTime.RootTable.Columns
                If col.Index = 0 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                ElseIf col.Index = 1 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                ElseIf col.Index = 2 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                ElseIf col.Index = 3 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    'col.EditType = Janus.Windows.GridEX.EditType.Custom
                End If
            Next
            'EmpObj = New EmpOverTimeScheduleBE
            'Me.chkBrActive.Checked = Me.grdBranchDetail.CurrentRow.Cells("Active").Value
            'End If
            Me.TabEmpHistory.SelectedTab = Me.TabEmpHistory.Tabs(0).TabPage.Tab

            ''Me.btnPrintCard.Enabled = True
            'Me.btnSave.Text = "&Update"
            'ReSetControls()
            'ApplySecurity()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Delete() = False Then
                ShowErrorMessage("Data could not be deleted")
            End If
            ReSetControls()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'task: 2592 Load all controls again
        Try
            FillCombos("ShiftGroup")
            FillCombos("Emp")
            'GetAllRecords("Master Record")
            'Me.btnSave.Text = "&Save"
            'Me.grdEmpOverTime.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'end task: 2592
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            'Me.lstEmployee.ListItem.SelectedItems.Clear()
            'If Me.grdEmpOverTime.RowCount = 0 Then Exit Sub
            'Me.DateTimePicker1.Value = Now
            'Me.DateTimePicker2.Value = Now.AddDays(15)
            'Me.DateTimePicker3.Value = Now
            'Me.DateTimePicker4.Value = Now
            'Me.DateTimePicker3.Format = DateTimePickerFormat.Time
            'Me.DateTimePicker4.Format = DateTimePickerFormat.Time
            'GetAllRecords("Master Record")
            'Dim dt As DataTable = CType(grdEmpOverTime.DataSource, DataTable)
            'dt.Clear()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtHours_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHours.KeyPress
        NumValidation(sender, e)
    End Sub

    Private Sub dtpStartTime_Leave(sender As Object, e As EventArgs) Handles dtpStartTime.Leave, dtpEndTime.Leave
        Try

            If Me.dtpStartTime.Checked = True Or Me.dtpEndTime.Checked = True Then
                IsHours = False
                Me.txtHours.Text = 0
                Me.txtHours.ReadOnly = True
                Me.dtpEndTime.Checked = True
            Else
                IsHours = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try

            Dim dv As New DataView
            _Search.TableName = "Default"
            _Search.CaseSensitive = False
            dv.Table = _Search
            dv.RowFilter = "Employee_Name Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class