Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class AdjustmentTypeDAL
    Public Function Add(ByVal Type As AdjustmentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = ""
            str = "INSERT INTO tblAdjustmentType(AdjType, Sort_Order, Active,AdjustmentInShort) Values(N'" & Type.AdjTypeName & "'," & Type.Sort_Order & "," & IIf(Type.Active = True, 1, 0) & ", " & IIf(Type.AdjustmentInShort = True, 1, 0) & ")"
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
    Public Function Update(ByVal Type As AdjustmentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = ""
            str = "UPDATE  tblAdjustmentType  SET AdjType=N'" & Type.AdjTypeName & "', Sort_Order=" & Type.Sort_Order & ", Active=" & IIf(Type.Active = True, 1, 0) & ", AdjustmentInShort=" & IIf(Type.AdjustmentInShort = True, 1, 0) & " WHERE AdjType_Id=" & Type.AdjTypeId
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
    Public Function Delete(ByVal Type As AdjustmentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = ""
            str = "Delete From  tblAdjustmentType WHERE AdjType_Id=" & Type.AdjTypeId
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
    Public Function getAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select AdjType_Id, AdjType, Sort_Order, isnull(Active,0) as Active, Isnull(AdjustmentInShort,0) as AdjustmentInShort From tblAdjustmentType")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
