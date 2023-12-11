Imports SBModel
Imports System.Data
Imports System.Data.SqlClient

Public Class LogicalBifurcationDAL
    Public Function Add(ByVal objModel As LogicalBifurcationBE) As Boolean
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
    Public Function Add(ByVal objModel As LogicalBifurcationBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  LogicalBifurcation (DocumentNo, DocumentDate, StartDate, FromCostCenterId, Remarks) values (N'" & objModel.DocumentNo.Replace("'", "''") & "', N'" & objModel.DocumentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.StartDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.FromCostCenterId & ", N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
            objModel.LogicalBifurcationId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            Call New LogicalBifurcationDetailDAL().Add(objModel, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As LogicalBifurcationBE) As Boolean
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
    Function Update(ByVal objModel As LogicalBifurcationBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update LogicalBifurcation set DocumentNo= N'" & objModel.DocumentNo.Replace("'", "''") & "', DocumentDate= N'" & objModel.DocumentDate.ToString("yyyy-M-d h:mm:ss tt") & "', StartDate= N'" & objModel.StartDate.ToString("yyyy-M-d h:mm:ss tt") & "', FromCostCenterId= " & objModel.FromCostCenterId & ", Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' where LogicalBifurcationId=" & objModel.LogicalBifurcationId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            Call New LogicalBifurcationDetailDAL().Add(objModel, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As LogicalBifurcationBE) As Boolean
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
    Function Delete(ByVal objModel As LogicalBifurcationBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from LogicalBifurcation  where LogicalBifurcationId= " & objModel.LogicalBifurcationId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            Call New LogicalBifurcationDetailDAL().DeleteDetail(objModel.LogicalBifurcationId, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select LogicalBifurcationId, DocumentNo, DocumentDate, StartDate, FromCostCenterId, tblDefCostCenter.Name AS FromCostCenter, Remarks from LogicalBifurcation LEFT OUTER JOIN tblDefCostCenter ON LogicalBifurcation.FromCostCenterId = tblDefCostCenter.CostCenterID  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select LogicalBifurcationId, DocumentNo, DocumentDate, StartDate, FromCostCenterId, Remarks from LogicalBifurcation  where LogicalBifurcationId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


