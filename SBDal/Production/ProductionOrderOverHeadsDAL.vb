Imports SBModel
Imports System.Data.SqlClient
Public Class ProductionOrderOverHeadsDAL
    Function Add(ByVal objModel As ProductionOrderOverHeadsBE) As Boolean
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
    Function Add(ByVal objModel As ProductionOrderOverHeadsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ProductionOrderOverHeads (AccountId, Amount) values (N'" & objModel.AccountId & "', N'" & objModel.Amount & "') Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal Obj As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ProductionOrderOverHeadsBE In Obj.OverHeadList
                If objModel.ID = 0 Then
                    strSQL = "insert into  ProductionOrderOverHeads (ProductionOrderId, AccountId, Amount) values (" & Obj.ProductionOrderId & ", " & objModel.AccountId & ",  " & objModel.Amount & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = " Update ProductionOrderOverHeads SET ProductionOrderId = " & Obj.ProductionOrderId & ", AccountId = " & objModel.AccountId & " , Amount = " & objModel.Amount & " WHERE ID = " & objModel.ID & ""
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

    Function Update(ByVal objModel As ProductionOrderOverHeadsBE) As Boolean
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
    Function Update(ByVal objModel As ProductionOrderOverHeadsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProductionOrderOverHeads set AccountId= N'" & objModel.AccountId & "', Amount= N'" & objModel.Amount & "' where ID=" & objModel.ID
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

    Public Shared Function Delete(ByVal objModel As ProductionOrderOverHeadsBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As ProductionOrderOverHeadsBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrderOverHeads  where ID= " & objModel.ID
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
    Public Shared Function Delete(ByVal ProductionOrderId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrderOverHeads  where ProductionOrderId= " & ProductionOrderId
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
            strSQL = " Select ID, AccountId, Amount from ProductionOrderOverHeads  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, AccountId, Amount from ProductionOrderOverHeads  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetOverHeads(ByVal ProductionOrderId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select OverHeads.ID, OverHeads.ProductionOrderId, OverHeads.AccountId, Account.detail_title AS Account, OverHeads.Amount from ProductionOrderOverHeads AS OverHeads  INNER JOIN vwCOADetail AS Account ON OverHeads.AccountId = Account.coa_detail_id " _
                   & " WHERE OverHeads.ProductionOrderId = " & ProductionOrderId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
