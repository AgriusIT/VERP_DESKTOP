Public Class ApprovalHistoryDetailBE
    Public Property AprovalHistoryDetailId As Integer
    Public Property AprovalHistoryId As Integer
    Public Property StageId As Integer
    Public Property Status As String
    Public Property Remarks As String
    Public Property ApprovalUserId As Integer
    Public Property Level As Integer
    Public Property ApprovalUsersGroupList As List(Of ApprovalUsersGroupBE)
    Public ApprovalRejectionReason As String
    Public ApprovalRejectionReasonId As Integer
    Public Property ApprovalDate As DateTime
    Public Property ActivityLog() As ActivityLog
End Class
