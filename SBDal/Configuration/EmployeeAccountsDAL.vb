Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBDal.UnitDAL
Imports SBUtility.Utility


Public Class EmployeeAccountsDAL
    Public Function Save(ByVal EmpAcList As List(Of EmployeeAccountsBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strQuery As String = String.Empty
            For Each emp As EmployeeAccountsBE In EmpAcList
                strQuery = String.Empty
                'TASKM169151 Add new field Value_In
                strQuery = "INSERT INTO tblEmployeeAccounts(Employee_Id, Account_Id, Type_Id, FlgReceivable, Active, Sort_Order, Amount) Values(Ident_Current('tblDefEmployee'), " & emp.Account_Id & ", " & emp.Type_Id & ", " & IIf(emp.FlgReceivable = True, 1, 0) & ", " & IIf(emp.Active = True, 1, 0) & ", " & emp.Sort_Order & "," & emp.Amount & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal EmpAcList As List(Of EmployeeAccountsBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strQuery As String = String.Empty
            If Not EmpAcList.Count = 0 Then
                strQuery = "Delete From tblEmployeeAccounts WHERE Employee_Id=" & EmpAcList.Item(0).Employee_Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            End If

            For Each emp As EmployeeAccountsBE In EmpAcList
                strQuery = String.Empty
                'TASKM169151 Add new field Value_In
                strQuery = "INSERT INTO tblEmployeeAccounts(Employee_Id, Account_Id, Type_Id, FlgReceivable, Active, Sort_Order,Amount) Values(" & emp.Employee_Id & ", " & emp.Account_Id & ", " & emp.Type_Id & ", " & IIf(emp.FlgReceivable = True, 1, 0) & ", " & IIf(emp.Active = True, 1, 0) & ", " & emp.Sort_Order & ", " & emp.Amount & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal EmpAcList As EmployeeAccountsBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strQuery As String = String.Empty

            strQuery = "Delete From tblEmployeeAccounts WHERE Employee_Id=" & EmpAcList.Employee_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

End Class
