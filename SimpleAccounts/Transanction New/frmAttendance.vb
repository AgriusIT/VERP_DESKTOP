'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
Imports SBModel
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmAttendance

    Dim isOpenedForm As Boolean = False

    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Me.Close()
    '    Me.Cursor = Cursors.Default
    'End Sub
    'Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        FillGrid()
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try

    'End Sub

    'Private Sub FillGrid()
    '    Dim strSql As String
    '    grd.DataSource = Nothing
    '    If grd.RootTable.Columns.Contains("Status") = True Then grd.RootTable.Columns.Remove("Status")
    '    strSql = "Select top 1 Attendance_ID from tblAttendance where Attendance_Date='" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "'"
    '    Dim dtr As DataTable = GetRecords(strSql)
    '    Dim dr As DataRow
    '    dr = Nothing
    '    If dtr.Rows.Count > 0 Then
    '        dr = dtr.Rows(0)
    '    End If
    '    If dr Is Nothing Then
    '        txtID.Text = ""
    '        ''Save mode
    '        ', '' AS Status
    '        strSql = "SELECT Employee_ID, Employee_Name [Employee Name], null as Status_ID  FROM         dbo.tblDefEmployee WHERE     (Active = 1)"
    '        Dim dt As DataTable = GetRecords(strSql)
    '        If dt.Rows.Count > 0 Then
    '            grd.DataSource = dt
    '            GridFormating()
    '        End If
    '        btnSave.Text = "Save"
    '    Else
    '        txtID.Text = dr.Item(0)
    '        strSql = "SELECT     dbo.tblAttendance.Employee_ID, dbo.tblDefEmployee.Employee_Name AS [Employee Name], dbo.tblAttendance.Status_ID " _
    '                & " FROM         dbo.tblAttendance INNER JOIN  dbo.tblDefEmployee ON dbo.tblAttendance.Employee_ID = dbo.tblDefEmployee.Employee_ID" _
    '                & " WHERE     (dbo.tblAttendance.Attendance_Date = '" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "')"
    '        ''update mode
    '        Dim dt As DataTable = GetRecords(strSql)
    '        If dt.Rows.Count > 0 Then
    '            grd.DataSource = dt
    '            'AddColumnInGrid()
    '            GridFormating()
    '            LoadStatus()
    '            ' GridFormating()
    '        End If
    '        btnSave.Text = "Update"
    '    End If

    'End Sub
    'Private Sub GridFormating()
    '    'If grd.Rows.Count > 0 Then
    '    '    grd.AllowUserToAddRows = False

    '    '    grd.Columns(0).Visible = False
    '    '    grd.Columns(1).Width = 300
    '    '    grd.Columns(1).ReadOnly = True
    '    '    AddColumnInGrid()
    '    '    grd.Columns(2).Visible = False
    '    '    grd.Columns(3).Width = 150
    '    '    If grd.Rows.Count > 0 Then
    '    '        For i As Integer = 0 To grd.Rows.Count - 1
    '    '            grd.Rows(i).Cells(3).Value = 1
    '    '        Next
    '    '    End If
    '    'End If
    'End Sub
    'Private Sub AddColumnInGrid()
    '    Dim strSql As String
    '    Dim dcStatus As New DataGridViewComboBoxColumn
    '    With dcStatus
    '        .Name = "Status"
    '        .DataPropertyName = "Attendance_Status_ID"
    '        .HeaderText = "Status"
    '        strSql = "Select Attendance_Status_ID, Status_Description from tblAttendance_Status"
    '        Dim dt As DataTable = GetRecords(strSql)

    '        .DataSource = dt
    '        .ValueMember = "Attendance_Status_ID"
    '        .DisplayMember = "Status_Description"

    '    End With

    '    grd.RootTable.Columns.Add(dcStatus)
    'End Sub
    'Private Sub LoadStatus()
    '    For i As Integer = 0 To grd.Rows.Count - 1
    '        grd.Rows(i).Cells(3).Value = grd.Rows(i).Cells(2).Value

    '    Next
    'End Sub
    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
    '        If btnSave.Text = "Save" Then
    '            If Save() Then
    '                'sg_Information("Record save successfully")
    '                grd.DataSource = Nothing
    '                If grd.Columns.Contains("Status") = True Then grd.Columns.Remove("Status")
    '            End If
    '        Else
    '            If Update1() Then
    '                'msg_Information("Record update successfully")
    '                grd.DataSource = Nothing
    '                If grd.Columns.Contains("Status") = True Then grd.Columns.Remove("Status")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    'Private Function Save() As Boolean
    '    Me.lblProgress.Text = "Processing Please Wait ..."
    '    Me.lblProgress.BackColor = Color.LightYellow
    '    Me.lblProgress.Visible = True
    '    Application.DoEvents()
    '    Dim strSql As String
    '    Dim objCommand As New OleDb.OleDbCommand
    '    Dim objCon As OleDb.OleDbConnection
    '    objCon = Con
    '    If objCon.State = ConnectionState.Open Then objCon.Close()

    '    objCon.Open()
    '    objCommand.Connection = objCon

    '    Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
    '    Try
    '        objCommand.CommandType = CommandType.Text
    '        objCommand.Transaction = trans

    '        For i As Integer = 0 To grd.RowCount - 1
    '            strSql = "Insert into tblAttendance (Attendance_Date, Employee_ID, Status_ID) values( " _
    '            & " '" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "', " & grd.Rows(i).Cells(0).Value & ", " _
    '            & " " & grd.Rows(i).Cells(3).Value & ")"
    '            objCommand.CommandText = strSql
    '            objCommand.ExecuteNonQuery()
    '        Next

    '        trans.Commit()
    '        Save = True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Save = False
    '        ShowErrorMessage("An error occured while saving record" & ex.Message)
    '    Finally
    '        Me.lblProgress.Visible = False
    '    End Try

    'End Function
    'Private Function Update1() As Boolean
    '    Dim strSql As String
    '    Dim objCommand As New OleDb.OleDbCommand
    '    Dim objCon As OleDb.OleDbConnection
    '    objCon = Con
    '    If objCon.State = ConnectionState.Open Then objCon.Close()

    '    objCon.Open()
    '    objCommand.Connection = objCon

    '    Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
    '    Try
    '        objCommand.CommandType = CommandType.Text
    '        objCommand.Transaction = trans

    '        strSql = "Delete from tblAttendance where Attendance_Date = '" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "'"
    '        objCommand.CommandText = strSql
    '        objCommand.ExecuteNonQuery()

    '        strSql = ""
    '        For i As Integer = 0 To grd.RowCount - 1
    '            strSql = "Insert into tblAttendance (Attendance_Date, Employee_ID, Status_ID) values( " _
    '            & " '" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "', " & grd.Rows(i).Cells(0).Value & ", " _
    '            & " " & grd.Rows(i).Cells(3).Value & ")"
    '            objCommand.CommandText = strSql
    '            objCommand.ExecuteNonQuery()
    '        Next

    '        trans.Commit()
    '        Update1 = True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Update1 = False
    '        ShowErrorMessage("An error occured while saving record" & ex.Message)
    '    End Try
    'End Function

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub

    '        If btnSave.Text = "Update" Then
    '            Delete() 'hen msg_Information("Record has been deleted")
    '            grd.DataSource = Nothing
    '            If grd.Columns.Contains("Status") = True Then grd.Columns.Remove("Status")
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    'Private Function Delete() As Boolean
    '    Me.lblProgress.Text = "Processing Please Wait ..."
    '    Me.lblProgress.BackColor = Color.LightYellow
    '    Me.lblProgress.Visible = True
    '    Application.DoEvents()
    '    Dim strSql As String
    '    Dim objCommand As New OleDb.OleDbCommand
    '    Dim objCon As OleDb.OleDbConnection
    '    objCon = Con
    '    If objCon.State = ConnectionState.Open Then objCon.Close()

    '    objCon.Open()
    '    objCommand.Connection = objCon

    '    Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
    '    Try
    '        objCommand.CommandType = CommandType.Text
    '        objCommand.Transaction = trans

    '        strSql = "Delete from tblAttendance where Attendance_Date = '" & Format(dtpAttendance.Value.Date, "yyyy/MM/dd") & "'"
    '        objCommand.CommandText = strSql
    '        objCommand.ExecuteNonQuery()



    '        trans.Commit()
    '        Delete = True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Delete = False
    '        ShowErrorMessage("An error occured while saving record" & ex.Message)
    '    Finally
    '        Me.lblProgress.Visible = False
    '    End Try
    'End Function
    'Private Sub GetSecurityRights()
    '    Try
    '        If LoginGroup = "Administrator" Then
    '            Me.btnSave.Enabled = True
    '            Me.btnDelete.Enabled = True
    '            'Me.btnPrint.Enabled = True
    '            Exit Sub
    '        End If
    '        If getConfigValueByType("NewSecurityRights").ToString = "True" Then
    '            'Me.Visible = False
    '            Me.btnSave.Enabled = False
    '            Me.btnDelete.Enabled = False
    '            'Me.BtnPrint.Enabled = False
    '            'CtrlGrdBar1.mGridPrint.Enabled = False
    '            'CtrlGrdBar1.mGridExport.Enabled = False

    '            For i As Integer = 0 To Rights.Count - 1
    '                If Rights.Item(i).FormControlName = "View" Then
    '                    '       Me.Visible = True
    '                ElseIf Rights.Item(i).FormControlName = "Save" Then
    '                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
    '                ElseIf Rights.Item(i).FormControlName = "Update" Then
    '                    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
    '                ElseIf Rights.Item(i).FormControlName = "Delete" Then
    '                    Me.btnDelete.Enabled = True
    '                ElseIf Rights.Item(i).FormControlName = "Print" Then
    '                    'Me.BtnPrint.Enabled = True
    '                    'CtrlGrdBar1.mGridPrint.Enabled = True
    '                    'ElseIf Rights.Item(i).FormControlName = "Export" Then
    '                    'CtrlGrdBar1.mGridExport.Enabled = True
    '                    'ElseIf Rights.Item(i).FormControlName = "Post" Then
    '                    'me.chkPost.Visible = True
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub frmAttendance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.F4 Then
    '        btnSave_Click(Nothing, Nothing)

    '    End If
    '    If e.KeyCode = Keys.Delete Then
    '        btnDelete_Click(Nothing, Nothing)
    '    End If
    'End Sub

    'Private Sub frmAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'End Sub
    Enum enmGrid
        Employee_Id
        Employee_Name
        Employee_Code
        EmployeeDesignationName
        RegionName
        ZoneName
        BeltName
        Status
        AttendanceTime
        ShiftGroupId
    End Enum

    Public Sub Fillgrid()
        Try


            Dim strSQL As String = String.Empty

            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            strSQL = "SELECT a.Employee_Id, a.Employee_Name as Employee, a.Employee_Code as Code, a.EmployeeDesignationName as Designation, a.RegionName as Region,a.ZoneName as Zone, a.BeltName as Belt, 'Present' as Status, IsNull(a.ShiftStartTime,getDate()) as AttendanceTime, IsNull(a.ShiftGroupId,0) as ShiftGroupId From EmployeesView a Order By a.Employee_Name ASC"
            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
            strSQL = "SELECT a.Employee_Id, a.Employee_Name as Employee, a.Employee_Code as Code, a.EmployeeDesignationName as Designation, a.RegionName as Region,a.ZoneName as Zone, a.BeltName as Belt, 'Present' as Status, IsNull(a.ShiftStartTime,getDate()) as AttendanceTime, IsNull(a.ShiftGroupId,0) as ShiftGroupId From EmployeesView a where a.active = 1 Order By a.Employee_Name ASC"
            'Altered Against Task#2015060025 to only load Active Employees Ali Ansari

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grd.DataSource = dt

            Dim str() As String = {"Present", "Leave", "Absent"}
            Me.grd.RootTable.Columns("Status").ValueList.PopulateValueList(str)

            ApplyGridSettings()
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            Fillgrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> enmGrid.AttendanceTime AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrid.Status Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grd.RootTable.Columns("AttendanceTime").FormatString = "hh:mm:ss tt"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grd_UpdatingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles grd.UpdatingCell
        Try
            If e.Column.Index = enmGrid.Status Then
                If e.Value.ToString <> "Present" Then
                    Me.grd.GetRow.Cells("AttendanceTime").Value = DBNull.Value
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Ahmad Sharif : Added function for filling the combos on winform load event
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Designation" Then
                FillDropDown(Me.cmbDesignation, "select EmployeeDesignationId,EmployeeDesignationName from EmployeeDesignationDefTable")
            ElseIf Condition = "Department" Then
                FillDropDown(Me.cmbDepartment, "select EmployeeDeptId,EmployeeDeptName from EmployeeDeptDefTable")
            ElseIf Condition = "Region" Then
                FillDropDown(Me.cmbRegion, "select RegionId,RegionName from tblListRegion")
            ElseIf Condition = "Zone" Then
                FillDropDown(Me.cmbZone, "select ZoneId,ZoneName from tblListZone where RegionId='" & Me.cmbRegion.SelectedValue & "'")
            ElseIf Condition = "Belt" Then
                FillDropDown(Me.cmbBelt, "select BeltId,BeltName from tblListBelt where ZoneId='" & Me.cmbZone.SelectedValue & "'")
            ElseIf Condition = "City" Then
                FillDropDown(Me.cmbCity, "select CityId,CityName from tblListCity")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Ahmad Sharif : Added Form load event
    Private Sub frmProjectVisit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
            FillCombos("Designation")
            FillCombos("Region")
            FillCombos("Belt")
            FillCombos("Department")
            FillCombos("Zone")
            FillCombos("City")

            isOpenedForm = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Ahmad Sharif : Added combo box region selection change event
    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            If isOpenedForm = True Then
                FillCombos("Zone")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmAttendance)
            Me.btnSave.Enabled = False
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Save" Then
                    Me.btnSave.Enabled = True
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

    'Ahmad Sharif : Added combo box zone selection change event
    Private Sub cmbZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbZone.SelectedIndexChanged
        Try
            If isOpenedForm = True Then
                FillCombos("Belt")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Ahmad Sharif : Added combo box belt selection change event
    'Private Sub cmbBelt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBelt.SelectedIndexChanged

    '    Try
    '        fill()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    'Ahmad Sharif : Button Save Click event
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If grd.RowCount < 0 Then
            Exit Sub
        End If

        Try
            If Me.btnSave.Text = "Save" Then
                If Now.Date < Me.dtpAttendance.Value.Date Then
                    ShowErrorMessage("This date is not allowed for attendance")
                    dtpAttendance.Focus()
                    Exit Sub
                Else
                    Save()
                End If


            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Ahmad Sharif : Save attendance details into tblAttendanceDetail
    Private Sub Save()
        Dim str As String = String.Empty

        Dim cmd As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cmd.Connection = Con

        Try
            Dim row As Janus.Windows.GridEX.GridEXRow
            Dim i As Integer = 0I

            Dim empId As String = String.Empty
            Dim attDate As String = String.Empty
            Dim attType As String = String.Empty
            Dim attTime As String = String.Empty
            Dim attStatus As String = String.Empty
            Dim shiftId As String = String.Empty
            Dim auto As String = String.Empty
            Dim flex_in_time As String = String.Empty
            Dim flex_out_time As String = String.Empty
            Dim Sch_In_Time As String = String.Empty
            Dim Sch_Out_Time As String = String.Empty




            For i = 0 To Me.grd.RowCount - 1
                Me.grd.Row = i
                row = Me.grd.GetRow()
                empId = row.Cells("Employee_Id").Value
                attDate = Me.dtpAttendance.Value.ToString
                'attType = row.Cells("AttendanceType").Value
                attTime = row.Cells("AttendanceTime").Value
                attStatus = row.Cells("Status").Value
                'shiftId = row.Cells("ShiftId").Value
                'auto = row.Cells("Auto").Value
                'flex_in_time = row.Cells("Flexibility_In_Time").Value
                'flex_out_time = row.Cells("Flexibility_Out_Time").Value
                'Sch_In_Time = row.Cells("Sch_In_Time").Value
                'Sch_Out_Time = row.Cells("Sch_Out_Time").Value

                '    cmd.CommandText = "insert into tblAttendanceDetail(EmpId,AttendanceDate,AttendanceType,AttendanceTime, " _
                '& " AttendanceStatus,ShiftId,Auto,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) values( " _
                '& " N'" & empId.Replace("'", "''") & "'," _
                '& " N'" & attDate.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & attType.Replace("'", "''") & "'," _
                '& " N'" & attTime.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & attStatus.Replace("'", "''") & "'," _
                '& " N'" & shiftId.Replace("'", "''") & "'," _
                '& " N'" & auto.Replace("'", "''") & "'," _
                '& " N'" & flex_in_time.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & flex_out_time.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & Sch_In_Time.ToString("yyyy-M-d hh:mm:ss tt") & "'," _
                '& " N'" & Sch_Out_Time.ToString("yyyy-M-d hh:mm:ss tt") & "')"

            Next




        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Attendence"
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class