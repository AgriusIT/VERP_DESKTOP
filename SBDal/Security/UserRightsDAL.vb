Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class UserRightsDAL
    Public Function Add(ByVal Rights As List(Of Rights)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            For Each RightsDetail As Rights In Rights
                Dim dt As DataTable = ExistingRecordByGroup(RightsDetail.GroupId, RightsDetail.FormId, RightsDetail.FormControlId, trans)
                If dt Is Nothing Then Return False
                If dt.Rows.Count < 1 Then
                    str = String.Empty
                    str = "INSERT INTO tblRights(GroupId, FormId, FormControlId,Rights) Values(" & RightsDetail.GroupId & ", " & RightsDetail.FormId & ", " & RightsDetail.FormControlId & ", " & IIf(RightsDetail.Rights = True, 1, 0) & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = String.Empty
                    str = "UPDATE tblRights SET Rights=" & IIf(RightsDetail.Rights = True, 1, 0) & " WHERE GroupId=" & RightsDetail.GroupId & " AND FormId=" & RightsDetail.FormId & " AND FormControlId=" & RightsDetail.FormControlId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddNotification(ByVal Notifications As List(Of NotificationActivityConfig)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            Dim typeId As Integer = 0
            str = String.Empty

            For Each item As NotificationActivityConfig In Notifications
                If item.NotificationActivityDetailId = 0 Then
                    str = String.Empty
                    str = "INSERT INTO tblNotificationActivityConfig(NotificationActivityId,FormId,GroupID,SMS,Email,Notifications) Values(" & item.NotificationActivityId & "," & item.FormId & "," & item.GroupId & "," & IIf(item.SMS = True, 1, 0) & "," & IIf(item.Email = True, 1, 0) & "," & IIf(item.Notifications = True, 1, 0) & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = String.Empty
                    str = "UPDATE tblNotificationActivityConfig Set NotificationActivityId=" & item.NotificationActivityId & ",FormId=" & item.FormId & ",GroupID=" & item.GroupId & ",SMS=" & IIf(item.SMS = True, 1, 0) & ",Email=" & IIf(item.Email = True, 1, 0) & ",Notifications=" & IIf(item.Notifications = True, 1, 0) & " WHERE NotificationActivityDetailId=" & item.NotificationActivityDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                End If
            Next

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'Public Function AddDetail(ByVal RightsDetail As List(Of RightsDetail)) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        Dim str As String = String.Empty
    '        For Each RightsList As RightsDetail In RightsDetail
    '            Dim dt As DataTable = ExistingRecord(RightsList.GroupId, RightsList.UserId, RightsList.FormId, RightsList.FormControlId, trans)
    '            If Not dt Is Nothing Then
    '                If dt.Rows.Count = 0 Then
    '                    str = String.Empty
    '                    str = "INSERT INTO tblRightsDetail(GroupId,UserId,FormId,FormControlId,Rights) Values(" & RightsList.GroupId & ", " & RightsList.UserId & ", " & RightsList.FormId & ", " & RightsList.FormControlId & "," & IIf(RightsList.Rights = True, 1, 0) & ")"
    '                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '                Else
    '                    str = String.Empty
    '                    str = "UPDATE tblRightsDetail SET Rights=" & IIf(RightsList.Rights = True, 1, 0) & " WHERE GroupId=" & RightsList.GroupId & " AND UserId=" & RightsList.UserId & " AND FormId=" & RightsList.FormId & " AND FormControlId=" & RightsList.FormControlId & ""
    '                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '                End If
    '            Else
    '                str = String.Empty
    '                str = "INSERT INTO tblRightsDetail(GroupId,UserId,FormId,FormControlId,Rights) Values(" & RightsList.GroupId & ", " & RightsList.UserId & ", " & RightsList.FormId & ", " & IIf(RightsList.FormControlId = True, 1, 0) & ")"
    '                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '            End If
    '        Next
    '        trans.Commit()
    '        Return True
    '    Catch ex As SqlException
    '        trans.Rollback()
    '        Throw ex
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    Public Function GetRights(ByVal Groupid As Integer, ByVal MainMenu As String) As DataTable
        Try
            Dim _strquery As String = " SELECT ISNULL(Rights.GroupId, 0) AS GroupId, a.FormId, a.FormControlId,b.FormModule, b.FormCaption, a.FormControlName, ISNULL(Rights.Rights, 0) AS Rights " _
               & " FROM dbo.tblFormsControls a INNER JOIN " _
               & " dbo.tblForms b ON a.FormId = b.FormId LEFT OUTER JOIN " _
               & " (SELECT ISNULL(GroupId, 0) AS GroupId, FormControlId, ISNULL(Rights, 0) AS Rights " _
               & " FROM tblRights  " _
               & " WHERE GroupId =" & Groupid & ") Rights ON Rights.FormControlId = a.FormControlId"
            _strquery += "" & IIf(MainMenu.Length > 0, " WHERE b.FormModule IN (" & MainMenu & ") ", " WHERE b.FormModule = '' ") & ""

            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNotifications(ByVal Groupid As Integer, ByVal MainMenu As String) As DataTable
        Try

            'Dim _strquery As String = " SELECT ISNULL(tblNotificationTemplate.ID, 0) AS ID, tblNotificationTemplate.Template, dbo.tblNotificationType.TypeId, dbo.tblNotificationType.TemplateId, ISNULL(dbo.tblNotificationType.SMS, 0) AS SMS, ISNULL(dbo.tblNotificationType.Email, 0) AS Email, " _
            '   & " ISNULL(dbo.tblNotificationType.[System Notification], 0) AS [System Notification], dbo.tblNotificationType.NotificationId, Notifications.FormModule " _
            '   & " FROM dbo.tblNotificationType Right outer join " _
            '   & " tblNotificationTemplate ON dbo.tblNotificationType.TemplateId = tblNotificationTemplate.ID LEFT OUTER JOIN " _
            '   & " (SELECT NotificationId, FormId, FormModule, GroupId, UserID From dbo.tblNotifications WHERE GroupId =" & Groupid & "" & IIf(MainMenu.Length > 0, " And FormModule IN (" & MainMenu & ") ", "") & " ) As Notifications " _
            '   & " ON dbo.tblNotificationType.NotificationId = Notifications.NotificationId "
            '  _strquery += "" & IIf(MainMenu.Length > 0, " WHERE Notifications.FormModule IN (" & MainMenu & ") ", "") & ""
            Dim _strquery As String = String.Empty
            _strquery = "SELECT IsNull(Notification_Config.NotificationActivityDetailId,0) as NotificationActivityDetailId, dbo.tblNotificationActivity.NotificationActivityId, dbo.tblNotificationActivity.FormId, dbo.tblForms.FormModule as Module, dbo.tblForms.FormCaption as Form, " _
                        & " dbo.tblNotificationActivity.NotificationActivityName as Activity, ISNULL(Notification_Config.SMS, 0) AS SMS, ISNULL(Notification_Config.Email, 0) AS Email, " _
                        & " ISNULL(Notification_Config.Notifications, 0) AS Notifications " _
                        & " FROM dbo.tblForms INNER JOIN dbo.tblNotificationActivity ON dbo.tblForms.FormId = dbo.tblNotificationActivity.FormId LEFT OUTER JOIN " _
                        & " (SELECT IsNull(NotificationActivityDetailId,0) as NotificationActivityDetailId, IsNull(NotificationActivityId,0) as NotificationActivityId, FormId, SMS, Email, Notifications " _
                        & " FROM dbo.tblNotificationActivityConfig WHERE GroupId=" & Groupid & ") AS Notification_Config ON  " _
                        & " Notification_Config.NotificationActivityId = dbo.tblNotificationActivity.NotificationActivityId AND Notification_Config.FormId = dbo.tblNotificationActivity.FormId " & IIf(MainMenu.Length > 0, "WHERE tblForms.FormModule in(" & MainMenu & ")", "") & ""

            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function Delete(ByVal Rights As RightsDetail) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        Dim str As String = "Delete From tblRightsDetail WHERE GroupId=" & Rights.GroupId & " "
    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

    '    Catch ex As SqlException
    '        trans.Rollback()
    '        Throw ex
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    End Try
    'End Function
    'Public Function ExistingRecord(ByVal GroupId As Integer, ByVal UserId As Integer, ByVal FormId As Integer, ByVal FormControlId As Integer, ByVal trans As SqlTransaction) As DataTable
    '    Try
    '        Dim str As String = "Select * From tblRightsDetail WHERE GroupId=" & GroupId & " AND UserId=" & UserId & " AND FormId=" & FormId & " AND FormControlId=" & FormControlId & ""
    '        Return UtilityDAL.GetDataTable(str, trans)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function ExistingRecordByGroup(ByVal GroupId As Integer, ByVal FormId As Integer, ByVal FormControlId As Integer, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim str As String = "Select * From tblRights WHERE GroupId=" & GroupId & " AND FormId=" & FormId & " AND FormControlId=" & FormControlId & ""
            Return UtilityDAL.GetDataTable(str, trans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
