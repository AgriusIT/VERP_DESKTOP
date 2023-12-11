'' TFS2644 : Ayesha Rehman : Security Rights missing on Employee Profile 
'' TFS3567 : Ayesha Rehman : User right based Cost Center's employees in dropdown on Employee Profile Configuration based.
Imports System.Drawing.Image
Imports System.Data.OleDb
Imports System.Math
Imports System.IO
Imports SBDal
Imports SBModel
Public Class frmEmployeeProfile
    Dim ProfileDal As New EmployeeProfileDAL
    Dim dtEmpoyeeData As DataTable
    Dim dtProfile As DataTable
    Dim flgCostCenterRights As Boolean = False ''TFS3567
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            'Dim str As String = "Select Employee_ID,  Employee_Name,Employee_Code, EmpPicture, Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id ORDER BY Employee_Name Asc"

            'Dim str As String = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id ORDER BY Employee_Name Asc"
            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            'Altered Against Task#2015060025 to only load Active Employees Ali Ansari

            Dim str As String = String.Empty
            If Condition = String.Empty Then
                '' Commented Agianst TFS3567 : Ayesha Rehman : 19-06-2018
                ''  str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation,CostCenter.Name as CostCenter, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id INNER join dbo.tbldefcostcenter CostCenter on  CostCenter.CostCenterID = tblDefEmployee.CostCentre  where tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                '' TASK TFS4917 all joins have been converted to LEFT JOIN to load all employees whether they have cost center association or not. Done on 15-11-2018
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation,CostCenter.Name as CostCenter, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee LEFT JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id LEFT JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id LEFT JOIN dbo.tbldefcostcenter CostCenter on  CostCenter.CostCenterID = tblDefEmployee.CostCentre  WHERE tblDefEmployee.active = 1 "
                ''Start TFS3566
                If flgCostCenterRights = True Then
                    str += " And tblDefEmployee.CostCentre  in (Select CostCentre_Id  FROM  tblUserCostCentreRights  where UserID = " & LoginUserId & " and (CostCentre_Id is Not Null) )"
                End If
                ''End TFS3566
                str += " ORDER BY Employee_Name Asc"
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                FillUltraDropDown(Me.cmbEmployees, str)
                Me.cmbEmployees.Rows(0).Activate()
                If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
                'ElseIf Condition = "EmployeeByDepartement" Then
                '    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id Where tblDefEmployee.Dept_Id = " & Me.cmbSearchByDepartment.Value & " And tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                '    'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                '    FillUltraDropDown(Me.cmbEmployees, str)
                '    Me.cmbEmployees.Rows(0).Activate()
                '    If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                '    End If
                'ElseIf Condition = "EmployeeByDesignation" Then
                '    str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id Where tblDefEmployee.Desig_Id = " & Me.cmbSearchByDesignation.Value & " And tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                '    'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                '    FillUltraDropDown(Me.cmbEmployees, str)
                '    Me.cmbEmployees.Rows(0).Activate()
                '    If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                '    End If
                'ElseIf Condition = "EmployeeByCostCentre" Then
                '    str = "Select Employee_ID, Employee_Name, Father_Name, Employee_Code, Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id Where tblDefEmployee.CostCentre = " & Me.cmbCostCentre.SelectedValue & " And tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                '    'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                '    FillUltraDropDown(Me.cmbEmployees, str)
                '    Me.cmbEmployees.Rows(0).Activate()
                '    If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                '    End If
                'FillDropDown(Me.cmbAttendanceStatus, "Select Att_Status_ID, Att_Status_Name, Att_Status_Code, Active From tblDefAttendenceStatus WHERE Active=1")
                'ElseIf Condition = "Designation" Then
                '    str = "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable"
                '    FillUltraDropDown(Me.cmbSearchByDesignation, str)
                '    Me.cmbSearchByDesignation.Rows(0).Activate()
                '    If Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbSearchByDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationId").Hidden = True
                '    End If
                'ElseIf Condition = "Department" Then
                '    str = "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable"
                '    FillUltraDropDown(Me.cmbSearchByDepartment, str)
                '    Me.cmbSearchByDepartment.Rows(0).Activate()
                '    If Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbSearchByDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptId").Hidden = True
                '    End If
                'ElseIf Condition = "CostCentre" Then
                '    FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 ORDER BY 2 ASC")
                'ElseIf Condition = "CriteriaWiseEmployee" Then
                '    str = "Select Employee_ID, Employee_Name, Father_Name, Employee_Code, Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id Where tblDefEmployee.active = 1 "
                '    If Me.cmbCostCentre.SelectedValue > 0 Then
                '        str += " And tblDefEmployee.CostCentre = " & Me.cmbCostCentre.SelectedValue & ""
                '    End If
                '    If Me.cmbSearchByDesignation.Value > 0 Then
                '        str += " And tblDefEmployee.Desig_Id = " & Me.cmbSearchByDesignation.Value & ""
                '    End If
                '    If Me.cmbSearchByDepartment.Value > 0 Then
                '        str += " And tblDefEmployee.Dept_Id = " & Me.cmbSearchByDepartment.Value & " "
                '    End If
                '    str += " ORDER BY Employee_Name Asc"
                '    'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                '    FillUltraDropDown(Me.cmbEmployees, str)
                '    Me.cmbEmployees.Rows(0).Activate()
                '    If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                '        Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                '    End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Public Sub FillGrid(ByVal ProcessId As Integer)
    '    Try
    '        Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
    '        Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
    '        Dim DtpLeavePolicyDate As DateTime = getConfigValueByType("Attendance_Period").ToString
    '        Dim strFilter As String = String.Empty
    '        'Dim sp As String = "Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id, Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department, IsNull(EmployeesView.Salary,0) as [Basic Salary],isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId, IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId From EmployeesView LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable where ProcessId=" & ProcessId & ") SalaryExp on SalaryExp.EmployeeId = EmployeesView.Employee_Id"
    '        'sp += " WHERE Employee_Name <> '' AND IsNull(Active,0) = 1"
    '        'sp += " ORDER BY 2 Asc"
    '        '"SP_EmployeeSalarySheet '" & dtpFrom.Value & "','" & dtpTo.Value & "'"
    '        'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId,City_Id,Employee_Id,Employee_Code, 0 as EmployeeType,0 as LeavesAlloted,Employee_Name,  Designation,Department,[Basic Salary],EmpSalaryAccountId,ProcessId,SalaryExpId,   WorkingDays,TotalLeaves,PresentDays,LeaveDays,isnull(PrvLeave,0) as  PrvLeave " _
    '        '            & " , (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance,  case when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) > " _
    '        '            & "  0 then  0  when PrvLeave  > convert(money,totalleaves,5)  then - -convert(int,(([Basic Salary]/convert(int,WorkingDays))) * leaveDays) when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) < 0 then " _
    '        '             & " -convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  (convert(money,TotalLeaves,5) - (LeaveDays + PrvLeave))),0) end as LeavDeduction ,isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax, Convert(decimal,0) as _taxableIncome from (Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department,IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId,  IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId  ,(select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays,   " _
    '        '            & " (select config_value from  ConfigValuesTable where config_type = 'Leave_Days') as TotalLeaves, sum(isnull(PresentDays,0)) as PresentDays,sum(isnull(Tleaves,0)) as LeaveDays  ,PrevBalance.PreviousLeave as PrvLeave ,sum(isnull(Ot_Amount,0)) as OT_AMount, Convert(decimal,0) as _IncomeTax from EmployeesView " _
    '        '            & "LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable   where ProcessId=" & intProcessId & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id  left outer join (select empid,sum(tleaves)  as PreviousLeave from   Vw_EmpAttendance   where convert(datetime,attendancedate) between '" & DtpLeavePolicyDate & "' and '" & DateAdd(DateInterval.Day, -1, dtpFromDate) & "'  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid left outer join (select empid,sum(PresentDays) as PresentDays,sum(LeavesDays)  as TLEaves from   Vw_EmpAttendance   where convert(datetime,attendancedate) between '" & dtpFromDate & "' and '" & dtpToDate & "'  group by empid )  CurrentLEaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
    '        '            & "left join  (select employeeid,sum(OTAmount)  AS Ot_Amount from (select employeeid,datediff(dd,start_date,end_date)  * datediff(hh,start_time,End_time) * overtime_rate_hr as OTAmount   from tblEmployeeOverTimeSchedule where start_date between '" & dtpFromDate & "' and '" & dtpToDate & "' ) as a group by employeeid ) as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  " _
    '        '            & " WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1  group by Dept_Id,Desig_Id,ShiftGroupId,Employee_Code,Employee_Name,EmployeeDesignationName, Salary,EmpSalaryAccountId,ProcessId,City_ID,Employee_ID,EmployeeDeptName,SalaryExpId,PreviousLeave  ) as a"

    '        'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId,City_Id,Employee_Id,Employee_Code,Employee_Name,  Designation,Department,[Basic Salary],EmpSalaryAccountId,ProcessId,SalaryExpId, WorkingDays ,TotalLeaves,isnull(PrvLeave,0) as  PrvLeave,LeaveDays,(convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance,PresentDays " _
    '        '            & " , case when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) > " _
    '        '            & "  0 then  0  when IsNull(PrvLeave,0)  > convert(money,totalleaves,5)  then convert(int,(([Basic Salary]/convert(int,WorkingDays))) * IsNull(leaveDays,0)) when (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) < 0 then " _
    '        '             & "  -convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  IsNull(AbsentDays,0))),0) end as LeavDeduction ,isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax, Convert(decimal,0) as _taxableIncome from (Select IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id, IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id, IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department,IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId,IsNull(SalaryExp.ProcessId,0) as ProcessId,  IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId  ,(select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays,   " _
    '        '            & " isnull(leavesalloted,0) as TotalLeaves, sum(isnull(PresentDays,0)) as PresentDays,sum(isnull(Tleaves,0)) as LeaveDays  ,PrevBalance.PreviousLeave as PrvLeave ,sum(isnull(Ot_Amount,0)) as OT_AMount, Convert(decimal,0) as _IncomeTax from EmployeesView " _
    '        '            & "LEFT OUTER JOIN(Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable   where ProcessId=" & intProcessId & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id  left outer join (select EmpId,sum(LeavesDays)  as PreviousLeave from Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & DateAdd(DateInterval.Day, -1, dtpFromDate).ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid left outer join (select empid,sum(IsNull(PresentDays,0)) as PresentDays,sum(IsNull(LeavesDays,0))  as TLeaves, Sum(IsNull(AbsentDays,0)) as AbsentDays from   Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(datetime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
    '        '            & "left join  (select employeeid,sum(OTAmount)  AS Ot_Amount from (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(hh,start_time,End_time) * IsNull(overtime_rate_hr,0))) as OTAmount   from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) ) as a group by employeeid ) as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  " _
    '        '            & " WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1  group by Dept_Id,Desig_Id,ShiftGroupId,Employee_Code,Employee_Name,EmployeeDesignationName, Salary,EmpSalaryAccountId,ProcessId,City_ID,Employee_ID,EmployeeDeptName,SalaryExpId,PreviousLeave,leavesalloted  ) as a"


    '        'Dim sp = "select Dept_Id,Desig_Id,ShiftGroupId," _
    '        '& " City_Id,IsNull(SalaryExpAcId,0) as SalaryExpAcId,Employee_Id,Employee_Code,Employee_Name," _
    '        '& " Designation,Department,[Basic Salary],EmpSalaryAccountId," _
    '        '& " ProcessId,SalaryExpId, WorkingDays ,TotalLeaves," _
    '        '& " isnull(PrvLeave,0) as  PrvLeave,LeaveDays," _
    '        '& " (convert(money,isnull(TotalLeaves,0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance," _
    '        '& " PresentDays, IsNull(AbsentDays,0) as AbsentDays, " _
    '        '& " Convert(int,(([Basic Salary]/convert(int,WorkingDays)) *  IsNull(AbsentDays,0)))  as LeavDeduction, isnull(Round(Convert(float, OverTimeHrs), 2), 0) As OverTimeHrs," _
    '        '& " isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax," _
    '        '& " Convert(decimal,0) as _taxableIncome  " _
    '        '& " from (Select IsNull(SalaryExpAcId,0) as SalaryExpAcId, IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id," _
    '        '& " IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id," _
    '        '& " IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name," _
    '        '& " EmployeeDesignationName as Designation, EmployeeDeptName as Department," _
    '        '& " IsNull(EmployeesView.Salary,0) as [Basic Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId," _
    '        '& " IsNull(SalaryExp.ProcessId,0) as ProcessId,  " _
    '        '& " IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId, (" _
    '        '& " select config_value from  ConfigValuesTable where config_type = 'Working_Days') as WorkingDays," _
    '        '& " (isnull(leavesalloted,0)) as TotalLeaves, (isnull(PresentDays,0)) as PresentDays,(isnull(Tleaves,0)) as LeaveDays," _
    '        '& " (PrevBalance.PreviousLeave) as PrvLeave ," _
    '        '& " isnull(Convert(float, OverTimeHrs), 0) As OverTimeHrs," _
    '        '& " (isnull(Ot_Amount,0)) as OT_AMount," _
    '        '& " (IsNull(AbsentDays,0)) as AbsentDays," _
    '        '& " Convert(decimal,0) as _IncomeTax from EmployeesView " _
    '        '& " LEFT OUTER JOIN(" _
    '        '& " Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable " _
    '        '& " where ProcessId=" & ProcessId & " AND IsNull(EmpDepartmentID,0)=" & Me.cmbDepartment.SelectedValue & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
    '        '& " left outer join (select EmpId,sum(LeavesDays) as PreviousLeave from Vw_EmpAttendance where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & DateAdd(DateInterval.Day, -1, dtpFromDate).ToString("yyyy-M-d 23:59:59") & "',102))  group by empid )  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
    '        '& " left outer join (select empid,sum(IsNull(PresentDays,0)) as PresentDays,sum(IsNull(LeavesDays,0))  as TLeaves, Sum(IsNull(AbsentDays,0)) as AbsentDays from   Vw_EmpAttendance   where (convert(varchar,attendancedate,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))  group by empid ) " _
    '        '& " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
    '        '& " left  join  (" _
    '        '& " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
    '        '& " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi,start_time,End_time)* 1/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
    '        '& " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid   WHERE Employee_Name <> ''  AND IsNull(Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
    '        '& " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
    '        ''SUBSTRING(CONVERT(VARCHAR(20),(job_end - job_start),120),12,8) from tableA
    '        ''datediff(mi,start_time,End_time)
    '        ' SELECT CONVERT(NUMERIC(18, 2), 398 / 60 + (398 % 60) / 100.0)

    '        Dim strWorkingDays As String = DateTime.DaysInMonth(Me.txtYear.Text, (Me.cmbMonth.SelectedIndex + 1))
    '        Dim sp As String = "select Dept_Id,Desig_Id,ShiftGroupId," _
    '        & " City_Id,IsNull(SalaryExpAcId,0) as SalaryExpAcId,Employee_Id,Employee_Code,Employee_Name," _
    '        & " Designation,Department,[Salary],EmpSalaryAccountId, CostCentre, CostCenterName As [Cost Centre Name], " _
    '        & " ProcessId,SalaryExpId, MonthDays, WorkingDays,TotalLeaves," _
    '        & " isnull(PrvLeave,0) as  PrvLeave,LeaveDays," _
    '        & " (convert(money,isnull(Convert(float,TotalLeaves),0),5) - (isnull(LeaveDays,0) + isnull(PrvLeave,0))) as LeaveBalance," _
    '        & " PresentDays, IsNull(AbsentDays,0) as AbsentDays, " _
    '        & " Convert(int,(([Salary]/ Case When convert(int, MonthDays) = 0 Then 1 Else convert(int, MonthDays) End) *  IsNull(AbsentDays,0)))  as LeavDeduction, isnull(OverTimeHrs,0) as OverTimeHrs," _
    '        & " isnull(OT_AMount,0) as OT_Amount, IsNull(_IncomeTax,0) as _IncomeTax," _
    '        & " Convert(decimal,0) as _taxableIncome,VisitAllowance  " _
    '        & " from (Select IsNull(SalaryExpAcId,0) as SalaryExpAcId, IsNull(Dept_Id,0) as Dept_Id, IsNull(Desig_Id,0) as Desig_Id," _
    '        & " IsNull(ShiftGroupId,0) as ShiftGroupId, IsNull(City_ID,0) as City_Id," _
    '        & " IsNull(Employee_Id,0) as Employee_Id,   Employee_Code, Employee_Name," _
    '        & " EmployeeDesignationName as Designation, EmployeeDeptName as Department," _
    '        & " IsNull(EmployeesView.Salary,0) as [Salary], isnull(EmpSalaryAccountId,0) as EmpSalaryAccountId," _
    '        & " IsNull(EmployeesView.CostCentre, 0) As CostCentre, tblDefCostCenter.Name AS CostCenterName, IsNull(SalaryExp.ProcessId,0) as ProcessId,  " _
    '        & " IsNull(SalaryExp.SalaryExpId,0) as SalaryExpId, " _
    '        & " " & Convert.ToInt32(Val(strWorkingDays)) & " as MonthDays," _
    '        & " (ISNULL(PresentDays, 0)) + (ISNULL(Tleaves, 0)) As WorkingDays, " _
    '        & " (isnull(leavesalloted,0)) as TotalLeaves, (isnull(PresentDays,0)) as PresentDays,(isnull(Tleaves,0)) as LeaveDays," _
    '        & " (PrevBalance.PreviousLeave) as PrvLeave ," _
    '        & " isnull(Convert(float, OverTimeHrs), 0) As OverTimeHrs," _
    '        & " (isnull(Convert(float,Ot_Amount),0)) as OT_AMount," _
    '        & " (IsNull(Convert(float,AbsentDays),0)) as AbsentDays," _
    '        & " Convert(decimal,0) as _IncomeTax,ISNULL(VisitAllowance.TotalAmount, 0) AS VisitAllowance from EmployeesView LEFT JOIN tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID" _
    '        & " LEFT OUTER JOIN(" _
    '        & " Select EmployeeId, ProcessId, SalaryExpId From SalariesExpenseMasterTable " _
    '        & " where ProcessId=" & ProcessId & " AND IsNull(EmpDepartmentID,0)=" & Me.cmbDepartment.SelectedValue & ") SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
    '        & " left outer join (select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
    '        & " left outer join (select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "')) " _
    '        & " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
    '        & " left  join  (" _
    '        & " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
    '        & " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
    '        & " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & dtpFromDate.ToString & "',102) And Convert(DateTime,'" & dtpToDate.ToString & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> ''  AND IsNull(EmployeesView.Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
    '        & " ) as a " & IIf(Me.btnSave.Text <> "&Save", " WHERE ProcessId <> 0", "") & " "
    '        Dim dt As DataTable = GetDataTable(sp)
    '        'dt.Columns("Paid_Salary").Expression = "MonthlySalary-Allowance-Insurance-Gratuity_Fund-Advance-WHTax-ESSI-EOBI"
    '        dt.TableName = "SalarySheet"
    '        'DeductionAgainstLeaves
    '        'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
    '        'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
    '        'TASK239151 Added Field ApplyValue And ExistingAccount
    '        Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(ApplyValue,'Fixed') as ApplyValue, IsNull(DeductionAgainstSalary, 0) As DeductionAgainstSalary,Isnull(SiteVisitAllowance,0) AS SiteVisitAllowance From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> ''"
    '        'END TASK239151
    '        Dim dtSalaryType As New DataTable
    '        dtSalaryType = GetDataTable(strSalaryType)
    '        dtSalaryType.AcceptChanges()
    '        For Each row As DataRow In dtSalaryType.Rows
    '            If Not dtSalaryType.Columns.Contains(row(1)) Then
    '                dt.Columns.Add(row(0), GetType(System.Int16), row(0))
    '                dt.Columns.Add(row(1) & "-" & row(0), GetType(System.String), row(1))
    '                If row.Item("DeductionAgainstLeaves") = "True" Then
    '                    dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("LeavDeduction").ColumnName)
    '                ElseIf row.Item("AllowanceOverTime") = "True" Then
    '                    dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("OT_Amount").ColumnName)
    '                ElseIf row.Item("SiteVisitAllowance") = "True" Then
    '                    dt.Columns.Add(row(2), GetType(System.Double), dt.Columns("VisitAllowance").ColumnName)
    '                Else
    '                    dt.Columns.Add(row(2), GetType(System.Double))
    '                End If
    '                dt.Columns.Add(row(3) & "^" & row(0), GetType(System.Double), row(3))
    '                dt.Columns.Add(row(enmSalaryType.DeductionAgainsIncomeTax) & "$" & row(0), GetType(System.Boolean), row(enmSalaryType.DeductionAgainsIncomeTax))
    '                dt.Columns.Add(row(enmSalaryType.IncomeTaxExempted) & "#" & row(0), GetType(System.Boolean), row(enmSalaryType.IncomeTaxExempted))
    '                dt.Columns.Add(row(enmSalaryType.GrossSalaryType) & "*" & row(0), GetType(System.Boolean), row(enmSalaryType.GrossSalaryType))
    '                dt.Columns.Add(CStr(row(enmSalaryType.ApplyValue) & "~" & row(0)), GetType(System.String))

    '            End If
    '        Next
    '        For Each row As DataRow In dt.Rows
    '            'For c As Integer = enmSalary.Count To dt.Columns.Count - 4 Step 4
    '            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '            For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '                If Not row.Table.Columns(c + 2).ReadOnly Then
    '                    row.BeginEdit()
    '                    row(c + 2) = 0
    '                    row.EndEdit()
    '                End If
    '            Next
    '        Next
    '        ' If dt.Columns.Contains("") Then
    '        Dim strTotal As String = String.Empty
    '        Dim strTotalExempt As String = String.Empty
    '        Dim strIncomeTaxSalaryType As String = String.Empty
    '        Dim strGrossSalaryType As String = String.Empty
    '        Dim strIncomeTaxable As String = String.Empty
    '        Dim strBasicSalaryColName As String = String.Empty
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            'For c As Integer = enmSalary.Count To dt.Columns.Count - 9 Step 9
    '            Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 6).ColumnName, dt.Columns(c + 6).ColumnName.LastIndexOf("*") - 1 + 1))
    '            If flg = True Then
    '                strBasicSalaryColName = dt.Columns(c + 2).ColumnName
    '                strGrossSalaryType = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                Exit For
    '            End If
    '        Next
    '        Dim dtchk As New DataTable
    '        Dim str As String = String.Empty
    '        dtchk = GetDataTable("Select Count(*) From dbo.SalariesExpenseMasterTable WHERE ProcessId=" & ProcessId & "")
    '        dtchk.AcceptChanges()
    '        'Dim strSQL1 As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) as Amount, 'Edit' as Record_Mode " _
    '        '                          & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
    '        '                          & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId INNER JOIN SalaryExpenseType On SalariesExpenseDetailTable.SalaryExpTypeId = SalaryExpenseType.SalaryExpTypeId WHERE dbo.SalariesExpenseMasterTable.ProcessId=" & ProcessId & "  and dbo.SalariesExpenseDetailTable.SalaryExpTypeId <>  0 Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, IsNull(SalaryExpenseType.GrossSalaryType,0) Having abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) <> 0 ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"

    '        Dim strSQL1 As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, Case WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) Else Abs(Sum(IsNull(EmpDeductions.DeductionAmount,0))) End  Else  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) end as Amount, 'Edit' as Record_Mode " _
    '                                  & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
    '                                 & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId INNER JOIN SalaryExpenseType On SalariesExpenseDetailTable.SalaryExpTypeId = SalaryExpenseType.SalaryExpTypeId LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = dbo.SalariesExpenseMasterTable.EmployeeId WHERE dbo.SalariesExpenseMasterTable.ProcessId=" & ProcessId & "  and dbo.SalariesExpenseDetailTable.SalaryExpTypeId <>  0 Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, IsNull(SalaryExpenseType.GrossSalaryType,0), SalaryExpenseType.DeductionAgainstSalary, EmpDeductions.DeductionAmount ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"

    '        Dim strSQL2 As String = "Select DISTINCT IsNull(tblEmployeeAccounts.Employee_Id,0) as EmployeeId, IsNull(Type_Id,0) as SalaryExpTypeId, Case When IsNull(SalaryExpenseType.GrossSalaryType,0)=1 Then Case When IsNull(Amount,0)=0 Then IsNull(Emp.Salary,0) Else IsNull(Amount,0) End  WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then IsNull(Amount,0) Else IsNull(EmpDeductions.DeductionAmount,0) End  Else IsNull(Amount,0) end as Amount, 'New' as Record_Mode, IsNull(SalaryExpenseType.GrossSalaryType,0) as GrossSalaryType  From tblEmployeeAccounts LEFT OUTER JOIN EmployeesView Emp On Emp.Employee_Id = tblEmployeeAccounts.Employee_Id INNER JOIN SalaryExpenseType on SalaryExpenseType.SalaryExpTypeId = Type_Id LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = Emp.Employee_Id  WHERE Emp.Employee_Id <> 0 " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND Emp.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"
    '        If Not dtchk.Rows.Count <= 0 Then
    '            If Val(dtchk.Rows(0).Item(0).ToString) > 0 Then
    '                str = String.Empty
    '                str = strSQL1
    '            Else
    '                str = String.Empty
    '                str = strSQL2
    '            End If
    '        Else
    '            str = String.Empty
    '            str = strSQL2
    '        End If
    '        Dim dt_Data As New DataTable
    '        dt_Data = GetDataTable(str)
    '        Dim dr() As DataRow
    '        For Each r As DataRow In dt.Rows
    '            dr = dt_Data.Select("EmployeeId='" & r("Employee_Id") & "'")
    '            If dr IsNot Nothing Then
    '                If dr.Length > 0 Then
    '                    For Each drFound As DataRow In dr
    '                        If (dt.Columns.IndexOf(drFound(1)) + 2) >= enmSalary.Count Then
    '                            If Not r.Table.Columns(dt.Columns.IndexOf(drFound(1)) + 2).ReadOnly Then
    '                                r.BeginEdit()
    '                                If drFound(3).ToString = "New" Then
    '                                    Dim strApplyValue As String = dt.Columns(dt.Columns.IndexOf(drFound(1)) + 7).ToString.Substring(0, dt.Columns(dt.Columns.IndexOf(drFound(1)) + 7).ToString.LastIndexOf("~"))
    '                                    If strApplyValue.Length > 0 Then
    '                                        If strApplyValue = "Fixed" Then
    '                                            r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
    '                                        Else
    '                                            Dim dblPercent As Double = (Val(drFound(2).ToString) / 100)
    '                                            Dim dblAmount As Double = r(dt.Columns.IndexOf("Salary"))
    '                                            r(dt.Columns.IndexOf(drFound(1)) + 2) = dblAmount * dblPercent
    '                                        End If
    '                                    End If
    '                                Else
    '                                    r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
    '                                End If
    '                                r.EndEdit()
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next
    '        dt.AcceptChanges()
    '        '''''''''''''''''
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            If strTotal.Length > 0 Then
    '                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
    '                strTotal = strTotal & IIf(flg = False, "+", "-") & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '            Else
    '                strTotal = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '            End If
    '        Next
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 4).ColumnName, dt.Columns(c + 4).ColumnName.LastIndexOf("$") - 1 + 1))
    '            If flg = True Then
    '                strIncomeTaxSalaryType = "" & dt.Columns(c + 2).ColumnName & ""
    '                Exit For
    '            End If
    '        Next
    '        Dim strTotalEarning As String = String.Empty
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
    '            If flg = False Then
    '                If strTotalEarning.Length > 0 Then
    '                    strTotalEarning = strTotalEarning & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                Else
    '                    strTotalEarning = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                End If
    '            End If
    '        Next
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 5).ColumnName, dt.Columns(c + 5).ColumnName.LastIndexOf("#") - 1 + 1))
    '            If flg = True Then
    '                If strTotalExempt.Length > 0 Then
    '                    strTotalExempt = strTotalExempt & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                Else
    '                    strTotalExempt = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                End If
    '            End If
    '        Next
    '        'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
    '        For c As Integer = enmSalary.Count To dt.Columns.Count - 8 Step 8
    '            Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
    '            Dim flgInEx As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 5).ColumnName, dt.Columns(c + 5).ColumnName.LastIndexOf("#") - 1 + 1))
    '            If flg = False AndAlso flgInEx = False Then
    '                If strIncomeTaxable.Length > 0 Then
    '                    strIncomeTaxable = strIncomeTaxable & "+" & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                Else
    '                    strIncomeTaxable = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
    '                End If
    '            End If
    '        Next
    '        'Dim IncomeTax As String = Val(strGrossSalaryType) - Val(strTotalExempt)
    '        If strTotalExempt.Length > 0 Then
    '            dt.Columns("_IncomeTax").Expression = "(((" & strTotalEarning & "-IsNull([LeavDeduction],0)" & ")-(" & strTotalExempt.ToString & ")))"
    '        Else
    '            dt.Columns("_IncomeTax").Expression = strTotalEarning
    '        End If
    '        dt.AcceptChanges()
    '        'Me.lblValidating.Visible = True
    '        'Me.pgbValidating.Visible = True
    '        Application.DoEvents()
    '        If Me.btnSave.Text <> "&Update" Then
    '            'dt.Columns("_taxableIncome").Expression = strIncomeTaxable.ToString
    '            For Each r As DataRow In dt.Rows
    '                r.BeginEdit()
    '                If Val(itax(Val(r.Item("_IncomeTax").ToString))) >= 85.91333333 Then
    '                    'Exit Sub
    '                End If
    '                If strIncomeTaxSalaryType.Length > 0 Then
    '                    r(dt.Columns.IndexOf(strIncomeTaxSalaryType)) = itax(Val(r.Item("_IncomeTax").ToString))
    '                End If
    '                r.EndEdit()
    '            Next
    '        End If
    '        dt.Columns.Add("Total Salary", GetType(System.Double))
    '        dt.Columns("Total Salary").Expression = "(" & strTotal.ToString & ")"
    '        dt.AcceptChanges()
    '        'Me.grdSalary.DataSource = dt
    '        'Me.grdSalary.RetrieveStructure()
    '        ''Me.grdSalary.

    '        ''itax()
    '        'ApplyGridSetting()
    '        'Me.grdSalary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
    '        ''22-02-2017
    '        For Each Dr1 As DataRow In dtSalaryType.Rows
    '            If Dr1.Item("DeductionAgainstLeaves") = "True" Or Dr1.Item("AllowanceOverTime") = "True" Or Dr1.Item("DeductionAgainsIncomeTax") = "True" Or Dr1.Item("GrossSalaryType") = "True" Or Dr1.Item("DeductionAgainstSalary") = "True" Or Dr1.Item("SiteVisitAllowance") = "True" Then ''DeductionAgainstSalary
    '                If Me.grdSalary.RootTable.Columns.Contains(Dr1.Item("SalaryExpType").ToString) Then
    '                    Me.grdSalary.RootTable.Columns(Dr1.Item("SalaryExpType").ToString).EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                End If
    '                'For Each Column As Janus.Windows.GridEX.GridEXColumn In Me.grdSalary.RootTable.Columns

    '                'Next
    '            End If
    '        Next
    '        'For Each Dr1 As DataRow In dtSalaryType.Rows
    '        '    If Dr1.Item("DeductionAgainstLeaves") = "False" Or Dr1.Item("AllowanceOverTime") = "False" Or Dr1.Item("DeductionAgainsIncomeTax") = "False" Or Dr1.Item("GrossSalaryType") = "False" Or Dr1.Item("DeductionAgainstSalary") = "False" Or Dr1.Item("SiteVisitAllowance") = "False" Then ''DeductionAgainstSalary
    '        '        If Me.grdSalary.RootTable.Columns.Contains(Dr1.Item("SalaryExpType").ToString) Then
    '        '            Me.grdSalary.RootTable.Columns(Dr1.Item("SalaryExpType").ToString).EditType = Janus.Windows.GridEX.EditType.TextBox
    '        '        End If
    '        '        'For Each Column As Janus.Windows.GridEX.GridEXColumn In Me.grdSalary.RootTable.Columns

    '        '        'Next
    '        '    End If
    '        'Next
    '        'Me.grdSalary.FilterRowUpdateMode =
    '        'Me.grdSalary.RetrieveStructure()
    '        ''End 22-02-2017
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        'Me.lblValidating.Visible = False
    '        'Me.pgbValidating.Visible = False
    '    End Try
    'End Sub

    Private Sub frmEmployeeProfile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
            FillCombos("")
            FillCombos("Department")
            FillCombos("Designation")
            FillCombos("CostCentre")
            FillMonth()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSearchByDepartment_ValueChanged(sender As Object, e As EventArgs)
        'Try
        '    If Not cmbSearchByDepartment.ActiveRow Is Nothing Then
        '        FillCombos("CriteriaWiseEmployee")
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbSearchByDesignation_ValueChanged(sender As Object, e As EventArgs)
        'Try
        '    If Not cmbSearchByDesignation.ActiveRow Is Nothing Then
        '        FillCombos("CriteriaWiseEmployee")
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbCostCentre_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Try
        '    If Not cmbCostCentre.SelectedIndex = -1 Then
        '        FillCombos("CriteriaWiseEmployee")
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ID As Integer = 0
        Try
            ResetControls()
            'ID = Me.cmbSearchByDepartment.Value
            'FillCombos("Department")
            ''Me.cmbSearchByDepartment.Value = ID
            'ID = Me.cmbSearchByDesignation.Value
            'FillCombos("Designation")
            'Me.cmbSearchByDesignation.Value = ID
            'ID = Me.cmbCostCentre.SelectedValue
            'FillCombos("CostCentre")
            'Me.cmbCostCentre.SelectedValue = ID
            ID = Me.cmbEmployees.Value
            FillCombos("")
            Me.cmbEmployees.Value = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TFS2644 : Ayesha Rehman : Added Security Rights to the form
    ''' </summary>
    ''' <remarks>TFS2644 : Ayesha Rehman </remarks>
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnDisplay.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnDisplay.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnDisplay.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Display" Then
                        Me.btnDisplay.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''End TFS2644
    Private Sub FillProfile(ByVal EmployeeId As Integer)
        Try
            dtProfile = ProfileDal.GetProfile(EmployeeId)
            If dtProfile.Rows.Count > 0 Then
                If Not IsDBNull(dtProfile.Rows(0).Item("EmpPicture")) Then
                    If IO.File.Exists(dtProfile.Rows(0).Item("EmpPicture").ToString) Then
                        Try
                            Me.pbemployee.ImageLocation = dtProfile.Rows(0).Item("EmpPicture").ToString
                            pbemployee.Update()
                        Catch ex As Exception
                        End Try
                    Else
                        Me.pbemployee.Image = Nothing
                    End If
                Else
                    Me.pbemployee.Image = Nothing
                End If
                Me.lblNameValue.Text = dtProfile.Rows(0).Item("Employee_Name").ToString
                Me.lblFatherNameValue.Text = dtProfile.Rows(0).Item("Father_Name").ToString
                Me.lblDepartmentValue.Text = dtProfile.Rows(0).Item("EmployeeDeptName").ToString
                Me.lblDesignationValue.Text = dtProfile.Rows(0).Item("EmployeeDesignationName").ToString
                Me.lblGenderValue.Text = dtProfile.Rows(0).Item("Gender").ToString

                'Dim _Date As DateTime = Convert.ToDateTime(dtProfile.Rows(0).Item("DOB").ToString).ToString("dd/MMM/yyyy")
                'Me.lblDOBValue.Text = _Date
                'Dim JoiningDate As DateTime = Convert.ToDateTime(dtProfile.Rows(0).Item("Joining_Date").ToString).ToString("dd/MMM/yyyy")
                'Me.lblJoiningDateValue.Text = JoiningDate
                Me.lblDOBValue.Text = Convert.ToDateTime(dtProfile.Rows(0).Item("DOB").ToString).ToString("dd/MMM/yyyy")
                Me.lblJoiningDateValue.Text = Convert.ToDateTime(dtProfile.Rows(0).Item("Joining_Date").ToString).ToString("dd/MMM/yyyy")
                If IsDBNull(dtProfile.Rows(0).Item("ContractEndingDate")) Then
                    Me.lblContractEndingDate.Text = String.Empty
                Else
                    Me.lblContractEndingDate.Text = Convert.ToDateTime(dtProfile.Rows(0).Item("ContractEndingDate").ToString).ToString("dd/MMM/yyyy") ''TFS2645
                End If

                Me.lblNICValue.Text = dtProfile.Rows(0).Item("NIC").ToString
                Me.lblCostCentreValue.Text = dtProfile.Rows(0).Item("CostCentre").ToString
                Me.lblMobileValue.Text = dtProfile.Rows(0).Item("Mobile").ToString
                'Me.lblMobileValue.Text = dtProfile.Rows(0).Item("Mobile").ToString
                Me.lblEmailValue.Text = dtProfile.Rows(0).Item("Email").ToString
                Me.lblAddressValue.Text = dtProfile.Rows(0).Item("Address").ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
        Try
            If Me.cmbEmployees.Value > 0 Then
                If Not dtProfile Is Nothing Then
                    dtProfile = Nothing
                End If
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                FillProfile(Me.cmbEmployees.Value)
                'Me.GetAttendance()
                If Not dtEmpoyeeData Is Nothing Then
                    dtEmpoyeeData = Nothing
                End If
                Me.GetEmployeeData()

            Else
                msg_Error("Please select an employee.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub ResetControls()
        Try
            Me.pbemployee.Image = Nothing
            Me.lblNameValue.Text = ""
            Me.lblFatherNameValue.Text = ""
            Me.lblDepartmentValue.Text = ""
            Me.lblDesignationValue.Text = ""
            Me.lblGenderValue.Text = ""
            Me.lblDOBValue.Text = ""
            Me.lblJoiningDateValue.Text = ""
            Me.lblNICValue.Text = ""
            Me.lblCostCentreValue.Text = ""
            Me.lblMobileValue.Text = ""
            Me.lblMobileValue.Text = ""
            Me.lblEmailValue.Text = ""
            Me.lblAddressValue.Text = ""
            Me.txtYear.Enabled = True
            Me.cmbMonth.Enabled = True
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Not e.Tab.Index = 0 Then
                If Me.cmbEmployees.Value > 0 Then
                    If e.Tab.Index = 1 Then
                        Dim strWorkingDays As Integer = DateTime.DaysInMonth(Me.txtYear.Text, (Me.cmbMonth.SelectedIndex + 1))
                        Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
                        Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
                        Dim dt As DataTable = ProfileDal.GetAttendance(Me.cmbEmployees.Value, dtpFromDate, dtpToDate, strWorkingDays)
                        Me.GridEX1.DataSource = dt
                        Me.GridEX1.RetrieveStructure()
                        Me.GridEX1.RootTable.Columns("Salary").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("TotalLeaves").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("TotalLeaves").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("TotalLeaves").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("TotalLeaves").Caption = "Total Leaves"
                        Me.GridEX1.RootTable.Columns("PrvLeave").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("PrvLeave").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("PrvLeave").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("PrvLeave").Caption = "Previous Leaves"

                        Me.GridEX1.RootTable.Columns("TLeaves").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("TLeaves").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("TLeaves").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("TLeaves").Caption = "Total Leaves"

                        Me.GridEX1.RootTable.Columns("OT_Amount").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("OT_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("OT_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("OT_Amount").Caption = "Over Time Amount"

                        Me.GridEX1.RootTable.Columns("LeavDeduction").FormatString = "N"
                        Me.GridEX1.RootTable.Columns("LeavDeduction").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("LeavDeduction").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.GridEX1.RootTable.Columns("LeavDeduction").Caption = "Leave Deduction"

                    End If
                Else
                    ShowErrorMessage("Please select an employee")

                    UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                End If
            End If
        Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles gbSalaryDetail.Enter

    End Sub
    Private Sub GetAttendance()
        Try
            Dim strWorkingDays As Integer = DateTime.DaysInMonth(Me.txtYear.Text, (Me.cmbMonth.SelectedIndex + 1))
            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))

            Dim dtAdvanceDeduction As DataTable = ProfileDal.AdvanceDeductions(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)
            Dim dtOtherDeduction As DataTable = ProfileDal.GetDeduction(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)
            Dim dtAllowances As DataTable = ProfileDal.GetAllowances(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)

            'If dtAdvanceDeduction.Rows.Count > 0 Then
            '    Me.lblAdvanceDeductionValue.Text = dtAdvanceDeduction.Rows(0).Item(0).ToString
            'Else
            '    Me.lblAdvanceDeductionValue.Text = Val(0)
            'End If
            'If dtOtherDeduction.Rows.Count > 0 Then
            '    Me.lblTotalDeductionValue.Text = dtOtherDeduction.Rows(0).Item(0).ToString
            'Else
            '    Me.lblTotalDeductionValue.Text = Val(0)
            'End If
            If dtAllowances.Rows.Count > 0 Then
                Me.lblAllowancesValue.Text = dtAllowances.Rows(0).Item(1).ToString
            Else
                Me.lblAllowancesValue.Text = Val(0)
            End If
            Dim dt As DataTable = ProfileDal.GetAttendance(Me.cmbEmployees.Value, dtpFromDate, dtpToDate, strWorkingDays)
            dt.AcceptChanges()
            Me.lblBasicSalaryValue.Text = Val(dt.Rows(0).Item("Salary").ToString)
            Me.lblAllocatedLeavesValue.Text = Val(dt.Rows(0).Item("TotalLeaves").ToString)
            Me.lblCurrentLeavesValue.Text = Val(dt.Rows(0).Item("PrvLeave").ToString)
            Me.lblTotalLeavesValue.Text = Val(dt.Rows(0).Item("TLeaves").ToString)


            Me.lblLeaveBalanceValue.Text = Val(dt.Rows(0).Item("LeaveBalance").ToString)
            Me.lblPresentDaysValue.Text = Val(dt.Rows(0).Item("PresentDays").ToString)
            Me.lblAbsentDaysValue.Text = Val(dt.Rows(0).Item("AbsentDays").ToString)
            Me.lblWorkingDaysValue.Text = Val(dt.Rows(0).Item("WorkingDays").ToString)


            'Me.lblMonthDaysValue.Text = Val(dt.Rows(0).Item("MonthDays").ToString)
            'Me.lblOverTimeOversValue.Text = Val(dt.Rows(0).Item("OverTimeHrs").ToString)
            'Me.lblLeaveDeductionValue.Text = Val(dt.Rows(0).Item("LeavDeduction").ToString)
            'Me.lblOverTimeAmountValue.Text = Val(dt.Rows(0).Item("OT_Amount").ToString)
            'Dim TotalSalary As Double = (Val(Me.lblBasicSalaryValue.Text) + Val(Me.lblOverTimeAmountValue.Text) + Val(Me.lblAllowancesValue.Text))
            'Dim GrossSalary As Double = (Val(Me.lblBasicSalaryValue.Text) + Val(Me.lblOverTimeAmountValue.Text) + Val(Me.lblAllowancesValue.Text)) - (Val(Me.lblTotalDeductionValue.Text) + Val(Me.lblAdvanceDeductionValue.Text))

            'If ProfileDal.IncomeTaxDeduction("Incom tax at salary") = True Then
            '    Dim IncomeTax As Double = itax(GrossSalary)
            '    Me.lblIncomeTaxValue.Text = IncomeTax
            '    Me.lblGrossSalaryValue.Text = Val(GrossSalary - IncomeTax)
            'Else
            '    Me.lblIncomeTaxValue.Text = 0
            '    Me.lblGrossSalaryValue.Text = Val(GrossSalary)
            'End If
            'Me.GridEX1.DataSource = dt
            'Me.GridEX1.RetrieveStructure()
            'Me.GridEX1.RootTable.Columns("Salary").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TotalLeaves").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("TotalLeaves").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TotalLeaves").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TotalLeaves").Caption = "Total Leaves"
            'Me.GridEX1.RootTable.Columns("PrvLeave").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("PrvLeave").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("PrvLeave").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("PrvLeave").Caption = "Previous Leaves"
            'Me.GridEX1.RootTable.Columns("TLeaves").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("TLeaves").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TLeaves").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TLeaves").Caption = "Total Leaves"
            'Me.GridEX1.RootTable.Columns("OT_Amount").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("OT_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("OT_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("OT_Amount").Caption = "Over Time Amount"
            'Me.GridEX1.RootTable.Columns("LeavDeduction").FormatString = "N"
            'Me.GridEX1.RootTable.Columns("LeavDeduction").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("LeavDeduction").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("LeavDeduction").Caption = "Leave Deduction"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillMonth()
        Try
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.Text = Date.Now.ToString("MMMMM")
            Me.txtYear.Text = Date.Now.Year
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDeductionAndAllowance()
        Try
            'Dim dtDeduction As DataTable = ProfileDal.GetDeductions()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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
    Private Sub GetEmployeeData()
        Try
            Dim DtpLeavePolicyDate As DateTime = getConfigValueByType("Attendance_Period").ToString
            Dim dtpPrevMonthEnd As DateTime = CDate(Val(Me.txtYear.Text) & "-" & GetMonthName(Me.cmbMonth.SelectedIndex + 1) & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            Dim strWorkingDays As Integer = DateTime.DaysInMonth(Me.txtYear.Text, (Me.cmbMonth.SelectedIndex + 1))
            Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            Dim Allowances As Double = 0
            'Dim dtAdvanceDeduction As DataTable = ProfileDal.AdvanceDeductions(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)
            'Dim dtOtherDeduction As DataTable = ProfileDal.GetDeduction(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)
            'Dim dtAllowances As DataTable = ProfileDal.GetAllowances(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)

            dtEmpoyeeData = ProfileDal.GetEmployeeData(Me.cmbEmployees.Value, dtpFromDate, dtpToDate, DtpLeavePolicyDate, dtpPrevMonthEnd, Val(Me.txtYear.Text), Me.cmbMonth.SelectedValue, 30)
            dtEmpoyeeData.AcceptChanges()
            If dtEmpoyeeData.Rows.Count > 0 Then
                ''Me.lblAdvanceDeductionValue.Text = dtEmployeeData.Rows(0).Item(0).ToString
                'Me.lblAdvanceDeductionValue.Text = Val(0)
                'Me.lblTotalDeductionValue.Text = dt.Rows(0).Item(0).ToString
                'Me.lblTotalDeductionValue.Text = Val(0)
                'Me.lblAllowancesValue.Text = dt.Rows(0).Item(1).ToString
                'Me.lblAllowancesValue.Text = Val(0)
                Me.lblBasicSalaryValue.Text = Val(dtEmpoyeeData.Rows(0).Item("Salary").ToString)
                Me.lblAllocatedLeavesValue.Text = Val(dtEmpoyeeData.Rows(0).Item("TotalLeaves").ToString)
                Me.lblCurrentLeavesValue.Text = Val(dtEmpoyeeData.Rows(0).Item("LeaveDays").ToString)
                Me.lblPreviousLeavesValue.Text = Val(dtEmpoyeeData.Rows(0).Item("PrvLeave").ToString)
                Me.lblTotalLeavesValue.Text = Val(dtEmpoyeeData.Rows(0).Item("LeaveDays").ToString)
                Me.lblLeaveBalanceValue.Text = Val(dtEmpoyeeData.Rows(0).Item("LeaveBalance").ToString)
                Me.lblWorkingDaysValue.Text = Val(dtEmpoyeeData.Rows(0).Item("MonthDays").ToString)
                'Me.lblMonthDaysValue.Text = Val(dtEmpoyeeData.Rows(0).Item("WorkingDays").ToString)
                'Me.lblOverTimeOversValue.Text = Val(dtEmpoyeeData.Rows(0).Item("OverTimeHrs").ToString)
                'Me.lblLeaveDeductionValue.Text = Val(dtEmpoyeeData.Rows(0).Item("LeavDeduction").ToString)
                ''Me.lblOverTimeAmountValue.Text = Val(dtEmpoyeeData.Rows(0).Item("OT_Amount").ToString)
                'Me.lblIncomeTaxValue.Text = itax(dtEmpoyeeData.Rows(0).Item("_IncomeTax"))
                Me.lblGrossSalaryValue.Text = Val(dtEmpoyeeData.Rows(0).Item("Total Salary"))
                If dtEmpoyeeData.Columns.Contains("Medical Allownce") Then
                    Allowances += Val(dtEmpoyeeData.Rows(0).Item("Medical Allownce").ToString)
                End If
                If dtEmpoyeeData.Columns.Contains("Conveyance Allownce") Then
                    Allowances += Val(dtEmpoyeeData.Rows(0).Item("Conveyance Allownce").ToString)
                End If
                If dtEmpoyeeData.Columns.Contains("VisitAllowance") Then
                    Allowances += Val(dtEmpoyeeData.Rows(0).Item("VisitAllowance").ToString)
                End If
                If dtEmpoyeeData.Columns.Contains("Dearness Allownce") Then
                    Allowances += Val(dtEmpoyeeData.Rows(0).Item("Dearness Allownce").ToString)
                End If
                Me.lblAllowancesValue.Text = Allowances ''(Val(dtEmpoyeeData.Rows(0).Item("Medical Allownce").ToString) + Val(dtEmpoyeeData.Rows(0).Item("Conveyance Allownce").ToString) + Val(dtEmpoyeeData.Rows(0).Item("VisitAllowance").ToString) + Val(dtEmpoyeeData.Rows(0).Item("Dearness Allownce").ToString))
            End If
            Dim dtTotalLeaves As DataTable = ProfileDal.GetTotalLeaves(Me.cmbEmployees.Value, dtpFromDate, dtpToDate)
            If dtTotalLeaves.Rows.Count > 0 Then
                If dtTotalLeaves.Columns.Contains("Present") Then
                    Me.lblPresentDaysValue.Text = Val(dtTotalLeaves.Rows(0).Item("Present").ToString)
                End If

                If dtTotalLeaves.Columns.Contains("Absent") Then
                    Me.lblAbsentDaysValue.Text = Val(dtTotalLeaves.Rows(0).Item("Absent").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Off Day") Then
                    Me.lblOffDaysValue.Text = Val(dtTotalLeaves.Rows(0).Item("Off Day").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Outdoor Duty") Then
                    Me.lblOutdoorDutyValue.Text = Val(dtTotalLeaves.Rows(0).Item("Outdoor Duty").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Short Leave") Then
                    Me.lblShortLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Short Leave").ToString)
                End If

                If dtTotalLeaves.Columns.Contains("Half Leave") Then
                    Me.lblHalfLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Half Leave").ToString)

                End If
                If dtTotalLeaves.Columns.Contains("Half Absent") Then
                    Me.lblHalfAbsentsValue.Text = Val(dtTotalLeaves.Rows(0).Item("Half Absent").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Leave") Then
                    Me.lblLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Leave").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Sick Leave") Then
                    Me.lblSickLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Sick Leave").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Anual Level") Then
                    Me.lblAnnualLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Anual Level").ToString)

                End If
                If dtTotalLeaves.Columns.Contains("Gazetted Leave") Then
                    Me.lblGazettedLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Gazetted Leave").ToString)
                End If
                If dtTotalLeaves.Columns.Contains("Maternity Leave") Then
                    Me.lblMaternityLeavesValue.Text = Val(dtTotalLeaves.Rows(0).Item("Maternity Leave").ToString)

                End If
            End If

            'Dim TotalSalary As Double = (Val(Me.lblBasicSalaryValue.Text) + Val(Me.lblOverTimeAmountValue.Text) + Val(Me.lblAllowancesValue.Text))
            'Dim GrossSalary As Double = (Val(Me.lblBasicSalaryValue.Text) + Val(Me.lblOverTimeAmountValue.Text) + Val(Me.lblAllowancesValue.Text)) - (Val(Me.lblTotalDeductionValue.Text) + Val(Me.lblAdvanceDeductionValue.Text))

            'If ProfileDal.IncomeTaxDeduction("Incom tax at salary") = True Then
            '    Dim IncomeTax As Double = itax(GrossSalary)
            '    Me.lblIncomeTaxValue.Text = IncomeTax
            '    Me.lblGrossSalaryValue.Text = Val(GrossSalary - IncomeTax)
            'Else
            '    Me.lblIncomeTaxValue.Text = 0
            '    Me.lblGrossSalaryValue.Text = Val(GrossSalary)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class