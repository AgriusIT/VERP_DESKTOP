Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class AllowanceTypeDAL
    Public Function Add(ByVal AllowanceType As AllowanceTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "INSERT INTO tblAllowanceType(AllowanceType, AccountId, Active) VALUES(N'" & AllowanceType.AllowanceType & "', " & AllowanceType.AccountId & ", " & IIf(AllowanceType.Active = True, 1, 0) & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal AllowanceType As AllowanceTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "UPDATE tblAllowanceType SET " _
                  & " AllowanceType=N'" & AllowanceType.AllowanceType & "', " _
                  & " AccountId=" & AllowanceType.AccountId & ", " _
                  & " Active = " & IIf(AllowanceType.Active = True, 1, 0) & " " _
                  & " WHERE AllowanceTypeId=" & AllowanceType.AllowanceTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal AllowanceType As AllowanceTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "DELETE FROM tblAllowanceType WHERE AllowanceTypeId=" & AllowanceType.AllowanceTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select a.AllowanceTypeId, a.AllowanceType, a.AccountId, a.Active, b.detail_title From tblAllowanceType a LEFT OUTER JOIN vwCOADetail b on a.AccountId = b.coa_detail_id"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AllowanceTypeData() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select a.AllowanceTypeId, a.AllowanceType From tblAllowanceType a WHERE a.active=1"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmployeesData() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefEmployee.Employee_ID as EmpId, dbo.tblDefEmployee.Employee_Code as [Code], dbo.tblDefEmployee.Employee_Name as Employee, " _
            & " dbo.EmployeeDesignationDefTable.EmployeeDesignationName as Designation, dbo.EmployeeDeptDefTable.EmployeeDeptName as Department " _
            & " FROM dbo.tblDefEmployee INNER JOIN dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
            & " dbo.EmployeeDesignationDefTable ON dbo.tblDefEmployee.Desig_ID = dbo.EmployeeDesignationDefTable.EmployeeDesignationId ORDER BY 2 asc"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
