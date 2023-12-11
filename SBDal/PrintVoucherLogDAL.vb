Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class PrintVoucherLogDAL
    Public Shared Function Save(ByVal PrintLog As PrintVoucherLogBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = "INSERT INTO tblPrintVoucherLog(SaleManId, VoucherDate, UserName) Values(" & PrintLog.SaleManId & ", N'" & PrintLog.VoucherDate & "', N'" & PrintLog.UserName.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
