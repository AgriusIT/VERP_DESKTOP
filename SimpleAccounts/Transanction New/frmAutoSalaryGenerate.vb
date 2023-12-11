'2015-06-05 Task#201506002 Ali Ansari Save Auto Salary Generate and general improvements of form
''23-9-2015 TASK239151 Imran Ali Fixed/Percentage Value Apply
''TASK: TFS881 Auto Salary Generation criteria is not working properly. Ameen on 16-08-2017
Imports System.Data.OleDb
Imports SBModel
Public Class frmAutoSalaryGenerate
    Public Enum enmSalary
        Dept_Id
        Desig_Id
        ShiftGroupId
        City_ID
        SalaryExpAcId
        EmployeeId
        EmployeeCode
        EmployeeName
        Designation
        Department
        SalaryRate
        EmpSalaryAccountId
        CostCentre
        CostCenterName
        ProcessId
        SalaryExpId
        MonthDays
        WorkingDays
        TotalLeaves
        PrvLeave
        LeaveDays
        LeaveBalance
        PresentDays
        AbsentDays
        LeavDeduction
        OverTimeHrs
        OTAmount
        InComeTax
        taxableIncome
        VisitAllowance
        Count
    End Enum
    Enum enmSalaryType
        SalaryExpTypeId
        SalaryDeduction
        SalaryExpType
        AccountId
        DeductionAgainstLeaves
        AllowanceOverTime
        DeductionAgainsIncomeTax
        IncomeTaxExempted
        GrossSalaryType
        'TASK239151 Added Indedx
        ApplyValue
        'END TASK239151
    End Enum
    Dim intProcessId As Integer = 0I
    Dim blnPosted As Boolean = False
    Dim ITaxAccount As String = String.Empty
    Private Sub frmAutoSalaryGenerate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombo()
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.Text = Date.Now.ToString("MMMMM")
            Me.txtYear.Text = Date.Now.Year
            Me.dtpSalaryDate.Value = Date.Now
            GetProcessRecords()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(ByVal ProcessId As Integer)
        Try
            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            Dim DtpLeavePolicyDate As DateTime = getConfigValueByType("Attendance_Period").ToString
            Dim dtpPrevMonthEnd As DateTime
            'Ali Faisal : TFS1618 : Previous Month end date error when Current month is January
            If Me.cmbMonth.SelectedIndex = 0 Then
                dtpPrevMonthEnd = CDate(Val(Me.txtYear.Text - 1) & "-" & GetMonthName(Me.cmbMonth.SelectedIndex + 12) & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 12), Val(Me.txtYear.Text - 1)))
            Else
                dtpPrevMonthEnd = CDate(Val(Me.txtYear.Text) & "-" & GetMonthName(Me.cmbMonth.SelectedIndex) & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex), Val(Me.txtYear.Text)))
            End If
            'Ali Faisal : TFS1618 : End
            Dim strFilter As String = String.Empty

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
            'Dim sp As String = "Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id, Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department, IsNull(EmployeesView.Salary,0) as [Basic Salary],isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId, IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId From EmployeesView LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable where ProcessId=" & ProcessId & ") SalaryExp on SalaryExp.EmployeeId = EmployeesView.Employee_Id"
            'sp += " WHERE Employee_Name <> '' AND IsNull(Active,0) = 1"
            'sp += " ORDER BY 2 Asc"
            '"SP_EmployeeSalarySheet '" & dtpFrom.Value & "','" & dtpTo.Value & "'"
            'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId,City_Id,Employee_Id,Employee_Code, 0 as EmployeeType,0 as LeavesAlloted,Employee_Name,  Designation,Department,[Basic Salary],EmpSalaryAccountId,ProcessId,SalaryExpId,   WorkingDays,TotalLeaves,PresentDays,LeaveDays,isnull(PrvLeave,0) as  PrvLeave " _
            '            & " , (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance,  case when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) > " _
            '            & "  0 then  0  when PrvLeave  > convert(money,totalleaves,5)  then - -convert(int,(([Basic Salary]/convert(int,WorkingDays))) * leaveDays) when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) < 0 then " _
            '             & " -convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  (convert(money,TotalLeaves,5) - (LeaveDays + PrvLeave))),0) end as LeavDeduction ,isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax, Convert(decimal,0) as _taxableIncome from (Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department,IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId,  IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId  ,(select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays,   " _
            '            & " (select config_value from  ConfigValuesTable where config_type = 'Leave_Days') as TotalLeaves, sum(isnull(PresentDays,0)) as PresentDays,sum(isnull(Tleaves,0)) as LeaveDays  ,PrevBalance.PreviousLeave as PrvLeave ,sum(isnull(Ot_Amount,0)) as OT_AMount, Convert(decimal,0) as _IncomeTax from EmployeesView " _
            '            & "LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable   where ProcessId=" & intProcessId & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id  left outer join (select empid,sum(tleaves)  as PreviousLeave from   Vw_EmpAttendance   where convert(datetime,attendancedate) between '" & DtpLeavePolicyDate & "' and '" & DateAdd(DateInterval.Day, -1, dtpFromDate) & "'  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid left outer join (select empid,sum(PresentDays) as PresentDays,sum(LeavesDays)  as TLEaves from   Vw_EmpAttendance   where convert(datetime,attendancedate) between '" & dtpFromDate & "' and '" & dtpToDate & "'  group by empid )  CurrentLEaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            '            & "left join  (select employeeid,sum(OTAmount)  AS Ot_Amount from (select employeeid,datediff(dd,start_date,end_date)  * datediff(hh,start_time,End_time) * overtime_rate_hr as OTAmount   from tblEmployeeOverTimeSchedule where start_date between '" & dtpFromDate & "' and '" & dtpToDate & "' ) as a group by employeeid ) as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  " _
            '            & " WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1  group by Dept_Id,Desig_Id,ShiftGroupId,Employee_Code,Employee_Name,EmployeeDesignationName, Salary,EmpSalaryAccountId,ProcessId,City_ID,Employee_ID,EmployeeDeptName,SalaryExpId,PreviousLeave  ) as a"

            'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId,City_Id,Employee_Id,Employee_Code,Employee_Name,  Designation,Department,[Basic Salary],EmpSalaryAccountId,ProcessId,SalaryExpId, WorkingDays ,TotalLeaves,isnull(PrvLeave,0) as  PrvLeave,LeaveDays,(convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance,PresentDays " _
            '            & " , case when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) > " _
            '            & "  0 then  0  when IsNull(PrvLeave,0)  > convert(money,totalleaves,5)  then convert(int,(([Basic Salary]/convert(int,WorkingDays))) * IsNull(leaveDays,0)) when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) < 0 then " _
            '             & "  -convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  IsNull(AbsentDays,0))),0) end as LeavDeduction ,isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax, Convert(decimal,0) as _taxableIncome from (Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department,IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId,  IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId  ,(select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays,   " _
            '            & " isnull(leavesalloted,0) as TotalLeaves, sum(isnull(PresentDays,0)) as PresentDays,sum(isnull(Tleaves,0)) as LeaveDays  ,PrevBalance.PreviousLeave as PrvLeave ,sum(isnull(Ot_Amount,0)) as OT_AMount, Convert(decimal,0) as _IncomeTax from EmployeesView " _
            '            & "LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable   where ProcessId=" & intProcessId & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id  left outer join (select EmpId,sum(LeavesDays)  as PreviousLeave from Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & DateAdd(DateInterval.Day, -1, dtpFromDate).ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid left outer join (select empid,sum(IsNull(PresentDays,0)) as PresentDays,sum(IsNull(LeavesDays,0))  as TLeaves, Sum(IsNull(AbsentDays,0)) as AbsentDays from   Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(datetime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            '            & "left join  (select employeeid,sum(OTAmount)  AS Ot_Amount from (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(hh,start_time,End_time) * IsNull(overtime_rate_hr,0))) as OTAmount   from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) ) as a group by employeeid ) as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  " _
            '            & " WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1  group by Dept_Id,Desig_Id,ShiftGroupId,Employee_Code,Employee_Name,EmployeeDesignationName, Salary,EmpSalaryAccountId,ProcessId,City_ID,Employee_ID,EmployeeDeptName,SalaryExpId,PreviousLeave,leavesalloted  ) as a"


            'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId," _
            '& " City_Id,IsNull(SalaryExpAcId,0) as SalaryExpAcId,Employee_Id,Employee_Code,Employee_Name," _
            '& " Designation,Department,[Basic Salary],EmpSalaryAccountId," _
            '& " ProcessId,SalaryExpId, WorkingDays ,TotalLeaves," _
            '& " isnull(PrvLeave,0) as  PrvLeave,LeaveDays," _
            '& " (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance," _
            '& " PresentDays, IsNull(AbsentDays,0) as AbsentDays, " _
            '& " Convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  IsNull(AbsentDays,0)))  as LeavDeduction, isnull(Round(Convert(float, OverTimeHrs), 2), 0) As OverTimeHrs," _
            '& " isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax," _
            '& " Convert(decimal,0) as _taxableIncome  " _
            '& " from (Select IsNull(SalaryExpAcId,0) as SalaryExpAcId, IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id," _
            '& " IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id," _
            '& " IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name," _
            '& " EmployeeDesignationName as Designation, EmployeeDeptName as Department," _
            '& " IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId," _
            '& " IsNull(SalaryExp.ProcessId,0) as ProcessId,  " _
            '& " IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId, (" _
            '& " select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays," _
            '& " (isnull(leavesalloted,0)) as TotalLeaves, (isnull(PresentDays,0)) as PresentDays,(isnull(Tleaves,0)) as LeaveDays," _
            '& " (PrevBalance.PreviousLeave) as PrvLeave ," _
            '& " isnull(Convert(float, OverTimeHrs), 0) As OverTimeHrs," _
            '& " (isnull(Ot_Amount,0)) as OT_AMount," _
            '& " (IsNull(AbsentDays,0)) as AbsentDays," _
            '& " Convert(decimal,0) as _IncomeTax from EmployeesView " _
            '& " LEFT OUTER JOIN(" _
            '& " Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable " _
            '& " where ProcessId=" & ProcessId & " AND IsNull(EmpDepartmentID,0)=" & Me.cmbDepartment.SelectedValue & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
            '& " left outer join (select EmpId,sum(LeavesDays) as PreviousLeave from Vw_EmpAttendance where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & DateAdd(DateInterval.Day, -1, dtpFromDate).ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            '& " left outer join (select empid,sum(IsNull(PresentDays,0)) as PresentDays,sum(IsNull(LeavesDays,0))  as TLeaves, Sum(IsNull(AbsentDays,0)) as AbsentDays from   Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))  group by empid ) " _
            '& " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            '& " left  join  (" _
            '& " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            '& " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi,start_time,End_time)* 1/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            '& " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid   WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
            '& " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
            ''SUBSTRING(CONVERT(VARCHAR(20),(job_end - job_start),120),12,8) from tableA
            ''datediff(mi,start_time,End_time)
            ' SELECT CONVERT(NUMERIC(18, 2), 398 / 60 + (398 % 60) / 100.0)

            Dim strWorkingDays As String = DateTime.DaysInMonth(Me.txtYear.Text, (Me.cmbMonth.SelectedIndex + 1))
            'Dim sp As String = "select Dept_Id,Desig_Id,ShiftGroupId," _
            '& " City_Id,IsNull(SalaryExpAcId,0) as SalaryExpAcId,Employee_Id,Employee_Code,Employee_Name," _
            '& " Designation,Department,[Salary],EmpSalaryAccountId, CostCentre, CostCenterName As [Cost Centre Name], " _
            '& " ProcessId,SalaryExpId, MonthDays, WorkingDays,TotalLeaves," _
            '& " isnull(PrvLeave,0) as  PrvLeave,LeaveDays," _
            '& " (convert(money,isnull(Convert(float,TotalLeaves),0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance," _
            '& " PresentDays, IsNull(AbsentDays,0) as AbsentDays, " _
            '& " Convert(int,(([Salary]/ Case When convert(int, MonthDays) = 0 Then 1 Else convert(int, MonthDays) End) *  IsNull(AbsentDays,0)))  as LeavDeduction, isnull(OverTimeHrs,0) as OverTimeHrs," _
            '& " isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax," _
            '& " Convert(decimal,0) as _taxableIncome,VisitAllowance  " _
            '& " from (Select IsNull(SalaryExpAcId,0) as SalaryExpAcId, IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id," _
            '& " IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id," _
            '& " IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name," _
            '& " EmployeeDesignationName as Designation, EmployeeDeptName as Department," _
            '& " IsNull(EmployeesView.Salary,0) as [Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId," _
            '& " IsNull(EmployeesView.CostCentre, 0) As CostCentre, tblDefCostCenter.Name AS CostCenterName, IsNull(SalaryExp.ProcessId,0) as ProcessId,  " _
            '& " IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId, " _
            '& " " & Convert.ToInt32(Val(strWorkingDays)) & " as MonthDays," _
            '& " (ISNULL(PresentDays, 0)) + (ISNULL(Tleaves, 0)) As WorkingDays, " _
            '& " (isnull(leavesalloted,0)) as TotalLeaves, (isnull(PresentDays,0)) as PresentDays,(isnull(Tleaves,0)) as LeaveDays," _
            '& " (PrevBalance.PreviousLeave) as PrvLeave ," _
            '& " isnull(Convert(float, OverTimeHrs), 0) As OverTimeHrs," _
            '& " (isnull(Convert(float,Ot_Amount),0)) as OT_AMount," _
            '& " (IsNull(Convert(float,AbsentDays),0)) as AbsentDays," _
            '& " Convert(decimal,0) as _IncomeTax,ISNULL(VisitAllowance.TotalAmount, 0) AS VisitAllowance from EmployeesView LEFT JOIN tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID" _
            '& " LEFT OUTER JOIN(" _
            '& " Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable " _
            '& " where ProcessId=" & ProcessId & " AND IsNull(EmpDepartmentID,0)=" & Me.cmbDepartment.SelectedValue & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
            '& " left outer join (select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            '& " left outer join (select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "')) " _
            '& " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            '& " left  join  (" _
            '& " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            '& " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            '& " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & dtpFromDate.ToString & "',102) And Convert(DateTime,'" & dtpToDate.ToString & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> ''  AND IsNull(EmployeesView.Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
            '& " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
            'TFS4714: Waqar: Applied some changes to remove some bugs related to Cost Center and History Updation and Salary Issue
            Dim sp As String = "select Dept_Id,Desig_Id,ShiftGroupId," _
            & " City_Id,IsNull(SalaryExpAcId,0) as SalaryExpAcId,Employee_Id,Employee_Code,Employee_Name," _
            & " Designation,Department,[Salary],EmpSalaryAccountId, CostCentre, CostCenterName As [Cost Centre Name], " _
            & " ProcessId,SalaryExpId, MonthDays, WorkingDays,TotalLeaves," _
            & " isnull(PrvLeave,0) as  PrvLeave,LeaveDays," _
            & " (convert(money,isnull(Convert(float,TotalLeaves),0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance," _
            & " PresentDays, IsNull(AbsentDays,0) as AbsentDays, " _
            & " Convert(int,(([Salary]/ Case When convert(int, MonthDays) = 0 Then 1 Else convert(int, MonthDays) End) *  IsNull(AbsentDays,0)))  as LeavDeduction, isnull(OverTimeHrs,0) as OverTimeHrs," _
            & " isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax," _
            & " Convert(decimal,0) as _taxableIncome,VisitAllowance  " _
            & " from (Select IsNull(SalaryExpAcId,0) as SalaryExpAcId, IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id," _
            & " IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id," _
            & " IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name," _
            & " EmployeeDesignationName as Designation, EmployeeDeptName as Department," _
            & " IsNull(EmployeesView.Salary,0) as [Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId," _
            & " IsNull(EmployeesView.CostCentre, 0) As CostCentre, tblDefCostCenter.Name AS CostCenterName, IsNull(SalaryExp.ProcessId,0) as ProcessId,  " _
            & " IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId, " _
            & " " & Convert.ToInt32(Val(strWorkingDays)) & " as MonthDays," _
            & " (ISNULL(PresentDays, 0)) + (ISNULL(Tleaves, 0)) As WorkingDays, " _
            & " (isnull(leavesalloted,0)) as TotalLeaves, (isnull(PresentDays,0)) as PresentDays,(isnull(Tleaves,0)) as LeaveDays," _
            & " (PrevBalance.PreviousLeave) as PrvLeave ," _
            & " isnull(Convert(float, OverTimeHrs), 0) As OverTimeHrs," _
            & " (isnull(Convert(float,Ot_Amount),0)) as OT_AMount," _
            & " (IsNull(Convert(float,AbsentDays),0)) as AbsentDays," _
            & " Convert(decimal,0) as _IncomeTax,ISNULL(VisitAllowance.TotalAmount, 0) AS VisitAllowance from EmployeesView LEFT JOIN tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID" _
            & " LEFT OUTER JOIN(" _
            & " Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable " _
            & " where ProcessId=" & ProcessId & " AND IsNull(EmpDepartmentID,0)=" & Me.cmbDepartment.SelectedValue & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
            & " left outer join (select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpPrevMonthEnd.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            & " left outer join (select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "')) " _
            & " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            & " left  join  (" _
            & " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            & " (select employeeid,Case when RegularDayHrs<>0 then ISNULL(RegularDayOTRate, 0) *  ISNULL(RegularDayHrs, 0) Else ISNULL(OffDayOTRate, 0) * ISNULL(OffDayHrs, 0) end AS OTAmount, (IsNull(RegularDayHrs, 0)+IsNull(OffDayHrs, 0)) as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            & " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 23:59:59") & "',102) And Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> '' " & IIf(ProcessId > 0, "", " AND IsNull(EmployeesView.Active,0) = 1") & " AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND EmployeesView.CostCentre=" & Me.cmbCostCenter.SelectedValue & "", "") & "    " _
            & " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
            Dim dt As DataTable = GetDataTable(sp)
            'dt.Columns("Paid_Salary").Expression = "MonthlySalary-Allowance-Insurance-Gratuity_Fund-Advance-WHTax-ESSI-EOBI"
            dt.TableName = "SalarySheet"

            '  & " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            '& " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            '& " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & dtpFromDate.ToString & "',102) And Convert(DateTime,'" & dtpToDate.ToString & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> ''  AND IsNull(EmployeesView.Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
            '& " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
            'DeductionAgainstLeaves
            'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
            'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
            'TASK239151 Added Field ApplyValue And ExistingAccount
            Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(ApplyValue,'Fixed') as ApplyValue, IsNull(DeductionAgainstSalary, 0) As DeductionAgainstSalary,Isnull(SiteVisitAllowance,0) AS SiteVisitAllowance From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> ''"
            'END TASK239151
            Dim dtSalaryType As New DataTable
            dtSalaryType = GetDataTable(strSalaryType)
            dtSalaryType.AcceptChanges()
            For Each row As DataRow In dtSalaryType.Rows
                If Not dtSalaryType.Columns.Contains(row(1)) Then
                    dt.Columns.Add(row(0), GetType(System.Int16), row(0))
                    dt.Columns.Add(row(1) & "-" & row(0), GetType(System.String), row(1))
                    If row.Item("DeductionAgainstLeaves") = "True" Then
                        dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("LeavDeduction").ColumnName)
                    ElseIf row.Item("AllowanceOverTime") = "True" Then
                        dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("OT_Amount").ColumnName)
                    ElseIf row.Item("SiteVisitAllowance") = "True" Then
                        dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("VisitAllowance").ColumnName)
                    Else
                        dt.Columns.Add(row(2), GetType(System.Double))
                    End If
                    dt.Columns.Add(row(3) & "^" & row(0), GetType(System.Double), row(3))
                    dt.Columns.Add(row(enmSalaryType.DeductionAgainsIncomeTax) & "$" & row(0), GetType(System.Boolean), row(enmSalaryType.DeductionAgainsIncomeTax))
                    dt.Columns.Add(row(enmSalaryType.IncomeTaxExempted) & "#" & row(0), GetType(System.Boolean), row(enmSalaryType.IncomeTaxExempted))
                    dt.Columns.Add(row(enmSalaryType.GrossSalaryType) & "*" & row(0), GetType(System.Boolean), row(enmSalaryType.GrossSalaryType))
                    dt.Columns.Add(CStr(row(enmSalaryType.ApplyValue) & "~" & row(0)), GetType(System.String))

                End If
            Next
            For Each row As DataRow In dt.Rows
                'For c As Integer = enmSalary.Count To dt.Columns.Count - 4 Step 4
                'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
                For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                    If Not row.Table.Columns(c + 2).ReadOnly Then
                        row.BeginEdit()
                        row(c + 2) = 0
                        row.EndEdit()
                    End If
                Next
            Next
            ' If dt.Columns.Contains("") Then
            Dim strTotal As String = String.Empty
            Dim strTotalExempt As String = String.Empty
            Dim strIncomeTaxSalaryType As String = String.Empty
            Dim strGrossSalaryType As String = String.Empty
            Dim strIncomeTaxable As String = String.Empty
            Dim strBasicSalaryColName As String = String.Empty
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                'For c As Integer = enmSalary.Count To dt.Columns.Count - 9 Step 9
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 6).ColumnName, dt.Columns(c + 6).ColumnName.LastIndexOf("*") - 1 + 1))
                If flg = True Then
                    strBasicSalaryColName = dt.Columns(c + 2).ColumnName
                    strGrossSalaryType = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    Exit For
                End If
            Next
            Dim dtchk As New DataTable
            Dim str As String = String.Empty
            dtchk = GetDataTable("Select Count(*) From dbo.SalariesExpenseMasterTable WHERE ProcessId=" & ProcessId & "")
            dtchk.AcceptChanges()
            'Dim strSQL1 As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) as Amount, 'Edit' as Record_Mode " _
            '                          & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
            '                          & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId INNER JOIN SalaryExpenseType On SalariesExpenseDetailTable.SalaryExpTypeId = SalaryExpenseType.SalaryExpTypeId WHERE dbo.SalariesExpenseMasterTable.ProcessId=" & ProcessId & "  and dbo.SalariesExpenseDetailTable.SalaryExpTypeId <>  0 Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, IsNull(SalaryExpenseType.GrossSalaryType,0) Having abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) <> 0 ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"

            'Ali Faisal : Zendesk4978 : Deductions are not calculated accurately on salary change for Sultan and Kamil on 25-Oct-2018
            Dim strSQL1 As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, Case WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) Else Abs(IsNull(EmpDeductions.DeductionAmount,0)) End  Else  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) end as Amount, 'Edit' as Record_Mode " _
                                      & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
                                     & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId INNER JOIN SalaryExpenseType On SalariesExpenseDetailTable.SalaryExpTypeId = SalaryExpenseType.SalaryExpTypeId LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = dbo.SalariesExpenseMasterTable.EmployeeId WHERE dbo.SalariesExpenseMasterTable.ProcessId=" & ProcessId & "  and dbo.SalariesExpenseDetailTable.SalaryExpTypeId <>  0 Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, IsNull(SalaryExpenseType.GrossSalaryType,0), SalaryExpenseType.DeductionAgainstSalary, EmpDeductions.DeductionAmount ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"

            Dim strSQL2 As String = "Select DISTINCT IsNull(tblEmployeeAccounts.Employee_Id,0) as EmployeeId, IsNull(Type_Id,0) as SalaryExpTypeId, Case When IsNull(SalaryExpenseType.GrossSalaryType,0)=1 Then Case When IsNull(Amount,0)=0 Then IsNull(Emp.Salary,0) Else IsNull(Amount,0) End  WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then IsNull(Amount,0) Else IsNull(EmpDeductions.DeductionAmount,0) End  Else IsNull(Amount,0) end as Amount, 'New' as Record_Mode, IsNull(SalaryExpenseType.GrossSalaryType,0) as GrossSalaryType  From tblEmployeeAccounts LEFT OUTER JOIN EmployeesView Emp On Emp.Employee_Id = tblEmployeeAccounts.Employee_Id INNER JOIN SalaryExpenseType on SalaryExpenseType.SalaryExpTypeId = Type_Id LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = Emp.Employee_Id  WHERE Emp.Employee_Id <> 0 " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND Emp.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"
            If Not dtchk.Rows.Count <= 0 Then
                If Val(dtchk.Rows(0).Item(0).ToString) > 0 Then
                    str = String.Empty
                    str = strSQL1
                Else
                    str = String.Empty
                    str = strSQL2
                End If
            Else
                str = String.Empty
                str = strSQL2
            End If
            Dim dt_Data As New DataTable
            dt_Data = GetDataTable(str)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dt_Data.Select("EmployeeId='" & r("Employee_Id") & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            If (dt.Columns.IndexOf(drFound(1)) + 2) >= enmSalary.Count Then
                                If Not r.Table.Columns(dt.Columns.IndexOf(drFound(1)) + 2).ReadOnly Then
                                    r.BeginEdit()
                                    If drFound(3).ToString = "New" Then
                                        Dim strApplyValue As String = dt.Columns(dt.Columns.IndexOf(drFound(1)) + 7).ToString.Substring(0, dt.Columns(dt.Columns.IndexOf(drFound(1)) + 7).ToString.LastIndexOf("~"))
                                        If strApplyValue.Length > 0 Then
                                            If strApplyValue = "Fixed" Then
                                                r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
                                            Else
                                                Dim dblPercent As Double = (Val(drFound(2).ToString) / 100)
                                                Dim dblAmount As Double = r(dt.Columns.IndexOf("Salary"))
                                                r(dt.Columns.IndexOf(drFound(1)) + 2) = dblAmount * dblPercent
                                            End If
                                        End If
                                    Else
                                        r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
                                    End If
                                    r.EndEdit()
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            '''''''''''''''''
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                If strTotal.Length > 0 Then
                    Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
                    strTotal = strTotal & IIf(flg = False, "+", "-") & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                Else
                    strTotal = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                End If
            Next
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 4).ColumnName, dt.Columns(c + 4).ColumnName.LastIndexOf("$") - 1 + 1))
                'Microsoft.VisualBasic.Left()
                If flg = True Then
                    strIncomeTaxSalaryType = "" & dt.Columns(c + 2).ColumnName & ""
                    Exit For
                End If
            Next
            Dim strTotalEarning As String = String.Empty
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
                If flg = False Then
                    If strTotalEarning.Length > 0 Then
                        strTotalEarning = strTotalEarning & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    Else
                        strTotalEarning = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    End If
                End If
            Next
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 5).ColumnName, dt.Columns(c + 5).ColumnName.LastIndexOf("#") - 1 + 1))
                If flg = True Then
                    If strTotalExempt.Length > 0 Then
                        strTotalExempt = strTotalExempt & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    Else
                        strTotalExempt = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    End If
                End If
            Next
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
                Dim flgInEx As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 5).ColumnName, dt.Columns(c + 5).ColumnName.LastIndexOf("#") - 1 + 1))
                If flg = False AndAlso flgInEx = False Then
                    If strIncomeTaxable.Length > 0 Then
                        strIncomeTaxable = strIncomeTaxable & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    Else
                        strIncomeTaxable = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                    End If
                End If
            Next
            'Dim IncomeTax As String = Val(strGrossSalaryType) - Val(strTotalExempt)
            If strTotalExempt.Length > 0 Then
                dt.Columns("_IncomeTax").Expression = "(((" & strTotalEarning & "-IsNull([LeavDeduction],0)" & ")-(" & strTotalExempt.ToString & ")))"
            Else
                dt.Columns("_IncomeTax").Expression = strTotalEarning
            End If
            dt.AcceptChanges()
            Me.lblValidating.Visible = True
            Me.pgbValidating.Visible = True
            Application.DoEvents()
            If Me.btnSave.Text <> "&Update" Then
                'dt.Columns("_taxableIncome").Expression = strIncomeTaxable.ToString
                For Each r As DataRow In dt.Rows
                    r.BeginEdit()
                    If Val(itax(Val(r.Item("_IncomeTax").ToString))) >= 85.91333333 Then
                        'Exit Sub
                    End If
                    If strIncomeTaxSalaryType.Length > 0 Then
                        r(dt.Columns.IndexOf(strIncomeTaxSalaryType)) = itax(Val(r.Item("_IncomeTax").ToString))
                    End If
                    r.EndEdit()
                Next
            End If
            dt.Columns.Add("Total Salary", GetType(System.Double))
            dt.Columns("Total Salary").Expression = "(" & strTotal.ToString & ")"
            dt.AcceptChanges()
            Me.grdSalary.DataSource = dt
            Me.grdSalary.RetrieveStructure()
            'Me.grdSalary.

            'itax()
            ApplyGridSetting()



            'Me.grdSalary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            ''22-02-2017
            For Each Dr1 As DataRow In dtSalaryType.Rows
                If Dr1.Item("DeductionAgainstLeaves") = "True" Or Dr1.Item("AllowanceOverTime") = "True" Or Dr1.Item("DeductionAgainsIncomeTax") = "True" Or Dr1.Item("GrossSalaryType") = "True" Or Dr1.Item("DeductionAgainstSalary") = "True" Or Dr1.Item("SiteVisitAllowance") = "True" Then ''DeductionAgainstSalary
                    If Me.grdSalary.RootTable.Columns.Contains(Dr1.Item("SalaryExpType").ToString) Then
                        Me.grdSalary.RootTable.Columns(Dr1.Item("SalaryExpType").ToString).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                    'For Each Column As Janus.Windows.GridEX.GridEXColumn In Me.grdSalary.RootTable.Columns

                    'Next
                End If
            Next
            'For Each Dr1 As DataRow In dtSalaryType.Rows
            '    If Dr1.Item("DeductionAgainstLeaves") = "False" Or Dr1.Item("AllowanceOverTime") = "False" Or Dr1.Item("DeductionAgainsIncomeTax") = "False" Or Dr1.Item("GrossSalaryType") = "False" Or Dr1.Item("DeductionAgainstSalary") = "False" Or Dr1.Item("SiteVisitAllowance") = "False" Then ''DeductionAgainstSalary
            '        If Me.grdSalary.RootTable.Columns.Contains(Dr1.Item("SalaryExpType").ToString) Then
            '            Me.grdSalary.RootTable.Columns(Dr1.Item("SalaryExpType").ToString).EditType = Janus.Windows.GridEX.EditType.TextBox
            '        End If
            '        'For Each Column As Janus.Windows.GridEX.GridEXColumn In Me.grdSalary.RootTable.Columns

            '        'Next
            '    End If
            'Next
            'Me.grdSalary.FilterRowUpdateMode =
            'Me.grdSalary.RetrieveStructure()
            ''End 22-02-2017
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblValidating.Visible = False
            Me.pgbValidating.Visible = False
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grdSalary.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSalary.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSalary.RootTable.Columns(0).Visible = False
            Me.grdSalary.RootTable.Columns(1).Visible = False
            Me.grdSalary.RootTable.Columns(2).Visible = False
            Me.grdSalary.RootTable.Columns(3).Visible = False
            Me.grdSalary.RootTable.Columns(4).Visible = False
            Me.grdSalary.RootTable.Columns("Employee_ID").Visible = False
            Me.grdSalary.RootTable.Columns("_InComeTax").Visible = False
            Me.grdSalary.RootTable.Columns("_taxableIncome").Visible = False
            Me.grdSalary.RootTable.Columns("ProcessId").Visible = False
            Me.grdSalary.RootTable.Columns("SalaryExpId").Visible = False
            Me.grdSalary.RootTable.Columns("Salary").Visible = False
            Me.grdSalary.RootTable.Columns("EmpSalaryAccountId").Visible = False
            Me.grdSalary.RootTable.Columns("SalaryExpAcId").Visible = False
            Me.grdSalary.RootTable.Columns("CostCentre").Visible = False
            Me.grdSalary.FrozenColumns = 10
            'For c As Integer = enmSalary.Count To Me.grdSalary.RootTable.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To Me.grdSalary.RootTable.Columns.Count - 8 Step 8
                If Me.grdSalary.RootTable.Columns(c).Key <> "Total Salary" Then
                    Me.grdSalary.RootTable.Columns(c).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 1).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 4).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 5).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 6).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 7).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 2).AllowSort = False
                    Me.grdSalary.RootTable.Columns(c + 2).CellStyle.BackColor = Color.WhiteSmoke
                    Me.grdSalary.RootTable.Columns(c + 3).Visible = False
                    Me.grdSalary.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdSalary.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdSalary.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdSalary.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                    Me.grdSalary.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
                End If
            Next

            Dim dtTest As DataTable = Me.grdSalary.DataSource

            Me.grdSalary.RootTable.Columns("WorkingDays").Caption = "Working Days"
            Me.grdSalary.RootTable.Columns("MonthDays").Caption = "Days In Month"

            'Ali Faisal : Stop Editing of fileds not to be Editable on 03-04-2017
            'Start
            Me.grdSalary.RootTable.Columns("Employee_Code").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("Employee_Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("Designation").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("Department").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("Cost Centre Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("MonthDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("WorkingDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("TotalLeaves").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("PrvLeave").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeaveDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeaveBalance").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeavDeduction").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("OverTimeHrs").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("OT_Amount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("VisitAllowance").EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grdSalary.RootTable.Columns("Employee_Code").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("Employee_Name").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("Designation").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("Department").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("Cost Centre Name").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("MonthDays").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("WorkingDays").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("TotalLeaves").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("PrvLeave").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("LeaveDays").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("LeaveBalance").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("LeavDeduction").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("OverTimeHrs").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("OT_Amount").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdSalary.RootTable.Columns("VisitAllowance").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'End

            Me.grdSalary.RootTable.Columns("Total Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSalary.RootTable.Columns("Total Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("Total Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("Total Salary").CellStyle.BackColor = Color.Honeydew

            Me.grdSalary.RootTable.Columns("Salary").FormatString = "N"
            Me.grdSalary.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSalary.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ''''''''''''''''''''''''''''''''''
            'Altered By Ali Ansari Task# 201507004 to apply grid settings on Leave Columns
            'WorkingDays
            Me.grdSalary.RootTable.Columns("WorkingDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("WorkingDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("WorkingDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("WorkingDays").Caption = "Working Days"

            Me.grdSalary.RootTable.Columns("MonthDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("MonthDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("MonthDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("MonthDays").Caption = "Days In Month"
            'Me.grdSalary.RootTable.Columns("WorkingDays").EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grdSalary.RootTable.Columns("MonthDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("MonthDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("MonthDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("MonthDays").Caption = "Days In Month"
            'Me.grdSalary.RootTable.Columns("WorkingDays").EditType = Janus.Windows.GridEX.EditType.NoEdit


            Me.grdSalary.RootTable.Columns("TotalLeaves").FormatString = "N"
            Me.grdSalary.RootTable.Columns("TotalLeaves").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("TotalLeaves").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("TotalLeaves").Caption = "Allowed Leaves"
            'Me.grdSalary.RootTable.Columns("TotalLeaves").EditType = Janus.Windows.GridEX.EditType.NoEdit



            Me.grdSalary.RootTable.Columns("AbsentDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("AbsentDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("AbsentDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("AbsentDays").Caption = "Absent Days"
            Me.grdSalary.RootTable.Columns("AbsentDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdSalary.RootTable.Columns("AbsentDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("PresentDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("PresentDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("PresentDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("PresentDays").Caption = "Present Days"
            Me.grdSalary.RootTable.Columns("PresentDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdSalary.RootTable.Columns("PresentDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeaveDays").FormatString = "N"
            Me.grdSalary.RootTable.Columns("LeaveDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeaveDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeaveDays").Caption = "Current Leaves"
            'Me.grdSalary.RootTable.Columns("LeaveDays").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("PrvLeave").FormatString = "N"
            Me.grdSalary.RootTable.Columns("PrvLeave").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("PrvLeave").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("PrvLeave").Caption = "Availed Leaves"
            'Me.grdSalary.RootTable.Columns("PrvLeave").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeaveBalance").FormatString = "N"
            Me.grdSalary.RootTable.Columns("LeaveBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeaveBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeaveBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdSalary.RootTable.Columns("LeaveBalance").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("LeavDeduction").FormatString = "N"
            Me.grdSalary.RootTable.Columns("LeavDeduction").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeavDeduction").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("LeavDeduction").Caption = "Absent Deduction"
            Me.grdSalary.RootTable.Columns("LeavDeduction").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdSalary.RootTable.Columns("LeavDeduction").EditType = Janus.Windows.GridEX.EditType.NoEdit
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            'Altered By Ali Ansari Task# 201507004 to apply grid settings on Leave Columns
            Me.grdSalary.RootTable.Columns("OverTimeHrs").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("OverTimeHrs").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdSalary.RootTable.Columns("OverTimeHrs").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSalary.RootTable.Columns("Ot_Amount").FormatString = "N"
            Me.grdSalary.RootTable.Columns("Ot_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("Ot_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("Ot_Amount").Caption = "Over Time"
            Me.grdSalary.RootTable.Columns("Ot_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSalary.RootTable.Columns("Total Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSalary.RootTable.Columns("Ot_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSalary.RootTable.Columns("Ot_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSalary.RootTable.Columns("Total Salary").FormatString = "N" & DecimalPointInValue
            Me.grdSalary.RootTable.Columns("Total Salary").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSalary.RootTable.Columns("Total Salary").FormatString = "N" & DecimalPointInValue ''

            Me.grdSalary.RootTable.Columns("VisitAllowance").FormatString = "N"
            Me.grdSalary.RootTable.Columns("VisitAllowance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("VisitAllowance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalary.RootTable.Columns("VisitAllowance").Caption = "Visit Allowance"
            Me.grdSalary.RootTable.Columns("VisitAllowance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSalary.RootTable.Columns("VisitAllowance").FormatString = "N" & DecimalPointInValue
            Me.grdSalary.RootTable.Columns("VisitAllowance").TotalFormatString = "N" & DecimalPointInValue

            'CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSalary.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
    '    Try

    '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployeeSalarySheet.Name) Then
    '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployeeSalarySheet.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '            'Me.grdSaved.SaveLayoutFile(fs)
    '            Me.grdEmployeeSalarySheet.LoadLayoutFile(fs)
    '            fs.Close()
    '            fs.Dispose()
    '        End If
    '        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Wise Monthly Salary Sheet " & vbCrLf & " Date From: " & Me.dtpFrom.Value & "  Date To: " & Me.dtpTo.Value & ""


    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub


    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            If Me.grdSalary.RowCount > 0 Then Me.grdSalary.ClearStructure()
            If IsValidateSalaryMonth() = False Then
                ShowErrorMessage("Already salary has been generated of this month [" & Me.cmbMonth.Text & "," & Me.txtYear.Text & "]")
                Exit Sub
            End If
            If Me.chkAutoAdjustAbsentAttendance.Checked = True Then If Not msg_Confirm("Please attention!, its going to mark absent of employees in which they din't mark their present") = False Then AbsentAttendanceMark()
            FillGrid(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub AbsentAttendanceMark()
        Try

            Dim startDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim str As String = getConfigValueByType("DayOff").ToString
            Dim strArr() As String
            strArr = str.Split(",")

            Dim i As Integer = 0I
            Do While i <= Date.DaysInMonth(startDate.Year, startDate.Month)
                Dim AttendanceDate As DateTime = startDate.AddDays(i)
                dayOfWeek = AttendanceDate.DayOfWeek.ToString

                Dim strDay As String = String.Empty

                If Array.Find(strArr, AddressOf getDayName) IsNot Nothing Then
                    strDay = Array.Find(strArr, AddressOf getDayName)
                End If

                If strDay = String.Empty Or strDay.ToUpper <> dayOfWeek.ToUpper Then
                    dayOfWeek = String.Empty
                    Dim cmd As New OleDbCommand
                    Dim objConn As New OleDbConnection(Con.ConnectionString)
                    If objConn.State = ConnectionState.Closed Then objConn.Open()
                    Dim trans As OleDbTransaction = objConn.BeginTransaction
                    Try
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = objConn
                        cmd.Transaction = trans
                        cmd.CommandTimeout = 120
                        cmd.CommandText = ""
                        cmd.CommandText = "INSERT INTO tblAttendanceDetail(EmpId, AttendanceDate, AttendanceStatus, ShiftId, Auto) Select Employee_Id, Convert(dateTime,'" & AttendanceDate.ToString("yyyy-M-d 00:00:00") & "',102),'Absent',ShiftId,0 as Auto From EmployeesView WHERE Convert(dateTime,'" & AttendanceDate.ToString("yyyy-M-d 00:00:00") & "',102) Not In(Select Convert(DateTime,Convert(varchar,AttendanceDate,102),102) From tblAttendanceDetail) AND EmployeesView.Active=1 "
                        cmd.ExecuteNonQuery()
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                    End Try
                End If
                i += 1
            Loop
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            'Dim transdate As Date
            'transdate = GetLastDateOfMonth(Val(txtYear.Text), Trim(cmbMonth.Text))
            Me.grdSalary.UpdateData()
            If Me.grdSalary.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Exit Sub
            End If
            If IsValidToSave(intProcessId) = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Save()
                    msg_Information("Salary has been generated for the month of [" & Me.cmbMonth.Text & "," & Me.txtYear.Text & "]")
                    RestControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    msg_Information("Salary has been updated for the month of [" & Me.cmbMonth.Text & "," & Me.txtYear.Text & "]")
                    Update1()
                    RestControls()
                End If
            Else
                MsgBox("Salary already posted, kindly un-post to edit", MsgBoxStyle.Information)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Me.grdProcess_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function Update1() As Boolean
        Dim objCommand As New OleDbCommand
        Dim ObjCommand2 As New OleDbCommand
        Dim objCon As OleDbConnection


        Dim TradeValue As Double = 0D
        Dim transdate As DateTime
        Dim TransId As String = String.Empty
        Dim SalaryId As Integer = 0I
        Dim str As String = String.Empty


        transdate = dtpSalaryDate.Value 'GetLastDateOfMonth(Val(txtYear.Text), Trim(cmbMonth.Text))

        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()



        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From SalariesExpenseDetailTable WHERE SalaryExpId In(Select SalaryExpId From SalariesExpenseMasterTable where ProcessId=" & intProcessId & ")"
            objCommand.ExecuteNonQuery()

            Dim dtEmpData As New DataTable
            dtEmpData = GetDataTable("SELECT empAc.Account_Id, empAc.Employee_Id, empAc.Amount, sType.flgAdvance,IsNull(empAc.Type_Id,0) as Type_Id FROM dbo.tblEmployeeAccounts AS empAc INNER JOIN dbo.SalaryExpenseType AS sType ON empAc.Type_Id = sType.SalaryExpTypeId WHERE   (sType.flgAdvance = 1) ORDER BY sType.flgAdvance", trans)
            dtEmpData.AcceptChanges()

            For i As Integer = 0 To Me.grdSalary.RowCount - 1

                If Val(Me.grdSalary.GetRows(i).Cells("SalaryExpId").Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update SalariesExpenseMasterTable Set NetSalary=" & Val(Me.grdSalary.GetRows(i).Cells("Total Salary").Value.ToString) & ", EmpDepartmentID=" & Me.cmbDepartment.SelectedValue & ", SalaryExpDate=Convert(dateTime,'" & dtpSalaryDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)  WHERE SalaryExpId=" & Val(Me.grdSalary.GetRows(i).Cells("SalaryExpId").Value.ToString) & " And ProcessId=" & intProcessId & ""
                    objCommand.ExecuteNonQuery()



                    For j As Integer = enmSalary.Count To Me.grdSalary.RootTable.Columns.Count - 8 Step 8
                        Dim dr() As DataRow = dtEmpData.Select("Employee_Id=" & Val(grdSalary.GetRows(i).Cells("Employee_Id").Value.ToString) & " AND Type_Id=" & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "")

                        Dim intAccountId As Integer = 0I

                        If dr.Length > 0 Then
                            intAccountId = Val(dr(0).Item(0).ToString)
                        End If

                        Dim strType As String = "SELECT SalaryExpTypeId FROM SalaryExpenseType WHERE DeductionAgainstSalary = 'True'"
                        Dim dtType As DataTable = GetDataTable(strType)
                        Dim SalaryExpTypeId As Integer = 0I
                        If dtType IsNot Nothing Then
                            If dtType.Rows.Count > 0 Then
                                SalaryExpTypeId = Val(dtType.Rows(0).Item(0))
                            Else
                                SalaryExpTypeId = 0
                            End If
                        End If
                        Dim AdvStr As String = ""
                        Dim AdvDt As DataTable
                        Dim flgAdvance As Boolean = False
                        If Val(grdSalary.GetRows(i).Cells(j).Value.ToString) = Val(SalaryExpTypeId) Then
                            AdvStr = "SELECT DeductionsDetailTable.EmployeeId, ISNULL(tblDefAdvancesType.AccountId,0) AccountId, ISNULL(DeductionsDetailTable.DeductionAmount,0) Amount FROM DeductionsDetailTable INNER JOIN AdvanceRequestTable ON DeductionsDetailTable.RequestID = AdvanceRequestTable.RequestID LEFT OUTER JOIN tblDefAdvancesType ON AdvanceRequestTable.Advance_TypeID = tblDefAdvancesType.Id WHERE (DeductionsDetailTable.EmployeeId = " & Val(grdSalary.GetRows(i).Cells("Employee_Id").Value.ToString) & ") AND DeductionsDetailTable.EntryDate BETWEEN '" & dtpFromDate & "' AND '" & dtpToDate & "'"
                            AdvDt = GetDataTable(AdvStr)
                            If AdvDt IsNot Nothing Then
                                If AdvDt.Rows.Count > 0 Then
                                    If Val(AdvDt.Rows(0).Item(1)) > Val(0) Then
                                        flgAdvance = True
                                        intAccountId = Val(AdvDt.Rows(0).Item(1))
                                    Else
                                        flgAdvance = False
                                    End If
                                End If
                            End If
                        End If

                        If Me.grdSalary.RootTable.Columns(j).DataMember <> "Total Salary" Then
                            If grdSalary.GetRow(i).Cells(j + 1).Value.ToString = "False" Then
                                objCommand.CommandText = ""
                                objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,Earning,deduction) values (" & Val(Me.grdSalary.GetRows(i).Cells("SalaryExpId").Value.ToString) & "," & IIf(intAccountId = 0, Val(grdSalary.GetRows(i).Cells(j + 3).Value.ToString), intAccountId) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(grdSalary.GetRows(i).Cells(j + 2).Value.ToString) & ",0)"
                                objCommand.ExecuteNonQuery()
                            Else
                                If flgAdvance = True Then
                                    For Each row As DataRow In AdvDt.Rows
                                        objCommand.CommandText = ""
                                        objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,deduction,Earning) values (" & Val(Me.grdSalary.GetRows(i).Cells("SalaryExpId").Value.ToString) & "," & Val(row.Item(1)) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(row.Item(2)) & ",0)"
                                        objCommand.ExecuteNonQuery()
                                    Next
                                Else
                                    objCommand.CommandText = ""
                                    objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,deduction,Earning) values (" & Val(Me.grdSalary.GetRows(i).Cells("SalaryExpId").Value.ToString) & "," & IIf(intAccountId = 0, Val(grdSalary.GetRows(i).Cells(j + 3).Value.ToString), intAccountId) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(grdSalary.GetRows(i).Cells(j + 2).Value.ToString) & ",0)"
                                    objCommand.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    Next
                End If
            Next

            trans.Commit()
            Update1 = True
            'InsertVoucher()
            If Me.grdSalary.RowCount > 0 Then Me.grdSalary.ClearStructure()
            Me.grdSalary.UpdateData()

        Catch ex As Exception
            trans.Rollback()
            Update1 = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        'SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)

    End Function
    Private Function Save() As Boolean
        Dim objCommand As New OleDbCommand
        Dim ObjCommand2 As New OleDbCommand
        Dim objCon As OleDbConnection


        Dim TradeValue As Double = 0D
        Dim transdate As DateTime
        Dim TransId As String = String.Empty
        Dim SalaryId As Integer = 0I
        Dim str As String = String.Empty
        

        transdate = dtpSalaryDate.Value 'GetLastDateOfMonth(Val(txtYear.Text), Trim(cmbMonth.Text))

        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()



        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans


            objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblSalaryProcess(SalaryProcessDate,Posted,UserName) Values(Convert(datetime,'" & dtpSalaryDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),0, '" & LoginUserName.Replace("'", "''") & "')Select @@Identity"
            'objCommand.CommandText = "INSERT INTO tblSalaryProcess(SalaryProcessDate,SalaryMonth,SalaryYear,Posted,UserName,AutoAdjustAbsentAttendance,EmpDepartmentID) Values(Convert(datetime,'" & dtpSalaryDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Val(Me.cmbMonth.SelectedIndex + 1) & "," & Val(Me.txtYear.Text) & ",0, '" & LoginUserName.Replace("'", "''") & "'," & IIf(Me.chkAutoAdjustAbsentAttendance.Checked = True, 1, 0) & "," & Me.cmbDepartment.SelectedValue & ")Select @@Identity"
            objCommand.CommandText = "INSERT INTO tblSalaryProcess(SalaryProcessDate,SalaryMonth,SalaryYear,Posted,UserName,AutoAdjustAbsentAttendance,EmpDepartmentID,CostCenterId) Values(Convert(datetime,'" & dtpSalaryDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Val(Me.cmbMonth.SelectedIndex + 1) & "," & Val(Me.txtYear.Text) & ",0, '" & LoginUserName.Replace("'", "''") & "'," & IIf(Me.chkAutoAdjustAbsentAttendance.Checked = True, 1, 0) & "," & Me.cmbDepartment.SelectedValue & ", " & Me.cmbCostCenter.SelectedValue & ")Select @@Identity"
            intProcessId = objCommand.ExecuteScalar()

            Dim dtEmpData As New DataTable
            dtEmpData = GetDataTable("SELECT empAc.Account_Id, empAc.Employee_Id, empAc.Amount, sType.flgAdvance,IsNull(empAc.Type_Id,0) as Type_Id FROM dbo.tblEmployeeAccounts AS empAc INNER JOIN dbo.SalaryExpenseType AS sType ON empAc.Type_Id = sType.SalaryExpTypeId WHERE   (sType.flgAdvance = 1) ORDER BY sType.flgAdvance", trans)
            dtEmpData.AcceptChanges()


            For i As Integer = 0 To Me.grdSalary.RowCount - 1
                Dim GrossSalary As Double
                If grdSalary.RootTable.Columns.Contains("Gross Salary") = True Then
                    GrossSalary = Val(grdSalary.GetRow(i).Cells("Gross Salary").Value.ToString)
                Else
                    GrossSalary = Val(grdSalary.GetRow(i).Cells("Basic Salary").Value.ToString)
                End If

                'If Val(grdSalary.GetRows(i).Cells("Total Salary").Value.ToString) > 0 Then
                'Ali Faisal : TFS1659 : If payable is zero then salary is not saving for that employee
                If GrossSalary > 0 Then
                    'Ali Faisal : TFS1659 : End

                    TransId = GetNextSalaryNo("ES-" & dtpSalaryDate.Value.ToString("yy"), 5, "SalariesExpenseMasterTable", "SalaryExpNo", trans)
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Insert into SalariesExpenseMasterTable(SalaryExpDate,SalaryExpNo,employeeid,autogenerate,fdate,netsalary,GrossSalary,CostCenterId,ProcessId,EmpDepartmentID) Values ('" & transdate & "', '" & TransId & "'," & Val(grdSalary.GetRows(i).Cells("Employee_Id").Value.ToString) & ",1,'" & Date.Today & "'," & Val(grdSalary.GetRows(i).Cells("Total Salary").Value.ToString) & "," & GrossSalary & ", " & Val(grdSalary.GetRows(i).Cells("CostCentre").Value.ToString) & "  ," & intProcessId & ", " & Me.cmbDepartment.SelectedValue & ") SELECT @@IDENTITY"
                    Dim intSalaryExpId As Integer = objCommand.ExecuteScalar()




                    For j As Integer = enmSalary.Count To Me.grdSalary.RootTable.Columns.Count - 8 Step 8
                        If Me.grdSalary.RootTable.Columns(j).DataMember <> "Total Salary" Then


                            Dim dr() As DataRow = dtEmpData.Select("Employee_Id=" & Val(grdSalary.GetRows(i).Cells("Employee_Id").Value.ToString) & " AND Type_Id=" & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "")

                            Dim intAccountId As Integer = 0I

                            If dr.Length > 0 Then
                                intAccountId = Val(dr(0).Item(0).ToString)
                            End If

                            Dim strType As String = "SELECT SalaryExpTypeId FROM SalaryExpenseType WHERE DeductionAgainstSalary = 'True'"
                            Dim dtType As DataTable = GetDataTable(strType)
                            Dim SalaryExpTypeId As Integer = 0I
                            If dtType IsNot Nothing Then
                                If dtType.Rows.Count > 0 Then
                                    SalaryExpTypeId = Val(dtType.Rows(0).Item(0))
                                Else
                                    SalaryExpTypeId = 0
                                End If
                            End If
                            Dim AdvStr As String = ""
                            Dim AdvDt As DataTable
                            Dim flgAdvance As Boolean = False
                            If Val(grdSalary.GetRows(i).Cells(j).Value.ToString) = Val(SalaryExpTypeId) Then
                                AdvStr = "SELECT DeductionsDetailTable.EmployeeId, ISNULL(tblDefAdvancesType.AccountId,0) AccountId, ISNULL(DeductionsDetailTable.DeductionAmount,0) Amount FROM DeductionsDetailTable INNER JOIN AdvanceRequestTable ON DeductionsDetailTable.RequestID = AdvanceRequestTable.RequestID LEFT OUTER JOIN tblDefAdvancesType ON AdvanceRequestTable.Advance_TypeID = tblDefAdvancesType.Id WHERE (DeductionsDetailTable.EmployeeId = " & Val(grdSalary.GetRows(i).Cells("Employee_Id").Value.ToString) & ") AND DeductionsDetailTable.EntryDate BETWEEN '" & dtpFromDate & "' AND '" & dtpToDate & "'"
                                AdvDt = GetDataTable(AdvStr)
                                If AdvDt IsNot Nothing Then
                                    If AdvDt.Rows.Count > 0 Then
                                        If Val(AdvDt.Rows(0).Item(1)) > Val(0) Then
                                            flgAdvance = True
                                            intAccountId = Val(AdvDt.Rows(0).Item(1))
                                        Else
                                            flgAdvance = False
                                        End If
                                    End If
                                End If
                            End If
                            If grdSalary.GetRow(i).Cells(j + 1).Value.ToString = "False" Then
                                objCommand.CommandText = ""
                                'Marked Against Task# 201506002 to add leave deductions and ot fields Ali Ansari
                                'objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,Earning,deduction) values (ident_current('SalariesExpenseMasterTable')," & grdSalary.GetRows(i).Cells.Item("EmpSalaryAccountId").Value.ToString & "," & grdSalary.GetRows(i).Cells(j).Value.ToString & "," & grdSalary.GetRows(i).Cells(j + 2).Value.ToString & ",0)"
                                'Marked Against Task# 201506002 to add leave deductions and ot fields Ali Ansari
                                'Altered Against Task# 201506002 to add leave deductions and ot fields Ali Ansari
                                objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,Earning,deduction) values (ident_current('SalariesExpenseMasterTable')," & IIf(intAccountId = 0, Val(grdSalary.GetRows(i).Cells(j + 3).Value.ToString), intAccountId) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(grdSalary.GetRows(i).Cells(j + 2).Value.ToString) & ",0)"
                                'Altered Against Task# 201506002 to add leave deductions and ot fields Ali Ansari
                                'objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,Earning,deduction) values (ident_current('SalariesExpenseMasterTable')," & grdSalary.GetRows(i).Cells.Item("EmpSalaryAccountId").Value.ToString & "," & grdSalary.GetRows(i).Cells(j).Value.ToString & "," & grdSalary.GetRows(i).Cells(j + 2).Value.ToString & ",0)"
                                objCommand.ExecuteNonQuery()
                            Else
                                If flgAdvance = True Then
                                    For Each row As DataRow In AdvDt.Rows
                                        objCommand.CommandText = ""
                                        objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,deduction,Earning) values (ident_current('SalariesExpenseMasterTable')," & Val(row.Item(1)) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(row.Item(2)) & ",0)"
                                        objCommand.ExecuteNonQuery()
                                    Next
                                Else
                                    objCommand.CommandText = ""
                                    objCommand.CommandText = "insert into SalariesExpenseDetailTable (SalaryExpId,AccountId,SalaryExpTypeId,deduction,Earning) values (ident_current('SalariesExpenseMasterTable')," & IIf(intAccountId = 0, Val(grdSalary.GetRows(i).Cells(j + 3).Value.ToString), intAccountId) & "," & Val(grdSalary.GetRows(i).Cells(j).Value.ToString) & "," & Val(grdSalary.GetRows(i).Cells(j + 2).Value.ToString) & ",0)"
                                    objCommand.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    Next
                End If
            Next
            'CreateTempTableAndInsertValues(dt, "tempSalarySheet")
            trans.Commit()
            Save = True
            'InsertVoucher()
            If Me.grdSalary.RowCount > 0 Then Me.grdSalary.ClearStructure()
            Me.grdSalary.UpdateData()

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        'SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)

    End Function
    Private Function GetNextSalaryNo(ByVal Prefix As String, ByVal Length As Integer, ByVal tableName As String, ByVal FieldName As String, ByVal trans As OleDb.OleDbTransaction) As String
        Try


            Dim str As String = 0
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            If Prefix = "" Then
                strSql = "select  isnull(max(convert(integer," & tableName & "." & FieldName & ")),0)+1 from " & tableName & " "
            Else
                strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            End If
            Dim dt As New DataTable
            'Dim adp As New OleDbDataAdapter
            'adp = New OleDbDataAdapter(strSql, New OleDbConnection(Con.ConnectionString))
            'adp.Fill(dt)
            dt = GetDataTable(strSql, trans)
            If dt.Rows.Count > 0 Then
                str = dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
            If Prefix = "" Then
                Return str.PadLeft(Length, "0")
            End If

            Return Prefix & "-" & str.PadLeft(Length, "0")
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Sub BtnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click

        If Me.grdSalary.RowCount = 0 Then
            ShowErrorMessage("Record not in grid.")
            Exit Sub
        End If
        Dim objCommand As New OleDbCommand
        Dim ObjCommand2 As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim str As String = String.Empty
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction

        Try

            objCommand.Connection = objCon
            objCommand.Transaction = trans
            objCommand.CommandType = CommandType.Text

            If btnPost.Text = "Post" Then
                If Not msg_Confirm("Do you want to post the salary? Salary would not be edit after post") = True Then Exit Sub



                str = "select SalaryExpId, SalaryExpNo,SalaryExpDate,EmployeeId, CostCenterId,Remarks,GrossSalary,NetSalary, EmployeesView.EmpSalaryAccountId,EmployeesView.Employee_Name, IsNull(ProcessId,0) as ProcessId, IsNull(EmployeesView.SalaryExpAcId,0) as SalaryExpAcId, IsNull(EmployeesView.Employee_ID,0) as Employee_ID From  SalariesExpenseMasterTable INNER JOIN EmployeesView On EmployeesView.Employee_Id = SalariesExpenseMasterTable.EmployeeId WHERE  SalaryExpNo <> '' and ProcessId=" & intProcessId & ""
                Dim dt As New DataTable
                dt = GetDataTable(str, trans)
                dt.AcceptChanges()

                'mToDate = CDate(MyToDate(Me.cmbMonth.SelectedValue, Val(Me.txtYear.Text)))
                str = String.Empty
                'str = "Select " & CDate(MyToDate()) & ""
                str = " Select SalaryMonth, SalaryYear From tblSalaryProcess Where SalaryProcessId=" & intProcessId & ""
                Dim dtProcessSalary As New DataTable
                dtProcessSalary = GetDataTable(str, trans)
                dtProcessSalary.AcceptChanges()
                Dim MonthYear As DateTime
                If dtProcessSalary.Rows.Count > 0 Then
                    MonthYear = CDate(MyToDate(Val(dtProcessSalary.Rows(0).Item(0).ToString), Val(dtProcessSalary.Rows(0).Item(1).ToString)))
                End If
                For i As Integer = 0 To dt.Rows.Count - 1

                    objCommand.CommandText = ""
                    'Below line is commented by Ameen to save month & year value of auto salary generate to vocher date instead date field value on 30-09-2016.  
                    objCommand.CommandText = "Insert into tblVoucher(Location_Id,Voucher_no,Voucher_code,voucher_date,finiancial_year_id, voucher_month,coa_detail_id,post,Source,Reference,UserName,voucher_type_id,SalaryProcessId) Values(0,'" & dt.Rows(i).Item("SalaryExpNo").ToString & "', '" & dt.Rows(i).Item("SalaryExpNo").ToString & "',Convert(dateTime, '" & CDate(dt.Rows(i).Item("SalaryExpDate").ToString).ToString("yyyy-M-d hh:mm:ss tt") & "',102),1, " & CDate(dt.Rows(i).Item("SalaryExpDate").ToString).Month & ", " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ",1,'frmEmployeeSalaryVoucher','Salary " & dt.Rows(i).Item("Employee_Name").ToString & " for the month of " & CDate(MonthYear).ToString("MM-yyyy") & "', '" & LoginUserName & "',9," & Val(dt.Rows(0).Item("ProcessId").ToString) & ") SELECT @@IDENTITY "
                    'objCommand.CommandText = "Insert into tblVoucher(Location_Id,Voucher_no,Voucher_code,voucher_date,finiancial_year_id, voucher_month,coa_detail_id,post,Source,Reference,UserName,voucher_type_id,SalaryProcessId) Values(0,'" & dt.Rows(i).Item("SalaryExpNo").ToString & "', '" & dt.Rows(i).Item("SalaryExpNo").ToString & "',Convert(dateTime, '" & MonthYear.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1, " & CDate(dt.Rows(i).Item("SalaryExpDate").ToString).Month & ", " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ",1,'frmEmployeeSalaryVoucher','Salary " & dt.Rows(i).Item("Employee_Name").ToString & " for the month of " & CDate(dt.Rows(i).Item("SalaryExpDate")).ToString("MM-yyyy") & "', '" & LoginUserName & "',9," & Val(dt.Rows(0).Item("ProcessId").ToString) & ") SELECT @@IDENTITY "

                    Dim Voucher_Id As Integer = objCommand.ExecuteScalar()

                    objCommand.CommandText = ""
                    '' Commented below line by Ameen to save EmployeeId to empId column of voucherdetail instead EmpSalaryAccountId. It is done after discussion with Ahmad sb. On 26-09-2016
                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(Location_id, Voucher_id, coa_detail_id, comments, debit_amount, credit_amount, empId, CostCenterID) Select 0 as Location_Id," & Voucher_Id & " as voucher_id, Case When " & Val(dt.Rows(i).Item("SalaryExpAcId").ToString) & " = 0 Then SalariesExpenseDetailTable.AccountId When IsNull(GrossSalaryType,0) = 1  then " & Val(dt.Rows(i).Item("SalaryExpAcId").ToString) & " else SalariesExpenseDetailTable.AccountId end as coa_detail_id, ExpType.SalaryExpType +' ' + 'for the month of " & CDate(dt.Rows(i).Item("SalaryExpDate").ToString).ToString("MM-yyyy") & "', Case When Earning=0 Then 0 Else Earning End as debit_amount, Case When Deduction=0 then 0 else Deduction end as credit_amount, " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ", " & Val(dt.Rows(i).Item("CostCenterId").ToString) & " From SalariesExpenseDetailTable LEFT OUTER JOIN (Select SalaryExpTypeId, Convert(nvarchar, SalaryExpType) as SalaryExpType, IsNull(GrossSalaryType,0) as GrossSalaryType From SalaryExpenseType) ExpType On ExpType.SalaryExpTypeId = SalariesExpenseDetailTable.SalaryExpTypeId  WHERE (IsNull(SalariesExpenseDetailTable.Earning,0) <> 0 Or IsNull(SalariesExpenseDetailTable.Deduction,0) <> 0) and SalariesExpenseDetailTable.SalaryExpId=" & Val(dt.Rows(i).Item("SalaryExpId").ToString) & " Union All Select 0 as Location_Id, " & Voucher_Id & ", " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ", 'Salary " & dt.Rows(i).Item("Employee_Name").ToString.Replace("'", "''") & " for the month of " & CDate(dt.Rows(i).Item("SalaryExpDate")).ToString("MMM-yyyy") & "',0, " & Val(dt.Rows(i).Item("NetSalary").ToString) & ", " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ", " & Val(dt.Rows(i).Item("CostCenterId").ToString) & ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(Location_id, Voucher_id, coa_detail_id, comments, debit_amount, credit_amount, empId, CostCenterID) Select 0 as Location_Id," & Voucher_Id & " as voucher_id, Case When " & Val(dt.Rows(i).Item("SalaryExpAcId").ToString) & " = 0 Then SalariesExpenseDetailTable.AccountId When IsNull(GrossSalaryType,0) = 1  then " & Val(dt.Rows(i).Item("SalaryExpAcId").ToString) & " else SalariesExpenseDetailTable.AccountId end as coa_detail_id, ExpType.SalaryExpType +' ' + 'for the month of " & CDate(MonthYear).ToString("MM-yyyy") & "', Case When Earning=0 Then 0 Else Earning End as debit_amount, Case When Deduction=0 then 0 else Deduction end as credit_amount, " & Val(dt.Rows(i).Item("EmployeeId").ToString) & ", " & Val(dt.Rows(i).Item("CostCenterId").ToString) & " From SalariesExpenseDetailTable LEFT OUTER JOIN (Select SalaryExpTypeId, Convert(nvarchar, SalaryExpType) as SalaryExpType, IsNull(GrossSalaryType,0) as GrossSalaryType From SalaryExpenseType) ExpType On ExpType.SalaryExpTypeId = SalariesExpenseDetailTable.SalaryExpTypeId  WHERE (IsNull(SalariesExpenseDetailTable.Earning,0) <> 0 Or IsNull(SalariesExpenseDetailTable.Deduction,0) <> 0) and SalariesExpenseDetailTable.SalaryExpId=" & Val(dt.Rows(i).Item("SalaryExpId").ToString) & " Union All Select 0 as Location_Id, " & Voucher_Id & ", " & Val(dt.Rows(i).Item("EmpSalaryAccountId").ToString) & ", 'Salary " & dt.Rows(i).Item("Employee_Name").ToString.Replace("'", "''") & " for the month of " & CDate(MonthYear).ToString("MM-yyyy") & "',0, " & Val(dt.Rows(i).Item("NetSalary").ToString) & ", " & Val(dt.Rows(i).Item("EmployeeId").ToString) & ", " & Val(dt.Rows(i).Item("CostCenterId").ToString) & ""
                    objCommand.ExecuteNonQuery()

                Next

                objCommand.CommandText = ""
                objCommand.CommandText = "update tblSalaryProcess set Posted = 1 where SalaryProcessId=" & intProcessId & ""
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "update SalariesExpenseMasterTable set Post = 1 where ProcessId=" & intProcessId & ""
                objCommand.ExecuteNonQuery()



            Else


                objCommand.CommandText = ""
                objCommand.CommandText = "Delete From tblVoucherDetail where Voucher_Id in(Select Voucher_Id From tblVoucher where SalaryProcessId=" & intProcessId & ")"
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "Delete From tblVoucher where SalaryProcessId=" & intProcessId & ""
                objCommand.ExecuteNonQuery()


                objCommand.CommandText = ""
                objCommand.CommandText = "update tblSalaryProcess set Posted = 0 where SalaryProcessId=" & intProcessId & ""
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "update SalariesExpenseMasterTable set Post = 0 where ProcessId=" & intProcessId & ""
                objCommand.ExecuteNonQuery()

            End If
            trans.Commit()

            'If Me.grdSalary.RowCount > 0 Then Me.grdSalary.ClearStructure()
            'Me.grdSalary.UpdateData()

            RestControls()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
    End Sub
    Private Function IsValidToSave(ByVal ProcessId As Integer) As Boolean
        Try
            Dim strSql As String = "SELECT IsNull(Count(*),0) as Cont from  SalariesExpenseMasterTable WHERE IsNull(Post,0)=1 AND ProcessId = " & ProcessId & " AND EmpDepartmentID=" & Me.cmbDepartment.SelectedValue & ""
            Dim dt As New DataTable
            Dim adp As New OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            If dt.Rows.Count > 0 AndAlso Val(dt.Rows(0).Item(0).ToString) > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetProcessRecords()
        Try
            Dim dt As New DataTable
            'dt = GetDataTable("Select * From tblSalaryProcess ORDER BY SalaryProcessId DESC")
            'dt = GetDataTable("Select tblSalaryProcess.SalaryProcessId,EmployeeDeptDefTable.EmployeeDeptName as Department, tblSalaryProcess.SalaryProcessDate,tblSalaryProcess.SalaryYear, tblSalaryProcess.SalaryMonth, tblSalaryProcess.Posted, IsNull(tblSalaryProcess.AutoAdjustAbsentAttendance,0) as [Auto Adjust Absent], tblSalaryProcess.UserName, IsNull(tblSalaryProcess.EmpDepartmentID,0) as EmpDepartmentID, SalariesExpenseMasterTable.SalaryExpId from tblSalaryProcess LEFT OUTER JOIN  EmployeeDeptDefTable on EmployeeDeptDefTable.EmployeeDeptID = tblSalaryProcess.EmpDepartmentID LEFT JOIN SalariesExpenseMasterTable ON tblSalaryProcess.SalaryProcessId = SalariesExpenseMasterTable.ProcessId ORDER BY tblSalaryProcess.SalaryProcessId DESC")
            dt = GetDataTable("Select tblSalaryProcess.SalaryProcessId,EmployeeDeptDefTable.EmployeeDeptName as Department, tblSalaryProcess.SalaryProcessDate,tblSalaryProcess.SalaryYear, tblSalaryProcess.SalaryMonth, tblSalaryProcess.Posted, IsNull(tblSalaryProcess.AutoAdjustAbsentAttendance,0) as [Auto Adjust Absent], tblSalaryProcess.UserName, IsNull(tblSalaryProcess.EmpDepartmentID,0) as EmpDepartmentID, IsNull(tblSalaryProcess.CostCenterId, 0) As CostCenterId from tblSalaryProcess LEFT OUTER JOIN  EmployeeDeptDefTable on EmployeeDeptDefTable.EmployeeDeptID = tblSalaryProcess.EmpDepartmentID ORDER BY tblSalaryProcess.SalaryProcessId DESC")
            dt.AcceptChanges()
            Me.grdProcess.DataSource = dt
            Me.grdProcess.RetrieveStructure()
            Me.grdProcess.RootTable.Columns(0).Visible = False
            Me.grdProcess.RootTable.Columns("EmpDepartmentID").Visible = False
            Me.grdProcess.RootTable.Columns("CostCenterId").Visible = False
            'Me.grdProcess.RootTable.Columns("SalaryExpId").Visible = False
            Me.grdProcess.RootTable.Columns("SalaryProcessDate").FormatString = "dd/MMM/yyyy"
            'Me.grdProcess.RootTable.Columns.Add("Column1")
            'Me.grdProcess.RootTable.Columns("Column1").UseHeaderSelector = True
            'Me.grdProcess.RootTable.Columns("Column1").ActAsSelector = True
            Me.grdProcess.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub RestControls()
        Try
            Me.dtpSalaryDate.Value = Date.Now
            Me.btnPost.Text = "Post"
            Me.btnSave.Text = "&Save"
            Me.btnGenerate.Enabled = True
            Me.txtYear.Enabled = True
            Me.cmbMonth.Enabled = True
            intProcessId = 0I
            blnPosted = False
            Me.lblValidating.Visible = False
            Me.pgbValidating.Visible = False
            cmbDepartment.Enabled = True
            cmbCostCenter.Enabled = True
            Me.cmbDepartment.SelectedIndex = 0
            Me.cmbCostCenter.SelectedIndex = 0
            GetProcessRecords()
            If Me.grdSalary.RowCount > 0 Then
                Me.grdSalary.DataSource = Nothing
            End If
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdProcess_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProcess.DoubleClick
        Try
            If Me.grdProcess.RowCount = 0 Then Exit Sub
            intProcessId = Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString)
            Me.cmbCostCenter.SelectedValue = Val(Me.grdProcess.GetRow.Cells("CostCenterId").Value.ToString)
            Me.cmbDepartment.SelectedValue = Val(Me.grdProcess.GetRow.Cells("EmpDepartmentID").Value.ToString)
            Me.dtpSalaryDate.Value = Me.grdProcess.GetRow.Cells("SalaryProcessDate").Value.ToString
            Me.txtYear.Text = Val(Me.grdProcess.GetRow.Cells("SalaryYear").Value.ToString)
            Me.cmbMonth.SelectedIndex = Val(Me.grdProcess.GetRow.Cells("SalaryMonth").Value.ToString) - 1
            blnPosted = Me.grdProcess.GetRow.Cells("Posted").Value
            Me.chkAutoAdjustAbsentAttendance.Checked = Me.grdProcess.GetRow.Cells("Auto Adjust Absent").Value
            Me.btnPost.Text = IIf(blnPosted = True, "UnPost", "Post")
            Me.btnSave.Text = "&Update"
            Me.btnGenerate.Enabled = False
            Me.cmbMonth.Enabled = False
            Me.txtYear.Enabled = False
            cmbDepartment.Enabled = False
            cmbCostCenter.Enabled = False
            FillGrid(intProcessId)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.dtpSalaryDate.Focus()
            UpdateIncomeTaxAtSalary()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintSalarySheet()
        Try
            If Me.grdProcess.RowCount = 0 Then Exit Sub
            intProcessId = Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString)
            FillGrid(intProcessId)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            RestControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        If Me.grdProcess.RowCount = 0 Then Exit Sub
        Dim objCommand As New OleDbCommand
        Dim ObjCommand2 As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim str As String = String.Empty
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try


            objCommand.Connection = objCon
            objCommand.Transaction = trans
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From tblVoucherDetail where Voucher_Id in(Select Voucher_Id From tblVoucher where SalaryProcessId=" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & ")"
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From tblVoucher where SalaryProcessId=" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & ""
            objCommand.ExecuteNonQuery()


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From SalariesExpenseDetailTable WHERE SalaryExpId In(Select SalaryExpId From SalariesExpenseMasterTable where ProcessId=" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & ")"
            objCommand.ExecuteNonQuery()


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From SalariesExpenseMasterTable WHERE ProcessId =" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & ""
            objCommand.ExecuteNonQuery()


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From tblSalaryProcess WHERE SalaryProcessId =" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & ""
            objCommand.ExecuteNonQuery()

            trans.Commit()

            msg_Information("Salary has been deleted.")

            RestControls()


        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            If Me.grdProcess.RowCount = 0 Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("Select * From SalariesExpenseMasterTable where ProcessId=" & Val(Me.grdProcess.GetRow.Cells("SalaryProcessId").Value.ToString) & "")
            dt.AcceptChanges()


            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows

                    AddRptParam("@CurrentId", Val(r.Item("SalaryExpId").ToString))
                    ShowReport("rptEmployeeSalary", , , , True)
                Next
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddSalaryType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSalaryType.Click
        Try
            Dim id As Integer = 0I
            Dim frm As New frmSalaryType
            ApplyStyleSheet(frm)
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                'id = Me.cmbType.SelectedIndex
                'Dim Str As String = "Select SalaryExpTypeId, SalaryExpType From SalaryExpenseType WHERE flgAdvance=1"
                'FillDropDown(Me.cmbType, Str)
                'Me.cmbType.SelectedIndex = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValidateSalaryMonth() As Boolean
        Dim _false As Boolean = True
        Try
            Dim dt As New DataTable
            Dim str As String = ""
            'dt = GetDataTable("Select Count(*) From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' AND EmpDepartmentID=" & Me.cmbDepartment.SelectedValue & "")
            ''''' Today commented str = "Select Count(*) From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' AND(EmpDepartmentID=" & Me.cmbDepartment.SelectedValue & " OR CostCenterID =" & Me.cmbCostCenter.SelectedValue & " )"
            'str = "Select Count(*) From tblSalaryProcess WHERE CostCenterID In(Select IsNull(CostCenterID, 0) As CostCenterID From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' And CostCenterID =" & Me.cmbCostCenter.SelectedValue & " And EmpDepartmentID =" & Me.cmbDepartment.SelectedValue & " ) OR EmpDepartmentID In(Select IsNull(EmpDepartmentID, 0) As EmpDepartmentID From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' And CostCenterID =0 And EmpDepartmentID =" & Me.cmbDepartment.SelectedValue & " ) OR CostCenterID In(Select IsNull(CostCenterID, 0) As CostCenterID  From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' And CostCenterID =0 And EmpDepartmentID > 0 And EmpDepartmentID = " & Me.cmbDepartment.SelectedValue & ") OR EmpDepartmentID In(Select  IsNull(EmpDepartmentID, 0) As EmpDepartmentID From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "' And EmpDepartmentID > 0 And EmpDepartmentID = " & Me.cmbDepartment.SelectedValue & ")"
            'str = "Select Count(*) From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "'"
            str = "Select CostCenterId, EmpDepartmentID From tblSalaryProcess WHERE SalaryMonth='" & Val(Me.cmbMonth.SelectedIndex + 1) & "' AND SalaryYear='" & Val(Me.txtYear.Text) & "'"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                For Each Row As DataRow In dt.Rows
                    If Val(Row.Item(0).ToString) = 0 AndAlso Val(Row.Item(1).ToString) = 0 Then
                        Return False
                    ElseIf Val(Row.Item(0).ToString) > 0 AndAlso Val(Row.Item(1).ToString) = 0 Then
                        If Val(Row.Item(0).ToString) = Me.cmbCostCenter.SelectedValue AndAlso Me.cmbDepartment.SelectedValue = 0 Then
                            Return False
                        End If
                    ElseIf Val(Row.Item(0).ToString) = 0 AndAlso Val(Row.Item(1).ToString) > 0 Then
                        If Me.cmbCostCenter.SelectedValue > 0 Or Me.cmbDepartment.SelectedValue = Val(Row.Item(1).ToString) Then
                            Return False
                        End If
                    ElseIf Val(Row.Item(0).ToString) > 0 AndAlso Val(Row.Item(1).ToString) = 0 Then
                        If Me.cmbDepartment.SelectedValue > 0 Or Me.cmbCostCenter.SelectedValue = Val(Row.Item(0).ToString) Then
                            Return False
                        End If
                    ElseIf Val(Row.Item(0).ToString) > 0 AndAlso Val(Row.Item(1).ToString) > 0 Then
                        If Me.cmbDepartment.SelectedValue = Val(Row.Item(1).ToString) Or Me.cmbCostCenter.SelectedValue = Val(Row.Item(0).ToString) Then
                            Return False
                        End If
                    End If
                Next
                Return True
            Else
                Return True
            End If
            'If Me.cmbCostCenter.SelectedValue <= 0 AndAlso Me.cmbDepartment.SelectedValue <= 0 Then
            '    str += " And IsNull(CostCenterId, 0) = 0 And IsNull(EmpDepartmentId, 0) = 0 "
            'End If
            'If Me.cmbCostCenter.SelectedValue > 0 AndAlso Me.cmbDepartment.SelectedValue <= 0 Then
            '    'str += " And CostCenterId = " & Me.cmbCostCenter.SelectedValue & " And IsNull(EmpDepartmentId, 0) = 0 "
            '    str += " And IsNull(CostCenterId, 0) = " & Me.cmbCostCenter.SelectedValue & " And IsNull(EmpDepartmentId, 0) = 0 "
            '    'str += " And CostCenterId = " & Me.cmbCostCenter.SelectedValue & " "
            'End If
            'If Me.cmbCostCenter.SelectedValue <= 0 AndAlso Me.cmbDepartment.SelectedValue > 0 Then
            '    str += "  And IsNull(EmpDepartmentId, 0) = " & Me.cmbDepartment.SelectedValue & " And IsNull(CostCenterId, 0) = 0"
            'End If
            'If Me.cmbCostCenter.SelectedValue > 0 AndAlso Me.cmbDepartment.SelectedValue > 0 Then
            '    str += " And IsNull(CostCenterId, 0) = " & Me.cmbCostCenter.SelectedValue & " And IsNull(EmpDepartmentId, 0) = " & Me.cmbDepartment.SelectedValue & " "
            'End If
            'If Me.cmbCostCenter.SelectedValue <= 0 AndAlso Me.cmbDepartment.SelectedValue <= 0 Then
            '    str += " And IsNull(CostCenterId, 0) = " & Me.cmbCostCenter.SelectedValue & " And IsNull(EmpDepartmentId, 0) = " & Me.cmbDepartment.SelectedValue & " "
            'End If
            'dt = GetDataTable(str)
            'dt.AcceptChanges()
            ' ''TASK : TFS881 managed criteria to prevent the repetition of salary generation considering cost center and department.
            'If dt.Rows.Count > 0 Then
            '    If Val(dt.Rows(0).Item(0).ToString) = 0 AndAlso Val(dt.Rows(0).Item(1).ToString) = 0 Then
            '        Return False
            '    ElseIf Val(dt.Rows(0).Item(0).ToString) > 0 AndAlso Val(dt.Rows(0).Item(1).ToString) = 0 Then
            '        If Val(dt.Rows(0).Item(0).ToString) = Me.cmbCostCenter.SelectedValue Or Me.cmbDepartment.SelectedValue > 0 Then
            '            Return False
            '        Else
            '            If Me.cmbCostCenter.SelectedValue = 0 AndAlso Me.cmbDepartment.SelectedValue = 0 Then
            '                Return False
            '            Else
            '                Return True
            '            End If
            '            Return True
            '        End If
            '    ElseIf Val(dt.Rows(0).Item(0).ToString) > 0 AndAlso Val(dt.Rows(0).Item(1).ToString) > 0 Then
            '        If Val(dt.Rows(0).Item(0).ToString) = Me.cmbCostCenter.SelectedValue AndAlso Val(dt.Rows(0).Item(1).ToString) = Me.cmbDepartment.SelectedValue Then
            '            Return False
            '        Else
            '            Return True
            '        End If
            '    ElseIf Val(dt.Rows(0).Item(0).ToString) = 0 AndAlso Val(dt.Rows(0).Item(1).ToString) > 0 Then
            '        If Me.cmbCostCenter.SelectedValue > 0 Or Val(dt.Rows(0).Item(1).ToString) = Me.cmbDepartment.SelectedValue Then
            '            Return False
            '        Else
            '            If Me.cmbCostCenter.SelectedValue = 0 AndAlso Me.cmbDepartment.SelectedValue = 0 Then
            '                Return False
            '            Else
            '                If Me.cmbCostCenter.SelectedValue = 0 AndAlso Val(dt.Rows(0).Item(1).ToString) = Me.cmbDepartment.SelectedValue Then
            '                    Return False
            '                Else
            '                    Return True
            '                End If
            '                Return True
            '            End If
            '            Return True
            '        End If

            '    Else
            '        Return True
            '    End If
            'Else
            '    Return True
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function itax(ByVal AnnualSalary As Double) As Double
        Try
            Dim dt As New DataTable
            Dim str As String = String.Empty

            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))


            'If (Val(AnnualSalary) * 12) = 401376 Then
            '    Exit Function
            'End If
            str = "SELECT (((" & (Val(AnnualSalary) * 12) & " - IsNull(valuefrom,0)) ) * (IsNull(taxper,0)/100)) / 12  + ((IsNull(fixed,0)) /12) as PerMonthTax FROM tbldeftaxslab WHERE " & (Val(AnnualSalary) * 12) & " BETWEEN valuefrom AND valueto and taxtype = 'Income Tax on Salary' and (Convert(DateTime,Convert(varchar,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102),102) between Convert(DateTime,Convert(varchar,fromdate,102),102) and Convert(DateTime,Convert(varchar,todate,102),102)) "
            'str = "SELECT (((" & Val(AnnualSalary) & " - IsNull(valuefrom,0)) + (IsNull(fixed,0))) * (IsNull(taxper,0)/100)) / 12 as PerMonthTax FROM tbldeftaxslab WHERE " & Val(AnnualSalary) & " BETWEEN valuefrom AND valueto and taxtype = 'Income Tax on Salary' and (Convert(DateTime,Convert(varchar,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102),102) between Convert(DateTime,Convert(varchar,fromdate,102),102) and Convert(DateTime,Convert(varchar,todate,102),102)) and (Convert(DateTime,Convert(varchar,'" & dtpToDate.ToString("yyyy-M-d 00:00:00") & "',102),102) between Convert(DateTime,Convert(varchar,fromdate,102),102) and Convert(DateTime,Convert(varchar,todate,102),102)) "

            dt = GetDataTable(str)
            dt.AcceptChanges()

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Math.Round(Val(dt.Rows(0).Item(0).ToString), 0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnOpenAttendanceSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOpenAttendanceSetup.Click
        Try
            Dim id As Integer = 0I
            Dim frm As New frmHolidySetup
            ApplyStyleSheet(frm)
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSalary_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSalary.CellUpdated
        Me.lblValidating.Visible = True
        Me.pgbValidating.Visible = True
        Application.DoEvents()
        Try
            Me.grdSalary.UpdateData()
            'Dim dt As New DataTable
            Dim strIncomeTaxSalaryType As String = String.Empty
            Dim strIncomeTaxable As String = String.Empty
            'dt = CType(Me.grdSalary.DataSource, DataTable)
            'dt.AcceptChanges()
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            '    Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
            '    Dim flgInEx As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 5).ColumnName, dt.Columns(c + 5).ColumnName.LastIndexOf("#") - 1 + 1))
            '    If flg = False AndAlso flgInEx = False Then
            '        If strIncomeTaxable.Length > 0 Then
            '            strIncomeTaxable = strIncomeTaxable & "+" & "[" & dt.Columns(c + 2).ColumnName & "]"
            '        Else
            '            strIncomeTaxable = "[" & dt.Columns(c + 2).ColumnName & "]"
            '        End If
            '    End If
            'Next

            'dt.Columns("_taxableIncome").Expression = "((" & strIncomeTaxable.ToString & ")*12)"

            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalary.Count To grdSalary.RootTable.Columns.Count - 9 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(Me.grdSalary.RootTable.Columns(c + 4).DataMember, Me.grdSalary.RootTable.Columns(c + 4).DataMember.LastIndexOf("$") - 1 + 1))
                If flg = True Then
                    strIncomeTaxSalaryType = "" & Me.grdSalary.RootTable.Columns(c + 2).DataMember & ""
                    Exit For
                End If
            Next

            'dt.AcceptChanges()
            'For Each r As DataRow In dt.Rows
            '    r.BeginEdit()
            '    r(dt.Columns.IndexOf(strIncomeTaxSalaryType)) = itax(Val(r.Item("_taxableIncome").ToString))
            '    r.EndEdit()
            '    grdSalary.UpdateData()
            '    grdSalary.Refresh()
            'Next

            'For Each r As DataRow In dt.Rows
            '    r.BeginEdit()
            '    r(dt.Columns.IndexOf(strIncomeTaxSalaryType)) = itax(Val(r.Item("_taxableIncome").ToString))
            '    r.EndEdit()
            'Next
            'If e.Column.DataMember = strIncomeTaxSalaryType.ToString Then
            Me.grdSalary.GetRow.BeginEdit()
            If strIncomeTaxSalaryType.ToString.Length > 0 Then
                Me.grdSalary.GetRow.Cells(strIncomeTaxSalaryType.ToString).Value = itax(Val(grdSalary.GetRow.Cells("_IncomeTax").Value.ToString))
            End If
            Me.grdSalary.GetRow.EndEdit()
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblValidating.Visible = False
            Me.pgbValidating.Visible = False
        End Try
    End Sub
    Private Sub BtnTaxSlabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTaxSlabs.Click
        Try
            Dim id As Integer = 0I
            Dim frm As New frmDefTaxSlabs
            ApplyStyleSheet(frm)
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnLoanDeduction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoanDeduction.Click
        Try
            Dim id As Integer = 0I
            Dim frm As New frmEmployeeDeductions
            ApplyStyleSheet(frm)
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnConfiguration_Click(sender As Object, e As EventArgs) Handles btnConfiguration.Click
        Try
            ApplyStyleSheet(frmSalaryConfig)
            frmSalaryConfig.ShowDialog()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            If Convert.ToBoolean(getConfigValueByType("RightBasedCostCenters").ToString) = False Then
                FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " and ISNULL(CostCentre_Id, 0) > 0) " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder") ''TASKTFS75 added and set active =1
            Else
                FillDropDown(Me.cmbCostCenter, " SELECT  CostCenterID,Name,Code , SortOrder FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 ORDER BY 2 ASC ") ''TASKTFS75 added and set active =1
            End If
            FillDropDown(Me.cmbDepartment, "Select Isnull(EmployeeDeptID,0) as EmployeeDeptID,  EmployeeDeptName, IsNull(SalaryExpAcID,0) as SalaryExpAcId From EmployeeDeptDefTable WHERE EmployeeDeptID in(Select Dept_ID From tblDefEmployee where Dept_Id <> 0)")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSalary.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSalary.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdSalary.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Auto Salary Generate"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblDailyAttendance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblDailyAttendance.LinkClicked
        Try
            frmMain.LoadControl("DailyAttendance")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal Print salary sheet against task no 466 on 18-July-2016
    Private Sub btnPrintSheet_Click(sender As Object, e As EventArgs) Handles btnPrintSheet.Click
        Try
            'Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            'Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            'AddRptParam("@FromDate", dtpFromDate)
            'AddRptParam("@ToDate", dtpToDate)
            'AddRptParam("@ProcessId", intProcessId)
            'ShowReport("rptEmployeeSalarySheet")
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
            'Dim Count As Integer = dt.Columns.Count
            'Dim ColumnsList As New List(Of Integer)
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 1
            '    If dt.Columns.Contains(dt.Columns(c).ColumnName) Then
            '        If dt.Columns(c).ColumnName <> "Total Salary" Then
            '            If IsExpenseType(dt.Columns(c).ColumnName) = False Then
            '                'dt.Columns.Remove(dt.Columns(c).ColumnName)
            '                ColumnsList.Add(c)
            '                'dt.Columns.Remove(dt.Columns(c + 1).ColumnName)
            '                'dt.Columns.Remove(dt.Columns(c + 4).ColumnName)
            '                'dt.Columns.Remove(dt.Columns(c + 5).ColumnName)
            '                'dt.Columns.Remove(dt.Columns(c + 6).ColumnName)
            '                'dt.Columns.Remove(dt.Columns(c + 7).ColumnName)
            '                'dt.Columns.Remove(dt.Columns(c + 3).ColumnName)
            '                'dt.Columns(c + 1).Visible = False
            '                'dt.Columns(c + 4).Visible = False
            '                'dt.Columnss(c + 5).Visible = False
            '                'dt.Columns(c + 6).Visible = False
            '                'dt.Columns(c + 7).Visible = False
            '                'Me.grdSalary.RootTable.Columns(c + 2).AllowSort = False
            '                'Me.grdSalary.RootTable.Columns(c + 2).CellStyle.BackColor = Color.WhiteSmoke
            '                'dt.Columns(c + 3).Visible = False
            '                'Me.grdSalary.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '                'Me.grdSalary.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '                'Me.grdSalary.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            '                'Me.grdSalary.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
            '                'Me.grdSalary.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
            '            End If
            '        End If
            '    End If
            'Next
            'For Each i As Integer In ColumnsList
            '    dt.Columns.Remove(dt.Columns(i).ColumnName)
            'Next
            If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                PrintSalarySheet()
            End If
            If intProcessId > 0 Then
                Dim dt As DataTable = CType(Me.grdSalary.DataSource, DataTable)
                If dt IsNot Nothing Then
                    ''TASK TFS3538 Separted the reports to be loaded over configuration. User will decide by configuration which report should be loaded. 
                    Dim NewSalarySheetPrint As Boolean = False
                    If Not getConfigValueByType("NewSalarySheetPrint") = "Error" Then
                        NewSalarySheetPrint = CBool(getConfigValueByType("NewSalarySheetPrint"))
                    End If
                    dt.AcceptChanges()
                    If NewSalarySheetPrint = False Then
                        ShowReport("rptEmployeeSalarySheetNew", , , , , , , dt, , , , , , , ) ''rptEmployeeSalarySheetSP
                    Else
                        CreateTempTableAndInsertValues(dt, "tempSalarySheet")
                        AddRptParam("@ProcessId", intProcessId)
                        ShowReport("rptEmployeeSalarySheetSP") ''rptEmployeeSalarySheetSP
                    End If
                    ''End TASK TFS3538
                End If
            Else
                ShowErrorMessage("Record is not saved.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function MyToDate(ByVal Month As Integer, ByVal Year As Integer) As DateTime
        Try
            Dim myDate As DateTime
            If Month = 2 Then
                If Date.IsLeapYear(Year) Then
                    myDate = CDate(Year & "-" & Month & "-29") 'Feb Last Date
                Else
                    myDate = CDate(Year & "-" & Month & "-28") 'Feb Last Date
                End If
            ElseIf Month = 1 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jan Last Date
            ElseIf Month = 3 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Mar Last Date
            ElseIf Month = 4 Then
                myDate = CDate(Year & "-" & Month & "-30") 'April Last Date
            ElseIf Month = 5 Then
                myDate = CDate(Year & "-" & Month & "-31") 'May Last Date
            ElseIf Month = 6 Then
                myDate = CDate(Year & "-" & Month & "-30") 'June Last Date
            ElseIf Month = 7 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jully Last Date
            ElseIf Month = 8 Then
                myDate = CDate(Year & "-" & Month & "-31") 'August Last Date
            ElseIf Month = 9 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Sep Last Date
            ElseIf Month = 10 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Oct Last Date
            ElseIf Month = 11 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Nov Last Date
            ElseIf Month = 12 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Dec Last Date
            End If
            Return myDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnPrintSelected_Click(sender As Object, e As EventArgs)
        Try
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdProcess.GetCheckedRows
                Me.Cursor = Cursors.WaitCursor
                Try
                    If Me.grdProcess.RowCount = 0 Then Exit Sub
                    AddRptParam("@CurrentId", Val(r.Cells("SalaryExpId").Value.ToString))
                    ShowReport("rptEmployeeSalary", , , , True)
                    ''ShowReport("rptEmployeeSalary")
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.Cursor = Cursors.Default
                End Try
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UpdateIncomeTaxAtSalary()
        Me.lblValidating.Visible = True
        Me.pgbValidating.Visible = True
        Application.DoEvents()
        Try
            Me.grdSalary.UpdateData()
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdSalary.GetRows
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSalary.RootTable.Columns
                    If col.Key.Contains("Incom tax at salary") Then
                        If row.Cells("_IncomeTax").Value > 0 Then
                            row.BeginEdit()
                            row.Cells("Incom tax at salary").Value = itax(Val(row.Cells("_IncomeTax").Value.ToString))
                            row.EndEdit()
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblValidating.Visible = False
            Me.pgbValidating.Visible = False
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnsave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnPost.Enabled = True
                Me.btnPrintSheet.Enabled = True
                Me.btnConfiguration.Enabled = True
                Me.btnAddSalaryType.Enabled = True
                Me.BtnOpenAttendanceSetup.Enabled = True
                Me.BtnTaxSlabs.Enabled = True
                Me.BtnLoanDeduction.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True

                'Me.chkIssued.Checked = True 'TASK:M21 Set Issued Checked 
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnsave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    Me.btnPost.Enabled = False
                    Me.btnPrintSheet.Enabled = False
                    Me.btnConfiguration.Enabled = False
                    Me.btnAddSalaryType.Enabled = False
                    Me.BtnOpenAttendanceSetup.Enabled = False
                    Me.BtnTaxSlabs.Enabled = False
                    Me.BtnLoanDeduction.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnsave.Text = "Save" Or Me.btnsave.Text = "&Save" Then
                            Me.btnsave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnsave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString

                    End If
                End If
                'GetSecurityByPostingUser(UserPostingRights, Me.SaveToolStripButton, Me.DeleteToolStripButton)
            Else
                'Me.Visible = False
                Me.btnsave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                IsCrystalReportExport = False
                IsCrystalReportPrint = False
                Me.btnPost.Enabled = False
                Me.btnPrintSheet.Enabled = False
                Me.btnConfiguration.Enabled = False
                Me.btnAddSalaryType.Enabled = False
                Me.BtnOpenAttendanceSetup.Enabled = False
                Me.BtnTaxSlabs.Enabled = False
                Me.BtnLoanDeduction.Enabled = False
                'TASK:M21 Set Issued Checked 
                'Me.btnSelectedIssuenceUpdate.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnsave.Text = "&Save" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnsave.Text = "&Update" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '    Me.chkPost.Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.btnPost.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print Salary Sheet" Then
                        If Me.btnPrintSheet.Text = "&Print Salary Sheet" Then btnPrintSheet.Enabled = True
                        '' 04-Jul-2014 TASK:2716 Imran Ali Selected Store Issuance Update 
                    ElseIf RightsDt.FormControlName = "Add Salary Type" Then
                        Me.btnAddSalaryType.Enabled = True
                        'End Task:2716
                    ElseIf RightsDt.FormControlName = "Configuration" Then
                        Me.btnConfiguration.Enabled = True
                    ElseIf RightsDt.FormControlName = "Attendance Setup" Then
                        Me.BtnOpenAttendanceSetup.Enabled = True
                    ElseIf RightsDt.FormControlName = "Tax Slabs" Then
                        Me.BtnTaxSlabs.Enabled = True
                    ElseIf RightsDt.FormControlName = "Loan Deduction" Then
                        Me.BtnLoanDeduction.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Private Sub CreateTable(ByVal dt As DataTable, ByVal TableName As String)
    '    Try

    '        Dim strconnection As String = ""
    '        Dim table As String = ""
    '        table += "IF NOT EXISTS (SELECT * FROM " _
    '              & " sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" &
    '        TableName & "]') AND type in (N'U'))"
    '        table += "BEGIN "
    '        table += "create table " + TableName + ""
    '        table += "("
    '        For i As Integer = 0 To dt.Columns.Count

    '                if (i != dt.Columns.Count-1)
    '                    table += dt.Columns[i].ColumnName + " 

    '              " + "varchar(max)" + ",";
    '            Else
    '                    table += dt.Columns[i].ColumnName + " 

    '" + "varchar(max)";
    '            Next
    '            table += ") ";
    '            table += "END";
    '            InsertQuery(table,strconnection);
    '            CopyData(strconnection, dt, tablename);
    '        }
    '        public void InsertQuery(string qry,string 

    'connection)
    '        {


    '            SqlConnection _connection = new 

    'SqlConnection(connection);
    '            SqlCommand cmd = new SqlCommand();
    '            cmd.CommandType = CommandType.Text;
    '            cmd.CommandText = qry;
    '            cmd.Connection = _connection;
    '            _connection.Open();
    '            cmd.ExecuteNonQuery();
    '            _connection.Close();
    '        }
    '        public static void CopyData(string connStr, 

    'DataTable dt, string tablename)
    '        {
    '            using (SqlBulkCopy bulkCopy =
    '            new SqlBulkCopy(connStr, 

    'SqlBulkCopyOptions.TableLock))
    '            {
    '                bulkCopy.DestinationTableName = 

    'tablename;
    '                bulkCopy.WriteToServer(dt);
    '            }
    '        }

    '        Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


    Private Function IsExpenseType(ByVal ColumnName As String) As Boolean
        Dim Query As String = String.Empty
        Try
            Query = "SELECT ISNULL(SalaryExpType, '') AS ExpenseType FROM SalaryExpenseType"
            Dim dt As DataTable = GetDataTable(Query)
            dt.AcceptChanges()
            For Each row As DataRow In dt.Rows
                If row.Item(0).ToString.ToLower = ColumnName.ToLower Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class