Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class EmployeesAllowanceDetailDAL
    Public Function Add(ByVal EmpAllowance As List(Of EmployeesAllowanceDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            For Each EmpAllowanceDt As EmployeesAllowanceDetail In EmpAllowance
                If IsValidate(EmpAllowanceDt.AllowanceTypeId, EmpAllowanceDt.EmployeeId, trans) = False Then
                    Dim str As String = "INSERT INTO tblAllowanceDetail(AllowanceTypeId, EmployeeId, Allowance_Amount) Values(" & EmpAllowanceDt.AllowanceTypeId & ", " & EmpAllowanceDt.EmployeeId & ", " & EmpAllowanceDt.Allowance_Amount & " )  "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    Dim str1 As String = "UPDATE tblAllowanceDetail SET Allowance_Amount=" & EmpAllowanceDt.Allowance_Amount & " WHERE AllowanceTypeId=" & EmpAllowanceDt.AllowanceTypeId & " AND EmployeeId=" & EmpAllowanceDt.EmployeeId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidate(ByVal AllowanceTypeId As Integer, ByVal EmpId As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = "Select * From tblAllowanceDetail WHERE AllowanceTypeId=" & AllowanceTypeId & " AND EmployeeId=" & EmpId & " "
            Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select EmployeeId, AllowanceTypeId,  Allowance_Amount From tblAllowanceDetail")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
