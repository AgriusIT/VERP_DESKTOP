Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ApprovalProcessDAL
    Function Add(ByVal objModel As ApprovalProcessBE) As Boolean
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
    Function Add(ByVal objModel As ApprovalProcessBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ApprovalProcess (Title, Active, SortOrder, Details) values (N'" & objModel.Title.Replace("'", "''") & "', N'" & objModel.Active & "', N'" & objModel.SortOrder & "', N'" & objModel.Details.Replace("'", "''") & "') Select @@Identity"
            objModel.ApprovalProcessId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Approval Process"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Utility"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ApprovalProcessBE) As Boolean
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
    Function Update(ByVal objModel As ApprovalProcessBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ApprovalProcess set Title= N'" & objModel.Title.Replace("'", "''") & "', Active= N'" & objModel.Active & "', SortOrder= N'" & objModel.SortOrder & "', Details= N'" & objModel.Details.Replace("'", "''") & "' where ApprovalProcessId=" & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Approval Process"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Utility"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ApprovalProcessBE) As Boolean
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
    Function Delete(ByVal objModel As ApprovalProcessBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ApprovalProcess  where ApprovalProcessId= " & objModel.ApprovalProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Approval Rejection Reason"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Utility"
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
            strSQL = " Select ApprovalProcessId, Title,  Details , Active, SortOrder from ApprovalProcess order By SortOrder  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ApprovalProcessId, Title, Active, SortOrder, Details from ApprovalProcess  where ApprovalProcessId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
