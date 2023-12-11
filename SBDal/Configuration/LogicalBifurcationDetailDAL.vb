Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class LogicalBifurcationDetailDAL
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
    Public Function Add(ByVal obj As LogicalBifurcationBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As LogicalBifurcationDetailBE In obj.Detail
                If objModel.LogicalBifurcationDetailId < 1 AndAlso objModel.IsDeleted = 0 Then
                    strSQL = "INSERT INTO  LogicalBifurcationDetail (LogicalBifurcationId, ToCostCenterId, AmountPercentage, Comments) values (" & obj.LogicalBifurcationId & ", " & objModel.ToCostCenterId & ", " & objModel.Amount & ", N'" & objModel.Comments.Replace("'", "''") & "') "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ElseIf objModel.LogicalBifurcationDetailId > 0 AndAlso objModel.IsDeleted = 0 Then
                    strSQL = "UPDATE LogicalBifurcationDetail SET LogicalBifurcationId = " & obj.LogicalBifurcationId & ", ToCostCenterId=" & objModel.ToCostCenterId & ", AmountPercentage=" & objModel.Amount & ", Comments = N'" & objModel.Comments.Replace("'", "''") & "' WHERE LogicalBifurcationDetailId = " & objModel.LogicalBifurcationDetailId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ElseIf objModel.LogicalBifurcationDetailId > 0 AndAlso objModel.IsDeleted = 1 Then
                    strSQL = " DELETE FROM LogicalBifurcationDetail  WHERE LogicalBifurcationDetailId = " & objModel.LogicalBifurcationDetailId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
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

    Function Update(ByVal objModel As LogicalBifurcationDetailBE) As Boolean
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
    Function Update(ByVal objModel As LogicalBifurcationDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update LogicalBifurcationDetail set LogicalBifurcationId= N'" & objModel.LogicalBifurcationId & "', ToCostCenterId= N'" & objModel.ToCostCenterId & "', Amount= N'" & objModel.Amount & "', Comments= N'" & objModel.Comments.Replace("'", "''") & "' where LogicalBifurcationDetailId=" & objModel.LogicalBifurcationDetailId
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

    Public Function Delete(ByVal objModel As LogicalBifurcationDetailBE) As Boolean
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
    Public Function Delete(ByVal objModel As LogicalBifurcationDetailBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from LogicalBifurcationDetail  where LogicalBifurcationDetailId= " & objModel.LogicalBifurcationDetailId
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
    Public Function DeleteDetail(ByVal LogicalBifurcationId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from LogicalBifurcationDetail  where LogicalBifurcationId= " & LogicalBifurcationId
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

    Public Function GetDetail(ByVal LogicalBifurcationId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select LogicalBifurcationDetailId, LogicalBifurcationId, ToCostCenterId, tblDefCostCenter.Name AS ToCostCenter, AmountPercentage, Comments, 0 AS IsDeleted from LogicalBifurcationDetail  LEFT OUTER JOIN tblDefCostCenter ON LogicalBifurcationDetail.ToCostCenterId = tblDefCostCenter.CostCenterID WHERE LogicalBifurcationId = " & LogicalBifurcationId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select LogicalBifurcationDetailId, LogicalBifurcationId, ToCostCenterId, Amount, Comments from LogicalBifurcationDetail  where LogicalBifurcationDetailId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


