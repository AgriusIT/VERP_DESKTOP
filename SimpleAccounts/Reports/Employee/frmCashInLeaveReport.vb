''TASK:TFS994 Grid based report to show the remaining leaves and then convert them into cash. Done by Ameen on 19-07-2017
'Ali Faisal : TFS1202 : Improve the report effeciency to load the records in short time
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmCashInLeaveReport
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Enum CashInLeave
        EmployeeId
        EmployeeName
        EmployeeCode
        Designation
        Department
        CostCentre
        Salary
        AllocatedLeaves
        'Ali Faisal : TFS1202 : Add three columns to get employee wise attendance records
        AbsentDays
        LeavesDays
        PresentDays
        'Ali Faisal : TFS1202 : End
        Count
    End Enum
    Public Sub FillCombo()
        '#Task1248
        'Try
        '    FillUltraDropDown(Me.cmbDepartment, "Select Isnull(EmployeeDeptID,0) as EmployeeDeptID,  EmployeeDeptName As [Department], IsNull(SalaryExpAcID,0) as SalaryExpAcId From EmployeeDeptDefTable WHERE EmployeeDeptID in(Select Dept_ID From tblDefEmployee where Dept_Id <> 0)")
        '    Me.cmbDepartment.Rows(0).Activate()
        '    Me.cmbDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptID").Hidden = True
        '    Me.cmbDepartment.DisplayLayout.Bands(0).Columns("SalaryExpAcId").Hidden = True
        '    Me.cmbDepartment.DisplayLayout.Bands(0).Columns("Department").Width = 300

        '    'Me.cmbDepartment.DisplayLayout.Bands(0).Columns("EmployeeDeptName"). = True
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub frmCashInLeaveReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            RefreshDate()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ID As Integer = 0
        Try
            RefreshDate()
            '#Task1248
            'ID = Me.cmbDepartment.Value
            'FillCombo()
            'Me.cmbDepartment.Value = ID
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
    Private Sub RefreshDate()
        Try
            Me.dtpFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            Me.dtpTo.Value = Now
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = " Processing please wait..."
            Me.lblProgress.Visible = True
            Me.lblProgress.BackColor = Color.LightYellow
            Application.DoEvents()
            GetCashInLeave()
            Me.lblProgress.Visible = False
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : TFS1202 : Commented due to old and slow process of report
    'Public Sub GetCashInLeave()
    '    Dim DepartmentId As Integer = 0
    '    Try
    '        Dim dtFromDate As DateTime
    '        dtFromDate = dtpFrom.Value
    '        If Not cmbDepartment.ActiveRow Is Nothing Then
    '            DepartmentId = Me.cmbDepartment.Value
    '        End If
    '        Dim dtData As New DataTable
    '        Dim mydt As New DataTable
    '        dtData = GetDataTable("Select EmployeesView.Employee_ID As EmployeeId, EmployeesView.Employee_Name As EmployeeName, EmployeesView.Employee_Code As EmployeeCode, EmployeesView.EmployeeDesignationName As Designation, EmployeesView.EmployeeDeptName As Department, CostCentre.Name As CostCentre , IsNull(EmployeesView.Salary, 0) As Salary, IsNull(EmployeesView.leavesalloted, 0) As AllocatedLeaves FROM EmployeesView Inner Join tblDefEmployee ON EmployeesView.Employee_ID=tblDefEmployee.Employee_ID  Left Outer Join tblDefCostCenter As CostCentre ON EmployeesView.CostCentre = CostCentre.CostCenterID " & IIf(DepartmentId > 0, "Where tblDefEmployee.Dept_ID=" & DepartmentId & "", "") & " ")
    '        dtData.AcceptChanges()
    '        Dim i As Integer = 0I
    '        Dim tempValue As Integer = 0I
    '        Dim weekNo As Integer = 1
    '        Dim k As Integer = 0I
    '        While Me.dtpFrom.Value.AddDays(i).Date < Me.dtpTo.Value.Date
    '            Dim myDate As Date = Me.dtpFrom.Value.AddDays(i).Date
    '            Dim daysInMonth As Integer = Date.DaysInMonth(myDate.Year, myDate.Month)
    '            Dim lastDateOfMonth As Date = New Date(myDate.Year, myDate.Month, daysInMonth)
    '            Dim MName As String = myDate.ToString("MMM")
    '            Dim Year As Integer = myDate.Year
    '            Dim month_year As String = String.Empty
    '            month_year = MName & "-" & Year

    '            If i = 0 Then
    '                If Not dtData.Columns.Contains(Me.dtpFrom.Value.AddDays(i).Date) Then
    '                    dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFrom.Value.AddDays(i).Date & " | " & IIf(Me.dtpTo.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpTo.Value.Date) & ")", GetType(System.DateTime))
    '                    dtData.Columns.Add(k & "^" & "Leaves", GetType(System.Double))
    '                    dtData.Columns.Add(k & "^" & "Absents", GetType(System.Double))
    '                End If
    '            Else
    '                i = 0
    '                If Not dtData.Columns.Contains(Me.dtpFrom.Value.AddDays(i).Date) Then
    '                    dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFrom.Value.AddDays(i + 1).Date & " | " & IIf(Me.dtpTo.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpTo.Value.Date) & ")", GetType(System.DateTime))
    '                    dtData.Columns.Add(k & "^" & "Leaves", GetType(System.Double))
    '                    dtData.Columns.Add(k & "^" & "Absents", GetType(System.Double))
    '                End If
    '            End If

    '            i += 1
    '            k += 1
    '            Me.dtpFrom.Value = lastDateOfMonth.Date
    '        End While


    '        dtData.AcceptChanges()
    '        For Each r As DataRow In dtData.Rows
    '            r.BeginEdit()
    '            For c As Integer = CashInLeave.Count To dtData.Columns.Count - 3 Step 3
    '                r(c + 1) = 0
    '                r(c + 2) = 0
    '            Next
    '            r.EndEdit()
    '        Next
    '        dtData.AcceptChanges()
    '        Dim strTotalLeaves As String = String.Empty
    '        Dim strTotalAbsents As String = String.Empty
    '        For c As Integer = CashInLeave.Count To dtData.Columns.Count - 3 Step 3
    '            If strTotalLeaves.Length > 0 Then
    '                strTotalLeaves += "+" & "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
    '            Else
    '                strTotalLeaves = "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
    '            End If
    '            If strTotalAbsents.Length > 0 Then
    '                strTotalAbsents += "+" & "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
    '            Else
    '                strTotalAbsents = "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
    '            End If
    '        Next




    '        For Each dr As DataRow In dtData.Rows
    '            'Dim strTotalSales As String = String.Empty
    '            'Dim strTotalReceiving As String = String.Empty
    '            For c As Integer = CashInLeave.Count To dtData.Columns.Count - 3 Step 3

    '                'Dim mydates() As Object = dtData.Columns(c).ColumnName.ToString.Split("|")
    '                'Me.dtpFromDate.Value = mydates(0)
    '                'Me.dtpToDate.Value = mydates(1)

    '                'Task#10092015 split Dates from dtData data table by ahmad sharif

    '                Dim temp() As Object = dtData.Columns(c).ColumnName.ToString.Split("(")
    '                ''temp(0)  =   'Sep-2015
    '                ''temp(1)  =   ' 9/10/2015 | 9/30/2015 )
    '                Dim tempStr1 As String = String.Empty
    '                Dim tempStr2 As String = String.Empty
    '                tempStr1 = temp(0)      'Sep-2015
    '                tempStr2 = temp(1)      ' 9/10/2015 | 9/30/2015 )

    '                Dim temp2() As Object = tempStr2.Split("|")
    '                ''temp2(0) = Week-1 
    '                ''temp2(1) =   1/10/2015 
    '                Dim tempStr3 As String = String.Empty
    '                Dim tempStr4 As String = String.Empty
    '                tempStr3 = temp2(0)     '9/10/2015
    '                tempStr4 = temp2(1)     ' 9/30/2015 ) 

    '                Dim temp3() As Object = tempStr4.Split(")")
    '                Dim tempStr5 As String = String.Empty
    '                Dim tempStr6 As String = String.Empty
    '                tempStr5 = temp3(0)     ' 9/30/2015
    '                tempStr6 = temp3(1)     ' ""

    '                Me.dtpFrom.Value = CDate(tempStr3)
    '                Me.dtpTo.Value = CDate(tempStr5)

    '                'Here is Query Of Receiving/Recovery 
    '                Dim dtLeaves As DataTable = GetDataTable("Select IsNull(EmpId, 0) As EmpId, AbsentDays, LeavesDays, PresentDays From FuncCashInLeave('" & CDate(tempStr3).ToString("yyyy-M-d 00:00:00") & "', '" & CDate(tempStr5).ToString("yyyy-M-d 23:59:59") & "', " & dr.Item("EmployeeId") & ")")
    '                dtLeaves.AcceptChanges()

    '                If dtLeaves.Rows.Count > 0 Then
    '                    dr.Item(c + 1) = Val(dtLeaves.Rows(0).Item("LeavesDays").ToString)
    '                    dr.Item(c + 2) = Val(dtLeaves.Rows(0).Item("AbsentDays").ToString)
    '                End If
    '            Next
    '        Next
    '        Me.dtpFrom.Value = dtFromDate
    '        dtData.AcceptChanges()
    '        dtData.Columns.Add("Availed Leaves", GetType(System.Double))
    '        dtData.Columns.Add("Total Absents", GetType(System.Double))
    '        dtData.Columns.Add("Balance Leaves", GetType(System.Double))
    '        dtData.Columns.Add("Cash In Leaves", GetType(System.Double))
    '        dtData.Columns("Availed Leaves").Expression = strTotalLeaves.ToString
    '        dtData.Columns("Total Absents").Expression = strTotalAbsents.ToString
    '        dtData.Columns("Balance Leaves").Expression = "[AllocatedLeaves]-[Availed Leaves]"
    '        dtData.Columns("Cash In Leaves").Expression = "[Salary]/30*[Balance Leaves]"


    '        dtData.AcceptChanges()
    '        Me.grdCashInLeave.DataSource = dtData
    '        Me.grdCashInLeave.RetrieveStructure()
    '        grdCashInLeave.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    '        Me.grdCashInLeave.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
    '        Me.grdCashInLeave.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
    '        Me.grdCashInLeave.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
    '        Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
    '        Me.grdCashInLeave.RootTable.ColumnSetRowCount = 1
    '        ColumnSet = Me.grdCashInLeave.RootTable.ColumnSets.Add
    '        ColumnSet.ColumnCount = 7
    '        ColumnSet.Caption = "Employee Detail"
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("EmployeeName"), 0, 0)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("EmployeeCode"), 0, 1)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Designation"), 0, 2)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Department"), 0, 3)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("CostCentre"), 0, 4)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Salary"), 0, 5)
    '        ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("AllocatedLeaves"), 0, 6)
    '        Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
    '        For c As Integer = CashInLeave.Count To Me.grdCashInLeave.RootTable.Columns.Count - 3 Step 3
    '            If Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Availed Leaves" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Total Absents" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Balance Leaves" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Cash In Leaves" Then
    '                Me.grdCashInLeave.RootTable.Columns(c + 1).Caption = "Leaves"
    '                Me.grdCashInLeave.RootTable.Columns(c + 2).Caption = "Absents"
    '                ColumnSet1 = Me.grdCashInLeave.RootTable.ColumnSets.Add
    '                ColumnSet1.ColumnCount = 2
    '                ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '                ColumnSet1.Caption = Me.grdCashInLeave.RootTable.Columns(c).Caption
    '                ColumnSet1.Add(Me.grdCashInLeave.RootTable.Columns(c + 1), 0, 0)
    '                ColumnSet1.Add(Me.grdCashInLeave.RootTable.Columns(c + 2), 0, 1)
    '                grdCashInLeave.RootTable.Columns(c).FormatString = "dd/MMM/yyyy"
    '                'Me.grdCashInLeave.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '                'Me.grdCashInLeave.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '                Me.grdCashInLeave.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '                Me.grdCashInLeave.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '                Me.grdCashInLeave.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '                Me.grdCashInLeave.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '                Me.grdCashInLeave.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
    '                Me.grdCashInLeave.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
    '                'Me.grdCashInLeave.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
    '                'Me.grdCashInLeave.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
    '            End If
    '        Next

    '        'dtData.Columns.Add("Availed Leaves", GetType(System.Double))
    '        'dtData.Columns.Add("Total Absents", GetType(System.Double))
    '        'dtData.Columns.Add("Balance Leaves", GetType(System.Double))
    '        'dtData.Columns.Add("Cash In Leaves", GetType(System.Double))



    '        Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
    '        Me.grdCashInLeave.RootTable.ColumnSetRowCount = 1
    '        ColumnSet2 = Me.grdCashInLeave.RootTable.ColumnSets.Add
    '        ColumnSet2.ColumnCount = 4
    '        ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Availed Leaves"), 0, 0)
    '        ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Total Absents"), 0, 1)
    '        ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Balance Leaves"), 0, 2)
    '        ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Cash In Leaves"), 0, 3)
    '        Me.grdCashInLeave.RootTable.Columns("Salary").FormatString = "N"
    '        Me.grdCashInLeave.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdCashInLeave.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grdCashInLeave.RootTable.Columns("Availed Leaves").FormatString = "N" & DecimalPointInValue
    '        Me.grdCashInLeave.RootTable.Columns("Balance Leaves").FormatString = "N" & DecimalPointInValue
    '        Me.grdCashInLeave.RootTable.Columns("Cash In Leaves").FormatString = "N" & DecimalPointInValue
    '        Me.grdCashInLeave.RootTable.Columns("Total Absents").FormatString = "N" & DecimalPointInValue
    '        'Me.grdCashInLeave.FrozenColumns = 1

    '        'ColumnSet.


    '        'Me.grdCashInLeave.RootTable.Columns("Salary"). = "N"

    '        CtrlGrdBar1_Load(Nothing, Nothing)
    '        Me.grdCashInLeave.AutoSizeColumns()
    '        'Me.grdReport.Width = 100
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Ali Faisal : TFS1202 : End
    ''' <summary>
    ''' Ali Faisal : TFS1202 : Add method to get the attendance records in short time and effeciently
    ''' </summary>
    ''' <remarks>'Ali Faisal : TFS1202 : 06-Sep-2017</remarks>
    Public Sub GetCashInLeave()
        Dim DepartmentId As Integer = 0
        Try
            Dim dtFromDate As DateTime
            dtFromDate = dtpFrom.Value
            'Task1248
            'If Not cmbDepartment.ActiveRow Is Nothing Then
            '    DepartmentId = Me.cmbDepartment.Value
            'End If
            Dim dtData As New DataTable
            'Ali Faisal : TFS1202 : Get the records from procedure
            Dim str As String = ""
            'Ayesha Rehman : TFS3418 : Changing Sp Parameters
            str = "SP_CashInLeaves  '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & Me.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59") & "'," & IIf(Me.lstEmpDesignation.SelectedIDs.Length > 0, "'" & Me.lstEmpDesignation.SelectedIDs & "'", "Null") & "," & IIf(Me.lstEmpDepartment.SelectedIDs.Length > 0, "'" & Me.lstEmpDepartment.SelectedIDs & "'", "Null") & "," & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, "'" & Me.lstCostCenter.SelectedIDs & "'", "Null") & "," & IIf(Me.lstEmpCity.SelectedIDs.Length > 0, "'" & Me.lstEmpCity.SelectedIDs & "'", "Null") & "," & IIf(Me.lstEmpShiftGroup.SelectedIDs.Length > 0, "'" & Me.lstEmpShiftGroup.SelectedIDs & "'", "Null") & "," & IIf(Me.lstEmployee.SelectedIDs.Length > 0, "'" & Me.lstEmployee.SelectedIDs & "'", "Null") & ""
            dtData = GetDataTable(str)
            'Ali Faisal : TFS1202 : End
            dtData.AcceptChanges()
            Dim i As Integer = 0I
            Dim k As Integer = 0I
            While Me.dtpFrom.Value.AddDays(i).Date < Me.dtpTo.Value.Date
                Dim myDate As Date = Me.dtpFrom.Value.AddDays(i).Date
                'Ali Faisal : TFS2135 : Date from issue in month from martch and onward
                If myDate.Day <> 1 Then
                    myDate = myDate.AddDays(1 - i)
                End If
                'Ali Faisal : TFS2135 : End
                Dim daysInMonth As Integer = Date.DaysInMonth(myDate.Year, myDate.Month)
                Dim lastDateOfMonth As Date = New Date(myDate.Year, myDate.Month, daysInMonth)
                Dim MName As String = myDate.ToString("MMM")
                Dim Year As Integer = myDate.Year
                Dim month_year As String = String.Empty
                month_year = MName & "-" & Year
                'Ali Faisal : TFS2135 : Date from issue in month from martch and onward
                Me.dtpFrom.Value = myDate
                'Ali Faisal : TFS2135 : End
                If i = 0 Then
                    If Not dtData.Columns.Contains(Me.dtpFrom.Value.AddDays(i).Date) Then
                        'Ali Faisal : TFS1202 : Date formate
                        'Ali Faisal : TFS2135 : Date from issue in month from martch and onward
                        dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFrom.Value.ToString("dd-MMM-yyyy") & " | " & IIf(Me.dtpTo.Value.Date > lastDateOfMonth, lastDateOfMonth.ToString("dd-MMM-yyyy"), Me.dtpTo.Value.Date.ToString("dd-MMM-yyyy")) & ")", GetType(System.DateTime))
                        'Ali Faisal : TFS2135 : End
                        'Ali Faisal : TFS1202 : End
                        dtData.Columns.Add(k & "^" & "Leaves", GetType(System.Double))
                        dtData.Columns.Add(k & "^" & "Absents", GetType(System.Double))
                    End If
                Else
                    If Not dtData.Columns.Contains(Me.dtpFrom.Value.AddDays(i).Date) Then
                        'Ali Faisal : TFS1202 : Date formate
                        'Ali Faisal : TFS2135 : Date from issue in month from martch and onward
                        dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFrom.Value.ToString("dd-MMM-yyyy") & " | " & IIf(Me.dtpTo.Value.Date > lastDateOfMonth, lastDateOfMonth.ToString("dd-MMM-yyyy"), Me.dtpTo.Value.Date.ToString("dd-MMM-yyyy")) & ")", GetType(System.DateTime))
                        'Ali Faisal : TFS2135 : End
                        'Ali Faisal : TFS1202 : End
                        dtData.Columns.Add(k & "^" & "Leaves", GetType(System.Double))
                        dtData.Columns.Add(k & "^" & "Absents", GetType(System.Double))
                    End If
                End If
                dtData.AcceptChanges()
                'Ali Faisal : TFS1202 : Get attendance records from procedure
                Dim dt As DataTable
                Dim str1 As String = ""
                'Ali Faisal : TFS2135 : Date from issue in month from martch and onward
                str1 = "SP_CashInLeavesRecords  '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & IIf(Me.dtpTo.Value.Date > lastDateOfMonth, lastDateOfMonth.ToString("yyyy-MM-dd 23:59:59"), Me.dtpTo.Value.Date.ToString("yyyy-MM-dd 23:59:59")) & "'"
                'Ali Faisal : TFS2135 : End
                dt = GetDataTable(str1)
                For Each dr As DataRow In dtData.Rows
                    For Each dr1 As DataRow In dt.Rows
                        If dr.Item("EmployeeId") = dr1.Item("EmpId") Then
                            dr.BeginEdit()
                            For c As Integer = CashInLeave.Count To dtData.Columns.Count - 3 Step 3
                                If dt.Rows.Count > 0 Then
                                    If dtData.Columns.Contains(k & "^" & "Leaves") Then
                                        dr.Item(k & "^" & "Leaves") = Val(dr1.Item("LeavesDays").ToString)
                                    End If
                                    If dtData.Columns.Contains(k & "^" & "Absents") Then
                                        dr.Item(k & "^" & "Absents") = Val(dr1.Item("AbsentDays").ToString)
                                    End If
                                End If
                            Next
                            dr.EndEdit()
                        End If
                    Next
                Next
                i += 1
                k += 1
                Me.dtpFrom.Value = lastDateOfMonth.Date
            End While
            dtData.Columns.Add("Availed Leaves", GetType(System.Double))
            dtData.Columns.Add("Total Absents", GetType(System.Double))
            dtData.Columns.Add("Balance Leaves", GetType(System.Double))
            dtData.Columns.Add("Cash In Leaves", GetType(System.Double))
            dtData.Columns("Balance Leaves").Expression = "[AllocatedLeaves]-[Availed Leaves]"
            dtData.Columns("Cash In Leaves").Expression = "[Salary]/30*[Balance Leaves]"
            For Each dr As DataRow In dtData.Rows
                dr.Item("Availed Leaves") = Val(dr.Item("LeavesDays"))
                dr.Item("Total Absents") = Val(dr.Item("AbsentDays"))
            Next
            Me.dtpFrom.Value = dtFromDate
            dtData.AcceptChanges()
            Me.grdCashInLeave.DataSource = dtData
            Me.grdCashInLeave.RetrieveStructure()

            'Ali Faisal : TFS1202 : Column set as the header for specific columns in grid
            Me.grdCashInLeave.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdCashInLeave.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdCashInLeave.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 7
            ColumnSet.Caption = "Employee Detail"
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("EmployeeName"), 0, 0)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("EmployeeCode"), 0, 1)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Designation"), 0, 2)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Department"), 0, 3)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("CostCentre"), 0, 4)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("Salary"), 0, 5)
            ColumnSet.Add(Me.grdCashInLeave.RootTable.Columns("AllocatedLeaves"), 0, 6)
            'Ali Faisal : TFS1202 : Column set as the header for specific columns in grid
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            For c As Integer = CashInLeave.Count To Me.grdCashInLeave.RootTable.Columns.Count - 3 Step 3
                If Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Availed Leaves" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Total Absents" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Balance Leaves" AndAlso Me.grdCashInLeave.RootTable.Columns(c).DataMember <> "Cash In Leaves" Then
                    Me.grdCashInLeave.RootTable.Columns(c + 1).Caption = "Leaves"
                    Me.grdCashInLeave.RootTable.Columns(c + 2).Caption = "Absents"
                    ColumnSet1 = Me.grdCashInLeave.RootTable.ColumnSets.Add
                    ColumnSet1.ColumnCount = 2
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet1.Caption = Me.grdCashInLeave.RootTable.Columns(c).Caption
                    ColumnSet1.Add(Me.grdCashInLeave.RootTable.Columns(c + 1), 0, 0)
                    ColumnSet1.Add(Me.grdCashInLeave.RootTable.Columns(c + 2), 0, 1)
                    grdCashInLeave.RootTable.Columns(c).FormatString = "dd/MMM/yyyy"
                    Me.grdCashInLeave.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdCashInLeave.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdCashInLeave.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdCashInLeave.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdCashInLeave.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                    Me.grdCashInLeave.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                End If
            Next

            'Ali Faisal : TFS1202 : Column set as the header for specific columns in grid
            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdCashInLeave.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdCashInLeave.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 4
            ColumnSet2.Caption = "Cash In Leaves Detail"
            ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Availed Leaves"), 0, 0)
            ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Total Absents"), 0, 1)
            ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Balance Leaves"), 0, 2)
            ColumnSet2.Add(Me.grdCashInLeave.RootTable.Columns("Cash In Leaves"), 0, 3)

            grdCashInLeave.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdCashInLeave.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdCashInLeave.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grdCashInLeave.RootTable.Columns("Salary").FormatString = "N"
            Me.grdCashInLeave.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCashInLeave.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCashInLeave.RootTable.Columns("Availed Leaves").FormatString = "N" & DecimalPointInValue
            Me.grdCashInLeave.RootTable.Columns("Balance Leaves").FormatString = "N" & DecimalPointInValue
            Me.grdCashInLeave.RootTable.Columns("Cash In Leaves").FormatString = "N" & DecimalPointInValue
            Me.grdCashInLeave.RootTable.Columns("Total Absents").FormatString = "N" & DecimalPointInValue
            'Ali Faisal : TFS1202 : Hide unnecessary columns
            Me.grdCashInLeave.RootTable.Columns("PresentDays").Visible = False
            Me.grdCashInLeave.RootTable.Columns("AbsentDays").Visible = False
            Me.grdCashInLeave.RootTable.Columns("LeavesDays").Visible = False
            Me.grdCashInLeave.RootTable.Columns("EmployeeId").Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdCashInLeave.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Ali Faisal : TFS1202 : End

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            RefreshDate()
            'Task1248
            'If Not Me.cmbDepartment.ActiveRow Is Nothing Then
            '    Me.cmbDepartment.Rows(0).Activate()
            'End If
            'Ali Faisal : TFS1202 : Clear grid at new button event
            Me.grdCashInLeave.DataSource = Nothing
            'Ali Faisal : TFS1202 : End
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCashInLeave.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCashInLeave.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdCashInLeave.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "")
        Try
            If LoginGroup = "Administrator" Then
                Me.btnNew.Enabled = True
                Me.btnShow.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    CtrlGrdBar1.mGridExport.Enabled = True
                    CtrlGrdBar1.mGridPrint.Enabled = True
                    Exit Sub
                End If
                'Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                'If Not dt Is Nothing Then
                '    If Not dt.Rows.Count = 0 Then
                '        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                '            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                '        Else
                '            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                '        End If
                '        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                '        Me.btnDelete1.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                '        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                '        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                '    End If
                'End If
            Else
                Me.btnNew.Enabled = False
                Me.btnShow.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                IsCrystalReportExport = False
                IsCrystalReportPrint = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        'Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Export" Then
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity1(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

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