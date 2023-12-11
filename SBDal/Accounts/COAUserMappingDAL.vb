Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel

Public Class COAUserMappingDAL
    Function Add(ByVal objModel As List(Of COAUserMappingBE)) As Boolean
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
    Function Add(ByVal objMod As List(Of COAUserMappingBE), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As COAUserMappingBE In objMod
                strSQL = "insert into  COAUserMapping (User_Id, COAGroupId) values (N'" & objModel.User_Id & "', N'" & objModel.COAGroupId & "') Select @@Identity"
                objModel.COAGroupUserMappingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Next
            'objMod.ActivityLog.ActivityName = "Save"
            'objMod.ActivityLog.RecordType = "Configuration"
            'objMod.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As COAUserMappingBE) As Boolean
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
    Function Update(ByVal objModel As COAUserMappingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update COAUserMapping set User_Id= N'" & objModel.User_Id & "', COAGroupId= N'" & objModel.COAGroupId & "' where COAGroupUserMappingId=" & objModel.COAGroupUserMappingId
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

    Function Delete(ByVal objModel As COAUserMappingBE) As Boolean
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
    Function Delete(ByVal objModel As COAUserMappingBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from COAUserMapping  where COAGroupUserMappingId= " & objModel.COAGroupUserMappingId
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
            strSQL = " Select COAGroupUserMappingId, User_Id, COAGroupId from COAUserMapping  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select COAGroupUserMappingId, User_Id, COAGroupId from COAUserMapping  where COAGroupUserMappingId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
