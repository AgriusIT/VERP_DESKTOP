Imports System.Data.SqlClient
Imports SBModel
Public Class NotificationDAL
    Public Function AddNotification(ByVal GNotifications As List(Of AgriusNotifications)) As Boolean
        Dim con As SqlConnection
        Dim trans As SqlTransaction

        Try

            con = New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            trans = con.BeginTransaction

            AddNotification(GNotifications, trans)

            trans.Commit()
            Return True

        Catch ex As Exception

            If Not trans Is Nothing Then
                trans.Rollback()
            End If
            Throw ex

        Finally

            If Not con Is Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If

        End Try
    End Function

    Public Function AddNotification(ByVal GNotifications As List(Of AgriusNotifications), ByVal Trans As SqlTransaction) As Boolean

        Try

            Dim str As String = String.Empty
            Dim typeId As Integer = 0
            str = String.Empty

            For Each item As AgriusNotifications In GNotifications

                str = String.Empty
                str = " INSERT INTO tblGluonNotifications (NotificationDate, NotificationTitle, NotificationDescription, SourceApplication, ApplicationReference, ExpireOn) " _
                    & " VALUES        (getdate(),'" & item.NotificationTitle & "','" & item.NotificationDescription & "','" & item.SourceApplication & "','" & item.ApplicationReference & "','" & Date.Now.AddMonths(1).ToString("dd-MMM-yyyy 23:59:59") & "') Select @@Identity "

                item.NotificationId = SQLHelper.ExecuteScaler(Trans, CommandType.Text, str)

                If item.NotificationDetils Is Nothing Then

                    str = "INSERT INTO tblGluonNotificationDetail (NotificationId, UserId, UserGroup, ReadStatus, ClearStatus) SELECT " & item.NotificationId & ",  User_ID, 0,0 FROM tblUser WHERE (Active = 1)"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)

                Else

                    For Each Detail As NotificationDetail In item.NotificationDetils
                        str = "INSERT INTO tblGluonNotificationDetail (NotificationId, UserId, UserGroup, ReadStatus, ClearStatus) " _
                            & " VALUES (" & item.NotificationId & "," & Detail.NotificationUser.UserID & ", " & Detail.GroupId & ",0,0) "
                        SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                    Next

                End If

            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetPendingNotificationsCount(UserId As Integer) As Integer

        Try

            Dim strSQL As String = String.Empty
            strSQL = " SELECT        count(tblGluonNotifications.NotificationId) as NotificationCount  FROM            tblGluonNotificationDetail INNER JOIN " _
                    & " tblGluonNotifications ON tblGluonNotificationDetail.NotificationId = tblGluonNotifications.NotificationId " _
                    & " WHERE        (tblGluonNotificationDetail.ClearStatus = 0) AND (tblGluonNotificationDetail.UserId = " & UserId & ") AND (tblGluonNotifications.ExpireOn > GETDATE()) AND (tblGluonNotificationDetail.ReadStatus = 0) "
            Dim dt As New DataTable
            Dim adp As New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            adp.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetNotificationsList(UserId As Integer) As DataTable

        Try

            Dim strSQL As String = String.Empty
            strSQL = " SELECT        tblGluonNotifications.NotificationId, tblGluonNotifications.NotificationDate, tblGluonNotifications.NotificationTitle, " _
                    & " tblGluonNotifications.NotificationDescription, tblGluonNotifications.SourceApplication, tblGluonNotifications.ApplicationReference, tblGluonNotificationDetail.ReadStatus FROM  tblGluonNotificationDetail INNER JOIN " _
                    & " tblGluonNotifications ON tblGluonNotificationDetail.NotificationId = tblGluonNotifications.NotificationId " _
                    & " WHERE        (tblGluonNotificationDetail.ClearStatus = 0) AND (tblGluonNotificationDetail.UserId = " & UserId & ") AND (tblGluonNotifications.ExpireOn > GETDATE()) order by 1 "
            Dim dt As New DataTable
            Dim adp As New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            adp.Fill(dt)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function MarkNotificationRead(UserId As Integer) As Boolean

        Try

            Dim strSQL As String = String.Empty
            strSQL = " update tblGluonNotificationDetail set tblGluonNotificationDetail.ReadStatus = 1 WHERE tblGluonNotificationDetail.ReadStatus = 0 AND (tblGluonNotificationDetail.UserId = " & UserId & ")  "
            SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL)
            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
