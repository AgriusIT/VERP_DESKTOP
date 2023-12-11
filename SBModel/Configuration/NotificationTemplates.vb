Public Class NotificationTemplates
    Public Property NotificationTemplatesId As Integer
    Public Property Subject As String
    Public Property Template As String
    Public Property TemplateDate As DateTime
    Public Property TableName As String
    Public Property NGList As List(Of NotificationGroups)
    Public Property NUList As List(Of NotificationUsers)
    Public Sub New()
        NGList = New List(Of NotificationGroups)
        NUList = New List(Of NotificationUsers)
    End Sub
End Class
