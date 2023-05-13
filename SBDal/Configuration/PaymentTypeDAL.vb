Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Public Class PaymentTypeDAL
    Public Enum enmPaymentType
        PayTypeId
        PaymentType
        Active
        SortOrder
    End Enum
    Public Function Save(ByVal Type As PaymentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblPaymentType(PaymentType, Active, SortOrder) VALUES(N'" & Type.PaymentType.Replace("'", "''") & "', " & IIf(Type.Active = True, 1, 0) & "," & Type.SortOrder & ")"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal Type As PaymentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update tblPaymentType SET PaymentType=N'" & Type.PaymentType.Replace("'", "''") & "', Active=" & IIf(Type.Active = True, 1, 0) & ", SortOrder=" & Type.SortOrder & "  WHERE PayTypeId=" & Type.PayTypeId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal Type As PaymentTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblPaymentType WHERE PayTypeId=" & Type.PayTypeId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select * From tblPaymentType")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
