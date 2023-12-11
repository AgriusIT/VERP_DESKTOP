Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Public Class ShiftGroupDAL
    Public Function Add(ByVal ShiftGroup As ShiftGroupBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO ShiftGroupTable (ShiftGroupCode, ShiftGroupName, ShiftGroupComments, Active, SortOrder) " _
            & " Values(N'" & ShiftGroup.ShiftGroupCode & "', N'" & ShiftGroup.ShiftGroupName & "', N'" & ShiftGroup.ShiftGroupComments & "', " & IIf(ShiftGroup.Active = True, 1, 0) & ", " & Val(ShiftGroup.SortOrder) & ") Select @@Identity"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ShiftGroup As ShiftGroupBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = " UPDATE ShiftGroupTable Set ShiftGroupCode=N'" & ShiftGroup.ShiftGroupCode & "', " _
                 & " ShiftGroupName=N'" & ShiftGroup.ShiftGroupName & "', " _
                 & " ShiftGroupComments=N'" & ShiftGroup.ShiftGroupComments & "', " _
                 & " Active=" & IIf(ShiftGroup.Active = True, 1, 0) & ",  " _
                 & " SortOrder=" & Val(ShiftGroup.SortOrder) & "  WHERE ShiftGroupId=" & ShiftGroup.ShiftGroupId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ShiftGroup As ShiftGroupBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE FROM ShiftGroupTable WHERE ShiftGroupId=" & ShiftGroup.ShiftGroupId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select * From ShiftGroupTable")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
