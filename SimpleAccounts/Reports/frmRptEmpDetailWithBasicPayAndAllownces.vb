Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmRptEmpDetailWithBasicPayAndAllownces
    Dim _SearchDt As New DataTable
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If Me.cmbEmp.Value < 1 Then
                ShowErrorMessage("Please Select Employee")
                Exit Sub
            End If
            GetCrystalReportRights()

            'Dim strFilter As String = String.Empty

            'strFilter = " {SP_SalarySheetDetail;1.Employee_Name} <> ''"



            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@DesignationsIds", Me.lstDesignation.SelectedIDs)
            AddRptParam("@DepartmentIds", Me.lstDepartment.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@CityIds", Me.lstCity.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstShiftGroup.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstShiftGroup.SelectedIDs)
            AddRptParam("@EmployeeId", Me.cmbEmp.Value)
            ShowReport("rptEmpBasicPayAndAllownces")

            '#task1248
            'If Me.cmbDepartment.SelectedIndex > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.Dept_Id} =" & Me.cmbDepartment.SelectedValue & ""
            'End If
            'If Me.cmbEmployee.SelectedIndex > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.Employee_Id}=" & Me.cmbEmployee.SelectedValue & ""

            'End If
            'If Me.cmbBank.SelectedIndex > 0 AndAlso Me.cmbBank.Text.Length > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.Bank_Ac_Name}='" & Me.cmbBank.Text.Replace("'", "''") & "'"
            'End If
            'If Me.cmbCostCentre.SelectedValue > 0 Then
            '    strFilter += " AND {SP_SalarySheetDetail;1.CostCentre}=" & Me.cmbCostCentre.SelectedValue & ""
            'End If

            'ShowReport("rptEmpSalarySheetDetail", strFilter.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
         
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
          
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnShow.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Public Sub FillCombos(Optional Condition As String = "")
        Try
            If Condition = "Employees" Then
                'FillListBox(Me.cmbEmployee, "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1")
                FillUltraDropDown(Me.cmbEmp, "SELECT Employee_Id, Employee_Name FROM tblDefEmployee ")
                Me.cmbEmp.Rows(0).Activate()
                Me.cmbEmp.DisplayLayout.Bands(0).Columns("Employee_Id").Hidden = True
                Me.cmbEmp.DisplayLayout.Bands(0).Columns("Employee_Name").Width = 200
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstDesignation.ListItem, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable WHERE Active=1 Order By 2 Asc")
            ElseIf Condition = "CostCentre" Then
                ' FillListBox(Me.lstCostCenter.ListItem, "SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 ") ''TFS3320
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
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Sub frmRptEmpDetailWithBasicPayAndAllownces_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            FillCombos("Employees")
            FillCombos("Designation")
            Me.lstDesignation.DeSelect()
            FillCombos("Department")
            Me.lstDepartment.DeSelect()
            FillCombos("ShiftGroup")
            Me.lstShiftGroup.DeSelect()
            FillCombos("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombos("CostCentre")
            Me.lstCostCenter.DeSelect()
            FillCombos("City")
            Me.lstCity.DeSelect()
            '_SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            '_SearchDt.AcceptChanges()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim str As String = ""
            str = "SELECT     Emp.Employee_ID, Emp.Dept_ID, Emp.Desig_ID, Dept.EmployeeDeptName, Desig.EmployeeDesignationName, Emp.Employee_Name, " _
                     & " Emp.Employee_Code, Emp.Father_Name, Emp.NIC,Emp.Salary , Emp.Bank_Ac_Name, Emp.BankAccount_No, CostCenter.Name As CostCenterName , Shift.ShiftGroupName As ShiftName , City.CityName As CityName " _
                     & " FROM        dbo.tblDefEmployee AS Emp LEFT OUTER JOIN " _
                     & " dbo.EmployeeDeptDefTable AS Dept ON Emp.Dept_ID = Dept.EmployeeDeptId LEFT OUTER JOIN " _
                     & " dbo.EmployeeDesignationDefTable AS Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
                     & " dbo.tblDefCostCenter As CostCenter ON Emp.CostCentre  = CostCenter.CostCenterID LEFT OUTER JOIN " _
                     & " dbo.tblListCity As City ON emp.City_ID  = City.CityId LEFT OUTER JOIN " _
                     & " ShiftGroupTable As Shift on Emp.ShiftGroupId = Shift.ShiftGroupId  " _
                     & "     WHERE  Emp.Employee_ID = " & cmbEmp.Value & "  "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND CostCenter.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstDepartment.SelectedIDs.Length > 0 Then
                str += " AND Dept.EmployeeDeptId IN (" & Me.lstDepartment.SelectedIDs & ")"
            End If
            If Me.lstDesignation.SelectedIDs.Length > 0 Then
                str += " AND CostCenter.CostCenterID IN (" & Me.lstDesignation.SelectedIDs & ")"
            End If
            If Me.lstShiftGroup.SelectedIDs.Length > 0 Then
                str += " AND Shift.ShiftGroupId IN (" & Me.lstShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                str += " AND City.CityId IN (" & Me.lstCity.SelectedIDs & ")"
            End If
           
            Dim dt As DataTable = GetDataTable(str)
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class