Public Class frmApprovalHistory
    Public DocumentNo As String = String.Empty
    Public Source As String = String.Empty
    Public Shared dt As DataTable
    Private Sub frmApprovalHistory_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAll()
        Try
            Dim str As String = "SELECT     ApprovalStages.Title AS Stage, ApprovalHistoryDetail.Status, tblUser.FullName As [User] , ApprovalHistoryDetail.ApprovalDate AS Date, Case when ApprovalHistoryDetail.Status = 'Rejected' then ApprovalHistoryDetail.ApprovalRejectionReason When ApprovalHistoryDetail.Status = 'None' Then  '' else ApprovalHistoryDetail.Remarks End AS Details" _
                                & " FROM    ApprovalStages INNER JOIN " _
                     & " ApprovalHistoryDetail ON ApprovalStages.ApprovalStagesId = ApprovalHistoryDetail.StageId INNER JOIN " _
                      & "ApprovalHistory ON ApprovalHistoryDetail.AprovalHistoryId = ApprovalHistory.ApprovalHistoryId LEFT OUTER JOIN " _
                     & "  tblUser ON ApprovalHistoryDetail.ApprovalUserId = tblUser.User_ID where 1=1 "

            If DocumentNo.Length > 0 Then

                str = str & " and  (ApprovalHistory.DocumentNo = '" & DocumentNo & "') "
            Else
                ShowInformationMessage("No Record Found Against this Document") : Exit Sub
            End If
            If Source.Length > 0 Then
                str = str & " and  (ApprovalHistory.Source = '" & Source & "') "
            End If
            str += " order by AprovalHistoryDetailId "
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
            Me.grd.RootTable.Columns("Details").Width = 150
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class