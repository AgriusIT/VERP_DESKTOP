'Ali Faisal : TFS1250 : Add report to show the list of those employees who's CNIC is expired
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmEmployeeCNICExpiry
    Implements IGeneral
    Dim _SearchDt As New DataTable
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        EmployeeId
        EmpCode
        EmpName
        DepartmentId
        Department
        DesignationId
        Designation
        CostCenterId
        CostCenter
        ShiftGroupId
        CityId
        CostCenterGroup
        CNICNo
        ExpiryDate
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.EmployeeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DepartmentId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DesignationId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenterId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CityId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ShiftGroupId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenterGroup).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ExpiryDate).FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Department" Then
                FillListBox(Me.lstEmpDepartment.ListItem, "SELECT EmployeeDeptId, EmployeeDeptName FROM EmployeeDeptDefTable WHERE Active = 1")
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstEmpDesignation.ListItem, "SELECT EmployeeDesignationId, EmployeeDesignationName FROM EmployeeDesignationDefTable WHERE Active = 1")
            ElseIf Condition = "ShiftGroup" Then
                FillListBox(Me.lstEmpShiftGroup.ListItem, "SELECT ShiftGroupId, ShiftGroupName FROM ShiftGroupTable WHERE Active = 1")
            ElseIf Condition = "City" Then
                FillListBox(Me.lstEmpCity.ListItem, "SELECT CityId, CityName FROM tblListCity WHERE Active = 1")
            ElseIf Condition = "Employee" Then
                FillListBox(Me.lstEmployee.ListItem, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1")
            ElseIf Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "CostCenter" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name  FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name  FROM tblDefCostCenter WHERE Active=1 ")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            str = "SELECT tblDefEmployee.Employee_ID AS EmployeeId, tblDefEmployee.Employee_Code AS [Emp Code], tblDefEmployee.Employee_Name AS [Emp Name], tblDefEmployee.Dept_ID AS DepartmentId, EmployeeDeptDefTable.EmployeeDeptName AS Department, tblDefEmployee.Desig_ID AS DesignationId, EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, tblDefEmployee.CostCentre AS CostCenterId, tblDefCostCenter.Name AS [Cost Center], tblDefEmployee.ShiftGroupId, tblDefEmployee.City_ID AS CityId, tblDefCostCenter.CostCenterGroup, tblDefEmployee.NIC AS [CNIC No], tblDefEmployee.CNICExpiryDate AS [Expiry Date] " _
                & "FROM tblDefEmployee LEFT OUTER JOIN tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId " _
                & "WHERE tblDefEmployee.Active = 1 AND tblDefEmployee.CNICExpiryDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"

            If Me.lstEmpDepartment.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.Dept_ID IN (" & Me.lstEmpDepartment.SelectedIDs & ")"
            End If
            If Me.lstEmpDesignation.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.Desig_ID IN (" & Me.lstEmpDesignation.SelectedIDs & ")"
            End If
            If Me.lstEmpShiftGroup.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.ShiftGroupId IN (" & Me.lstEmpShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstEmpCity.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.City_ID IN (" & Me.lstEmpCity.SelectedIDs & ")"
            End If
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.Employee_ID IN (" & Me.lstEmployee.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND tblDefEmployee.CostCentre IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If

            Dim dt As DataTable = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            Me.txtSearch.Text = ""
            FillCombos("Department")
            Me.lstEmpDepartment.DeSelect()
            FillCombos("Designation")
            Me.lstEmpDesignation.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstEmpShiftGroup.DeSelect()
            FillCombos("City")
            Me.lstEmpCity.DeSelect()
            FillCombos("Employee")
            Me.lstEmployee.DeSelect()
            FillCombos("HeadCostCenter")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCenter")
            Me.lstCostCenter.DeSelect()
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

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
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee CNIC Expiry Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployeeNoOfHits_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1250 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@DepartmentIds", Me.lstEmpDepartment.SelectedIDs)
            AddRptParam("@DesignationIds", Me.lstEmpDesignation.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstEmpShiftGroup.SelectedIDs)
            AddRptParam("@CityIds", Me.lstEmpCity.SelectedIDs)
            AddRptParam("@EmployeeIds", Me.lstEmployee.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            ShowReport("rptEmployeeCNICExpiry")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Search employee list by name or code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                '' FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "')") ''TFS3320
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') ")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class