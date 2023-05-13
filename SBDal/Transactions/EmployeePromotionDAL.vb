Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class EmployeePromotionDAL

    Public Function Save(ByVal emp As EmployeePromotionBE, ByVal protype As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Insert Into tblEmployeePromotion(PromotionType,Ref_No,Ref_Date,EmployeeId,OldDepartmentId,OldDepartmentName,OldDivisionId,OldDivisionName,OldDesignationId,OldDesignationName,DepartmentId,DivisionId,DesignationId,Increament_Salary,NewBasicSalary,BasicSalary,Status,EntryDate,UserName,Old_EmployeeId,Old_BasicSalary) values(N'" & emp.PromotionType & "', N'" & emp.Ref_No & "', N'" & emp.Ref_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & emp.EmployeeId & ", " & emp.OldDepartmentId & ", N'" & emp.OldDepartmentName & "', " & emp.OldDivisionId & ", N'" & emp.OldDivisionName & "', " & emp.OldDesignationId & ", N'" & emp.OldDesignationName & "', " & emp.DepartmentId & ", " & emp.DivisionId & ", " & emp.DesignationId & ", N'" & emp.Increament_Salary & "', N'" & emp.NewBasicSalary & "', N'" & emp.BasicSalary & "', " & IIf(emp.Status = True, 1, 0) & ", N'" & emp.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & emp.UserName & "', " & emp.Old_EmployeeId & ", N'" & emp.Old_BasicSalary & "')Select @@Identity"
            emp.PromotionId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            If protype = "Increment" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.NewBasicSalary & " Where Employee_ID = " & emp.EmployeeId & " "
            ElseIf protype = "Promotion" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.NewBasicSalary & ", Dept_ID = " & emp.DepartmentId & ", Desig_ID = " & emp.DesignationId & ", Dept_Division = " & emp.DivisionId & " Where Employee_ID = " & emp.EmployeeId & " "
            ElseIf protype = "Transfer" Then
                strSQL = "Update tblDefEmployee set Dept_ID = " & emp.DepartmentId & ", Desig_ID = " & emp.DesignationId & ", Dept_Division = " & emp.DivisionId & " Where Employee_ID = " & emp.EmployeeId & " "
            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal emp As EmployeePromotionBE, ByVal protype As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            ' Old Query Reset
            If protype = "Increment" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.Old_BasicSalary & " Where Employee_ID = " & emp.Old_EmployeeId & " "
            ElseIf protype = "Promotion" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.Old_BasicSalary & ", Dept_ID = " & emp.OldDepartmentId & ", Desig_ID = " & emp.OldDesignationId & ", Dept_Division = " & emp.OldDivisionId & " Where Employee_ID = " & emp.Old_EmployeeId & " "
            ElseIf protype = "Transfer" Then
                strSQL = "Update tblDefEmployee set Dept_ID = " & emp.OldDepartmentId & ", Desig_ID = " & emp.OldDesignationId & ", Dept_Division = " & emp.OldDivisionId & " Where Employee_ID = " & emp.Old_EmployeeId & " "
            End If
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ' Update Employee Promotion Record
            strSQL = "Update tblEmployeePromotion set PromotionType = N'" & emp.PromotionType & "',Ref_No = N'" & emp.Ref_No & "',Ref_Date = N'" & emp.Ref_Date.ToString("yyyy-M-d h:mm:ss tt") & "',EmployeeId = " & emp.EmployeeId & ",OldDepartmentId = " & emp.OldDepartmentId & ",OldDivisionId = " & emp.OldDivisionId & ",OldDesignationId = " & emp.OldDesignationId & ",DepartmentId = " & emp.DepartmentId & ",DivisionId = " & emp.DivisionId & ",DesignationId = " & emp.DesignationId & ",Increament_Salary = N'" & emp.Increament_Salary & "',NewBasicSalary = N'" & emp.NewBasicSalary & "',BasicSalary = N'" & emp.BasicSalary & "',Status = " & IIf(emp.Status = True, 1, 0) & ",EntryDate = N'" & emp.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "',UserName = N'" & emp.UserName & "', Old_EmployeeId = " & emp.Old_EmployeeId & ",Old_BasicSalary = " & emp.BasicSalary & "  WHERE PromotionId=" & emp.PromotionId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ' Update Employee Rescord
            If protype = "Increment" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.NewBasicSalary & " Where Employee_ID = " & emp.EmployeeId & " "
            ElseIf protype = "Promotion" Then
                strSQL = "Update tblDefEmployee set salary = " & emp.NewBasicSalary & ", Dept_ID = " & emp.DepartmentId & ", Desig_ID = " & emp.DesignationId & ", Dept_Division = " & emp.DivisionId & " Where Employee_ID = " & emp.EmployeeId & " "
            ElseIf protype = "Transfer" Then
                strSQL = "Update tblDefEmployee set Dept_ID = " & emp.DepartmentId & ", Desig_ID = " & emp.DesignationId & ", Dept_Division = " & emp.DivisionId & " Where Employee_ID = " & emp.EmployeeId & " "
            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal emp As EmployeePromotionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblEmployeePromotion WHERE PromotionId=" & emp.PromotionId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT TOP 100 PERCENT dbo.tblEmployeePromotion.PromotionId, dbo.tblEmployeePromotion.PromotionType, dbo.tblEmployeePromotion.Ref_No, " _
                  & " dbo.tblEmployeePromotion.Ref_Date, dbo.tblEmployeePromotion.EmployeeId, dbo.tblDefEmployee.Employee_Code, dbo.tblDefEmployee.Employee_Name, " _
                  & " dbo.tblDefEmployee.Father_Name, dbo.tblEmployeePromotion.OldDepartmentId, dbo.tblEmployeePromotion.OldDepartmentName,  " _
                  & " dbo.tblEmployeePromotion.OldDivisionId, dbo.tblEmployeePromotion.OldDivisionName, dbo.tblEmployeePromotion.OldDesignationId,  " _
                  & " dbo.tblEmployeePromotion.OldDesignationName, dbo.tblEmployeePromotion.DepartmentId, dbo.tblEmployeePromotion.DivisionId,  " _
                  & " dbo.tblEmployeePromotion.DesignationId, dbo.tblEmployeePromotion.Increament_Salary, dbo.tblEmployeePromotion.NewBasicSalary,  " _
                  & " dbo.tblEmployeePromotion.BasicSalary, dbo.tblEmployeePromotion.Status, dbo.tblEmployeePromotion.Old_EmployeeId,  " _
                  & " dbo.tblEmployeePromotion.Old_BasicSalary " _
                  & " FROM dbo.tblEmployeePromotion INNER JOIN " _
                  & " dbo.tblDefEmployee ON dbo.tblEmployeePromotion.EmployeeId = dbo.tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
                  & " dbo.EmployeeDeptDefTable ON dbo.tblEmployeePromotion.DepartmentId = dbo.EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
                  & " dbo.EmployeeDesignationDefTable ON dbo.tblEmployeePromotion.DesignationId = dbo.EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                  & " dbo.tblDefDivision ON dbo.tblEmployeePromotion.DivisionId = dbo.tblDefDivision.Division_Id " _
                  & " ORDER BY dbo.tblEmployeePromotion.Ref_No DESC "
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetSerialNo(Optional ByVal trans As SqlTransaction = Nothing) As String
        Try

            Dim serial As String
            Dim serialid As Integer
            Dim serialno As String = String.Empty
            serial = "Select isnull( Max(Right(Ref_No,6)),0)+1 as Serial from tblEmployeePromotion"
            Dim dtser As New DataTable
            If trans IsNot Nothing Then
                dtser = GetDataTable(serial, trans)

            Else
                dtser = GetDataTable(serial)
            End If

            If dtser IsNot Nothing Then
                If Not dtser.Rows.Count = 0 Then
                    serialid = dtser.Rows(0).Item(0)
                End If
            End If

            serialno = "EP-" & Right("000000" & CStr(serialid), 6)

            Return serialno

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
