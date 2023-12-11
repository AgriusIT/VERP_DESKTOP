Public Class frmReturnStoreIssuenceReport
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "Employees" Then
                str = "Select Employee_Id, Employee_Name ,Father_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department, ShiftGroupName as Shift, IsNull(ShiftGroupId,0) as ShiftGroupId , IsNull(Salary,0) as Salary, IsNull(EmpSalaryAccountId,0) as EmpSalaryAccountId From EmployeesView WHERE Active=1"
                FillUltraDropDown(Me.cmbEmployee, str, True)
                Me.cmbEmployee.Rows(0).Activate()

                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_Id").Hidden = True
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("EmpSalaryAccountId").Hidden = True
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Shift").Hidden = True
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Salary").Hidden = True
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_Name").Header.Caption = "Employee Name"
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Father_Name").Header.Caption = "Father Name"

            ElseIf Condition = "Location" Then
                str = "Select location_id , location_code As Location From tblDefLocation"
                FillDropDown(Me.cmbLocation, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Try
            'AddRptParam("@EmployeeID", Me.cmbEmployee.Value)
            'AddRptParam("@LocationID", Me.cmbLocation.SelectedIndex)
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptReturnStoreIssuenceSummary", "{sp_Return_Store_Issuence_Summary;1.ArticleDescription} <> '' " & IIf(Me.cmbEmployee.Value > 0, " AND {sp_Return_Store_Issuence_Summary;1.Employee_ID} = " & Me.cmbEmployee.Value & "", "") & "  " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND {sp_Return_Store_Issuence_Summary;1.location_id} = " & Me.cmbLocation.SelectedIndex & "", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmReturnStoreIssuenceReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Employees")
            FillCombos("Location")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class