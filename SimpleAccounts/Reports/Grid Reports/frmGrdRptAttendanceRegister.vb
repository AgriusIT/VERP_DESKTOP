Imports SBModel
Imports SBDal
Imports SBUtility

Public Class frmGrdRptAttendanceRegister
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private Enum enmEmp
        Employee_Id
        Employee_Name
        Employee_Code
        Designation
        Depart
        CostCenter
        OverTime
        Count
    End Enum
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Try
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
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
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
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
            ServerDate()
            Me.dtpFromDate.Value = GetServerDate.AddDays(-(GetServerDate.Day - 1))
            Me.dtpToDate.Value = GetServerDate

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetDatesOfMonth(FromDate As DateTime, ToDate As DateTime) As DataTable
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Dated", GetType(System.DateTime))
            Dim dtpStartDate As DateTime = FromDate.Date
            Dim dtpEndDate As DateTime = ToDate.Date
            _DateFrom = dtpStartDate
            _DateTo = dtpEndDate
            Dim intTotalDays As Integer = DateDiff(DateInterval.Day, _DateFrom, _DateTo) + 1
            Dim dr As DataRow
            Dim i As Integer = 0I
            For i = 0 To intTotalDays - 1
                dr = dt.NewRow
                dr(0) = dtpStartDate.AddDays(i)
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Dim starttime As DateTime
            starttime = Date.Now
            FillGrid()
            Me.lblResultTime.Text = "Result retrieved in " & DateDiff(DateInterval.Second, starttime, Date.Now) & " seconds"
            Me.lblResultTime.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub FillGrid()
        Try

            Dim dtMonth As New DataTable
            Dim dtEmp As New DataTable

            dtMonth = GetDatesOfMonth(Me.dtpFromDate.Value, dtpToDate.Value)



            'Task1248  dtEmp = GetDataTable("Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation, EmployeeDeptName as Department, tblDefCostCenter.Name As [Cost Centre] ,IsNull(empOver.OverTime,0) as EmpOverTime From EmployeesView Left Outer Join ( Select EmployeeId, SUM(Case When Datediff(dd, Convert(DateTime,[Start_Date],102),Convert(DateTime,End_Date,102)) =0 then 1 else Datediff(dd, Convert(DateTime,[Start_Date],102),Convert(DateTime,End_Date,102))+1 end * Datediff(hh, Convert(DateTime,Start_Time,102),Convert(DateTime,End_Time,102))) as OverTime  from tblEmployeeOverTimeSchedule WHERE (Convert(datetime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  BETWEEN Convert(Datetime,Start_Date,102)  AND Convert(DateTime,End_Date,102)) And (Convert(datetime,'" & _DateTo.ToString("yyyy-M-d 00:00:00") & "',102)  BETWEEN Convert(Datetime,Start_Date,102)  AND Convert(DateTime,End_Date,102)) Group By employeeId) as empOver on empOver.EmployeeId = EmployeesView.Employee_Id Left Join tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID WHERE EmployeesView.Active=1 " & IIf(cmbDepartment.SelectedIndex > 0, "And EmployeesView.Dept_ID =  " & Me.cmbDepartment.SelectedValue & "", "") & "")
            dtEmp.AcceptChanges()

            dtMonth.AcceptChanges()
            For Each dr As DataRow In dtMonth.Rows
                If Not dtEmp.Columns.Contains(dr(0)) Then
                    dtEmp.Columns.Add(dr(0), GetType(System.String))
                End If
            Next

            dtEmp.AcceptChanges()
            Dim strSQL As String = "Select att_date.Dates as AttendanceDate, IsNull(AttendanceStatus,'A')  as  AttendanceStatus,att_date.EmpId From FuncAttendanceDates('" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "','" & _DateTo.ToString("yyyy-M-d 23:59:59") & "') as att_date LEFT OUTER JOIN(Select AttendanceDate," _
                                & " Case " _
                                & " When AttendanceStatus='Present' then 'P'  " _
                                & " When AttendanceStatus='Absent' then 'A' " _
                                & " When AttendanceStatus='Leave' then 'L' " _
                                & " When AttendanceStatus='Short Leave' then 'SL'" _
                                & " When AttendanceStatus='Half Leave' then 'HL'" _
                                & " When AttendanceStatus='Sick Leave' then 'SKL'" _
                                & " When AttendanceStatus='Off Day' then 'Off' " _
                                & " When AttendanceStatus='Outdoor Duty' then 'OD'" _
                                & " When AttendanceStatus='Anual Level' then 'ANL'" _
                                & " When AttendanceStatus='Gazetted Leave' then 'GL'" _
                                & " When AttendanceStatus='Maternity Leave' then 'ML' ELSE 'A' " _
                                & " End as AttendanceStatus,EmpId from tblAttendancedetail where AttendanceId in " _
                                & " (select Max(AttendanceId) From tblAttendanceDetail Group By EmpId, AttendanceDate)" _
                                & " AND (Convert(datetime,attendanceDate,102) BETWEEN Convert(DateTime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102))) as att on att.AttendanceDate = att_date.Dates AND att.EmpId = att_date.EmpId "
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)
            dtData.AcceptChanges()
            For Each r As DataRow In dtEmp.Rows
                r.BeginEdit()
                Dim dr() As DataRow = dtData.Select("EmpId=" & Val(r.Item("Employee_Id").ToString) & "")
                If dr.Length > 0 Then
                    For Each drFound As DataRow In dr
                        r(dtEmp.Columns.IndexOf(drFound(0))) = drFound(1).ToString
                    Next
                End If
                r.EndEdit()
            Next

            strSQL = "Select Count(Case When AttendanceStatus='Present' then 'P' else null end) as [Present], Count(Case When AttendanceStatus='Off Day' then 'DO' ELSE NULL end) AS [Off Day],   Count(Case When AttendanceStatus='Outdoor Duty' then 'OD' else null end) as [Outdoor Duty],  Count(Case When AttendanceStatus='Short Leave' then 'SL' else null end) as [Short Leave],    Count(Case When AttendanceStatus='Half Leave' then 'HL' else null end) as [Half Leave],  Count(Case When AttendanceStatus='Absent' then 'A' when AttendanceStatus is null then 'A' end)  as [Absent], Count(Case When AttendanceStatus='Half Absent' then 'HA' when AttendanceStatus is null then null end)  as [Half Absent],  Count(Case When AttendanceStatus='Leave' then 'L' else null end) as [Leave],   	 Count(Case When AttendanceStatus='Sick Leave' then 'SL' else null end) as [Sick Leave],    Count(Case When AttendanceStatus='Anual Level' then 'AL' else null end) as [Anual Level],   Count(Case When AttendanceStatus='Gazetted Leave' then 'GL' else null end) as [Gazetted Leave],    Count(Case When AttendanceStatus='Maternity Leave' then 'ML' else null end ) as [Maternity Leave], att_date.empId from  FuncAttendanceDates('" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "','" & _DateTo.ToString("yyyy-M-d 23:59:59") & "') as att_date  left outer join (  Select * From tblAttendanceDetail where AttendanceId in  (select Max(AttendanceId) From tblAttendanceDetail Group By EmpId, AttendanceDate) AND (Convert(datetime,attendanceDate,102) BETWEEN Convert(DateTime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102))) as att on att.empid = att_date.empid AND att.attendanceDate = att_date.dates group by att_date.empid "

            Dim dtTotals As New DataTable
            dtTotals = GetDataTable(strSQL)
            dtTotals.AcceptChanges()


            dtEmp.Columns.Add("Present", GetType(System.UInt32))
            dtEmp.Columns.Add("Off Day", GetType(System.UInt32))
            dtEmp.Columns.Add("Outdoor Duty", GetType(System.UInt32))
            dtEmp.Columns.Add("Short Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Half Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Absent", GetType(System.UInt32))
            dtEmp.Columns.Add("Half Absent", GetType(System.UInt32))
            dtEmp.Columns.Add("Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Sick Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Anual Level", GetType(System.UInt32))
            dtEmp.Columns.Add("Gazetted Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Maternity Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("OverTime", GetType(System.Double))

            For Each r As DataRow In dtEmp.Rows
                Dim dr() As DataRow = dtTotals.Select("EmpId=" & Val(r.Item("Employee_Id").ToString) & "")
                r.BeginEdit()
                If dr.Length > 0 Then
                    r("Present") = Val(dr(0)(0).ToString)
                    r("Off Day") = Val(dr(0)(1).ToString)
                    r("Outdoor Duty") = Val(dr(0)(2).ToString)
                    r("Short Leave") = Val(dr(0)(3).ToString)
                    r("Half Leave") = Val(dr(0)(4).ToString)
                    r("Absent") = Val(dr(0)(5).ToString)
                    r("Leave") = Val(dr(0)(6).ToString)
                    r("Sick Leave") = Val(dr(0)(7).ToString)
                    r("Anual Level") = Val(dr(0)(8).ToString)
                    r("Gazetted Leave") = Val(dr(0)(9).ToString)
                    r("Maternity Leave") = Val(dr(0)(10).ToString)
                End If
                r.EndEdit()
            Next

            dtEmp.AcceptChanges()
            dtEmp.Columns("OverTime").Expression = "IsNull(EmpOverTime,0)"

            Me.grd.DataSource = dtEmp
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("EmpOverTime").Visible = False
            getApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub getApplyGridSettings()
        Try
            'Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 12
            Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 13
            For c As Integer = enmEmp.Count To intCounter - 1
                Me.grd.RootTable.Columns(c).Caption = CDate(Me.grd.RootTable.Columns(c).Caption).Day.ToString
                Me.grd.RootTable.Columns(c).Width = 50
            Next
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns.GridEX.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grd.FrozenColumns = 5
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Attendence Register" & Chr(10) & "Date From:" & _DateFrom.ToString("dd/MMM/yyyy") & " Date To:" & _DateTo.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo()
        '#task1248
        'Try
        '    Dim Str As String
        '    Str = "Select  EmployeeDeptId, EmployeeDeptName from EmployeeDeptDefTable Where Active =1"
        '    FillDropDown(Me.cmbDepartment, Str, True)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Print" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1") ''TASKTFS75 added and set active =1
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
End Class