Imports System.Data
Imports System.Data.SqlClient
Imports SBModel

Public Class NotificationConfigurationDAL

    Public Function AddNotification(objModel As NotificationConfigurationBE) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction

        Try
            Dim strSQL As String = String.Empty

            strSQL = "INSERT INTO [dbo].[tblNotificationCenterNotificationList] ([EventId], [Description],[Active],[CreatedByUser],[CreatedDate]) " _
                    & " VALUES(" & objModel.EventId & ", '" & objModel.Description & "' , 1 ," & LoginUser.LoginUserId & " ,getdate())  Select @@Identity "

            Con.Open()
            trans = Con.BeginTransaction()

            objModel.NotificationId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            '// Saving selected roles
            If objModel.RolesList.Count > 0 Then
                For Each Child As Object In objModel.RolesList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListRoles] ([NotificationListId] ,[NotificationRoleId]) VALUES (" & objModel.NotificationId & " ," & Child.RoleId & ")")
                Next
            End If

            '// Saving selected user groups
            If objModel.UserGroupList.Count > 0 Then
                For Each Child As UserGroup In objModel.UserGroupList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListUserGroups] ([NotificationListId] ,[UserGroupId]) VALUES (" & objModel.NotificationId & " ," & Child.GroupId & ")")
                Next
            End If

            '// Saving selected users
            If objModel.UsersList.Count > 0 Then
                For Each Child As Users In objModel.UsersList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListUsers] ([NotificationListId] ,[UserId]) VALUES (" & objModel.NotificationId & " ," & Child.UserId & ")")
                Next
            End If

            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)

            trans.Commit()

            Return True

        Catch ex As Exception
            If Not trans Is Nothing Then
                trans.Rollback()
            End If
            Throw ex

        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Function

    Public Function UpdateNotification(objModel As NotificationConfigurationBE) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction

        Try
            Dim strSQL As String = String.Empty

            strSQL = " Update [tblNotificationCenterNotificationList] set [Description]='" & objModel.Description & "' where NotificationId= " & objModel.NotificationId

            Con.Open()
            trans = Con.BeginTransaction()

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            '// Deleting existing user, user groups and roles data
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Delete from tblNotificationCenterNotificationListRoles where NotificationListId=" & objModel.NotificationId _
                                                            & " Delete from tblNotificationCenterNotificationListUserGroups where NotificationListId=" & objModel.NotificationId _
                                                            & " Delete from tblNotificationCenterNotificationListUsers where NotificationListId=" & objModel.NotificationId)


            '// Saving selected roles
            If objModel.RolesList.Count > 0 Then
                For Each Child As Object In objModel.RolesList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListRoles] ([NotificationListId] ,[NotificationRoleId]) VALUES (" & objModel.NotificationId & " ," & Child.RoleId & ")")
                Next
            End If

            '// Saving selected user groups
            If objModel.UserGroupList.Count > 0 Then
                For Each Child As UserGroup In objModel.UserGroupList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListUserGroups] ([NotificationListId] ,[UserGroupId]) VALUES (" & objModel.NotificationId & " ," & Child.GroupId & ")")
                Next
            End If

            '// Saving selected users
            If objModel.UsersList.Count > 0 Then
                For Each Child As Users In objModel.UsersList
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [tblNotificationCenterNotificationListUsers] ([NotificationListId] ,[UserId]) VALUES (" & objModel.NotificationId & " ," & Child.UserId & ")")
                Next
            End If


            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)

            trans.Commit()

            Return True

        Catch ex As Exception
            If Not trans Is Nothing Then
                trans.Rollback()
            End If
            Throw ex

        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Function

    Public Function DeleteNotification(NotificationId As Integer) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction

        Try
            Dim strSQL As String = String.Empty

            Con.Open()
            trans = Con.BeginTransaction()

            '// Deleting existing user, user groups and roles data
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Delete from tblNotificationCenterNotificationListRoles where NotificationListId=" & NotificationId _
                                                            & " Delete from tblNotificationCenterNotificationListUserGroups where NotificationListId=" & NotificationId _
                                                            & " Delete from tblNotificationCenterNotificationListUsers where NotificationListId=" & NotificationId _
                                                            & " Delete from [tblNotificationCenterNotificationList] where NotificationId= " & NotificationId)


            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)

            trans.Commit()

            Return True

        Catch ex As Exception
            If Not trans Is Nothing Then
                trans.Rollback()
            End If
            Throw ex

        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Function

    Public Function GetNotificationUsers(EventName As String) As List(Of NotificationDetail)
        Try

            Dim objList As New List(Of NotificationDetail)

            Dim strSQL As String = "SELECT        tblNotificationCenterNotificationListUsers.UserId " _
                                & " FROM            tblNotificationCenterEvents INNER JOIN " _
                                & " tblNotificationCenterNotificationList ON tblNotificationCenterEvents.EventId = tblNotificationCenterNotificationList.EventId INNER JOIN " _
                                & " tblNotificationCenterNotificationListUsers ON tblNotificationCenterNotificationList.NotificationId = tblNotificationCenterNotificationListUsers.NotificationListId " _
                                & " Where EventTitle='" & EventName & "'"


            Dim dr As  SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dr.HasRows Then
                While dr.Read
                    Dim objDetail As New NotificationDetail
                    objDetail.NotificationUser = New SecurityUser(Val(dr.Item(0).ToString))
                    objList.Add(objDetail)
                End While
            End If

            Return objList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNotificationGroups(EventName As String) As List(Of NotificationDetail)
        Try
            Dim objList As New List(Of NotificationDetail)

            Dim strSQL As String = "SELECT        tblNotificationCenterNotificationListUserGroups.UserGroupId " _
                                 & " FROM            tblNotificationCenterEvents INNER JOIN " _
                                 & " tblNotificationCenterNotificationList ON tblNotificationCenterEvents.EventId = tblNotificationCenterNotificationList.EventId INNER JOIN " _
                                 & " tblNotificationCenterNotificationListUserGroups ON tblNotificationCenterNotificationList.NotificationId = tblNotificationCenterNotificationListUserGroups.NotificationListId " _
                                 & " Where EventTitle='" & EventName & "'"


            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dr.HasRows Then
                While dr.Read
                    Dim objDetail As New NotificationDetail
                    objDetail.GroupId = Val(dr.Item(0).ToString)
                    objList.Add(objDetail)
                End While
            End If

            Return objList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNotificationRoles(EventName As String) As List(Of Object)
        Try
            Dim objList As New List(Of Object)

            Dim strSQL As String = "SELECT        tblNotificationCenterNotificationListRoles.Id, tblNotificationCenterNotificationListRoles.NotificationListId, tblNotificationCenterNotificationListRoles.NotificationRoleId, tblNotificationCenterRolesList.RoleTitle " _
                                 & " FROM            tblNotificationCenterEvents INNER JOIN " _
                                 & " tblNotificationCenterNotificationList ON tblNotificationCenterEvents.EventId = tblNotificationCenterNotificationList.EventId INNER JOIN " _
                                 & " tblNotificationCenterNotificationListRoles ON tblNotificationCenterNotificationList.NotificationId = tblNotificationCenterNotificationListRoles.NotificationListId INNER JOIN " _
                                 & " tblNotificationCenterRolesList ON tblNotificationCenterNotificationListRoles.NotificationRoleId = tblNotificationCenterRolesList.RoleId " _
                                 & " Where EventTitle='" & EventName & "'"

            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dr.HasRows Then
                While dr.Read
                    Dim ObjDetail As New Object
                    ObjDetail.RoleTitle = dr.Item(0).ToString
                    objList.Add(objDetail)
                End While
            End If

            Return objList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNotificationRoles1(EventName As String) As List(Of String)
        Try
            Dim objList As New List(Of String)

            Dim strSQL As String = "SELECT        tblNotificationCenterNotificationListRoles.Id, tblNotificationCenterNotificationListRoles.NotificationListId, tblNotificationCenterNotificationListRoles.NotificationRoleId, tblNotificationCenterRolesList.RoleTitle " _
                                 & " FROM            tblNotificationCenterEvents INNER JOIN " _
                                 & " tblNotificationCenterNotificationList ON tblNotificationCenterEvents.EventId = tblNotificationCenterNotificationList.EventId INNER JOIN " _
                                 & " tblNotificationCenterNotificationListRoles ON tblNotificationCenterNotificationList.NotificationId = tblNotificationCenterNotificationListRoles.NotificationListId INNER JOIN " _
                                 & " tblNotificationCenterRolesList ON tblNotificationCenterNotificationListRoles.NotificationRoleId = tblNotificationCenterRolesList.RoleId " _
                                 & " Where EventTitle='" & EventName & "'"

            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dr.HasRows Then
                While dr.Read
                    Dim ObjDetail As String
                    ObjDetail = dr.Item("RoleTitle").ToString
                    objList.Add(ObjDetail)
                End While
            End If

            Return objList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
