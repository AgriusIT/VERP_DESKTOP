Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class EmployeeProfileDAL
    Dim strSQL As String = ""
    Public Function GetAttendance(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal WorkingDays As Integer) As DataTable
        Dim dt As DataTable
        Try
            Dim TimeSpan As TimeSpan = FromDate - ToDate
            Dim TotalDays As Integer = TimeSpan.Days
            'strSQL = " Select Employee_Id, Employee_Code, Employee_Name, " _
            '& " Designation, Department, [Salary], EmpSalaryAccountId, CostCentre, CostCenterName As [Cost Centre Name], " _
            strSQL = " Select EmployeesView.Salary, EmployeesView.leavesalloted As TotalLeaves, " _
            & " isnull(PrevBalance.PreviousLeave,0) as  PrvLeave, CurrentLeaves.TLeaves, " _
            & " (convert(money,isnull(Convert(float, EmployeesView.leavesalloted),0),5) - (isnull(CurrentLeaves.TLeaves,0) + isnull(PrevBalance.PreviousLeave,0))) as LeaveBalance," _
            & " CurrentLeaves.PresentDays, IsNull(CurrentLeaves.AbsentDays,0) as AbsentDays, " _
            & " (ISNULL(CurrentLeaves.PresentDays, 0)) + (ISNULL(CurrentLeaves.Tleaves, 0)) As WorkingDays, " & WorkingDays & " As MonthDays, " _
            & " isnull(Overtime.OverTimeHrs,0) as OverTimeHrs," _
            & " Convert(int,((EmployeesView.Salary/ Case When " & WorkingDays & " = 0 Then 1 Else " & WorkingDays & " End) *  IsNull(CurrentLeaves.AbsentDays,0)))  as LeavDeduction, " _
            & " isnull(Overtime.OTAmount,0) as OT_Amount " _
            & " From EmployeesView " _
            & " Left Join ( Select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays FROM FuncAttendanceData('" & FromDate.ToString("yyyy-M-d 23:59:59") & "', '" & ToDate.ToString("yyyy-M-d 23:59:59") & "')) CurrentLeaves on EmployeesView.employee_id  = CurrentLeaves.empid " _
            & " Left Join ( Select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & FromDate.ToString("yyyy-M-d 00:00:00") & "','" & ToDate.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            & " Left Join ( Select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, " _
            & " Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule " _
            & " Where (Convert(varchar,start_date,102) between Convert(dateTime,'" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as Overtime on EmployeesView.employee_id  = Overtime.employeeid Where EmployeesView.employee_id =" & EmployeeId & " "
            'strSQL = "select Dept_Id,Desig_Id,ShiftGroupId," _
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
            '& " left outer join (select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & FromDate.ToString("yyyy-M-d 00:00:00") & "','" & ToDate.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            '& " left outer join (select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays from FuncAttendanceData('" & FromDate.ToString("yyyy-M-d 00:00:00") & "','" & ToDate.ToString("yyyy-M-d 23:59:59") & "')) " _
            '& " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            '& " left  join  (" _
            '& " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            '& " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            '& " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & FromDate.ToString & "',102) And Convert(DateTime,'" & ToDate.ToString & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> ''  AND IsNull(EmployeesView.Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0  " & IIf(Me.cmbDepartment.SelectedIndex = 0, "", " AND EmployeesView.Dept_ID=" & Me.cmbDepartment.SelectedValue & "") & " " & IIf(Me.btnSave.Text = "&Save", " AND EmployeesView.Employee_ID Not In(Select EmployeeID From SalariesExpenseMasterTable WHERE (Convert(Varchar,SalaryExpDate,102) = Convert(DateTime,'" & CDate(Me.txtYear.Text & "-" & Me.cmbMonth.SelectedValue & "-1").ToString("yyyy-M-d 00:00:00") & "',102)))", "") & "     " _
            '& " ) as"
            'strSQL = " Select * From EmployeesView " _
            '    & " Left Join(Select FROM FuncAttendanceData('" & FromDate.ToString("yyyy-M-d 23:59:59") & "', '" & ToDate.ToString("yyyy-M-d 23:59:59") & "') " _
            '    & " Left Join ( )"
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSalary(ByVal EmployeeId As Integer) As DataTable
        Dim dt As DataTable
        Try
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetProfile(ByVal EmployeeId As Integer) As DataTable
        Dim dt As DataTable
        Try
            strSQL = "SELECT [Employee_ID], [Employee_Name], [Employee_Code], [Father_Name], [NIC], [NTN], [Gender], [Martial_Status], [Religion] " _
       & " ,Convert(DateTime, [DOB], 102) As DOB  ,[CityName]  ,[StateName] ,[Address] ,[Phone] ,[Mobile] ,[Email], Convert(DateTime, [Joining_Date], 102) As Joining_Date, [EmployeeDeptName], [EmployeeDesignationName] " _
      & " ,[Salary], [dbo].[EmployeesView].[Active], Convert(DateTime, [ContractEndingDate], 102) As ContractEndingDate, Convert(DateTime, [Leaving_Date], 102) As Leaving_Date ,[EmployeeCommission],[EmpPicture], [Qualification], [Blood_Group] " _
      & " ,[Language], [Emergency_No], [Passport_No], [BankAccount_No], [NIC_Place], [Domicile], [Relation], [InReplacementNewCode], [Previous_Code] " _
       & " ,[Last_Update], [JobType], [Dept_Division], [PayRoll_Division], [RefEmployeeId], [Bank_Ac_Name], tblDefCostCenter.Name As [CostCentre] , [ShiftName], [ShiftStartTime] " _
       & " ,[ShiftEndTime], [CountryName], [RegionName], [ZoneName], [BeltName], [leavesalloted], [ConfirmationDate],[NightShift] ,[IsDailyWages] FROM [dbo].[EmployeesView] Left Join tblDefCostCenter ON  [dbo].[EmployeesView].CostCentre = tblDefCostCenter.CostCenterID Where  [Employee_ID] = " & EmployeeId & ""
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function GetDeductions(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
    '    Dim dt As DataTable
    '    Dim expStr As String = ""
    '    Try
    '        expStr = "Select Case WHEN IsNull(ExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(DeductionDetail.DeductionAmount,0)=0 Then  abs(Sum(isnull(ExpenseDetail.Earning,0))-SUM(isnull(ExpenseDetail.Deduction,0))) Else Abs(Sum(IsNull(DeductionDetail.DeductionAmount,0))) End  Else  abs(Sum(isnull(ExpenseDetail.Earning,0))-SUM(isnull(ExpenseDetail.Deduction,0))) end as DeductionAmount FROM SalariesExpenseMasterTable As ExpenseMaster Inner Join SalariesExpenseDetailTable As ExpenseDetail On ExpenseDetail.SalaryExpId = ExpenseMaster.SalaryExpId " _
    '                            & " Inner Join SalaryExpenseType As ExpenseType On ExpenseDetail.SalaryExpTypeId = ExpenseType.SalaryExpTypeId " _
    '                            & " Left Join (Select EmployeeId, Sum(IsNull(DeductionAmount, 0)) As DeductionAmount FROM DeductionsDetailTable Where (Convert(DateTime, EntryDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102)) Group By EmployeeId) As DeductionDetail On ExpenseMaster.EmployeeId = DeductionDetail.EmployeeId " _
    '                            & " Where (Convert(DateTime, ExpenseMaster.SalaryExpDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102)) And ExpenseMaster.EmployeeId = " & EmployeeId & " And ExpenseType.SalaryExpType <> 'Gross Salary' Group By ExpenseType.DeductionAgainstSalary, DeductionDetail.DeductionAmount "
    '        dt = UtilityDAL.GetDataTable(expStr)
    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function AdvanceDeductions(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable
        Dim expStr As String = ""
        Try
            'expStr = "Select Case WHEN IsNull(ExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(DeductionDetail.DeductionAmount,0)=0 Then  abs(Sum(isnull(ExpenseDetail.Earning,0))-SUM(isnull(ExpenseDetail.Deduction,0))) Else Abs(Sum(IsNull(DeductionDetail.DeductionAmount,0))) End  Else  abs(Sum(isnull(ExpenseDetail.Earning,0))-SUM(isnull(ExpenseDetail.Deduction,0))) end as DeductionAmount FROM SalariesExpenseMasterTable As ExpenseMaster Inner Join SalariesExpenseDetailTable As ExpenseDetail On ExpenseDetail.SalaryExpId = ExpenseMaster.SalaryExpId " _
            '                    & " Inner Join SalaryExpenseType As ExpenseType On ExpenseDetail.SalaryExpTypeId = ExpenseType.SalaryExpTypeId " _
            '                    & " Left Join (Select EmployeeId, Sum(IsNull(DeductionAmount, 0)) As DeductionAmount FROM DeductionsDetailTable Where (Convert(DateTime, EntryDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102)) Group By EmployeeId) As DeductionDetail On ExpenseMaster.EmployeeId = DeductionDetail.EmployeeId " _
            '                    & " Where (Convert(DateTime, ExpenseMaster.SalaryExpDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102)) And ExpenseMaster.EmployeeId = " & EmployeeId & " And ExpenseType.SalaryExpType <> 'Gross Salary' Group By ExpenseType.DeductionAgainstSalary, DeductionDetail.DeductionAmount "
            '
            expStr = "Select EmployeeId, Sum(IsNull(DeductionAmount, 0)) As DeductionAmount FROM DeductionsDetailTable Where (Convert(DateTime, EntryDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102)) And EmployeeId =" & EmployeeId & " Group By EmployeeId"
            dt = UtilityDAL.GetDataTable(expStr)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllowances(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable
        Dim expStr As String = ""
        Try
            expStr = "Select ExpenseMaster.EmployeeId,  IsNull(Sum(ExpenseDetail.Earning), 0) As TotalAllowance FROM SalariesExpenseMasterTable As ExpenseMaster Inner Join SalariesExpenseDetailTable As ExpenseDetail On ExpenseDetail.SalaryExpId = ExpenseMaster.SalaryExpId " _
                                & " Inner Join SalaryExpenseType As ExpenseType On ExpenseDetail.SalaryExpTypeId = ExpenseType.SalaryExpTypeId " _
                                & " Where Convert(Varchar, ExpenseMaster.SalaryExpDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102) And ExpenseMaster.EmployeeId = " & EmployeeId & " And ExpenseType.SalaryExpType <> 'Gross Salary' Group By ExpenseMaster.EmployeeId"
            '& " Left Join (Select EmployeeId Sum(IsNull(DeductionAmount, 0)) As DeductionAmount FROM DeductionsDetailTable Where Convert(Varchar, EntryDate, 102) Between Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23-59-59") & "', 102) Group By EmployeeId) As DeductionDetail On ExpenseMaster.EmployeeId = DeductionDetail.EmployeeId " _
            dt = UtilityDAL.GetDataTable(expStr)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDeduction(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable
        Dim expStr As String = ""
        Try
            expStr = "Select ExpenseMaster.EmployeeId,  IsNull(Sum(ExpenseDetail.Deduction), 0) As TotalAllowance FROM SalariesExpenseMasterTable As ExpenseMaster Inner Join SalariesExpenseDetailTable As ExpenseDetail On ExpenseDetail.SalaryExpId = ExpenseMaster.SalaryExpId " _
                                & " Inner Join SalaryExpenseType As ExpenseType On ExpenseDetail.SalaryExpTypeId = ExpenseType.SalaryExpTypeId " _
                                & " Where Convert(Varchar, ExpenseMaster.SalaryExpDate, 102) Between Convert(DateTime, '" & FromDate.ToString() & "',102) And Convert(DateTime, '" & ToDate.ToString() & "', 102) And ExpenseMaster.EmployeeId = " & EmployeeId & " And ExpenseType.SalaryExpType <> 'Gross Salary' Group By ExpenseMaster.EmployeeId"
            '& " Left Join (Select EmployeeId Sum(IsNull(DeductionAmount, 0)) As DeductionAmount FROM DeductionsDetailTable Where Convert(Varchar, EntryDate, 102) Between Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23-59-59") & "', 102) Group By EmployeeId) As DeductionDetail On ExpenseMaster.EmployeeId = DeductionDetail.EmployeeId " _
            dt = UtilityDAL.GetDataTable(expStr)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IncomeTaxDeduction(ByVal SalaryExpType As String) As Boolean
        Dim strIncomeTax As String = ""
        Dim dt As DataTable
        Try
            strIncomeTax = "Select DeductionAgainsIncomeTax FROM SalaryExpenseType WHERE SalaryExpType ='" & SalaryExpType & "'"
            dt = UtilityDAL.GetDataTable(strIncomeTax)
            If dt.Rows.Count > 0 Then
                If Convert.ToBoolean(dt.Rows(0).Item(0)) = True Then
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
    Public Function GetEmployeeData(ByVal EmployeeId As Integer, ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, ByVal DtpLeavePolicyDate As DateTime, ByVal dtpPrevMonthEnd As DateTime, ByVal Year As Integer, ByVal Month As Integer, ByVal enmSalaryCount As Integer, Optional ByVal ProcessId As Integer = 0) As DataTable
        Try
            'Dim dtpFromDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-1")
            'Dim dtpToDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & GetDayOfEndMonth((Me.cmbMonth.SelectedIndex + 1), Val(Me.txtYear.Text)))
            'Dim DtpLeavePolicyDate As DateTime = getConfigValueByType("Attendance_Period").ToString
            Dim strFilter As String = String.Empty
            Dim strWorkingDays As String = DateTime.DaysInMonth(Year, (Month))
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
            & " where Convert(Varchar, SalaryExpDate, 102) Between Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(DateTime, '" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) SalaryExp on  SalaryExp.EmployeeId = EmployeesView.Employee_Id " _
            & " left outer join (select EmpId,IsNull(LeavesDays,0) as PreviousLeave from FuncAttendanceData('" & DtpLeavePolicyDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpPrevMonthEnd.ToString("yyyy-M-d 23:59:59") & "'))  PrevBalance  on EmployeesView.employee_id  = PrevBalance.empid " _
            & " left outer join (select empid,IsNull(PresentDays,0) as PresentDays,IsNull(LeavesDays,0)  as TLeaves, IsNull(AbsentDays,0) as AbsentDays from FuncAttendanceData('" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "')) " _
            & " CurrentLeaves on EmployeesView.employee_id  = CurrentLEaves.empid " _
            & " left  join  (" _
            & " select employeeid,IsNull(sum(IsNull(OTAmount,0)),0)  AS Ot_Amount, IsNull(sum(isnull(Convert(float,OverTimeHrs),0)),0) as OverTimeHrs from " _
            & " (select employeeid,(Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (convert(float,datediff(mi,start_time,End_time)* 1)/60 * IsNull(overtime_rate_hr,0))) as OTAmount, Convert(float, (Case When datediff(dd,start_date,end_date)=0 then 1 else  datediff(dd,Convert(dateTime,Convert(varchar,start_date,102),102),Convert(DateTime,Convert(varchar,end_date,102),102))+1 end * (datediff(mi, start_time, End_time))))* 1/60 as OverTimeHrs from tblEmployeeOverTimeSchedule where (Convert(varchar,start_date,102) between Convert(dateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(dateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as a group by employeeid ) " _
            & " as  Overtime   on EmployeesView.employee_id  = Overtime.employeeid  Left Join (Select EID,Sum(Total_Amount) as TotalAmount From tblEmployeeNoOfVisits Where Convert(Varchar,MY,102) Between Convert(DateTime,'" & dtpFromDate.ToString & "',102) And Convert(DateTime,'" & dtpToDate.ToString & "',102) Group by EID) as VisitAllowance on EmployeesView.employee_id  = VisitAllowance.EID  WHERE Employee_Name <> ''  AND IsNull(EmployeesView.Active,0) = 1 AND IsNull(EmployeesView.IsDailyWages,0) = 0 " _
            & " ) as a Where a.employee_id =" & EmployeeId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(sp)
            'dt.Columns("Paid_Salary").Expression = "MonthlySalary-Allowance-Insurance-Gratuity_Fund-Advance-WHTax-ESSI-EOBI"
            dt.TableName = "SalarySheet"
            'DeductionAgainstLeaves
            'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
            'Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType  From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> '' "
            'TASK239151 Added Field ApplyValue And ExistingAccount
            Dim strSalaryType As String = "Select SalaryExpTypeId, IsNull(SalaryDeduction,0) as SalaryDeduction, Convert(Varchar, SalaryExpType) as SalaryExpType, IsNull(AccountId,0) as AccountId,isnull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves,isnull(AllowanceOverTime,0) as AllowanceOverTime,isnull(DeductionAgainsIncomeTax,0) as DeductionAgainsIncomeTax,isnull(IncomeTaxExempted,0) as IncomeTaxExempted,IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(ApplyValue,'Fixed') as ApplyValue, IsNull(DeductionAgainstSalary, 0) As DeductionAgainstSalary,Isnull(SiteVisitAllowance,0) AS SiteVisitAllowance From SalaryExpenseType WHERE IsNull(Active,0)=1 And Convert(Varchar, SalaryExpType) <> ''"
            'END TASK239151
            Dim dtSalaryType As New DataTable
            dtSalaryType = UtilityDAL.GetDataTable(strSalaryType)
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
                    'dt.Columns.Add(row(enmSalaryType.DeductionAgainsIncomeTax) & "$" & row(0), GetType(System.Boolean), row(enmSalaryType.DeductionAgainsIncomeTax))
                    'dt.Columns.Add(row(enmSalaryType.IncomeTaxExempted) & "#" & row(0), GetType(System.Boolean), row(enmSalaryType.IncomeTaxExempted))
                    'dt.Columns.Add(row(enmSalaryType.GrossSalaryType) & "*" & row(0), GetType(System.Boolean), row(enmSalaryType.GrossSalaryType))
                    'dt.Columns.Add(CStr(row(enmSalaryType.ApplyValue) & "~" & row(0)), GetType(System.String))
                    dt.Columns.Add(row(6) & "$" & row(0), GetType(System.Boolean), row(6))
                    dt.Columns.Add(row(7) & "#" & row(0), GetType(System.Boolean), row(7))
                    dt.Columns.Add(row(8) & "*" & row(0), GetType(System.Boolean), row(8))
                    dt.Columns.Add(CStr(row(9) & "~" & row(0)), GetType(System.String))

                End If
            Next
            For Each row As DataRow In dt.Rows
                For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
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
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
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
            dtchk = UtilityDAL.GetDataTable("Select Count(*) From dbo.SalariesExpenseMasterTable WHERE ProcessId=" & ProcessId & "")
            dtchk.AcceptChanges()
            Dim strSQL1 As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, Case WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) Else Abs(Sum(IsNull(EmpDeductions.DeductionAmount,0))) End  Else  abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) end as Amount, 'Edit' as Record_Mode " _
                                      & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
                                     & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId INNER JOIN SalaryExpenseType On SalariesExpenseDetailTable.SalaryExpTypeId = SalaryExpenseType.SalaryExpTypeId LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = dbo.SalariesExpenseMasterTable.EmployeeId WHERE dbo.SalariesExpenseMasterTable.ProcessId=" & ProcessId & "  and dbo.SalariesExpenseDetailTable.SalaryExpTypeId <>  0 Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, IsNull(SalaryExpenseType.GrossSalaryType,0), SalaryExpenseType.DeductionAgainstSalary, EmpDeductions.DeductionAmount ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"

            Dim strSQL2 As String = "Select DISTINCT IsNull(tblEmployeeAccounts.Employee_Id,0) as EmployeeId, IsNull(Type_Id,0) as SalaryExpTypeId, Case When IsNull(SalaryExpenseType.GrossSalaryType,0)=1 Then Case When IsNull(Amount,0)=0 Then IsNull(Emp.Salary,0) Else IsNull(Amount,0) End  WHEN IsNull(SalaryExpenseType.DeductionAgainstSalary,0)=1 Then Case When IsNull(EmpDeductions.DeductionAmount,0)=0 Then IsNull(Amount,0) Else IsNull(EmpDeductions.DeductionAmount,0) End  Else IsNull(Amount,0) end as Amount, 'New' as Record_Mode, IsNull(SalaryExpenseType.GrossSalaryType,0) as GrossSalaryType  From tblEmployeeAccounts LEFT OUTER JOIN EmployeesView Emp On Emp.Employee_Id = tblEmployeeAccounts.Employee_Id INNER JOIN SalaryExpenseType on SalaryExpenseType.SalaryExpTypeId = Type_Id LEFT OUTER JOIN (Select EmployeeId, SUM(IsNull(DeductionAmount,0)) as DeductionAmount from DeductionsDetailTable WHERE (Convert(varchar,EntryDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & dtpToDate.ToString("yyyy-M-d 23:59:59") & "',102)) Group By EmployeeId) EmpDeductions On EmpDeductions.EmployeeId = Emp.Employee_Id  WHERE Emp.Employee_Id <> 0 ORDER BY IsNull(SalaryExpenseType.GrossSalaryType,0) DESC"
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
            dt_Data = UtilityDAL.GetDataTable(str)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dt_Data.Select("EmployeeId='" & r("Employee_Id") & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            If (dt.Columns.IndexOf(drFound(1)) + 2) >= enmSalaryCount Then
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
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
                If strTotal.Length > 0 Then
                    Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
                    strTotal = strTotal & IIf(flg = False, "+", "-") & "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                Else
                    strTotal = "IsNull([" & dt.Columns(c + 2).ColumnName & "],0)"
                End If
            Next
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
                Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 4).ColumnName, dt.Columns(c + 4).ColumnName.LastIndexOf("$") - 1 + 1))
                'Microsoft.VisualBasic.Left()
                If flg = True Then
                    strIncomeTaxSalaryType = "" & dt.Columns(c + 2).ColumnName & ""
                    Exit For
                End If
            Next
            Dim strTotalEarning As String = String.Empty
            'For c As Integer = enmSalary.Count To dt.Columns.Count - 7 Step 7
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
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
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
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
            For c As Integer = enmSalaryCount To dt.Columns.Count - 8 Step 8
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

            'If Me.btnSave.Text <> "&Update" Then
            'dt.Columns("_taxableIncome").Expression = strIncomeTaxable.ToString
            'For Each r As DataRow In dt.Rows
            '    r.BeginEdit()
            '    If Val(itax(Val(r.Item("_IncomeTax").ToString))) >= 85.91333333 Then
            '        'Exit Sub
            '    End If
            'dt.Rows(0).BeginEdit()
            'If strIncomeTaxSalaryType.Length > 0 Then
            '    dt.Columns.IndexOf(strIncomeTaxSalaryType) = itax(Val(r.Item("_IncomeTax").ToString))
            'End If
            'dt.Rows(0).EndEdit()
            'Next
            'End If
            dt.Columns.Add("Total Salary", GetType(System.Double))
            dt.Columns("Total Salary").Expression = "(" & strTotal.ToString & ")"
            dt.AcceptChanges()
            Return dt
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
        End Try
    End Function

    Public Function GetTotalLeaves(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim cm As New SqlCommand
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
            cm.CommandText = " Exec AttendanceRegisterUpdateDates '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & ToDate.ToString("yyyy-M-d 23:59:59") & "' "
            cm.ExecuteNonQuery()
            'cm.
            trans.Commit()
            'dtEmp.AcceptChanges()
            Dim strTotalLeaves As String = "Select Count(Case When AttendanceStatus='Present' then 'P' else null end) as [Present], Count(Case When AttendanceStatus='Off Day' then 'DO' ELSE NULL end) AS [Off Day],   Count(Case When AttendanceStatus='Outdoor Duty' then 'OD' else null end) as [Outdoor Duty],  Count(Case When AttendanceStatus='Short Leave' then 'SL' else null end) as [Short Leave],    Count(Case When AttendanceStatus='Half Leave' then 'HL' else null end) as [Half Leave],  Count(Case When AttendanceStatus='Absent' then 'A' when AttendanceStatus is null then 'A' end)  as [Absent], Count(Case When AttendanceStatus='Half Absent' then 'HA' when AttendanceStatus is null then null end)  as [Half Absent],  Count(Case When AttendanceStatus='Leave' then 'L' else null end) as [Leave],   	 Count(Case When AttendanceStatus='Sick Leave' then 'SL' else null end) as [Sick Leave], Count(Case When AttendanceStatus='Anual Level' then 'AL' else null end) as [Anual Level],   Count(Case When AttendanceStatus='Gazetted Leave' then 'GL' else null end) as [Gazetted Leave],    Count(Case When AttendanceStatus='Maternity Leave' then 'ML' else null end ) as [Maternity Leave], att_date.empId from tmpAttendanceDates as att_date  left outer join (Select * From tblAttendanceDetail where (Convert(datetime,attendanceDate,102) BETWEEN Convert(DateTime,'" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102))) as att on att.empid = att_date.empid AND att.attendanceDate = att_date.dates And att.AttendanceId = att_date.AttendanceId Where att_date.empId = " & EmployeeId & " Group By att_date.empId"
            dt = UtilityDAL.GetDataTable(strTotalLeaves)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
