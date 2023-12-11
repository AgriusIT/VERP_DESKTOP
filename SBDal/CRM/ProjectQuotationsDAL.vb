Imports SBDal
Imports SBModel
Imports System.Data.SqlClient
Public Class ProjectQuotationsDAL
    Function Add(ByVal objModel As ProjectQuotationsBE) As Boolean
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
    Function Add(ByVal objModel As ProjectQuotationsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  tblDefProjectQuotations (QuotationId, ProjectId) values (N'" & objModel.QuotationId & "', N'" & objModel.ProjectId & "') Select @@Identity"
            objModel.ProjectQuotaionID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ProjectQuotationsBE) As Boolean
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
    Function Update(ByVal objModel As ProjectQuotationsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDefProjectQuotations set QuotationId= N'" & objModel.QuotationId & "', ProjectId= N'" & objModel.ProjectId & "' where ProjectQuotaionID=" & objModel.ProjectQuotaionID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ProjectQuotationsBE) As Boolean
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
    Function Delete(ByVal objModel As ProjectQuotationsBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblDefProjectQuotations  where ProjectQuotaionID= " & objModel.ProjectQuotaionID
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
            strSQL = " Select ProjectQuotaionID, QuotationId, ProjectId from tblDefProjectQuotations  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProjectQuotaionID, QuotationId, ProjectId from tblDefProjectQuotations  where ProjectQuotaionID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
