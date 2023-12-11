Public Class ActivityFeedbackBE
    Public Property ActivityFeedbackId As Integer
    Public Property ActivityId As Integer
    Public Property ActivityFeedbackStatusId As Integer
    Public Property FeedbackDate As DateTime
    Public Property Reason As String
    Public Property Details As String
    Public Property NextActionPlan As Integer
    Public Property ActivityLog() As ActivityLog

    Public Property Potential_Detail As List(Of PotentialBE)

End Class


Public Class PotentialBE

    Public Property Id As Integer

    Public Property ActivityFeedbackId As Integer

    Public Property PotentialId As Integer

    Public Property P_Check As Boolean

End Class
