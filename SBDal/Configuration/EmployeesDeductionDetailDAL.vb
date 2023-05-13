Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class EmployeesDeductionDetailDAL
    Public Function Add(ByVal EmpDeduction As List(Of EmployeesDeductionDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            For Each EmpDeductionDt As EmployeesDeductionDetail In EmpDeduction
                If IsValidate(EmpDeductionDt.DeductionTypeId, EmpDeductionDt.EmployeeId, trans) = False Then
                    Dim str As String = "INSERT INTO tblDeductionDetail(DeductionTypeId, EmployeeId, Deduction_Amount) Values(" & EmpDeductionDt.DeductionTypeId & ", " & EmpDeductionDt.EmployeeId & ", " & EmpDeductionDt.Deduction_Amount & " )  "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    Dim str1 As String = "UPDATE tblDeductionDetail SET Deduction_Amount=" & EmpDeductionDt.Deduction_Amount & " WHERE DeductionTypeId=" & EmpDeductionDt.DeductionTypeId & " AND EmployeeId=" & EmpDeductionDt.EmployeeId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidate(ByVal DeductionTypeId As Integer, ByVal EmpId As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = "Select * From tblDeductionDetail WHERE DeductionTypeId=" & DeductionTypeId & " AND EmployeeId=" & EmpId & " "
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
            Return UtilityDAL.GetDataTable("Select EmployeeId, DeductionTypeId,  Deduction_Amount From tblDeductionDetail")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
