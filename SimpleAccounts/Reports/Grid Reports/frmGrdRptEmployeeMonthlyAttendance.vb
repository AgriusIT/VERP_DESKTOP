'' Muhammad Ameen has done TASK TFS3402 on 23-05-2018 : Extention of retrieval of record from one month to unlimited date range , changed main column title to full date and a new column out time.
Imports SBModel
Imports SBDal
Imports SBUtility

Public Class frmGrdRptEmployeeMonthlyAttendance

    Implements IGeneral
    Dim _SearchDt As New DataTable
    Dim IsOpenForm As Boolean = False
    Dim mFromDate As DateTime
    Dim mToDate As DateTime
    Enum enmEmp
        Employee_Id
        Employee_Code
        Employee_Name
        EmployeeDesignationName
        EmployeeDeptName
        CostCentre
        Count
    End Enum

    Private Sub frmGrdRptEmployeeMonthlyAttendance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdRptEmployeeMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtYear.Text = Now.Year
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Designation")
            Me.lstDesignation.DeSelect()
            FillCombos("Department")
            Me.lstDepartment.DeSelect()
            FillCombos("Division")
            Me.lstDivision.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstCity.DeSelect()
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            Me.cmbMonth.SelectedValue = Now.Month
            Me.dtpFromdate.Value = New DateTime(Now.Year, Now.Month, 1)
            'Me.dtpTodate.Value = Me.dtpFromdate.Value.AddMonths(1).AddDays(-1)

            Me.dtpTodate.Value = Now

            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Employees" Then
                FillListBox(Me.lstEmployee.ListItem, "Select Employee_Id, Employee_Code +'-'+ Employee_Name  As [Employee Name] From tblDefEmployee WHERE Active=1 ORDER BY 2 Asc") ''TASKTFS75 added and set active =1
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Division" Then
                FillListBox(Me.lstDivision.ListItem, "Select Division_Id, Division_Name, Division_Code From tblDefDivision WHERE Active=1 ")
            ElseIf Condition = "CostCentre" Then
                '' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select distinct CostCenterID, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstShiftGroup.ListItem, "SELECT ShiftGroupId,ShiftGroupName FROM ShiftGroupTable WHERE Active=1 ")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstCity.ListItem, "SELECT  CityId, CityName FROM tblListCity WHERE Active=1 ")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub GetSecurityRights()
    '    Try
    '        If LoginGroup = "Administrator" Then
    '            Me.Visible = True
    '            Me.btnPrint.Enabled = True
    '            Exit Sub
    '        End If
    '        If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
    '            If RegisterStatus = EnumRegisterStatus.Expired Then
    '                Me.Visible = False
    '                Me.btnPrint.Enabled = False
    '                Exit Sub
    '            End If
    '        Else
    '            Me.Visible = False
    '            Me.btnPrint.Enabled = False
    '            For Each RightsDt As GroupRights In Rights
    '                If RightsDt.FormControlName = "View" Then
    '                    Me.Visible = True
    '                ElseIf RightsDt.FormControlName = "Print" Then
    '                    Me.btnPrint.Enabled = True
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub
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

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            'mFromDate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.SelectedValue & "-1")
            'mToDate = CDate(MyToDate(Me.cmbMonth.SelectedValue, Val(Me.txtYear.Text)))
            mFromDate = CDate(Me.dtpFromdate.Value)
            mToDate = CDate(Me.dtpTodate.Value)
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            ''Query Edit Against TFS3418 : Ayesha Rehman : 28-05-2018
            strSQL = "SELECT tblDefEmployee.Employee_Id, dbo.tblDefEmployee.Employee_Code, " _
                    & "  dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, dbo.EmployeeDeptDefTable.EmployeeDeptName, tblDefCostCenter.Name AS [Cost Centre] " _
                    & "  FROM  dbo.tblDefEmployee INNER JOIN " _
                    & "  dbo.EmployeeDesignationDefTable ON dbo.tblDefEmployee.Desig_ID = dbo.EmployeeDesignationDefTable.EmployeeDesignationId INNER JOIN " _
                    & "  dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId LEFT JOIN tblDefCostCenter ON dbo.tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID " _
                    & " INNER JOIN (SELECT ShiftGroupId FROM ShiftScheduleTable INNER JOIN ShiftTable ON  ShiftScheduleTable.ShiftId = ShiftTable.ShiftId " & IIf(rbtnNightShift.Checked = True, " WHERE ISNULL(ShiftTable.NightShift, 0)=1", " WHERE ISNULL(ShiftTable.NightShift, 0)=0 ") & ") AS Shift ON tblDefEmployee.ShiftGroupId = Shift.ShiftGroupId WHERE Employee_Code <> '' AND tblDefEmployee.active = 1"
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                strSQL += " AND Employee_Id in (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstDesignation.SelectedIDs.Length > 0 Then
                strSQL += " AND Desig_Id in (" & Me.lstDesignation.SelectedIDs & ")"
            End If
            If Me.lstDepartment.SelectedIDs.Length > 0 Then
                strSQL += " AND tblDefEmployee.Dept_Id in (" & Me.lstDepartment.SelectedIDs & ")"
            End If
            If Me.lstDivision.SelectedIDs.Length > 0 Then
                strSQL += " AND Dept_Division in (" & Me.lstDivision.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strSQL += " AND dbo.tblDefEmployee.CostCentre in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstShiftGroup.SelectedIDs.Length > 0 Then
                strSQL += " AND tblDefEmployee.ShiftGroupId in(" & Me.lstShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                strSQL += " AND City_ID in(" & Me.lstCity.SelectedIDs & ")"
            End If

            dt = GetDataTable(strSQL)
            Dim intDays As Integer = mFromDate.Subtract(mToDate).Days

            Dim mDates As New List(Of DateTime)
            For i As Integer = 0 To Math.Abs(intDays)
                mDates.Add(mFromDate.AddDays(i))
            Next
            'For i As Integer = 0 To Math.Abs(intDays)
            '    mDates.Add(mFromDate)
            'Next
            Dim d As Integer = 1
            'For Each mdate As DateTime In mDates
            '    dt.Columns.Add(d, GetType(System.String), d)
            '    dt.Columns.Add("Status-" & mdate.ToString("dd"), GetType(System.String))
            '    dt.Columns.Add("In-" & mdate.ToString("dd"), GetType(System.String))
            '    dt.Columns.Add("Out-" & mdate.ToString("dd"), GetType(System.String))

            '    d += 1
            'Next
            For Each mdate As DateTime In mDates


                'dt.Columns.Add(d

                'dt.Columns.Add(d, GetType(System.String), d)
                'dt.Columns.Add("Status-" & mdate.ToString("dd"), GetType(System.String))
                'dt.Columns.Add("In-" & mdate.ToString("dd"), GetType(System.String))
                'dt.Columns.Add("Out-" & mdate.ToString("dd"), GetType(System.String))
                ''TASK TFS3402
                Dim _date As String = mdate.ToString("dd/MMM/yyyy")
                dt.Columns.Add(_date, GetType(System.String), d)
                dt.Columns.Add("Status-" & _date, GetType(System.String))
                dt.Columns.Add("In-" & _date, GetType(System.String))
                dt.Columns.Add("Out-" & _date, GetType(System.String))
                ''END TASK TFS3402
                d += 1
            Next

            For Each r As DataRow In dt.Rows
                For c As Integer = enmEmp.Count To dt.Columns.Count - 4 Step 4
                    r.BeginEdit()
                    r(c + 1) = "A"
                    r(c + 2) = ":0"
                    r(c + 3) = ":0"
                    r.EndEdit()
                Next
            Next
            dt.AcceptChanges()
            Dim dtData As New DataTable
            strSQL = String.Empty
            '' Below code is commented on 22-05-2018 against TASK TFS3402
            'strSQL = "Select EmpId, Day(AttendanceDate) as AttendanceDate, Time, Status From( " _
            '         & " Select AttendanceId, EmpId, Convert(DateTime, AttendanceDate,108) as AttendanceDate,  Left(Right(Convert(Varchar,AttendanceTime,109),14),8) + ' ' + Right(Convert(Varchar,AttendanceTime,109),2)  as [Time]," _
            '         & " Case When AttendanceStatus='Present' Then 'P'  " _
            '         & " When AttendanceStatus='Absent' Then 'A'  " _
            '         & " When AttendanceStatus='Leave' Then 'L'  " _
            '         & " When AttendanceStatus='Short Leave' Then 'SHL'  " _
            '         & " When AttendanceStatus='Half Leave' Then 'HL'  " _
            '         & " When AttendanceStatus='Casual Leave' Then 'CL' " _
            '         & " When AttendanceStatus='Anual Leave' Then 'AL' " _
            '         & " When AttendanceStatus='Sick Leave' Then 'A'   " _
            '         & " When AttendanceStatus='OD' Then 'OD' " _
            '         & " end as Status From tblattendanceDetail where (attendancetype ='In' Or attendancetype is null) AND (Convert(Varchar, AttendanceDate, 102) BETWEEN Convert(DateTime, '" & mFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & mToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
            '         & "  ) a where a.AttendanceId in (select Min(AttendanceId) as Ids From tblAttendanceDetail where (attendancetype ='In' Or attendancetype is null) AND (Convert(Varchar, AttendanceDate, 102) BETWEEN Convert(DateTime, '" & mFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & mToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By Convert(DateTime, AttendanceDate,108), AttendanceType,EmpId) ORDER BY 2 asc"
            ''TASK TFS3402 modified below query to get out time too.
            strSQL = "Select EmpId,  AttendanceDate, Left(Right(Convert(Varchar,[In],109),14),8) + ' ' + Right(Convert(Varchar,[In],109),2)  AS [In], CASE WHEN COUNT1 > 1 THEN Left(Right(Convert(Varchar,[Out],109),14),8) + ' ' + Right(Convert(Varchar,[Out],109),2) ELSE '' END AS [Out], Status From( " _
                     & " Select Min(AttendanceId) AS AttendanceId, tblattendanceDetail.EmpId, Replace(Convert(NVARCHAR, tblattendanceDetail.AttendanceDate, 106), ' ', '/')  as AttendanceDate,   MIN(AttendanceTime)  as [In], Max(OutTime.[Out]) AS [Out], " _
                     & " Case When AttendanceStatus='Present' Then 'P'  " _
                     & " When AttendanceStatus='Absent' Then 'A'  " _
                     & " When AttendanceStatus='Leave' Then 'L'  " _
                     & " When AttendanceStatus='Short Leave' Then 'SHL'  " _
                     & " When AttendanceStatus='Half Leave' Then 'HL'  " _
                     & " When AttendanceStatus='Casual Leave' Then 'CL' " _
                     & " When AttendanceStatus='Anual Leave' Then 'AL' " _
                     & " When AttendanceStatus='Sick Leave' Then 'A'   " _
                     & " When AttendanceStatus='OD' Then 'OD' " _
                     & " end as Status, Max(OutTime.COUNT1) AS COUNT1 From tblattendanceDetail INNER JOIN (SELECT  COUNT(AttendanceId) AS COUNT1, EmpId,  Replace(Convert(NVARCHAR, AttendanceDate, 106), ' ', '/')  as AttendanceDate,  Max(AttendanceTime) AS [Out] From tblAttendanceDetail WHERE (Convert(Varchar, AttendanceDate, 102) BETWEEN Convert(DateTime, '" & mFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & mToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By Convert(DateTime, AttendanceDate,108), EmpId HAVING  MAX(replace(convert(char(5),cast(Left(Right(Convert(Varchar,AttendanceTime,109),14),8) + ' ' + Right(Convert(Varchar,AttendanceTime,109),2) as datetime),108),':','')) = MAX(replace(convert(char(5),cast(Left(Right(Convert(Varchar,AttendanceTime,109),14),8) + ' ' + Right(Convert(Varchar,AttendanceTime,109),2) as datetime),108),':',''))) AS OutTime ON Replace(Convert(NVARCHAR, tblattendanceDetail.AttendanceDate, 106), ' ', '/') = OutTime.AttendanceDate AND tblattendanceDetail.EmpId = OutTime.EmpId WHERE  (Convert(Varchar, tblattendanceDetail.AttendanceDate, 102) BETWEEN Convert(DateTime, '" & mFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & mToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " Group By Convert(DateTime, tblattendanceDetail.AttendanceDate,108), tblattendanceDetail.EmpId, tblattendanceDetail.AttendanceStatus HAVING  MIN(replace(convert(char(5),cast(Left(Right(Convert(Varchar,AttendanceTime,109),14),8) + ' ' + Right(Convert(Varchar,AttendanceTime,109),2) as datetime),108),':','')) = MIN(replace(convert(char(5),cast(Left(Right(Convert(Varchar,AttendanceTime,109),14),8) + ' ' + Right(Convert(Varchar,AttendanceTime,109),2) as datetime),108),':',''))) a  ORDER BY 2 asc"

            dtData = GetDataTable(strSQL)
            Dim dr() As DataRow
            Dim j As Integer = 0
            For Each r As DataRow In dt.Rows
                dr = dtData.Select("EmpId=" & Val(r.Item("Employee_Id").ToString) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            ''TASK TFS3402
                            r(dt.Columns.IndexOf(drFound(1).ToString.Trim) + 1) = drFound(4).ToString
                            If drFound(2).ToString.Length > 0 Then
                                r(dt.Columns.IndexOf(drFound(1).ToString.Trim) + 2) = drFound(2).ToString
                            End If
                            If drFound(3).ToString.Length > 0 Then
                                r(dt.Columns.IndexOf(drFound(1).ToString.Trim) + 3) = drFound(3).ToString
                            End If
                            '' END TASK TFS3402
                            r.EndEdit()
                            j += 1
                        Next
                    End If
                End If
            Next

            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdSaved.RootTable.ColumnSetRowCount = 1
            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            ColumnSet1 = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet1.ColumnCount = 5
            ColumnSet1.Caption = "Employee Detail"
            ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet1.Add(Me.grdSaved.RootTable.Columns(enmEmp.Employee_Code), 0, 0)
            ColumnSet1.Add(Me.grdSaved.RootTable.Columns(enmEmp.Employee_Name), 0, 1)
            ColumnSet1.Add(Me.grdSaved.RootTable.Columns(enmEmp.EmployeeDesignationName), 0, 2)
            ColumnSet1.Add(Me.grdSaved.RootTable.Columns(enmEmp.EmployeeDeptName), 0, 3)
            ColumnSet1.Add(Me.grdSaved.RootTable.Columns(enmEmp.CostCentre), 0, 4)

            Me.grdSaved.RootTable.Columns(enmEmp.Employee_Code).Caption = "Emp Code"
            Me.grdSaved.RootTable.Columns(enmEmp.Employee_Name).Caption = "Name"
            Me.grdSaved.RootTable.Columns(enmEmp.EmployeeDesignationName).Caption = "Designation"
            Me.grdSaved.RootTable.Columns(enmEmp.EmployeeDeptName).Caption = "Department"

            For c As Integer = enmEmp.Count To Me.grdSaved.RootTable.Columns.Count - 4 Step 4
                Me.grdSaved.RootTable.Columns(c + 1).Caption = "Status"
                Me.grdSaved.RootTable.Columns(c + 2).Caption = "In"
                Me.grdSaved.RootTable.Columns(c + 3).Caption = "Out"

                Me.grdSaved.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                ColumnSet = Me.grdSaved.RootTable.ColumnSets.Add
                ColumnSet.ColumnCount = 3
                ColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSet.Caption = Me.grdSaved.RootTable.Columns(c).Caption
                ColumnSet.Add(Me.grdSaved.RootTable.Columns(c + 1), 0, 0)
                ColumnSet.Add(Me.grdSaved.RootTable.Columns(c + 2), 0, 1)
                ColumnSet.Add(Me.grdSaved.RootTable.Columns(c + 3), 0, 2)

            Next
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("Employees")
            Me.lstEmployee.DeSelect()
            FillCombos("Designation")
            Me.lstDesignation.DeSelect()
            FillCombos("Department")
            Me.lstDepartment.DeSelect()
            FillCombos("Division")
            Me.lstDivision.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstCity.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Monthly Employee Attendance"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

    Public Sub ApplyGridSettings1(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "[Employee Name] Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
