'10-May-2014 TASK:M40 Imran Ali Deduction Slot Time Wise
''10-May-2014 TASK:2623 Imran Ali Slot Late Time Implement On Employee Salaries Detail Report (Shop and Save)
''16-May-2014 TASK2623 Slot LateTime  On Employee Salaries Detail
Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmGrdRptGenerelEmployeeSalary
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Public dblTotalWorkingDays As Double = 0D
    Public DateTimePicker1 As New DateTimePicker
    Public DateTimePicker2 As New DateTimePicker
    Public DefaultWorkingHours As Double = 10D
    Enum enmEmp
        Employee_Id
        Employee_Name
        Employee_Code
        EmployeeDeptName
        CostCentre
        Salary
        TotalWorkingDays
        Leaves
        Absents
        TotalLeaves
        TotalWorkedTime
        SchTotalWorkedTime
        TotalLate
        TotalOverTime
        LeaveEncashment
        ActualWorkingHours
        OverTimeSalary
    End Enum
    Private Sub frmGrdRptGenerelEmployeeSalary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtYear.Text = Now.Year
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
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
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            Me.cmbMonth.SelectedValue = Now.Month
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        'task1248
        'Try
        '    If Condition = "Dept" Then
        '        FillDropDown(Me.ComboBox1, "Select * from EmployeeDeptDefTable")
        '    ElseIf Condition = "Emp" Then
        '        FillDropDown(Me.ComboBox2, "Select * From tblDefEmployee " & IIf(Me.ComboBox1.SelectedIndex > 0, " WHERE Dept_Id=" & Me.ComboBox1.SelectedValue & " And Active=1 ", "") & "")
        '    ElseIf Condition = "CostCentre" Then
        '        FillDropDown(Me.cmbCostCentre, "Select * FROM tblDefCostCenter Order By SortOrder, Name")
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Private Sub frmGrdRptGenerelEmployeeSalary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            Me.txtYear.Text = Now.Year
            Me.cmbMonth.Text = Now.ToString("MMMM")
            'Me.DateTimePicker1.Value = Now.AddMonths(-1)
            'Me.DateTimePicker2.Value = Now
            FillCombo("Dept")
            FillCombo("Emp")
            FillCombo("CostCentre")
            'task1248
            'If Not Me.ComboBox1.SelectedIndex = -1 Then Me.ComboBox1.SelectedIndex = 0
            'If Not Me.ComboBox2.SelectedIndex = -1 Then Me.ComboBox2.SelectedIndex = 0
            If getConfigValueByType("Working_Days").ToString <> "" Then
                dblTotalWorkingDays = Val(getConfigValueByType("Working_Days").ToString)
            End If
            If getConfigValueByType("DefaultWorkingHours").ToString <> "" Then
                DefaultWorkingHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
            End If
            'task1248
            'Me.lblDefaultWorkingHours.Text = "&Default Working Hours: " & DefaultWorkingHours & ""

            Me.btnShow.Enabled = True


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Me.btnLateDeductions.Enabled = True
            FillData()
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
                ElseIf RightsDt.FormControlName = "Print" Then
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
    Public Sub FillData(Optional ByVal Condition As String = "")

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim objCmd As New OleDb.OleDbCommand

        Try

            objCmd.Connection = Con
            objCmd.Transaction = objTrans
            Dim strSQL As String = String.Empty
            Dim da As New OleDb.OleDbDataAdapter
            Dim dt As New DataTable

            strSQL = String.Empty
            strSQL = "TRUNCATE TABLE tblTempEmployeeAttendanceSummary"
            objCmd.CommandText = strSQL
            objCmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "SP_EmpAttendanceDt '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "'"
            objCmd.CommandText = ""
            objCmd.CommandText = strSQL
            da.SelectCommand = objCmd
            da.Fill(dt)



            Dim dtData As New DataTable
            dtData.Columns.Add("Id", GetType(System.Int32))
            dtData.Columns.Add("EmpId", GetType(System.Int32))
            dtData.Columns.Add("AttendanceDate", GetType(System.DateTime))
            dtData.Columns.Add("In_Time", GetType(System.DateTime))
            dtData.Columns.Add("Sch_In_Time", GetType(System.String))
            dtData.Columns.Add("Flexibility_In_Time", GetType(System.String))
            dtData.Columns.Add("Out_Time", GetType(System.DateTime))
            dtData.Columns.Add("Sch_Out_Time", GetType(System.String))
            dtData.Columns.Add("Flexibility_Out_Time", GetType(System.String))
            'Task:2623 Added Columns
            dtData.Columns.Add("LateIn", GetType(System.Double))
            dtData.Columns.Add("LateOut", GetType(System.Double))
            'End Task:2623
            dtData.Columns("Id").AutoIncrement = True
            dtData.Columns("Id").AutoIncrementSeed = 1
            dtData.Columns("Id").AutoIncrementStep = 1

            Dim drData As DataRow = Nothing
            Dim flg As Boolean = False
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Try


                            flg = False
                            If dt.Rows(i).Item("AttendanceType").ToString = "In" Then
                                drData = dtData.NewRow
                                drData(1) = dt.Rows(i).Item("EmpId").ToString
                                drData(2) = dt.Rows(i).Item("AttendanceDate").ToString
                                drData(3) = dt.Rows(i).Item("AttendanceTime").ToString
                                drData(4) = IIf(dt.Rows(i).Item("ScheduleTime").ToString <> "", dt.Rows(i).Item("ScheduleTime").ToString, DBNull.Value)
                                drData(5) = IIf(dt.Rows(i).Item("FlexibilityTime").ToString <> "", dt.Rows(i).Item("FlexibilityTime").ToString, DBNull.Value)
                                'Task:2623 Added Slot Late Time Logic
                                If dt.Rows(i).Item("FlexibilityTime").ToString.Length > 0 AndAlso (CDate(dt.Rows(i).Item("FlexibilityTime").ToString) > CDate(dt.Rows(i).Item("ScheduleTime").ToString)) Then
                                    Dim intFlexibilityTime As Double = DateDiff(DateInterval.Minute, CDate(dt.Rows(i).Item("FlexibilityTime").ToString), CDate(dt.Rows(i).Item("AttendanceTime").ToString))
                                    If intFlexibilityTime > 0 Then
                                        strSQL = String.Empty
                                        strSQL = "Select * from tblLateTimeSlot WHERE " & intFlexibilityTime & " BETWEEN Slot_Start AND Slot_End"
                                        objCmd.CommandText = ""
                                        objCmd.CommandText = strSQL
                                        Dim objTimeSlotDA As New OleDb.OleDbDataAdapter
                                        Dim objTimeSlotDt As New DataTable
                                        objTimeSlotDA.SelectCommand = objCmd
                                        objTimeSlotDA.Fill(objTimeSlotDt)
                                        If objTimeSlotDt IsNot Nothing Then
                                            If objTimeSlotDt.Rows.Count > 0 Then
                                                drData(9) = Val(objTimeSlotDt.Rows(0).Item(2).ToString)
                                            End If
                                        End If
                                    End If
                                End If
                                'End Task:2623
                                flg = True
                            End If
                            If (i + 1) <= dt.Rows.Count - 1 Then
                                If dt.Rows(i + 1).Item("AttendanceType").ToString = "Out" Then
                                    drData(6) = dt.Rows(i + 1).Item("AttendanceTime").ToString
                                    drData(7) = IIf(dt.Rows(i).Item("ScheduleTime").ToString <> "", dt.Rows(i + 1).Item("ScheduleTime").ToString, DBNull.Value)
                                    drData(8) = IIf(dt.Rows(i).Item("FlexibilityTime").ToString <> "", dt.Rows(i + 1).Item("FlexibilityTime").ToString, DBNull.Value)
                                    'Task:2623 Added Slot Late Time Logic
                                    If dt.Rows(i).Item("FlexibilityTime").ToString.Length > 0 AndAlso (CDate(dt.Rows(i).Item("ScheduleTime").ToString) > CDate(dt.Rows(i).Item("FlexibilityTime").ToString)) Then
                                        Dim intFlexibilityTime As Double = DateDiff(DateInterval.Minute, CDate(dt.Rows(i).Item("AttendanceTime").ToString), CDate(dt.Rows(i).Item("FlexibilityTime").ToString))
                                        If intFlexibilityTime > 0 Then
                                            strSQL = String.Empty
                                            strSQL = "Select * from tblLateTimeSlot WHERE " & intFlexibilityTime & " BETWEEN Slot_Start AND Slot_End"
                                            objCmd.CommandText = ""
                                            objCmd.CommandText = strSQL
                                            Dim objTimeSlotDA As New OleDb.OleDbDataAdapter
                                            Dim objTimeSlotDt As New DataTable
                                            objTimeSlotDA.SelectCommand = objCmd
                                            objTimeSlotDA.Fill(objTimeSlotDt)
                                            If objTimeSlotDt IsNot Nothing Then
                                                If objTimeSlotDt.Rows.Count > 0 Then
                                                    drData(10) = Val(objTimeSlotDt.Rows(0).Item(2).ToString)
                                                End If
                                            End If
                                        End If
                                    End If
                                    'End Task:2623
                                    flg = True
                                End If
                            End If

                            If flg = True Then dtData.Rows.Add(drData)
                        Catch ex As Exception

                        End Try
                    Next
                End If
            End If


            For Each objRow As DataRow In dtData.Rows
                strSQL = String.Empty
                'Task:2623 before 
                'strSQL = "INSERT INTO tblTempEmployeeAttendanceSummary(EmpId, [AttendanceDate],[In_Time],[Sch_In_Time],[Flexibility_In_Time],[Out_Time],[Sch_Out_Time],[Flexibility_Out_Time])" _
                '& " VALUES(" & Val(objRow.Item("EmpId").ToString) & ", Convert(DateTime, '" & objRow.Item("AttendanceDate").ToString & "',102), " & IIf(objRow.Item("In_Time").ToString.Length > 1, "Convert(Datetime,'" & objRow.Item("In_Time").ToString & "',102)", "NULL") & ", " & IIf(objRow.Item("Sch_In_Time").ToString.Length > 1, "'" & objRow.Item("Sch_In_Time").ToString & "'", "NULL") & ", " & IIf(objRow.Item("Flexibility_In_Time").ToString.Length > 1, "'" & objRow.Item("Flexibility_In_Time").ToString & "'", "NULL") & "," & IIf(objRow.Item("Out_Time").ToString.Length > 1, "Convert(Datetime,'" & objRow.Item("Out_Time").ToString & "',102)", "NULL") & ", " & IIf(objRow.Item("Sch_Out_Time").ToString.Length > 1, "'" & objRow.Item("Sch_Out_Time").ToString & "'", "NULL") & ", " & IIf(objRow.Item("Flexibility_Out_Time").ToString.Length > 1, "'" & objRow.Item("Flexibility_Out_Time").ToString & "'", "NULL") & " )"
                'Task:2623 Added Columns LateIn, LateOut
                strSQL = "INSERT INTO tblTempEmployeeAttendanceSummary(EmpId, [AttendanceDate],[In_Time],[Sch_In_Time],[Flexibility_In_Time],[Out_Time],[Sch_Out_Time],[Flexibility_Out_Time], LateIn, LateOut)" _
              & " VALUES(" & Val(objRow.Item("EmpId").ToString) & ", Convert(DateTime, '" & objRow.Item("AttendanceDate").ToString & "',102), " & IIf(objRow.Item("In_Time").ToString.Length > 1, "Convert(Datetime,'" & objRow.Item("In_Time").ToString & "',102)", "NULL") & ", " & IIf(objRow.Item("Sch_In_Time").ToString.Length > 1, "'" & objRow.Item("Sch_In_Time").ToString & "'", "NULL") & ", " & IIf(objRow.Item("Flexibility_In_Time").ToString.Length > 1, "'" & objRow.Item("Flexibility_In_Time").ToString & "'", "NULL") & "," & IIf(objRow.Item("Out_Time").ToString.Length > 1, "Convert(Datetime,'" & objRow.Item("Out_Time").ToString & "',102)", "NULL") & ", " & IIf(objRow.Item("Sch_Out_Time").ToString.Length > 1, "'" & objRow.Item("Sch_Out_Time").ToString & "'", "NULL") & ", " & IIf(objRow.Item("Flexibility_Out_Time").ToString.Length > 1, "'" & objRow.Item("Flexibility_Out_Time").ToString & "'", "NULL") & ", " & Val(objRow.Item("LateIn").ToString) & ", " & Val(objRow.Item("LateOut").ToString) & ")"
                'End Task:2623
                objCmd.CommandText = ""
                objCmd.CommandText = strSQL
                objCmd.ExecuteNonQuery()
            Next


            'Task:2623 Added Group EmpId On Min Date Query
            strSQL = "Select Employee_Id, Employee_Name as [Emp Name], Employee_Code as [Emp Code], EmployeeDeptName as Department, tblDefCostCenter.Name AS [Cost Centre], Salary,IsNull(TotalWorkingDays,0) as TotalWorkingDays,IsNull(LeaveAtt.LeaveCount,0) as Leaves,IsNull(AbsentAtt.AbsentCount,0) as Absents, 0.0 as [Total Leaves], 0 as [Leave Encashment],IsNull(TotalWorkedTime,0) as TotalWorkedTime, Case When IsNull(SchTotalWorkedTime,0)=0 Then  IsNull(TotalWorkedTime,0) Else IsNull(SchTotalWorkedTime,0) End as SchTotalWorkedTime, IsNull(TotalLate,0) as TotalLate, IsNull(TotalOverTime,0) as TotalOverTime,  Convert(float,0) as [Actual Working Hours], IsNull(OverTimeSalary,0) as OverTimeSalary  From tblDefEmployee LEFT JOIN tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID " _
                     & " INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = Dept_Id " _
                      & " LEFT OUTER JOIN EmployeeDesignationDefTable On EmployeeDesignationDefTable.EmployeeDesignationId = tblDefEmployee.Desig_ID " _
                      & "  LEFT OUTER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId " _
                     & " LEFT OUTER JOIN  tblListCity ON tblDefEmployee.City_ID = tblListCity.CityId " _
                     & " Left Outer Join ( " _
                     & " Select EmpId, SUM(TotalWorkedTime) as TotalWorkedTime, SUM(TotalSchTime) as SchTotalWorkedTime, Sum(Isnull(InLate,0)+Isnull(OutLate,0)) as TotalLate From ( " _
                     & "  Select EmpId, (Convert(float,(Datediff(mi,Convert(Datetime,In_Time),Case When Out_Time <> '' Then Out_Time When Sch_Out_Time <> '' Then Sch_Out_Time End)))/60)  as TotalWorkedTime, (Convert(float,(Datediff(mi,Convert(Datetime,Sch_In_Time),Convert(Datetime, Sch_Out_Time))))/60) as TotalSchTime, (LateIn/60) as InLate, (LateOut/60) as OutLate  From tblTempEmployeeAttendanceSummary  " _
                     & " )a Group by EmpId " _
                     & " ) TotalWork on TotalWork.EmpId = Employee_Id " _
                     & " LEFT OUTER JOIN ( " _
                     & " Select EmployeeId, SUM((DateDiff(mi, " _
                     & " Convert(DateTime, (Convert(Varchar, [Start_Date], 101) + ' ' + Start_Time)), " _
                     & " Convert(DateTime, (Convert(Varchar, End_Date, 101) + ' ' + End_Time)))/60)) as TotalOverTime, " _
                     & " SUM((DateDiff(mi, " _
                     & " Convert(DateTime, (Convert(Varchar, [Start_Date], 101) + ' ' + Start_Time)), " _
                     & " Convert(DateTime, (Convert(Varchar, End_Date, 101) + ' ' + End_Time)))/60)*Isnull(OverTime_Rate_HR,0)) as OverTimeSalary  From tblEmployeeOverTimeSchedule " _
                     & " WHERE (Convert(Varchar, [Start_Date], 102) >= Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Varchar, End_Date,102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " _
                     & " Group By EmployeeId " _
                     & " ) OverTime On OverTime.EmployeeId = Employee_Id " _
                     & " LEFT OUTER JOIN (Select EmpId, Count(AttendanceTime) as TotalWorkingDays From tblAttendanceDetail WHERE AttendanceType='In' AND AttendanceTime IN(Select Min(AttendanceTime) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group by AttendanceDate, EmpId)  Group by EmpId) " _
                     & " WorkingDays On WorkingDays.EmpId = Employee_Id"

            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as LeaveCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='Leave' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By tblAttendanceDetail.EmpId) LeaveAtt On LeaveAtt.EmpId = Employee_Id "
            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as AbsentCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='Absent' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By tblAttendanceDetail.EmpId) AbsentAtt On AbsentAtt.EmpId = Employee_Id "
            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as HalfLeaveCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='Half Leave' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By tblAttendanceDetail.EmpId) HalfLeaveAtt On HalfLeaveAtt.EmpId = Employee_Id "
            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as ShortLeaveCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='Short Leave' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail  WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By tblAttendanceDetail.EmpId) ShortLeaveAtt On ShortLeaveAtt.EmpId = Employee_Id "
            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as ODLeaveCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='OD' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By tblAttendanceDetail.EmpId) ODLeaveAtt On ODLeaveAtt.EmpId = Employee_Id "
            strSQL += " LEFT OUTER JOIN(Select tblAttendanceDetail.EmpId, Count(tblAttendanceDetail.AttendanceDate) as CasualLeaveCount From tblAttendanceDetail where tblAttendanceDetail.AttendanceStatus='Casual Leave' AND tblAttendanceDetail.AttendanceDate In(Select Min(AttendanceDate) From tblAttendanceDetail WHERE (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND (Convert(varchar, AttendanceDate,102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Group By EmpId, AttendanceDate) Group By tblAttendanceDetail.EmpId) CasualLeaveAtt On CasualLeaveAtt.EmpId = Employee_Id "





            strSQL += " WHERE Employee_Code <> '' "
            ''Start TFS3418
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                strSQL += " AND Employee_Id in (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                strSQL += " AND Desig_Id in (" & Me.lstEmpDesignation.SelectedIDs & ")"
            End If
            If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                strSQL += " AND tblDefEmployee.Dept_Id in (" & Me.lstEmpDepartment.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strSQL += " AND dbo.tblDefEmployee.CostCentre in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                strSQL += " AND dbo.tblDefEmployee.ShiftGroupId in (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                strSQL += " AND City_ID in(" & Me.lstEmpCity.SelectedIDs & ")"
            End If
            ''End TFS3418

            'End Task:2623
            '#task1248
            'If Me.ComboBox1.SelectedIndex > 0 Then
            '    strSQL += " AND Dept_Id=" & Me.ComboBox1.SelectedValue & ""
            'End If
            'If Me.ComboBox2.SelectedIndex > 0 Then
            '    strSQL += " AND Employee_Id=" & Me.ComboBox2.SelectedValue & ""
            'End If
            'If Me.cmbCostCentre.SelectedValue > 0 Then
            '    strSQL += " AND tblDefEmployee.CostCentre =" & Me.cmbCostCentre.SelectedValue & ""
            'End If



            dt.Clear()
            dt.Columns.Clear()

            objCmd.CommandText = ""
            objCmd.CommandText = strSQL
            da.SelectCommand = objCmd
            da.Fill(dt)
            dt.AcceptChanges()

            objCmd.CommandText = ""
            objCmd.CommandText = "Select IsNull(Step1,0) as Step1, IsNull(Step2,0) as Step2 From tblLeaveEncashment"
            Dim dtstep As New DataTable
            Dim dastep As New OleDb.OleDbDataAdapter
            dastep.SelectCommand = objCmd
            dastep.Fill(dtstep)

            Dim dr() As DataRow

            For Each objrow As DataRow In dt.Rows
                objrow.BeginEdit()
                dr = dtstep.Select("Step1 >= " & Val(objrow.Item("TotalWorkingDays").ToString) & " And Step1 <= " & Val(objrow.Item("TotalWorkingDays").ToString) & "")
                If dr.Length > 0 Then
                    objrow.Item("Leave Encashment") = Val(dr(0).Item(1).ToString)
                End If
                objrow.EndEdit()
            Next
            dt.Columns("Total Leaves").Expression = "(Leaves+[Absents])"
            dt.Columns("Actual Working Hours").Expression = "((SchTotalWorkedTime-TotalLate)+([Leave Encashment]*" & DefaultWorkingHours & "))"
            dt.Columns.Add("Net Salary", GetType(System.Double))
            dt.Columns("Net Salary").Expression = "(((Salary/" & Me.DateTimePicker2.Value.Day & ")/" & DefaultWorkingHours & ")*[Actual Working Hours])+[OverTimeSalary]"   '"((Salary/IIF(TotalWorkingDays <> 0,TotalWorkingDays,1))*IIF(TotalWorkingDays <> 0,SchTotalWorkedTime,1))+OverTimeSalary"

            objTrans.Commit()
            dt.TableName = "Default"

            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.AutoSizeColumns()
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Me.GridEX1.RootTable.Columns("TotalLate").Caption = "Late Hours"
            Me.GridEX1.RootTable.Columns("TotalWorkedTime").Caption = "Total Worked Hours"
            Me.GridEX1.RootTable.Columns("SchTotalWorkedTime").Caption = "Sch Worked Hours"
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GridEX1.RootTable.Columns("Net Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("OverTimeSalary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            For c As Int32 = enmEmp.Salary To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(c).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(c).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(c).FormatString = "N" & DecimalPointInValue
                Me.GridEX1.RootTable.Columns(c).TotalFormatString = "N" & DecimalPointInValue
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            '#task1248
            'If Me.ComboBox1.SelectedIndex = -1 Then Exit Sub
            FillCombo("Emp")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Salaries Detail" & vbCrLf & "Date From:" & Me.DateTimePicker1.Value.Date & " Date To: " & Me.DateTimePicker2.Value.Date & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try

            If Me.txtYear.Text.Length < 4 Then Exit Sub
            Dim MonthEndDay As Integer = 1
            Dim cmb As ComboBox = CType(Me.cmbMonth, ComboBox)
            Select Case cmb.SelectedIndex
                Case 0
                    MonthEndDay = 31
                Case 1
                    If Val(Me.txtYear.Text) Mod 4 = 0 Then
                        MonthEndDay = 29
                    Else
                        MonthEndDay = 28
                    End If
                Case 2
                    MonthEndDay = 31
                Case 3
                    MonthEndDay = 30
                Case 4
                    MonthEndDay = 31
                Case 5
                    MonthEndDay = 30
                Case 6
                    MonthEndDay = 31
                Case 7
                    MonthEndDay = 31
                Case 8
                    MonthEndDay = 30
                Case 9
                    MonthEndDay = 31
                Case 10
                    MonthEndDay = 30
                Case 11
                    MonthEndDay = 31
            End Select

            Me.DateTimePicker1.Value = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.SelectedIndex + 1 & "-1")
            Me.DateTimePicker2.Value = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.SelectedIndex + 1 & "-" & MonthEndDay)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
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
    End Sub

    Private Sub btnLateDeductions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLateDeductions.Click
        Try

            Me.btnShow.Enabled = False
            Me.btnLateDeductions.Enabled = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Me.btnLateDeductions.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            Me.btnLateDeductions.Enabled = False
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
                'FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
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

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
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