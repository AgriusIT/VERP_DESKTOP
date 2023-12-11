
Public Class NotificationConfigurationBE
    Property NotificationId As Integer
    Property EventId As Integer
    Property CategoryId As Integer

    Property Description As String

    Property RolesList As List(Of Roles)

    Property UserGroupList As List(Of UserGroup)

    Property UsersList As List(Of Users)

    Property ActivityLog As ActivityLog

End Class

Public Class Roles
    Property RoleId As Integer

End Class