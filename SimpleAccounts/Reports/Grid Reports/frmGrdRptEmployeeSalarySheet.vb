Public Class frmGrdRptEmployeeSalarySheet
    Enum grdEnum
        Id
        Name
        Code
        Desig
        Dept
        Count
    End Enum
    Private Sub frmGrdRptEmployeeSalarySheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.dtpFromDate.Value = Me.dtpFromDate.Value.AddMonths(-1)
            FillBind()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillBind()
        Try

            Dim str As String = String.Empty
            str = "SELECT tblDefEmployee.Employee_Id, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Employee_Code, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
                  & " dbo.EmployeeDeptDefTable.EmployeeDeptName " _
                  & " FROM dbo.EmployeeDesignationDefTable LEFT OUTER JOIN " _
                  & " dbo.tblDefEmployee ON dbo.EmployeeDesignationDefTable.EmployeeDesignationId = dbo.tblDefEmployee.Desig_ID LEFT OUTER JOIN " _
                  & " dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId "
            Dim dtMain As DataTable = GetDataTable(str)
            str = "Select SalaryExpTypeId, SalaryExpType From SalaryExpenseType WHERE Active=1 ORDER BY SalaryExpTypeId, SalaryExpType "
            Dim dt As DataTable = GetDataTable(str)
            For Each r As DataRow In dt.Rows
                dtMain.Columns.Add(r(0), GetType(System.Int16), r(0))
                dtMain.Columns.Add(r(1), GetType(System.String))
            Next


            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtMain
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("Employee_Name").Caption = "Name"
            Me.grd.RootTable.Columns("Employee_Code").Caption = "Code"
            Me.grd.RootTable.Columns("EmployeeDesignationName").Caption = "Designation"
            Me.grd.RootTable.Columns("EmployeeDeptName").Caption = "Department"

            ApplyGridSetting()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAllRecords()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim str As String = String.Empty
            str = "SELECT SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, SUM(dbo.SalariesExpenseDetailTable.Earning) " _
            & " AS Earning, SUM(dbo.SalariesExpenseDetailTable.Deduction) AS Deduction " _
            & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
            & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId " _
            & " WHERE (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
            & " GROUP BY dbo.SalariesExpenseDetailTable.SalaryExpTypeId, SalariesExpenseMasterTable.EmployeeId"
            Dim dt As DataTable = GetDataTable(str)
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dt.Select("EmployeeId =" & r(grdEnum.Id))
                If Not dr Is Nothing Then
                    If dr.Length > 0 Then
                        For Each foundDr As DataRow In dr
                            r.BeginEdit()
                            r(dtGrd.Columns.IndexOf(foundDr(0)) + 1) = foundDr(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dtGrd.Columns.Add("NetPayable", GetType(System.Double))
            dtGrd.AcceptChanges()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            FillBind()
            GetAllRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grd.RootTable.Columns(grdEnum.Id).Visible = False
            For c As Integer = grdEnum.Count To Me.grd.RootTable.Columns.Count - 2 Step 2
                Me.grd.RootTable.Columns(c).Visible = False
                Me.grd.RootTable.Columns(c).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 1).AllowSort = False
            Next

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index < grdEnum.Count Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Monthly Target Achieved" & vbCrLf & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class