Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class COAAccountMappingDAL

    Public Shared COAGroupMappingId As Integer
    Function Add(ByVal objModel As List(Of COAAccountMappingBE)) As Boolean
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
    Function Add(ByVal objMod As List(Of COAAccountMappingBE), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty

            For Each objModel As COAAccountMappingBE In objMod
                strSQL = "insert into  COAAccountMapping (COAGroupId, AccountId, AccountLevel) values (N'" & objModel.COAGroupId & "', N'" & objModel.AccountId & "', N'" & objModel.AccountLevel & "') Select @@Identity"
                objModel.COAGroupMappingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                COAGroupMappingId = objModel.COAGroupMappingId
            Next
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
           
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
  
    Function Update(ByVal objModel As COAAccountMappingBE) As Boolean
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
    Function Update(ByVal objModel As COAAccountMappingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update COAAccountMapping set COAGroupId= N'" & objModel.COAGroupId & "', AccountId= N'" & objModel.AccountId & "', AccountLevel= N'" & objModel.AccountLevel & "' where COAGroupMappingId=" & objModel.COAGroupMappingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As COAAccountMappingBE) As Boolean
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
    Function Delete(ByVal objModel As COAAccountMappingBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from COAAccountMapping  where COAGroupMappingId= " & objModel.COAGroupMappingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select COAGroupMappingId, COAGroupId, AccountId, AccountLevel from COAAccountMapping  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select COAGroupMappingId, COAGroupId, AccountId, AccountLevel from COAAccountMapping  where COAGroupMappingId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
