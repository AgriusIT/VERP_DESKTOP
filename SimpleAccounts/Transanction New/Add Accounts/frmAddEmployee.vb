Imports SBDal
Imports System.Data.SqlClient
Imports System
Public Class frmAddEmployee
    Dim strEmpCode As String = String.Empty

    Private Sub frmAddEmployee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            Button1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub AddEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            strEmpCode = GetNextDocNo("E", 5, "tblDefEmployee", "Employee_Code")
            FillDropDown(Me.cmbCity, "Select CityId, CityName From tblListCity")
            FillDropDown(Me.cmbDepartment, "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable")
            FillDropDown(Me.cmbDesignation, "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable")
            Me.txtFatherName.Text = String.Empty
            Me.txtName.Text = String.Empty
            Me.Text = strEmpCode
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim str As String = "INSERT INTO tblDefEmployee(Employee_Code, Employee_Name,  Father_Name, City_ID, Dept_ID,Desig_ID, Active) Values('" & GetNextDocNo("E", 5, "tblDefEmployee", "Employee_Code") & "', '" & Me.txtName.Text & "', '" & Me.txtFatherName.Text & "', " & Me.cmbCity.SelectedValue & " , " & Me.cmbDepartment.SelectedValue & ", " & cmbDesignation.SelectedValue & ", 1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Con.Close()
        End Try
    End Sub
End Class