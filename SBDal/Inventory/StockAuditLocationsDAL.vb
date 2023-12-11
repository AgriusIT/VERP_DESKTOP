Imports SBModel
Imports System.Data.SqlClient

Public Class StockAuditLocationsDAL
    Function Add(ByVal objModel As StockAuditTableBE) As Boolean
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
    Function Add(ByVal obj As StockAuditTableBE, trans As SqlTransaction) As Boolean
        Try
            For Each objModel As StockAuditLocationsBE In obj.Locations
                Dim strSQL As String = String.Empty
                strSQL = "insert into  StockAuditLocations (LocationId, StockAuditId) values (" & objModel.LocationId & ", " & obj.ID & ") Select @@Identity"
                objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
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

    Function Update(ByVal objModel As StockAuditLocationsBE) As Boolean
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
    Function Update(ByVal objModel As StockAuditLocationsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update StockAuditLocations set LocationId= N'" & objModel.LocationId & "', StockAuditId= N'" & objModel.StockAuditId & "' where ID=" & objModel.ID
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

    Function Delete(ByVal StockAuditId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(StockAuditId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal StockAuditId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from StockAuditLocations  WHERE StockAuditId= " & StockAuditId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll(ByVal StockAuditId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, LocationId, StockAuditId from StockAuditLocations INNER JOIN tblDefLocation AS Location ON StockAuditLocations.LocationId = Location.location_id WHERE StockAuditId = " & StockAuditId & "  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, LocationId, StockAuditId from StockAuditLocations  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

