Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class PaymentTermsScheduleDAL
    Public Function Save(ByVal PaySchedule As List(Of PaymentTermsScheduleBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblPaymentSchedule WHERE OrderId=" & PaySchedule(0).OrderId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            For Each Pay As PaymentTermsScheduleBE In PaySchedule
                strSQL = String.Empty
                'Ali Faisal : TFS1441 : Add new columns to have the initial schedule date and status of schedule for scheduled Payable 
                strSQL = "INSERT INTO tblPaymentSchedule(PayTypeId, SchDate, OrderId, OrderType, Amount, InitialSchDate, PaymentStatus) VALUES(" & Pay.PayTypeId & ", N'" & Pay.SchDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & Pay.OrderId & ", N'" & Pay.OrderType.Replace("'", "''") & "', " & Val(Pay.Amount) & ", N'" & Pay.SchDate.ToString("yyyy-M-d h:mm:ss tt") & "', 'UnPaid')"
                'Ali Faisal : TFS1441 : 
                SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            Next
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal PaySchedule As List(Of PaymentTermsScheduleBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblPaymentSchedule WHERE OrderId=" & PaySchedule(0).OrderId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetDetailRecords(ByVal OrderId As Integer, ByVal OrderType As String) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT S.SchDate, T.PayTypeId, T.PaymentType, S.Amount, S.OrderId, S.OrderType FROM dbo.tblPaymentType AS T INNER JOIN dbo.tblPaymentSchedule AS S ON T.PayTypeId = S.PayTypeId WHERE(S.OrderId = " & OrderId & " AND S.OrderType=N'" & OrderType.Replace("'", "''") & "')"
            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckExistingData(ByVal OrderId As Integer, ByVal OrderType As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select * From tblPaymentSchedule WHERE OrderId=" & OrderId & " AND OrderType=N'" & OrderType.Replace("'", "''") & "'"
            If (SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)) > 0 Then
                objTrans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
