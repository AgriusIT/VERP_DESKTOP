Imports System.Data.oledb
Imports System.Math
Public Class frmDefEmployeeOld

    Private Sub frmEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCombo()
        RefreshControls()
        DisplayRecord()


    End Sub
    Private Sub DisplayRecord()
        Dim str As String
        str = "SELECT     Emp.Employee_ID, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
              & " Emp.Religion, Emp.DOB, City.CityName, Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date, Dept.EmployeeDeptName, " _
              & " Desig.EmployeeDesignationName, Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments " _
              & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
              & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
              & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN " _
              & " dbo.EmployeeDeptDefTable Dept ON Emp.Dept_ID = Dept.EmployeeDeptId"

        FillGrid(grdSaved, str)
        For i As Integer = 0 To grdSaved.Columns.Count - 1
            grdSaved.Columns(i).Visible = False
        Next
        If Not grdSaved.RowCount > 0 Then Exit Sub
        grdSaved.Columns(1).Visible = True
        grdSaved.Columns(2).Visible = True
        grdSaved.Columns(3).Visible = True
        grdSaved.Columns(4).Visible = True
        grdSaved.Columns(18).Visible = True
        grdSaved.Columns(10).Visible = True
        grdSaved.Columns(6).Visible = True

        grdSaved.Columns(1).HeaderText = "Name"
        grdSaved.Columns(2).HeaderText = "Code"
        grdSaved.Columns(3).HeaderText = "Father Name"
        grdSaved.Columns(4).HeaderText = "NIC"
        grdSaved.Columns(18).HeaderText = "Salary"
        grdSaved.Columns(10).HeaderText = "City"
        grdSaved.Columns(6).HeaderText = "Gender"

        'grdSaved.Columns(4).co = DataGridViewCheckBoxCell
        grdSaved.Columns(1).Width = 175
        grdSaved.Columns(2).Width = 75
        grdSaved.Columns(3).Width = 175
        grdSaved.Columns(4).Width = 50
        grdSaved.Columns(18).Width = 100
        grdSaved.Columns(10).Width = 100
        grdSaved.Columns(6).Width = 50


    End Sub
    Private Sub RefreshControls()

        txtReligion.Text = ""
        txtName.Text = ""
        txtCode.Text = ""
        txtFather.Text = ""
        txtNIC.Text = ""
        txtNTN.Text = ""
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtMobile.Text = ""
        txtEmail.Text = ""
        txtSalary.Text = ""
        txtComments.Text = ""

        ddlGender.SelectedIndex = 0
        ddlCity.SelectedIndex = 0
        ddlDept.SelectedIndex = 0
        ddlDesignation.SelectedIndex = 0
        ddlMaritalStatus.SelectedIndex = 0

        chkActive.Checked = True
        dtpDOB.Value = Date.Today 'Format(Date.Today, "dd/MM/yyyy")
        dtpLeaving.Value = Date.Today 'Format(Now, "dd/MM/yyyy")

    End Sub
    Private Function Save() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=SimplePOS;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Insert into tblDefEmployee   (Employee_Name, Employee_Code, Father_Name, NIC, NTN, Gender, Martial_Status, Religion, DOB, City_ID, Address, Phone, Mobile, Email, " _
                                     & " Joining_Date, Dept_ID, Desig_ID, Salary, Active, Leaving_Date, Comments) values( " _
                                    & " '" & txtName.Text & "','" & txtCode.Text & "','" & txtFather.Text & "','" & txtNIC.Text & "','" & txtNTN.Text & "','" & ddlGender.SelectedItem.ToString & "','" & ddlMaritalStatus.SelectedItem.ToString & "', " _
                                    & " '" & txtReligion.Text & "','" & Format(dtpDOB.Value, "yyyy/MM/dd") & "'," & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ",'" & txtAddress.Text & "','" & txtPhone.Text & "','" & txtMobile.Text & "','" & txtEmail.Text & "', " _
                                    & " '" & Format(dtpJoining.Value, "yyyy/MM/dd") & "'," & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & "," & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", " & Val(txtSalary.Text) & "," & Abs(Val(chkActive.Checked)) & ", " _
                                    & " " & IIf(chkActive.Checked = True, "NULL", Format(dtpLeaving.Value, "yyyy/MM/dd")) & ", '" & txtComments.Text & "')"

            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""


            trans.Commit()
            Save = True
        Catch ex As Exception
            trans.Rollback()
            Save = False
        End Try

    End Function
    Private Sub FillCombo(Optional ByVal strCondition As String = "")
        Dim str As String


        'If strCondition = "Item" Then
        str = "Select CityID, CityName from tblListCity"
        FillDropDown(ddlCity, str)

        'ElseIf strCondition = "Category" Then
        str = "Select EmployeeDeptID, EmployeeDeptName from EmployeeDeptDefTable"
        FillDropDown(ddlDept, str)

        'ElseIf strCondition = "ItemFilter" Then
        str = "Select EmployeeDesignationID, EmployeeDesignationName from EmployeeDesignationDefTable"
        FillDropDown(ddlDesignation, str)
        '        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If FormValidate() Then

            If btnSave.Text = "Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then
                    msg_Information(str_informSave)
                    RefreshControls()
                    DisplayRecord()
                Else
                    msg_Error("Record has not been added")

                End If
            Else

                If Update_Record() Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    msg_Information(str_informUpdate)
                    RefreshControls()
                    DisplayRecord()
                Else
                    msg_Error("Record has not been updated")

                End If
            End If

        End If

    End Sub
    Private Function FormValidate() As Boolean
        If txtName.Text = "" Then
            msg_Error("You must enter Employee Name")
            txtName.Focus() : FormValidate = False : Exit Function
        End If

        If txtCode.Text = "" Then
            msg_Error("You must enter Employee Code")
            txtCode.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.ddlCity.SelectedIndex > 0 Then
            msg_Error("Please select a city") : ddlCity.Focus() : Return False : Exit Function
        End If
        If Not Me.ddlDept.SelectedIndex > 0 Then
            msg_Error("Please select a Department") : ddlDept.Focus() : Return (False) : Exit Function
        End If
        If Not Me.ddlDesignation.SelectedIndex > 0 Then
            msg_Error("Please select a Designation") : ddlDesignation.Focus() : Return False : Exit Function
        End If
        Return True

    End Function
    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer = 0

        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=SimplePOS;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Update tblDefEmployee set Employee_Name='" & txtName.Text & "',Employee_Code='" & txtCode.Text & "', Father_Name='" & txtFather.Text & "', " _
                                    & " NIC='" & txtNIC.Text & "', NTN='" & txtNTN.Text & "', Gender='" & ddlGender.SelectedItem.ToString & "', " _
                                    & " Martial_Status='" & ddlMaritalStatus.SelectedItem.ToString & "', Religion='" & txtReligion.Text & "', DOB='" & Format(dtpDOB.Value, "yyyy/MM/dd") & "', " _
                                    & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address='" & txtAddress.Text & "', Phone='" & txtPhone.Text & "', Mobile='" & txtMobile.Text & "', Email='" & txtEmail.Text & "', " _
                                    & " Joining_Date='" & Format(dtpJoining.Value, "yyyy/MM/dd") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
                                    & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", Format(dtpLeaving.Value, "yyyy/MM/dd")) & ", Comments='" & txtComments.Text & "' " _
                                    & " Where Employee_ID= " & txtEmpID.Text & " "


            objCommand.ExecuteNonQuery()

            trans.Commit()
            Update_Record = True
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        End Try


    End Function

    Private Sub grdSaved_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSaved.CellClick

        txtEmpID.Text = grdSaved.Rows(e.RowIndex).Cells(0).Value
        txtName.Text = grdSaved.Rows(e.RowIndex).Cells(1).Value
        txtCode.Text = grdSaved.Rows(e.RowIndex).Cells(2).Value
        txtFather.Text = grdSaved.Rows(e.RowIndex).Cells(3).Value & ""
        txtNIC.Text = grdSaved.Rows(e.RowIndex).Cells(4).Value & ""

        txtNTN.Text = grdSaved.Rows(e.RowIndex).Cells(5).Value & ""
        ddlGender.SelectedIndex = ddlGender.FindStringExact((grdSaved.Rows(e.RowIndex).Cells(6).Value)) 'grdSaved.Rows(e.RowIndex).Cells(8).Value & ""
        ddlMaritalStatus.SelectedIndex = ddlMaritalStatus.FindStringExact((grdSaved.Rows(e.RowIndex).Cells(7).Value))
        txtReligion.Text = grdSaved.Rows(e.RowIndex).Cells(8).Value & ""
        dtpDOB.Value = grdSaved.Rows(e.RowIndex).Cells(9).Value & ""
        If grdSaved.Rows(e.RowIndex).Cells(10).Value & "" <> "" Then
            ddlCity.SelectedIndex = ddlCity.FindStringExact((grdSaved.Rows(e.RowIndex).Cells(10).Value & ""))
        Else
            ddlCity.SelectedIndex = 0
        End If

        txtAddress.Text = grdSaved.Rows(e.RowIndex).Cells(11).Value & ""
        txtPhone.Text = grdSaved.Rows(e.RowIndex).Cells(12).Value & ""

        txtMobile.Text = grdSaved.Rows(e.RowIndex).Cells(13).Value & ""
        txtEmail.Text = grdSaved.Rows(e.RowIndex).Cells(14).Value & ""

        dtpJoining.Value = grdSaved.Rows(e.RowIndex).Cells(15).Value & ""
        If grdSaved.Rows(e.RowIndex).Cells(16).Value & "" <> "" Then
            ddlDept.SelectedIndex = ddlDept.FindStringExact((grdSaved.Rows(e.RowIndex).Cells(16).Value & ""))
        Else
            ddlDept.SelectedIndex = 0
        End If
        If grdSaved.Rows(e.RowIndex).Cells(17).Value & "" <> "" Then
            ddlDesignation.SelectedIndex = ddlDesignation.FindStringExact((grdSaved.Rows(e.RowIndex).Cells(17).Value & ""))
        Else
            ddlDesignation.SelectedIndex = 0
        End If
        txtSalary.Text = grdSaved.Rows(e.RowIndex).Cells(18).Value & ""

        chkActive.Checked = grdSaved.Rows(e.RowIndex).Cells(19).Value
        If grdSaved.Rows(e.RowIndex).Cells(20).Value & "" <> "" Then
            dtpLeaving.Value = grdSaved.Rows(e.RowIndex).Cells(20).Value & ""
        End If
        txtComments.Text = grdSaved.Rows(e.RowIndex).Cells(21).Value & ""


        'Call DisplayDetail(grdSaved.Rows(e.RowIndex).Cells(5).Value)
        btnSave.Text = "Update"
    End Sub

    Private Sub grdSaved_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSaved.CellContentClick

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RefreshControls()
    End Sub
End Class