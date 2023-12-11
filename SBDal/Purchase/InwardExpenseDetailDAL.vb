Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class InwardExpenseDetailDAL
    Function Add(ByVal objModel As List(Of InwardExpenseDetailBE), ByVal PurchaseId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, PurchaseId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objMod As List(Of InwardExpenseDetailBE), ByVal PurchaseId As Integer, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As InwardExpenseDetailBE In objMod
                strSQL = "insert into  InwardExpenseDetailTable (PurchaseId, AccountId, Exp_Amount, DocType) values (N'" & PurchaseId & "', N'" & objModel.AccountId & "', N'" & objModel.Exp_Amount & "', N'" & objModel.DocType.Replace("'", "''") & "') Select @@Identity"
                objModel.InwardExpDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
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

    Function Update(ByVal objModel As InwardExpenseDetailBE) As Boolean
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
    Function Update(ByVal objModel As InwardExpenseDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update InwardExpenseDetailTable set PurchaseId= N'" & objModel.PurchaseId & "', AccountId= N'" & objModel.AccountId & "', Exp_Amount= N'" & objModel.Exp_Amount & "', DocType= N'" & objModel.DocType.Replace("'", "''") & "' where InwardExpDetailId=" & objModel.InwardExpDetailId
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

    Function Delete(ByVal PurchaseId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(PurchaseId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal PurchaseId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete From InwardExpenseDetailTable WHERE PurchaseId=" & PurchaseId & " AND Doctype='VQ' "
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
            strSQL = " Select InwardExpDetailId, PurchaseId, AccountId, Exp_Amount, DocType from InwardExpenseDetailTable  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InwardExpDetailId, PurchaseId, AccountId, Exp_Amount, DocType from InwardExpenseDetailTable  where InwardExpDetailId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function ValidateIfExist(ByVal PurchaseId As Integer) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InwardExpDetailId  From InwardExpenseDetailTable WHERE PurchaseId= " & PurchaseId & " AND Doctype='VQ'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
