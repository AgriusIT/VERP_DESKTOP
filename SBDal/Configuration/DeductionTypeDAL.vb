Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class DeductionTypeDAL
    Public Function Add(ByVal DeductionType As DeductionTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "INSERT INTO tblDeductionType(DeductionType, AccountId, Active) VALUES(N'" & DeductionType.DeductionType & "', " & DeductionType.AccountId & ", " & IIf(DeductionType.Active = True, 1, 0) & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal DeductionType As DeductionTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try



            Dim str As String = String.Empty
            str = "UPDATE tblDeductionType SET " _
                  & " DeductionType=N'" & DeductionType.DeductionType & "', " _
                  & " AccountId=" & DeductionType.AccountId & ", " _
                  & " Active = " & IIf(DeductionType.Active = True, 1, 0) & " " _
                  & " WHERE DeductionTypeId=" & DeductionType.DeductionTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal DeductionType As DeductionTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "DELETE FROM tblDeductionType WHERE DeductionTypeId=" & DeductionType.DeductionTypeId & ""
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
            str = "Select a.DeductionTypeId, a.DeductionType, a.AccountId, a.Active, b.detail_title From tblDeductionType a LEFT OUTER JOIN vwCOADetail b on a.AccountId = b.coa_detail_id"
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
    Public Function DeductionTypeData() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select a.DeductionTypeId, a.DeductionType From tblDeductionType a WHERE a.active=1"
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
