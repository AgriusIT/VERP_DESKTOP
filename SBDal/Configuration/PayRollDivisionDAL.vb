''24-Feb-2014 TASK:M22 Imran Ali Leave Wise Salary Calculation
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class PayRollpayisionDAL

    Public Function Save(ByVal pay As PayRollDivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            'Before against task:M22
            'strSQL = "Insert Into tblDefPayRollDivision(Division_Id,PayRollDivisionName,Level,Active,Sort_Order) values(" & pay.Division_Id & ", N'" & pay.PayRollDivisionName & "', N'" & pay.Level & "', " & IIf(pay.Active = True, 1, 0) & ", " & pay.Sort_Order & ")Select @@Identity"
            'Task:M22 Added Field AnnualAllowedLeave.
            strSQL = "Insert Into tblDefPayRollDivision(Division_Id,PayRollDivisionName,Level,Active,Sort_Order, AnnualAllowedLeave) values(" & pay.Division_Id & ", N'" & pay.PayRollDivisionName.Replace("'", "''") & "', N'" & pay.Level & "', " & IIf(pay.Active = True, 1, 0) & ", " & pay.Sort_Order & ", " & Val(pay.AnnualAllowedLeave) & ")Select @@Identity"
            'Task:M22
            pay.PayRollDivision_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal pay As PayRollDivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            'Before against task:M22
            'strSQL = "Update tblDefPayRollDivision set Division_Id=" & pay.Division_Id & ", PayRollDivisionName=N'" & pay.PayRollDivisionName & "', Level=N'" & pay.Level & "', Active=" & IIf(pay.Active = True, 1, 0) & ", Sort_Order=" & pay.Sort_Order & "  WHERE PayRollDivision_Id = " & pay.PayRollDivision_Id
            'Task:M22 Added Field AnnualAllowedLeave
            strSQL = "Update tblDefPayRollDivision set Division_Id=" & pay.Division_Id & ", PayRollDivisionName=N'" & pay.PayRollDivisionName.Replace("'", "''") & "', Level=N'" & pay.Level & "', Active=" & IIf(pay.Active = True, 1, 0) & ", Sort_Order=" & pay.Sort_Order & ", AnnualAllowedLeave=" & Val(pay.AnnualAllowedLeave) & "  WHERE PayRollDivision_Id = " & pay.PayRollDivision_Id
            'End Task:M22
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete_Record(ByVal pay As PayRollDivisionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblDefPayRollDivision WHERE PayRollDivision_Id=" & pay.PayRollDivision_Id
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
            'Before against task:M22 
            'str = "Select * from tblDefPayRollDivision"
            'str = "SELECT     dbo.tblDefPayRollDivision.PayRollDivision_Id, dbo.tblDefPayRollDivision.Division_Id, dbo.tblDefDivision.Division_Name, dbo.tblDefPayRollDivision.PayRollDivisionName, dbo.tblDefPayRollDivision.[Level], dbo.tblDefPayRollDivision.Sort_Order, dbo.tblDefPayRollDivision.Active " _
            '& " FROM         dbo.tblDefDivision INNER JOIN " _
            '& " dbo.tblDefPayRollDivision ON dbo.tblDefDivision.Division_Id = dbo.tblDefPayRollDivision.Division_Id"
            'Task:M22 Added Field AnnualAllowedLeave
            str = "SELECT     dbo.tblDefPayRollDivision.PayRollDivision_Id, dbo.tblDefPayRollDivision.Division_Id, dbo.tblDefDivision.Division_Name, dbo.tblDefPayRollDivision.PayRollDivisionName, dbo.tblDefPayRollDivision.[Level], Isnull(dbo.tblDefPayRollDivision.AnnualAllowedLeave,0) as AnnualAllowedLeave,  dbo.tblDefPayRollDivision.Sort_Order, dbo.tblDefPayRollDivision.Active " _
                       & " FROM         dbo.tblDefDivision INNER JOIN " _
               & " dbo.tblDefPayRollDivision ON dbo.tblDefDivision.Division_Id = dbo.tblDefPayRollDivision.Division_Id"
            'End Task:M22

            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
