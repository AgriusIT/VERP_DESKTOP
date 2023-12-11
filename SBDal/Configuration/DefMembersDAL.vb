Imports SBModel
Imports System.Data.SqlClient
Imports SBUtility.Utility




Public Class DefMembersDAL

#Region "Local Functions and Procedures"
    '<summary>
    '</summary>
    '<param name="objClass"></param>
    '<returns></returns>
    '<remarks></remarks>
    Public Function IsValidateForSave(ByVal objMembers As DefMembers) As Boolean

        Try
            Dim strSQL As String
            strSQL = "SELECT      Membersname, Memberscode" _
            & " FROM         tblDefMembers " _
            & " WHERE Membersname = '" & objMembers.MembersName.Replace("'", "''") & "' AND Membersid <> '" & objMembers.MembersID & "'"


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                'If Not objNameDR.HasRows Then

                strSQL = "SELECT     Membersname, Memberscode" _
                & " FROM         tblDefMembers " _
                & " WHERE MembersCode = '" & objMembers.MembersCode.Replace("'", "''") & "'  AND Membersid <> '" & objMembers.MembersID & "'"

                Using objCodeDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                    If objCodeDR.HasRows Then

                        Throw New Exception(gstrMsgDuplicateCode)

                    End If

                End Using

                'ElseIf objNameDR.HasRows Then

                '    Throw New Exception(gstrMsgDuplicateName)

                'End If

                Return True

            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' 
    ''' </summary>
    ''' <param name="objMembers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidateForDelete(ByVal objMembers As DefMembers) As Boolean

        Try

            'TODO: Apply to verify record deletion
            ''Dim strSQL As String

            '' ''Check in Areas
            ''strSQL = "SELECT     *  " _
            ''& " FROM         tblDefMembersAreas " _
            ''& " WHERE Members_id = '" & objMembers.MembersID & "'"

            ''Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            ''    If objNameDR.HasRows Then
            ''        Throw New Exception(gstrMsgDependentRecordExist & " (tblDefMembersAreas)")
            ''    End If

            ''End Using

            '' ''========
            '' ''Check in Shops
            ''strSQL = "SELECT     *  " _
            ''& " FROM         tblDefShops " _
            ''& " WHERE Members_id = '" & objMembers.MembersID & "'"

            ''Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            ''    If objNameDR.HasRows Then
            ''        Throw New Exception(gstrMsgDependentRecordExist & " (tblDefShops)")
            ''    End If

            ''End Using

            '' ''========
            '' ''Check in Member
            ''strSQL = "SELECT     *  " _
            ''& " FROM         tblMemberInfo " _
            ''& " WHERE Members_id = '" & objMembers.MembersID & "'"

            ''Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            ''    If objNameDR.HasRows Then
            ''        Throw New Exception(gstrMsgDuplicateCode & " (tblMemberInfo)")
            ''    End If

            ''End Using


            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



#Region "Public Functions and Procedures"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objMembers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal objMembers As DefMembers) As Boolean
        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForSave(objMembers)

        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "INSERT INTO TblDefMembers ( Membersname, Memberscode, sortOrder, comments, Active, IsDate) " _
            & " VALUES ( '" & objMembers.MembersName.Trim.Replace("'", "''") & "', '" & objMembers.MembersCode.Trim.Replace("'", "''") & "', '" & objMembers.SortOrder & "', '" & objMembers.Comments.Trim.Replace("'", "''") & "', '" & IIf(objMembers.Active = True, "1", "0") & "', '" & Date.Now & "') " _
            & " Select @@Identity"

            ' ''Execute SQL 
            objMembers.MembersID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            '' ''SQL Statement Log
            ''objMembers.ActivityLog.SQLType = "INSERT"
            ''objMembers.ActivityLog.TableName = "TblDefMembers"
            ''objMembers.ActivityLog.SQL = strSQL
            ''UtilityDAL.BuildSQLLog(objMembers.ActivityLog, trans)

            '' ''Activity Log
            ''objMembers.ActivityLog.FormAction = "Save"
            ''UtilityDAL.BuildActivityLog(objMembers.ActivityLog, trans)

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objMembers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal objMembers As DefMembers) As Boolean

        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForSave(objMembers)


        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "UPDATE TblDefMembers SET " _
            & " Membersname = '" & objMembers.MembersName.Trim.Replace("'", "''") & "', " _
            & " MembersCode = '" & objMembers.MembersCode.Trim.Replace("'", "''") & "', " _
             & " sortOrder = '" & objMembers.SortOrder & "',  " _
            & " comments = '" & objMembers.Comments.Trim.Replace("'", "''") & "',  " _
            & " Active = '" & IIf(objMembers.Active, "1", "0") & "' " _
            & " WHERE MembersId = " & objMembers.MembersID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ' ''SQL Statement Log
            ''objMembers.ActivityLog.SQLType = "UPDATE"
            ''objMembers.ActivityLog.TableName = "TblDefMembers"
            ''objMembers.ActivityLog.SQL = strSQL
            ''UtilityDAL.BuildSQLLog(objMembers.ActivityLog, trans)

            '' ''Activity Log
            ''objMembers.ActivityLog.FormAction = "Update"
            ''UtilityDAL.BuildActivityLog(objMembers.ActivityLog, trans)


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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objMembers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Deleted(ByVal objMembers As DefMembers) As Boolean

        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForDelete(objMembers)

        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "DELETE FROM TblDefMembers " _
            & " WHERE MembersId = " & objMembers.MembersID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            '' ''SQL Statement Log
            ''objMembers.ActivityLog.SQLType = "DELETE"
            ''objMembers.ActivityLog.TableName = "TblDefMembers"
            ''objMembers.ActivityLog.SQL = strSQL
            ''UtilityDAL.BuildSQLLog(objMembers.ActivityLog, trans)

            '' ''Activity Log
            ''objMembers.ActivityLog.FormAction = "Delete"
            ''UtilityDAL.BuildActivityLog(objMembers.ActivityLog, trans)

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT     MembersId AS [Members ID], MembersCode AS [Members Code], MembersName AS [Members Name], SortOrder AS [Sort Order], " & _
                    " Comments AS Address, Active AS Active " & _
                    " FROM tblDefMembers " & _
                    " ORDER BY SortOrder, MembersName"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("MembersList") 'EnumHashTableKeyConstants.GetMembersList.ToString)
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


