''TFS3320 : Ayesha Rehman : Cost Center Rights should be implemented on HR Reports
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility

Partial Class DailyAttendance
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Public flgCompanyRights As Boolean = False
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            GetDailyAttendanceData()
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            ''Start TFS3418
            AddRptParam("@DepartmentIds", Me.lstEmpDepartment.SelectedIDs)
            AddRptParam("@DesignationsIds", Me.lstEmpDesignation.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstEmpShiftGroup.SelectedIDs)
            AddRptParam("@CityIds", Me.lstEmpCity.SelectedIDs)
            AddRptParam("@EmployeeId", Me.lstEmployee.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            ''End TFS3418
            ShowReport("rptEmployeeAttendanceNew") ' IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and {SP_Employee_Attendance;1.Employee_Id}=" & Me.cmbEmployee.Value & "", "") & " " & IIf(Me.cmbCostCenter.SelectedValue > 0, " and {SP_Employee_Attendance;1.CostCentre}=" & Me.cmbCostCenter.SelectedValue & "", ""))
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub SummaryofSalesTaxInvoices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            Me.Text = "Daily Employee Attendance"
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Designation")
            Me.lstEmpDesignation.DeSelect()
            FillCombos("Department")
            Me.lstEmpDepartment.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstEmpShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstEmpCity.DeSelect()
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub GetDailyAttendanceData()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Dim strQuery As String = String.Empty
        cmd.Connection = Con
        cmd.Transaction = trans
        Try
            strQuery = "SELECT EmpId, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, Flexibility_In_Time, Flexibility_Out_Time, " _
            & " Sch_In_Time, Sch_Out_Time FROM dbo.tblAttendanceDetail AS Att_d WHERE (Convert(Varchar,AttendanceDate,102) BETWEEN Convert(DateTime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ORDER BY EmpId, AttendanceDate, AttendanceTime "
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.AcceptChanges()
            Dim strAttendanceType As String = String.Empty
            Dim intEmpId As Integer = 0I
            Dim strType As String = String.Empty
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Truncate Table tblTempAttendanceDetail"
            cmd.ExecuteNonQuery()
            For Each r As DataRow In dt.Rows
                If intEmpId <> Val(r.Item("EmpId").ToString) Then
                    strType = String.Empty
                    strAttendanceType = String.Empty
                End If
                Select Case strAttendanceType
                    Case "In"
                        strType = "Out"
                    Case "Out"
                        strType = "In"
                    Case ""
                        strType = "In"
                End Select
                If r.Item("AttendanceStatus").ToString = "Break" Then
                    strType = "Out"
                End If
                Dim strSQL As String = String.Empty
                strSQL = " INSERT INTO tblTempAttendanceDetail(EmpId,AttendanceDate,AttendanceType,AttendanceTime,AttendanceStatus,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) " _
                & " VALUES(" & Val(r.Item("EmpId").ToString) & ", Convert(DateTime,'" & r.Item("AttendanceDate").ToString & "',102), '" & strType.Replace("'", "''") & "',Convert(DateTime,'" & r.Item("AttendanceTime") & "',102), '" & r.Item("AttendanceStatus").ToString.Replace("'", "''") & "',Convert(DateTime,'" & r.Item("Flexibility_In_Time") & "',102),Convert(DateTime,'" & r.Item("Flexibility_Out_Time") & "',102),Convert(DateTime,'" & r.Item("Sch_In_Time") & "',102),Convert(DateTime,'" & r.Item("Sch_Out_Time") & "',102))"
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
                intEmpId = Val(r.Item("EmpId").ToString)
                Select Case strType
                    Case "In"
                        strAttendanceType = "In"
                    Case "Out"
                        strAttendanceType = "Out"
                    Case ""
                        strAttendanceType = "In"
                End Select
            Next
            trans.Commit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.OK_Button.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.OK_Button.Enabled = False
            Me.btnPrint.Enabled = False

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1")
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstEmpShiftGroup.ListItem, "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1 ")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstEmpCity.ListItem, "SELECT  CityId, CityName FROM tblListCity WHERE Active=1 ")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function


    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Employee_Name Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class