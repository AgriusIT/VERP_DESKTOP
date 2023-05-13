Imports sbModel
Imports sbUtility.Utility
Imports System.Data.SqlClient

Public Class GroupRightsDAL

#Region "Local Functions and Procedures"

   
#End Region

#Region "Public Functions and Procedures"
   
    ''' <summary>
    ''' 
    ''' </summary>
    Public Function Update(ByVal objRights As List(Of SecurityGroupRights)) As Boolean

        Try
            ''check if rights collection is empty
            If objRights Is Nothing Then Return False
            If objRights.Count = 0 Then Return False

            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction

            Try

                Dim strInsertSQL As String = "Insert Into tblSecurityControlRight ( GroupID , ControlID) " _
                & " Values(@GroupID , @ControlID)"

                Dim strDeleteSQL As String = "Delete From tblSecurityControlRight " _
                & " Where GroupID = @GroupID AND ControlID = @ControlID"


                ''setting parameters for the command
                Dim prms(1) As SqlParameter
                prms(0) = SQLHelper.CreateParameter("@GroupID", SqlDbType.BigInt, objRights(0).GroupInfo.GroupID)
                prms(1) = SQLHelper.CreateParameter("@ControlID", SqlDbType.BigInt, Nothing)

                ''iterate the rights collection
                For Each r As SecurityGroupRights In objRights
                    ''assiging values to parameters
                    prms(1).Value = r.ControlID

                    ''delete existing records
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDeleteSQL, prms)
                    ''buil sql Log
                    'objRights(0).ActivityLog.SQLType = "DELETE"
                    'objRights(0).ActivityLog.TableName = "TblSecurityUser"
                    'objRights(0).ActivityLog.SQL = strInsertSQL
                    'UtilityDAL.BuildSQLLog(objRights(0).ActivityLog, trans)

                    If r.IsSelected = True Then
                        Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strInsertSQL, prms))
                        ''buil sql Log
                        'objRights(0).ActivityLog.SQLType = "INSERT"
                        'objRights(0).ActivityLog.TableName = "TblSecurityUser"
                        'objRights(0).ActivityLog.SQL = strInsertSQL
                        'UtilityDAL.BuildSQLLog(objRights(0).ActivityLog, trans)
                    End If
                Next

                ''Activity Log
                'objRights(0).ActivityLog.FormAction = "Update"
                'objRights(0).ActivityLog.LogRef = objRights(0).GroupInfo.GroupID
                'objRights(0).ActivityLog.RefType = "Group ID"
                'UtilityDAL.BuildActivityLog(objRights(0).ActivityLog, trans)

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

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Public Function GetAll(ByVal GroupID As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT     tblSecurityFormControl.ControlID as [Control ID], tblSecurityForm.FORM_CATEGORY as [Form Category], " _
            & " tblSecurityForm.FORM_LABEL as [Form Label], tblSecurityFormControl.ControlCaption [Control Caption], " _
            & " convert(bit, CASE WHEN tblSecurityControlRight.RightsID IS NULL THEN 0 ELSE 1 END) AS [Is Selected]" _
            & " FROM         tblSecurityFormControl INNER JOIN" _
            & " tblSecurityForm ON tblSecurityFormControl.FormID = tblSecurityForm.FORM_ID LEFT OUTER JOIN" _
            & " (SELECT     GroupID, ControlID, RightsID" _
            & " FROM tblSecurityControlRight" _
            & " WHERE      GroupID = " & GroupID & " ) tblSecurityControlRight ON tblSecurityControlRight.ControlID = " _
            & " tblSecurityFormControl.ControlID " _
            & " ORDER BY tblSecurityForm.Sort_Order"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("GroupRights")
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

    Public Function GetAllMenuCategories() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String

            ''If Convert.ToBoolean(GetSystemConfigurationValue("Supplier_Module")) Then
            ''    strSQL = "SELECT DISTINCT FORM_CATEGORY,  SortOrder   FROM   tblSecurityForm WHERE     (FORM_CATEGORY <> 'NULL'  AND FORM_CATEGORY <> 'Purchase') AND IsInUse = 1 " _
            ''    & " ORDER BY SortOrder"
            ''Else

            strSQL = "SELECT DISTINCT FORM_CATEGORY,  Sort_Order      FROM   tblSecurityForm WHERE     FORM_CATEGORY <> 'NULL'   " _
            & " ORDER BY Sort_Order"
            ''End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("MenuCategories")
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
