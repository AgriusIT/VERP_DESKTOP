Imports sbModel
Imports System.Data.SqlClient
Imports sbUtility

Public Class GroupDAL

#Region "Local Functions and Procedures"
   
    Public Function IsValidateForSave(ByVal objGroup As SecurityGroup) As Boolean

        Try
            Dim strSQL As String
            strSQL = "SELECT      GROUP_NAME " _
            & " FROM         tblSecurityGroup " _
            & " WHERE GROUP_NAME = N'" & objGroup.GroupName.Replace("'", "''") & "' AND GROUP_ID <> N'" & objGroup.GroupID & "'"


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then

                    Throw New Exception("Duplicate group name")

                End If

                Return True

            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForDelete(ByVal objGroup As SecurityGroup) As Boolean

        Try
            Dim strSQL As String

            ''Check in tblGLSecurityUser
            strSQL = "SELECT     *  " _
            & " FROM         tblSecurityUser " _
            & " WHERE group_id = N'" & objGroup.GroupID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(" Dependent record exists (tblSecurityUser)")
                End If

            End Using


            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Public Functions and Procedures"

    Public Function Add(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String = ""

            strSQL = "insert into tblSecurityGroup ( group_name, group_comments ) " _
            & " VALUES ( N'" & objGroup.GroupName.Trim.Replace("'", "''") & "',N'" & objGroup.GroupComments.Trim.Replace("'", "''") & "')" _
            & " Select Ident_Current('tblSecurityGroup')"

            ''Execute SQL 
            objGroup.GroupID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))


            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Public Function Update(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = ""
            strSQL = "UPDATE tblSecurityGroup SET " _
            & " GROUP_NAME = N'" & objGroup.GroupName.Trim.Replace("'", "''") & "', " _
            & " GROUP_COMMENTS = N'" & objGroup.GroupComments.Trim.Replace("'", "''") & "'  " _
            & " WHERE group_id = " & objGroup.GroupID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function


    Public Function Deleted(ByVal objGroup As SecurityGroup) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "DELETE FROM tblSecurityGroup " _
            & " WHERE group_id = " & objGroup.GroupID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()

        End Try
    End Function

    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT tblSecurityGroup.GROUP_ID as [Group ID]," _
            & " tblSecurityGroup.GROUP_NAME as  [Group Name]," _
            & " tblSecurityGroup.GROUP_COMMENTS as  Comments " _
            & "   FROM    tblSecurityGroup " _
            & " ORDER BY [Group Name]   "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("SecurityGroups")
            objDA.Fill(MyCollectionList)
            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

#End Region


End Class
