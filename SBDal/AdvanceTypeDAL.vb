Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class AdvanceTypeDAL

    'Declare Enum for indexing
    Public Enum enmAdvanceType
        Id
        Title
        Comments
        SalaryDeduct
        SortOrder
        Active
        AccountId
    End Enum

 
    Public Function Add(ByVal objMod As AdvanceTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblDefAdvancesType(Title, SalaryDeduct,Active, SortOrder,Comments,AccountId) " _
                & " VALUES('" & objMod.Title.Replace("'", "''") & "',N'" & IIf(objMod.SalaryDeduct = True, 1, 0) & "',N'" & IIf(objMod.Active = True, 1, 0) & "'," _
                & " " & objMod.SortOrder & ",N'" & objMod.Comments.Replace("'", "''") & "'," & objMod.AccountId & ") Select @@Identity"
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(objMod As AdvanceTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update tblDefAdvancesType SET Title= '" & objMod.Title.Replace("'", "''") & "', " _
                      & " Comments=N'" & objMod.Comments.Replace("'", "''") & "', " _
                      & " SalaryDeduct=" & IIf(objMod.SalaryDeduct = True, 1, 0) & ", " _
                      & " Active=" & IIf(objMod.Active = True, 1, 0) & ", " _
                      & " SortOrder=" & objMod.SortOrder & ", " _
                      & " AccountId=" & objMod.AccountId & " WHERE Id = " & objMod.id & ""

                                
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(objMod As AdvanceTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblDefAdvancesType WHERE Id=" & objMod.id & ""
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll(Optional ByVal Condition As String = "") As List(Of AdvanceTypeBE)
        Try
            Dim strQuery As String = String.Empty
            strQuery = " Select Id ,Title ,Comments ,SalaryDeduct ,SortOrder ,Active, ISNULL(AccountId, 0) AS AccountId from tblDefAdvancesType  "
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim AdvanceTypeList As New List(Of AdvanceTypeBE)
            Dim AdvanceType As AdvanceTypeBE
            If dr.HasRows Then
                While dr.Read
                    AdvanceType = New AdvanceTypeBE
                    AdvanceType.id = dr.GetValue(enmAdvanceType.Id)
                    AdvanceType.Title = dr.GetValue(enmAdvanceType.Title).ToString
                    AdvanceType.Comments = dr.GetValue(enmAdvanceType.Comments).ToString
                    AdvanceType.SalaryDeduct = dr.GetValue(enmAdvanceType.SalaryDeduct)
                    AdvanceType.SortOrder = dr.GetValue(enmAdvanceType.SortOrder)
                    AdvanceType.Active = dr.GetValue(enmAdvanceType.Active)
                    AdvanceType.AccountId = dr.GetValue(enmAdvanceType.AccountId)

                    AdvanceTypeList.Add(AdvanceType)
                End While
            End If
            Return AdvanceTypeList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
