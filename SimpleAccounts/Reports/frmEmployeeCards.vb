Imports CRUFLIDAutomation
Public Class frmEmployeeCards

    Private Sub frmRptEmployeeCards_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombo("Employee")
            FillCombo("City")
            FillCombo("Department")
            FillCombo("Designation")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "City" Then
                FillListBox(Me.lstCity.ListItem, "Select CityId, CityName From tblListCity")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "Select EmployeeDeptId,EmployeeDeptName From EmployeeDeptDefTable")
            ElseIf Condition = "Designation" Then
                FillListBox(Me.lstDesignation.ListItem, "Select EmployeeDesignationId,EmployeeDesignationName From EmployeeDesignationDefTable")
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbEmployee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmployee.SelectedIndexChanged
        Try
            If Me.cmbEmployee.SelectedIndex = -1 Then Exit Sub
            If Me.cmbEmployee.SelectedIndex > 0 Then
                Me.GroupBox1.Enabled = False
            Else
                Me.GroupBox1.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbEmployee.SelectedIndex
            FillCombo("Employee")
            Me.cmbEmployee.SelectedIndex = id

            FillCombo("City")
            FillCombo("Department")
            FillCombo("Designation")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try


            Dim strSQL As String = String.Empty
            strSQL = "SP_EmployeeInformation"

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Dim dv As New DataView
            dt.TableName = "dtEmployeeInformation"
            dv.Table = dt
            Dim strFilter As String = String.Empty
            If dt IsNot Nothing Then

                strFilter = " Employee_Code <> ''"
                If Not Me.cmbEmployee.SelectedIndex = -1 Then
                    If Me.cmbEmployee.SelectedIndex > 0 Then
                        strFilter += " AND Employee_Id=" & Me.cmbEmployee.SelectedValue & ""
                    End If
                End If

                If Me.GroupBox1.Enabled = True Then

                    If Me.lstCity.SelectedIDs.Length > 0 Then
                        strFilter += " AND City_Id in (" & Me.lstCity.SelectedIDs & ")"
                    End If

                    If Me.lstDepartment.SelectedIDs.Length > 0 Then
                        strFilter += " AND Dept_Id in (" & Me.lstDepartment.SelectedIDs & ")"
                    End If

                    If Me.lstDesignation.SelectedIDs.Length > 0 Then
                        strFilter += " AND Desig_Id in (" & Me.lstDesignation.SelectedIDs & ")"
                    End If
                End If

                dv.RowFilter = strFilter.ToString
            End If
            Dim dtData As DataTable = CType(dv.ToTable, DataTable)
            Dim objCRFU As New CRUFLIDAutomation.FontEncoder 'Create Object 
            For Each r As DataRow In dtData.Rows  'Loop 
                r.BeginEdit()
                r("BarCode") = objCRFU.Code128(r.Item("Employee_Code").ToString, 0)
                r.EndEdit()
            Next


            Dim frm As New frmEmployeeCardViewer
            frm._Dt = dtData
            frm.Show()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class