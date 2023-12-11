Public Class ApprovalHistoryBE
    Public Property ApprovalHistoryId As Integer
    Public Property AprovalProcessId As Integer
    Public Property voucher_type_id As Integer ''TFS2804
    Public Property ReferenceType As String
    Public Property ReferenceId As Integer
    Public Property DocumentNo As String
    Public Property DocumentDate As DateTime
    Public Property Description As String
    Public Property CurrentStage As String
    Public Property Status As String
    Public Property Source As String ''TFS2804
    Public Property ApprovalHistoryDetailList As List(Of ApprovalHistoryDetailBE)
    Public Property LogUserID As Integer
    Public Property ActivityLog() As ActivityLog
End Class
