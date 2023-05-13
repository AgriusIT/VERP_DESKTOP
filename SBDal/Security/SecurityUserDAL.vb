Imports sbModel
Imports sbUtility
Imports System.Data.SqlClient

Public Class SecurityUserDAL

#Region "Local Functions and Procedures"
    
    Public Function IsValidateForSave(ByVal objUser As SecurityUser) As Boolean

        Try
            Dim strSQL As String
            strSQL = "select user_log_id from tblSecurityUser Where user_log_id=N'" & objUser.LoginID.Trim.Replace("'", "''") & "'" ''''  AND group_id = " & cboExistingGroups.ItemData(cboExistingGroups.ListIndex) & ""            

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
                If objNameDR.HasRows Then
                    Throw New Exception("Duplicate User Login ID is not allowed ...")
                End If
                Return True
            End Using

        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForUpdate(ByVal objUser As SecurityUser) As Boolean

        Try
            Dim strSQL As String
            strSQL = "select user_log_id from tblSecurityUser where (user_log_id=N'" & objUser.LoginID & "' or user_name=N'" & objUser.UserName.Trim.Replace("'", "''") & "') and group_id = " & objUser.GroupInfo.GroupID & " and user_id <> " & objUser.UserID

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then

                    Throw New Exception("Duplicate User Login ID is not allowed ...")

                End If

                Return True

            End Using

        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Public Functions and Procedures"
    ''' <summary>
    ''' 
    ''' </summary>
    Public Function Add(ByVal objUser As SecurityUser) As Boolean

        Try
            'Check DB Validation, If Not validate then the function returns an exception
            Me.IsValidateForSave(objUser)

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            'Thorw exception if db validation false
            Throw ex

        End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            ' Insert Statement ..
            strSQL = "insert into tblSecurityuser ( group_id, user_name, user_log_id, user_log_password, user_email, user_comments )" _
            & " VALUES ( @GroupID , @UserName, @UserLogID , @UserLogPassword, @UserEmail, " _
            & " @UserComments) " _
            & " Select Ident_Current('tblSecurityuser')"

            ' Setting Parameters For The Command ..
            Dim prms(5) As SqlParameter
            prms(0) = SQLHelper.CreateParameter("@GroupID", SqlDbType.BigInt, objUser.GroupInfo.GroupID)
            prms(1) = SQLHelper.CreateParameter("@UserName", SqlDbType.NVarChar, objUser.UserName.Trim)
            prms(2) = SQLHelper.CreateParameter("@UserLogID", SqlDbType.NVarChar, objUser.LoginID.Trim)
            prms(3) = SQLHelper.CreateParameter("@UserLogPassword", SqlDbType.NVarChar, Utility.EncryptWithALP(objUser.LoginPassword.Trim))
            prms(4) = SQLHelper.CreateParameter("@UserEmail", SqlDbType.NVarChar, objUser.UserEmail.Trim)
            prms(5) = SQLHelper.CreateParameter("@UserComments", SqlDbType.NVarChar, objUser.UserComments.Trim)

            ' Execute SQL 
            objUser.UserID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, prms))

            ' Commit Transaction .. 
            trans.Commit()

            ' Return ..
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

    Public Function Delete(ByVal objUser As SecurityUser) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try

            ''delete user locations
            Dim strSQL As String '[= "DELETE FROM tblGLSecurityUserLocation WHERE (user_id = " & objUser.UserID & ")"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            'delete user
            strSQL = "Delete from tblSecurityUser where user_id = " & objUser.UserID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()
            Return True

        Catch ex As SqlException
            trans.Rollback()
            If ex.Number = -2147217873 Or ex.Number = 547 Then
                Throw New Exception("Unable to Delete this record because related information exists")
            Else : Throw ex
            End If
        Finally
            conn.Close()
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Public Function Update(ByVal objUser As SecurityUser) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            ''update Statement
            strSQL = "UPDATE [tblSecurityuser] SET " _
             & " [User_name] = @UserName , " _
             & " [user_log_id] = @UserLogID ," _
             & " [User_log_Password] =  @UserLogPassword ," _
             & " [USER_EMAIL] = @UserEmail ," _
             & " [USER_COMMENTS] = @UserComments " _
             & " WHERE User_ID = @UserID "

            ''setting parameters for the command
            Dim prms(7) As SqlParameter
            prms(0) = SQLHelper.CreateParameter("@UserName", SqlDbType.NVarChar, objUser.UserName.Trim)
            prms(1) = SQLHelper.CreateParameter("@UserLogID", SqlDbType.NVarChar, objUser.LoginID.Trim)
            prms(2) = SQLHelper.CreateParameter("@UserLogPassword", SqlDbType.NVarChar, Utility.EncryptWithALP(objUser.LoginPassword.Trim))
            prms(3) = SQLHelper.CreateParameter("@UserEmail", SqlDbType.NVarChar, objUser.UserEmail.Trim)
            prms(4) = SQLHelper.CreateParameter("@UserComments", SqlDbType.NVarChar, objUser.UserComments.Trim)
            prms(5) = SQLHelper.CreateParameter("@Block", SqlDbType.Bit, Convert.ToByte(objUser.IsBlocked))
            prms(6) = SQLHelper.CreateParameter("@Mob1ileNo", SqlDbType.NVarChar, objUser.MobileNo.Trim)
            prms(7) = SQLHelper.CreateParameter("@UserID", SqlDbType.Int, objUser.UserID)

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, prms))

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
    Public Function GetAll(ByVal GroupID As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT tblSecurityUser.user_id AS pk_id, tblSecurityUser.group_id , tblSecurityUser.user_name AS [User Name], tblSecurityUser.user_log_id AS [Log ID], tblSecurityUser.user_log_password AS password, tblSecurityUser.user_email [Email],  tblSecurityUser.user_comments AS Comments, End_Date [End Date]  " _
            & " FROM tblSecurityUser " _
            & " Where (tblSecurityUser.group_id = " & GroupID & ")" _
            & " ORDER BY tblSecurityUser.user_name"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("SecurityUsers")
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

    '<summary>

    '</summary>
    'Public Function GetByID(ByVal intUserID As Integer) As SecurityUser

    'End Function


    ''' <summary>
    ''' This function will get an object of SecurityUser class
    ''' and will query to database for the valid user information 
    ''' against the ginven values of loginDI and LoginPassword.
    ''' If userID not found or Password does't match then the function will throw an Exception
    ''' other wise the same class object would be return with details of the login user
    ''' </summary>

    Public Function FindValidUser(ByVal strUserLoginID As String, ByVal strUserLoginPassword As String) As SecurityUser

        Dim objUser As SecurityUser
        Try
            Dim strSQL As String
            strSQL = "  select  b.user_id, b.User_log_id, b.User_log_password, b.User_name, a.group_name" _
                     & " from tblSecurityGroup a, tblSecurityUser b " _
                    & " where b.user_log_id = N'" & strUserLoginID & "'" _
                     & " and a.group_id = b.group_id"

            Using objMyRecords As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objMyRecords.HasRows Then

                    objUser = New SecurityUser

                    objMyRecords.Read()

                    If strUserLoginPassword <> Utility.DecryptWithALP(objMyRecords.Item("User_log_password")) Then
                        ''Throw Excepiton if User Found, but Password is incorrect
                        Throw New Exception("Invalid Password.")

                    Else
                        ''Set all the properties of the object, if record found and password matches
                        objUser.UserID = objMyRecords.Item("user_id")
                        objUser.UserName = objMyRecords.Item("User_name")
                        objUser.LoginID = objMyRecords.Item("User_log_id")
                        objUser.LoginPassword = Utility.DecryptWithALP(objMyRecords.Item("User_log_password"))
                        objUser.GroupInfo.GroupName = objMyRecords.Item("group_name")
                    End If

                Else
                    ''Throw Excepiton if User Not Found
                    Throw New Exception("Login ID not found.")
                End If

            End Using

            Return objUser

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetUserFormRights(ByVal intUserID As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT     tblSecurityForm.FORM_Name as [Form Name], tblSecurityFormControl.ControlCaption as [Control Caption], tblSecurityFormControl.ControlName as [Control Name]" _
            & " FROM         tblSecurityGroup INNER JOIN TblSecurityUser ON tblSecurityGroup.GROUP_ID = TblSecurityUser.GROUP_ID INNER JOIN tblSecurityControlRight ON tblSecurityGroup.GROUP_ID = tblSecurityControlRight.GroupID INNER JOIN tblSecurityFormControl ON tblSecurityControlRight.ControlID = tblSecurityFormControl.ControlID INNER JOIN tblSecurityForm ON tblSecurityFormControl.FormID = tblSecurityForm.FORM_ID " _
            & " WHERE     (TblSecurityUser.User_id = " & intUserID & ")"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("UserFormRights")

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

    Public Function GetUserShops(ByVal objUser As SecurityUser) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            If objUser.GroupInfo.GroupType = 3 Then ' 3 = Shop
                strSQL = "SELECT  c.shop_code as [Shop Code] , c.shop_name  as [Shop Name] , '['+ c.shop_code +']   '+ c.shop_name  as Shop , c.shop_id as [Shop ID], b.User_id as  [User ID], tblDefShopGroups1.field_name as [Group Name] , c.sort_order as [Sort Order], c.shop_code as [Shop Code] FROM tblSecurityGroup a INNER JOIN TblSecurityUser b ON a.GROUP_ID = b.GROUP_ID INNER JOIN tblDefShops c ON b.Shop_id = c.shop_id INNER JOIN tblDefShopGroups d ON c.shop_group_id = d.shop_group_id INNER JOIN tblDefShopGroups tblDefShopGroups1 ON c.shop_group_id = tblDefShopGroups1.shop_group_id WHERE (b.User_id = N'" & objUser.UserID & "') order by  c.sort_order, c.shop_Name"

            ElseIf objUser.GroupInfo.GroupType = 1 Then ' 1 = admin
                strSQL = "SELECT    a.shop_code as [Shop Code] , a.shop_name  as  [Shop Name], '['+ a.shop_code +']   '+ a.shop_name  as Shop  , a.shop_id as [Shop ID],  a.sort_order as [Sort Order] , a.shop_code as [Shop Code]" _
                    & " FROM  tblDefShops a , tblDefShopGroups b " _
                    & " WHERE a.shop_group_id = b.shop_group_id " _
                    & " Order by  a.sort_order, a.shop_Name"

            ElseIf objUser.GroupInfo.GroupType = 2 Then ' 2 = HO  And pbGroupUserForSelectedShops = True Then
                strSQL = "SELECT    tblDefShops.shop_code as [Shop Code] , tblDefShops.shop_name  as  [Shop Name], '['+ tblDefShops.shop_code +']   '+ tblDefShops.shop_name  as Shop  , tblDefShops.shop_id as [Shop ID],  tblDefShops.sort_order as [Sort Order] , tblDefShops.shop_code as [Shop Code] " _
                & " FROM         tblDefShops  INNER JOIN" _
                & "                      tblDefShopGroups ON tblDefShops.shop_group_id = tblDefShopGroups.shop_group_id INNER JOIN" _
                & "                      tblSecurityGroupShops ON tblDefShops.shop_id = tblSecurityGroupShops.Shop_Id INNER JOIN" _
                & "                      tblSecurityGroup ON tblSecurityGroupShops.Group_Id = tblSecurityGroup.GROUP_ID" _
                & " WHERE     (tblSecurityGroup.GROUP_ID =" _
                & "                          (SELECT     TblSecurityUser.group_id" _
                & "                            From TblSecurityUser" _
                & "                            WHERE      User_id = N'" & objUser.UserID & "'))" _
                & " ORDER BY tblDefShops.sort_order,  tblDefShops.shop_name"

            Else
                strSQL = "SELECT    a.shop_code as [Shop Code] , a.shop_name  as  [Shop Name], '['+ a.shop_code +']   '+ a.shop_name  as Shop  , a.shop_id as [Shop ID],  a.sort_order as [Sort Order] , a.shop_code as [Shop Code]  FROM tblDefShops a INNER JOIN tblDefShopGroups ON a.shop_group_id = tblDefShopGroups.shop_group_id ORDER BY a.sort_order, a.Shop_Name"
            End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("abc")

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
    Public Function GetUserList() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT TblSecurityUser.User_name, TblSecurityUser.User_id FROM tblSecurityGroup INNER JOIN " _
                    & " TblSecurityUser ON tblSecurityGroup.GROUP_ID = TblSecurityUser.GROUP_ID " _
                    & " WHERE (tblSecurityGroup.GROUP_TYPE = 1 OR tblSecurityGroup.GROUP_TYPE = 2) " _
                    & " Order By TblSecurityUser.User_name "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable '( "SecurityUsers")
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

    Public Function ChangePassword(ByVal objUser As SecurityUser) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try
            Dim strSQL As String = String.Empty
            'strSQL = "UPDATE tblGLSecurityUser SET user_log_password = N'" & EncryptWithALP(objUser.LoginPassword) _
            '        & "' WHERE user_log_id=N'" & objUser.LoginID & "'"

            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function



#End Region

End Class
