''TASK TFS4876 Done by Muhammad Amin on 01-11-2018: Cost Center wise employees attendance.

Imports System.Data.OleDb

Public Class frmHolidySetup
    Dim CurrentId As Integer
    Dim IsOpenForm As Boolean = False
    Dim _SearchDt As New DataTable
    Dim flgCostCenterRights As Boolean = False ''TFS3566
    Private Sub FillCombo()
        Try

            FillDropDown(Me.CmbStatus, "Select Att_Status_ID, Att_Status_Name, Att_Status_Code, Active From tblDefAttendenceStatus WHERE Active=1 Order By SortOrder ASC", False)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillList()
        Try
            FillListBox(Me.LstDepartment, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable Order BY EmployeeDeptId ASC")
            FillListBox(Me.LstDesignation, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable Order BY EmployeeDesignationId ASC")
            If flgCostCenterRights = False Then
                FillListBox(Me.LstEmployees.ListItem, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Code + '~' + dbo.EmployeeDeptDefTable.EmployeeDeptName + '~'  + tblDefEmployee.Employee_Name  + '~'  + tblDefEmployee.Father_Name as Employee_Name from tblDefEmployee LEFT OUTER JOIN  EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId  Where tblDefEmployee.Active = 1 Order BY tblDefEmployee.Employee_ID ASC") ''TASKTFS75 added and set active =1
                FillListBox(Me.lstAddedEmployees.ListItem, "Select Employee_ID, Employee_Code + '~'  + Employee_Name as Employee_Name From tblDefEmployee Where Employee_Id=-1 And Active = 1 Order BY Employee_ID ASC") ''TASKTFS75 added and set active =1
            Else
                '' TASK TFS4876
                FillListBox(Me.LstEmployees.ListItem, "Select tblDefEmployee.Employee_ID, tblDefEmployee.Employee_Code + '~' + dbo.EmployeeDeptDefTable.EmployeeDeptName + '~'  + tblDefEmployee.Employee_Name  + '~'  + tblDefEmployee.Father_Name as Employee_Name from tblDefEmployee LEFT OUTER JOIN  EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId  Where tblDefEmployee.Active = 1 AND ISNULL(CostCentre, 0) IN (SELECT ISNULL(CostCentre_Id, 0) AS CostCenterId FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) Order BY tblDefEmployee.Employee_ID ASC") ''TASKTFS75 added and set active =1
                FillListBox(Me.lstAddedEmployees.ListItem, "Select Employee_ID, Employee_Code + '~'  + Employee_Name as Employee_Name From tblDefEmployee Where Employee_Id=-1 And Active = 1 AND ISNULL(CostCentre, 0) IN (SELECT ISNULL(CostCentre_Id, 0) AS CostCenterId FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) Order BY Employee_ID ASC") ''TASKTFS75 added and set active =1
                '' END TASK TFS4876
            End If
            ''Start TFS3569 : Ayesha Rehman
            FillListBox(Me.LstCostCenter, IIf(flgCostCenterRights = True, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1", "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 "))
            ''End TFS3569
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RdbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbAll.CheckedChanged
        Try
            PnDat.Visible = False
            LstDepartment.Visible = False
            LstDesignation.Visible = False
            LstEmployees.Visible = False
            LstCostCenter.Visible = False ''TFS3569
            Me.txtSearch.Visible = False
            plnEmp.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub RdbDept_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbDept.CheckedChanged
        Try
            PnDat.Visible = True
            LstDesignation.Visible = False
            LstEmployees.Visible = False
            LstDepartment.Visible = True
            LstCostCenter.Visible = False  ''TFS3569
            Me.txtSearch.Visible = False
            plnEmp.Visible = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub RdbDesig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbDesig.CheckedChanged
        Try
            PnDat.Visible = True
            LstDepartment.Visible = False
            LstDesignation.Visible = True
            LstEmployees.Visible = False
            Me.txtSearch.Visible = False
            LstCostCenter.Visible = False ''TFS3569
            plnEmp.Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RdbEmp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbEmp.CheckedChanged
        Try
            PnDat.Visible = True
            LstDepartment.Visible = False
            LstDesignation.Visible = False
            LstCostCenter.Visible = False
            LstEmployees.Visible = True
            Me.txtSearch.Visible = True
            plnEmp.Visible = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            GetDays()
            If Me.BtnSave.Text = "&Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    Call ReSetControls()
                Else
                    Exit Sub
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
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

    'Private Function ValidateCostCenter() As Boolean
    '    ''Start TFS3566
    '    If flgCostCenterRights = True Then
    '        If Not Me.LstCostCenter.SelectedItems.Count > 0 Then
    '            msg_Error("Please select a Cost Center") : Me.LstCostCenter.Focus() : Return False : Exit Function
    '        End If
    '    End If
    '    ''End TFS3566  
    '    Return True
    'End Function
    Public Function Save(Optional ByVal Condition As String = "") As Boolean
        Dim objtrans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        objtrans = Con.BeginTransaction
        Dim cm As New OleDbCommand
        Dim Identity As Integer = 0I
        Try
            Dim DDate As Date
            Dim str As String = String.Empty
            Dim TType As Integer = 0
            If RdbAll.Checked = True Then
                TType = 0
            ElseIf RdbDept.Checked = True Then
                TType = 1
            ElseIf RdbDesig.Checked = True Then
                TType = 2
            ElseIf RdbEmp.Checked = True Then
                TType = 3
                ''Start TFS3569 : Ayesha Rehman : 19-06-2018
            ElseIf RdbCostCenter.Checked = True Then
                TType = 4
                ''End TFS3569
            End If


            cm.Connection = Con
            cm.Transaction = objtrans
            cm.CommandTimeout = 120

            Dim IDs As String = String.Empty
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = ""
                cm.CommandText = "insert into tblholidaysetup(AttendancaeStatus,StartDate,EndDate,totaldays,remarks,HType) values(N'" & Me.CmbStatus.Text & "',N'" & DtpStartDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & DtpEndDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & Me.TxtTotDays.Text.ToString.Replace("'", "''") & "',N'" & Me.TxtRemarks.Text.ToString.Replace("'", "''") & "', " & TType & ") Select @@Identity"
                Identity = Convert.ToInt32(cm.ExecuteScalar())
            Else
                cm.CommandText = ""
                cm.CommandText = "update tblholidaysetup set  AttendancaeStatus = N'" & Me.CmbStatus.Text & "' ,StartDate = N'" & DtpStartDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',EndDate = N'" & DtpEndDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',totaldays = N'" & Me.TxtTotDays.Text.ToString.Replace("'", "''") & "',remarks= N'" & Me.TxtRemarks.Text.ToString.Replace("'", "''") & "',HType =  " & TType & " where HSCode=" & Me.CurrentId
                cm.ExecuteNonQuery()
                Identity = Me.CurrentId
            End If

            cm.CommandText = ""
            cm.CommandText = "delete from tblattendancedetail where policyid = " & Identity & ""
            cm.ExecuteNonQuery()

            TxtTotDays.Text = DateDiff(DateInterval.Day, DtpStartDate.Value, DtpEndDate.Value) + 1
            For a As Integer = 0 To Val(TxtTotDays.Text) - 1
                DDate = DateAdd(DateInterval.Day, a, DtpStartDate.Value)
                str = String.Empty
                ''Query Edit Against TFS3569
                ''Below lines are commented against TASK TFS4876
                'str = "Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceStatus,AttendanceTime,PolicyID, Auto) " _
                '              & " select employee_id, N'" & DDate.ToString("yyyy-M-d 00:00:00") & "', " & IIf(CmbStatus.Text.Contains("Present") = True, "'In'", "Null") & ",'" & CmbStatus.Text & "'," & IIf(Me.cmbState.Text.ToUpper <> "PRESENT", "NULL", "Convert(Datetime,'" & DDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & Identity & ", 0 from tbldefemployee a " _
                '              & " left join EmployeeDeptDefTable b on a.dept_id = b.employeedeptid " _
                '              & " left join EmployeeDesignationDefTable c on a.desig_id = c.employeedesignationid " _
                '              & " left join ShiftTable d on a.shiftgroupid = d.shiftid " _
                '              & " left join EmployeeDeptDefTable e on a.dept_id = e.employeedeptid " _
                '              & " left join dbo.tbldefcostcenter on a.CostCentre = dbo.tbldefcostcenter.CostCenterID " _
                '              & " where a.active = 1  "
                ''TASK TFS4876 Addition of Cost Center filter. done on 01-11-2018 by Muhammad Amin
                str = "Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceStatus,AttendanceTime,PolicyID, Auto) " _
                              & " select employee_id, N'" & DDate.ToString("yyyy-M-d 00:00:00") & "', " & IIf(CmbStatus.Text.Contains("Present") = True, "'In'", "Null") & ",'" & CmbStatus.Text & "'," & IIf(Me.cmbState.Text.ToUpper <> "PRESENT", "NULL", "Convert(Datetime,'" & DDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & Identity & ", 0 from tbldefemployee a " _
                              & " left join EmployeeDeptDefTable b on a.dept_id = b.employeedeptid " _
                              & " left join EmployeeDesignationDefTable c on a.desig_id = c.employeedesignationid " _
                              & " left join ShiftTable d on a.shiftgroupid = d.shiftid " _
                              & " left join EmployeeDeptDefTable e on a.dept_id = e.employeedeptid " _
                              & " left join dbo.tbldefcostcenter on a.CostCentre = dbo.tbldefcostcenter.CostCenterID " _
                              & " where ISNULL(a.active, 0) = 1  " & IIf(flgCostCenterRights = True, " AND ISNULL(dbo.tbldefcostcenter.CostCenterID, 0) IN (SELECT ISNULL(CostCentre_Id, 0) AS CostCenterId FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & " ) ", "") & ""
                If TType = 0 Then
                    cm.CommandText = ""
                    cm.CommandText = str
                    cm.ExecuteScalar()
                ElseIf TType = 1 Then  ' Department Wise
                    For Each obj As Object In Me.LstDepartment.SelectedItems
                        If TypeOf obj Is DataRowView Then
                            Dim dr As DataRowView = CType(obj, DataRowView)
                            IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.LstDepartment.ValueMember).ColumnName)
                        End If
                    Next
                    If IDs.Length > 0 Then
                        str = str & "and a.dept_id in (" & IDs & ")"
                    End If
                    cm.CommandText = ""
                    cm.CommandText = str
                    cm.ExecuteScalar()
                    ElseIf TType = 2 Then
                        For Each obj As Object In Me.LstDesignation.SelectedItems
                            If TypeOf obj Is DataRowView Then
                                Dim dr As DataRowView = CType(obj, DataRowView)
                                IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.LstDesignation.ValueMember).ColumnName)
                            End If
                    Next
                    If IDs.Length > 0 Then
                        str = str & "and a.desig_id in (" & IDs & ")"
                    End If
                    cm.CommandText = ""
                    cm.CommandText = str
                    cm.ExecuteScalar()
                    ElseIf TType = 3 Then
                        'For Each obj As Object In Me.LstEmployees.SelectedItems
                        For Each obj As Object In Me.lstAddedEmployees.ListItem.Items
                            If TypeOf obj Is DataRowView Then
                                Dim dr As DataRowView = CType(obj, DataRowView)
                                IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.LstEmployees.ListItem.ValueMember).ColumnName)
                            End If
                    Next
                    If IDs.Length > 0 Then
                        str = str & "and a.Employee_ID in (" & IDs & ")"
                    End If
                    cm.CommandText = ""
                    cm.CommandText = str
                    cm.ExecuteScalar()
                    ''Start TFS3569 : Ayesha Rehman : 19-06-2018
                    ElseIf TType = 4 Then
                        'For Each obj As Object In Me.LstEmployees.SelectedItems
                        For Each obj As Object In Me.LstCostCenter.SelectedItems
                            If TypeOf obj Is DataRowView Then
                                Dim dr As DataRowView = CType(obj, DataRowView)
                                IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.LstCostCenter.ValueMember).ColumnName)
                            End If
                    Next
                    If IDs.Length > 0 Then
                        str = str & "and a.CostCentre in (" & IDs & ")"
                    End If
                    cm.CommandText = ""
                    cm.CommandText = str
                    cm.ExecuteScalar()
                    'End TFS3569
                    End If
            Next

            objtrans.Commit()






        Catch ex As Exception
            objtrans.Rollback()
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
            ReSetControls()
        End Try
        SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", Identity, Me.CurrentId), True)
        Me.CurrentId = 0
        Return True

    End Function
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean
        Try
            If DtpEndDate.Value < DtpStartDate.Value Then
                ShowErrorMessage("End date should be greater than start date")
                Me.DtpEndDate.Focus()
                Return False
            End If
            If Me.TxtRemarks.Text = String.Empty Then
                ShowErrorMessage("Please Enter remarks")
                Me.TxtRemarks.Focus()
                Return False
            End If

            If Me.CmbStatus.SelectedValue = 0 Then
                ShowErrorMessage("Please select holiday type")
                Me.CmbStatus.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetDays()
        Try
            TxtTotDays.Text = DateDiff(DateInterval.Day, DtpStartDate.Value, DtpEndDate.Value) + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub TxtRemarks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRemarks.GotFocus
    '    Try
    '        GetDays()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Sub ReSetControls(Optional ByVal Condition As String = "")
        Try
            Me.BtnSave.Text = "&Save"
            Me.TxtRemarks.Text = String.Empty
            Me.TxtTotDays.Text = String.Empty
            Me.DtpStartDate.Value = Date.Now
            Me.DtpEndDate.Value = Date.Now
            Me.CmbStatus.SelectedIndex = 0
            Call DisplayRecord()
            RdbAll.Checked = True
            PnDat.Visible = False
            LstDepartment.ClearSelected()
            LstEmployees.ListItem.ClearSelected()
            LstDesignation.ClearSelected()
            LstCostCenter.ClearSelected() ''TFS3569
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.SplitContainer1.Panel2Collapsed = True
            BtnDelete.Enabled = False
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            If Not Me.GrdHistory.RowCount > 0 Then Exit Sub
            Me.TxtRemarks.Text = GrdHistory.CurrentRow.Cells("Remarks").Value.ToString
            Me.TxtTotDays.Text = GrdHistory.CurrentRow.Cells("TotalDays").Value.ToString
            DtpStartDate.Value = GrdHistory.CurrentRow.Cells("StartDate").Value
            DtpEndDate.Value = GrdHistory.CurrentRow.Cells("EndDate").Value
            Me.CurrentId = GrdHistory.CurrentRow.Cells("HSCode").Value
            Me.CmbStatus.Text = GrdHistory.CurrentRow.Cells("AttendancaeStatus").Value.ToString
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.BtnSave.Text = "&Update"
            Me.BtnDelete.Enabled = True
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmHolidySetup)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        If Me.BtnPrint.Text = "&Print" Then BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmHolidySetup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                BtnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Public Function ReturnRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormName = Me.Name Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmHolidySetup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
            GetSecurityRights()
            Rights = GroupRights.FindAll(AddressOf ReturnRights)
            FillCombo()
            FillList()
            _SearchDt = CType(Me.LstEmployees.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayRecord()
        Try
            Dim str As String = String.Empty
            ''Edit Against TFS3569 : Ayesah Reman
            str = "Select HSCode,AttendancaeStatus,StartDate,EndDate,TotalDays,Remarks, " & _
            "  case  when HType = 0 then 'All'  " & _
            "  when HType = 1 then 'Department Wise'  " & _
            "  when HType = 2 then 'Designation Wise'  " & _
            "  when HType = 3 then 'Employee Wise'  " & _
            "  when HType = 4 then 'CostCenter Wise'  End as HolidayType from tblholidaysetup order by HSCode Desc"

            FillGridEx(GrdHistory, str, True)
            ApplyGridSettings()
            Me.GrdHistory.AutoSizeColumns()
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub GrdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdHistory.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Not GrdHistory.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Exit Sub
            End If

            If msg_Confirm(str_ConfirmDelete) = True Then
                Try


                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    Dim cm As New OleDbCommand
                    Dim CurrentId As Integer = 0I

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblattendancedetail where policyid = " & Me.GrdHistory.CurrentRow.Cells("HSCode").Value.ToString & ""
                    cm.ExecuteNonQuery()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblholidaysetup where HSCode = " & Me.GrdHistory.CurrentRow.Cells("HSCode").Value.ToString & ""
                    cm.ExecuteNonQuery()
                    ReSetControls()
                    Try
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.GrdHistory.CurrentRow.Cells("HSCode").Value.ToString, True)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try
                Me.ReSetControls()



            Else
                msg_Error(str_ErrorDependentRecordFound)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings()
        Try
            Me.GrdHistory.RootTable.Columns("HSCode").Visible = False
            Me.GrdHistory.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
            Me.GrdHistory.RootTable.Columns("EndDate").FormatString = str_DisplayDateFormat
            Me.GrdHistory.RootTable.Columns("StartDate").Caption = "Start Date"
            Me.GrdHistory.RootTable.Columns("EndDate").Caption = "End Date"
            Me.GrdHistory.RootTable.Columns("AttendancaeStatus").Caption = "Attendance Status"
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub GrdHistory_SelectionChanged(sender As Object, e As EventArgs) Handles GrdHistory.SelectionChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.GrdHistory.Row < 0 Then Exit Sub
            If Me.GrdHistory.RowCount = 0 Then Exit Sub
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            End If
            Dim jsRow As Janus.Windows.GridEX.GridEXRow
            jsRow = Me.GrdHistory.GetRow
            If jsRow Is Nothing Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("SP_AttendenceSetupDetail " & Val(jsRow.Cells("HSCode").Value.ToString) & "")
            dt.AcceptChanges()
            Me.grdAttendanceHistory.DataSource = dt
            Me.grdAttendanceHistory.RetrieveStructure()
            Me.grdAttendanceHistory.RootTable.Columns("AttendanceId").Visible = False
            Me.grdAttendanceHistory.RootTable.Columns("EmpId").Visible = False
            Me.grdAttendanceHistory.RootTable.Columns("Check Time").FormatString = "dd/MMM/yyyy hh:mm:ss tt"
            Me.grdAttendanceHistory.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DtpStartDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpStartDate.ValueChanged, DtpEndDate.ValueChanged
        Try
            GetDays()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddedEmp_Click(sender As Object, e As EventArgs) Handles btnAddedEmp.Click
        Try

            'Dim IDs As String = String.Empty
            'For Each obj As Object In Me.LstEmployees.SelectedItems
            '    If TypeOf obj Is DataRowView Then
            '        Dim dr As DataRowView = CType(obj, DataRowView)
            '        IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.LstEmployees.ValueMember).ColumnName)
            '    End If
            'Next

            Dim dt As DataTable = CType(Me.lstAddedEmployees.ListItem.DataSource, DataTable)
            dt.AcceptChanges()

            For Each obj As Object In Me.LstEmployees.ListItem.SelectedItems
                Dim dr As DataRow = dt.NewRow
                If TypeOf obj Is DataRowView Then
                    Dim objRow As DataRowView = CType(obj, DataRowView)
                    Dim drfound() As DataRow = dt.Select("Employee_ID=" & Val(objRow.Item("Employee_Id").ToString) & "")
                    If Not drfound.Length > 0 Then
                        dr(0) = Val(objRow.Item(0).ToString)
                        dr(1) = objRow.Item(1).ToString
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                    End If
                End If
            Next


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRemoveEmp_Click(sender As Object, e As EventArgs) Handles btnRemoveEmp.Click
        Try

            Dim strIDs As String = lstAddedEmployees.SelectedIDs
            Dim dt As DataTable = CType(Me.lstAddedEmployees.ListItem.DataSource, DataTable)
            dt.AcceptChanges()

            If strIDs.Length > 0 Then
                Dim drFound() As DataRow = dt.Select("Employee_ID In(" & strIDs & ")")
                For Each r As DataRow In drFound
                    dt.Rows.Remove(r)
                Next
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try

            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt


            dv.RowFilter = "Employee_Name LIKE '%" & Me.txtSearch.Text & "%'"
            Me.LstEmployees.ListItem.DataSource = dv.ToTable

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.GrdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Attendance Setup "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Start TFS3569
    Private Sub RdbCostCenter_CheckedChanged(sender As Object, e As EventArgs) Handles RdbCostCenter.CheckedChanged
        Try
            PnDat.Visible = True
            LstDepartment.Visible = False
            LstDesignation.Visible = False
            LstEmployees.Visible = False
            LstCostCenter.Visible = True
            Me.txtSearch.Visible = False
            plnEmp.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End TFS3569
End Class