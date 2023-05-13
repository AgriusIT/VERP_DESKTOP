Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class ApprovalHistoryDAL
    Function Add(ByVal objModel As ApprovalHistoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As ApprovalHistoryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty

            strSQL = "insert into  ApprovalHistory (AprovalProcessId, ReferenceType, ReferenceId, DocumentNo, DocumentDate, Description, CurrentStage, Status , LogUserID , Source , voucher_type_id) values (N'" & objModel.AprovalProcessId & "', N'" & objModel.ReferenceType.Replace("'", "''") & "', N'" & objModel.ReferenceId & "', N'" & objModel.DocumentNo.Replace("'", "''") & "', N'" & objModel.DocumentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.Description.Replace("'", "''") & "', N'" & objModel.CurrentStage.Replace("'", "''") & "', N'" & objModel.Status.Replace("'", "''") & "'," & objModel.LogUserID & ", N'" & objModel.Source.Replace("'", "''") & "'," & objModel.voucher_type_id & ") Select @@Identity"
            objModel.ApprovalHistoryId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddDetail(objModel.ApprovalHistoryId, objModel, trans)
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddDetail(ByVal ApprovalHistoryId As Integer, ByVal ApprovalHistory As ApprovalHistoryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            Dim ApprovalHistoryDetailList As List(Of ApprovalHistoryDetailBE) = ApprovalHistory.ApprovalHistoryDetailList
            For Each objModel As ApprovalHistoryDetailBE In ApprovalHistoryDetailList
                strSQL = "insert into  ApprovalHistoryDetail (AprovalHistoryId, StageId, Status, Remarks, ApprovalUserId, Level) values (N'" & ApprovalHistoryId & "', N'" & objModel.StageId & "', N'" & objModel.Status.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.ApprovalUserId & "'," & objModel.Level & ") Select @@Identity"
                objModel.AprovalHistoryDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                strSQL = " insert into ApprovalUsersGroup (ApprovalHistoryDetailID, GroupId) " _
                      & " SELECT " & objModel.AprovalHistoryDetailId & ", ApprovalUsersMapping.GroupId FROM  ApprovalStagesMappping INNER JOIN  " _
                      & "ApprovalUsersMapping ON ApprovalStagesMappping.ApprovalStagesMapppingId = ApprovalUsersMapping.ApprovalStagesMapppingId " _
                      & "WHERE ApprovalStagesMappping.ApprovalProcessId = " & ApprovalHistory.AprovalProcessId & " And ApprovalStagesMappping.Level = " & objModel.Level & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
           

                'objModel.ActivityLog.ActivityName = "Save"
                'objModel.ActivityLog.RecordType = "Configuration"
                'objModel.ActivityLog.RefNo = ""
                'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)

                'Dim Key As String
                'If 1 = 1 Then
                '    Key = "AccountApproval"
                'End If
                'UtilityDAL.GetConfigValue(Key)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ApprovalHistoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As ApprovalHistoryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ApprovalHistory set AprovalProcessId= N'" & objModel.AprovalProcessId & "', ReferenceType= N'" & objModel.ReferenceType.Replace("'", "''") & "', ReferenceId= N'" & objModel.ReferenceId & "', DocumentNo= N'" & objModel.DocumentNo.Replace("'", "''") & "', DocumentDate= N'" & objModel.DocumentDate & "', Description= N'" & objModel.Description.Replace("'", "''") & "', CurrentStage= N'" & objModel.CurrentStage.Replace("'", "''") & "', Status= N'" & objModel.Status.Replace("'", "''") & "' where ApprovalHistoryId=" & objModel.ApprovalHistoryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ApprovalHistoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As ApprovalHistoryBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ApprovalHistory  where ApprovalHistoryId= " & objModel.ApprovalHistoryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ApprovalHistoryId, AprovalProcessId, ReferenceType, ReferenceId, DocumentNo, DocumentDate, Description, CurrentStage, Status from ApprovalHistory  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ApprovalHistoryId, AprovalProcessId, ReferenceType, ReferenceId, DocumentNo, DocumentDate, Description, CurrentStage, Status from ApprovalHistory  where ApprovalHistoryId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
