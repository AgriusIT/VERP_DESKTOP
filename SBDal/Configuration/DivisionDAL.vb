Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class DivisionDAL

    Public Function Save(ByVal div As DivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Insert Into tblDefDivision(Dept_Id,Division_Code,Division_Name,Active,Sort_Order) values(" & div.Dept_Id & ", N'" & div.Division_Code & "', N'" & div.Division_Name & "', " & IIf(div.Active = True, 1, 0) & ", " & div.Sort_Order & ")Select @@Identity"
            div.Division_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal div As DivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update tblDefDivision set Dept_Id=" & div.Dept_Id & ", Division_Code=N'" & div.Division_Code & "', Division_Name=N'" & div.Division_Name & "', Active=" & IIf(div.Active = True, 1, 0) & ", Sort_Order=" & div.Sort_Order & "  WHERE Division_Id=" & div.Division_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal div As DivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblDefDivision WHERE Division_Id=" & div.Division_Id
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
            'str = "Select * from tblDefDivision"
            str = "SELECT     dbo.tblDefDivision.Division_Id, dbo.tblDefDivision.Dept_Id, dbo.EmployeeDeptDefTable.EmployeeDeptName, dbo.tblDefDivision.Division_Code, " _
            & " dbo.tblDefDivision.Division_Name, dbo.tblDefDivision.Sort_Order, dbo.tblDefDivision.Active " _
            & " FROM         dbo.tblDefDivision INNER JOIN " _
            & " dbo.EmployeeDeptDefTable ON dbo.tblDefDivision.Dept_Id = dbo.EmployeeDeptDefTable.EmployeeDeptId "
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
