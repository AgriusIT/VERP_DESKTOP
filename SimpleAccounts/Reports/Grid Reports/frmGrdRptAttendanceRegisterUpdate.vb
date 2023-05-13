''TASK:991 mark red color of cell in case value is Off Day. Done by Ameen on 23-06-2017
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmGrdRptAttendanceRegisterUpdate
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private AttendanceStatusList As List(Of String)
    Dim dtAttendanceStatus As DataTable
    Dim AttendanceEmp As AttendanceEmp
    Dim AutoAttendance As Boolean = False
    Dim AttendanceEmpDAL As New AttendanceEmpDAL
    Dim _SearchDt As New DataTable ''TFS3418
    Private Enum enmEmp
        Employee_Id
        Employee_Name
        Employee_Code
        Designation
        Depart
        EmployeeType
        CostCenter
        OverTime
        ShiftGroupId
        Count
    End Enum
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ''Commented Against TFS3418
            'Dim ID As Integer = 0
            'Dim ID1 As Integer = 0
            'ID = Me.cmbDepartment.SelectedValue
            'ID1 = Me.cmbShift.SelectedValue
            'FillCombo()
            'Me.cmbDepartment.SelectedValue = ID
            'Me.cmbShift.SelectedValue = ID1
            ''Start TFS3418
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
            ''End TFS3418
            'Me.dtpFromDate.Value = Now
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' FillCombo() ''Commented Against TFS3418
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
            AttStatusList()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional Condition As String = "")
        Try
            If Condition = "Employees" Then

                Dim dtCostCenter As New DataTable
                Dim str = String.Empty
                Dim costCentersID As String = String.Empty

                str = "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1"

                If Convert.ToBoolean(getConfigValueByType("RightBasedCostCenters").ToString) = True Then

                    dtCostCenter = GetDataTable("SELECT CostCenterID FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1")

                    If dtCostCenter.Rows.Count > 0 Then

                        str = str & " And CostCentre In ("

                        For Each row As DataRow In dtCostCenter.Rows

                            costCentersID = costCentersID & Val(row.Item("CostCenterID").ToString) & ","

                        Next

                        costCentersID = costCentersID.Trim().Remove(costCentersID.Length - 1)

                        str = str & costCentersID & ")"

                        FillListBox(Me.lstEmployee.ListItem, str)

                    End If
                Else
                    FillListBox(Me.lstEmployee.ListItem, str) ''TASKTFS75 added and set active =1
                End If

            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation

                'Task 3570 Saad Afzaal User right based Cost Centers in dropdowns according to that Cost Centers on Attendance Register Update Configuration based.

                'FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")

                If Convert.ToBoolean(getConfigValueByType("RightBasedCostCenters").ToString) = True Then
                    FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1")
                Else
                    FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1")
                End If


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

    Private Sub FillGrid()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim OffDay As String = ""
        Dim strSQL As String = "" ''TFS3418
        Try
            If getConfigValueByType("DayOff") = "Error" Then
                OffDay = "Sunday"
            Else
                OffDay = getConfigValueByType("DayOff")
            End If
            '' 24-04-2017
            Dim NormalMul As String = String.Empty
            Dim OffMul As String = String.Empty
            If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
            Else
                NormalMul = 1
            End If
            If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
            Else
                OffMul = 1
            End If
            ''End 24-04-2017
            Dim DayOff As String = OffDay.Replace(",", "', '")
            Dim dtMonth As New DataTable
            Dim dtEmp As New DataTable
            dtMonth = GetDatesOfMonth(Me.dtpFromDate.Value, dtpToDate.Value)
            'dtEmp = GetDataTable("Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation, EmployeeDeptName as Department, tblDefCostCenter.Name As [Cost Centre] ,IsNull(empOver.OverTime,0) as EmpOverTime, EmployeesView.ShiftGroupId From EmployeesView Left Outer Join ( Select EmployeeId, SUM(Case When Datediff(dd, Convert(DateTime,[Start_Date],102),Convert(DateTime,End_Date,102)) =0 then 1 else Datediff(dd, Convert(DateTime,[Start_Date],102),Convert(DateTime,End_Date,102))+1 end * Datediff(hh, Convert(DateTime,Start_Time,102),Convert(DateTime,End_Time,102))) as OverTime  from tblEmployeeOverTimeSchedule WHERE (Convert(datetime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  BETWEEN Convert(Datetime,Start_Date,102)  AND Convert(DateTime,End_Date,102)) And (Convert(datetime,'" & _DateTo.ToString("yyyy-M-d 00:00:00") & "',102)  BETWEEN Convert(Datetime,Start_Date,102)  AND Convert(DateTime,End_Date,102)) Group By employeeId) as empOver on empOver.EmployeeId = EmployeesView.Employee_Id Left Join tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID WHERE EmployeesView.Active=1 " & IIf(cmbDepartment.SelectedIndex > 0, "And EmployeesView.Dept_ID =  " & Me.cmbDepartment.SelectedValue & "", "") & "")
            ''Commented Against TFS3418
            'dtEmp = GetDataTable("Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation, EmployeeDeptName as Department, TblEmployeeType.EmployeeTypeName as EmployeeType, tblDefCostCenter.Name As [Cost Centre] ,IsNull(empOver.OverTime,0) as EmpOverTime, EmployeesView.ShiftGroupId From EmployeesView Left Outer Join TblEmployeeType ON EmployeesView.EmployeeTypeId = TblEmployeeType.EmployeeTypeId LEFT OUTER JOIN (Select EmployeeId, Sum((IsNull(RegularDayOTRate, 0)*IsNull(RegularDayHrs, 0) * " & NormalMul & ")+(IsNull(OffDayOTRate, 0)*IsNull(OffDayHrs, 0)* " & OffMul & ")) as OverTime from tblEmployeeOverTimeSchedule WHERE (Convert(Datetime,Start_Date,102) Between Convert(datetime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(datetime,'" & _DateTo.ToString("yyyy-M-d 00:00:00") & "',102)) Group By employeeId) as empOver on empOver.EmployeeId = EmployeesView.Employee_Id Left Join tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID WHERE EmployeesView.Active=1 " & IIf(cmbDepartment.SelectedIndex > 0, "And EmployeesView.Dept_ID =  " & Me.cmbDepartment.SelectedValue & "", "") & "" & IIf(cmbShift.SelectedIndex > 0, "And EmployeesView.ShiftGroupId =  " & Me.cmbShift.SelectedValue & "", "") & "")
            strSQL = "Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation, EmployeeDeptName as Department, TblEmployeeType.EmployeeTypeName as EmployeeType, tblDefCostCenter.Name As [Cost Centre] ,IsNull(empOver.OverTime,0) as EmpOverTime, EmployeesView.ShiftGroupId From EmployeesView Left Outer Join TblEmployeeType ON EmployeesView.EmployeeTypeId = TblEmployeeType.EmployeeTypeId LEFT OUTER JOIN (Select EmployeeId, Sum((IsNull(RegularDayOTRate, 0)*IsNull(RegularDayHrs, 0) * " & NormalMul & ")+(IsNull(OffDayOTRate, 0)*IsNull(OffDayHrs, 0)* " & OffMul & ")) as OverTime from tblEmployeeOverTimeSchedule WHERE (Convert(Datetime,Start_Date,102) Between Convert(datetime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(datetime,'" & _DateTo.ToString("yyyy-M-d 00:00:00") & "',102)) Group By employeeId) as empOver on empOver.EmployeeId = EmployeesView.Employee_Id Left Join tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID " _
                     & " INNER JOIN (SELECT ShiftGroupId FROM ShiftScheduleTable INNER JOIN ShiftTable ON  ShiftScheduleTable.ShiftId = ShiftTable.ShiftId " & IIf(rbtnNightShift.Checked = True, " WHERE ISNULL(ShiftTable.NightShift, 0)=1", " WHERE ISNULL(ShiftTable.NightShift, 0)=0 ") & ") AS Shift ON EmployeesView.ShiftGroupId = Shift.ShiftGroupId WHERE EmployeesView.Active=1 "
            ''Start TFS3418

            'Task 3570 Saad Afzaal User right based Cost Centers in history according to that Cost Centers on Attendance Register Update Configuration based.

            If Convert.ToBoolean(getConfigValueByType("RightBasedCostCenters").ToString) = True Then
                strSQL += "And EmployeesView.CostCentre in (Select CostCentre_Id  FROM  tblUserCostCentreRights  where UserID = " & LoginUserId & " and (CostCentre_Id is Not Null) )"
            End If

            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                strSQL += " AND Employee_Id in (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                strSQL += " AND Desig_Id in (" & Me.lstEmpDesignation.SelectedIDs & ")"
            End If
            If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                strSQL += " AND EmployeesView.Dept_Id in (" & Me.lstEmpDepartment.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strSQL += " AND dbo.EmployeesView.CostCentre in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                strSQL += " AND dbo.EmployeesView.ShiftGroupId in (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                strSQL += " AND City_ID in(" & Me.lstEmpCity.SelectedIDs & ")"
            End If
            ''End TFS3418

            dtEmp = GetDataTable(strSQL)
            dtEmp.AcceptChanges()
            dtMonth.AcceptChanges()
            For Each dr As DataRow In dtMonth.Rows
                If Not dtEmp.Columns.Contains(dr(0)) Then
                    dtEmp.Columns.Add(dr(0), GetType(System.String))
                End If
            Next
            Dim cm As New OleDb.OleDbCommand
            cm.Connection = Con
            cm.Transaction = trans
            '            if exists (select  * from tempdb.dbo.sysobjects Where name='##tmpAttendanceDates' And xtype= 'U')
            '--DROP TABLE ##tmpAttendanceDates
            'Create Table ##tmpAttendanceDates
            '(
            'Dates datetime not null,
            'EmpId int,
            'AttendanceId int,
            'Primary Key(Dates,EmpId, AttendanceId),
            '                UNIQUE CLUSTERED(Dates, EmpId, AttendanceId)
            'cm.CommandText = ""
            'cm.CommandText = "if Not exists (select  * from tempdb.dbo.sysobjects Where name='##tmpAttendanceDates' And xtype= 'U') Create Table ##tmpAttendanceDates(Dates datetime not null, EmpId int, AttendanceId int null, Primary Key(Dates,EmpId), UNIQUE CLUSTERED(Dates, EmpId, AttendanceId))"
            'cm.ExecuteNonQuery()
            cm.CommandText = " Truncate Table tmpAttendanceDates"
            cm.ExecuteNonQuery()
            cm.CommandText = ""
            cm.CommandText = " Exec AttendanceRegisterUpdateDates '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "' "
            cm.ExecuteNonQuery()
            'cm.
            'trans.Commit()
            dtEmp.AcceptChanges()
            'Dim strSQL As String = "Select att_date.Dates as AttendanceDate, IsNull(AttendanceStatus,'A')  as  AttendanceStatus,att_date.EmpId, IsNull(att.AttendanceId, 0) As AttendanceId From FuncAttendanceDates('" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "','" & _DateTo.ToString("yyyy-M-d 23:59:59") & "') as att_date LEFT OUTER JOIN(Select AttendanceDate," _
            '                    & " Case " _
            '                    & " When AttendanceStatus='Present' then 'P'  " _
            '                    & " When AttendanceStatus='Absent' then 'A' " _
            '                    & " When AttendanceStatus='Leave' then 'L' " _
            '                    & " When AttendanceStatus='Short Leave' then 'SL'" _
            '                    & " When AttendanceStatus='Half Leave' then 'HL'" _
            '                    & " When AttendanceStatus='Sick Leave' then 'SKL'" _
            '                    & " When AttendanceStatus='Off Day' then 'Off' " _
            '                    & " When AttendanceStatus='Outdoor Duty' then 'OD'" _
            '                    & " When AttendanceStatus='Anual Level' then 'ANL'" _
            '                    & " When AttendanceStatus='Gazetted Leave' then 'GL'" _
            '                    & " When AttendanceStatus='Maternity Leave' then 'ML' ELSE 'A' " _
            '                    & " End as AttendanceStatus,EmpId, AttendanceId from tblAttendancedetail where AttendanceId in " _
            '                    & " (select Max(AttendanceId) From tblAttendanceDetail Group By EmpId, AttendanceDate)" _
            '                    & " AND (Convert(datetime,attendanceDate,102) BETWEEN Convert(DateTime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102))) as att on att.AttendanceDate = att_date.Dates AND att.EmpId = att_date.EmpId "

            cm.CommandText = "Select att_date.Dates as AttendanceDate, IsNull(AttendanceStatus,'A')  as  AttendanceStatus,att_date.EmpId, IsNull(att.AttendanceId, 0) As AttendanceId From tmpAttendanceDates as att_date LEFT OUTER JOIN(Select AttendanceDate," _
                              & " Case " _
                              & " When AttendanceStatus='Present' then 'P'  " _
                              & " When AttendanceStatus='Absent' then 'A' " _
                              & " When AttendanceStatus='Half Absent' then 'HA' " _
                              & " When AttendanceStatus='Short Absent' then 'SA'  " _
                              & " When AttendanceStatus='Leave' then 'L' " _
                              & " When AttendanceStatus='Short Leave' then 'SL'" _
                              & " When AttendanceStatus='Half Leave' then 'HL'" _
                                & " When AttendanceStatus='Sick Leave' then 'SKL'" _
                                & " When AttendanceStatus='Off Day' then 'Off' " _
                                & " When AttendanceStatus='Outdoor Duty' then 'OD'" _
                                & " When AttendanceStatus='Anual Leave' then 'ANL'" _
                                & " When AttendanceStatus='Gazetted Leave' then 'GL'" _
                                & " When AttendanceStatus='Maternity Leave' then 'ML' ELSE 'A' " _
                                  & " End as AttendanceStatus,EmpId, AttendanceId from tblAttendancedetail) as att on att.AttendanceDate = att_date.Dates AND att.EmpId = att_date.EmpId And att.AttendanceId = att_date.AttendanceId "
            Dim dtData As New DataTable
            Dim da As New OleDb.OleDbDataAdapter(cm)
            da.Fill(dtData)
            'dtData = GetDataTable(strSQL)
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
            cm.CommandText = "Select Count(Case When AttendanceStatus='Present' then 'P' else null end) as [Present], Count(Case When AttendanceStatus='Off Day' then 'DO' ELSE NULL end) AS [Off Day],   Count(Case When AttendanceStatus='Outdoor Duty' then 'OD' else null end) as [Outdoor Duty],  Count(Case When AttendanceStatus='Short Leave' then 'SL' else null end) as [Short Leave],    Count(Case When AttendanceStatus='Half Leave' then 'HL' else null end) as [Half Leave],  Count(Case When AttendanceStatus='Absent' then 'A' when AttendanceStatus is null then 'A' end)  as [Absent], Count(Case When AttendanceStatus='Half Absent' then 'HA' when AttendanceStatus is null then null end)  as [Half Absent], Count(Case When AttendanceStatus='Short Absent' then 'SA' else null end ) as [Short Absent],  Count(Case When AttendanceStatus='Leave' then 'L' else null end) as [Leave],   	 Count(Case When AttendanceStatus='Sick Leave' then 'SL' else null end) as [Sick Leave], Count(Case When AttendanceStatus='Anual Leave' then 'AL' else null end) as [Anual Leave],   Count(Case When AttendanceStatus='Gazetted Leave' then 'GL' else null end) as [Gazetted Leave],    Count(Case When AttendanceStatus='Maternity Leave' then 'ML' else null end ) as [Maternity Leave], att_date.empId from tmpAttendanceDates as att_date  left outer join (Select * From tblAttendanceDetail where (Convert(datetime,attendanceDate,102) BETWEEN Convert(DateTime,'" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102))) as att on att.empid = att_date.empid AND att.attendanceDate = att_date.dates And att.AttendanceId = att_date.AttendanceId Group By att_date.empId"
            Dim dtTotals As New DataTable
            Dim da1 As New OleDb.OleDbDataAdapter(cm)
            da1.Fill(dtTotals)
            'dtTotals = GetDataTable(strSQL)
            dtTotals.AcceptChanges()
            dtEmp.Columns.Add("Present", GetType(System.UInt32))
            dtEmp.Columns.Add("Off Day", GetType(System.UInt32))
            dtEmp.Columns.Add("Outdoor Duty", GetType(System.UInt32))
            dtEmp.Columns.Add("Short Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Half Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Absent", GetType(System.UInt32))
            dtEmp.Columns.Add("Half Absent", GetType(System.UInt32))
            dtEmp.Columns.Add("Short Absent", GetType(System.UInt32))
            dtEmp.Columns.Add("Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Sick Leave", GetType(System.UInt32))
            dtEmp.Columns.Add("Anual Leave", GetType(System.UInt32))
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
                    r("Half Absent") = Val(dr(0)(6).ToString)
                    r("Short Absent") = Val(dr(0)(7).ToString)
                    r("Leave") = Val(dr(0)(8).ToString)
                    r("Sick Leave") = Val(dr(0)(9).ToString)
                    r("Anual Leave") = Val(dr(0)(10).ToString)
                    r("Gazetted Leave") = Val(dr(0)(11).ToString)
                    r("Maternity Leave") = Val(dr(0)(12).ToString)
                End If
                r.EndEdit()
            Next
            dtEmp.AcceptChanges()
            dtEmp.Columns("OverTime").Expression = "IsNull(EmpOverTime,0)"
            Me.grd.DataSource = dtEmp
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("EmpOverTime").Visible = False
            Me.grd.RootTable.Columns("ShiftGroupId").Visible = False
            getApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            trans.Commit()
            'cm.CommandText = ""
            'cm.CommandText = " If Exists(Select  * from tempdb.dbo.sysobjects Where name='##tmpAttendanceDates' And xtype= 'U') Drop Table ##tmpAttendanceDates"
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Private Sub getApplyGridSettings()
        Try
            'Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 12
            Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 14
            For c As Integer = enmEmp.Count To intCounter - 1

                Me.grd.RootTable.Columns(c).Caption = CDate(Me.grd.RootTable.Columns(c).Caption).Day.ToString
                Me.grd.RootTable.Columns(c).Width = 50
                Me.grd.RootTable.Columns(c).HasValueList = True
                Me.grd.RootTable.Columns(c).LimitToList = True
                Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.Combo
                'Me.grd.RootTable.Columns(c).
                Me.grd.RootTable.Columns(c).ValueList.PopulateValueList(dtAttendanceStatus.DefaultView, "Status", "Status")
            Next
            Me.grd.FrozenColumns = 6

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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Attendence Register Update" & Chr(10) & "Date From:" & _DateFrom.ToString("dd/MMM/yyyy") & " Date To:" & _DateTo.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo()
        Try
            Dim Str As String
            Str = "Select  EmployeeDeptId, EmployeeDeptName from EmployeeDeptDefTable Where Active =1"
            FillDropDown(Me.cmbDepartment, Str, True)
            Dim str1 As String
            str1 = "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1"
            FillDropDown(Me.cmbShift, str1, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        If e.Tab.Index = 0 Then
            Me.btnSave.Visible = False
        Else
            Me.btnSave.Visible = True
        End If
    End Sub
    Private Sub AttStatusList()
        Try
            'Dim dt As New DataTable
            dtAttendanceStatus = New DataTable
            Dim row As DataRow
            dtAttendanceStatus.TableName = "AttendanceStatus"
            dtAttendanceStatus.Columns.Add("Status")
            row = dtAttendanceStatus.NewRow
            row("Status") = "P"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "A"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "HA"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "SA"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "L"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "SL"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "HL"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "SKL"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "Off"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "OD"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "ANL"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "GL"
            dtAttendanceStatus.Rows.Add(row)
            row = dtAttendanceStatus.NewRow
            row("Status") = "ML"
            dtAttendanceStatus.Rows.Add(row)
            dtAttendanceStatus.AcceptChanges()

            'AttendanceStatusList = New List(Of String)
            'AttendanceStatusList.Add("P")
            'AttendanceStatusList.Add("A")
            'AttendanceStatusList.Add("L")
            'AttendanceStatusList.Add("SL")
            'AttendanceStatusList.Add("HL")
            'AttendanceStatusList.Add("SKL")
            'AttendanceStatusList.Add("OFF")
            'AttendanceStatusList.Add("OD")
            'AttendanceStatusList.Add("ANL")
            'AttendanceStatusList.Add("GL")
            'AttendanceStatusList.Add("ML")
            'Dim Dictionary As Dictionary(Of String, Integer)
            'Dim dt As DataTable

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        'Try
        '    If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '        Dim Index As Integer = e.Column.Index
        '        Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 13
        '        If Index >= enmEmp.Count AndAlso Index < intCounter - 1 Then
        '            Me.grd.GetRow.BeginEdit()
        '            Dim Key As String = e.Column.Key
        '            Dim Value As String = Me.grd.GetRow.Cells(Index).Value.ToString
        '            Me.grd.GetRow.Cells(Index).Value = Value
        '            Me.grd.GetRow.EndEdit()
        '            If SaveSingle(Index) Then
        '                msg_Information("Attendance status has been updated successfully.")
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grd_TextChanged(sender As Object, e As EventArgs) Handles grd.TextChanged
        'Try
        '    If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '        Dim Index As Integer = e.Colum
        '        Dim Key As String = e.Column.Key
        '        Dim Value As String = Me.grd.GetRow.Cells(Index).Value.ToString
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        'Try
        '    Me.grd.UpdateData()
        '    If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '        Me.grd.GetRow.BeginEdit()
        '        Dim Index As Integer = e.Column.Index
        '        Dim Key As String = e.Column.Key
        '        Dim Value As String = Me.grd.GetRow.Cells(Index).Value.ToString
        '        Me.grd.GetRow.BeginEdit()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            If Save() Then
                msg_Information("Record has been updated successfully.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean
        Try
            'Task No 2593 Append Some New Lines Of Code for Newly Added Fields
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 14
                For c As Integer = enmEmp.Count To intCounter - 1
                    Dim dtShift As DataTable = GetShift(Val(Row.Cells("ShiftGroupId").Value.ToString))
                    AttendanceEmp = New AttendanceEmp
                    If dtShift.Rows.Count > 0 Then
                        'Dim objRow As DataRowView
                        'objRow = CType(Me.cmbShift.SelectedItem, DataRowView)
                        If dtShift.Rows(0).Item("FlexInTime").ToString <> "" Then
                            AttendanceEmp.FlexiblityInTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("FlexInTime").ToString).ToLongTimeString
                        Else
                            AttendanceEmp.FlexiblityInTime = Date.Now
                        End If
                        If dtShift.Rows(0).Item("FlexOutTime").ToString <> "" Then
                            AttendanceEmp.FlexiblityOutTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("FlexOutTime").ToString).ToLongTimeString
                        Else
                            AttendanceEmp.FlexiblityOutTime = Date.Now
                        End If
                        If dtShift.Rows(0).Item("ShiftStartTime").ToString <> "" Then
                            AttendanceEmp.SchInTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("ShiftStartTime").ToString).ToLongTimeString
                        Else
                            AttendanceEmp.SchInTime = Date.Now
                        End If
                        If dtShift.Rows(0).Item("ShiftEndTime").ToString <> "" Then
                            AttendanceEmp.SchOutTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("ShiftEndTime").ToString).ToLongTimeString
                        Else
                            AttendanceEmp.SchOutTime = Date.Now
                        End If

                    Else
                        AttendanceEmp.FlexiblityInTime = String.Empty
                        AttendanceEmp.FlexiblityOutTime = String.Empty
                        AttendanceEmp.SchInTime = String.Empty
                        AttendanceEmp.SchOutTime = String.Empty
                    End If
                    'AttendanceEmp.AttendanceId = Val(Row.Cells("AttendanceId").Value.ToString)
                    AttendanceEmp.EmpID = Val(Row.Cells("Employee_Id").Value.ToString)
                    AttendanceEmp.AttendanceDate = CDate(Row.Cells(c).Column.Key.ToString).Date
                    'If Not (Me.cmbAttendanceStatus.Text = "Absent" Or Me.cmbAttendanceStatus.Text = "Leave" Or Me.cmbAttendanceStatus.Text = "Casual Leave" Or Me.cmbAttendanceStatus.Text = "Anual Leave" Or Me.cmbAttendanceStatus.Text = "Sick Leave") = True Then
                    'If Me.dtpAttendanceTime.Enabled = True Then
                    '    AttendanceEmp.AttendanceType = IIf(Me.RbtIn.Checked = True, "In", "Out")
                    '    AttendanceEmp.AttendanceTime = CDate(Me.dtpAttendanceDate.Value.Date & " " & Me.dtpAttendanceTime.Value.ToLongTimeString)  'Me.dtpAttendanceTime.Value
                    'Else
                    'AttendanceEmp.AttendanceType = Nothing
                    'AttendanceEmp.AttendanceTime = Nothing

                    'End If
                    Select Case Row.Cells(c).Value.ToString
                        Case "P"
                            AttendanceEmp.AttendanceStatus = "Present"
                            'AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                        Case "A"
                            AttendanceEmp.AttendanceStatus = "Absent"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "HA"
                            AttendanceEmp.AttendanceStatus = "Half Absent"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "SA"
                            AttendanceEmp.AttendanceStatus = "Short Absent"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "L"
                            AttendanceEmp.AttendanceStatus = "Leave"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "HL"
                            AttendanceEmp.AttendanceStatus = "Half Leave"
                            'AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                        Case "SL"
                            AttendanceEmp.AttendanceStatus = "Short Leave"
                            'AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                            'Case "AL"
                            '    AttendanceEmp.AttendanceStatus = "Anual Leave"
                        Case "ANL"
                            AttendanceEmp.AttendanceStatus = "Anual Leave"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "SKL"
                            AttendanceEmp.AttendanceStatus = "Sick Leave"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "OD"
                            AttendanceEmp.AttendanceStatus = "Outdoor Duty"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "B"
                            AttendanceEmp.AttendanceStatus = "Break"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "Off"
                            AttendanceEmp.AttendanceStatus = "Off Day"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "GL"
                            AttendanceEmp.AttendanceStatus = "Gazetted Leave"
                            AttendanceEmp.AttendanceTime = Nothing
                        Case "ML"
                            AttendanceEmp.AttendanceStatus = "Maternity Leave"
                            AttendanceEmp.AttendanceTime = Nothing
                    End Select

                    'AttendanceEmp.AttendanceStatus = Me.cmbAttendanceStatus.Text
                    AttendanceEmp.ShiftId = Val(dtShift.Rows(0).Item("ShiftId").ToString)
                    AttendanceEmp.Auto = AutoAttendance
                    AttendanceEmpDAL.AddAttendanceRegister(AttendanceEmp)
                Next
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveSingle(ByVal ColIndex As Integer) As Boolean
        Try
            ''Task No 2593 Append Some New Lines Of Code for Newly Added Fields
            ''For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
            'Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 13
            ''For ColIndex = enmEmp.Count To intCounter - 1
            'If ColIndex >= enmEmp.Count AndAlso ColIndex < intCounter - 1 Then
            Dim dtShift As DataTable = GetShift(Val(Me.grd.GetRow.Cells("ShiftGroupId").Value.ToString))
            AttendanceEmp = New AttendanceEmp
            If dtShift.Rows.Count > 0 Then
                'Dim objRow As DataRowView
                'objRow = CType(Me.cmbShift.SelectedItem, DataRowView)
                If dtShift.Rows(0).Item("FlexInTime").ToString <> "" Then
                    AttendanceEmp.FlexiblityInTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("FlexInTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.FlexiblityInTime = Date.Now
                End If
                If dtShift.Rows(0).Item("FlexOutTime").ToString <> "" Then
                    AttendanceEmp.FlexiblityOutTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("FlexOutTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.FlexiblityOutTime = Date.Now
                End If
                If dtShift.Rows(0).Item("ShiftStartTime").ToString <> "" Then
                    AttendanceEmp.SchInTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("ShiftStartTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.SchInTime = Date.Now
                End If
                If dtShift.Rows(0).Item("ShiftEndTime").ToString <> "" Then
                    AttendanceEmp.SchOutTime = Now.Date & " " & CDate(dtShift.Rows(0).Item("ShiftEndTime").ToString).ToLongTimeString
                Else
                    AttendanceEmp.SchOutTime = Date.Now
                End If

            Else
                AttendanceEmp.FlexiblityInTime = String.Empty
                AttendanceEmp.FlexiblityOutTime = String.Empty
                AttendanceEmp.SchInTime = String.Empty
                AttendanceEmp.SchOutTime = String.Empty
            End If
            'AttendanceEmp.AttendanceId = Val(Row.Cells("AttendanceId").Value.ToString)
            AttendanceEmp.EmpID = Val(Me.grd.GetRow.Cells("Employee_Id").Value.ToString)
            AttendanceEmp.AttendanceDate = CDate(Me.grd.GetRow.Cells(ColIndex).Column.Key.ToString).Date
            'If Not (Me.cmbAttendanceStatus.Text = "Absent" Or Me.cmbAttendanceStatus.Text = "Leave" Or Me.cmbAttendanceStatus.Text = "Casual Leave" Or Me.cmbAttendanceStatus.Text = "Anual Leave" Or Me.cmbAttendanceStatus.Text = "Sick Leave") = True Then
            'If Me.dtpAttendanceTime.Enabled = True Then
            '    AttendanceEmp.AttendanceType = IIf(Me.RbtIn.Checked = True, "In", "Out")
            '    AttendanceEmp.AttendanceTime = CDate(Me.dtpAttendanceDate.Value.Date & " " & Me.dtpAttendanceTime.Value.ToLongTimeString)  'Me.dtpAttendanceTime.Value
            'Else
            AttendanceEmp.AttendanceType = Nothing
            'AttendanceEmp.AttendanceTime = Nothing


            'End If
            Select Case Me.grd.GetRow.Cells(ColIndex).Value.ToString
                Case "P"
                    AttendanceEmp.AttendanceStatus = "Present"
                    AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                Case "A"
                    AttendanceEmp.AttendanceStatus = "Absent"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "HA"
                    AttendanceEmp.AttendanceStatus = "Half Absent"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "SA"
                    AttendanceEmp.AttendanceStatus = "Short Absent"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "L"
                    AttendanceEmp.AttendanceStatus = "Leave"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "HL"
                    AttendanceEmp.AttendanceStatus = "Half Leave"
                    AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                Case "SL"
                    AttendanceEmp.AttendanceStatus = "Short Leave"
                    AttendanceEmp.AttendanceTime = CDate(Now.Date & " " & Now.ToLongTimeString)
                    'Case "AL"
                    '    AttendanceEmp.AttendanceStatus = "Anual Leave"
                Case "ANL"
                    AttendanceEmp.AttendanceStatus = "Anual Leave"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "SKL"
                    AttendanceEmp.AttendanceStatus = "Sick Leave"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "OD"
                    AttendanceEmp.AttendanceStatus = "Outdoor Duty"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "B"
                    AttendanceEmp.AttendanceStatus = "Break"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "Off"
                    AttendanceEmp.AttendanceStatus = "Off Day"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "GL"
                    AttendanceEmp.AttendanceStatus = "Gazetted Leave"
                    AttendanceEmp.AttendanceTime = Nothing
                Case "ML"
                    AttendanceEmp.AttendanceStatus = "Maternity Leave"
                    AttendanceEmp.AttendanceTime = Nothing
            End Select

            'AttendanceEmp.AttendanceStatus = Me.cmbAttendanceStatus.Text
            AttendanceEmp.ShiftId = Val(dtShift.Rows(0).Item("ShiftId").ToString)
            AttendanceEmp.Auto = AutoAttendance
            AttendanceEmpDAL.AddSingle(AttendanceEmp)
            'Next
            'Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetShift(ByVal ShiftGroupId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            'str = "Select * From (SELECT DISTINCT dbo.ShiftTable.ShiftId, dbo.ShiftTable.ShiftName, Convert(datetime,FlexInTime) as FlexInTime, Convert(datetime,FlexOutTime) as FlexOutTime, IsNull(OvertimeRate,0) as OverTimeRate, Convert(DateTime, ShiftStartTime) as ShiftStartTime, Convert(DateTime,ShiftEndTime) as ShiftEndTime, tblDefEmployee.Employee_Id FROM dbo.tblDefEmployee INNER JOIN dbo.ShiftGroupTable ON dbo.tblDefEmployee.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId INNER JOIN dbo.ShiftScheduleTable ON dbo.ShiftGroupTable.ShiftGroupId = dbo.ShiftScheduleTable.ShiftGroupId INNER JOIN dbo.ShiftTable ON dbo.ShiftScheduleTable.ShiftId = dbo.ShiftTable.ShiftId) a WHERE a.Employee_Id=" & EmpId & " AND ('" & Me.dtpAttendanceTime.Value.ToLongTimeString & "' BETWEEN left(right(convert (varchar, a.ShiftStartTime, 114), 14),8) + ' ' + Right(Convert(varchar, a.ShiftStartTime, 109),2)   AND left(right(convert (varchar, a.ShiftEndTime, 114), 14),8) + ' ' + Right(Convert(varchar, a.ShiftEndTime, 109),2))"
            str = "SELECT DISTINCT dbo.ShiftTable.ShiftId, dbo.ShiftTable.ShiftName, Convert(datetime,FlexInTime) as FlexInTime, Convert(datetime,FlexOutTime) as FlexOutTime, IsNull(OvertimeRate,0) as OverTimeRate, Convert(DateTime, ShiftStartTime) as ShiftStartTime, Convert(DateTime,ShiftEndTime) as ShiftEndTime FROM ShiftTable WHERE ShiftId IN(Select ShiftId From ShiftScheduleTable WHERE ShiftGroupId=" & ShiftGroupId & ")"
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            Return dt
            'Me.cmbShift.DataSource = Nothing
            'FillDropDown(Me.cmbShift, str, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            Try
                If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Dim Index As Integer = e.Column.Index
                    Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 13
                    If Index >= enmEmp.Count AndAlso Index <= intCounter - 1 Then
                        Me.grd.GetRow.BeginEdit()
                        Dim Key As String = e.Column.Key
                        Dim Value As String = Me.grd.GetRow.Cells(Index).Value.ToString
                        Me.grd.GetRow.Cells(Index).Value = Value
                        Me.grd.GetRow.EndEdit()
                        If SaveSingle(Index) Then
                            msg_Information("Attendance status has been updated successfully.")
                        End If
                    End If
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try
            ''TASK:991 mark red color of cell in case value is Off Day. Done by Ameen on 23-06-2017
            If Not e.Row Is Nothing Then
                Dim Index As Integer = 0 ''e.Column.Index
                Dim intCounter As Integer = Me.grd.RootTable.Columns.Count - 13
                For i As Integer = 1 To intCounter
                    Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                    rowStyle.BackColor = Color.Red
                    If e.Row.Cells(i).Value.ToString = "Off" Then
                        e.Row.Cells(i).FormatStyle = rowStyle

                        'e.Row.
                    End If
                Next
                'End If
            End If
            ''TASK:991

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
            dv.RowFilter = "[Employee_Name] Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class