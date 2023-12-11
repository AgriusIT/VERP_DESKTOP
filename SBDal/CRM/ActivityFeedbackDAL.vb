Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class ActivityFeedbackDAL
    Dim feedbackId As Integer
    Function Add(ByVal objModel As ActivityFeedbackBE, Optional ByRef ActivityfeedbackId As Integer = 0) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans, ActivityfeedbackId)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As ActivityFeedbackBE, trans As SqlTransaction, Optional ByRef ActivityfeedbackId As Integer = 0) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ActivityFeedback (ActivityId, ActivityFeedbackStatusId, FeedbackDate, Reason, Details, NextActionPlan) values (N'" & objModel.ActivityId & "',N'" & objModel.ActivityFeedbackStatusId & "', N'" & objModel.FeedbackDate & "', N'" & objModel.Reason.Replace("'", "''") & "', N'" & objModel.Details.Replace("'", "''") & "', " & objModel.NextActionPlan & ") Select @@Identity"
            feedbackId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Activity Feedback"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "CRM"
            'objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            ActivityfeedbackId = feedbackId
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddPotential(ByVal list As List(Of PotentialBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PotentialBE In list
                str = "INSERT INTO ActivityFeedbackPotential(ActivityFeedbackId, PotentialId) Values(" & feedbackId & ", " & IIf(obj.P_Check = True, obj.PotentialId, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Function Update(ByVal objModel As ActivityFeedbackBE) As Boolean
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
    Function Update(ByVal objModel As ActivityFeedbackBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ActivityFeedback set ActivityId= N'" & objModel.ActivityId & "',ActivityFeedbackStatusId= N'" & objModel.ActivityFeedbackStatusId & "', FeedbackDate= N'" & objModel.FeedbackDate & "', Reason= N'" & objModel.Reason.Replace("'", "''") & "', Details= N'" & objModel.Details.Replace("'", "''") & "', NextActionPlan= N'" & objModel.NextActionPlan & "' where ActivityFeedbackId=" & objModel.ActivityFeedbackId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Activity Feedback"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "CRM"
            'objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePotential(ByVal list As List(Of PotentialBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PotentialBE In list
                str = "If Exists(Select ISNULL(Id, 0) as Id From ActivityFeedbackPotential Where Id=" & obj.Id & ") Update ActivityFeedbackPotential Set ActivityFeedbackId =" & feedbackId & ", PotentialId=" & IIf(obj.P_Check = True, obj.PotentialId, 0) & "  WHERE Id = " & obj.Id & "" _
                 & " Else INSERT INTO ActivityFeedbackPotential(ActivityFeedbackId, PotentialId) Values(" & feedbackId & ", " & IIf(obj.P_Check = True, obj.PotentialId, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As ActivityFeedbackBE) As Boolean
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
    Function Delete(ByVal objModel As ActivityFeedbackBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ActivityFeedback  where ActivityFeedbackId= " & objModel.ActivityFeedbackId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "ActivityFeedback"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "CRM"
            'objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ActivityId, ActivityFeedbackId, ActivityFeedbackStatusId, FeedbackDate, Reason, Details, NextActionPlan from ActivityFeedback  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT ActivityFeedback.ActivityFeedbackId, LeadActivity.ActivityId, ActivityFeedback.ActivityFeedbackStatusId, LeadActivity.ActivityDate, ActivityFeedback.Reason, ActivityFeedback.Details, ActivityFeedback.NextActionPlan, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadActivityTypeID, LeadActivity.ResponsiblePerson_Employee_Id FROM ActivityFeedback RIGHT OUTER JOIN LeadActivity ON ActivityFeedback.ActivityId = LeadActivity.ActivityId  where LeadActivity.ActivityId =" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
